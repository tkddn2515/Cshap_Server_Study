using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Threading.Tasks;
using GeneralLib;

namespace RemotingServer
{
  class Program
  {
    static void Main(string[] args)
    {
      HttpChannel hc = new HttpChannel(10400); // TCP채널 : BinaryFormatter, HTTP채널 : SoapFormatter
      ChannelServices.RegisterChannel(hc, false);
      RemotingConfiguration.RegisterWellKnownServiceType(
        typeof(General),
        "MyRemote",
        WellKnownObjectMode.Singleton);
      Console.ReadKey();
    }
  }
}
