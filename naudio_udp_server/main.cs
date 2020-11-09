using System;
using NAudio.Wave;
using Grpc.Core;
using MagicOnion.Server;
using MagicOnion.Hosting;

namespace naudio_udp_server
{
    class main
    {
        static int Main(string[] args)
        {
            // device.samplingRate device.bitRate channel udpPort deviceNumber bufferMilliSec

            GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());
            var service = MagicOnionEngine.BuildServerServiceDefinition(isReturnExceptionStackTraceInErrorDetail: true);

            var server = new global::Grpc.Core.Server
            {
                Services = { service },
                Ports = { new ServerPort("localhost", 12345, ServerCredentials.Insecure) }
            };
            server.Start();
            return 0;
        }
    }
}
