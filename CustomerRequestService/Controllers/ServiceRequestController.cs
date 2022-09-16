using CustomerRequestService.Interfaces;
using CustomerRequestService.Models;
using CustomerRequestService.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRequestService.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : Controller
    {
        private readonly IUserRequestService _userRequestService;
        public ServiceRequestController(IUserRequestService userRequestService)
        {
            _userRequestService = userRequestService;
        }

        /// <summary>
        /// create user service request
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateServiceRequest([FromBody] RequestDetails requestData)
        {
            var result = await _userRequestService.CreateRequest(requestData);
            return Ok(result);
        }

        /// <summary>
        /// get all user service request
        /// </summary>
        /// <param name="roleNum"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRequest([FromQuery] string userName)
        {
            var result = await _userRequestService.GetRequests(userName);
            return Ok(result);
        }

        /// <summary>
        /// Assign service request
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [HttpPut("[action]")]
        public async Task<IActionResult> AssignRequest([FromBody] RequestDetails requestData)
        {
            var result = await _userRequestService.AssignRequestTo(requestData);
            return Ok(result);
        }

        /// <summary>
        /// Add  service request comment
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), 200)]
        [HttpPut("[action]")]
        public async Task<IActionResult> AddComment([FromBody] RequestDetails requestData)
        {
            var result = await _userRequestService.AddRequestComment(requestData);
            return Ok(result);
        }
    }
}