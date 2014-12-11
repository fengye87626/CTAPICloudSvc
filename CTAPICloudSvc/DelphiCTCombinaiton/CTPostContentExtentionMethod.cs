using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Runtime;
using DelphiCTCombinaiton.Models;
using System.Diagnostics;

namespace DelphiCTCombinaiton
{
   public static  class CTPostContentExtentionMethod
    {
       public static void PostContent2CT<T>(this List<T> lst, Action<T> post)
       {
           Parallel.ForEach<T>(lst, post);
       }
    }
}

