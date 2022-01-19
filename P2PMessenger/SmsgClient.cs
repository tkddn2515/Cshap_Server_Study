using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace P2PMessenger
{
  public static class SmsgClient
  {
    public static void SendMsgAsync(string other_ip, int other_port, string text)
    {
      SendDele dele = SendMsg;
      dele.BeginInvoke(other_ip, other_port, text, null, null);
    }

    delegate void SendDele(string other_ip, int other_port, string text);

    public static void SendMsg(string other_ip, int other_port, string text)
    {
      try
      {
        byte[] packet = new byte[1024];
        MemoryStream ms = new MemoryStream(packet);
        BinaryWriter bw = new BinaryWriter(ms);
        bw.Write(text);
        ms.Close();
        bw.Close();

        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse(other_ip), other_port);
        sock.Connect(iep);
        sock.Send(packet);
        sock.Close();
      }
      catch
      {

      }
    }
  }
}