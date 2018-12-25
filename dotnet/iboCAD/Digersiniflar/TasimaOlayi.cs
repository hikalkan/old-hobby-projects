using System;
using System.Drawing;
namespace iboCAD
{
	//Bir taþýma olayýný temsil eden sýnýf
	public class TasimaOlayi
	{
		//private deðiþkenler
		private Nokta		baslangicNoktasi; // þekil taþýnmadan önceki konumu
		private Nokta		simdikiNokta; // taþýnýrken o andaki konumu
		private Sekil		tasinanSekil; // taþýnan þekile referans
		private frmCizim	cizimFormu; // þeklin üzerinde bulunduðu çizim formu
		private Bitmap		ilkGoruntu = null; // çizim formunun taþýnmadan önceki görüntüsü (taþýnan þekil hariç)
		public  AracTipi    TasimaSonrasi = AracTipi.Tasima; // taþýma bitince hangi araç seçilsin
		//Kurucu fonksyon
		public TasimaOlayi(frmCizim cizimFormu, Sekil tasinanSekil, Nokta ilkNokta)
		{
			this.cizimFormu = cizimFormu;
			this.tasinanSekil = tasinanSekil;
			//çizim formunun ilk andaki görüntüsünü (taþýnan þekil hariç) sakla
			ilkGoruntu = cizimFormu.CizimAlaniGoruntusuVer(tasinanSekil, true);
			//þeklin baþlangýçtaki noktasýný sakla
			baslangicNoktasi = tasinanSekil.DikdortgenselKoordinat().solUstKose;
			//mouse'un koordinatlarýný sakla
			simdikiNokta = ilkNokta;
		}
		//Þekli hareket ettirmek ve ayný anda görüntüyü güncellemek için yöntem
		public void Tasi(Nokta yeniNokta)
		{
			tasinanSekil.Tasi(cizimFormu.gercekBoy(yeniNokta.x-simdikiNokta.x),
							  cizimFormu.gercekBoy(simdikiNokta.y-yeniNokta.y));
			simdikiNokta = yeniNokta;
			goruntuGuncelle();
		}
		//Taþýma olayýný iptal etmek için
		public void IptalEt()
		{
			Nokta simdikiNokta = tasinanSekil.DikdortgenselKoordinat().solUstKose;
			tasinanSekil.Tasi(baslangicNoktasi.x - simdikiNokta.x, baslangicNoktasi.y - simdikiNokta.y);
			cizimFormu.GoruntuyuGuncelle();	
			ilkGoruntu.Dispose();
			ilkGoruntu = null;
		}
		//Ekrandaki görüntüyü güncellemek için
		private void goruntuGuncelle()
		{
			//ilk görüntüyü kopyala
			Bitmap goruntu = (Bitmap)ilkGoruntu.Clone();
			//çizim yapabilmek için grafik nesnesi oluþturuluyor
			Graphics grafik = Graphics.FromImage(goruntu);			
			//taþýnan þekli çiz
			tasinanSekil.Ciz(cizimFormu, grafik, true);
			//oluþturulan görüntüyü picturebox'da göster
			cizimFormu.Goruntu = goruntu;
		}
		//Taþýma olayýný tamamlamak için
		public void Tamamla()
		{
			if(TasimaSonrasi!=AracTipi.Tasima)
				cizimFormu.AnaForm.AracKutusu.AracDegistir(TasimaSonrasi);
			cizimFormu.GoruntuyuGuncelle();
			//yapýlan taþýma iþlemini daha sonra geri alabilmek için çizim formundaki deðiþikliklere ekle
			Nokta simdikiNokta = tasinanSekil.DikdortgenselKoordinat().solUstKose;
			cizimFormu.YeniDegisiklikEkle(new TasimaDegisikligi(tasinanSekil,
										  simdikiNokta.x - baslangicNoktasi.x,
										  simdikiNokta.y - baslangicNoktasi.y));
		}
	}
}
