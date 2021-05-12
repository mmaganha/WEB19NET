using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static ProjetoWEB19NET.Util.UtilizavelGeral.Util;

namespace ProjetoWEB19NET.Controllers
{
    /// <summary>
    /// Controller.
    /// </summary>
    public class GerarToken : Controller
    {
        /// <summary>
        /// Geração de Token por CHAVE.
        /// </summary>
        /// <param name="usuario">Usuário passado como parametro.</param>
        /// <param name="signingConfigurations"></param>
        /// <param name="tokenConfigurations"></param>
        /// <returns>Retorna um Token vinculado ao CPF informado.</returns>
        /// <response code="200">Retorna token para CPF</response>
        /// <response code="204">Senão gerar Token</response>  
        [AllowAnonymous]
        [HttpPost, Route("geracaoToken")]
        public object GeracaoToken([FromBody] string usuario, [FromServices] SigningConfigurations signingConfigurations, [FromServices] TokenConfigurations tokenConfigurations)
        {
            if (usuario.Length != 0)
            {
                try
                {
                    ClaimsIdentity identity = GetClaimsIdentity(usuario, "");

                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao.AddHours(8);

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });

                    var token = handler.WriteToken(securityToken);                   

                    return new
                    {
                        authenticated = true,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                        accessToken = token,
                        token_type = "Bearer",
                        message = "OK"
                    };
                }
                catch (Exception ex)
                {                  
                    return NotFound(ex.Message);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }
    }
}
