using CustomerRequestService.DatabaseEntity;
using CustomerRequestService.Models;
using CustomerRequestService.Wrappers;

namespace CustomerRequestService.Interfaces
{
    public interface IUserRequestService
    {
        Task<BaseResponse<string>> AddRequestComment(RequestDetails requestData);
        Task<BaseResponse<string>> AssignRequestTo(RequestDetails requestData);
        Task<BaseResponse<string>> CreateRequest(RequestDetails requestData);
        List<RequestDetails> GetRequest(List<ServiceRequest> serviceRequests);
        Task<BaseResponse<List<RequestDetails>>> GetRequests(string userName);
    }
}
