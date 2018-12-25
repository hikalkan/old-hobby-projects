using System;
using System.Drawing;
using System.Windows.Forms;

namespace iboCAD
{
	//Araç kutusundaki bir aracýn tipini saklamak için enum
	public enum AracTipi {Secme, Tasima, Dikdortgen, Cember, Elips, Dogru, DogruSerisi};
	//Araç kutusundaki bir aracýn genel olarak görevi
	public enum AracGorevi {Secme, Tasima, Cizim}
	//Araç kutusundaki bir aracý göstermek için sýnýf
	public class Arac
	{
		//private ve public deðiþkenler
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
		//araçlarý göstermek için kullanýlan grafiklerin bulunduðu klasörü dönderir
		private static String aracDizini()
		{
			return System.IO.Directory.GetCurrentDirectory()+"\\grafik\\araclar\\";
		}
		//mouse ile araça týklanýnca bu aracý seç
		private void resimKutusu_Click(object sender, EventArgs e)
		{
			aracKutusu.SeciliArac = this;
		}
		//mouse iþaretçisi aracý temsil eden þeklin üstüne geldiðinde görüntüyü deðiþtir
		private void resimKutusu_MouseEnter(object sender, EventArgs e)
		{
			if(!secili)
				resimKutusu.Image = ustundeGrafik;
		}
		//mouse iþaretçisi aracý temsil eden þekli terkettiðinde duruma baðlý olarak görüntüyü deðiþtir
		private void resimKutusu_MouseLeave(object sender, EventArgs e)
		{
			if(secili)
				resimKutusu.Image = seciliGrafik;
			else
				resimKutusu.Image = normalGrafik;
		}
		//bu aracýn seçili olma durumunu verir/dönderir
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
		//bu aracýn tipini dönderir
		public AracTipi Tip
		{
			get
			{
				return tip;
			}
		}
		//bu araç için çizim tipini dönderir
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
		//bu aracýn görevini dönderir
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
