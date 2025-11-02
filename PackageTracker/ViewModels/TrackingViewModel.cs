using System; // Matheus Angelo de Souza Santos - CB3025489
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
/*
Tema: Aplicação de Rastreamento logístico com .NET MAUI e MVVM
Descrição do Trabalho: Imagine que você foi contratado por uma empresa de
logística para desenvolver uma aplicação móvel de rastreamento de pacotes. A
empresa deseja que a aplicação permita que os clientes rastreiem o status de
seus pacotes em tempo real.
Requisitos: Crie um projeto .NET MAUI no Visual Studio ou no Visual Studio
Code, projete a interface do usuário usando XAML. A interface deve incluir:
• Uma página inicial que permita aos clientes inserirem o código de
rastreamento do pacote.
• Uma página de resultados que exiba o status atual do pacote, incluindo
informações como data de envio, data prevista de entrega, localização
atual e histórico de eventos.
• Implemente o padrão de arquitetura MVVM para separar as
responsabilidades de modelo, visualização e lógica de visualização.
• Crie uma classe de modelo para representar informações sobre um
pacote, como ID do pacote, status, data de envio, data prevista de
entrega e histórico de eventos.
• Crie uma classe de ViewModel para gerenciar a lógica de rastreamento
de pacotes. Esta classe deve permitir que os clientes insiram o código de
rastreamento e, em seguida, buscar informações sobre o pacote.
• Implemente a funcionalidade de buscar informações sobre o pacote, que
pode ser simulada usando dados fictícios.
• Exiba as informações do pacote na página de resultados usando
controles XAML apropriados, como rótulos e listas.
• Forneça uma interface de usuário intuitiva que permita aos clientes
navegar entre a página de entrada de código de rastreamento e a página
de resultados.
Entrega: - Fazer em dupla e colocar o nome da dupla no corpo do E-MAIL e em
comentários no código.
- Colocar o código no GITHUB e enviar o endereço do GIT para o professor:
tulermoraes@yahoo.com
- Colocar no título do e-mail => CBTPRDM - Trabalho Prático 03
- Enviar vídeo ou GIF da aplicação funcionando. 
 */