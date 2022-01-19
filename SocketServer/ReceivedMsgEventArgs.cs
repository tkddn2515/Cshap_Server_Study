﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
  public delegate void ReceivedMsgEventHandler(object sender, ReceivedMsgEventArgs e);

  public class ReceivedMsgEventArgs : EventArgs
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

    public string Msg
    {
      get;
      private set;
    }


    public ReceivedMsgEventArgs(IPEndPoint remote_EP, string msg)
    {
      RemoteEP = remote_EP;
      Msg = msg;
    }
  }
}
