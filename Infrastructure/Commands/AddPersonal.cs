using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WPFCoreMVVM_EF.Infrastructure.Commands.Base;
using WPFCoreMVVM_EF;
using WPFCoreMVVM_EF.Models;
using WPFCoreMVVM_EF.ViewModels;
using WPFCoreMVVM_EF.Views.Windows;

namespace WPFCoreMVVM_EF.Infrastructure.Commands
{
    internal class AddPersonal : Command
    {
        private static Window GetWindow(object p) => p as Window ?? App.FocusedWindow ?? App.ActivedWindow;

        protected override bool CanExecute(object p) => GetWindow(p) != null;

        protected override void Execute(object p) => GetWindow(p)?.Close();
    }
}
