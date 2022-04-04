using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Regions;
using TrilliaAccountBook.Views;

namespace TrilliaAccountBook.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public LoginViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoginCommand = new DelegateCommand(LoginCommandExecute);

        }
        public DelegateCommand LoginCommand { get; }

        private void LoginCommandExecute()
        {
            // Menu表示
            var p = new NavigationParameters();
            //p.Add(nameof(MenuViewModel.OperatorCode), OperatorCode);
            _regionManager.RequestNavigate("ContentRegion", nameof(Menu), p);

        }

    }
}
