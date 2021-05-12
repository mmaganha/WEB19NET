using AutoMapper;
using Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;
using ProjetoWEB19NET.Dominio.ModelViews;
using ProjetoWEB19NET.Dominio.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleReceita.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ServiceCategoria serviceCategoria;
        private readonly IMapper mapper;
        public CategoriaController(ServiceCategoria serviceCategoria, IMapper mapper)
        {
            this.serviceCategoria = serviceCategoria;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoriaView>>> Listar()
        {
            var lista = this.mapper.Map<List<CategoriaView>>(await this.serviceCategoria.GetCategorias());
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoriaView categoriaView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoria = this.mapper.Map<Categoria>(categoriaView);
                    await this.serviceCategoria.Adicionar(categoria);
                    return RedirectToAction(nameof(Listar));
                }
                else
                {
                    return View(categoriaView);
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<ActionResult<CategoriaView>> Edit(int id)
        {
            var categoriaView = this.mapper.Map<CategoriaView>(await this.serviceCategoria.GetCategoria(id));
            if (categoriaView.IdCategoria > 0)
            {
                return View(categoriaView);
            }
            else
                return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoriaView categoriaView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoria = this.mapper.Map<Categoria>(categoriaView);
                    await this.serviceCategoria.Alterar(categoria);
                    return RedirectToAction(nameof(Listar));
                }
                else
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = this.mapper.Map<Categoria>(await this.serviceCategoria.GetCategoria(id));
            if (categoria.IdCategoria > 0)
            {
                await this.serviceCategoria.Excluir(categoria.IdCategoria);
            }
            return RedirectToAction(nameof(Listar));
        }
    }
}
