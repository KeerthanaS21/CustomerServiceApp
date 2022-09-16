using AccountService.Interfaces;
using AccountService.Models;
using AccountService.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterAccount registerModel)
        {
            var result = await _userService.RegisterUserAsync(registerModel);
            return Ok(result);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<AuthenticationModel>), 200)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginCreds loginModel)
        {
            var result = await _userService.LoginUserAsync(loginModel);
            if (result.Success is true)
            {
                return Ok(result);
            }

            return Ok(result);
        }
    }
}
