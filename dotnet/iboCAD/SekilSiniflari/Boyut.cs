using System;

namespace iboCAD
{
	// iki boyutta dikd�rtgensel bir alan� temsil eden s�n�f
	public class Boyut
	{
		// alan�n boyutlar�
		public float genislik;
		public float yukseklik;
		// bo� kurucu fonksyon
		public Boyut()
		{
			genislik = 0.0f;
			yukseklik = 0.0f;
		}
		// de�erleri verilen kurucu fonksyon
		public Boyut(float genislik, float yukseklik)
		{
			this.genislik = genislik;
			this.yukseklik = yukseklik;
		}
		// bu boyut bilgisinin bir kopyas�n� verir
		public Boyut kopyasi()
		{
			return new Boyut(genislik, yukseklik);
		}
	}
}
