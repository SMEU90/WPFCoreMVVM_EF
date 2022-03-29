using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WPFCoreMVVM_EF.Models;
using WPFCoreMVVM_EF.Models.Base;

namespace WPFCoreMVVM_EF.ViewModels.Base
{
    internal class StaticObservableCollections
    {
        public static ObservableCollection<Position> allPositions = new ObservableCollection<Position>(ContextDB.GetContext().Positions.ToList());//получение всех должностей
        public static ObservableCollection<Personal> allPersonal = new ObservableCollection<Personal>(ContextDB.GetContext().Personals.ToList());//получение всех сотрудников
        public static ObservableCollection<Models.Type> allType = new ObservableCollection<Models.Type>(ContextDB.GetContext().Types.ToList());//получение всех типов объектов оборудования
        public static ObservableCollection<Account> allAccount = new ObservableCollection<Account>(ContextDB.GetContext().Accounts.ToList());//получение всех заявок объектов оборудования
        public static ObservableCollection<Models.Object> allObject = new ObservableCollection<Models.Object>(ContextDB.GetContext().Objects.ToList());//получение всех объектов оборудования
    }
}
