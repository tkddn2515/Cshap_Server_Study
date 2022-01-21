using System;
using System.Net;

namespace FileReceiveServer
{
  public delegate void FileLengthReceiveEventHandler(object sender, FileLengthReceiveEventArgs e);

  public class FileLengthReceiveEventArgs : EventArgs
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

    public long Length
    {
      get;
      private set;
    }

    public FileLengthReceiveEventArgs(string fname, IPEndPoint rep, long length)
    {
      FileName = fname;
      RemoteEndPoint = rep;
      Length = length;
    }
  }
}