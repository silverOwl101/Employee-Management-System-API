using Employee_Management_System_API.Data;
using Employee_Management_System_API.Domain.Entities;
using Employee_Management_System_API.DTOs.Response;
using Employee_Management_System_API.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Employee_Management_System_API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IEmployeeService _employeeService;
        private readonly ApplicationDBContext _context;
        private readonly IRefreshTokenService _refreshTokenService;

        public TokenService(IConfiguration configuration,
                            IUserAuthenticationService userAuthenticationService,
                            IEmployeeService employeeService,
                            ApplicationDBContext context,
                            IRefreshTokenService refreshTokenService)
        {
            _configuration = configuration;
            var signingKey = _configuration["JWT:SigningKey"]
                            ?? throw new InvalidOperationException("JWT configuration not found!");
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            _userAuthenticationService = userAuthenticationService;
            _employeeService = employeeService;
            _context = context;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<(string token, string refreshToken)> CreateToken(AppUser user, string employeeId)
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
                return await CreatingTokenForEmployee(userClaims, roleClaims, user, employeeDetails);

            return (string.Empty, string.Empty);
        }

        public async Task<(string token, string refreshToken)> CreateToken(AppUser user)
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
                return await CreatingTokenForSA(userClaims, roleClaims, user);

            return (string.Empty, string.Empty);
        }
        public async Task<string> CreateTokenOnly(AppUser user, string employeeId)
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

            if (userRoles is not null && userRoles.Contains("SuperAdmin") && user.Email is not null && user.UserName is not null)
                return CreatingTokenForSA_Refresh(userClaims, roleClaims, user);

            if (user.Email is not null && user.UserName is not null && employeeDetails is not null)
                return CreatingTokenForEmp_Refresh(userClaims, roleClaims, user, employeeDetails);

            return string.Empty;
        }

        // For employee token generator
        private async Task<(string token, string refreshToken)> CreatingTokenForEmployee(IEnumerable<Claim> userClaims,
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

            var tokenDescriptor = TokenDescriptor(claims, creds);

            var (plainToken, hashToken) = await Refresh_Token(user);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return (tokenHandler.WriteToken(token), plainToken);
        }

        // For sa token generator
        private async Task<(string token, string refreshToken)> CreatingTokenForSA(IEnumerable<Claim> userClaims,
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

            var tokenDescriptor = TokenDescriptor(claims, creds);

            var (plainToken, hashToken) = await Refresh_Token(user);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return (tokenHandler.WriteToken(token), plainToken);
        }

        private string CreatingTokenForSA_Refresh(IEnumerable<Claim> userClaims,
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

            var tokenDescriptor = TokenDescriptor(claims, creds);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string CreatingTokenForEmp_Refresh(IEnumerable<Claim> userClaims,
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

            var tokenDescriptor = TokenDescriptor(claims, creds);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private async Task<string> GeneratePlainToken()
        {
            return await Task.Run(() =>
            {
                byte[] randomBytes = RandomNumberGenerator.GetBytes(64);
                string plainToken = Convert.ToBase64String(randomBytes);
                return plainToken;
            });
        }
        private async Task<(string plainToken, string hashedToken)> Refresh_Token(AppUser user)
        {
            string plainToken = await GeneratePlainToken();

            byte[] plainBytes = System.Text.Encoding.UTF8.GetBytes(plainToken);
            byte[] hashBytes;

            using (var sha256 = SHA256.Create())
            using (var ms = new MemoryStream(plainBytes))
            using (var cryptoStream = new CryptoStream(ms, sha256, CryptoStreamMode.Read))
            {
                using var hashMs = new MemoryStream();
                await cryptoStream.CopyToAsync(hashMs);
                hashBytes = sha256.Hash!;
            }

            string hashedToken = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            await SaveToken(hashedToken, user);
            return (plainToken, hashedToken);
        }
        public async Task<string> Refresh_Token(RefreshToken refreshOldToken)
        {
            string plainToken = await GeneratePlainToken();

            byte[] plainBytes = System.Text.Encoding.UTF8.GetBytes(plainToken);
            byte[] hashBytes;

            using (var sha256 = SHA256.Create())
            using (var ms = new MemoryStream(plainBytes))
            using (var cryptoStream = new CryptoStream(ms, sha256, CryptoStreamMode.Read))
            {
                using var hashMs = new MemoryStream();
                await cryptoStream.CopyToAsync(hashMs);
                hashBytes = sha256.Hash!;
            }

            string hashedToken = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            await UpdateToken(refreshOldToken, hashedToken);
            return plainToken;
        }
        private async Task SaveToken(string hashedToken, AppUser user)
        {
            var refreshToken = new RefreshToken
            {
                Token = hashedToken,
                Expires = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };
            //_context.RefreshTokens.Add(refreshToken);
            //await _context.SaveChangesAsync();
            await _refreshTokenService.CreateRefreshToken(refreshToken);
        }
        private async Task UpdateToken(RefreshToken oldToken, string newHashedToken)
        {
            //oldToken.Token = newHashedToken;
            //oldToken.Expires = DateTime.UtcNow.AddDays(7);
            //oldToken.IsRevoked = false;

            //await _context.SaveChangesAsync();
            await _refreshTokenService.UpdateRefreshToken(newHashedToken, oldToken);
        }
        private SecurityTokenDescriptor TokenDescriptor(List<Claim>? claims, SigningCredentials? creds)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15), // short-lived token
                SigningCredentials = creds,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
            };

            return tokenDescriptor;
        }
    }
}
