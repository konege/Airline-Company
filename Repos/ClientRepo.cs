using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using midterm_project.Model;

namespace Repos
{
    public class ClientRepo
    {
        private IConfiguration _configuration;
        public ClientRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Client? CreateClient(SignUp model)
        {
            using var context = new KonegeContext();
            try
            {
                var client = new Client
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = model.Username,
                    ClientPassword = model.Password
                };
                client = CreateToken(client);

                context.Clients.Add(client);
                context.SaveChanges();
                return client;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Client? GetClientByUsername(string username)
        {
            using var context = new KonegeContext();
            try
            {
                var client = context.Clients.Single(u => u.Username == username);
                return client;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Client? GetClientByToken(string token)
        {
            using var context = new KonegeContext();
            try
            {
                var client = context.Clients.Single(u => u.Token == token);
                return client;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Client? GetClientLogin(Login login)
        {
            using var context = new KonegeContext();
            try
            {
                var client = context.Clients.Single(u => u.Username == login.Username && u.ClientPassword == login.Password);
                return client;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private Client CreateToken(Client client)
        {
            var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Email, client.Username),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var token = GetToken(authClaims);
            client.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return client;
        }
        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMonths(12),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}