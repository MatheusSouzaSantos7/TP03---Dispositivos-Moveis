using PackageTracker.Models;
using PackageTracker.ViewModels;

namespace PackageTracker.Views
{
    public partial class ResultsPage : ContentPage, IQueryAttributable
    {
        public ResultsPage()
        {
            InitializeComponent();
        }

        // Este método é chamado automaticamente pelo Shell quando a página é navegada
        // com parâmetros (via dicionário). Aqui tratamos 'PackageInfo' (objeto) ou 'code' (string).
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query == null) return;

            // Caso 1: recebeu o objeto Package diretamente
            if (query.TryGetValue("PackageInfo", out var pkgObj) && pkgObj is Package pkg)
            {
                // Usa ResultsViewModel e atribui o Package
                if (BindingContext is ResultsViewModel vm)
                {
                    vm.PackageInfo = pkg;
                }
                else
                {
                    var vm2 = new ResultsViewModel { PackageInfo = pkg };
                    // GoBackCommand já é criado no ResultsViewModel
                    BindingContext = vm2;
                }
                return;
            }

            // Caso 2: recebeu apenas o código 'code' (string) — vamos simular a busca aqui
            if (query.TryGetValue("code", out var codeObj) && codeObj is string codeStr)
            {
                // Simula a criação do Package (mesma lógica de mock do TrackingViewModel)
                var today = DateTime.Now;
                var mockPackage = new Package
                {
                    TrackingId = codeStr,
                    Status = "Em trânsito",
                    ShippingDate = today.AddDays(-3),
                    EstimatedDeliveryDate = today.AddDays(2),
                    CurrentLocation = "Centro de Distribuição - São Paulo"
                };

                mockPackage.Events.Add(new PackageEvent(today.AddDays(-3), "Curitiba", "Objeto postado"));
                mockPackage.Events.Add(new PackageEvent(today.AddDays(-2), "Curitiba", "Objeto encaminhado para São Paulo"));
                mockPackage.Events.Add(new PackageEvent(today.AddDays(-1), "São Paulo", "Objeto em trânsito para o destino"));

                // Atribui ao ViewModel da página
                if (BindingContext is ResultsViewModel vm)
                {
                    vm.PackageInfo = mockPackage;
                }
                else
                {
                    var vm2 = new ResultsViewModel { PackageInfo = mockPackage };
                    BindingContext = vm2;
                }
            }
        }
    }
}
