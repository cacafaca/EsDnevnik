using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProCode.EsDnevnikMob.ViewModels
{
    public class ErrorPageViewModel : BindableBase
    {
        public ErrorPageViewModel()
        {

        }

        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        private DelegateCommand exitCommand;
        public DelegateCommand ExitCommand => exitCommand ?? (exitCommand = new DelegateCommand(ExecuteExitCommand));

        private void ExecuteExitCommand()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
