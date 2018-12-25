using System;
using System.Drawing;
namespace iboCAD
{
	//Bir ta��ma olay�n� temsil eden s�n�f
	public class TasimaOlayi
	{
		//private de�i�kenler
		private Nokta		baslangicNoktasi; // �ekil ta��nmadan �nceki konumu
		private Nokta		simdikiNokta; // ta��n�rken o andaki konumu
		private Sekil		tasinanSekil; // ta��nan �ekile referans
		private frmCizim	cizimFormu; // �eklin �zerinde bulundu�u �izim formu
		private Bitmap		ilkGoruntu = null; // �izim formunun ta��nmadan �nceki g�r�nt�s� (ta��nan �ekil hari�)
		public  AracTipi    TasimaSonrasi = AracTipi.Tasima; // ta��ma bitince hangi ara� se�ilsin
		//Kurucu fonksyon
		public TasimaOlayi(frmCizim cizimFormu, Sekil tasinanSekil, Nokta ilkNokta)
		{
			this.cizimFormu = cizimFormu;
			this.tasinanSekil = tasinanSekil;
			//�izim formunun ilk andaki g�r�nt�s�n� (ta��nan �ekil hari�) sakla
			ilkGoruntu = cizimFormu.CizimAlaniGoruntusuVer(tasinanSekil, true);
			//�eklin ba�lang��taki noktas�n� sakla
			baslangicNoktasi = tasinanSekil.DikdortgenselKoordinat().solUstKose;
			//mouse'un koordinatlar�n� sakla
			simdikiNokta = ilkNokta;
		}
		//�ekli hareket ettirmek ve ayn� anda g�r�nt�y� g�ncellemek i�in y�ntem
		public void Tasi(Nokta yeniNokta)
		{
			tasinanSekil.Tasi(cizimFormu.gercekBoy(yeniNokta.x-simdikiNokta.x),
							  cizimFormu.gercekBoy(simdikiNokta.y-yeniNokta.y));
			simdikiNokta = yeniNokta;
			goruntuGuncelle();
		}
		//Ta��ma olay�n� iptal etmek i�in
		public void IptalEt()
		{
			Nokta simdikiNokta = tasinanSekil.DikdortgenselKoordinat().solUstKose;
			tasinanSekil.Tasi(baslangicNoktasi.x - simdikiNokta.x, baslangicNoktasi.y - simdikiNokta.y);
			cizimFormu.GoruntuyuGuncelle();	
			ilkGoruntu.Dispose();
			ilkGoruntu = null;
		}
		//Ekrandaki g�r�nt�y� g�ncellemek i�in
		private void goruntuGuncelle()
		{
			//ilk g�r�nt�y� kopyala
			Bitmap goruntu = (Bitmap)ilkGoruntu.Clone();
			//�izim yapabilmek i�in grafik nesnesi olu�turuluyor
			Graphics grafik = Graphics.FromImage(goruntu);			
			//ta��nan �ekli �iz
			tasinanSekil.Ciz(cizimFormu, grafik, true);
			//olu�turulan g�r�nt�y� picturebox'da g�ster
			cizimFormu.Goruntu = goruntu;
		}
		//Ta��ma olay�n� tamamlamak i�in
		public void Tamamla()
		{
			if(TasimaSonrasi!=AracTipi.Tasima)
				cizimFormu.AnaForm.AracKutusu.AracDegistir(TasimaSonrasi);
			cizimFormu.GoruntuyuGuncelle();
			//yap�lan ta��ma i�lemini daha sonra geri alabilmek i�in �izim formundaki de�i�ikliklere ekle
			Nokta simdikiNokta = tasinanSekil.DikdortgenselKoordinat().solUstKose;
			cizimFormu.YeniDegisiklikEkle(new TasimaDegisikligi(tasinanSekil,
										  simdikiNokta.x - baslangicNoktasi.x,
										  simdikiNokta.y - baslangicNoktasi.y));
		}
	}
}
