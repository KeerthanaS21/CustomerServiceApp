using CustomerRequestService.DatabaseEntity;
using CustomerRequestService.Enum;
using CustomerRequestService.Interfaces;
using CustomerRequestService.Models;
using CustomerRequestService.Strings;
using CustomerRequestService.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace CustomerRequestService.Services
{
    public class UserRequestService : IUserRequestService
    {
        private readonly CustomerDatabaseContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRequestService> _logger;

        public UserRequestService(CustomerDatabaseContext dbContext, ILogger<UserRequestService> logger, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<BaseResponse<string>> CreateRequest(RequestDetails requestData)
        {
            var serviceRequest = new ServiceRequest();
            serviceRequest.RequestType = requestData.RequestType;
            serviceRequest.RequestDetail = requestData.RequestDetail;
            serviceRequest.RequestStatus = StatusEnum.Unopen.ToString();
            serviceRequest.RequestCreatedBy = requestData.RequestCreatedBy;
            serviceRequest.RequestCreatedDate = DateTime.Today.Date;

            _dbContext.ServiceRequests.Add(serviceRequest);
            await _dbContext.SaveChangesAsync();

            return BaseResponse<string>.Ok(ResponseString.MessageAfterRequestCreated);

        }

        public async Task<BaseResponse<List<RequestDetails>>> GetRequests(string userName)
        {
            int roleNum = _dbContext.AspNetUsers.Where(u => u.UserName == userName).Select(x => x.UserRole).FirstOrDefault();
            List<RequestDetails> requestDetails = new List<RequestDetails>();
            if (roleNum == 1)
            {
                var serviceRequest = _dbContext.ServiceRequests.Where(x => x.RequestCreatedBy == userName).ToList();
                requestDetails = GetRequest(serviceRequest);
                return BaseResponse<List<RequestDetails>>.Ok(requestDetails);
            }
            var serviceRequests = await _dbContext.ServiceRequests.ToListAsync();
            requestDetails = GetRequest(serviceRequests);
            return BaseResponse<List<RequestDetails>>.Ok(requestDetails);
        }

        public async Task<BaseResponse<string>> AssignRequestTo(RequestDetails requestData)
        {
            if(requestData.RequestStatus.ToLower() == StatusEnum.Closed.ToString().ToLower())
            {
                return BaseResponse<string>.Ok(ResponseString.MessageWhenRequestClosed);
            }
            string id = _dbContext.AspNetUsers.Where(x => x.UserName == requestData.RequestAssignedTo).Select(x=>x.Id).FirstOrDefault();
            var request = _dbContext.ServiceRequests.Where(x => x.RequestId == requestData.RequestId).FirstOrDefault();
            request.RequestAssignedTo = id;
            request.RequestStatus = StatusEnum.Inprogress.ToString();
            _dbContext.Update(request);
            await _dbContext.SaveChangesAsync();
            return BaseResponse<string>.Ok(ResponseString.MessageAfterAssign);
        }

        public async Task<BaseResponse<string>> AddRequestComment(RequestDetails requestData)
        {
            var request = _dbContext.ServiceRequests.Where(x => x.RequestId == requestData.RequestId).FirstOrDefault();
            request.RequestAssigneeComments = request.RequestAssigneeComments +  "   " + requestData.RequestAssigneeComments;
            if(requestData.RequestStatus.ToLower() == StatusEnum.Closed.ToString().ToLower())
            {
                request.RequestClosedDate = DateTime.Now.Date;
            }
            request.RequestStatus = requestData.RequestStatus;
            _dbContext.ServiceRequests.Update(request);
            await _dbContext.SaveChangesAsync();
            return BaseResponse<string>.Ok(ResponseString.MessageAfterAddComment);
        }

        public List<RequestDetails> GetRequest(List<ServiceRequest> serviceRequests)
        {
            List<RequestDetails> requestDetails = new List<RequestDetails>();   
            foreach (ServiceRequest serviceRequest in serviceRequests)
            {
                RequestDetails request = new RequestDetails();
                request.RequestId = serviceRequest.RequestId;
                request.RequestType = serviceRequest.RequestType;
                request.RequestDetail =     serviceRequest.RequestDetail;
                request.RequestStatus = serviceRequest.RequestStatus;
                request.RequestAssignedTo = _dbContext.AspNetUsers.Where(x => x.Id == serviceRequest.RequestAssignedTo).Select(x => x.UserName).FirstOrDefault(); ;
                request.RequestAssigneeComments = serviceRequest.RequestAssigneeComments;
                request.RequestCreatedDate = serviceRequest.RequestCreatedDate.Date;
                request.RequestCreatedBy = serviceRequest.RequestCreatedBy;
                request.RequestClosedDate = serviceRequest.RequestClosedDate;
                requestDetails.Add(request);
            }
            return requestDetails;
        }

    }
}
