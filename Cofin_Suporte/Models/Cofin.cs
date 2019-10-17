using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cofin_Suporte.Models
{
    public partial class Cofin
    {
        public Cofin()
        {
            CofinSolucao = new HashSet<CofinSolucao>();
        }

        public int IdCofin { get; set; }

        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Formato inválido")]
        public DateTime? Data { get; set; }
        [Required(ErrorMessage = "Preenchimento Obrigatório")]
        public string Observacao { get; set; }
        public int IdTipo { get; set; }
        public int IdCategoria { get; set; }
        public int IdBacklog { get; set; }

        [NotMapped]
        public virtual ICollection<Solucao> Solucoes { get; set; } /*todas as solucoes*/

        [NotMapped]
        public virtual int[] Solution { get; set; } /*solucoes set pelo usuario*/
        //public virtual ICollection<Solucao> Solution { get; set; }

        public virtual Backlog IdBacklogNavigation { get; set; }
        public virtual Categoria IdCategoriaNavigation { get; set; }
        public virtual Tipo IdTipoNavigation { get; set; }
        public virtual ICollection<CofinSolucao> CofinSolucao { get; set; } /*relacionamento das tabelas */
    }
}
