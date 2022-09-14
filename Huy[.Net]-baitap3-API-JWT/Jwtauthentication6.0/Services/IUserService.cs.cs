using Jwtauthentication6._0.Models;
using System.Threading.Tasks;

namespace Jwtauthentication6._0.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthenticationModel> RefreshTokenAsync(string token);
        ApplicationUser GetById(string id);
        bool RevokeToken(string token);
    }
}
