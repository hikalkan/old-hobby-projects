using System;

namespace iboCAD
{
	// bir noktay� temsil eden s�n�f
	public class Nokta
	{
		public float x; // x koordinat�
		public float y; // y koordinat�
		// bo� kurucu fonksyon
		public Nokta()
		{
			x = 0.0f;
			y = 0.0f;
		}
		// koordinatlar� atayan kurucu fonksyon
		public Nokta(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		// verilen nokta ile bu nokta aras�ndaki mesafeyi verir
		public float Uzaklik(Nokta ikinciNokta)
		{
			return (float)(Math.Sqrt(Math.Pow(ikinciNokta.x-this.x, 2) + Math.Pow(ikinciNokta.y-this.y, 2)));
		}
		// bu noktan�n kopyas�n� verir
		public Nokta Kopyasi()
		{
			return new Nokta(x, y);
		}
	}
}
