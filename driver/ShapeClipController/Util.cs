using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ShapeClipController
{
    static class Util
    {
        public static List<string[]> OpenSCA()
        {
            var fileName = string.Empty;
            var fileContent = string.Empty;

            var loadedAnimations = new List<string[]>();

            // Opens a file and returns the contents as a string.
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "/..";
                openFileDialog.DefaultExt = ".sca";
                openFileDialog.Filter = "ShapeClip Animation | *.sca";
                openFileDialog.Title = "Open Animation";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in openFileDialog.FileNames)
                    {
                        var fileStream = File.Open(file, FileMode.Open);
                        fileName = Path.GetFileNameWithoutExtension(file);

                        using (var reader = new StreamReader(fileStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }

                        loadedAnimations.Add( new [] { fileName, fileContent });
                    }
                }
            }

            return loadedAnimations;
        }

        public static bool VerifySCA(string file)
        {
            return true;
        }
    }
}
