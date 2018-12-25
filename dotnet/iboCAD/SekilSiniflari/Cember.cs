using System;
using System.Drawing;

namespace iboCAD
{
	// bir çemberi temsil eden sýnýf
    public class Cember : Sekil
	{
		// çemberin koordinatlarý ve boyutu
		public Nokta merkez;
		public float yariCap;
		// boþ kurucu fonksyon
		public Cember()
		{
			merkez = new Nokta();
			yariCap = 0.0f;
		}
		// konumu ve yarý çapý verilen kurucu fonksyon
		public Cember(Nokta merkez, float yariCap)
		{
			this.merkez = merkez;
			this.yariCap = yariCap;
		}
		// doðruyu çizmek için
		public override void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//ekranda gözükmesi gereken nokta ve yarýçap belirleniyor
			Nokta ekrMerkez = cizimFormu.ekrandakiNokta(merkez);
			float ekrYariCap = cizimFormu.ekrandakiBoy(yariCap);
			//çizim yapýlýyor
			if(secili)
                grafik.DrawEllipse(new Pen(cizgiRengi, 3.0f), ekrMerkez.x-ekrYariCap, ekrMerkez.y-ekrYariCap, ekrYariCap*2.0f, ekrYariCap*2.0f);
			else
				grafik.DrawEllipse(new Pen(cizgiRengi, Kalinlik), ekrMerkez.x-ekrYariCap, ekrMerkez.y-ekrYariCap, ekrYariCap*2.0f, ekrYariCap*2.0f);
		}
		// çemberi taþýmak için
		public override void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			merkez.x += yatayMiktar;
			merkez.y += dikeyMiktar;
		}
		// dikdörtgenin koordinatlarý
		public override Dikdortgen DikdortgenselKoordinat()
		{
			return new Dikdortgen(new Nokta(merkez.x-yariCap, merkez.y-yariCap), new Boyut(yariCap*2.0f, yariCap*2.0f));
		}
		// çemberin iç alaný
		public override float Alani()
		{
			return (float)(2.0f * Math.PI * yariCap);
		}
		//verilen noktanýn þeklin çizgilerine olan uzaklýðýný verir
		public override float Uzaklik(Nokta nokta)
		{
			return Math.Abs(merkez.Uzaklik(nokta)-yariCap);
		}
		//verilen noktanýn þeklin çizgilerine belirli bir uzaklýk içinde olup olmadýðýný dönderir
		public override bool Civarinda(Nokta nokta, float uzaklik)
		{
			return (Uzaklik(nokta) <= uzaklik);
		}
		//verilen noktanýn þeklin içinde olup olmadýðýný dönderir
		public override bool Icinde(Nokta nokta)
		{
			return (merkez.Uzaklik(nokta) < yariCap);
		}
	}
}
