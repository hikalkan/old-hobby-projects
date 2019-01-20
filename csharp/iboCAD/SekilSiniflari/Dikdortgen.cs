using System;
using System.Drawing;

namespace iboCAD
{
	// bir dikdörtgeni temsil eden sýnýf
	public class Dikdortgen : Sekil
	{
		// dikdörtgenin sol üst ve sað alt köþelerinin koordinatlarý
		public Nokta solUstKose;
		public Boyut boyut;
		// boþ kurucu fonksyon
		public Dikdortgen()
		{
			solUstKose = new Nokta();
			boyut = new Boyut();
		}
		// sol üst köþesi ve boyutlarý verilen kurucu fonksyon
		public Dikdortgen(Nokta solUstKose, Boyut boyut)
		{
			this.solUstKose = solUstKose;
			this.boyut = boyut;
		}
		// sol üst köþesi ve sað alt köþesi verilen kurucu fonksyon
		public Dikdortgen(Nokta solUstKose, Nokta sagAltKose)
		{
			this.solUstKose = solUstKose;
			this.boyut = new Boyut(sagAltKose.x - solUstKose.x, sagAltKose.y - solUstKose.y);
		}
		// dikdörtgeni çizmek için
		public override void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//dikdörtgenin sol üst köþesinin ekrandaki koordinatlarý ve boyutlarý belirleniyor
			Nokta ekrSolUstKose = cizimFormu.ekrandakiNokta(solUstKose);
			Boyut ekrBoyut = cizimFormu.ekrandakiBoyut(boyut);
			//çizim yapýlýyor
			if(secili)
				grafik.DrawRectangle(new Pen(cizgiRengi, 3.0f), ekrSolUstKose.x, ekrSolUstKose.y , ekrBoyut.genislik , ekrBoyut.yukseklik);
			else
				grafik.DrawRectangle(new Pen(cizgiRengi, Kalinlik), ekrSolUstKose.x ,ekrSolUstKose.y , ekrBoyut.genislik , ekrBoyut.yukseklik);
		}
		// dikdörtgeni taþýmak için
		public override void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			solUstKose.x += yatayMiktar;
			solUstKose.y += dikeyMiktar;
		}
		// dikdörtgenin koordinatlarý
		public override Dikdortgen DikdortgenselKoordinat()
		{
			return new Dikdortgen(solUstKose.Kopyasi(), boyut.kopyasi());
		}
		// dikdörtgenin alaný
		public override float Alani()
		{
			return (boyut.genislik * boyut.yukseklik);
		}
		//verilen noktanýn þeklin çizgilerine olan uzaklýðýný verir
		public override float Uzaklik(Nokta nokta)
		{
			return 100.0f;
		}
		//verilen noktanýn þeklin çizgilerine belirli bir uzaklýk içinde olup olmadýðýný dönderir
		public override bool Civarinda(Nokta nokta, float uzaklik)
		{
			//bu dikdörtgenden uzaklik kadar geniþ bir dikdortgen oluþtur
			Dikdortgen buyuk = new Dikdortgen(new Nokta(solUstKose.x-uzaklik, solUstKose.y+uzaklik),
											  new Boyut(boyut.genislik+2.0f*uzaklik, boyut.yukseklik+2.0f*uzaklik));
			//bu dikdörtgenden uzalik kadar küçük bir dikdörtgen oluþtur
			Dikdortgen kucuk = new Dikdortgen(new Nokta(solUstKose.x+uzaklik, solUstKose.y-uzaklik),
											  new Boyut(boyut.genislik-2.0f*uzaklik, boyut.yukseklik-2.0f*uzaklik));
			//büyük dikdörtgenin içinde ve küçük dikdörtgenin dýþýndysa bu dikdörtgen civarýndadýr
			if((buyuk.Icinde(nokta)) && (!kucuk.Icinde(nokta)))
				return true;
			return false;
		}
		//verilen noktanýn þeklin içinde olup olmadýðýný dönderir
		public override bool Icinde(Nokta nokta)
		{
			if( nokta.x > solUstKose.x && nokta.x < (solUstKose.x+boyut.genislik) &&
				nokta.y < solUstKose.y && nokta.y > (solUstKose.y-boyut.yukseklik) )
				return true;
			return false;
		}
		//verilen herhangi iki nokta için doðru ve kurallý bir dikdörtgen dönderir
		public static Dikdortgen KuralliDikdortgen(Nokta ilkNokta, Nokta ikinciNokta)
		{
			Nokta solUstKose = new Nokta();
			Boyut boyut = new Boyut();

			if(ikinciNokta.x >= ilkNokta.x)
			{
				solUstKose.x = ilkNokta.x;
				boyut.genislik = ikinciNokta.x - ilkNokta.x;
			}
			else
			{
				solUstKose.x = ikinciNokta.x;
				boyut.genislik = ilkNokta.x - ikinciNokta.x;
			}

			if(ikinciNokta.y <= ilkNokta.y)
			{
				solUstKose.y = ilkNokta.y;
				boyut.yukseklik = ilkNokta.y - ikinciNokta.y;
			}
			else
			{
				solUstKose.y = ikinciNokta.y;
				boyut.yukseklik = ikinciNokta.y - ilkNokta.y;
			}

			return new Dikdortgen(solUstKose, boyut);
		}
	}
}
