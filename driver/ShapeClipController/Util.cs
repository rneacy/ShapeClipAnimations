using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

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
    }
}
