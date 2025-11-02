using PackageTracker.Views;

namespace PackageTracker.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            string trackingCode = TrackingCodeEntry.Text?.Trim();

            if (string.IsNullOrEmpty(trackingCode))
            {
                await DisplayAlert("Aviso", "Por favor, insira um código de rastreamento.", "OK");
                return;
            }

            // Navegar para a página de resultados usando rota nomeada e enviando um parâmetro 'code'
            await Shell.Current.GoToAsync(nameof(ResultsPage), true, new Dictionary<string, object>
            {
                { "code", trackingCode }
            });
        }
    }
}
