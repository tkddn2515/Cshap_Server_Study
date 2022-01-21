using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLib
{
  public class General : MarshalByRefObject
  {
    public string ConvertIntToString(int num)
    {
      Console.WriteLine("ConvertIntToString 메소드 수행(전달 받은 인자:{0})", num);
      switch (num) {
        case 0: return "영";
        case 1: return "일";
        case 2: return "이";
        default: return "아모른직다";
      }
    }
  }
}