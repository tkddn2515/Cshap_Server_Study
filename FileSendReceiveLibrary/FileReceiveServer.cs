using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FileReceiveServer
{
  public class FileReceiveServer
  {
    const int MAX_PACK_SIZE = 1024;
    public event AcceptedEventHandler AcceptedEventHandler = null;
    public event ClosedEventHandler ClosedEventHandler = null;
    public event ReceiveFileNameEventHandler ReceiveFileNameEventHandler = null;
    public event FileLengthReceiveEventHandler FileLengthReceiveEventHandler = null;
    public event FileDataReceiveEventHandler FileDataReceiveEventHandler = null;

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

    public FileReceiveServer(string ip, int port)
    {
      IPStr = ip;
      Port = port;
    }

    Socket sock;
    public bool Start()
    {
      try
      {
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IPStr), Port);
        sock.Bind(iep);

        sock.Listen(5);

        AcceptLoopAsync();
      }
      catch
      {
        return false;
      }
      return true;
    }

    delegate void AcceptDele();

    private void AcceptLoopAsync()
    {
      AcceptDele dele = AcceptLoop;
      dele.BeginInvoke(null, null);
    }

    private void AcceptLoop()
    {
      while (true)
      {
        Socket dosock = sock.Accept();
        DoItAsync(dosock);
      }
    }

    Thread thread;

    private void DoItAsync(Socket dosock)
    {
      ParameterizedThreadStart pts = DoIt;
      thread = new Thread(pts);
      thread.Start(dosock);

    }

    void DoIt(object osock)
    {
      Socket dosock = osock as Socket;
      IPEndPoint rep = dosock.RemoteEndPoint as IPEndPoint;
      if(AcceptedEventHandler != null)
      {
        AcceptedEventHandler(this, new AcceptedEventArgs(rep));
      }
      string fname = ReceiveFileName(dosock);
      if(ReceiveFileNameEventHandler != null)
      {
        ReceiveFileNameEventHandler(this, new ReceiveFileNameEventArgs(fname, rep));
      }
      long length = ReceiveFileLenght(dosock);
      if(FileLengthReceiveEventHandler != null)
      {
        FileLengthReceiveEventHandler(this, new FileLengthReceiveEventArgs(fname, rep, length));
      }
      ReceiveFile(dosock, fname, length);
      dosock.Close();
      if(ClosedEventHandler != null)
      {
        ClosedEventHandler(this, new ClosedEventArgs(rep));
      }
    }

    private void ReceiveFile(Socket dosock, string fname, long length)
    {
      IPEndPoint rep = dosock.RemoteEndPoint as IPEndPoint;
      byte[] packet = new byte[MAX_PACK_SIZE];
      while(length>= MAX_PACK_SIZE)
      {
        int rlen = dosock.Receive(packet);
        if(FileDataReceiveEventHandler != null)
        {
          byte[] pd2 = new byte[rlen];
          MemoryStream ms = new MemoryStream(pd2);
          ms.Write(packet, 0, rlen);
          FileDataReceiveEventHandler(this, new FileDataReceiveEventArgs(fname, rep, length, pd2));
        }
        length -= rlen;
      }
      dosock.Receive(packet, (int)length, SocketFlags.None);
      if (FileDataReceiveEventHandler != null)
      {
        byte[] pd2 = new byte[length];
        MemoryStream ms = new MemoryStream(pd2);
        ms.Write(packet, 0, (int)length);
        FileDataReceiveEventHandler(this, new FileDataReceiveEventArgs(fname, rep, 0, pd2));
      }
    }

    private long ReceiveFileLenght(Socket dosock)
    {
      byte[] packet = new byte[8];
      dosock.Receive(packet);
      MemoryStream ms = new MemoryStream(packet);
      BinaryReader br = new BinaryReader(ms);
      long length = br.ReadInt64();
      br.Close();
      ms.Close();
      return length;
    }

    private string ReceiveFileName(Socket dosock)
    {
      byte[] packet = new byte[MAX_PACK_SIZE];
      dosock.Receive(packet);
      MemoryStream ms = new MemoryStream(packet);
      BinaryReader br = new BinaryReader(ms);
      string fname = br.ReadString();
      br.Close();
      ms.Close();
      return fname;
    }
  }
}
