using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
  public class EchoServer
  {
    public event AcceptedEventHandler AcceptedEventHandler = null;
    public event ClosedEventHandler ClosedEventHandler = null;
    public event ReceivedMsgEventHandler ReceivedMsgEventHandler = null;
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

    public EchoServer(string ipstr, int port)
    {
      this.IPStr = ipstr;
      this.Port = port;
    }
    Socket sock = null;
    public bool Start()
    {
      try
      {
        // 소켓 생성
        sock = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream, // TCP
            ProtocolType.Tcp);

        // 인터페이스와 결합
        IPAddress addr = IPAddress.Parse(IPStr);
        IPEndPoint iep = new IPEndPoint(addr, Port);
        sock.Bind(iep);

        // 백로고 큐 크기 설정
        sock.Listen(5);

        AcceptLoopAsync();

        return true;
      }
      catch
      {
        return false;
      }
    }
    public void Close()
    {
      if (sock != null)
      {
        try
        {
          sock.Close();
        }
        catch (Exception e)
        {
          
        }
      }
    }

    delegate void AcceptDele();

    private void AcceptLoopAsync()
    {
      AcceptDele dele = AcceptLoop;
      dele.BeginInvoke(null, null);
    }

    private void AcceptLoop()
    {
      Socket dosock = null;
      while (true)
      {
        Console.WriteLine("New While");
        dosock = sock.Accept();
        Console.WriteLine("New dosock");
        Console.WriteLine(dosock);
        DoItAsync(dosock);
      }
    }

    delegate void DoItDele(Socket dosock);

    private void DoItAsync(Socket dosock)
    {
      DoItDele dele = DoIt;
      dele.BeginInvoke(dosock, null, null);
    }

    private void DoIt(Socket dosock)
    {
      Console.WriteLine("New Dolt");
      IPEndPoint remote_ep = dosock.RemoteEndPoint as IPEndPoint;
      if(AcceptedEventHandler != null)
      {
        AcceptedEventHandler(this, new AcceptedEventArgs(remote_ep));
      }

      try
      {
        byte[] packet = new byte[1024];
        while (true)
        {
          dosock.Receive(packet);
          MemoryStream ms = new MemoryStream(packet);
          BinaryReader br = new BinaryReader(ms);
          string msg = br.ReadString();
          br.Close();
          ms.Close();
          if(ReceivedMsgEventHandler != null)
          {
            ReceivedMsgEventHandler(this, new ReceivedMsgEventArgs(remote_ep, msg));
          }

          dosock.Send(packet);
        }
      }
      catch
      {

      }
      finally
      {
        Console.WriteLine("끊겼짜너");
        dosock.Close();
        if (ClosedEventHandler != null)
        {
          ClosedEventHandler(this, new ClosedEventArgs(remote_ep));
        }
      }
    }
  }
}