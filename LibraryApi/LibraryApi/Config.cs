using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace LibraryApi
{
  public class Config
  {
    public static IEnumerable<ApiResource> GetApiResources()
    {
      return new List<ApiResource>
      {
        new ApiResource("resourceApi", "API Application")
      };
    }

    private static string spaClientUrl = "https://localhost:4200";

    public static IEnumerable<Client> GetClients()
    {
      return new List<Client>
      {
        new Client
        {
          ClientId = "spaCodeClient",
          ClientName = "SPA Code Client",
          AccessTokenType = AccessTokenType.Jwt,
          AccessTokenLifetime = 330, // 330 seconds, default 60 minutes
          IdentityTokenLifetime = 30,

          RequireClientSecret = false,
          AllowedGrantTypes = GrantTypes.Code,
          RequirePkce = true,

          AllowAccessTokensViaBrowser = true,
          RedirectUris = new List<string>
          {
            $"{spaClientUrl}/callback",
            $"{spaClientUrl}/silent-renew.html",
            "https://localhost:4200",
            "https://localhost:4200/silent-renew.html"
          },
          PostLogoutRedirectUris = new List<string>
          {
              $"{spaClientUrl}/unauthorized",
              $"{spaClientUrl}",
              "https://localhost:4200/unauthorized",
              "https://localhost:4200"
          },
          AllowedCorsOrigins = new List<string>
          {
              $"{spaClientUrl}",
              "https://localhost:4200"
          },
          AllowedScopes = new List<string>
          {
              IdentityServerConstants.StandardScopes.OpenId,
              IdentityServerConstants.StandardScopes.Profile,
              "resourceApi"
          }
        },
      };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
      return new List<IdentityResource>
      {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
      };
    }

    public static List<TestUser> GetTestUsers()
    {
      return new List<TestUser>
      {
        new TestUser{SubjectId = "818727", Username = "alice", Password = "alice",
          Claims =
          {
            new Claim(JwtClaimTypes.Name, "Alice Smith"),
            new Claim(JwtClaimTypes.GivenName, "Alice"),
            new Claim(JwtClaimTypes.FamilyName, "Smith"),
            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
            new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
          }
        },
        new TestUser{SubjectId = "88421113", Username = "bob", Password = "bob",
          Claims =
          {
            new Claim(JwtClaimTypes.Name, "Bob Smith"),
            new Claim(JwtClaimTypes.GivenName, "Bob"),
            new Claim(JwtClaimTypes.FamilyName, "Smith"),
            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
            new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
            new Claim("location", "somewhere")
          }
        }
      };
    }
  }
}