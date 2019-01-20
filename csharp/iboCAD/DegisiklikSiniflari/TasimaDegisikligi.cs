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
			//ta��ma i�lemini yinele
			tasinanSekil.Tasi(yatayMiktar, dikeyMiktar);
			//�izim formunun g�r�nt�s�n� g�ncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		public void IptalEt(frmCizim cizimFormu)
		{
			//yap�lan ta��man�n tersini yap
			tasinanSekil.Tasi(-yatayMiktar, -dikeyMiktar);
			//�izim formunun g�r�nt�s�n� g�ncelle
			cizimFormu.GoruntuyuGuncelle();
		}

		#endregion
	}
}
