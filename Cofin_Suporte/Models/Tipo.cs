using System;
using System.Collections.Generic;

namespace Cofin_Suporte.Models
{
    public partial class Tipo
    {
        public Tipo()
        {
            Cofin = new HashSet<Cofin>();
        }

        public int IdTipo { get; set; }
        public string DescricaoTipo { get; set; }

        public virtual ICollection<Cofin> Cofin { get; set; }
    }
}
