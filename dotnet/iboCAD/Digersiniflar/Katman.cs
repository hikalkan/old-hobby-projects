using System;
using System.Collections;

namespace iboCAD
{
	// çizim alanýndaki bir katmaný temsil eden sýnýf
	public class Katman
	{
		// bu katmanýn ismi
		public String		isim = null;
		// bu katmandaki þekilleri saklayan liste
		public  ArrayList	sekiller = new ArrayList();
		private Sekil		seciliSekil = null;
		// boþ kurucu fonksyon
		public Katman()
		{
			isim = "";
		}
		//public özellikler
		public Sekil SeciliSekil
		{
			get
			{
				return seciliSekil;
			}
			set
			{
				seciliSekil = value;
			}
		}
	}
}
