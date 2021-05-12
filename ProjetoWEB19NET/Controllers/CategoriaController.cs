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
    public class CategoriaController : Controller
    {
        private readonly ServiceCategoria serviceCategoria;
        private readonly IMapper mapper;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceCategoria"></param>
        /// <param name="mapper"></param>
        public CategoriaController(ServiceCategoria serviceCategoria, IMapper mapper)
        {
            this.serviceCategoria = serviceCategoria;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retornar lista de categorias.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Listar")]
        public async Task<ActionResult<List<CategoriaView>>> Listar()
        {
            try
            {
                var lista = this.mapper.Map<List<CategoriaView>>(await this.serviceCategoria.GetCategorias());
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma categoria.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetCategoria/{id:int}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            try
            {
                var categoria = this.mapper.Map<Categoria>(await this.serviceCategoria.GetCategoria(id));
                return Ok(categoria);
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Incluir uma categoria.
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns>Retorna o objeto com seu respectivo id.</returns>
        [HttpPost("Incluir")]
        public async Task<ActionResult<CategoriaView>> Incluir([FromBody] CategoriaView categoria)
        {
            try
            {
                var resultado = this.mapper.Map<Categoria>(categoria);
                if (resultado.Valid)
                {
                    return this.Ok(this.mapper.Map<CategoriaView>(await this.serviceCategoria.Adicionar(resultado)));
                }
                else
                {
                    return this.Ok(resultado.Lista);
                }                
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Alterar uma categoria informando o id.
        /// </summary>
        /// <param name="categoria">Modelo para ser preenchido.</param>
        /// <returns>Status 200.</returns>
        [HttpPost("Alterar")]
        public async Task<ActionResult> Alterar([FromBody] CategoriaView categoria)
        {
            try
            {
                var resultado = this.mapper.Map<Categoria>(categoria);
                if (resultado.Valid)
                {
                    await this.serviceCategoria.Alterar(resultado);
                    return this.Ok();
                }
                else
                {
                    return this.Ok(resultado.Lista);
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
        [HttpDelete("Excluir/{id:int}")]
        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                await this.serviceCategoria.Excluir(id);
                return Ok();
            }
            catch 
            {
                return StatusCode(500);
            }
        }
    }
}
