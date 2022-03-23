using System.Threading.Tasks;
using Books.Models.TokenAuth;
using Books.Web.Controllers;
using Shouldly;
using Xunit;

namespace Books.Web.Tests.Controllers
{
    public class HomeController_Tests: BooksWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}