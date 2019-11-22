using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeClipController
{
    public partial class MainWindow : Form
    {

        private int _toggleBtnState = 0;

        public MainWindow()
        {
            InitializeComponent();
            var ports = Util.GetSerialPorts();

            foreach (string port in ports)
            {
                Console.WriteLine(port);
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
    }
}
