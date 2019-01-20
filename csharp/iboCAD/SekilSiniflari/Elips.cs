using System;
using System.Drawing;

namespace iboCAD
{
	// bir elipsi temsil eden s�n�f
	public class Elips : Sekil
	{
		// elipsin sol �st k��esi noktas� ve boyutlar�
		public Nokta solUstKose;
		public Boyut boyut;
		// bo� kurucu fonksyon
		public Elips()
		{
			solUstKose = new Nokta();
			boyut = new Boyut();
		}
		// koordinatlar� verilen kurucu fonksyon
		public Elips(Nokta solUstKose, Boyut boyut)
		{
			this.solUstKose = solUstKose;
			this.boyut = boyut;
		}
		// elipsi ekrana �izmek i�in
		public override void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//ekipsin sol �st k��esinin ekrandaki koordinatlar� ve boyutlar� belirleniyor
			Nokta ekrSolUstKose = cizimFormu.ekrandakiNokta(solUstKose);
			Boyut ekrBoyut = cizimFormu.ekrandakiBoyut(boyut);
			//�izim yap�l�yor
			if(secili)
				grafik.DrawEllipse(new Pen(cizgiRengi, 3.0f), ekrSolUstKose.x ,ekrSolUstKose.y , ekrBoyut.genislik , ekrBoyut.yukseklik);
			else
				grafik.DrawEllipse(new Pen(cizgiRengi, Kalinlik), ekrSolUstKose.x ,ekrSolUstKose.y , ekrBoyut.genislik , ekrBoyut.yukseklik);
		}
		// Elipsi ta��mak i�in
		public override void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			solUstKose.x += yatayMiktar;
			solUstKose.y += dikeyMiktar;
		}
		// elipsin dikd�rtgensel olarak sol �st ve sa� alt koordinat�n� verir
		public override Dikdortgen DikdortgenselKoordinat()
		{
			return new Dikdortgen(solUstKose.Kopyasi(), boyut.kopyasi());
		}
		// elipsin alan�
		public override float Alani()
		{
			return 0.0f; // *** daha sonra form�l�ne bir kitaptan falan bak�lacak ;-) �imdi ezbere bilmiyorum...
		}
		//verilen noktan�n �eklin �izgilerine olan uzakl���n� verir
		public override float Uzaklik(Nokta nokta)
		{
			//mant�k: elipsin merkezlerinden birisinin kendisine yak�n olan k�sa kenara
			//olan uzakl���na m diyorum. m'in form�l� ��yle buldum:
			//m*m - b*m + a*a/4 = 0 (b: dikd�rtgenin uzun kenar�, a: k�sa kenar�)
			//daha sonra m yard�m�yla elipsin iki merkezini hesapl�yorum. Sonra bu merkezler
			//yard�m�yla verilen noktan�n elipse olan uzakl���n� bulaca��m. �imdi m hesaplan�yor...
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
			if(delta<0) return 1000.0f; //delta<0 ise k�kler sanal, devam etme.
			float kok1 = (float)(b + Math.Sqrt(delta)) / 2.0f;
            float kok2 = (float)(b - Math.Sqrt(delta)) / 2.0f;
			float m  = (kok1 > 0.0f && kok1 < (b / 2.0f)) ? (kok1):(kok2);
			//m hesapland�. �imdi Merkezler bulunuyor
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
			//verilen noktan�n merkezlere olan toplam uzakl��� hesaplan�yor
			float muzaklik = nokta.Uzaklik(merkez1) + nokta.Uzaklik(merkez2);
			//elipsin �st�ndeki bir noktan�n merkezlere olan toplam uzakl���
			//ile verilen noktan�nki aras�ndaki mutlak de�er d�nderiliyor
			//b = elipsin �st�ndeki bir noktan�n merkezlere olan toplam uzakl���
			return (float)(Math.Abs(muzaklik - b));
		}
		//verilen noktan�n �eklin �izgilerine belirli bir uzakl�k i�inde olup olmad���n� d�nderir
		public override bool Civarinda(Nokta nokta, float uzaklik)
		{
			return (Uzaklik(nokta) <= uzaklik);
		}
		//verilen noktan�n �eklin i�inde olup olmad���n� d�nderir
		public override bool Icinde(Nokta nokta)
		{
			return false; //sonra hesaplanacak ***
		}
		//verilen herhangi iki nokta i�in do�ru ve kurall� bir elipse d�nderir
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
