using System;

namespace iboCAD
{
	// iki boyutta dikdörtgensel bir alaný temsil eden sýnýf
	public class Boyut
	{
		// alanýn boyutlarý
		public float genislik;
		public float yukseklik;
		// boþ kurucu fonksyon
		public Boyut()
		{
			genislik = 0.0f;
			yukseklik = 0.0f;
		}
		// deðerleri verilen kurucu fonksyon
		public Boyut(float genislik, float yukseklik)
		{
			this.genislik = genislik;
			this.yukseklik = yukseklik;
		}
		// bu boyut bilgisinin bir kopyasýný verir
		public Boyut kopyasi()
		{
			return new Boyut(genislik, yukseklik);
		}
	}
}
