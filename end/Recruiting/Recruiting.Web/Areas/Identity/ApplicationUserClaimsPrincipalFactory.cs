using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Recruiting.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recruiting.Web.Areas.Identity
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<RecruitingUser>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<RecruitingUser> userManager, IOptions<IdentityOptions> optionsAccessor) 
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(RecruitingUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("FullName",
                user.FullName));

            return identity;
        }
    }
}
