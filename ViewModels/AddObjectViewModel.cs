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
    internal class AddObjectViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;
        public Models.Object NewObject { get; set; }
        public Models.Type ObjectType { get; set; }
        public AddObjectViewModel()
        {

        }
        public AddObjectViewModel(Models.Object obj)
        {
            NewObject = obj;
            ObjectType = NewObject.Type;
        }
        public AddObjectViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }


        private List<Models.Type> allType = ContextDB.GetContext().Types.ToList();//получение всех должностей
        public List<Models.Type> AllType
        {
            get
            {
                return allType;
            }
            private set
            {
                allType = value;
                OnPropertyChanged("AllType");
            }
        }

        public ICommand AddNewPersonal
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewPersonalExecuted, CanOpenAddNewPersonalExecute);
            }
        }
        private bool CanOpenAddNewPersonalExecute(object p) => true;
        private void OnOpenAddNewPersonalExecuted(object p)
        {
            /*MessageBox.Show(PersonalPosition.Id.ToString());
            MessageBox.Show(PersonalPosition.Name);
            MessageBox.Show(PersonalPosition.ToString());*/

            bool check = ContextDB.GetContext().Objects.Any(el => el.Failure_time == NewObject.Failure_time &&
                                                                  el.Real_working_hours == NewObject.Real_working_hours &&
                                                                  el.Tech_working_hours == NewObject.Tech_working_hours &&
                                                                  el.Humidity == NewObject.Humidity &&
                                                                  el.Tech_humidity == NewObject.Tech_humidity &&
                                                                  el.Airiness == NewObject.Airiness &&
                                                                  el.Heat == NewObject.Heat &&
                                                                  el.Tech_heat == NewObject.Tech_heat &&
                                                                  el.All_working_hours == NewObject.All_working_hours &&
                                                                  el.Name == NewObject.Name &&
                                                                  el.Status == NewObject.Status &&
                                                                  el.Type == ObjectType);
            if (!check)
            {
                Models.Object obj = new Models.Object
                {
                    Failure_time = NewObject.Failure_time,
                    Real_working_hours = NewObject.Real_working_hours,
                    Tech_working_hours = NewObject.Tech_working_hours,
                    Humidity = NewObject.Humidity,
                    Tech_humidity = NewObject.Tech_humidity,
                    Airiness = NewObject.Airiness,
                    Heat = NewObject.Heat,
                    Tech_heat = NewObject.Tech_heat,
                    All_working_hours = NewObject.All_working_hours,
                    Note = NewObject.Note,
                    Name = NewObject.Name,
                    Status = NewObject.Status,
                    Type = ObjectType,
                    ////////////////////////////расчет Критерия надежности!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    Reliability=1,

                };
                ContextDB.GetContext().Objects.Add(obj);
                ContextDB.GetContext().SaveChanges();
            }
            else
            {
                MessageBox.Show("Данное оборудование уже внесено в базу данных");
            }

        }


        /*public ICommand UpdateComboBoxPosition
        {
            get
            {
                return new LambdaCommand(OnUpdateComboBoxPositionExecuted, CanUpdateComboBoxPositionExecute);
            }
        }
        private bool CanUpdateComboBoxPositionExecute(object p) => true;
        private void OnUpdateComboBoxPositionExecuted(object p)
        {
            allPositions = ContextDB.GetContext().Positions.ToList();
            OnPropertyChanged("AllPositions");
        }*/
        //public string ObjcetAddType { get; set; }
        public ICommand OpenAddNewTypeWnd
        {
            get
            {
                return new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
            }
        }
        private bool CanOpenAddNewPositionWndExecute(object p) => true;
        private void OnOpenAddNewPositionWndExecuted(object p)
        {
            AddTypeWnd newTypeWindow = new AddTypeWnd();
            SetCenterPositionAndOpen(newTypeWindow);
            allType = ContextDB.GetContext().Types.ToList();//получение всех должностей
            OnPropertyChanged("AllType");
        }


}
}
