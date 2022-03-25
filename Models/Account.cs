using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCoreMVVM_EF.Models
{
    public class Account
    {
        public int Id { get; set; }
        public Nullable<DateTime> Date_repair { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public int Real_working_hours { get; set; }
        public string Type_of_repair { get; set; }
        public bool Status { get; set; }
        public Nullable<DateTime> Date_fail { get; set; }
        public virtual Object Object { get; set; }
    }
}
