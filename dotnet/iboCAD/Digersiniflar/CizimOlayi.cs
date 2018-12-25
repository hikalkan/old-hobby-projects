using System;
using System.Drawing;
using System.Collections;
namespace iboCAD
{
	//�izim olay�ndaki �izim tipini saklamak i�in enum
	public enum CizimTipi {Dikdortgen, Cember, Elips, Dogru, DogruSerisi};
	//Bir �izim olay�n� temsil eden s�n�f
	public class CizimOlayi
	{
		private CizimTipi	tip; //�izim yap�lan �ekil
		private frmCizim	cizimFormu; //�izimin �st�ne yap�ld��� form
		//�izimin olu�tu�u noktalar� saklamak i�in kuyruk yap�s�
		private Queue		noktalar = new Queue();
		// �izim formunun ta��nmadan �nceki g�r�nt�s� (ta��nan �ekil hari�)
		private Bitmap		ilkGoruntu = null;
		//Olay tan�mlamalar�
		public delegate void sekilCizimiTamamlandi(Sekil yeniSekil);
		public event sekilCizimiTamamlandi SekilCizimiTamamlandi;
		//kurucu fonksyon
		public CizimOlayi(frmCizim cizimFormu, CizimTipi tip)
		{
			this.cizimFormu = cizimFormu;
			this.tip = tip;
		}
		#region Public Fonksyonlar
		//Noktalar kuyru�una bir nokta ekle
		public void NoktaEkle(Nokta yeniNokta) //yeniNokta: santim tipinde
		{
			if(noktalar.Count==0) //e�er hi� nokta yoksa ilk noktay� sakla
			{
				noktalar.Enqueue(yeniNokta);
				//�izim formunun ilk andaki g�r�nt�s�n� sakla
				ilkGoruntu = cizimFormu.CizimAlaniGoruntusuVer(null, true);
			}
			else //en az bir nokta varsa �eklin tipine g�re karar ver
			{
				Nokta ilkNokta = (Nokta)noktalar.Dequeue(); //birinci noktay� kuyruktan al
				Sekil yeniSekil = null;
				switch(tip)
				{
					case CizimTipi.Dogru: //se�ili katmana bir do�ru ekle
						yeniSekil = new Dogru(ilkNokta.Kopyasi(), yeniNokta.Kopyasi()); //yeni bir do�ru olu�tur
						yeniSekil.isim = "yeni do�ru";
						break;
					case CizimTipi.Dikdortgen: //se�ili katmana bir dikd�rtgen ekle
						yeniSekil = Dikdortgen.KuralliDikdortgen(ilkNokta, yeniNokta);
						yeniSekil.isim = "yeni dikd�rtgen";
						break;
					case CizimTipi.Cember: //se�ili katmana bir �ember ekle
						yeniSekil = new Cember(ilkNokta.Kopyasi(), ilkNokta.Uzaklik(yeniNokta));
						yeniSekil.isim = "yeni �ember";
						break;
					case CizimTipi.Elips: //se�ili katmana bir Elips ekle
						yeniSekil = Elips.KuralliElips(ilkNokta, yeniNokta);
						yeniSekil.isim = "yeni elips";
						break;
					case CizimTipi.DogruSerisi:
						//se�ili katmana bir do�ru ekle ve di�er do�runun
						//ba�lang�� noktas�n� bunun biti� noktas� yap
						yeniSekil = new Dogru(ilkNokta.Kopyasi(), yeniNokta.Kopyasi()); //yeni bir do�ru olu�tur
						yeniSekil.isim = "yeni do�ru";
						SekilCizimiTamamlandi(yeniSekil);
						yeniSekil = null;
						NoktaEkle(yeniNokta); // yeni nokta sonraki do�runun ilk noktas�
						break;
					default:
						break;
				}
				//�izimin tamamland���n� belirten olay tetikleniyor
				if(yeniSekil!=null)	SekilCizimiTamamlandi(yeniSekil);
			}
		}
		//aktif olarak �izilen �eklin �nizlemesini g�rmek i�in
		public void NoktaEkleOnizleme(Nokta yeniNokta) //yeniNokta: santim tipinde
		{
			if(noktalar.Count>0) //en az bir nokta varsa devam et
			{
				Nokta ilkNokta = (Nokta)noktalar.Peek(); //birinci noktay� kuyruktan bul
				Sekil sekil = null;
				switch(tip)
				{
					case CizimTipi.Dikdortgen: //dikd�rtgen �nizlemesi
						sekil = Dikdortgen.KuralliDikdortgen(ilkNokta, yeniNokta);
						break;
					case CizimTipi.Cember: //�ember �nizlemesi
						sekil = new Cember(ilkNokta.Kopyasi(), ilkNokta.Uzaklik(yeniNokta));
						break;
					case CizimTipi.Elips: //elips �nizlemesi
						sekil = Elips.KuralliElips(ilkNokta, yeniNokta);
						break;
					case CizimTipi.Dogru: //do�ru serisi ya da do�ru �nizlemesi
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
		//�izim i�lemini iptal etmek i�in
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
		//Ekrandaki g�r�nt�y� g�ncellemek i�in
		//bu fonksyonda �nce ilk saklanan g�r�nt� �izilir sonra �zerine verilen �ekil �izilir
		private void goruntuGuncelle(Sekil cizilenSekil)
		{
			//ilk g�r�nt�y� kopyala
			Bitmap goruntu = (Bitmap)ilkGoruntu.Clone();
			if(cizilenSekil!=null)
			{
				//�izim yapabilmek i�in grafik nesnesi olu�turuluyor
				Graphics grafik = Graphics.FromImage(goruntu);			
				//verilen �ekli saklanan g�r�nt�n�n �st�ne �iz
				cizilenSekil.Ciz(cizimFormu, grafik, true);
			}
			//olu�turulan g�r�nt�y� picturebox'da g�ster
			cizimFormu.Goruntu = goruntu;
		}
		#endregion

		#region Public �zellikler
		//�izim tipini de�i�tirmek ve ��renmek i�in
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
