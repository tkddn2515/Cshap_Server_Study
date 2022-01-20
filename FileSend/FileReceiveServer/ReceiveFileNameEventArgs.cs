using System;
using System.Net;

namespace FileReceiveServer
{
  public delegate void ReceiveFileNameEventHandler(object sender, ReceiveFileNameEventArgs e);

  public class ReceiveFileNameEventArgs:EventArgs
  {
    public IPEndPoint RemoteEndPoint
    {
      get;
      private set;
    }

    public string FileName
    {
      get;
      private set;
    }

    public ReceiveFileNameEventArgs(string fname, IPEndPoint rep)
    {
      FileName = fname;
      RemoteEndPoint = rep;
    }
  }
}
