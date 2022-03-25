using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCoreMVVM_EF.ViewModels.Base;
using WPFCoreMVVM_EF.Views.Windows;
using WPFCoreMVVM_EF.Infrastructure.Commands;
using WPFCoreMVVM_EF.Services.Interfaces;
using WPFCoreMVVM_EF.Models;
using WPFCoreMVVM_EF.Models.Base;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFCoreMVVM_EF.ViewModels
{
    internal class AddTypeViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;
        public string TypeName { get; set; }

        public AddTypeViewModel()
        {
        }
        public AddTypeViewModel(string name)
        {
            TypeName=name;
        }
        public AddTypeViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }
        public ICommand AddNewType
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewPositionExecuted, CanOpenAddNewPositionExecute);
            }
        }
        private bool CanOpenAddNewPositionExecute(object p) => true;
        private void OnOpenAddNewPositionExecuted(object p)
        {

            bool check = ContextDB.GetContext().Types.Any(el => el.Name == TypeName);

            if (!check)
            {
                Models.Type type = new Models.Type
                {
                    Name = TypeName,
                };
                ContextDB.GetContext().Types.Add(type);
                ContextDB.GetContext().SaveChanges();

            }
            else
            {
                MessageBox.Show("Данный вид оборудования уже имеется в базе данных");
            }
        }
    }
}
