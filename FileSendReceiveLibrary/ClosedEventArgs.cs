using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileReceiveServer
{
  public delegate void ClosedEventHandler(object sender, ClosedEventArgs e);
  public class ClosedEventArgs : EventArgs
  {
    public IPEndPoint RemoteEndPoint
    {
      get;
      private set;
    }

    public string IPStr
    {
      get
      {
        return RemoteEndPoint.Address.ToString();
      }
    }

    public int Port
    {
      get
      {
        return RemoteEndPoint.Port;
      }
    }

    public ClosedEventArgs(IPEndPoint rep)
    {
      RemoteEndPoint = rep;
    }
  }
}
