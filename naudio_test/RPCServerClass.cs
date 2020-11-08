using MagicOnion.Server.Hubs;
using NAudio.Wave;
using System.Threading.Tasks;

namespace naudio_test
{
    class RPCServerClass
    {
    }
}

namespace naudio_udp_server
{
    public class RecordingOrderHubReciever : StreamingHubBase<IRecordingOrderHub, IRecordingOrderHubReciever>, IRecordingOrderHub
    {
        WaveInEvent audioWaveIn;
        public RecordingOrderHubReciever(WaveInEvent waveIn)
        {
            audioWaveIn = waveIn;
        }
        public async Task SettingInputDevice(int deviceNumber, int samplingRate, int bitRate, int channel, int bufferMilliSec = 16)
        {
            audioWaveIn.DeviceNumber = deviceNumber;
            audioWaveIn.WaveFormat = new WaveFormat(samplingRate, bitRate, channel);
            audioWaveIn.BufferMilliseconds = bufferMilliSec;

        }
        public async Task StartRecording()
        {
            audioWaveIn.StartRecording();
        }
        public async Task StopRecording()
        {
            audioWaveIn?.StopRecording();
            audioWaveIn?.Dispose();
            audioWaveIn = null;
        }
    }
}

