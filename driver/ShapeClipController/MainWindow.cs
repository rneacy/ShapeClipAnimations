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
        private int _toggleBtnState = 0;
        private Dictionary<string, string> _loadedAnims = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();
            var ports = Serial.GetSerialPorts();

            foreach (string port in ports)
            {
                comSelectBox.Items.Add(port);
            }

            comSelectBox.SelectedItem = comSelectBox.Items[0];
        }

        private void toggleButton_Click(object sender, EventArgs e)
        {
            _toggleBtnState = (_toggleBtnState > 0) ? 0 : 1;

            toggleButton.Text = (_toggleBtnState == 0)
                ? Properties.Resources.TOGGLEBTN_START
                : Properties.Resources.TOGGLEBTN_STOP;
        }

        private void addAnimButton_Click(object sender, EventArgs e)
        {
            var file = Util.OpenSCA();

            if (!file[0].Equals(""))
            {
                if (!animationList.Items.Contains(file[0]))
                {
                    var item = file[0];
                    var regex = Regex.Match(item, "^.*(?=(\\.sca))");

                    animationList.Items.Add(regex.Value);
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
                uploadButton.Text = Properties.Resources.UPLOADBTN_UPLOADING;

                var serial = new Serial(comSelectBox.SelectedItem.ToString());

                serial.Open();
                {
                    var sent = serial.Send(_loadedAnims[selected + ".sca"]);
                    if (!sent)
                    {
                        var title = "Oops!";
                        var message = "There was a problem with serial communications.";
                        var btns = MessageBoxButtons.OK;
                        var icon = MessageBoxIcon.Error;

                        MessageBox.Show(message, title, btns, icon);
                    }
                    while (serial.Reading) { }
                }
                serial.Close();

                uploadButton.Enabled = true;
                uploadButton.Text = Properties.Resources.UPLOADBTN_UPLOAD;
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
            _loadedAnims.Remove(selected + ".sca");
            animationList.Items.RemoveAt(animationList.SelectedIndex);
        }
    }
}
