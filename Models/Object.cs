using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCoreMVVM_EF.Models
{
    public class Object
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Failure_time { get; set; }//Время наработки на отказ
        public int Real_working_hours { get; set; }//Время работы после ремонта
        public int Tech_working_hours { get; set; }//Технически заявленное время работы
        public int Reliability { get; set; }//Критерий надежности
        public int Humidity { get; set; }//Влажность в пространстве
        public int Tech_humidity { get; set; }//Заявленная влажность в пространстве
        public int Airiness { get; set; }//Проветриваемость в пространстве
        public int Heat { get; set; }//Жара в пространстве
        public int Tech_heat { get; set; }//Заявленная жара в пространстве
        public int All_working_hours { get; set; }//Общее время работы
        //public string Type { get; set; }
        public int TypeId { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }//enable/disable
        public virtual Type Type { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
