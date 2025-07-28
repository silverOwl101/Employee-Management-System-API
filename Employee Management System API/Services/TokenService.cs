using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Employee_Management_System_API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IEmployeeService _employeeService;

        public TokenService(IConfiguration configuration,
                            IUserAuthenticationService userAuthenticationService,
                            IEmployeeService employeeService)
        {
            _configuration = configuration;
            var signingKey = _configuration["JWT:SigningKey"]
                            ?? throw new InvalidOperationException("JWT configuration not found!");
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            _userAuthenticationService = userAuthenticationService;
            _employeeService = employeeService;
        }

        public async Task<string> CreateToken(AppUser user, string employeeId)
        {
            var userClaims = await _userAuthenticationService.GetAccountClaims(user);
            var userRoles = await _userAuthenticationService.GetAccountRole(user);
            var employeeDetails = await _employeeService.GetEmployeeByIdAsync(employeeId);
            var roleClaims = new List<Claim>();
            if (userRoles is not null)
            {
                foreach (var role in userRoles)
                {
                    var roleObj = await _userAuthenticationService.FindAccountRole(role);
                    if (roleObj != null)
                    {
                        var claims = await _userAuthenticationService.GetAccountRoleClaims(roleObj);
                        roleClaims.AddRange(claims);
                    }
                }
            }           
            if (user.Email is not null && user.UserName is not null && employeeDetails is not null)
                return CreatingTokenForSA(userClaims, roleClaims, user, employeeDetails);

            return string.Empty;
        }
        public async Task<string> CreateToken(AppUser user)
        {
            var userClaims = await _userAuthenticationService.GetAccountClaims(user);
            var userRoles = await _userAuthenticationService.GetAccountRole(user);            
            var roleClaims = new List<Claim>();
            if (userRoles is not null)
            {
                foreach (var role in userRoles)
                {
                    var roleObj = await _userAuthenticationService.FindAccountRole(role);
                    if (roleObj != null)
                    {
                        var claims = await _userAuthenticationService.GetAccountRoleClaims(roleObj);
                        roleClaims.AddRange(claims);
                    }
                }
            }            
            if (user.Email is not null && user.UserName is not null)
                return CreatingTokenForSA(userClaims, roleClaims, user);

            return string.Empty;
        }

        // For employee token generator
        private string CreatingTokenForSA(IEnumerable<Claim> userClaims,
                                          IEnumerable<Claim> roleClaims,
                                          AppUser user,
                                          EmployeeResponse employeeDetails)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!),
                    new Claim("EmployeeID",employeeDetails!.EmployeePub_ID),
                    new Claim("DepartmentID",employeeDetails!.DepartmentPub_ID),
                    new Claim("RoleID",employeeDetails!.RolePub_ID)
                }.Union(userClaims)
                 .Union(roleClaims)
                 .ToList();

            var creds = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
        // For sa token generator
        private string CreatingTokenForSA(IEnumerable<Claim> userClaims,
                                          IEnumerable<Claim> roleClaims,
                                          AppUser user)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!),                    
                }.Union(userClaims)
                 .Union(roleClaims)
                 .ToList();

            var creds = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
