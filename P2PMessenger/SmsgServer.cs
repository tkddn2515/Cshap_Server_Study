using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace P2PMessenger
{
  public class SmsgServer
  {
    public event SmsgReceiveEventHandler SmsgReceiveEventHandler = null;
    public string IPStr
    {
      get;
      private set;
    }
    public int Port
    {
      get;
      private set;
    }

    public SmsgServer(string ipstr, int port)
    {
      IPStr = ipstr;
      Port = port;
    }
    Socket sock;
    public bool Start()
    {
      try
      {
        sock = new Socket(
        AddressFamily.InterNetwork,
        SocketType.Stream,
        ProtocolType.Tcp);

        IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IPStr), Port);
        sock.Bind(iep);
        sock.Listen(5);
        AcceptLoopAsync();
        return true;
      }
      catch
      {
        return false;
      }
    }

    delegate void AcceptDele();

    private void AcceptLoopAsync()
    {
      // AcceptLoop를 비동기적으로 실행
      AcceptDele dele = AcceptLoop;
      dele.BeginInvoke(null, null);
    }

    private void AcceptLoop()
    {
      Socket dosock = null;
      while (true)
      {
        dosock = sock.Accept();
        DoIt(dosock);
      }
    }

    private void DoIt(Socket dosock)
    {
      IPEndPoint remote = dosock.RemoteEndPoint as IPEndPoint;
      byte[] packet = new byte[1024];
      dosock.Receive(packet);
      dosock.Close();
      MemoryStream ms = new MemoryStream(packet);
      BinaryReader br = new BinaryReader(ms);
      string msg = br.ReadString();
      ms.Close();
      br.Close();
      if(SmsgReceiveEventHandler != null)
      {
        SmsgReceiveEventHandler(this, new SmsgReceiveEventArgs(remote, msg));
      }
    }
  }
}