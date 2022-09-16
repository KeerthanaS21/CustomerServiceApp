using AccountService.Models;
using AccountService.Wrappers;

namespace AccountService.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<AuthenticationModel>> LoginUserAsync(LoginCreds loginModel);
        Task<BaseResponse<string>> RegisterUserAsync(RegisterAccount request);
    }
}
