using Logging_JWT.Helpers;
using Logging_JWT.Models;
using Logging_JWT.Services;
using Microsoft.AspNetCore.Mvc;

namespace Logging_JWT.Controllers
{
    /// <summary>
    /// Controller for handling user-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">The user service to be injected.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticates user credentials.
        /// </summary>
        /// <param name="model">The authentication request model.</param>
        /// <returns>Returns IActionResult containing the authentication response.</returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var Respone = _userService.Authenticate(model);
            if (Respone == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(Respone);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Returns IActionResult containing the list of users.</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
