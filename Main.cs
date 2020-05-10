using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Registry;
using System.IO;
using System;
using System.Diagnostics;

namespace RegistryReader
{
    public partial class Main : Form
    {
        string USBSTOR = @"ROOT\ControlSet001\Enum\USBSTOR";
        string dateKey = "{83da6326-97a6-4088-9453-a1923f573b29}";
        string dateValue = "(default)";

        private RegistryHiveOnDemand _hive;

        public Main()
        {
            InitializeComponent();

            chkLive.CheckedChanged += ChkLive_CheckedChanged;
            btnStart.Click += BtnStart_Click;
        }

        private void ChkLive_CheckedChanged(object sender, EventArgs e)
        {
            txtPath.Enabled = !chkLive.Checked;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            string hivePath = GetHivePath();

            if (hivePath == null)
                return;

            _hive = new RegistryHiveOnDemand(hivePath);

            ParseUSBStor();

            File.Delete(txtPath.Text);
        }

        public void ParseUSBStor()
        {
            Registry.Abstractions.RegistryKey key = null;

            key = _hive.GetKey(USBSTOR);

            List<USBDevice> devices = new List<USBDevice>();

            foreach (var sub in key.SubKeys)
            {
                string description = sub.KeyName;

                key = _hive.GetKey(sub.KeyPath);

                foreach (var serial in key.SubKeys)
                {
                    USBDevice dev = new USBDevice();

                    dev.SetSerial(serial.KeyName);
                    dev.SetDescription(description);
                    dev.FriendlyName = GetValue(serial.KeyPath, "FriendlyName");

                    key = _hive.GetKey(serial.KeyPath + "\\Properties\\" + dateKey);

                    var firstInKey = key.SubKeys.FirstOrDefault(x => x.KeyName == "0064");
                    var lastInKey = key.SubKeys.FirstOrDefault(x => x.KeyName == "0066");
                    var lastRemKey = key.SubKeys.FirstOrDefault(x => x.KeyName == "0067");

                    string firstIn = GetValue(firstInKey.KeyPath, dateValue);
                    string lastIn = GetValue(lastInKey.KeyPath, dateValue);
                    string lastRem = GetValue(lastRemKey.KeyPath, dateValue);

                    dev.SetFirstInsertion(firstIn);
                    dev.SetLastInsertion(lastIn);
                    dev.SetLastRemoval(lastRem);

                    devices.Add(dev);
                }
            }

            SetGridSource(devices);
        }

        private string GetValue(string keyPath, string valueName)
        {
            string result = null;

            Registry.Abstractions.RegistryKey key = _hive.GetKey(keyPath);

            if (key != null)
            {
                var value = key.Values.FirstOrDefault(x => x.ValueName == valueName);

                if (value != null)
                {
                    result = value.ValueData;
                }
            }

            return result;
        }

        private void SetGridSource(List<USBDevice> devices)
        {
            grid.DataSource = devices;
            grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            grid.AllowUserToOrderColumns = true;
            grid.Font = new System.Drawing.Font("Consolas", 11F);

            grid.AutoResizeColumns();
        }

        private string GetHivePath()
        {
            if (!txtPath.Enabled)
            {
                GetSystemHive();
            }

            if (txtPath.Text != "")
                return txtPath.Text;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Hive File";
            ofd.InitialDirectory = @"C:\";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = ofd.FileName;
                return ofd.FileName;
            }
            else return null;
        }

        private void GetSystemHive()
        {
            string timestamp = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds.ToString();
            string savePath = Path.Combine(Environment.CurrentDirectory, timestamp);

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C reg save HKLM\\SYSTEM " + "\"" + savePath + "\"";
                startInfo.Verb = "runas";

                Process p = Process.Start(startInfo);

                while (!p.HasExited);
            }
            catch
            {
                savePath = null;
            }

            txtPath.Text = savePath;
        }
    }
}
