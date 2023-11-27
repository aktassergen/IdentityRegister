using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication2.Data;

namespace WebApplication2.Token
{
    //public class TokenGenerator
    //{
    //    public string GenerateToken(string email = "boş", string id = "0", string role = "member")
    //    {
    //        //hasan
    //        //id 
    //        //ef core role table  ve claim table sorgu atarsın
    //        //gelen verileri claim olarak eklersin
    //        // artım claim ve rollere ek 
    //        //hasanın adı' email'i soyadı, id'si bu gibi bilgileri claim olarak ekleyebilirsin


    //        //findbyıd => email, d.t , adı, soyadı, 
    //        //getcliamsby => user'ın cliamlerin
    //        //getrolebyıd => user role(claim)

    //        var myClaims = new List<Claim>()
    //        {

    //            new Claim(ClaimTypes.Role,role),
    //            new Claim(ClaimTypes.Email,email),
    //            new Claim(ClaimTypes.NameIdentifier,id)

    //        };

    //        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();


    //        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hasanhasan11111hasanhasan11111"));

    //        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);

    //        JwtSecurityToken token = new JwtSecurityToken(
    //            issuer: "http://localhost",
    //            audience: "http://localhost",
    //            notBefore: DateTime.Now,
    //            expires: DateTime.Now.AddHours(1),
    //            signingCredentials: signingCredentials,
    //            claims: myClaims);

    //        return handler.WriteToken(token);


    //    }

//    }
}
