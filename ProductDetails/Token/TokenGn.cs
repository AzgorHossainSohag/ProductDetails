using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductDetails.Token
{
    public class TokenGn
    {
        public static string tokengen(string email)
        {
            string secret = "dfewsf-f-sd-fs-df-sf-sfsdfsdfsdf-dfsdfsdfs-fdsfsfsfs-sdfsdfsdf-sdfsdfsdf";
            string Issuer = "Api backend";
            string Audience = "Api Users";

            var secretkey = Encoding.UTF8.GetBytes(secret);
            var signingkey = new SymmetricSecurityKey(secretkey);


            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email,email),
            });

            var tokendescription = new SecurityTokenDescriptor
            {
                Issuer = Issuer,
                Audience = Audience,
                Subject = claims,
                SigningCredentials = new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddMinutes(300)
            };

            var tokenhandelar = new JwtSecurityTokenHandler();
            var token = tokenhandelar.CreateToken(tokendescription);
            return tokenhandelar.WriteToken(token);
        }
    }
}
