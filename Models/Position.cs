using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace WPFCoreMVVM_EF.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Personal> Personals { get; set; }
    }
}
