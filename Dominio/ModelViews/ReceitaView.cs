using Microsoft.AspNetCore.Http;

namespace ProjetoWEB19NET.Dominio.ModelViews
{
    /// <summary>
    /// Receita view model.
    /// </summary>
    public class ReceitaView
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
        public IFormFile Foto { get; set; }
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
