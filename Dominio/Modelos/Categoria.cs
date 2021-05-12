using ProjetoWEB19NET.Dominio.ValidacaoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Modelos
{
    public class Categoria : Entidade
    {
        public Categoria()
        {

        }
        public Categoria(string descricao)
        {
            Descricao = descricao;
            this.Validate<Categoria>(this, new CategoriaValidacao());
        }

        public int IdCategoria { get; set; }
        public string Descricao { get; set; }
    }
}
