﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Security;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShapeClipController
{
    class Serial
    {
        private SerialPort _serialPort;

        public string Port { get; }
        public bool Reading { get; private set; }

        public Serial(string port)
        {
            Port = port;
            Reading = false;
        }

        public bool Open()
        {
            var success = true;

            // This might need handshaking in the future
            try
            {
                _serialPort = new SerialPort()
                {
                    BaudRate = 9600,
                    PortName = Port,
                    Parity   = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Encoding = Encoding.UTF8,

                    ReadTimeout = 500,
                    WriteTimeout = 500
                };

                _serialPort.Open();
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public bool Close()
        {
            _serialPort.Close();
            return true;
        }

        public bool SendAndRead(string message)
        {
            var readThread = new Thread(Read);
            readThread.Start(); // Shuts down automatically as reads only until we don't get anything

            return Send(message);
        }

        public bool Send(string message)
        {
            var success = true;

            _serialPort.ErrorReceived += (sender, args) => success = false;
            _serialPort.WriteLine(message);

            return success;
        }

        public void Read()
        {
            Reading = true;

            while (true)
            {
                try
                {
                    string line = _serialPort.ReadLine();
                    Console.WriteLine(line);
                }
                catch (TimeoutException)
                {
                    Close();
                    break;
                }
            }

            Reading = false;
        }

        public static string[] GetSerialPorts()
        {
            // code to remove unusable serial ports...
            var ports = SerialPort.GetPortNames();
            return ports;
        }
    }
}
