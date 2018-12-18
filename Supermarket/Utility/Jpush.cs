using Jiguang.JPush;
using Jiguang.JPush.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Utility
{
  public class Jpush
  {
    public void SendOrder(long SysUserId,string Name)
    {
      JPushClient client = new JPushClient("e84ca9e8b79c62c846480af2", "b18e6f9fff468584367401bb");
      PushPayload pushPayload = new PushPayload()
      {
        Platform = "android",
        Audience = new Audience()
        {
          Alias = new List<string>
          {
           SysUserId.ToString()
          }
        },
        Notification = new Notification()
        {
          Alert = Name,
          Android = new Android()
          {
            Alert = "您有新订单啦！请注意查收",
            Title = Name
          },
          //IOS = new IOS()
          //{
          //  Alert = "ios alert",
          //  Badge = "+1"
          //}
        },

        //Message = new Message()
        //{
        //  Title = "佰盛达超市",
        //  Content = "您有新订单啦！请注意查收",
        //  Extras = new Dictionary<string, string>()
        //  {
        //    ["Sound"] = "https://ia600406.us.archive.org/16/items/JM2013-10-05.flac16/V0/jm2013-10-05-t12-MP3-V0.mp3"
        //  }
        //}
      };
      var response = client.SendPush(pushPayload);


    }
  }
}
