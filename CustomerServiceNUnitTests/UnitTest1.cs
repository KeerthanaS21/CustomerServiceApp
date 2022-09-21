using AccountService.Controllers;
using AccountService.Interfaces;
using AccountService.Models;
using AccountService.Strings;
using AccountService.Wrappers;
using CustomerRequestService.Interfaces;
using CustomerRequestService.Models;
using Moq;
using ProfileService.Controllers;
using ProfileService.Interfaces;
using ProfileService.Models;
using ProfileService.Strings;

namespace CustomerServiceNUnitTests
{
    [TestFixture]
    public class Tests
    {
        public Tests()
        {
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestLogin()
        {
            LoginCreds loginCreds = new LoginCreds();
            loginCreds.UserEmail = "sam@gmail.com";
            loginCreds.UserPassword = "123456";

            var modelResponse = new AuthenticationModel();
            var mockresult = BaseResponse<AuthenticationModel>.Ok(modelResponse);

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.LoginUserAsync(loginCreds)).ReturnsAsync(mockresult);

            AccountController accountController = new AccountController(mockService.Object);
            var result = await accountController.Login(loginCreds);
            var obj = result.GetType().GetProperties().First(x => x.Name == "Value").GetValue(result);
            bool isResultTrue = Convert.ToBoolean(obj.GetType().GetProperty("Success").GetValue(obj));
            Assert.True(isResultTrue);

        }
        [Test]
        public async Task TestGetProfiles()
        {
            string name = "Tom";
            List<ProfileModel> profileModels = new List<ProfileModel>();
            UserProfileModel userProfileModel = new UserProfileModel();
            ProfileModel profileModel = new ProfileModel();
            profileModel.ProfileId = 2;
            profileModel.UserName = "Tom";
            profileModel.FirstName = "Tom";
            profileModel.LastName = "Cruise";
            profileModel.IdType = "aadhaar";
            profileModel.IdValue = "EJKSGY5497K";
            profileModels.Add(profileModel);
            var mockresult = BaseResponse<string>.Ok(ResponseString.MessageAfterContactUpdate);

            var mockService = new Mock<IUserProfileService>();
            //mockService.Setup(x => x.AddUserProfile(userProfileModel)).Returns(mockresult);

            ProfileController profileController = new ProfileController(mockService.Object);
            var result = profileController.AddProfile(userProfileModel);
            var obj = result.GetType().GetProperties().First(x => x.Name == "Value").GetValue(result);
            bool isResultTrue = Convert.ToBoolean(obj.GetType().GetProperty("Success").GetValue(obj));
            Assert.True(isResultTrue);
        }

    }
}