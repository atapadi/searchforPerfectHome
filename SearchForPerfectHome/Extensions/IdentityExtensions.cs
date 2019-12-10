using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace SearchForPerfectHome.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetSecurityUserId(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            IEnumerable<Claim> claims = claimsIdentity.Claims;

            string userId = "";
            try
            {
                if (claims.Any(x => x.Type == ClaimTypes.NameIdentifier))
                {
                    userId = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                }
                //else
                //    LogWriter.TraceMessage("No Claims found for the user", "GetSecurityUserId", "IdentityExtension");
            }
            catch (Exception ex)
            {
                //LogWriter.LogException("GetSecurityUserId", ex);
            }
            return userId;
        }


        public static string GetSecurityEmail(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            IEnumerable<Claim> claims = claimsIdentity.Claims;

            string email = "";
            try
            {
                if (claims.Any(x => x.Type == ClaimTypes.Name))
                {
                    email = claims.First(x => x.Type == ClaimTypes.Name).Value;
                }
                //else
                //    LogWriter.TraceMessage("No Claims found for the user", "GetSecurityUserId", "IdentityExtension");
            }
            catch (Exception ex)
            {
                //LogWriter.LogException("GetSecurityUserId", ex);
            }
            return email;
        }
    }
}