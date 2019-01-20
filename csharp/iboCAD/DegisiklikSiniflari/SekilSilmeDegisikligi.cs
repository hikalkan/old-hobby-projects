using System;

namespace iboCAD
{
	public class SekilSilmeDegisikligi : IDegisiklik
	{
		//private de�i�kenler
		private Katman		katman; //�eklin eskiden ait oldu�u katman
		private Sekil		silinenSekil;
		private int			sIndex; //silinen �eklin s�ras�

		//kurucu fonksyon
		public SekilSilmeDegisikligi(Katman katman, Sekil silinenSekil)
		{
			this.katman			= katman;
			this.silinenSekil	= silinenSekil;
			//bu �eklin kendi katman�ndaki index'ini sakla
			sIndex = katman.sekiller.IndexOf(silinenSekil);
		}

		#region IDegisiklik Members

		public void Uygula(frmCizim cizimFormu)
		{
			//e�er se�ili olan �ekil bu ise se�ili olma durumunu kald�r
			if(silinenSekil.Equals(katman.SeciliSekil))
				katman.SeciliSekil = null;
			//bu �ekli ait oldu�u katmandan ��kar
			katman.sekiller.RemoveAt(sIndex);
			//ana formdaki listeleri g�ncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//�izim alan�n�n g�r�nt�s�n� g�ncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		public void IptalEt(frmCizim cizimFormu)
		{
			//silinmi� �ekli eskiden ait oldu�u katmana ekle
			katman.sekiller.Insert(sIndex, silinenSekil);
			//ana formdaki listeleri g�ncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//�izim alan�n�n g�r�nt�s�n� g�ncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		#endregion
	}
}
