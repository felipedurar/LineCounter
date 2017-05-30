using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Durar_Line_Counter
{
    public partial class FilteredDirectory : Form
    {
        public List<string> Directory = new List<string>();
        public List<string> Exts = new List<string>();
        public bool UseFilter = true;
        public bool IncludeSubdirectories = true;

        public FilteredDirectory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add a folder
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listView1.Items.Add(fbd.SelectedPath);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Remove Directories
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                listView1.Items.Remove(lvi);
            }
            //
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Disable File Filter
            groupBox2.Enabled = checkBox1.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Remove Filters
            foreach (ListViewItem lvi in listView2.SelectedItems)
            {
                listView2.Items.Remove(lvi);
            }
            //
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Add a filter case it doesn't exists
            foreach (ListViewItem lvi in listView2.Items)
            {
                if (lvi.Text == textBox1.Text)
                {
                    return;
                }
            }
            //
            listView2.Items.Add(textBox1.Text);
            textBox1.Text = ".";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Cancel
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Stores the data in public variables accessible by Form1
            foreach (ListViewItem lvi in listView1.Items)
            {
                Directory.Add(lvi.Text);
            }
            foreach (ListViewItem lvi in listView2.Items)
            {
                Exts.Add(lvi.Text);
            }
            UseFilter = checkBox1.Checked;
            IncludeSubdirectories = checkBox2.Checked;
            // And close as OK
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
