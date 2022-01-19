using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
  class Program
  {
    static void Main(string[] args)
    {
      EchoServer es = new EchoServer("192.168.1.21", 10100);
      es.ReceivedMsgEventHandler += Es_ReceivedMsgEventHandler;
      es.AcceptedEventHandler += Es_AcceptedEventHandler;
      es.ClosedEventHandler += Es_ClosedEventHandler;
      if (es.Start() == false)
      {
        Console.WriteLine("서버 가동 실패");
        return;
      }
      Console.ReadKey();
    }

    private static void Es_AcceptedEventHandler(object sender, AcceptedEventArgs e)
    {
      Console.WriteLine("{0}:{1}에서 연결", e.IPStr, e.Port);
    }

    private static void Es_ClosedEventHandler(object sender, ClosedEventArgs e)
    {
      Console.WriteLine("{0}:{1}에서 끊김", e.IPStr, e.Port);
    }

    private static void Es_ReceivedMsgEventHandler(object sender, ReceivedMsgEventArgs e)
    {
      Console.WriteLine("{0}:{1}->{2}", e.IPStr, e.Port, e.Msg);
    }
  }
}
