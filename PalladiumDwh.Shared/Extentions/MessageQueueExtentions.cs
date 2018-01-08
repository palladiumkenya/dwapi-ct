using System;
using System.Collections.Generic;
using System.Messaging;

namespace PalladiumDwh.Shared.Extentions
{
  public  static class MessageQueueExtentions
    {
        public static int Count(this MessageQueue queue)
        {
            int count = 0;
            var enumerator = queue.GetMessageEnumerator2();
            while (enumerator.MoveNext())
                count++;

            return count;
        }

    public static List<string> GetIds(this MessageQueue queue)
    {
      List<string> ids = new List<string>();
      var enumerator = queue.GetMessageEnumerator2();
      while (enumerator.MoveNext())
      {
        if (enumerator.Current != null) ids.Add(enumerator.Current.Id);
      }
        
        

      return ids;
    }
      public static List<string> GetIds(this MessageQueue queue,int limit)
      {
        List<string> ids = new List<string>();
        var enumerator = queue.GetMessageEnumerator2();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current != null) ids.Add(enumerator.Current.Id);

          
        }



        return ids;
      }
  }
}
