using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
//using AspNet.Security.OpenIdConnect.Server;
using OpenIddict.Core;
using DAL.Core;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using A4.DAL.Entites;
using DAL;
using Microsoft.AspNetCore;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace A4.Empower.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private IUnitOfWork _unitOfWork;

        public AuthorizationController(
            IUnitOfWork unitOfWork,
            IOptions<IdentityOptions> identityOptions,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _identityOptions = identityOptions;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpPost("~/connect/token")]
        [Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest();
            try
            {
                //var 
                if (request.IsPasswordGrantType())
                {
                    var user = await _userManager.FindByEmailAsync(request.Username) ?? await _userManager.FindByNameAsync(request.Username);
                    if (user == null)
                    {
                        return BadRequest(new OpenIddictResponse
                        {
                            Error = Errors.InvalidGrant,
                            ErrorDescription = "Please check that your email and password is correct"
                        });
                    }

                    if (!user.IsEnabled)
                    {
                        return BadRequest(new OpenIddictResponse
                        {
                            Error = Errors.InvalidGrant,
                            ErrorDescription = "The specified user account is disabled"
                        });
                    }
                    var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

                    if (result.IsLockedOut)
                    {
                        return BadRequest(new OpenIddictResponse
                        {
                            Error = Errors.InvalidGrant,
                            ErrorDescription = "The specified user account has been suspended"
                        });
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return BadRequest(new OpenIddictResponse
                        {
                            Error = Errors.InvalidGrant,
                            ErrorDescription = "Invalid login procedure"
                        });
                    }
                    if (result.IsNotAllowed)
                    {
                        return BadRequest(new OpenIddictResponse
                        {
                            Error = Errors.InvalidGrant,
                            ErrorDescription = "The specified user is not allowed to sign in"
                        });
                    }

                    if (!result.Succeeded)
                    {
                        return BadRequest(new OpenIddictResponse
                        {
                            Error = Errors.InvalidGrant,
                            ErrorDescription = "Please check that your email and password is correct"
                        });
                    }
                    var ticket = await CreateTicketAsync(request, user);

                    return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
                }
                else if (request.IsRefreshTokenGrantType())
                {

                    var info = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                    var user = await _userManager.GetUserAsync(info.Principal);
                    if (user == null)
                    {
                        return BadRequest(new OpenIddictResponse
                        {
                            Error = Errors.InvalidGrant,
                            ErrorDescription = "The refresh token is no longer valid"
                        });
                    }
                    if (!await _signInManager.CanSignInAsync(user))
                    {
                        return BadRequest(new OpenIddictResponse
                        {
                            Error = Errors.InvalidGrant,
                            ErrorDescription = "The user is no longer allowed to sign in"
                        });
                    }

                    var ticket = await CreateTicketAsync(request, user);
                    return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
                }
                return BadRequest(new OpenIddictResponse
                {
                    Error = Errors.UnsupportedGrantType,
                    ErrorDescription = "The specified grant type is not supported"
                });
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private async Task<AuthenticationTicket> CreateTicketAsync(OpenIddictRequest request, ApplicationUser user)
        {
            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            principal.SetScopes(new[]
             {
                    Scopes.OpenId,
                     Scopes.Email,
                     Scopes.Phone,
                     Scopes.Profile,
                     Scopes.OfflineAccess,
                    Scopes.Roles,
            }.Intersect(request.GetScopes()));

            principal.SetResources(request.Resources);

            var propertiesDictionary = new Dictionary<string, string>();

            if ((principal.GetScopes() != null || principal.GetScopes().Length != 0) && (principal.GetResources() != null || principal.GetResources().Length != 0))
            {
                propertiesDictionary.Add(".scopes", string.Join(",", principal.GetScopes()));
                propertiesDictionary.Add(".resources", string.Join(",", principal.GetResources()));

            }

            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(propertiesDictionary), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            
            foreach (var claim in principal.Claims)
            {
                if (claim.Type == _identityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
                    continue;


                var destinations = new List<string> { Destinations.AccessToken };
                if ((claim.Type == Claims.Subject && ticket.Principal.HasScope(Scopes.OpenId)) ||
                    (claim.Type == Claims.Name && ticket.Principal.HasScope(Scopes.Profile)) ||
                    (claim.Type == Claims.Role && ticket.Principal.HasScope(Scopes.Roles)) ||
                    (claim.Type == CustomClaimTypes.Permission && ticket.Principal.HasScope(Scopes.Roles)))
                {
                    destinations.Add(Destinations.IdentityToken);
                }
                claim.SetDestinations(destinations);
            }


            var identity = ticket.Principal.Identity as ClaimsIdentity;


            if (ticket.Principal.HasScope(Scopes.Profile))
            {

                if (!string.IsNullOrWhiteSpace(user.FullName))
                    identity.AddClaim(CustomClaimTypes.FullName, user.FullName, Destinations.IdentityToken);

            }

            if (ticket.Principal.HasScope(Scopes.Email))
            {
                if (!string.IsNullOrWhiteSpace(user.Email))
                    identity.AddClaim(CustomClaimTypes.Email, user.Email, Destinations.IdentityToken);


                var module = _unitOfWork.ApplicationModule.GetAll();
                foreach (var item in module)
                {
                    switch (item.Type)
                    {
                        case "leave":
                            if (item.IsActive)
                            {
                                identity.AddClaim(CustomClaimTypes.Leave, "1", Destinations.IdentityToken);
                            }
                            else
                            {
                                identity.AddClaim(CustomClaimTypes.Leave, "0", Destinations.IdentityToken);
                            }
                            break;

                        case "recruitment":
                            if (item.IsActive)
                            {
                                identity.AddClaim(CustomClaimTypes.Recruitment, "1", Destinations.IdentityToken);
                            }
                            else
                            {
                                identity.AddClaim(CustomClaimTypes.Recruitment, "0", Destinations.IdentityToken);
                            }
                            break;

                        case "timesheet":
                            if (item.IsActive)
                            {
                                identity.AddClaim(CustomClaimTypes.Timesheet, "1", Destinations.IdentityToken);
                            }
                            else
                            {
                                identity.AddClaim(CustomClaimTypes.Timesheet, "0", Destinations.IdentityToken);
                            }
                            break;

                        case "performance":
                            if (item.IsActive)
                            {
                                identity.AddClaim(CustomClaimTypes.Performance, "1", Destinations.IdentityToken);
                            }
                            else
                            {
                                identity.AddClaim(CustomClaimTypes.Performance, "0", Destinations.IdentityToken);
                            }
                            break;
                        case "salesMarketing":
                            if (item.IsActive)
                            {
                                identity.AddClaim(CustomClaimTypes.SalesMarketing, "1", Destinations.IdentityToken);
                            }
                            else
                            {
                                identity.AddClaim(CustomClaimTypes.SalesMarketing, "0", Destinations.IdentityToken);
                            }
                            break;

                        case "expanseManagement":
                            if (item.IsActive)
                            {
                                identity.AddClaim(CustomClaimTypes.ExpanseManagement, "1", Destinations.IdentityToken);
                            }
                            else
                            {
                                identity.AddClaim(CustomClaimTypes.ExpanseManagement, "0", Destinations.IdentityToken);
                            }
                            break;
                        default:
                            break;
                    }
                }

            }

            if (ticket.Principal.HasScope(Scopes.Phone))
            {
                if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                    identity.AddClaim(CustomClaimTypes.Phone, user.PhoneNumber, Destinations.IdentityToken);

            }

            if (ticket.Principal.HasScope(Scopes.Email))
            {
                if (user.IsSuperAdmin)
                {
                    identity.AddClaim(CustomClaimTypes.Type, "superadmin", Destinations.IdentityToken);
                }
                else
                {

                    var roles = user.Roles;
                }
            }
            return ticket;
        }
    }
}
