using System;
using System.Drawing;

namespace iboCAD
{
	// bir �emberi temsil eden s�n�f
    public class Cember : Sekil
	{
		// �emberin koordinatlar� ve boyutu
		public Nokta merkez;
		public float yariCap;
		// bo� kurucu fonksyon
		public Cember()
		{
			merkez = new Nokta();
			yariCap = 0.0f;
		}
		// konumu ve yar� �ap� verilen kurucu fonksyon
		public Cember(Nokta merkez, float yariCap)
		{
			this.merkez = merkez;
			this.yariCap = yariCap;
		}
		// do�ruyu �izmek i�in
		public override void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//ekranda g�z�kmesi gereken nokta ve yar��ap belirleniyor
			Nokta ekrMerkez = cizimFormu.ekrandakiNokta(merkez);
			float ekrYariCap = cizimFormu.ekrandakiBoy(yariCap);
			//�izim yap�l�yor
			if(secili)
                grafik.DrawEllipse(new Pen(cizgiRengi, 3.0f), ekrMerkez.x-ekrYariCap, ekrMerkez.y-ekrYariCap, ekrYariCap*2.0f, ekrYariCap*2.0f);
			else
				grafik.DrawEllipse(new Pen(cizgiRengi, Kalinlik), ekrMerkez.x-ekrYariCap, ekrMerkez.y-ekrYariCap, ekrYariCap*2.0f, ekrYariCap*2.0f);
		}
		// �emberi ta��mak i�in
		public override void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			merkez.x += yatayMiktar;
			merkez.y += dikeyMiktar;
		}
		// dikd�rtgenin koordinatlar�
		public override Dikdortgen DikdortgenselKoordinat()
		{
			return new Dikdortgen(new Nokta(merkez.x-yariCap, merkez.y-yariCap), new Boyut(yariCap*2.0f, yariCap*2.0f));
		}
		// �emberin i� alan�
		public override float Alani()
		{
			return (float)(2.0f * Math.PI * yariCap);
		}
		//verilen noktan�n �eklin �izgilerine olan uzakl���n� verir
		public override float Uzaklik(Nokta nokta)
		{
			return Math.Abs(merkez.Uzaklik(nokta)-yariCap);
		}
		//verilen noktan�n �eklin �izgilerine belirli bir uzakl�k i�inde olup olmad���n� d�nderir
		public override bool Civarinda(Nokta nokta, float uzaklik)
		{
			return (Uzaklik(nokta) <= uzaklik);
		}
		//verilen noktan�n �eklin i�inde olup olmad���n� d�nderir
		public override bool Icinde(Nokta nokta)
		{
			return (merkez.Uzaklik(nokta) < yariCap);
		}
	}
}
