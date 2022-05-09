﻿using System.Security.Claims;

namespace WebSite.EndPoint.Utilities
{
    public static class ClaimUtility
    {
        public static string GetUserId(ClaimsPrincipal User)
        {
            var claimsIdentity = User.Identities as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
