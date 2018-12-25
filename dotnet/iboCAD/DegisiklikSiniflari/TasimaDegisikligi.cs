using System;

namespace iboCAD
{
	public class TasimaDegisikligi : IDegisiklik
	{
		//Private nesneler
		private Sekil		tasinanSekil;
		private float		yatayMiktar;
		private float		dikeyMiktar;

		//kurucu fonksyon
		public TasimaDegisikligi(Sekil tasinanSekil, float yatayMiktar, float dikeyMiktar)
		{
			this.tasinanSekil	= tasinanSekil;
			this.yatayMiktar	= yatayMiktar;
			this.dikeyMiktar	= dikeyMiktar;
		}

		#region IDegisiklik Members

		public void Uygula(frmCizim cizimFormu)
		{
			//taþýma iþlemini yinele
			tasinanSekil.Tasi(yatayMiktar, dikeyMiktar);
			//çizim formunun görüntüsünü güncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		public void IptalEt(frmCizim cizimFormu)
		{
			//yapýlan taþýmanýn tersini yap
			tasinanSekil.Tasi(-yatayMiktar, -dikeyMiktar);
			//çizim formunun görüntüsünü güncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		#endregion
	}
}
