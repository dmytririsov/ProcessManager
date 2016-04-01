using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; 

namespace ProcessManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var processList = Process.GetProcesses();
            foreach (var proc in processList)
                listBox1.Items.Add(String.Format("{0}.exe",proc.ProcessName));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var taskName = listBox1.Items[listBox1.SelectedIndex].ToString();
                taskName = taskName.Substring(0, taskName.Length - 4);
                try
                {
                    foreach (var procToKill in Process.GetProcessesByName(taskName))
                        procToKill.Kill();
                }
                catch(Win32Exception)
                {
                    MessageBox.Show("Отказано в доступе");
                }
                button1.PerformClick();
                
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            foreach (Process winProc in Process.GetProcesses())
            {
                string curProc = listBox1.Items[listBox1.SelectedIndex].ToString();
                int startindex = curProc.Length;
                curProc = curProc.Remove(startindex - 4, 4);
                if (winProc.ProcessName == curProc)
                    MessageBox.Show(listBox1.Items[listBox1.SelectedIndex].ToString() + " id: " + winProc.Id);
            }
        }
    }
}
