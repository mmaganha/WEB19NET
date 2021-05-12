using Microsoft.AspNetCore.Http;
using ProjetoWEB19NET.Dominio.ValidacaoModels;
using System;

namespace Dominio.Modelos
{
    public class Receita : Entidade
    {
        public Receita()
        {

        }
        
        public Receita(string titulo, string descricao, string ingredientes, string modoPreparo, int idCategoria)
        {
            Titulo = titulo;
            Descricao = descricao;
            Ingredientes = ingredientes;
            ModoPreparo = modoPreparo;
            IdCategoria = idCategoria;
            this.Validate<Receita>(this, new ReceitaValidacao());
        }

        public int Id { get; set; } 
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Ingredientes { get; set; }
        public string ModoPreparo { get; set; }
        public IFormFile Foto { get; set; }
        public string Tags { get; set; }
        public int IdCategoria { get; set; }
    }
}
