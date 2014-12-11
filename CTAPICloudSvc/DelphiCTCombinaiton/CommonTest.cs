using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton
{
   internal  class CommonTest
    {
        public static void  InvokeAddressWithGet(string address)
        {
            if(address!=string.Empty)
            {
                var httpitem = new HttpItem() { RelatedAddress = address, RequestMethod = RequestMethod.Get, Authorization = TokenSeeker.CurrentToken.token_type + " " + TokenSeeker.CurrentToken.access_token, Content_Type = "application/hal+json" };
             new HTTPHelper(httpitem).HttpHelperMethod().GetAwaiter();
            }
        }
    }

}
