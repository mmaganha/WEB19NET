using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelViews
{
    public class ReceitaViewRetorno
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Ingredientes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ModoPreparo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Byte[] Foto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IdCategoria { get; set; }
    }
}
