using System;

namespace iboCAD
{
	// bir noktayý temsil eden sýnýf
	public class Nokta
	{
		public float x; // x koordinatý
		public float y; // y koordinatý
		// boþ kurucu fonksyon
		public Nokta()
		{
			x = 0.0f;
			y = 0.0f;
		}
		// koordinatlarý atayan kurucu fonksyon
		public Nokta(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		// verilen nokta ile bu nokta arasýndaki mesafeyi verir
		public float Uzaklik(Nokta ikinciNokta)
		{
			return (float)(Math.Sqrt(Math.Pow(ikinciNokta.x-this.x, 2) + Math.Pow(ikinciNokta.y-this.y, 2)));
		}
		// bu noktanýn kopyasýný verir
		public Nokta Kopyasi()
		{
			return new Nokta(x, y);
		}
	}
}
