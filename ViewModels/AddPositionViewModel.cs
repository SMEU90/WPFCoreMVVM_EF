using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCoreMVVM_EF.ViewModels.Base;
using WPFCoreMVVM_EF.Views.Windows;
using WPFCoreMVVM_EF.Infrastructure.Commands;
using WPFCoreMVVM_EF.Services.Interfaces;
using WPFCoreMVVM_EF.Infrastructure.Commands.Base;
using WPFCoreMVVM_EF.Models;
using System.Collections.ObjectModel;
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
        public string AddButtonText { get; set; }
        public AddPositionViewModel()
        {
            AddButtonText = "Добавить должность";
            AddNewPosition = new LambdaCommand(OnOpenAddNewPositionExecuted, CanOpenAddNewPositionExecute);
            NewPosition = new Position();
        }
        public AddPositionViewModel(Position position)
        {
            AddButtonText = "Изменить должность";
            NewPosition = new Position();
            NewPosition.Name = position.Name;
            OldPosition = position;
            AddNewPosition = new LambdaCommand(OnOpenAddNewPositionExecuted, CanOpenAddNewPositionExecute);
            _isNewPosition = false;
        }
        public AddPositionViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }
        private bool _isNewPosition = true;
        public Position NewPosition { get; set; }
        private Position OldPosition { get; set; }
        public Command AddNewPosition { get; }
        private bool CanOpenAddNewPositionExecute(object p) => true;
        private void OnOpenAddNewPositionExecuted(object p)
        {

            bool check = ContextDB.GetContext().Positions.Any(el => el.Name == NewPosition.Name ); 

            if (!check)
            {
                Position newPosition = new Position
                {
                    Name = NewPosition.Name,
                };
                if (_isNewPosition)
                {
                    StaticObservableCollections.allPosition.Add(newPosition);
                    ContextDB.GetContext().Positions.Add(newPosition);
                    ContextDB.GetContext().SaveChanges();
                }
                else
                {
                    ContextDB.GetContext().Positions.Remove(OldPosition);
                    StaticObservableCollections.allPosition.Remove(OldPosition);
                    StaticObservableCollections.allPosition.Add(newPosition);
                    ContextDB.GetContext().Positions.Add(newPosition);
                    ContextDB.GetContext().SaveChanges();
                    OldPosition = newPosition;
                }
            } else
            {
                MessageBox.Show("Данная должность уже имеется в базе данных");
            }
        }


    }
}
