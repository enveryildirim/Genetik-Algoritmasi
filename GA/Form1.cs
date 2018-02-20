using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool KontrolEt()
        {
            bool durum = false;
            try
            {
                double d=Convert.ToDouble(txt_deger.Text);
            }
            catch 
            {
                durum = true;
               
            }
            return durum;
        }


        List<Populasyon> Populasyonlar = new List<Populasyon>();
        private void button1_Click(object sender, EventArgs e)
        {
            if(KontrolEt())
            {
                MessageBox.Show("Degerlerde Hata oldu Tekrar deneyiniz");
                return;
            }
            
            //degerler atanıyor 
            //uygunluk değerleri bölümü
            double iter_sayisi = (double)numericUpDown_iter.Value;
            int pop_sayisi = (int)numericUpDown_popsayisi.Value;
            double deger=Convert.ToDouble(txt_deger.Text);
            Populasyonlar.Clear();

            //başlangıç optimizasyonu bölümü
            //ilk kullanım için populasyona rastgele degerler atanıyor
            Populasyon p1 = Populasyon.RastgeleOlustur_Kromozom();
           
            Populasyon tmp = p1;
            Populasyonlar.Add(p1);
            List<Kromozom> tmp_krmzlr=null;

            //istenilen degere en uygun ve yakın sonucu bulmak için çaprazlama işlemi yapılıyor...
            for (int i = 0; i < iter_sayisi; i++)
            {
                tmp_krmzlr = new List<Kromozom>();
                //çaprazlama bölümü
                //çaprazlama işlemi bir baştan bir sondan olarak yapılıyor isteğe değişik methodlar kullanılabilir.
                int index= tmp.Kromozomlar.Count%2==0?tmp.Kromozomlar.Count/2:(tmp.Kromozomlar.Count-1)/2;
                
                for (int j = 0; j < index; j++)
                {
                    var aa = Populasyon.Caprazla(tmp.Kromozomlar[j], tmp.Kromozomlar[(tmp.Kromozomlar.Count - 1) - j]);
                    //mutasyon bölümü
                    //caprazlama olurken aynı degerleri uretme ihtimaline karşı mutasyon işlemi yapılıyor 
                    Random r = new Random();
                    //olasılığa göre ve rastgele sayı göre
                    if (r.Next(100) > (Populasyon.mutasyon_olasiligi * 100)) 
                    {
                        aa[0] = Populasyon.Mutasyon(aa[0]);
                        aa[1] = Populasyon.Mutasyon(aa[1]);
                    }
                    tmp_krmzlr.Add(aa[0]);
                    tmp_krmzlr.Add(aa[1]);
                }
                tmp = new Populasyon(tmp_krmzlr);
                //caprazlanan popuslasyon listeye atılıyor
                Populasyonlar.Add(tmp);
            }
            

            //optimum çözüm bölümü
            //istenilen sonucu göre en yakın degerler bulunuyor

            //istenilen degere olan uzaklık hesaplanıyor listeye ekleniyor
            //istenilen method kullanılabilir.
            Dictionary<Populasyon,double> optimum_degerler = new Dictionary<Populasyon,double>();

            foreach (var item in Populasyonlar)
            {
                double fark = Math.Abs(item.Ortalamasi()-deger);
                optimum_degerler.Add(item,fark);
            }
            //en kucukten buyuge doğru sıralanıyor
            var dd = optimum_degerler.OrderBy(p => p.Value).Take(10).ToList();

            //uygun şekillerin cizdirilmesi
            chart1.Series.Clear();
            for (int i = 0; i < pop_sayisi; i++)
            {
                chart1.Series.Add(new Series() { Name = i.ToString(), ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
                BorderWidth=3});
          
                    chart1.Series[i.ToString()].Color = Color.FromArgb(Color.Black.ToArgb() + (i*10500));
               
                foreach (var item in dd[i].Key.Kromozomlar)
                {
                    chart1.Series[i.ToString()].Points.AddXY(item.Gen.X,item.Sonuc);
                }
 
            }
          
           }
           
           
        }
       
    }

