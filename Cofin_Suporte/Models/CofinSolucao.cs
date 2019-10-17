using System;
using System.Collections.Generic;

namespace Cofin_Suporte.Models
{
    public partial class CofinSolucao
    {
        public int IdCofin { get; set; }
        public int IdSolucao { get; set; }
        public bool Check { get; set; }

        public virtual Cofin IdCofinNavigation { get; set; }
        public virtual Solucao IdSolucaoNavigation { get; set; }
    }
}
