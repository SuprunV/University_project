using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.Data.Party
{
    public sealed class CountryData : UniqueData
    {
        public string? ISO { get; set; }
        public string? CountryName { get; set; }
        public string? NativeName { get; set; }
    }
   
}
