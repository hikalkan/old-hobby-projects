using System;
namespace iboCAD
{
	public class KatmanSilmeDegisikligi : IDegisiklik
	{
		//private de�i�kenler
		private Katman		silinenKatman;
		private int			kIndex; //silinen katman�n s�ras�

		//kurucu fonksyon
		public KatmanSilmeDegisikligi(frmCizim cizimFormu, Katman silinenKatman)
		{
			this.silinenKatman	= silinenKatman;
			//bu katman�n �izim formundaki index'ini sakla
			kIndex = cizimFormu.katmanlar.IndexOf(silinenKatman);
		}

		#region IDegisiklik Members

		public void Uygula(frmCizim cizimFormu)
		{
			//e�er se�ili olan katman bu ise se�ili olma durumunu kald�r
			if(silinenKatman.Equals(cizimFormu.SeciliKatman))
				cizimFormu.SeciliKatman = null;
			//bu katman� �izim formundan ��kar
			cizimFormu.katmanlar.RemoveAt(kIndex);
			//ana formdaki listeleri g�ncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//�izim alan�n�n g�r�nt�s�n� g�ncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		public void IptalEt(frmCizim cizimFormu)
		{
			//silinmi� olan katman� katmanlara eski s�ras� ile yeniden ekle
			cizimFormu.katmanlar.Insert(kIndex, silinenKatman);
			//ana formdaki listeleri g�ncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//�izim alan�n�n g�r�nt�s�n� g�ncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		#endregion
	}
}
