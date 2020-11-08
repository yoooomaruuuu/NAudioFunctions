using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using NAudio.Wave;
using System.Net.Sockets;
using System.Net;

namespace naudio_test
{
    class main
    {
        static int Main(string[] args)
        {
            // device.samplingRate device.bitRate channel udpPort deviceNumber
            int samplingRate = Int32.Parse(args[1]);
            int bitRate = Int32.Parse(args[2]);
            int channel = Int32.Parse(args[3]);
            int hostPort = Int32.Parse(args[4]);
            var deviceNumber = Int32.Parse(args[5]);

            if(channel != WaveIn.GetCapabilities(deviceNumber).Channels)
            {
                Console.WriteLine("チャネル数未対応です。");
                return -1;
            }

            var waveIn = new WaveInEvent();
            waveIn.DeviceNumber = deviceNumber;
            waveIn.WaveFormat = new WaveFormat(samplingRate, bitRate, channel);
            waveIn.BufferMilliseconds = 16;
            UdpClient udp = new UdpClient();
            string hostip = "127.0.0.1";
            waveIn.DataAvailable += (_, ee) =>
            {
                udp.Send(ee.Buffer, ee.BytesRecorded, hostip, hostPort);
            };
            waveIn.RecordingStopped += (_, __) =>
            {
            };

            waveIn.StartRecording();
            Console.Write("何らかの文字を入力したら終了");
            Console.ReadLine();
            waveIn?.StopRecording();
            waveIn?.Dispose();
            waveIn = null;
            return 0;
        }
    }
}
