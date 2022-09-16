using CustomerRequestService.DatabaseEntity;
using CustomerRequestService.Enum;
using CustomerRequestService.Interfaces;
using CustomerRequestService.Models;
using CustomerRequestService.Strings;
using CustomerRequestService.Wrappers;

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
            serviceRequest.RequestCreatedDate = DateTime.Now;

            _dbContext.ServiceRequests.Add(serviceRequest);
            _dbContext.SaveChanges();

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
            var serviceRequests = _dbContext.ServiceRequests.ToList();
            requestDetails = GetRequest(serviceRequests);
            return BaseResponse<List<RequestDetails>>.Ok(requestDetails);
        }

        public async Task<BaseResponse<string>> AssignRequestTo(RequestDetails requestData)
        {
            if(requestData.RequestStatus == StatusEnum.Closed.ToString())
            {
                return BaseResponse<string>.Ok(ResponseString.MessageWhenRequestClosed);
            }
            var request = _dbContext.ServiceRequests.Where(x => x.RequestId == requestData.RequestId).FirstOrDefault();
            request.RequestAssignedTo = requestData.RequestAssignedTo;
            request.RequestStatus = StatusEnum.Inprogress.ToString();
            _dbContext.Update(request);
            _dbContext.SaveChanges();
            return BaseResponse<string>.Ok(ResponseString.MessageAfterAssign);
        }

        public async Task<BaseResponse<string>> AddRequestComment(RequestDetails requestData)
        {
            var request = _dbContext.ServiceRequests.Where(x => x.RequestId == requestData.RequestId).FirstOrDefault();
            request.RequestAssigneeComments = requestData.RequestAssigneeComments +  "   " + requestData.RequestAssigneeComments;
            if(requestData.RequestStatus == StatusEnum.Closed.ToString())
            {
                request.RequestClosedDate = DateTime.Now;
            }
            request.RequestStatus = requestData.RequestStatus;
            _dbContext.ServiceRequests.Update(request);
            _dbContext.SaveChanges();
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
                request.RequestAssignedTo = serviceRequest.RequestAssignedTo;
                request.RequestAssigneeComments = serviceRequest.RequestAssigneeComments;
                request.RequestCreatedDate = serviceRequest.RequestCreatedDate;
                request.RequestCreatedBy = serviceRequest.RequestCreatedBy;
                request.RequestClosedDate = serviceRequest.RequestClosedDate;
                requestDetails.Add(request);
            }
            return requestDetails;
        }

    }
}
