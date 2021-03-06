using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSneakersMob.Extensions.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<Client> GetClients(string apiRedirectUrl)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "the_sneakers_mob_api",
                    RequireConsent = false,

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { apiRedirectUrl },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "TheSneakersMobAPI"
                    }
                }
            };
        }
    }
}
