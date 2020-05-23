using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace RegistryReader
{
    public partial class OptionSelector : Form
    {
        public OptionSelector()
        {
            InitializeComponent();

            MaximumSize = MinimumSize = Size;

            btnDone.Click += BtnDone_Click;
            btnSys.Click += BtnSys_Click;
            chkLive.CheckedChanged += ChkLive_CheckedChanged;
        }

        private void BtnSys_Click(object sender, EventArgs e)
        {
            GetHivePath();
        }

        private void ChkLive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLive.Checked)
            {
                string path = GetSystemHive();

                txtPath.Text = path;
            }
        }

        private void BtnDone_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string SystemHive { get { return txtPath.Text; } }

        public string NTUserDat { get; private set; }

        public bool LiveSystem { get { return chkLive.Checked; } }

        private string GetHivePath()
        {
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

        private string GetSystemHive()
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

                while (!p.HasExited) ;
            }
            catch
            {
                savePath = null;
            }

            return savePath;
        }
    }
}
