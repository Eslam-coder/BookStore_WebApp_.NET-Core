using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Security
{
    public class CanEditOtherAdminRolesAndClaimsHandler : AuthorizationHandler<ManageAdminRulesAndClaims>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAdminRulesAndClaims requirement)
        {
            throw new NotImplementedException();
        }
    }
}
