using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Livraria.Dominio.Model.Entidades
{
    [Table("Livro")]
    public class LivroEntidade
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string Editora { get; set; }

        public int NumeroPaginas { get; set; }

        [DisplayName("Foto")]
        public string ImageUri { get; set; }

        [DisplayName("Última Visualização")]
        public DateTime? UltimaVisualizacao { get; set; }

        [DisplayName("Quantidade de Visualizações")]
        public int QtdVisualizacao { get; set; }
    }
}
