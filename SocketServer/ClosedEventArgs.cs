using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
  public delegate void ClosedEventHandler(object sender, ClosedEventArgs e);
  public class ClosedEventArgs : EventArgs
  {
    public IPEndPoint RemoteEP
    {
      get;
      private set;
    }
    public string IPStr
    {
      get
      {
        return RemoteEP.Address.ToString();
      }
    }
    public int Port
    {
      get
      {
        return RemoteEP.Port;
      }
    }

    public ClosedEventArgs(IPEndPoint remote_EP)
    {
      RemoteEP = remote_EP;
    }
  }
}
