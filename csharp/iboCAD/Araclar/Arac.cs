using System;
using System.Drawing;
using System.Windows.Forms;

namespace iboCAD
{
	//Ara� kutusundaki bir arac�n tipini saklamak i�in enum
	public enum AracTipi {Secme, Tasima, Dikdortgen, Cember, Elips, Dogru, DogruSerisi};
	//Ara� kutusundaki bir arac�n genel olarak g�revi
	public enum AracGorevi {Secme, Tasima, Cizim}
	//Ara� kutusundaki bir arac� g�stermek i�in s�n�f
	public class Arac
	{
		//private ve public de�i�kenler
		private PictureBox	resimKutusu;
		private Image		normalGrafik, ustundeGrafik, seciliGrafik;
		private bool		secili = false;
		private Araclar		aracKutusu;
		private AracTipi	tip;
		public  String		isim = "";
		//kurucu fonksyon
		public Arac(Araclar aracKutusu, PictureBox resimKutusu, AracTipi tip, String normalGrafik, String ustundeGrafik, String seciliGrafik)
		{
			try
			{
				this.tip = tip;
				this.aracKutusu = aracKutusu;
				this.resimKutusu = resimKutusu;
				this.normalGrafik = Image.FromFile(aracDizini()+normalGrafik);
				this.ustundeGrafik = Image.FromFile(aracDizini()+ustundeGrafik);
				this.seciliGrafik = Image.FromFile(aracDizini()+seciliGrafik);
				this.resimKutusu.Image = this.normalGrafik;
				this.resimKutusu.Click += new EventHandler(resimKutusu_Click);
				this.resimKutusu.MouseEnter += new EventHandler(resimKutusu_MouseEnter);
				this.resimKutusu.MouseLeave += new EventHandler(resimKutusu_MouseLeave);
			} 
			catch(System.IO.IOException hata)
			{
				MessageBox.Show(hata.Message);
			}
		}
		//ara�lar� g�stermek i�in kullan�lan grafiklerin bulundu�u klas�r� d�nderir
		private static String aracDizini()
		{
			return System.IO.Directory.GetCurrentDirectory()+"\\grafik\\araclar\\";
		}
		//mouse ile ara�a t�klan�nca bu arac� se�
		private void resimKutusu_Click(object sender, EventArgs e)
		{
			aracKutusu.SeciliArac = this;
		}
		//mouse i�aret�isi arac� temsil eden �eklin �st�ne geldi�inde g�r�nt�y� de�i�tir
		private void resimKutusu_MouseEnter(object sender, EventArgs e)
		{
			if(!secili)
				resimKutusu.Image = ustundeGrafik;
		}
		//mouse i�aret�isi arac� temsil eden �ekli terketti�inde duruma ba�l� olarak g�r�nt�y� de�i�tir
		private void resimKutusu_MouseLeave(object sender, EventArgs e)
		{
			if(secili)
				resimKutusu.Image = seciliGrafik;
			else
				resimKutusu.Image = normalGrafik;
		}
		//bu arac�n se�ili olma durumunu verir/d�nderir
		public bool Secili
		{
			get
			{
				return secili;
			}
			set
			{
				secili = value;
				if(secili)
					resimKutusu.Image = seciliGrafik;
				else
					resimKutusu.Image = normalGrafik;
			}
		}
		//bu arac�n tipini d�nderir
		public AracTipi Tip
		{
			get
			{
				return tip;
			}
		}
		//bu ara� i�in �izim tipini d�nderir
		public CizimTipi CizimTip
		{
			get
			{
				switch(tip)
				{
					case AracTipi.Dogru:
						return CizimTipi.Dogru;
					case AracTipi.Dikdortgen:
						return CizimTipi.Dikdortgen;
					case AracTipi.Cember:
						return CizimTipi.Cember;
					case AracTipi.Elips:
						return CizimTipi.Elips;
					case AracTipi.DogruSerisi:
						return CizimTipi.DogruSerisi;
					default:
						return CizimTipi.Dogru;
				}
			}
		}
		//bu arac�n g�revini d�nderir
		public AracGorevi Gorevi
		{
			get
			{
				switch(tip)
				{
					case AracTipi.Dogru:
					case AracTipi.Dikdortgen:
					case AracTipi.Cember:
					case AracTipi.Elips:
					case AracTipi.DogruSerisi:
						return AracGorevi.Cizim;
					case AracTipi.Tasima:
						return AracGorevi.Tasima;
					case AracTipi.Secme:
						return AracGorevi.Secme;
					default:
						return AracGorevi.Cizim;
				}
			}
		}
	}
}
