using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using TrilliaAccountBook.Views;

namespace TrilliaAccountBook.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private string _title = "Trillia Account Book";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(Login));

            LogoutCommand = new DelegateCommand(LogoutCommandExecute);

        }
        public DelegateCommand LogoutCommand { get; }
        private void LogoutCommandExecute()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(Login));
        }

    }
}
