using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA
{
    public class Populasyon
    {
        //populasyonun buyuklugu
        private static int buyukluk = 20;
        //mutasyın ihtimali
        public static double mutasyon_olasiligi = 00.7;
        public List<Kromozom> Kromozomlar { get; set; }
        public Populasyon(List<Kromozom> k)
        { this.Kromozomlar = k; }

        //ilk kullanım için
        public static Populasyon RastgeleOlustur_Kromozom()
        {
            List<Kromozom> krm = new List<Kromozom>();
            krm.Clear();
            Random r = new Random();
            Gen g = null;
            for (int i = 0; i < buyukluk; i++)
            {
                bool durum = true;
                while (durum)
                {
                    int a = r.Next(-10, 10);
                    int b = r.Next(-10, 10);
                    int tmp = krm.Where(p => p.Gen.X == a && p.Gen.Y == b).Count();
                    if (tmp == 0)
                    {
                        g = new Gen() { X = a, Y = b };
                        krm.Add(new Kromozom(g));
                        durum = false;
                    }
                }
            }
            return new Populasyon(krm);
        }
      
        //kriterler
        public double EnBuyukSonuc()
        {
            if (Kromozomlar == null)
                RastgeleOlustur_Kromozom();
            return Kromozomlar.OrderBy(p => p.Sonuc).FirstOrDefault().Sonuc;
        }
        public double EnKucukSonuc()
        {
            if (Kromozomlar == null)
                RastgeleOlustur_Kromozom();
            return Kromozomlar.OrderByDescending(p => p.Sonuc).FirstOrDefault().Sonuc;
        }
        public double Toplam()
        {
            var toplam = Convert.ToDouble(Kromozomlar.Sum(p => p.Sonuc));
            return toplam;
        }
        public double Ortalamasi()
        {
            return (Toplam()) / Kromozomlar.Count;
        }

        //genetik algoritma işlemleri
        //kromozomlar en optimum şekilde caprazlanarak en iyi sonucu almamız için çaprazlanır
        public static Kromozom[] Caprazla(Kromozom k1,Kromozom k2)
        {
            Kromozom tmp1 = new Kromozom(new Gen { X = k1.Gen.X, Y = k2.Gen.Y });
            Kromozom tmp2 = new Kromozom(new Gen { X = k2.Gen.X, Y = k1.Gen.Y });
            Kromozom[] k = { tmp1, tmp2 };
            return k;
        }
        //Benzerlikleri ortadan kaldırmak için
        public static Kromozom Mutasyon(Kromozom k1)
        {
            Kromozom tmp = k1;
            k1.Gen.X = tmp.Gen.Y;
            k1.Gen.Y = tmp.Gen.X;
            return k1;
        }
    }
}
