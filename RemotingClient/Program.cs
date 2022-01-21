using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Text;
using System.Threading.Tasks;
using GeneralLib;

namespace RemotingClient
{
  class Program
  {
    static void Main(string[] args)
    {
      HttpChannel hc = new HttpChannel();
      ChannelServices.RegisterChannel(hc, false);
      General gen = Activator.GetObject(typeof(General), "http://192.168.1.21:10400/MyRemote") as General;
      string str = gen.ConvertIntToString(0);
      Console.WriteLine("호출 결과: {0}", str);
      Console.ReadLine();
      str = gen.ConvertIntToString(1);
      Console.WriteLine("호출 결과: {0}", str);
      Console.ReadLine();

      str = gen.ConvertIntToString(2);
      Console.WriteLine("호출 결과: {0}", str);
      Console.ReadLine();

      str = gen.ConvertIntToString(3);
      Console.WriteLine("호출 결과: {0}", str);
      Console.ReadLine();
    }
  }
}
