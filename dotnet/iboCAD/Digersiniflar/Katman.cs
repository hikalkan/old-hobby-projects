using System;
using System.Collections;

namespace iboCAD
{
	// �izim alan�ndaki bir katman� temsil eden s�n�f
	public class Katman
	{
		// bu katman�n ismi
		public String		isim = null;
		// bu katmandaki �ekilleri saklayan liste
		public  ArrayList	sekiller = new ArrayList();
		private Sekil		seciliSekil = null;
		// bo� kurucu fonksyon
		public Katman()
		{
			isim = "";
		}
		//public �zellikler
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
