using ProfileService.Models;
using ProfileService.Wrappers;

namespace ProfileService.Interfaces
{
    public interface IUserProfileService
    {
        Task<BaseResponse<string>> AddUserProfile(UserProfileModel profileData);

        Task<BaseResponse<string>> AddContactDetails(UserContactModel contactData);
        Task<BaseResponse<List<ContactModel>>> ContactDetails(string name);
        Task<BaseResponse<List<ProfileModel>>> ProfileDetails(string name);
    }
}
