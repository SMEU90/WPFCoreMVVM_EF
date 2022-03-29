using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WPFCoreMVVM_EF.Models;
using WPFCoreMVVM_EF.Views.Windows;

namespace WPFCoreMVVM_EF.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
        protected bool SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(AddPersonalWnd window)//Add Personal
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AddPersonalViewModel personalViewModel = new AddPersonalViewModel();
            window.DataContext = personalViewModel;
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(Window window, AddPersonalViewModel personalViewModel)//Edit Personal
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = personalViewModel;
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(AddTypeWnd window)//Add Type
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = new AddTypeViewModel();
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(Window window, AddTypeViewModel typeViewModel)//Edit Type
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = typeViewModel;
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(AddPositionWnd window)//Add Position
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = new AddPositionViewModel();
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(Window window, AddPositionViewModel positionViewModel)//Edit Position
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = positionViewModel;
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(AddObjectWnd window)//Add Object
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = new AddObjectViewModel();
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(Window window, AddObjectViewModel objectViewModel)//Edit Object
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = objectViewModel;
            return (bool)window.ShowDialog();
        }
        protected void ApplyBlurEffect(Window window)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 4;
            window.Effect = objBlur;
        }
        protected void ClearEffect(Window window)
        {
            window.Effect = null;
        }
    }
}
