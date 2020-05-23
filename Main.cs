using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Registry;
using System.IO;
using System;

namespace RegistryReader
{
    public partial class Main : Form
    {
        string USBSTOR = @"ROOT\ControlSet001\Enum\USBSTOR";
        string dateKey = "{83da6326-97a6-4088-9453-a1923f573b29}";
        string dateValue = "(default)";
        string mountedDevices = @"ROOT\MountedDevices";

        private RegistryHiveOnDemand _hive;

        private OptionSelector _options;

        public Main()
        {
            InitializeComponent();

            _options = new OptionSelector();

            btnStart.Click += BtnStart_Click;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            _options.ShowDialog();

            string hivePath = _options.SystemHive;

            if (hivePath == null || hivePath == "")
                return;

            _hive = new RegistryHiveOnDemand(hivePath);

            ParseUSBStor();

            if (_options.LiveSystem)
                File.Delete(_options.SystemHive);
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
                    dev.FriendlyName = GetValueByKey(serial.KeyPath, "FriendlyName");

                    key = _hive.GetKey(serial.KeyPath + "\\Properties\\" + dateKey);

                    var firstInKey = key.SubKeys.FirstOrDefault(x => x.KeyName == "0064");
                    var lastInKey = key.SubKeys.FirstOrDefault(x => x.KeyName == "0066");
                    var lastRemKey = key.SubKeys.FirstOrDefault(x => x.KeyName == "0067");

                    string firstIn = GetValueByKey(firstInKey.KeyPath, dateValue);
                    string lastIn = GetValueByKey(lastInKey.KeyPath, dateValue);
                    string lastRem = GetValueByKey(lastRemKey.KeyPath, dateValue);

                    dev.SetFirstInsertion(firstIn);
                    dev.SetLastInsertion(lastIn);
                    dev.SetLastRemoval(lastRem);

                    var mountedDevsEntries = GetKeysByValue(mountedDevices, serial.KeyName);

                    // This list should contain 2 elements, one for the drive letter, another for the GUID
                    foreach (string entry in mountedDevsEntries)
                    {
                        if (entry.StartsWith("\\??\\"))
                        {
                            dev.SetDeviceGUID(entry);
                        }
                        else
                        {
                            dev.SetDriveLetter(entry);
                        }
                    }

                    devices.Add(dev);
                }
            }

            SetGridSource(devices);
        }

        private string GetValueByKey(string keyPath, string valueName)
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

        private List<string> GetKeysByValue(string keyPath, string valueData)
        {
            List<string> result = new List<string>();

            Registry.Abstractions.RegistryKey key = _hive.GetKey(keyPath);

            if (key != null)
            {
                foreach (var item in key.Values)
                {
                    string s = item.ValueType == "RegBinary" ?
                        System.Text.Encoding.Unicode.GetString(item.ValueDataRaw) : item.ValueData;

                    if (s.Contains(valueData))
                    {
                        result.Add(item.ValueName);
                    }
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
    }
}
