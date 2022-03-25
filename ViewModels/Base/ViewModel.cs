using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
        protected bool SetCenterPositionAndOpen(Window window, AddTypeViewModel typeViewModel)//Edit Type
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = typeViewModel;
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(Window window, AddPositionViewModel positionViewModel)//Edit Position
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = positionViewModel;
            return (bool)window.ShowDialog();
        }
        protected bool SetCenterPositionAndOpen(Window window, AddPersonalViewModel personalViewModel)//Edit Personal
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = personalViewModel;
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
