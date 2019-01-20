using System;
using System.Drawing;

namespace iboCAD
{
	// bir elipsi temsil eden sýnýf
	public class Elips : Sekil
	{
		// elipsin sol üst köþesi noktasý ve boyutlarý
		public Nokta solUstKose;
		public Boyut boyut;
		// boþ kurucu fonksyon
		public Elips()
		{
			solUstKose = new Nokta();
			boyut = new Boyut();
		}
		// koordinatlarý verilen kurucu fonksyon
		public Elips(Nokta solUstKose, Boyut boyut)
		{
			this.solUstKose = solUstKose;
			this.boyut = boyut;
		}
		// elipsi ekrana çizmek için
		public override void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//ekipsin sol üst köþesinin ekrandaki koordinatlarý ve boyutlarý belirleniyor
			Nokta ekrSolUstKose = cizimFormu.ekrandakiNokta(solUstKose);
			Boyut ekrBoyut = cizimFormu.ekrandakiBoyut(boyut);
			//çizim yapýlýyor
			if(secili)
				grafik.DrawEllipse(new Pen(cizgiRengi, 3.0f), ekrSolUstKose.x ,ekrSolUstKose.y , ekrBoyut.genislik , ekrBoyut.yukseklik);
			else
				grafik.DrawEllipse(new Pen(cizgiRengi, Kalinlik), ekrSolUstKose.x ,ekrSolUstKose.y , ekrBoyut.genislik , ekrBoyut.yukseklik);
		}
		// Elipsi taþýmak için
		public override void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			solUstKose.x += yatayMiktar;
			solUstKose.y += dikeyMiktar;
		}
		// elipsin dikdörtgensel olarak sol üst ve sað alt koordinatýný verir
		public override Dikdortgen DikdortgenselKoordinat()
		{
			return new Dikdortgen(solUstKose.Kopyasi(), boyut.kopyasi());
		}
		// elipsin alaný
		public override float Alani()
		{
			return 0.0f; // *** daha sonra formülüne bir kitaptan falan bakýlacak ;-) þimdi ezbere bilmiyorum...
		}
		//verilen noktanýn þeklin çizgilerine olan uzaklýðýný verir
		public override float Uzaklik(Nokta nokta)
		{
			//mantýk: elipsin merkezlerinden birisinin kendisine yakýn olan kýsa kenara
			//olan uzaklýðýna m diyorum. m'in formülü þöyle buldum:
			//m*m - b*m + a*a/4 = 0 (b: dikdörtgenin uzun kenarý, a: kýsa kenarý)
			//daha sonra m yardýmýyla elipsin iki merkezini hesaplýyorum. Sonra bu merkezler
			//yardýmýyla verilen noktanýn elipse olan uzaklýðýný bulacaðým. Þimdi m hesaplanýyor...
			float a, b;
			if(boyut.genislik > boyut.yukseklik)
			{
				a = boyut.yukseklik;
				b = boyut.genislik;
			}
			else
			{
				a = boyut.genislik;
				b = boyut.yukseklik;
			}
			float delta = (float)(Math.Pow(b, 2.0) - Math.Pow(a, 2.0));
			if(delta<0) return 1000.0f; //delta<0 ise kökler sanal, devam etme.
			float kok1 = (float)(b + Math.Sqrt(delta)) / 2.0f;
            float kok2 = (float)(b - Math.Sqrt(delta)) / 2.0f;
			float m  = (kok1 > 0.0f && kok1 < (b / 2.0f)) ? (kok1):(kok2);
			//m hesaplandý. þimdi Merkezler bulunuyor
			Nokta merkez1, merkez2;
			if(boyut.genislik > boyut.yukseklik)
			{
				merkez1 = new Nokta(solUstKose.x + m, solUstKose.y - boyut.yukseklik / 2.0f);
				merkez2 = new Nokta(solUstKose.x + boyut.genislik - m, solUstKose.y - boyut.yukseklik / 2.0f);
			}
			else
			{
				merkez1 = new Nokta(solUstKose.x + boyut.genislik / 2.0f, solUstKose.y - m);
				merkez2 = new Nokta(solUstKose.x + boyut.genislik / 2.0f, solUstKose.y - boyut.yukseklik + m);
			}
			//verilen noktanýn merkezlere olan toplam uzaklýðý hesaplanýyor
			float muzaklik = nokta.Uzaklik(merkez1) + nokta.Uzaklik(merkez2);
			//elipsin üstündeki bir noktanýn merkezlere olan toplam uzaklýðý
			//ile verilen noktanýnki arasýndaki mutlak deðer dönderiliyor
			//b = elipsin üstündeki bir noktanýn merkezlere olan toplam uzaklýðý
			return (float)(Math.Abs(muzaklik - b));
		}
		//verilen noktanýn þeklin çizgilerine belirli bir uzaklýk içinde olup olmadýðýný dönderir
		public override bool Civarinda(Nokta nokta, float uzaklik)
		{
			return (Uzaklik(nokta) <= uzaklik);
		}
		//verilen noktanýn þeklin içinde olup olmadýðýný dönderir
		public override bool Icinde(Nokta nokta)
		{
			return false; //sonra hesaplanacak ***
		}
		//verilen herhangi iki nokta için doðru ve kurallý bir elipse dönderir
		public static Elips KuralliElips(Nokta ilkNokta, Nokta ikinciNokta)
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

			return new Elips(solUstKose, boyut);
		}
	}
}
