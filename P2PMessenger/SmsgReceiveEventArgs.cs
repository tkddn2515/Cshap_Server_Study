using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P2PMessenger
{
  public delegate void SmsgReceiveEventHandler(object sender, SmsgReceiveEventArgs e);
  public class SmsgReceiveEventArgs : EventArgs
  {
    public IPEndPoint RemoteEndPoint
    {
      get;
      private set;
    }

    public string Msg
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

    public SmsgReceiveEventArgs(IPEndPoint remote, string msg)
    {
      RemoteEndPoint = remote;
      Msg = msg;
    }
  }
}
