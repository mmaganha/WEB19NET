using Dominio.Modelos;
using Dominio.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoWEB19NET.Dominio.ModelViews;
using ProjetoWEB19NET.Dominio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleReceita.Controllers
{
    public class CadastroController : Controller
    {
        private readonly ServiceCategoria serviceCategoria;
        private readonly ServiceReceita serviceReceita;
        private readonly AutoMapper.IMapper mapper;

        public CadastroController(ServiceCategoria serviceCategoria, ServiceReceita serviceReceita, AutoMapper.IMapper mapper)
        {
            this.serviceCategoria = serviceCategoria;
            this.serviceReceita = serviceReceita;
            this.mapper = mapper;
        }


        // GET: CadastroController
        public async Task<ActionResult> Index()
        {
            var lista = this.mapper.Map<List<CategoriaView>>(await serviceCategoria.GetCategorias());
            ViewBag.Categorias = new MultiSelectList(lista, "IdCategoria", "Descricao");
            return View(new ReceitaView());
        }

        [HttpGet]
        public async Task<ActionResult<List<ReceitaViewRetorno>>> Listar()
        {            
            var lista = await this.serviceReceita.GetTs();
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar(ReceitaView receitaView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Receita receita = this.mapper.Map<Receita>(receitaView);
                    await this.serviceReceita.Adicionar(receita);
                    return RedirectToAction(nameof(Listar));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }                
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var receitaView = this.mapper.Map<ReceitaView>(await this.serviceReceita.GetT(id));
                return View(receitaView);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ReceitaView receitaView)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var receita = this.mapper.Map<Receita>(receitaView);
                    await this.serviceReceita.Alterar(receita);
                    return RedirectToAction(nameof(Listar));
                }
                else
                {
                    return View();
                }                
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var receitaView = this.mapper.Map<Receita>(await this.serviceReceita.GetT(id));
            if (receitaView.Id > 0)
            {
                await this.serviceReceita.Excluir(receitaView.Id);
            }
            return RedirectToAction(nameof(Listar));
        }
    }
}
