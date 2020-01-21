using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeClipController
{
    public partial class MainWindow : Form
    {
        private string Port => comSelectBox.SelectedItem.ToString();
        private string _selectedFile;
        private int _toggleBtnState = 0;
        private Dictionary<string, string> _loadedAnims = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();
            GetSerialPorts();
        }

        private void toggleButton_Click(object sender, EventArgs e)
        {
            _toggleBtnState = (_toggleBtnState > 0) ? 0 : 1;

            toggleButton.Text = (_toggleBtnState == 0)
                ? Properties.Resources.TOGGLEBTN_START
                : Properties.Resources.TOGGLEBTN_STOP;

            var serial = new Serial(Port);

            if (serial.Open())
            {
                serial.Send(Properties.Resources.OP_EXEC);
                serial.Close();
            }
        }

        private void addAnimButton_Click(object sender, EventArgs e)
        {
            var files = Sca.OpenSca();

            foreach (var file in files)
            {
                if (!file[0].Equals(""))
                {
                    if (!animationList.Items.Contains(file[0]))
                    {
                        animationList.Items.Add(file[0]);
                        _loadedAnims.Add(file[0], file[1]);
                    }
                    else
                    {
                        var title = "Oops!";
                        var message = "You've already added animation '" + file[0] + "'.";
                        var btns = MessageBoxButtons.OK;
                        var icon = MessageBoxIcon.Exclamation;

                        MessageBox.Show(message, title, btns, icon);
                    }
                }
            }
        }

        private void animationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeAnimButton.Enabled = (animationList.Items.Count != 0);
            uploadButton.Enabled = removeAnimButton.Enabled;
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            string selected = animationList.SelectedItem.ToString();
            if (!selected.Equals(""))
            {
                uploadButton.Enabled = false;

                uploadButton.Text = Properties.Resources.UPLOADBTN_VERIFYING;
                if (Sca.VerifySca(_loadedAnims[selected]))
                {
                    uploadButton.Text = Properties.Resources.UPLOADBTN_UPLOADING;

                    var serial = new Serial(Port)
                    {
                        ExplicitClose = true
                    };

                    if (serial.Open())
                    {
                        // Set the ShapeClip to Upload Mode.
                        serial.Send(Properties.Resources.OP_UPLOAD);

                        // The ShapeClip should acknowledge that we are trying to upload to it.
                        // If serial timed out, for some reason the ShapeClip isn't responding.
                        // Otherwise, send the animation over once ShapeClip is in Upload Mode.
                        /*
                        if (serial.LastReadEmpty)
                        {
                            serial.SendAndRead(_loadedAnims[selected], false);
                            sent = true;
                        }*/

                        serial.Close();
                        serialMarshal.Start();
                        _selectedFile = selected;
                    }
                }
                else
                {
                    var title = "Oops!";
                    var message = "Your SCA file was incorrectly formatted.";
                    var btns = MessageBoxButtons.OK;
                    var icon = MessageBoxIcon.Error;

                    MessageBox.Show(message, title, btns, icon);
                }
            }
            else
            {
                var title = "Oops!";
                var message = "Invalid animation.";
                var btns = MessageBoxButtons.OK;
                var icon = MessageBoxIcon.Error;

                MessageBox.Show(message, title, btns, icon);
            }
        }

        private void removeAnimButton_Click(object sender, EventArgs e)
        {
            string selected = animationList.SelectedItem.ToString();
            _loadedAnims.Remove(selected);
            animationList.Items.RemoveAt(animationList.SelectedIndex);
        }

        private void comSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Text = Properties.Resources.TITLEBAR_MAIN + " | " + comSelectBox.SelectedItem;
        }

        private void serialPortRefreshButton_Click(object sender, EventArgs e)
        {
            GetSerialPorts();
        }

        private void GetSerialPorts()
        {
            var ports = Serial.GetSerialPorts();
            if (ports.Length != 0)
            {
                foreach (string port in ports)
                {
                    comSelectBox.Items.Add(port);
                }

                comSelectBox.SelectedItem = comSelectBox.Items[0];
                addAnimButton.Enabled = true;
            }
            else
            {
                Text = Properties.Resources.TITLEBAR_MAIN + " | " + Properties.Resources.TITLEBAR_NOPORT;
                addAnimButton.Enabled = false;
            }
        }

        private void serialMarshal_Tick(object sender, EventArgs e)
        {
            serialMarshal.Stop();

            var serial = new Serial(Port);
            if (serial.Open())
            {
                serial.Send(_loadedAnims[_selectedFile]);
                serial.Close();

                toggleButton.Enabled = true;
                uploadButton.Enabled = true;
                uploadButton.Text = Properties.Resources.UPLOADBTN_UPLOAD;
            }
        }
    }
}
