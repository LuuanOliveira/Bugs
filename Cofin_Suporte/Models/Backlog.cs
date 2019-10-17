using System;
using System.Collections.Generic;

namespace Cofin_Suporte.Models
{
    public partial class Backlog
    {
        public Backlog()
        {
            Cofin = new HashSet<Cofin>();
        }

        public int IdBacklog { get; set; }
        public int Codigo { get; set; }
        public string DescricaoBacklog { get; set; }

        public virtual ICollection<Cofin> Cofin { get; set; }
    }
}
