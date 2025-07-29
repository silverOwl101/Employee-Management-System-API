using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Request;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System_API.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAuthenticationService _userService;
        private readonly ITokenService _tokenService;
        private readonly IEmployeeService _employeeService;
        public AccountController(IUserAuthenticationService userService,
                                 ITokenService tokenService,
                                 IEmployeeService employeeService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _employeeService = employeeService;
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
            return Ok(new AccountResponse
            {
                UserName = account.UserName,
                Email = account.Email,
                Token = await _tokenService.CreateToken(appUser, getEmployeeInformation!.EmployeePub_ID)
            });
        }


        /// <summary>
        /// Log-in
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
                return Ok(new AccountResponse
                {
                    UserName = userExist.UserName,
                    Email = userExist.Email,
                    Token = await _tokenService.CreateToken(userExist, employeeInformation.EmployeePub_ID)
                });
            }

            return Ok(new AccountResponse
            {
                UserName = userExist.UserName,
                Email = userExist.Email,
                Token = await _tokenService.CreateToken(userExist)
            });
        }
    }
}
