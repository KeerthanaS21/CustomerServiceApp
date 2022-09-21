using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Interfaces;
using ProfileService.Models;
using ProfileService.Wrappers;

namespace ProfileService.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        public ProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="profileModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddProfile([FromBody] UserProfileModel profileModel)
        {
            var result = await _userProfileService.AddUserProfile(profileModel);
            return Ok(result);
        }

        /// <summary>
        /// Update user contact details
        /// </summary>
        /// <param name="profileModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddContact([FromBody] UserContactModel contactModel)
        {
            var result = await _userProfileService.AddContactDetails(contactModel);
            return Ok(result);
        }

        /// <summary>
        /// Get user profiles
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProfiles([FromQuery] string userName)
        {
            var result = await _userProfileService.ProfileDetails(userName);
            return Ok(result);
        }

        /// <summary>
        /// Get user contacts
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetContacts([FromQuery] string userName)
        {
            var result = await _userProfileService.ContactDetails(userName);
            return Ok(result);
        }


    }
}
