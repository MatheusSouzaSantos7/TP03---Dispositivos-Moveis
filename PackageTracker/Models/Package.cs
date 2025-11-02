using System;// Matheus Angelo de Souza Santos - CB3025489
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTracker.Models
{
    public class Package
    {
        public string TrackingId { get; set; }
        public string Status { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string CurrentLocation { get; set; }
        public ObservableCollection<PackageEvent> Events { get; set; }


        public Package()
        {
            Events = new ObservableCollection<PackageEvent>();
        }
    }
}