using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2PMessenger
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }


    private void btn_my_set_Click(object sender, EventArgs e)
    {
      string ip = tbox_my_ip.Text;
      int port = 0;
      if(!int.TryParse(tbox_my_port.Text, out port))
      {
        MessageBox.Show("포트 잘좀 입력하자 친구야");
        return;
      }
      SmsgServer sms = new SmsgServer(ip, port);
      sms.SmsgReceiveEventHandler += Sms_SmsgReceiveEventHandler;
      if (!sms.Start())
      {
        MessageBox.Show("서버 가동 실패");
      }
      else
      {
        tbox_my_ip.Enabled = tbox_my_port.Enabled = btn_my_set.Enabled = false;
      }
    }

    private void Sms_SmsgReceiveEventHandler(object sender, SmsgReceiveEventArgs e)
    {
      AddMessage(string.Format("{0}:{1}→{2}", e.IPStr, e.Port, e.Msg));
    }
    delegate void MyDele(String msg);
    private void AddMessage(string msg)
    {
      if (lbox_msg.InvokeRequired)
      {
        MyDele dele = AddMessage;
        object[] objs = new object[] { msg };
        lbox_msg.BeginInvoke(dele, objs);
      }
      else
      {
        lbox_msg.Items.Add(msg);
      }
    }

    string other_ip;
    int other_port = 0;

    private void btn_other_set_Click(object sender, EventArgs e)
    {
      other_ip = tbox_other_ip.Text;
      if(!int.TryParse(tbox_other_port.Text, out other_port))
      {
        MessageBox.Show("포트 번호를 정수로 변환할 수 없습니다.");
      }
    }

    private void btn_send_Click(object sender, EventArgs e)
    {
      SmsgClient.SendMsgAsync(other_ip, other_port, tbox_msg.Text);
      lbox_msg.Items.Add(string.Format("{0}:{1}→{2}", other_ip, other_port, tbox_msg.Text));
      tbox_msg.Text = "";
    }
  }
}
