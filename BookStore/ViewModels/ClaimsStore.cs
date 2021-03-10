using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public static  class ClaimsStore
    {
        public static List<Claim> allClaims = new List<Claim>()
        {
            new Claim("CreateRole","CreateRole"),
            new Claim("EditeRole","EditeRole"),
            new Claim("DeleteRole","DeleteRole")

        };
    }
}
