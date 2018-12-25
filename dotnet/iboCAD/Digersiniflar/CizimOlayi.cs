using System;
using System.Drawing;
using System.Collections;
namespace iboCAD
{
	//Çizim olayýndaki çizim tipini saklamak için enum
	public enum CizimTipi {Dikdortgen, Cember, Elips, Dogru, DogruSerisi};
	//Bir çizim olayýný temsil eden sýnýf
	public class CizimOlayi
	{
		private CizimTipi	tip; //çizim yapýlan þekil
		private frmCizim	cizimFormu; //çizimin üstüne yapýldýðý form
		//çizimin oluþtuðu noktalarý saklamak için kuyruk yapýsý
		private Queue		noktalar = new Queue();
		// çizim formunun taþýnmadan önceki görüntüsü (taþýnan þekil hariç)
		private Bitmap		ilkGoruntu = null;
		//Olay tanýmlamalarý
		public delegate void sekilCizimiTamamlandi(Sekil yeniSekil);
		public event sekilCizimiTamamlandi SekilCizimiTamamlandi;
		//kurucu fonksyon
		public CizimOlayi(frmCizim cizimFormu, CizimTipi tip)
		{
			this.cizimFormu = cizimFormu;
			this.tip = tip;
		}
		#region Public Fonksyonlar
		//Noktalar kuyruðuna bir nokta ekle
		public void NoktaEkle(Nokta yeniNokta) //yeniNokta: santim tipinde
		{
			if(noktalar.Count==0) //eðer hiç nokta yoksa ilk noktayý sakla
			{
				noktalar.Enqueue(yeniNokta);
				//çizim formunun ilk andaki görüntüsünü sakla
				ilkGoruntu = cizimFormu.CizimAlaniGoruntusuVer(null, true);
			}
			else //en az bir nokta varsa þeklin tipine göre karar ver
			{
				Nokta ilkNokta = (Nokta)noktalar.Dequeue(); //birinci noktayý kuyruktan al
				Sekil yeniSekil = null;
				switch(tip)
				{
					case CizimTipi.Dogru: //seçili katmana bir doðru ekle
						yeniSekil = new Dogru(ilkNokta.Kopyasi(), yeniNokta.Kopyasi()); //yeni bir doðru oluþtur
						yeniSekil.isim = "yeni doðru";
						break;
					case CizimTipi.Dikdortgen: //seçili katmana bir dikdörtgen ekle
						yeniSekil = Dikdortgen.KuralliDikdortgen(ilkNokta, yeniNokta);
						yeniSekil.isim = "yeni dikdörtgen";
						break;
					case CizimTipi.Cember: //seçili katmana bir çember ekle
						yeniSekil = new Cember(ilkNokta.Kopyasi(), ilkNokta.Uzaklik(yeniNokta));
						yeniSekil.isim = "yeni çember";
						break;
					case CizimTipi.Elips: //seçili katmana bir Elips ekle
						yeniSekil = Elips.KuralliElips(ilkNokta, yeniNokta);
						yeniSekil.isim = "yeni elips";
						break;
					case CizimTipi.DogruSerisi:
						//seçili katmana bir doðru ekle ve diðer doðrunun
						//baþlangýç noktasýný bunun bitiþ noktasý yap
						yeniSekil = new Dogru(ilkNokta.Kopyasi(), yeniNokta.Kopyasi()); //yeni bir doðru oluþtur
						yeniSekil.isim = "yeni doðru";
						SekilCizimiTamamlandi(yeniSekil);
						yeniSekil = null;
						NoktaEkle(yeniNokta); // yeni nokta sonraki doðrunun ilk noktasý
						break;
					default:
						break;
				}
				//çizimin tamamlandýðýný belirten olay tetikleniyor
				if(yeniSekil!=null)	SekilCizimiTamamlandi(yeniSekil);
			}
		}
		//aktif olarak çizilen þeklin önizlemesini görmek için
		public void NoktaEkleOnizleme(Nokta yeniNokta) //yeniNokta: santim tipinde
		{
			if(noktalar.Count>0) //en az bir nokta varsa devam et
			{
				Nokta ilkNokta = (Nokta)noktalar.Peek(); //birinci noktayý kuyruktan bul
				Sekil sekil = null;
				switch(tip)
				{
					case CizimTipi.Dikdortgen: //dikdörtgen önizlemesi
						sekil = Dikdortgen.KuralliDikdortgen(ilkNokta, yeniNokta);
						break;
					case CizimTipi.Cember: //çember önizlemesi
						sekil = new Cember(ilkNokta.Kopyasi(), ilkNokta.Uzaklik(yeniNokta));
						break;
					case CizimTipi.Elips: //elips önizlemesi
						sekil = Elips.KuralliElips(ilkNokta, yeniNokta);
						break;
					case CizimTipi.Dogru: //doðru serisi ya da doðru önizlemesi
					case CizimTipi.DogruSerisi:
						sekil = new Dogru(ilkNokta, yeniNokta); 
						break;
				}
				if(sekil!=null)
				{
					sekil.cizgiRengi = cizimFormu.AnaForm.CizimRengiSecimi.Color;
					goruntuGuncelle(sekil);
				}
			}
		}
		//Çizim iþlemini iptal etmek için
		public void IptalEt()
		{
			if(noktalar.Count>0)
			{
				noktalar.Clear();
				cizimFormu.GoruntuyuGuncelle();
			}
		}
		#endregion

		#region Private Fonksyonlar
		//Ekrandaki görüntüyü güncellemek için
		//bu fonksyonda önce ilk saklanan görüntü çizilir sonra üzerine verilen þekil çizilir
		private void goruntuGuncelle(Sekil cizilenSekil)
		{
			//ilk görüntüyü kopyala
			Bitmap goruntu = (Bitmap)ilkGoruntu.Clone();
			if(cizilenSekil!=null)
			{
				//çizim yapabilmek için grafik nesnesi oluþturuluyor
				Graphics grafik = Graphics.FromImage(goruntu);			
				//verilen þekli saklanan görüntünün üstüne çiz
				cizilenSekil.Ciz(cizimFormu, grafik, true);
			}
			//oluþturulan görüntüyü picturebox'da göster
			cizimFormu.Goruntu = goruntu;
		}
		#endregion

		#region Public Özellikler
		//çizim tipini deðiþtirmek ve öðrenmek için
		public CizimTipi Tip
		{
			get
			{
				return tip;
			}
			set
			{
				tip = value;
				IptalEt();
			}
		}
		#endregion
	}
}
