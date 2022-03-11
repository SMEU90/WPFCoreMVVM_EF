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
    internal class AddPositionViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;

       // private AddPositionViewModel _PositionViewModel;
        public AddPositionViewModel(/*AddPositionViewModel PositionViewModel*/)
        {
           // _PositionViewModel = PositionViewModel;
        }
        public AddPositionViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }

        public string PositionName { get; set; }


        public ICommand AddNewPosition
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewPositionExecuted, CanOpenAddNewPositionExecute);
            }
        }
        private bool CanOpenAddNewPositionExecute(object p) => true;
        private void OnOpenAddNewPositionExecuted(object p)
        {

            bool check = ContextDB.GetContext().Positions.Any(el => el.Name == PositionName ); 

            if (!check)
            {
                Position position = new Position
                {
                    Name = PositionName,
                };
                ContextDB.GetContext().Positions.Add(position);
                ContextDB.GetContext().SaveChanges();

            } else
            {
                MessageBox.Show("Данная должность уже имеется в базе данных");
            }
        }


    }
}
