using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamaskaVremena
{
    public class Namaz
    {
        public DateTime Sabah { get; set; }
        public DateTime Podne { get; set; }

        public DateTime Ikindija { get; set; }

        public DateTime Aksam { get; set; }

        public DateTime Jacija { get; set; }

    }

    public class Sunce
    {
        public DateTime Izlazak { get; set; }
        public DateTime Zalazak { get; set; }
    }

}
