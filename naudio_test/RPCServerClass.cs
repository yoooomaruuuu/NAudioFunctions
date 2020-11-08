using MagicOnion;
using MagicOnion.Server;
using NAudio.Wave;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Sockets;

namespace naudio_test
{
    class RPCServerClass
    {
    }
}

namespace naudio_udp_server
{
    public class RecordingOrderService : ServiceBase<IRecordingOrderService>, IRecordingOrderService
    {
        WaveInEvent audioWaveIn;
        string[] devices;
        public RecordingOrderService()//(int deviceNumber, int samplingRate, int bitRate, int channel, int bufferMilliSec = 16)
        {
            var devCnt = WaveInEvent.DeviceCount;
            devices = new string[devCnt];
            for(int i=0; i<devCnt; i++)
            {
                devices[i] = WaveInEvent.GetCapabilities(i).ProductName;
            }
            audioWaveIn = new WaveInEvent();
            audioWaveIn.DeviceNumber = 0;
            audioWaveIn.WaveFormat = new WaveFormat(48000, 16, 2);
            audioWaveIn.BufferMilliseconds = 16; //固定
            UdpClient udp = new UdpClient();
            string hostip = "127.0.0.1";
            audioWaveIn.DataAvailable += (_, ee) =>
            {
                udp.Send(ee.Buffer, ee.BytesRecorded, hostip, 2222);
            };
            audioWaveIn.RecordingStopped += (_, __) =>
            {
            };
        }
        public async UnaryResult<int> SettingInputDevice(int deviceNumber, int samplingRate, int bitRate, int channel, int bufferMilliSec = 16)
        {
            audioWaveIn.DeviceNumber = deviceNumber;
            audioWaveIn.WaveFormat = new WaveFormat(samplingRate, bitRate, channel);
            audioWaveIn.BufferMilliseconds = bufferMilliSec;
            return 0;

        }
        public async UnaryResult<int> StartRecording()
        {
            audioWaveIn.StartRecording();
            return 0;
        }
        public async UnaryResult<int> StopRecording()
        {
            audioWaveIn?.StopRecording();
            audioWaveIn?.Dispose();
            audioWaveIn = null;
            return 0;
        }

        public async UnaryResult<string[]> GetListInputDevices()
        {
            return devices; 
        }
    }
}

