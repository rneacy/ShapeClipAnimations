using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace ShapeClipController
{
    static class Util
    {
        public static string[] GetSerialPorts()
        {
            // code to remove unusable serial ports...
            var ports = SerialPort.GetPortNames();
            return ports;
        }

        public static string[] OpenSCA()
        {
            var fileName = string.Empty;
            var fileContent = string.Empty;

            // Opens a file and returns the contents as a string.
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "/..";
                openFileDialog.DefaultExt = ".sca";
                openFileDialog.Filter = "ShapeClip Animation | *.sca";
                openFileDialog.Title = "Open Animation";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileStream = openFileDialog.OpenFile();
                    fileName = openFileDialog.SafeFileName;

                    using (var reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            return new [] {fileName, fileContent};
        }
    }
}
