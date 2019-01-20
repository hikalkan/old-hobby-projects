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
			//bu þekli eskiden olduðu katmana ekle
			cizilenKatman.sekiller.Add(cizilenSekil);
			//ana formdaki listeleri güncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//çizim alanýnýn görüntüsünü güncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		public void IptalEt(frmCizim cizimFormu)
		{
			//eðer seçili olan þekil bu ise seçili olma durumunu kaldýr
			if(cizilenSekil.Equals(cizilenKatman.SeciliSekil))
				cizilenKatman.SeciliSekil = null;
			//bu þekli ait olduðu katmandan çýkar
			cizilenKatman.sekiller.Remove(cizilenSekil);
			//ana formdaki listeleri güncelle
			cizimFormu.AnaForm.ListeleriGuncelle();
			//çizim alanýnýn görüntüsünü güncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		#endregion
	}
}
