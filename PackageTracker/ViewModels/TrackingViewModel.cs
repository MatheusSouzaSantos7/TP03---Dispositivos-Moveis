using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PackageTracker.Models;
using PackageTracker.Views;


namespace PackageTracker.ViewModels
{
    public class TrackingViewModel : INotifyPropertyChanged
    {
        private string trackingCode;
        private Package packageInfo;


        public string TrackingCode
        {
            get => trackingCode;
            set
            {
                trackingCode = value;
                OnPropertyChanged();
            }
        }


        public Package PackageInfo
        {
            get => packageInfo;
            set
            {
                packageInfo = value;
                OnPropertyChanged();
            }
        }


        public ICommand SearchCommand { get; }
        public Command GoBackCommand { get; internal set; }

        public TrackingViewModel()
        {
            SearchCommand = new Command(ExecuteSearch);
        }


        private async void ExecuteSearch()
        {
            if (string.IsNullOrWhiteSpace(TrackingCode))
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Informe um código de rastreamento válido.", "OK");
                return;
            }

            var random = new Random();
            var today = DateTime.Now;

            var mockPackage = new Package
            {
                TrackingId = TrackingCode,
                Status = "Em trânsito",
                ShippingDate = today.AddDays(-3),
                EstimatedDeliveryDate = today.AddDays(2),
                CurrentLocation = "Centro de Distribuição - São Paulo"
            };

            mockPackage.Events.Add(new PackageEvent(today.AddDays(-3), "Curitiba", "Objeto postado"));
            mockPackage.Events.Add(new PackageEvent(today.AddDays(-2), "Curitiba", "Objeto encaminhado para São Paulo"));
            mockPackage.Events.Add(new PackageEvent(today.AddDays(-1), "São Paulo", "Objeto em trânsito para o destino"));

            PackageInfo = mockPackage;

            await Shell.Current.GoToAsync(nameof(ResultsPage), true, new Dictionary<string, object>
            {
                { "PackageInfo", PackageInfo }
            });

        }



        // Implementação do INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}