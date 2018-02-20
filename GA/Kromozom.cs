using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA
{
    public class Kromozom
    {
        //private const int gensayisi=20;

        public Gen Gen { get; set; }
        public double Sonuc
        {
            get 
            {
                //burada formulu uyguluyoruz
                double a=((Gen.X+(2*Gen.Y)-7));
                double b=((2*Gen.X)+Gen.Y-5);
                return Math.Pow(a,2)+Math.Pow(b,2) ; 
            }
  
        }
    
        public Kromozom(Gen g)
        {
            this.Gen = g;
        }
    }
}
