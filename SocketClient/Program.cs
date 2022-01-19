using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
  class Program
  {
    static void Main(string[] args)
    {
      Socket sock = new Socket(
              AddressFamily.InterNetwork,
              SocketType.Stream, // TCP
              ProtocolType.Tcp);

      IPAddress addr = IPAddress.Parse("192.168.1.21");
      IPEndPoint iep = new IPEndPoint(addr, 10100);

      sock.Connect(iep);
      string str;
      string str2;
      byte[] packet = new byte[1024]; // 보내기
      byte[] packet2 = new byte[1024]; // 받기
      while (true)
      {
        Console.WriteLine("전송할 메시지:");
        str = Console.ReadLine();
        MemoryStream ms = new MemoryStream(packet);
        BinaryWriter bw = new BinaryWriter(ms);
        bw.Write(str);
        bw.Close();
        ms.Close();
        sock.Send(packet);
        if (str == "exit")
        {
          break;
        }
        sock.Receive(packet2);
        MemoryStream ms2 = new MemoryStream(packet2);
        BinaryReader br2 = new BinaryReader(ms2);
        str2 = br2.ReadString();
        Console.WriteLine("수신한 메시지:{0}", str2);
        br2.Close();
        ms2.Close();
      }
      sock.Close();
    }
  }
}
