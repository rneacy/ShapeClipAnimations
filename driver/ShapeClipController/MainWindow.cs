﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ShapeClipController
{
    public partial class MainWindow : Form
    {
        private string Port => comSelectBox.SelectedItem.ToString();
        private int IntendedClips => int.Parse(clipCountLabel.Text);
        private int ConnectedClips => int.Parse(clipCountInput.Text);
        private string _selectedFile;
        private int _toggleBtnState = 0;
        private Dictionary<string, string> _loadedAnims = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();
            GetSerialPorts();
            delayText.Text = DelayTime(int.Parse(clipCountInput.Text)).ToString() + "ms";
        }

        private void toggleButton_Click(object sender, EventArgs e)
        {
            _toggleBtnState = (_toggleBtnState > 0) ? 0 : 1;

            toggleButton.Text = (_toggleBtnState == 0)
                ? Properties.Resources.TOGGLEBTN_START
                : Properties.Resources.TOGGLEBTN_STOP;

            uploadButton.Enabled = (_toggleBtnState == 0);
            pauseButton.Enabled = (_toggleBtnState == 1);

            var serial = new Serial(Port)
            {
                Baud = 9600
            };

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
            string selected;
            try
            {
                selected = animationList.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                selected = "";
            }

            if (!selected.Equals(""))
            {
                removeAnimButton.Enabled = (animationList.Items.Count != 0);
                uploadButton.Enabled = removeAnimButton.Enabled;

                clipCountLabel.Text = CalculateClipCount(_loadedAnims[selected]).ToString();
                animSizeLabel.Text = CalculateAnimSize(_loadedAnims[selected]).ToString();
                durationLabel.Text = CalculateAnimDuration(_loadedAnims[selected]).ToString("##.#") + "s";
            }
            else
            {
                removeAnimButton.Enabled = false;
                uploadButton.Enabled = false;
                clipCountLabel.Text = "0";
            }
        }

        private static int CalculateAnimSize(string anim) => anim.Split('\n').Length;
        private static int CalculateClipCount(string anim) => anim.Split('\n').Max(el => int.Parse(el.Split(':')[0]));

        private static double CalculateAnimDuration(string anim)
        {
            var maxMoveTime = 450d;

            // calc for each node
            var clips = Enumerable.Range(1, CalculateClipCount(anim));
            var lines = anim.Split('\n');
            var longest = 0d;

            foreach (var clip in clips)
            {
                var clipTotalMillis = 0d;
                var currentDelay = 0d;

                // do for each time. if clip total millis is bigger than the delay of each one, then treat as consec
                // i.e. don't add the delay on.
                foreach (var node in lines)
                {
                    var vals = node.Split(':');

                    // if correct clip
                    if (int.Parse(vals[0]) == clip)
                    {
                        var moveTime = maxMoveTime / (double.Parse(vals[2]) / 100);
                        var delay = double.Parse(vals[6].Split('.')[0]);

                        clipTotalMillis += moveTime;

                        // 
                        if ((int)currentDelay != (int)delay)
                        {
                            if ((int) delay > (int) clipTotalMillis)
                            {
                                clipTotalMillis += delay - currentDelay;
                                currentDelay = clipTotalMillis;
                            }
                        }
                    }
                }

                if (clipTotalMillis > longest) longest = clipTotalMillis;
            }

            return longest / 1000d;
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            string selected = animationList.SelectedItem.ToString();
            if (!selected.Equals(""))
            {
                uploadButton.Enabled = false;

                if (IntendedClips > ConnectedClips)
                {
                    var title = "Not enough clips!";
                    var message =
                        "This animation specifies for " + IntendedClips +
                        " clips, but you only have " + ConnectedClips +
                        " connected.\n\nUpload anyway?";
                    var btns = MessageBoxButtons.YesNo;
                    var icon = MessageBoxIcon.Question;

                    var result = MessageBox.Show(message, title, btns, icon);

                    if (result == DialogResult.No)
                    {
                        uploadButton.Enabled = true;
                        return;
                    }
                } 

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
            var ports = Serial.SerialPorts;
            comSelectBox.Items.Clear();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private int DelayTime(int clips) => clips * 210;

        private void clipCountInput_TextChanged(object sender, EventArgs e)
        {
            if(clipCountInput.Text.Length > 0) delayText.Text = DelayTime(int.Parse(clipCountInput.Text)).ToString() + "ms";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void durationLabel_Click(object sender, EventArgs e)
        {

        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            var serial = new Serial(Port)
            {
                Baud = 9600
            };

            if (serial.Open())
            {
                serial.Send(Properties.Resources.OP_PAUSE);
                serial.Close();
            }
        }
    }
}
