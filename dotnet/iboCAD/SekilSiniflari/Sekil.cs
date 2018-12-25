using System;
using System.Drawing;

namespace iboCAD
{
	// herhangi bir �ekli temsil eden ana s�n�f
	public class Sekil
	{
		// �ekilin rengi
		public Color cizgiRengi;
		// �eklin kal�nl���
		public float Kalinlik = 1.0f;
		// �eklin ismi
		public string isim = null;
		// bo� kurucu fonksyon
		public Sekil()
		{
			isim = "";
			cizgiRengi = Color.White;
		}
		// �ekli �izmek i�in sanal fonksyon
		public virtual void Ciz(frmCizim cizimFormu, Graphics grafik, bool secili)
		{
			//bo�
		}
		// bir �eklin dikd�rtgensel olarak sol �st ve sa� alt koordinat�n� verir
		public virtual Dikdortgen DikdortgenselKoordinat()
		{
			return null;
		}
		// bir �eklin istenilen kadar ta��nmas� i�in
		public virtual void Tasi(float yatayMiktar, float dikeyMiktar)
		{
			//bo�
		}
		// bir �eklin kaplad��� alan� verir
		public virtual float Alani()
		{
			return 0.0f;
		}
		//verilen noktan�n �eklin �izgilerine olan uzakl���n� verir
		public virtual float Uzaklik(Nokta nokta)
		{
			return 10000.0f;
		}
		//verilen noktan�n �eklin �izgilerine belirli bir uzakl�k i�inde olup olmad���n� d�nderir
		public virtual bool Civarinda(Nokta nokta, float uzaklik)
		{
			return false;
		}
		//verilen noktan�n �eklin i�inde olup olmad���n� d�nderir
		public virtual bool Icinde(Nokta nokta)
		{
			return false;
		}
	}
}
