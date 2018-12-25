using System;
using System.Drawing;

namespace iboCAD
{
	// bir dikd�rtgeni temsil eden s�n�f
	public class Dikdortgen : Sekil
	{
		// dikd�rtgenin sol �st ve sa� alt k��elerinin koordinatlar�
		public Nokta solUstKose;
		public Boyut boyut;
		// bo� kurucu fonksyon
		public Dikdortgen()
		{
			solUstKose = new Nokta();
			boyut = new Boyut();
		}
		// sol �st k��esi ve boyutlar� verilen kurucu fonksyon
		public Dikdortgen(Nokta solUstKose, Boyut boyut)
		{
			this.solUstKose = solUstKose;
			this.boyut = boyut;
		}
		// sol �st k��esi ve sa� alt k��esi verilen kurucu fonksyon
		public Dikdortgen(Nokta solUstKose, Nokta sagAltKose)
		{
			this.solUstKose = solUstKose;
			this.boyut = new Boyut(sagAltKose.x - solUstKose.x, sagAltKose.y - solUstKose.y);
		}
		// dikd�rtgeni �izmek i�in
		public override void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//dikd�rtgenin sol �st k��esinin ekrandaki koordinatlar� ve boyutlar� belirleniyor
			Nokta ekrSolUstKose = cizimFormu.ekrandakiNokta(solUstKose);
			Boyut ekrBoyut = cizimFormu.ekrandakiBoyut(boyut);
			//�izim yap�l�yor
			if(secili)
				grafik.DrawRectangle(new Pen(cizgiRengi, 3.0f), ekrSolUstKose.x, ekrSolUstKose.y , ekrBoyut.genislik , ekrBoyut.yukseklik);
			else
				grafik.DrawRectangle(new Pen(cizgiRengi, Kalinlik), ekrSolUstKose.x ,ekrSolUstKose.y , ekrBoyut.genislik , ekrBoyut.yukseklik);
		}
		// dikd�rtgeni ta��mak i�in
		public override void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			solUstKose.x += yatayMiktar;
			solUstKose.y += dikeyMiktar;
		}
		// dikd�rtgenin koordinatlar�
		public override Dikdortgen DikdortgenselKoordinat()
		{
			return new Dikdortgen(solUstKose.Kopyasi(), boyut.kopyasi());
		}
		// dikd�rtgenin alan�
		public override float Alani()
		{
			return (boyut.genislik * boyut.yukseklik);
		}
		//verilen noktan�n �eklin �izgilerine olan uzakl���n� verir
		public override float Uzaklik(Nokta nokta)
		{
			return 100.0f;
		}
		//verilen noktan�n �eklin �izgilerine belirli bir uzakl�k i�inde olup olmad���n� d�nderir
		public override bool Civarinda(Nokta nokta, float uzaklik)
		{
			//bu dikd�rtgenden uzaklik kadar geni� bir dikdortgen olu�tur
			Dikdortgen buyuk = new Dikdortgen(new Nokta(solUstKose.x-uzaklik, solUstKose.y+uzaklik),
											  new Boyut(boyut.genislik+2.0f*uzaklik, boyut.yukseklik+2.0f*uzaklik));
			//bu dikd�rtgenden uzalik kadar k���k bir dikd�rtgen olu�tur
			Dikdortgen kucuk = new Dikdortgen(new Nokta(solUstKose.x+uzaklik, solUstKose.y-uzaklik),
											  new Boyut(boyut.genislik-2.0f*uzaklik, boyut.yukseklik-2.0f*uzaklik));
			//b�y�k dikd�rtgenin i�inde ve k���k dikd�rtgenin d���ndysa bu dikd�rtgen civar�ndad�r
			if((buyuk.Icinde(nokta)) && (!kucuk.Icinde(nokta)))
				return true;
			return false;
		}
		//verilen noktan�n �eklin i�inde olup olmad���n� d�nderir
		public override bool Icinde(Nokta nokta)
		{
			if( nokta.x > solUstKose.x && nokta.x < (solUstKose.x+boyut.genislik) &&
				nokta.y < solUstKose.y && nokta.y > (solUstKose.y-boyut.yukseklik) )
				return true;
			return false;
		}
		//verilen herhangi iki nokta i�in do�ru ve kurall� bir dikd�rtgen d�nderir
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
