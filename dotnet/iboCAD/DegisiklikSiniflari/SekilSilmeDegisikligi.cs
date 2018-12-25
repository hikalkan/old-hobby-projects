using System;

namespace iboCAD
{
	public class SekilSilmeDegisikligi : IDegisiklik
	{
		//private deðiþkenler
		private Katman		katman; //þeklin eskiden ait olduðu katman
		private Sekil		silinenSekil;
		private int			sIndex; //silinen þeklin sýrasý

		//kurucu fonksyon
		public SekilSilmeDegisikligi(Katman katman, Sekil silinenSekil)
		{
			this.katman			= katman;
			this.silinenSekil	= silinenSekil;
			//bu þeklin kendi katmanýndaki index'ini sakla
			sIndex = katman.sekiller.IndexOf(silinenSekil);
		}

		#region IDegisiklik Members

		public void Uygula(frmCizim cizimFormu)
		{
			//eðer seçili olan þekil bu ise seçili olma durumunu kaldýr
			if(silinenSekil.Equals(katman.SeciliSekil))
				katman.SeciliSekil = null;
			//bu þekli ait olduðu katmandan çýkar
			katman.sekiller.RemoveAt(sIndex);
			//ana formdaki listeleri güncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//çizim alanýnýn görüntüsünü güncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		public void IptalEt(frmCizim cizimFormu)
		{
			//silinmiþ þekli eskiden ait olduðu katmana ekle
			katman.sekiller.Insert(sIndex, silinenSekil);
			//ana formdaki listeleri güncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//çizim alanýnýn görüntüsünü güncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		#endregion
	}
}
