using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Pine.Saas.Portal.Infastructure.IdentityServer4.Models
{
    public static class Config
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("JsPortalResource", "Portal Resource"),
                new ApiResource("api1","My API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                 new Client{
                    ClientId = "ngAdmin",
                    ClientName = "JavaScript Client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.Code,

                    RedirectUris =           { "http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = { "http://localhost:4200/" },
                    AllowedCorsOrigins =     { "http://localhost:4200" },
                    RequirePkce = true,
                    RequireClientSecret = false,

                    AllowedScopes = new List<string >{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "JsPortalResource"
                    }
                },
                new Client{
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.Code,

                    RedirectUris =           { "http://localhost:5000/DemoApps/Authorize/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5000/DemoApps/Authorize/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5000" },
                    RequirePkce = true,
                    RequireClientSecret = false,

                    AllowedScopes = new List<string >{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "JsPortalResource"
                    }
                },
                new Client{
                    ClientId = "websocket",
                    ClientName = "JavaScript Client",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.Code,

                    RedirectUris =           { "http://localhost:5000/DemoApps/IM/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5000/DemoApps/IM/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5000" },
                    RequirePkce = true,
                    RequireClientSecret = false,

                    AllowedScopes = new List<string >{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "JsPortalResource"
                    }
                },
                new Client{
                    ClientId = "mvc",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent=false,
                    RequirePkce=true,
                    RedirectUris = { "http://localhost:5000/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5000/signout-callback-oidc" },
                    //client code challenge
                    AllowPlainTextPkce=false,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "JsPortalResource"
                    },
                    AllowOfflineAccess = true
                }
            };

        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Address()
            };
    }
}
