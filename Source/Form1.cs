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

namespace W10_Installation_Fixer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("It is suggested that you close all other windows when running this, as you could lose progress on any open applications. After completing, your PC will reboot, are you sure you are ready to proceed?", 
                "Win10 Fixer", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                bool instED = checkBox1.Checked;
                int Browser = comboBox1.SelectedIndex;
                Program.RunScripts(Browser, instED);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This is only recommended for advanced users, are you sure you wish to continue?", "W10 Installation Fixer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Process editor = new Process();
                editor.StartInfo = new ProcessStartInfo("notepad.exe", "C:/temp/Win10_Fix/Auto_Decrapify.ps1");
                editor.Start();
            } else {
                //Do nothing
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Directory.Delete("C:/temp/Wind10_Fixer", true);
        }
    }
}