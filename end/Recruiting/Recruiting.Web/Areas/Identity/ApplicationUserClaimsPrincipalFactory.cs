using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Recruiting.Data.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recruiting.Web.Areas.Identity
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<RecruitingUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<RecruitingUser> userManager, 
                                                        RoleManager<IdentityRole> roleManager, 
                                                        IOptions<IdentityOptions> options) 
            : base(userManager, roleManager, options)
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
