using System;
using System.Collections.Generic;

namespace Cofin_Suporte.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Cofin = new HashSet<Cofin>();
        }

        public int IdCategoria { get; set; }
        public string DescricaoCategoria { get; set; }

        public virtual ICollection<Cofin> Cofin { get; set; }
    }
}
