using System;
using System.Drawing;

namespace iboCAD
{
	// bir do�ruyu temsil eden s�n�f
	public class Dogru : Sekil
	{
		// do�runun ba�lang�� ve biti� noktalar�
		public Nokta bas;
		public Nokta son;
		// bo� kurucu fonksyon
		public Dogru()
		{
			bas = new Nokta();
			son = new Nokta();
		}
		// ba�lang�� ve biti� noktalar� verilen kurucu fonksyon
		public Dogru(Nokta bas, Nokta son)
		{
			this.bas = bas;
			this.son = son;
		}
		// do�ruyu �izmek i�in
		public override void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//ba�lang�� ve biti� yerlerinin ekrandaki konumu bulunuyor
			Nokta ekrBas = cizimFormu.ekrandakiNokta(bas);
			Nokta ekrSon = cizimFormu.ekrandakiNokta(son);
			//�izim yap�l�yor
			if(secili)
				grafik.DrawLine(new Pen(cizgiRengi, 3.0f), ekrBas.x, ekrBas.y, ekrSon.x, ekrSon.y);
			else
				grafik.DrawLine(new Pen(cizgiRengi, Kalinlik), ekrBas.x, ekrBas.y, ekrSon.x, ekrSon.y);
		}
		// do�ruyu ta��mak i�in
		public override void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			bas.x += yatayMiktar;
			bas.y += dikeyMiktar;
			son.x += yatayMiktar;
			son.y += dikeyMiktar;
		}
		// do�runun dikd�rtgensel olarak koordinatlar�
		public override Dikdortgen DikdortgenselKoordinat()
		{
			return new Dikdortgen(bas.Kopyasi(), son.Kopyasi());
		}
		// do�runun alan�: 0.0
		public override float Alani()
		{
			return 0.0f;
		}
		//verilen noktan�n �eklin �izgilerine olan uzakl���n� verir
		public override float Uzaklik(Nokta nokta)
		{
			//mant�k: Verilen noktadan ge�en ve bu do�ruya dik olan do�ru ile bu do�runun
			//kesi�im noktas�n� bul ve bu noktayla verilen nokta aras�ndaki uzakl��� d�nder
			float m = Egimi(); //bu do�runun e�imi
			Nokta kesisim = new Nokta(); //yukar�da anlat�lan kesi�im noktas�			
			kesisim.x = (float)(nokta.x + m*m*bas.x - m*(bas.y-nokta.y)) / (m*m+1.0f); //kesi�imin x noktas�
			kesisim.y = (float)(m*(kesisim.x-bas.x)+bas.y); //kesi�imin y noktas�
			return nokta.Uzaklik(kesisim);
		}
		//verilen noktan�n �eklin �izgilerine belirli bir uzakl�k i�inde olup olmad���n� d�nderir
		public override bool Civarinda(Nokta nokta, float uzaklik)
		{
			return (Uzaklik(nokta) <= uzaklik);
		}
		//bu do�runun e�imini verir
		public float Egimi()
		{
			return (float)((son.y-bas.y)/(son.x-bas.x));
		}
		//verilen noktan�n �eklin i�inde olup olmad���n� d�nderir
		public override bool Icinde(Nokta nokta)
		{
			return false;
		}
	}
}
