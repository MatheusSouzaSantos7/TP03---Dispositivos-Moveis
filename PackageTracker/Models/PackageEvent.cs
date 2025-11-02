using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageTracker.Models
{
    public class PackageEvent
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }


        public PackageEvent(DateTime date, string location, string description)
        {
            Date = date;
            Location = location;
            Description = description;
        }


        public override string ToString()
        {
            return $"{Date:dd/MM/yyyy HH:mm} - {Location}: {Description}";
        }
    }
}