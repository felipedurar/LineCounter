using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Durar_Line_Counter
{
    public partial class Result : Form
    {
        public List<string> FileList = new List<string>();
        //
        private bool Once = false;
        private bool Running = true;
        //
        private int FileCounter = 0;
        private int LineCounter = 0;
        private int EmptyLinesCounter = 0;

        public Result()
        {
            InitializeComponent();
        }

        private void Result_Load(object sender, EventArgs e)
        {

        }

        public void Count()
        {
            // Counter
            // Set the progress bar maximum
            progressBar1.Maximum = FileList.Count;
            // Do The count in another thread avoiding freezes the current thread
            Thread cTh = new Thread(() =>
            {
                foreach (string cFile in FileList)
                {
                    // Stops when click in cancel
                    if (!Running) break;
                    // Check if file exists
                    if (!File.Exists(cFile))
                    {
                        // GUI
                        Invoke((MethodInvoker)(() =>
                            {
                                label5.Text = "Note: Some files was not found!";
                                label5.Visible = true;
                            }));
                        continue;
                    }
                    // Increase file Counter
                    FileCounter++;
                    // Line Coutner
                    try
                    {
                        StreamReader sr = new StreamReader(cFile);
                        string cLine = string.Empty;
                        while ((cLine = sr.ReadLine()) != null)
                        {
                            // Stops when click in cancel
                            if (!Running) break;
                            //
                            LineCounter++;
                            if (cLine.Trim().Length == 0) EmptyLinesCounter++;
                        }
                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        // GUI
                        Invoke((MethodInvoker)(() =>
                        {
                            label5.Text = "Note: An error occurred while counting!";
                            label5.Visible = true;
                        }));
                        break;
                    }
                    // GUI
                    Invoke((MethodInvoker)(() =>
                        {
                            label1.Text = "Files: " + FileCounter.ToString();
                            label2.Text = "Lines: " + LineCounter.ToString();
                            label3.Text = "Empty Lines: " + EmptyLinesCounter.ToString();
                            label4.Text = "Lines Without Empty Lines: " + (LineCounter - EmptyLinesCounter).ToString();
                            //
                            progressBar1.Value = FileCounter;
                        }));
                }
                //
                // GUI
                Invoke((MethodInvoker)(() =>
                    {
                        Running = false;
                        button1.Text = "OK";
                        MessageBox.Show("Counter Finished!");
                    }));
            });
            cTh.IsBackground = true;
            cTh.Start();
        }

        private void Result_Shown(object sender, EventArgs e)
        {
            // To make sure the count is run once
            if (Once) return;
            Once = true;
            //
            Count();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // OK and Cancel behavior
            if (button1.Text == "OK")
            {
                this.Close();
            }
            else
            {
                Running = false;
                button1.Text = "OK";
            }
        }
    }
}
