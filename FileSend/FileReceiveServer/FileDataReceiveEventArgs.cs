using System;
using System.Net;

namespace FileReceiveServer
{
  public delegate void FileDataReceiveEventHandler(object sender, FileDataReceiveEventArgs e);
  public class FileDataReceiveEventArgs : EventArgs
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

    public long RemainLength
    {
      get;
      private set;
    }

    public byte[] Data
    {
      get;
      private set;
    }

    public FileDataReceiveEventArgs(string fname, IPEndPoint rep, long rlen, byte[] data)
    {
      FileName = fname;
      RemoteEndPoint = rep;
      RemainLength = rlen;
      Data = data;
    }
  }
}
