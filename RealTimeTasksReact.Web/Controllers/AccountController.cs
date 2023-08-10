using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealTimeTasksReact.Data;
using RealTimeTasksReact.Web.ViewModels;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.SignalR;

namespace RealTimeTasksReact.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    { 
        private readonly string _connectionString;
        private IHubContext<TaskHub> _hub;

        public AccountController(IConfiguration configuration, IHubContext<TaskHub> hub)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _hub = hub;

        }

        [HttpPost]
        [Route("signup")]
        public void Signup(UserWithPassword user)
        {
            var repo = new Repo(_connectionString);
            repo.AddUser(user, user.Password);
        }
        [HttpPost]
        [Route("login")]
        public User Login(LoginViewModel viewModel)
        {
            var repo = new Repo(_connectionString);
            var user = repo.Login(viewModel.Email, viewModel.Password);
            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim("user", viewModel.Email)
            };
            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            return user;
        }

        [HttpGet]
        [Route("getcurrentuser")]
        public User GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var repo = new Repo(_connectionString);
            return repo.GetByEmail(User.Identity.Name);
        }

        [HttpGet]
        [Authorize]
        [Route("logout")]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }

    }
}
