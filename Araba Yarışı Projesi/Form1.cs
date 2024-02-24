using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Araba_Yarışı_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int kazanilanPuan = 0;
        int yolhizi = 5;
        int arabahizi = 5; //benim arabamın hızı
        bool solYon = false;
        bool sagYon = false;
        int digerarabahizi = 5;

        Random rnd = new Random();

        public void OyunuBaslat()
        {
            btn_oyunubaslat.Enabled = false;
            carpmaefekti.Visible = false;

            lbl_yuksekskor.Text = Settings1.Default.YuksekSkor;

            arabahizi = 5;
            digerarabahizi = 5;
            kazanilanPuan = 0;
            //arabanın koordinatı - Kendi arabamın koordinatı
            bizimaraba.Left = 220; //soldan 160 piksellik bir boşluk bırak
            bizimaraba.Top = 350;  //yukarıdan 300 piksellik bir boşluk bırak
            //diğer arabaların koordinatları
            araba1.Left = 100;
            araba1.Top = 50;

            araba2.Left = 320;
            araba2.Top = 50;

            solYon = false;
            sagYon = false;

            carpmaefekti.Left = 165;
            carpmaefekti.Top = 294;

            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OyunuBaslat();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            kazanilanPuan++;
            lbl_puan.Text = kazanilanPuan.ToString();

            yol.Top += yolhizi;
            if (yol.Top > 400)
            {
                yol.Top = -100;
            }
            if (solYon) { bizimaraba.Left -= arabahizi; }
            if (sagYon) { bizimaraba.Left += arabahizi; }

            if (bizimaraba.Left < 1) { solYon = false; }
            else if (bizimaraba.Left + bizimaraba.Width > 510) { sagYon = false; }

            araba1.Top += arabahizi;
            araba2.Top += arabahizi;

            if (araba1.Top > panel1.Height)
            {
                araba1Degistir();
                araba1.Left = rnd.Next(20, 50);
                araba1.Top = rnd.Next(40, 100) * -1;
            }
            if (araba2.Top > panel1.Height)
            {
                araba2Degistir();
                araba2.Left = rnd.Next(200, 300);
                araba2.Top = rnd.Next(40, 140) * -1;
            }
            if (bizimaraba.Bounds.IntersectsWith(araba1.Bounds) || bizimaraba.Bounds.IntersectsWith(araba2.Bounds))
            {
                
                oyunBitti();
            }
        }
        private void araba1Degistir()
        {
            int sira = rnd.Next(1, 7);

            switch (sira)
            {
                case 1:
                    araba1.Image = Properties.Resources.araba3;
                    break;
                case 2:
                    araba1.Image = Properties.Resources.araba7;
                    break;
                case 3:
                    araba1.Image = Properties.Resources.araba3;
                    break;
                case 4:
                    araba1.Image = Properties.Resources.araba4;
                    break;
                case 5:
                    araba1.Image = Properties.Resources.araba5;
                    break;
                case 6:
                    araba1.Image = Properties.Resources.araba6;
                    break;
                case 7:
                    araba1.Image = Properties.Resources.araba7;
                    break;
            }

        }
        private void araba2Degistir()
        {
            int sira = rnd.Next(1, 7);

            switch (sira)
            {
                case 1:
                    araba2.Image = Properties.Resources.araba1;
                    break;
                case 2:
                    araba2.Image = Properties.Resources.araba2;
                    break;
                case 3:
                    araba2.Image = Properties.Resources.araba3;
                    break;
                case 4:
                    araba2.Image = Properties.Resources.araba4;
                    break;
                case 5:
                    araba2.Image = Properties.Resources.araba5;
                    break;
                case 6:
                    araba2.Image = Properties.Resources.araba6;
                    break;
                case 7:
                    araba2.Image = Properties.Resources.araba7;
                    break;
            }
        }
        private void oyunBitti()
        {
            if (Convert.ToInt32(lbl_puan.Text) > Convert.ToInt32(Settings1.Default.YuksekSkor))
            {
                Settings1.Default.YuksekSkor = lbl_puan.Text;
            }
            timer1.Stop();
            btn_oyunubaslat.Enabled = true;
            carpmaefekti.Visible = Enabled;
            bizimaraba.Controls.Add(carpmaefekti);
            carpmaefekti.Location = new Point(7, -5);
            carpmaefekti.BringToFront();
            carpmaefekti.BackColor = Color.Transparent;
            MessageBox.Show("Tebrikler! Kazandığınız Puan:" + lbl_puan.Text, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Left && bizimaraba.Left > 0)
            {
                solYon = true;
            }
            if(e.KeyCode==Keys.Right && bizimaraba.Left+bizimaraba.Width < panel1.Width)
            {
                sagYon=true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) {solYon=false; }
            if(e.KeyCode == Keys.Right) { sagYon=false; }
        }

        private void btn_oyunubaslat_Click(object sender, EventArgs e)
        {
            OyunuBaslat();
        }
    }
}
