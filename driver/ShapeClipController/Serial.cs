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

        public bool ExplicitClose { get; set; }

        public string Port { get; }
        public bool Reading { get; private set; }
        public bool TimedOut { get; private set; }
        public string LastRead { get; private set; }
        public bool LastReadEmpty => LastRead.Equals("");

        public Serial(string port)
        {
            Port = port;
            Reading = false;
            TimedOut = false;
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

                    ReadTimeout = 1000,
                    WriteTimeout = 1000
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

        public void SendAndRead(string message)
        {
            /*var readThread = new Thread(Read);
            readThread.Start(); // Shuts down automatically as reads only until we don't get anything*/

            Send(message);
            Read();
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
            LastRead = "";

            while (true)
            {
                try
                {
                    string line = _serialPort.ReadLine();
                    Console.WriteLine(line);
                    LastRead += line;
                }
                catch (TimeoutException)
                {
                    TimedOut = true;
                    if(!ExplicitClose) Close();
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
