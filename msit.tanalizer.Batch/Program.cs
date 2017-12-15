using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using msit.twitterScore.Logic;


namespace msit.tanalizer.Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IntegrationLogic logic = new IntegrationLogic();
                logic.GetAllTwitterElements("\"MashiRafael\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
      
    }
}
