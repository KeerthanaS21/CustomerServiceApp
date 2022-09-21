using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProfileService.DatabaseEntity;
using ProfileService.Interfaces;
using ProfileService.Models;
using ProfileService.Strings;
using ProfileService.Wrappers;
using System.Linq;

namespace ProfileService.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly CustomerDatabaseContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserProfileService> _logger;

        public UserProfileService(CustomerDatabaseContext dbContext, ILogger<UserProfileService> logger, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<BaseResponse<string>> AddUserProfile(UserProfileModel profileData)
        {
            var id = _dbContext.AspNetUsers.Where(x => x.UserName == profileData.UserName).Select(x => x.Id).ToList()[0];

            var userProfile = new Profile();
            userProfile.FirstName = profileData.FirstName;
            userProfile.LastName = profileData.LastName;
            userProfile.Dob = profileData.Dob.Date;
            userProfile.IdType = profileData.IdType;
            userProfile.IdValue = profileData.IdValue;
            userProfile.UserRefId = id;

            bool profileExists = (_dbContext.Profiles.Where(x => x.UserRefId == id).FirstOrDefault() is null) ? false : true;

            if (profileExists)
            {
                var profileDetails = _dbContext.Profiles.Where(x => x.UserRefId == id).FirstOrDefault();
                profileDetails.FirstName = profileData.FirstName;
                profileDetails.LastName = profileData.LastName;
                profileDetails.Dob = profileData.Dob.Date;
                profileDetails.IdType = profileData.IdType;
                profileDetails.IdValue = profileData.IdValue;
                _dbContext.Profiles.Update(profileDetails);
                await _dbContext.SaveChangesAsync();
                return BaseResponse<string>.Ok(ResponseString.MessageAfterProfileDetailsUpdate);
            }
            _dbContext.Profiles.Add(userProfile);
            _dbContext.SaveChanges();
            return BaseResponse<string>.Ok(ResponseString.MessageAfterProfileDetailsIncluded);
        }

        public async Task<BaseResponse<string>> AddContactDetails(UserContactModel contactData)
        {
            var id = _dbContext.AspNetUsers.Where(x => x.UserName == contactData.UserName).Select(x => x.Id).ToList()[0];

            var userContact = new Contact();
            userContact.UserRefId = id;
            userContact.UserName = contactData.UserName;
            userContact.Address = contactData.Address;
            userContact.City = contactData.City;
            userContact.State = contactData.State;
            userContact.Country = contactData.Country;
            userContact.ContactPreference = contactData.ContactPreference;
            userContact.PhoneNo = contactData.PhoneNo;

            bool contactExists = (_dbContext.Contacts.Where(x => x.UserRefId == id).FirstOrDefault() is null) ? false : true;

            if (contactExists)
            {
                var contactDetails = _dbContext.Contacts.Where(x => x.UserRefId == id).FirstOrDefault();

                contactDetails.Address = contactData.Address;
                contactDetails.City = contactData.City;
                contactDetails.State = contactData.State;
                contactDetails.Country = contactData.Country;
                contactDetails.ContactPreference = contactData.ContactPreference;
                contactDetails.PhoneNo = contactData.PhoneNo;
                _dbContext.Contacts.Update(contactDetails);
                await _dbContext.SaveChangesAsync();
                return BaseResponse<string>.Ok(ResponseString.MessageAfterContactUpdate);
            }

            _dbContext.Contacts.Add(userContact);
            _dbContext.SaveChanges();
            return BaseResponse<string>.Ok(ResponseString.MessageAfterContactAdded);
        }

        public async Task<BaseResponse<List<ContactModel>>> ContactDetails(string name)
        {
            int userRole = _dbContext.AspNetUsers.Where(x => x.UserName == name).Select(x => x.UserRole).FirstOrDefault();
            List<ContactModel> contactModels = new List<ContactModel>();

            if (userRole == 0)
            {
                var customers = await _dbContext.AspNetUsers.Where(x => x.UserRole == 1).Select(x => x.UserName).ToListAsync();
                var contacts = _dbContext.Contacts.ToList();

                foreach (var contact in contacts)
                {
                    if (!customers.Contains(contact.UserName))
                    {
                        continue;
                    }
                    ContactModel contactModel = new ContactModel();
                    contactModel.ContactId = contact.ContactId;
                    contactModel.Address = contact.Address;
                    contactModel.City = contact.City;
                    contactModel.State = contact.State;
                    contactModel.Country = contact.Country;
                    contactModel.ContactPreference = contact.ContactPreference;
                    contactModel.PhoneNo = contact.PhoneNo;
                    contactModels.Add(contactModel);
                }
                return BaseResponse<List<ContactModel>>.Ok(contactModels);
            }
            else
            {
                var contact = _dbContext.Contacts.Where(x=>x.UserName==name).First();
                if(contact != null)
                {
                    ContactModel contactModel = new ContactModel();
                    contactModel.ContactId = contact.ContactId;
                    contactModel.Address = contact.Address;
                    contactModel.City = contact.City;
                    contactModel.State = contact.State;
                    contactModel.Country = contact.Country;
                    contactModel.ContactPreference = contact.ContactPreference;
                    contactModel.PhoneNo = contact.PhoneNo;
                    contactModels.Add(contactModel);
                    return BaseResponse<List<ContactModel>>.Ok(contactModels);
                }
                return BaseResponse<List<ContactModel>>.Ok(contactModels);
            }

        }

        public async Task<BaseResponse<List<ProfileModel>>> ProfileDetails(string name)
        {
            int userRole = _dbContext.AspNetUsers.Where(x => x.UserName == name).Select(x => x.UserRole).FirstOrDefault();
            List<ProfileModel> profileModels = new List<ProfileModel>();
            if (userRole == 0)
            {
                var customers = await _dbContext.AspNetUsers.Where(x => x.UserRole == 1).Select(x => x.Id).ToListAsync();
                var profiles = _dbContext.Profiles.ToList();
                
                foreach (var profile in profiles)
                {
                    if (!customers.Contains(profile.UserRefId))
                    {
                        continue;
                    }

                    string userName = _dbContext.AspNetUsers.Where(x => x.Id == profile.UserRefId).Select(x => x.UserName).FirstOrDefault().ToString();

                    ProfileModel profileModel = new ProfileModel();
                    profileModel.ProfileId = profile.ProfileId;
                    profileModel.UserName = userName;
                    profileModel.FirstName = profile.FirstName;
                    profileModel.LastName = profile.LastName;
                    profileModel.IdType = profile.IdType;
                    profileModel.IdValue = profile.IdValue;
                    profileModel.Dob = profile.Dob.Date;
                    profileModels.Add(profileModel);
                }
                return BaseResponse<List<ProfileModel>>.Ok(profileModels);
            }
            else
            {
                var profile = _dbContext.Profiles.Where(x => x.FirstName == name).FirstOrDefault();
                if(profile != null)
                {
                    ProfileModel profileModel = new ProfileModel();
                    profileModel.ProfileId = profile.ProfileId;
                    profileModel.UserName = name;
                    profileModel.FirstName = profile.FirstName;
                    profileModel.LastName = profile.LastName;
                    profileModel.IdType = profile.IdType;
                    profileModel.IdValue = profile.IdValue;
                    profileModel.Dob = profile.Dob.Date;
                    profileModels.Add(profileModel);
                    return BaseResponse<List<ProfileModel>>.Ok(profileModels);
                }
                return BaseResponse<List<ProfileModel>>.Ok(profileModels);
            }
            
        }
    }
}
