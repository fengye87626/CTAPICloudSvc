using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton.Models
{
    public class Claim
    {
        public Guid CustomerId{get;set;}
        public int ClaimId{get;set;}
        public string ClaimType{get;set;}
        public int ClaimSetVersion{get;set;}
        public string ClaimValue{get;set;}
    }
}
