using AutoMapper;
using Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;
using ProjetoWEB19NET.Dominio.ModelViews;
using ProjetoWEB19NET.Dominio.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoWEB19NET.Controllers
{
    /// <summary>
    /// Controller.
    /// </summary>
    public class ReceitaController : Controller
    {
        private readonly ServiceReceita serviceReceita;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceReceita"></param>
        /// <param name="mapper"></param>
        public ReceitaController(ServiceReceita serviceReceita, IMapper mapper)
        {
            this.serviceReceita = serviceReceita;
            this.mapper = mapper;
        }

        /// <summary>
        /// Adicionar uma receita.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Retorna objeto com o id que foi adicionado.</returns>
        [HttpPost("Adicionar")]
        public async Task<ActionResult<ReceitaView>> Adicionar([FromBody] ReceitaView model)
        {
            try
            {
                var resultado = this.mapper.Map<Receita>(model);
                if (resultado.Valid)
                {                    
                    return Ok(this.mapper.Map<ReceitaView>(await this.serviceReceita.Adicionar(resultado)));
                }
                else
                {
                    return Ok(resultado.Lista);
                }
                    
            }
            catch 
            {
                return StatusCode(500);
            }            
        }

        /// <summary>
        /// Listar todas as receitas.
        /// </summary>
        /// <returns>Retorna uma lista.</returns>
        [HttpGet("ListarReceita")]
        public async Task<ActionResult<List<ReceitaView>>> ListarReceita()
        {
            try
            {
                var lista = this.mapper.Map<List<ReceitaView>>(await this.serviceReceita.GetTs());
                return Ok(lista);
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retorna uma receita.
        /// </summary>
        /// <param name="id">Id para buscar.</param>
        /// <returns>retorna um objeto.</returns>
        [HttpGet("ReceitaPorId/{id:int}")]
        public async Task<ActionResult<ReceitaView>> ReceitaPorId(int id)
        {
            try
            {
                var receita = this.mapper.Map<ReceitaView>(await this.serviceReceita.GetT(id));
                return Ok(receita);
            }
            catch 
            {                
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Alterar uma receita informando o id.
        /// </summary>
        /// <param name="model">Modelo para ser preenchido.</param>
        /// <returns>Status 200.</returns>
        [HttpPost("AlterarReceita")]
        public async Task<ActionResult> AlterarReceita([FromBody] ReceitaView model)
        {
            try
            {
                var resultado = this.mapper.Map<Receita>(model);
                if (resultado.Valid)
                {
                    await this.serviceReceita.Alterar(resultado);
                    return Ok();
                }
                else
                {
                    return Ok(resultado.Lista);
                }

            }
            catch 
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Excluir um registro informando o id.
        /// </summary>
        /// <param name="id">Id para exclusão.</param>
        /// <returns>Status 200.</returns>
        [HttpDelete("ExcluirReceita/{id:int}")]
        public async Task<ActionResult> ExcluirReceita(int id)
        {
            try
            {
                await this.serviceReceita.Excluir(id);
                return Ok();
            }
            catch 
            {
                return StatusCode(500);
            }
        }
    }
}
