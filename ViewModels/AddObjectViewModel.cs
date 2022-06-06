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
using System.Collections.ObjectModel;
using WPFCoreMVVM_EF.Models;
using WPFCoreMVVM_EF.Models.Base;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFCoreMVVM_EF.ViewModels//add validation
{
    internal class AddObjectViewModel : ViewModel
    {
        private readonly IUserDialog _UserDialog;
        private readonly IDataService _DataService;
        public string AddButtonText { get; set; }
        public Models.Object NewObject { get; set; }
        private Models.Object OldObject { get; set; }
        private bool _isNewObject = true;
        public AddObjectViewModel()
        {
            AddButtonText = "Добавить оборудование";
            AddNewPersonal = new LambdaCommand(OnOpenAddNewPersonalExecuted, CanOpenAddNewPersonalExecute);
            OpenAddNewTypeWnd = new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
            NewObject = new Models.Object();
        }
        public AddObjectViewModel(Models.Object obj)
        {
            AddButtonText = "Изменить оборудование";
            OldObject = obj;
            NewObject = new Models.Object();
            NewObject.Failure_time=obj.Failure_time;
            NewObject.Real_working_hours=obj.Real_working_hours;
            NewObject.Tech_working_hours = obj.Real_working_hours;
            NewObject.Reliability=obj.Reliability;
            NewObject.Status=obj.Status;
            NewObject.Humidity=obj.Humidity;
            NewObject.Tech_humidity=obj.Tech_humidity;
            NewObject.Airiness=obj.Airiness;
            NewObject.Heat=obj.Heat;
            NewObject.Tech_heat=obj.Tech_heat;
            NewObject.All_working_hours = obj.All_working_hours;
            NewObject.Note=obj.Note;
            NewObject.Name=obj.Name;
            NewObject.Type=obj.Type;
            AddNewPersonal = new LambdaCommand(OnOpenAddNewPersonalExecuted, CanOpenAddNewPersonalExecute);
            OpenAddNewTypeWnd = new LambdaCommand(OnOpenAddNewPositionWndExecuted, CanOpenAddNewPositionWndExecute);
            _isNewObject = false;

        }
        public AddObjectViewModel(IUserDialog UserDialog, IDataService DataService)
        {
            _UserDialog = UserDialog;
            _DataService = DataService;
        }

        public ObservableCollection<Models.Type> AllType
        {
            get
            {
                return StaticObservableCollections.allType;
            }
            set
            {
                StaticObservableCollections.allType = value;
            }
        }
        public Command AddNewPersonal { get; }
        private bool CanOpenAddNewPersonalExecute(object p) => true;
        private void OnOpenAddNewPersonalExecuted(object p)
        {
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
                                                                  el.Type == NewObject.Type);
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
                    Type = NewObject.Type,
                    ////////////////////////////расчет Критерия надежности!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    Reliability=1,

                };
                if (_isNewObject)
                {
                    StaticObservableCollections.allObject.Add(obj);
                    ContextDB.GetContext().Objects.Add(obj);
                    ContextDB.GetContext().SaveChanges();
                }
                else
                {
                    ContextDB.GetContext().Objects.Remove(OldObject);
                    StaticObservableCollections.allObject.Remove(OldObject);
                    StaticObservableCollections.allObject.Add(obj);
                    ContextDB.GetContext().Objects.Add(obj);
                    ContextDB.GetContext().SaveChanges();
                    OldObject = obj;
                }
            }
            else
            {
                MessageBox.Show("Данное оборудование уже внесено в базу данных");
            }

        }
        public Command OpenAddNewTypeWnd { get; }
        private bool CanOpenAddNewPositionWndExecute(object p) => true;
        private void OnOpenAddNewPositionWndExecuted(object p)
        {
            AddTypeWnd newTypeWindow = new AddTypeWnd();
            SetCenterPositionAndOpen(newTypeWindow);
        }


}
}
