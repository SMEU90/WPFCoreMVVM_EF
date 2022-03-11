using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCoreMVVM_EF.Models
{
    public class Personal
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public int Age { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Surname { get; set; }
        public bool Status { get; set; }

        // public virtual ICollection<Graphs> Graphs { get; set; }
        public virtual Position Position { get; set; }
    }
}
