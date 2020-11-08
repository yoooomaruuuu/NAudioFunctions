using System.Collections.Generic;
using System.Threading.Tasks;
using MagicOnion;

namespace naudio_udp_server
{
    public interface IRecordingOrderHubReciever
    {
        void GetListInputDevices(List<string> devices);
    }

    public interface IRecordingOrderHub : IStreamingHub<IRecordingOrderHub, IRecordingOrderHubReciever>
    {
        Task SettingInputDevice(int deviceNumber, int samplingRate, int bitRate, int channel, int bufferMilliSec = 16);
        Task StartRecording();
        Task StopRecording();
    }
}
