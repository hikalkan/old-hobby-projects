using System;
using System.Drawing;

namespace iboCAD
{
	// bir doðruyu temsil eden sýnýf
	public class Dogru : Sekil
	{
		// doðrunun baþlangýç ve bitiþ noktalarý
		public Nokta bas;
		public Nokta son;
		// boþ kurucu fonksyon
		public Dogru()
		{
			bas = new Nokta();
			son = new Nokta();
		}
		// baþlangýç ve bitiþ noktalarý verilen kurucu fonksyon
		public Dogru(Nokta bas, Nokta son)
		{
			this.bas = bas;
			this.son = son;
		}
		// doðruyu çizmek için
		public override void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//baþlangýç ve bitiþ yerlerinin ekrandaki konumu bulunuyor
			Nokta ekrBas = cizimFormu.ekrandakiNokta(bas);
			Nokta ekrSon = cizimFormu.ekrandakiNokta(son);
			//çizim yapýlýyor
			if(secili)
				grafik.DrawLine(new Pen(cizgiRengi, 3.0f), ekrBas.x, ekrBas.y, ekrSon.x, ekrSon.y);
			else
				grafik.DrawLine(new Pen(cizgiRengi, Kalinlik), ekrBas.x, ekrBas.y, ekrSon.x, ekrSon.y);
		}
		// doðruyu taþýmak için
		public override void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			bas.x += yatayMiktar;
			bas.y += dikeyMiktar;
			son.x += yatayMiktar;
			son.y += dikeyMiktar;
		}
		// doðrunun dikdörtgensel olarak koordinatlarý
		public override Dikdortgen DikdortgenselKoordinat()
		{
			return new Dikdortgen(bas.Kopyasi(), son.Kopyasi());
		}
		// doðrunun alaný: 0.0
		public override float Alani()
		{
			return 0.0f;
		}
		//verilen noktanýn þeklin çizgilerine olan uzaklýðýný verir
		public override float Uzaklik(Nokta nokta)
		{
			//mantýk: Verilen noktadan geçen ve bu doðruya dik olan doðru ile bu doðrunun
			//kesiþim noktasýný bul ve bu noktayla verilen nokta arasýndaki uzaklýðý dönder
			float m = Egimi(); //bu doðrunun eðimi
			Nokta kesisim = new Nokta(); //yukarýda anlatýlan kesiþim noktasý			
			kesisim.x = (float)(nokta.x + m*m*bas.x - m*(bas.y-nokta.y)) / (m*m+1.0f); //kesiþimin x noktasý
			kesisim.y = (float)(m*(kesisim.x-bas.x)+bas.y); //kesiþimin y noktasý
			return nokta.Uzaklik(kesisim);
		}
		//verilen noktanýn þeklin çizgilerine belirli bir uzaklýk içinde olup olmadýðýný dönderir
		public override bool Civarinda(Nokta nokta, float uzaklik)
		{
			return (Uzaklik(nokta) <= uzaklik);
		}
		//bu doðrunun eðimini verir
		public float Egimi()
		{
			return (float)((son.y-bas.y)/(son.x-bas.x));
		}
		//verilen noktanýn þeklin içinde olup olmadýðýný dönderir
		public override bool Icinde(Nokta nokta)
		{
			return false;
		}
	}
}
