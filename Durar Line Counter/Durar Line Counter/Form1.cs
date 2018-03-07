using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Durar_Line_Counter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Add File to the list
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ofd.FileName;
                listView1.Items.Add(lvi);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Add a list of files from a directory with determined rules
            label2.Visible = false;
            //
            FilteredDirectory fd = new FilteredDirectory();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string dir in fd.Directory)
                {
                    // Get Files
                    string[] fileList = null;
                    if (fd.IncludeSubdirectories)
                    {
                        try
                        {
                            // Load the files
                            fileList = Directory.GetFiles(dir, "*.*", System.IO.SearchOption.AllDirectories);
                        }
                        catch (UnauthorizedAccessException uae)
                        {
                            label2.Text = "Note: Some directories could not be accessed due to unauthorized access!";
                            label2.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            label2.Text = "Note: Some error has occurred while loading the directory!";
                            label2.Visible = true;
                        }
                    }
                    else
                    {
                        try
                        {
                            // Load the files
                            fileList = Directory.GetFiles(dir, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                        }
                        catch (UnauthorizedAccessException uae)
                        {
                            label2.Text = "Note: Some directories could not be accessed due to unauthorized access!";
                            label2.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            label2.Text = "Note: Some error has occurred while loading the directory!";
                            label2.Visible = true;
                        }
                    }

                    if (fd.UseFilter && fileList != null)
                    {
                        // Filter and Add Files
                        foreach (string cFile in fileList)
                        {
                            foreach (string ext in fd.Exts)
                            {
                                try
                                {
                                    if (Path.GetExtension(cFile) == ext)
                                    {
                                        listView1.Items.Add(cFile);
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    label2.Text = "Note: Some error has occurred checking the file extension!";
                                    label2.Visible = true;
                                    continue;
                                }
                            }
                        }
                    }
                    else if (fileList != null)
                    {
                        // Add Files
                        foreach (string cFile in fileList)
                        {
                            listView1.Items.Add(cFile);
                        }
                    }
                }
                //
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Remove files from the list
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                listView1.Items.Remove(lvi);
            }
            //
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Pass the file list to the other form and show it
            Result res = new Result();
            foreach (ListViewItem lvi in listView1.Items)
            {
                res.FileList.Add(lvi.Text);
            }
            res.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the form
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show About
            MessageBox.Show("Durar Line Counter 1.0\nCoded by: Felipe Durar\nCopyright (C) Felipe Durar 2017");
        }
    }
}
