using System.Windows.Input;
using PackageTracker.Models;

namespace PackageTracker.ViewModels
{
    public class ResultsViewModel : BaseViewModel
    {
        private Package _packageInfo;
        public Package PackageInfo
        {
            get => _packageInfo;
            set => SetProperty(ref _packageInfo, value);
        }

        public ICommand GoBackCommand { get; }

        public ResultsViewModel()
        {
            GoBackCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });
        }
    }
}
