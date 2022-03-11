using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;


namespace WinFormsApp2
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string line, lineall, entryconverted, entry;

        private void button2_Click(object sender, EventArgs e)
        {
            Process command = new Process();
            command.StartInfo.FileName = "powershell.exe";
            command.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            command.StartInfo.UseShellExecute = false;
            command.StartInfo.RedirectStandardOutput = true;
            command.StartInfo.CreateNoWindow = true;
            command.StartInfo.Arguments = "Get-PnpDevice -Status ERROR | select FriendlyName -ExpandProperty Name | ft -hide";
            command.Start();

            //save Device Name into string -> show list
            while (!command.StandardOutput.EndOfStream)
            {
                lineall = command.StandardOutput.ReadToEnd();
                richTextBox1.Text = lineall;
            }

            //Save log files to put them into comboboxe's 
            command.StartInfo.Arguments = "Get-PnpDevice -Status ERROR | select FriendlyName -ExpandProperty Name | ft -hide > name.log";
            command.Start();

            command.StartInfo.Arguments = "Get-PnpDevice -Status ERROR | select InstanceId | findstr /c:VEN_ /c:VID_ > id.log";
            command.Start();

            //save Device Instance ID into String -> button Download
            command.StartInfo.Arguments = "Get-PnpDevice -Status ERROR | select InstanceId | findstr /c:VEN_ /c:VID_";
            command.Start();
            while (!command.StandardOutput.EndOfStream)
            {
                line = command.StandardOutput.ReadLine();
            }

            if (line == " " || line == "")
            {
                richTextBox1.Text = "Nothing to do here.";
                MessageBox.Show("No drivers required to be installed were found.","Information");

                command.StartInfo.Arguments = "rm id.log; rm name.log";
                command.Start();
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = comboBox1.SelectedIndex;
            entry = comboBox2.GetItemText(comboBox2.SelectedItem);
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void button3_MouseClick(object sender, MouseEventArgs e)
        {
            Process command = new Process();
            command.StartInfo.FileName = "powershell.exe";
            command.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            command.StartInfo.UseShellExecute = false;
            command.StartInfo.RedirectStandardOutput = true;
            command.StartInfo.CreateNoWindow = true;
            command.StartInfo.Arguments = "rm id.log; rm name.log";
            command.Start();
            Application.Exit();
        }

        //Panel = Mouse Drag (Flat GUI)
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            StreamReader sr = File.OpenText("name.log");
            string s = "";
            comboBox1.Items.Clear();
            while ((s = sr.ReadLine()) != null)
            {
                comboBox1.Items.Add(s);
            }
            sr.Close();

            StreamReader sr2 = File.OpenText("id.log");
            s = "";
            comboBox2.Items.Clear();
            while ((s = sr2.ReadLine()) != null)
            {
                comboBox2.Items.Add(s);
            }
            sr2.Close();
        }

        public void Form1_Load(object sender, EventArgs e)
        {

            button1.BackColor = Color.FromArgb(87, 101, 116);
            button2.BackColor = Color.FromArgb(44, 62, 80);

            Process command = new Process();
            command.StartInfo.FileName = "powershell.exe";
            command.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            command.StartInfo.UseShellExecute = false;
            command.StartInfo.RedirectStandardOutput = true;
            command.StartInfo.CreateNoWindow = true;
            command.StartInfo.Arguments = "Get-PnpDevice -Status ERROR | select FriendlyName -ExpandProperty Name | ft -hide";
            command.Start();

            //save Device Name into string -> show list
            while (!command.StandardOutput.EndOfStream)
            {
                lineall = command.StandardOutput.ReadToEnd();
                richTextBox1.Text = lineall;
            }

            //Save log files to put them into comboboxe's 
            command.StartInfo.Arguments = "Get-PnpDevice -Status ERROR | select FriendlyName -ExpandProperty Name | ft -hide > name.log";
            command.Start();

            command.StartInfo.Arguments = "Get-PnpDevice -Status ERROR | select InstanceId | findstr /c:VEN_ /c:VID_ > id.log";
            command.Start();

            //save Device Instance ID into String -> button Download
            command.StartInfo.Arguments = "Get-PnpDevice -Status ERROR | select InstanceId | findstr /c:VEN_ /c:VID_";
            command.Start();
            while (!command.StandardOutput.EndOfStream)
            {
                line = command.StandardOutput.ReadLine();
            }

            if (line == " " || line == "")
            {
                MessageBox.Show("No drivers required to be installed were found.", "Information");
                richTextBox1.Text = "Nothing to do here.";

                command.StartInfo.Arguments = "rm id.log; rm name.log";
                command.Start();

                Application.Exit();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {


                //Detecting device type
                string[] PCI = new string[]
                    { "PCI\\" };
                string[] USB_HID = new string[]
                {
                    "USB\\",
                    "HID\\"
                };

                foreach(string s in PCI)
                {
                    if (entry.StartsWith(s))
                    {
                        //delete all before VEN_
                        int pos = entry.IndexOf("VEN_");
                        if (pos >= 0)
                        {
                            entryconverted = entry.Remove(0, pos);
                        }

                        //delete all after &SUBSYS
                        pos = entryconverted.IndexOf("&SUBSYS");
                        if (pos >= 0)
                        {
                            entry = entryconverted.Remove(pos);
                        }
                    }
                }

                foreach (string s in USB_HID)
                {
                    if (entry.StartsWith(s))
                    {
                        //delete all before VEN_
                        int pos = entry.IndexOf("VID_");
                        if (pos >= 0)
                        {
                            entryconverted = entry.Remove(0, pos);
                        }

                        //delete all after &SUBSYS
                        pos = entryconverted.IndexOf("&MI");
                        if (pos >= 0)
                        {
                            entry = entryconverted.Remove(pos);
                        }
                    }
                }





                //replace & symbol between VEN_ and DEV_ ids for URL entry
                entryconverted = entry.Replace("&", "%26");

                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://download-drivers.net/search?q=" + entryconverted,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Choose the driver you want to download.", "Information");
            }
        }

    }
}
