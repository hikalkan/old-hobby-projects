using System;

namespace iboCAD
{
	public class CizimDegisikligi : IDegisiklik
	{
		//Private nesneler
		private Katman		cizilenKatman;
		private Sekil		cizilenSekil;

		//kurucu fonksyon
		public CizimDegisikligi(Katman cizilenKatman, Sekil cizilenSekil)
		{
			this.cizilenKatman	= cizilenKatman;
			this.cizilenSekil	= cizilenSekil;
		}

		#region IDegisiklik Members

		public void Uygula(frmCizim cizimFormu)
		{
			//bu �ekli eskiden oldu�u katmana ekle
			cizilenKatman.sekiller.Add(cizilenSekil);
			//ana formdaki listeleri g�ncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//�izim alan�n�n g�r�nt�s�n� g�ncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		public void IptalEt(frmCizim cizimFormu)
		{
			//e�er se�ili olan �ekil bu ise se�ili olma durumunu kald�r
			if(cizilenSekil.Equals(cizilenKatman.SeciliSekil))
				cizilenKatman.SeciliSekil = null;
			//bu �ekli ait oldu�u katmandan ��kar
			cizilenKatman.sekiller.Remove(cizilenSekil);
			//ana formdaki listeleri g�ncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//�izim alan�n�n g�r�nt�s�n� g�ncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		#endregion
	}
}
