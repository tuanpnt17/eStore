using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eStore.Helpers
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anomyous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _sessionStorage.GetAsync<UserSession>("UserSession");
                var userSession = token.Success ? token.Value : null;
                if(userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_anomyous));
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.Role),
                new Claim(ClaimTypes.StreetAddress, userSession.City),
                new Claim(ClaimTypes.Country, userSession.Country),
                new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString())
            }, "CustomAuth"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch (Exception)
            {

                return await Task.FromResult(new AuthenticationState(_anomyous));

            }
        }
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                 claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.Role),
                new Claim(ClaimTypes.StreetAddress, userSession.City),
                new Claim(ClaimTypes.Country, userSession.Country),
                new Claim(ClaimTypes.NameIdentifier, userSession.Id.ToString())
            }, "CustomAuth"));
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anomyous;
            }
            
        }
        //public async Task MarkUserAsAuthenticated(string token)
        //{
        //    await _localStorage.SetItemAsync("authToken", token);

        //    var authenticatedUser = new ClaimsPrincipal(
        //        new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));

        //    var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        //    NotifyAuthenticationStateChanged(authState);
        //}

        //public async Task MarkUserAsLoggedOut()
        //{
        //    await _localStorage.RemoveItemAsync("authToken");

        //    var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        //    var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        //    NotifyAuthenticationStateChanged(authState);
        //}

        //private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var token = handler.ReadJwtToken(jwt);
        //    return token.Claims;
        //}
    }
}
