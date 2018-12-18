using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Utility
{
  public class LotteryMath
  {
    public string math()
    {
      string res = "";
      string[] array = { "a", "b", "c" };
      var a = 10;
      var b = 100;
      var c = 400;
      Random ran = new Random();
      int n = ran.Next(1,1001);
      if( n > 0 && n <= a)
      {
        res = array[0];
      }else if ( n >= a && n < a+b)
      {
        res = array[1];
      }else if(n >= a+b && n <= a+b+c)
      {
        res = array[2];
      }
      return res;
    }
  }
}
