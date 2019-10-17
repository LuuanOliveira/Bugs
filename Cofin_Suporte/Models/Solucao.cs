using System;
using System.Collections.Generic;

namespace Cofin_Suporte.Models
{
    public partial class Solucao
    {
        public Solucao()
        {
            CofinSolucao = new HashSet<CofinSolucao>();
        }

        public int IdSolucao { get; set; }
        public string DescricaoSolução { get; set; }

        public virtual ICollection<CofinSolucao> CofinSolucao { get; set; }
    }
}
