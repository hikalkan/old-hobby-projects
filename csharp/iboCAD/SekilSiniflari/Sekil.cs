using System;
using System.Drawing;

namespace iboCAD
{
	// herhangi bir þekli temsil eden ana sýnýf
	public class Sekil
	{
		// þekilin rengi
		public Color cizgiRengi;
		// þeklin kalýnlýðý
		public float Kalinlik = 1.0f;
		// þeklin ismi
		public string isim = null;
		// boþ kurucu fonksyon
		public Sekil()
		{
			isim = "";
			cizgiRengi = Color.White;
		}
		// þekli çizmek için sanal fonksyon
		public virtual void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//boþ
		}
		// bir þeklin dikdörtgensel olarak sol üst ve sað alt koordinatýný verir
		public virtual Dikdortgen DikdortgenselKoordinat()
		{
			return null;
		}
		// bir þeklin istenilen kadar taþýnmasý için
		public virtual void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			//boþ
		}
		// bir þeklin kapladýðý alaný verir
		public virtual float Alani()
		{
			return 0.0f;
		}
		//verilen noktanýn þeklin çizgilerine olan uzaklýðýný verir
		public virtual float Uzaklik(Nokta nokta)
		{
			return 10000.0f;
		}
		//verilen noktanýn þeklin çizgilerine belirli bir uzaklýk içinde olup olmadýðýný dönderir
		public virtual bool Civarinda(Nokta nokta, float uzaklik)
		{
			return false;
		}
		//verilen noktanýn þeklin içinde olup olmadýðýný dönderir
		public virtual bool Icinde(Nokta nokta)
		{
			return false;
		}
	}
}
