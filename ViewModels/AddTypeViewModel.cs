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
using WPFCoreMVVM_EF.Infrastructure.Commands.Base;
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
        public string AddButtonText { get; set; }
        public Models.Type NewType { get; set; }
        private Models.Type OldType { get; set; }
        private bool _isNewType = true;

        public AddTypeViewModel()
        {
            AddButtonText = "Добавить";
            AddNewType = new LambdaCommand(OnOpenAddNewPositionExecuted, CanOpenAddNewPositionExecute);
            NewType = new Models.Type();
        }
        public AddTypeViewModel(Models.Type type)
        {
            AddButtonText = "Изменить";
            AddNewType = new LambdaCommand(OnOpenAddNewPositionExecuted, CanOpenAddNewPositionExecute);
            NewType = new Models.Type();
            NewType.Name = type.Name;
            OldType = type;
            _isNewType = false;
        }
        public AddTypeViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }
        public Command AddNewType { get; }
        private bool CanOpenAddNewPositionExecute(object p) => true;
        private void OnOpenAddNewPositionExecuted(object p)
        {

            bool check = ContextDB.GetContext().Types.Any(el => el.Name == NewType.Name);

            if (!check)
            {
                Models.Type type = new Models.Type
                {
                    Name = NewType.Name,
                };
                if (_isNewType)
                {
                    StaticObservableCollections.allType.Add(type);
                    ContextDB.GetContext().Types.Add(type);
                    ContextDB.GetContext().SaveChanges();
                }
                else
                {
                    ContextDB.GetContext().Types.Remove(OldType);
                    StaticObservableCollections.allType.Remove(OldType);
                    StaticObservableCollections.allType.Add(type);
                    ContextDB.GetContext().Types.Add(type);
                    ContextDB.GetContext().SaveChanges();
                    OldType = type;
                }
            }
            else
            {
                MessageBox.Show("Данный вид оборудования уже имеется в базе данных");
            }
        }
    }
}
