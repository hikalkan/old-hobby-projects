using System;
namespace iboCAD
{
	public class KatmanSilmeDegisikligi : IDegisiklik
	{
		//private deðiþkenler
		private Katman		silinenKatman;
		private int			kIndex; //silinen katmanýn sýrasý

		//kurucu fonksyon
		public KatmanSilmeDegisikligi(frmCizim cizimFormu, Katman silinenKatman)
		{
			this.silinenKatman	= silinenKatman;
			//bu katmanýn çizim formundaki index'ini sakla
			kIndex = cizimFormu.katmanlar.IndexOf(silinenKatman);
		}

		#region IDegisiklik Members

		public void Uygula(frmCizim cizimFormu)
		{
			//eðer seçili olan katman bu ise seçili olma durumunu kaldýr
			if(silinenKatman.Equals(cizimFormu.SeciliKatman))
				cizimFormu.SeciliKatman = null;
			//bu katmaný çizim formundan çýkar
			cizimFormu.katmanlar.RemoveAt(kIndex);
			//ana formdaki listeleri güncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//çizim alanýnýn görüntüsünü güncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		public void IptalEt(frmCizim cizimFormu)
		{
			//silinmiþ olan katmaný katmanlara eski sýrasý ile yeniden ekle
			cizimFormu.katmanlar.Insert(kIndex, silinenKatman);
			//ana formdaki listeleri güncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//çizim alanýnýn görüntüsünü güncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		#endregion
	}
}
