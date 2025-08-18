using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAuthenticationService _userService;
        private readonly ITokenService _tokenService;
        private readonly IEmployeeService _employeeService;
        private readonly ApplicationDBContext _context;
        private readonly IRefreshTokenService _refreshTokenService;
        public AccountController(IUserAuthenticationService userService,
                                 ITokenService tokenService,
                                 IEmployeeService employeeService,
                                 ApplicationDBContext context,
                                 IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _employeeService = employeeService;
            _context = context;
            _refreshTokenService = refreshTokenService;
        }

        /// <summary>
        /// Register new account
        /// </summary>
        [HttpPost("Register")]
        [Authorize(Policy = "Account.Register")]
        public async Task<IActionResult> Register([FromBody] InsertAccountRequest account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new AppUser
            {
                UserName = account.UserName,
                Email = account.Email,
            };

            var initEmployee = await _userService.InitEmployee(account);
            var createUser = await _userService.CreateAccount(appUser, account.Password, initEmployee);
            if (!createUser.Succeeded)
                return StatusCode(500, createUser.Errors);

            var assignUserRole = await _userService.AddAccountToRole(appUser, account.OrgRole);
            if (!assignUserRole.Succeeded)
                return StatusCode(500, assignUserRole.Errors);

            var getEmployeeInformation = await _employeeService.GetEmployeeByIdAsync(initEmployee.EmployeePub_ID);
            var (token, refreshToken) = await _tokenService.CreateToken(appUser, getEmployeeInformation!.EmployeePub_ID);
            return Ok(new AccountResponse
            {
                UserName = account.UserName,
                Email = account.Email
            });
        }

        /// <summary>
        /// Log-in account
        /// </summary>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LogInRequest login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExist = await _userService.GetAccountbyUserName(login.UserName);

            if (userExist is null)
                return Unauthorized("User not found!");

            var checkUser = await _userService.AccountSignIn(userExist, login.PassWord, false);

            if (!checkUser.Succeeded)
                return Unauthorized("Invalid credentials!");

            var employeeInformation = await _employeeService.GetEmployeeByGuidAsync(userExist.Id);

            if (employeeInformation is not null)
            {
                var emp_logTokens = await _refreshTokenService.GetRefreshToken(userExist.Id);
                if (emp_logTokens is not null)
                    await _refreshTokenService.DeleteRefreshToken(emp_logTokens);

                var (emp_token, emp_refreshToken) = await _tokenService
                                                    .CreateToken(userExist, employeeInformation.EmployeePub_ID);

                //Store refresh token in Httponly cookie
                Response.Cookies.Append("___rt", emp_refreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

                //Store token in Httponly cookie
                Response.Cookies.Append("___at", emp_token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(15)
                });

                return Ok();
            }

            var logTokens = await _refreshTokenService.GetRefreshToken(userExist.Id);
            if (logTokens is not null)
                await _refreshTokenService.DeleteRefreshToken(logTokens);

            var (token, refreshToken) = await _tokenService.CreateToken(userExist);

            //Store refresh token in Httponly cookie
            Response.Cookies.Append("___rt", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            //Store token in Httponly cookie
            Response.Cookies.Append("___at", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            return Ok();
        }

        /// <summary>
        /// Token refresh
        /// </summary>
        /// <param name="request">
        /// Parameters for token refresh.
        /// </param>        
        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.RefreshToken));
            var hashTokenFromClient = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

            var refreshToken = await _refreshTokenService.GetRefreshToken(hashTokenFromClient);

            if (refreshToken is null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
                return Unauthorized("Invalid refresh token");

            var newPlaintoken = await _tokenService.Refresh_Token(refreshToken);

            var newJwt = await _tokenService.CreateTokenOnly(refreshToken.AppUser, request.EmployeeId);

            //delete the cookies
            Response.Cookies.Delete("___rt", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            Response.Cookies.Delete("___at", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            //Store refresh token in Httponly cookie
            Response.Cookies.Append("___rt", newPlaintoken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            //Store token in Httponly cookie
            Response.Cookies.Append("___at", newJwt, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            return Ok();
        }

        /// <summary>
        /// Log-out account  
        /// </summary>        
        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.RefreshToken));
            var hashTokenFromClient = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

            var refreshToken = await _refreshTokenService.GetRefreshToken(hashTokenFromClient);

            if (refreshToken is not null)
            {
                await _refreshTokenService.Revoked(refreshToken, true);

                Response.Cookies.Delete("___rt", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                Response.Cookies.Delete("___at", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
                return Ok();
            }

            return Unauthorized();
        }
    }
}
