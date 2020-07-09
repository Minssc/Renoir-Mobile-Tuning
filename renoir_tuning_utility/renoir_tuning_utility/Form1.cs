using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace renoir_tuning_utility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            upDownTctlTemp.Enabled = checkTctlTemp.Checked = Properties.Settings.Default.TCTL_E;
            upDownStapmLimit.Enabled = checkStapmLimit.Checked = Properties.Settings.Default.PL0_E;
            upDownSlowTime.Enabled = checkSlowTime.Checked = Properties.Settings.Default.PL1T_E;
            upDownStapmTime.Enabled = checkStapmTime.Checked = Properties.Settings.Default.PL0T_E;
            upDownCurrentLimit.Enabled = checkCurrentLimit.Checked = Properties.Settings.Default.TDC_E;
            upDownMaxCurrentLimit.Enabled = checkMaxCurrentLimit.Checked = Properties.Settings.Default.EDC_E;
            upDownSlowLimit.Enabled = checkSlowLimit.Checked = Properties.Settings.Default.PL1_E;
            upDownFastLimit.Enabled = checkFastLimit.Checked = Properties.Settings.Default.PL2_E;

            upDownTctlTemp.Value = Properties.Settings.Default.TCTL;
            upDownStapmLimit.Value = Properties.Settings.Default.PL0;
            upDownSlowTime.Value = Properties.Settings.Default.PL1T;
            upDownStapmTime.Value = Properties.Settings.Default.PL0T;
            upDownCurrentLimit.Value = Properties.Settings.Default.TDC;
            upDownMaxCurrentLimit.Value = Properties.Settings.Default.EDC;
            upDownSlowLimit.Value = Properties.Settings.Default.PL1;
            upDownFastLimit.Value = Properties.Settings.Default.PL2; 
        }

        private void ApplySettings_Click(object sender, EventArgs e)
        {
            String args;
            String exe = Directory.GetCurrentDirectory() + "\\smu-tool\\smu-tool.exe";
            
            Properties.Settings.Default.PL0 = upDownStapmLimit.Value; 
            Properties.Settings.Default.PL2 = upDownFastLimit.Value; 
            Properties.Settings.Default.PL1 = upDownSlowLimit.Value; 
            Properties.Settings.Default.PL1T = upDownSlowTime.Value; 
            Properties.Settings.Default.PL0T = upDownStapmTime.Value; 
            Properties.Settings.Default.TCTL = upDownTctlTemp.Value; 
            Properties.Settings.Default.TDC = upDownCurrentLimit.Value; 
            Properties.Settings.Default.EDC = upDownMaxCurrentLimit.Value; 
            
            if (checkStapmLimit.Checked)
            {
                args = String.Format("--message=0x14 --arg0={0:0}000", upDownStapmLimit.Value);
                var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkFastLimit.Checked)
            {
                args = String.Format("--message=0x15 --arg0={0:0}000", upDownFastLimit.Value);
                var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkSlowLimit.Checked)
            {
                args = String.Format("--message=0x16 --arg0={0:0}000", upDownSlowLimit.Value);
                var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkSlowTime.Checked)
            {
                args = String.Format("--message=0x17 --arg0={0:0}", upDownSlowTime.Value);
                var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkStapmTime.Checked)
            {
                args = String.Format("--message=0x18 --arg0={0:0}", upDownStapmTime.Value);
                var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkTctlTemp.Checked)
            {
                args = String.Format("--message=0x19 --arg0={0:0}", upDownTctlTemp.Value);
                var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkCurrentLimit.Checked)
            {
                args = String.Format("--message=0x1A --arg0={0:0}000", upDownCurrentLimit.Value);
                var proc = System.Diagnostics.Process.Start(exe, args);
            }

            if (checkMaxCurrentLimit.Checked)
            {
                args = String.Format("--message=0x1C --arg0={0:0}000", upDownMaxCurrentLimit.Value);
                var proc = System.Diagnostics.Process.Start(exe, args);
            }
            
            Properties.Settings.Default.Save();
        }

        private void checkTctlTemp_CheckedChanged(object sender, EventArgs e)
        {
            upDownTctlTemp.Enabled = checkTctlTemp.Checked;
            Properties.Settings.Default.TCTL_E = checkTctlTemp.Checked; 
        }

        private void checkStapmLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownStapmLimit.Enabled = checkStapmLimit.Checked;
            Properties.Settings.Default.PL0_E = checkStapmLimit.Checked;
        }

        private void checkSlowTime_CheckedChanged(object sender, EventArgs e)
        {
            upDownSlowTime.Enabled = checkSlowTime.Checked;
            Properties.Settings.Default.PL1T_E = checkSlowTime.Checked; 
        }

        private void checkStapmTime_CheckedChanged(object sender, EventArgs e)
        {
            upDownStapmTime.Enabled = checkStapmTime.Checked;
            Properties.Settings.Default.PL0T_E = checkStapmTime.Checked; 
        }

        private void checkCurrentLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownCurrentLimit.Enabled = checkCurrentLimit.Checked;
            Properties.Settings.Default.TDC_E = checkCurrentLimit.Checked; 
        }

        private void checkMaxCurrentLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownMaxCurrentLimit.Enabled = checkMaxCurrentLimit.Checked;
            Properties.Settings.Default.EDC_E = checkMaxCurrentLimit.Checked; 
        }

        private void checkSlowLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownSlowLimit.Enabled = checkSlowLimit.Checked;
            Properties.Settings.Default.PL1_E = checkSlowLimit.Checked; 
        }

        private void checkFastLimit_CheckedChanged(object sender, EventArgs e)
        {
            upDownFastLimit.Enabled = checkFastLimit.Checked;
            Properties.Settings.Default.PL2_E = checkFastLimit.Checked; 
        }

        private void upDownSlowLimit_ValueChanged(object sender, EventArgs e)
        {
            if (upDownFastLimit.Value < upDownSlowLimit.Value)
            {
                upDownFastLimit.Value = upDownSlowLimit.Value;
            }
            if (upDownSlowLimit.Value < upDownStapmLimit.Value)
            {
                upDownStapmLimit.Value = upDownSlowLimit.Value;
                upDownStapmLimit_ValueChanged(sender, e);
            }

            if (!checkSlowLimit.Checked && Properties.Settings.Default.PL1 != upDownSlowLimit.Value)
            {
                Properties.Settings.Default.PL1_E = checkSlowLimit.Checked = true;
            }
        }

        private void upDownStapmLimit_ValueChanged(object sender, EventArgs e)
        {
            if (upDownFastLimit.Value < upDownStapmLimit.Value)
            {
                upDownFastLimit.Value = upDownStapmLimit.Value;
            }
            if (upDownSlowLimit.Value < upDownStapmLimit.Value)
            {
                upDownSlowLimit.Value = upDownStapmLimit.Value;
            }
            if (upDownCurrentLimit.Value < upDownStapmLimit.Value)
            {
                upDownCurrentLimit.Value = upDownStapmLimit.Value;
            }
            if (upDownMaxCurrentLimit.Value < upDownStapmLimit.Value + 15)
            {
                upDownMaxCurrentLimit.Value = upDownStapmLimit.Value + 15;
            }

            if(!checkStapmLimit.Checked && Properties.Settings.Default.PL0 != upDownStapmLimit.Value)
            {
                Properties.Settings.Default.PL0_E = checkStapmLimit.Checked = true; 
            }
        }

        private void upDownSlowTime_ValueChanged(object sender, EventArgs e)
        {
            if(Math.Round(upDownSlowTime.Value * 2) > upDownStapmTime.Value)
            {
                upDownStapmTime.Value = upDownSlowTime.Value * 2;
            }

            if(!checkSlowTime.Checked && Properties.Settings.Default.PL1T != upDownSlowTime.Value)
            {
                Properties.Settings.Default.PL1T_E = checkSlowTime.Checked = true; 
            }
        }

        private void upDownStapmTime_ValueChanged(object sender, EventArgs e)
        {
            if (Math.Round(upDownSlowTime.Value * 2) > upDownStapmTime.Value)
            {
                upDownSlowTime.Value = Math.Round(upDownStapmTime.Value / 2);
            }

            if(!checkStapmTime.Checked && Properties.Settings.Default.PL0T != upDownStapmTime.Value)
            {
                Properties.Settings.Default.PL0T_E = checkStapmTime.Checked = true; 
            }
        }

        private void upDownCurrentLimit_ValueChanged(object sender, EventArgs e)
        {
            if(upDownCurrentLimit.Value + 15 > upDownMaxCurrentLimit.Value)
            {
                upDownMaxCurrentLimit.Value = upDownCurrentLimit.Value + 15;
            }
            if (upDownCurrentLimit.Value < upDownStapmLimit.Value)
            {
                upDownStapmLimit.Value = upDownCurrentLimit.Value;
            }

            if(!checkCurrentLimit.Checked && Properties.Settings.Default.TDC != upDownCurrentLimit.Value)
            {
                Properties.Settings.Default.TDC_E = checkCurrentLimit.Checked = true; 
            }
        }

        private void upDownMaxCurrentLimit_ValueChanged(object sender, EventArgs e)
        {
            if(upDownCurrentLimit.Value + 15 > upDownMaxCurrentLimit.Value)
            {
                upDownCurrentLimit.Value = upDownMaxCurrentLimit.Value - 15;
            }
            if (upDownCurrentLimit.Value < upDownStapmLimit.Value)
            {
                upDownStapmLimit.Value = upDownCurrentLimit.Value;
            }

            if (!checkMaxCurrentLimit.Checked && Properties.Settings.Default.EDC != upDownMaxCurrentLimit.Value)
            {
                Properties.Settings.Default.EDC_E = checkMaxCurrentLimit.Checked = true; 
            }
        }

        private void upDownTctlTemp_ValueChanged(object sender, EventArgs e)
        {
            if (!checkTctlTemp.Checked && Properties.Settings.Default.TCTL != upDownTctlTemp.Value)
            {
                Properties.Settings.Default.TCTL_E = checkTctlTemp.Checked = true; 
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)  // PL2 
        {
            if (upDownFastLimit.Value < upDownSlowLimit.Value)
            {
                upDownSlowLimit.Value = upDownFastLimit.Value;
            }
            if (upDownFastLimit.Value < upDownStapmLimit.Value)
            {
                upDownStapmLimit.Value = upDownFastLimit.Value;
                upDownStapmLimit_ValueChanged(sender, e);
            }

            if (!checkFastLimit.Checked && Properties.Settings.Default.PL2 != upDownFastLimit.Value)
            {
                Properties.Settings.Default.PL2_E = checkFastLimit.Checked = true;
            }
        }

        private void checkMinimizeToTray_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void notifyIconRMT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIconRMT.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
