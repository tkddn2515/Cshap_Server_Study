using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReceiveServer
{
  class Program
  {
    static void Main(string[] args)
    {
      FileReceiveServer fs = new FileReceiveServer("192.168.1.21", 10340);
      fs.AcceptedEventHandler += Fs_AcceptedEventHandler;
      fs.ClosedEventHandler += Fs_ClosedEventHandler;
      fs.ReceiveFileNameEventHandler += Fs_ReceiveFileNameEventHandler;
      fs.FileDataReceiveEventHandler += Fs_FileDataReceiveEventHandler;
      fs.FileLengthReceiveEventHandler += Fs_FileLengthReceiveEventHandler;
      fs.Start();
      Console.ReadKey();
    }

    static long length;
    static FileStream fs;

    private static void Fs_ReceiveFileNameEventHandler(object sender, ReceiveFileNameEventArgs e)
    {
      Console.WriteLine("{0}:{1}에서 {2}전송 시작", e.RemoteEndPoint.Address, e.RemoteEndPoint.Port, e.FileName);
      fs = File.Create(e.FileName);
    }

    private static void Fs_FileLengthReceiveEventHandler(object sender, FileLengthReceiveEventArgs e)
    {
      Console.WriteLine("{0}:{1}에서 {2}길이: {3}", e.RemoteEndPoint.Address, e.RemoteEndPoint.Port, e.FileName, e.Length);
      length = e.Length;
    }

    private static void Fs_FileDataReceiveEventHandler(object sender, FileDataReceiveEventArgs e)
    {
      Console.WriteLine("{0}:{1}에서 {2}남은 길이: {3}", e.RemoteEndPoint.Address, e.RemoteEndPoint.Port, e.FileName, e.RemainLength);
      fs.Write(e.Data, 0, e.Data.Length);
      if(e.RemainLength == 0)
      {
        fs.Close();
      }
    }

    private static void Fs_ClosedEventHandler(object sender, ClosedEventArgs e)
    {
      Console.WriteLine("{0}:{1}와 연결 해제", e.IPStr, e.Port);
    }

    private static void Fs_AcceptedEventHandler(object sender, AcceptedEventArgs e)
    {
      Console.WriteLine("{0}:{1}와 연결", e.IPStr, e.Port);
    }
  }
}
