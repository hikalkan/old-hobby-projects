using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace iboCAD
{
	public class frmCizim : System.Windows.Forms.Form
	{
		#region Genel Sabitler
		public const short santim2pixel = 50; //bir santimin %100 �l�ekteki pixel kar��l���
		public const short cetvelBoslugu = 20; // Sol ve Alttaki cetvel bo�luklar�
		#endregion

		#region Genel De�i�kenler
		private String	isim = "Yeni �izim"; // �izimin Ad�
		private int		olcek = 100; // G�r�nt�n�n �l�e�i
		private Katman	seciliKatman = null;
		#endregion

		#region �izimle Alakal� Genel De�i�kenler
		public  ArrayList	katmanlar = new ArrayList(); //katmanlar�n listesi
		private Bitmap		goruntu = null; // Ekrandaki g�r�nt� bunun �st�ne �izilir
		//de�i�ik �izim kalemleri
		private Pen cetvelKalemi = new Pen(Color.LightGray, 1.0f); // cetvel �izgileri
		private Pen cetvelBirimKalemi = new Pen(Color.LightGray, 1.0f); // santim �izgileri
		//�izim alan�n�n ekranda g�z�ken sol alt k��esi
		public short solAltX = 0;
		public short solAltY = 0;
		//ta��ma ve �izim olaylar�nda kullan�lan de�i�kenler
		private TasimaOlayi tasima = null;
		public  CizimOlayi  cizim;
		//�izim alan�n�n arkaplan rengi
		private Color arkaPlanRengi;
		//�izim alan�nda yap�lan de�i�iklikleri saklayan y���n
        private Stack gecmisDegisiklikler		= new Stack(); // ge�mi�te kalanlar
		private Stack geriAlinmisDegisiklikler	= new Stack(); // 'ileri' almak i�in
		#endregion

		#region Otomatik Olu�an Kodlar
		private System.Windows.Forms.PictureBox pbCizimAlani;
		private System.Windows.Forms.HScrollBar yatayKaydirmaCubugu;
		private System.Windows.Forms.VScrollBar dikeyKaydirmaCubugu;
		private System.ComponentModel.Container components = null;
		private frmAna anaForm;
		//kurucu fonksyon
		public frmCizim(frmAna anaForm)
		{
			this.anaForm = anaForm;
			MdiParent = anaForm; 
			//ana formdan arkaplan rengini al
			this.arkaPlanRengi = anaForm.ArkaPlanRengi;
			//otomatik olu�an kodlar (form bile�enlerini olu�turur)
			InitializeComponent();
		}
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
			this.pbCizimAlani = new System.Windows.Forms.PictureBox();
			this.yatayKaydirmaCubugu = new System.Windows.Forms.HScrollBar();
			this.dikeyKaydirmaCubugu = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// pbCizimAlani
			// 
			this.pbCizimAlani.BackColor = System.Drawing.Color.Black;
			this.pbCizimAlani.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbCizimAlani.Location = new System.Drawing.Point(0, 0);
			this.pbCizimAlani.Name = "pbCizimAlani";
			this.pbCizimAlani.Size = new System.Drawing.Size(520, 520);
			this.pbCizimAlani.TabIndex = 0;
			this.pbCizimAlani.TabStop = false;
			this.pbCizimAlani.MouseDown += new MouseEventHandler(pbCizimAlani_MouseDown);
			this.pbCizimAlani.MouseUp += new MouseEventHandler(pbCizimAlani_MouseUp);
			this.pbCizimAlani.MouseMove += new MouseEventHandler(pbCizimAlani_MouseMove);
			// 
			// yatayKaydirmaCubugu
			// 
			this.yatayKaydirmaCubugu.LargeChange = 1;
			this.yatayKaydirmaCubugu.Location = new System.Drawing.Point(0, 520);
			this.yatayKaydirmaCubugu.Maximum = 50;
			this.yatayKaydirmaCubugu.Name = "yatayKaydirmaCubugu";
			this.yatayKaydirmaCubugu.Size = new System.Drawing.Size(520, 16);
			this.yatayKaydirmaCubugu.TabIndex = 1;
			this.yatayKaydirmaCubugu.Scroll += new System.Windows.Forms.ScrollEventHandler(this.yatayKaydirmaCubugu_Scroll);
			// 
			// dikeyKaydirmaCubugu
			// 
			this.dikeyKaydirmaCubugu.LargeChange = 1;
			this.dikeyKaydirmaCubugu.Location = new System.Drawing.Point(520, 0);
			this.dikeyKaydirmaCubugu.Maximum = 50;
			this.dikeyKaydirmaCubugu.Name = "dikeyKaydirmaCubugu";
			this.dikeyKaydirmaCubugu.Size = new System.Drawing.Size(16, 520);
			this.dikeyKaydirmaCubugu.TabIndex = 2;
			this.dikeyKaydirmaCubugu.Value = 50;
			this.dikeyKaydirmaCubugu.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dikeyKaydirmaCubugu_Scroll);
			// 
			// frmCizim
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 536);
			this.Controls.Add(this.dikeyKaydirmaCubugu);
			this.Controls.Add(this.yatayKaydirmaCubugu);
			this.Controls.Add(this.pbCizimAlani);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCizim";
			this.Text = "frmCizim";
			this.Load += new System.EventHandler(this.frmCizim_Load);
			this.Activated +=new EventHandler(frmCizim_Activated);	
			this.Closed += new EventHandler(frmCizim_Closed);
			this.ResumeLayout(false);
		}
		#endregion

		#region Kontrollerin Olaylar�
		private void frmCizim_Load(object sender, System.EventArgs e)
		{
			onAyarlamalar();
			ilkCizimAlaniniHazirla();
			GoruntuyuGuncelle();
		}
		private void yatayKaydirmaCubugu_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			this.solAltX = (short)e.NewValue;
			this.GoruntuyuGuncelle();			
		}
		private void dikeyKaydirmaCubugu_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			this.solAltY = (short)(dikeyKaydirmaCubugu.Maximum-e.NewValue);
			this.GoruntuyuGuncelle();
		}
		private void frmCizim_Activated(object sender, EventArgs e)
		{
			//ana formun etkin �izim formunu kendim yap
			anaForm.SeciliCizimFormu = this;
		}
		private void frmCizim_Closed(object sender, EventArgs e)
		{
			anaForm.CizimFormuCikar(this);
		}
		//yeni bir �ekil �izimi tamamland�ktan hemen sonra �al���r ve genel olarak ekran� g�nceller
		private void cizim_SekilCizimiTamamlandi(Sekil yeniSekil)
		{
			yeniSekil.cizgiRengi = anaForm.CizimRengiSecimi.Color; // �eklin rengini ayarla
			seciliKatman.sekiller.Add(yeniSekil); // �ekli se�ili katmana ekle
			seciliKatman.SeciliSekil = yeniSekil; // yeni �ekil se�ili �ekil olsun
			anaForm.ListeleriGuncelle(); //Katman ve �ekil listelerini g�ncelle
			GoruntuyuGuncelle(); // �izim formunu g�ncelle
			//Geri alma i�lemi i�in bu �izim i�lemini y���na at
            YeniDegisiklikEkle(new CizimDegisikligi(seciliKatman, yeniSekil));
		}
		#endregion

		#region Private Fonksyonlar
		private void onAyarlamalar()
		{
			//bir adet �izim olay� nesnesi olu�tur ve bu formdaki ilgili fonksyona olay� ba�la
			cizim = new CizimOlayi(this, CizimTipi.Dogru);
			cizim.SekilCizimiTamamlandi += new iboCAD.CizimOlayi.sekilCizimiTamamlandi(cizim_SekilCizimiTamamlandi);
		}
		private void pencereBasligiYaz()
		{
			this.Text = isim + " (%" + olcek.ToString() + ")";
		}
		private void cetveliCiz(Graphics grafik)
		{
			//�nce cetvelin alt�na gelen k�s�m siliniyor
			grafik.FillRectangle(new SolidBrush(arkaPlanRengi), 0, alanYuksekligi-cetvelBoslugu, alanGenisligi, alanYuksekligi); //alt
			grafik.FillRectangle(new SolidBrush(arkaPlanRengi), 0, 0, cetvelBoslugu, alanYuksekligi-cetvelBoslugu); //sol
			//soldaki cetvel �izgisi �iziliyor
			grafik.DrawLine(cetvelKalemi,cetvelBoslugu,0,cetvelBoslugu,alanYuksekligi);
			//alttaki cetvel �izgisi �iziliyor
			grafik.DrawLine(cetvelKalemi,0,alanYuksekligi-cetvelBoslugu,alanGenisligi,alanYuksekligi-cetvelBoslugu);
			//ay�rma �izgileri �iziliyor
			//Bir ay�rma �izgisi bo�lu�u ayn� zamanda bir santim uzunlu�un ekranda
			//ka� pixel oldu�unu da g�sterir
			int ayirmaCizgisiAraligi = (int)santim2pixel * olcek / 100;
			//sol �izgiler
			for(int i = alanYuksekligi-cetvelBoslugu; i>0; i-=ayirmaCizgisiAraligi)
				grafik.DrawLine(cetvelBirimKalemi, 5, i, cetvelBoslugu, i);
			int y1 = alanYuksekligi-5;
			int y2 = alanYuksekligi-cetvelBoslugu;
			for(int i = cetvelBoslugu; i<alanGenisligi; i+=ayirmaCizgisiAraligi)
				grafik.DrawLine(cetvelBirimKalemi, i, y1, i, y2); //alt �izgiler
		}
		//�izim alan�n�n ilk halini haz�rlar. Bo� bir katman ekler
		private void ilkCizimAlaniniHazirla()
		{
			Katman k = new Katman();
			k.isim = "ilk katman";
			SeciliKatman = k;
			katmanlar.Add(k);
		}
		//verilen bir noktan�n se�ili katmandaki herhangi bir �ekil civar�nda olup olmad���na
		//bakar. E�er �yleyse �ekle bir referans d�nderir. De�ilse null d�nderir
		private Sekil sekilSecimKontrolu(Nokta nokta)
		{
			float yakinlik = gercekBoy(5.0f); // 5.0 de�eri d��erse se�mek zorla��r (pixel cinsinden)
			if(seciliKatman!=null)
				foreach(Sekil sekil in seciliKatman.sekiller)
					if(sekil.Civarinda(nokta, yakinlik)) return sekil;
			return null;
		}
		#endregion

		#region Public Fonsyonlar
		//t�m �izimleri g�nceller
		public void GoruntuyuGuncelle()
		{
			//pencere ba�l���n� yenile
			pencereBasligiYaz();
			//�nceki g�r�nt�ye bir referans atan�yor (en son yok etmek i�in)
			Bitmap eskiGoruntu = goruntu;
			//yeni bir Bitmap olu�turuluyor
			goruntu = new Bitmap(alanGenisligi,alanYuksekligi);
			//�izim yapabilmek i�in grafik nesnesi olu�turuluyor
			Graphics grafik = Graphics.FromImage(goruntu);
			//�izim alan� temizleniyor
			grafik.Clear(arkaPlanRengi);
			//�ekiller �iziliyor
			foreach(Katman katman in katmanlar)
				foreach(Sekil sekil in katman.sekiller)
					sekil.Ciz(this, grafik, false);
			//se�ili �ekli kal�n �iz
			if(seciliKatman!=null)
				if(seciliKatman.SeciliSekil!=null)
					seciliKatman.SeciliSekil.Ciz(this, grafik, true);
			//cetvel �iziliyor
			cetveliCiz(grafik);			
			//�izilen g�r�nt� picturebox'da g�steriliyor
			pbCizimAlani.Image = goruntu;
			//eski g�r�nt� nesnesi yok ediliyor
			if(eskiGoruntu!=null) eskiGoruntu.Dispose();
		}
		//ta��ma olay�nda istenmeyenSekil hari� di�erlerinin g�r�nt�s�n� verir
		public Bitmap CizimAlaniGoruntusuVer(Sekil istenmeyenSekil, bool cetvelCizilsin)
		{
			//yeni bir Bitmap olu�turuluyor
			goruntu = new Bitmap(alanGenisligi,alanYuksekligi);
			//�izim yapabilmek i�in grafik nesnesi olu�turuluyor
			Graphics grafik = Graphics.FromImage(goruntu);
			//�izim alan� temizleniyor
			grafik.Clear(arkaPlanRengi);
			//�ekiller �iziliyor
			foreach(Katman katman in katmanlar)
				foreach(Sekil sekil in katman.sekiller)
					if(!sekil.Equals(istenmeyenSekil))
						sekil.Ciz(this, grafik, false);
			//isteniyorsa cetvel �iziliyor
			if(cetvelCizilsin) cetveliCiz(grafik);
			//sonu�ta olu�an g�r�nt� d�nderiliyor
			return goruntu;
		}
		//verilen bir noktan�n �izim alan�nda pixel olarak nereye d��t���n� verir
		public Nokta ekrandakiNokta(Nokta gercekNokta)
		{
			Nokta nokta = new Nokta();
			nokta.x = cetvelBoslugu + ((gercekNokta.x - solAltX) * santim2pixel) * ((float)olcek / 100.0f);
			nokta.y = (alanYuksekligi - cetvelBoslugu) - ((gercekNokta.y - solAltY) * santim2pixel) * ((float)olcek / 100.0f);
			return nokta;
		}
		//verilen bir uzunlu�un ekrandaki boyunu verir
		public float ekrandakiBoy(float gercekBoy)
		{
			return (float) gercekBoy * santim2pixel * olcek / 100.0f;
		}
		//santim olarak verilen bir dikd�rtgensel alan�n ekrandaki boyutunu verir
		public Boyut ekrandakiBoyut(Boyut gercekBoyut)
		{
			return new Boyut(ekrandakiBoy(gercekBoyut.genislik), ekrandakiBoy(gercekBoyut.yukseklik));
		}
		//verilen pixel aral��� i�in santim olarak kar��l���n� verir
		public float gercekBoy(float ekrandakiBoy)
		{
			return (float) ekrandakiBoy / (santim2pixel * olcek / 100.0f);
		}
		//verilen bir pixelin santim olarak kar��l���n� verir
		public Nokta gercekNokta(Nokta ekrandakiNokta)
		{
			Nokta nokta = new Nokta();
			nokta.x = (((float)ekrandakiNokta.x - cetvelBoslugu) / ((float)olcek / 100.0f) / (float)santim2pixel) + (float)solAltX;
			nokta.y = (float)(alanYuksekligi-cetvelBoslugu-ekrandakiNokta.y)/((float)olcek*santim2pixel/100.0f)+solAltY;
			return nokta;
		}
		//pixel cinsinde verilen bir dikd�rtgensel alan�n santim olarak boyutunu verir
		public Boyut gercekBoyut(Boyut ekrandakiBoyut)
		{
			return new Boyut(gercekBoy(ekrandakiBoyut.genislik), gercekBoy(ekrandakiBoyut.yukseklik));
		}
		//son yap�lan de�i�ikli�i geri al�r
		public void GeriAl()
		{
			if(gecmisDegisiklikler.Count>0)
			{
				//son yap�lan de�i�ikli�i y���ndan al
				IDegisiklik degisiklik = (IDegisiklik)gecmisDegisiklikler.Pop();
				//bu de�i�ikli�i iptal et
				degisiklik.IptalEt(this);
				//bu de�i�ikli�i 'ileri alma' i�lemi i�in di�er y���na ekle
				geriAlinmisDegisiklikler.Push(degisiklik);
				//e�er geri al�nacak hi� de�i�iklik kalmam��sa men�deki 'Geri Al' se�ene�ini pasif yap
				if(gecmisDegisiklikler.Count<=0)
					anaForm.GeriAlAktif = false;
				//men�deki '�leri Al' se�ene�ini aktif yap
				anaForm.IleriAlAktif = true;
			}
		}
		//son geri al�nan de�i�ikli�i tekrar uygular
		public void IleriAl()
		{
			if(geriAlinmisDegisiklikler.Count>0)
			{
				//son geri al�nan de�i�ikli�i y���ndan al
				IDegisiklik degisiklik = (IDegisiklik)geriAlinmisDegisiklikler.Pop();
				//bu de�i�ikli�i tekrar uygula
				degisiklik.Uygula(this);
				//bu de�i�ikli�i 'geri' i�lemi i�in di�er y���na ekle
				gecmisDegisiklikler.Push(degisiklik);
				//e�er ileri al�nacak hi� de�i�iklik kalmam��sa men�deki '�leri Al' se�ene�ini pasif yap
				if(geriAlinmisDegisiklikler.Count<=0)
					anaForm.IleriAlAktif = false;
				//men�deki 'Geri Al' se�ene�ini aktif yap
				anaForm.GeriAlAktif = true;
			}
		}
		//daha sonra geri alabilmek i�in yap�lan de�i�iklikleri y���na ekler
		public void YeniDegisiklikEkle(IDegisiklik degisiklik)
		{
			//yap�lan de�i�ikli�i y���na ekle
			gecmisDegisiklikler.Push(degisiklik);
			//ileri alma i�lemi i�in saklanan de�i�iklikleri sil
			geriAlinmisDegisiklikler.Clear();
			//Men�deki 'Geri Al' se�ene�ini aktif yap
			anaForm.GeriAlAktif = true;
			//Men�deki '�leri Al' se�ene�ini pasif yap
			anaForm.IleriAlAktif = false;
		}
		//�izimi kaydetmek i�in fonksyon
		public bool Kaydet(string dosyaAdi)
		{
			return true;
		}
		//Bir dosyadan �izim bilgilerini al�r
		public bool DosyaAc(string dosyaAdi)
		{
			return true;
		}
		#endregion

		#region Public �zellikler
		//�izim alan�n�n geni�lik ve y�kseklik de�erlerini verir
		public int alanGenisligi { get{ return pbCizimAlani.Width; } }
		public int alanYuksekligi { get { return this.pbCizimAlani.Height; } }
		//�izimin �l�e�ini de�i�tirmek i�in (yak�nla�t�rma/uzakla�t�rmak/��renmek)
		public int Olcek
		{
			get 
			{ 
				return olcek;
			}
			set
			{
				olcek = value; //yeni �l�ek de�eri al�n�yor
				short gorunenSantimSayisi; //ekranda g�z�kecek santim say�s�
				//yatay kayd�rma �ubu�una ayarla
				gorunenSantimSayisi = Convert.ToInt16((float)(alanGenisligi-cetvelBoslugu)/((float)santim2pixel*olcek/100.0f));
				if(gorunenSantimSayisi>=100)
					yatayKaydirmaCubugu.Maximum = 0;
				else 
					yatayKaydirmaCubugu.Maximum = 100-gorunenSantimSayisi;
				yatayKaydirmaCubugu.Value = 0;
				//dikey kayd�rma �ubu�una ayarla
				gorunenSantimSayisi = Convert.ToInt16((float)(alanYuksekligi-cetvelBoslugu)/((float)santim2pixel*olcek/100.0f));
				if(gorunenSantimSayisi>=100)
					dikeyKaydirmaCubugu.Maximum = 0;
				else 
					dikeyKaydirmaCubugu.Maximum = 100-gorunenSantimSayisi;
				dikeyKaydirmaCubugu.Value = dikeyKaydirmaCubugu.Maximum;
				//yeni de�erlere g�re g�r�nt�y� g�ncelle
				solAltX = 0;
				solAltY = 0;
				GoruntuyuGuncelle();
			}
		}
		//formda �u anda se�ili olan katman
		public Katman SeciliKatman
		{
			get
			{
				return seciliKatman;
			}
			set
			{
				seciliKatman = value;
			}
		}
		//�izim alan�n�n g�r�nt�s�n� d�nderir
		public Bitmap Goruntu
		{
			get
			{
				return goruntu;
			}
			set
			{
				goruntu = value;
				pbCizimAlani.Image = goruntu;
			}
		}
		//ana forma bu form d���ndan referans i�in
		public frmAna AnaForm
		{
			get
			{
				return anaForm;
			}
		}
		//�izim alan�n�n arkaplan rengi
		public Color ArkaPlanRengi
		{
			get
			{
				return arkaPlanRengi;
			}
			set
			{
				if(!arkaPlanRengi.Equals(value))
				{   //e�er arkaplan rengi �imdikinden farkl�ysa
					arkaPlanRengi = value;
                    GoruntuyuGuncelle();
				}
			}
		}
		#endregion

		#region �izim/Ta��ma/Se�im K�sm�
		private void pbCizimAlani_MouseDown(object sender, MouseEventArgs e)
		{
			if(seciliKatman==null) return; //formda se�ili bir katman yoksa derhal ��k
			if(anaForm.AracKutusu.SeciliArac.Tip==AracTipi.Tasima)
			{	//ta��mayla alakal� i�lemler
				if(e.Button==MouseButtons.Left)
				{	//ta��ma olay� ba�lat
					if(seciliKatman!=null)
						if(seciliKatman.SeciliSekil!=null)
							tasima = new TasimaOlayi(this, seciliKatman.SeciliSekil,new Nokta(e.X, e.Y));
				}
				else if(e.Button==MouseButtons.Right && tasima!=null)
				{	//ta��ma olay�n� iptal et
					tasima.IptalEt();
					tasima = null;
				}
			}
			else if(anaForm.AracKutusu.SeciliArac.Tip==AracTipi.Secme && e.Button==MouseButtons.Left)
			{
				//t�klanan yere g�re herhangi bir �ekilin se�ilmesi gerekiyor mu
				Sekil secilenSekil = sekilSecimKontrolu(gercekNokta(new Nokta(e.X, e.Y)));
                if(secilenSekil!=null)
				{
					//e�er zaten se�ili de�ilse t�klanan �ekili se�
					if(!secilenSekil.Equals(seciliKatman.SeciliSekil))
						anaForm.SekilSec(seciliKatman.sekiller.IndexOf(secilenSekil));
					//ayn� anda ta��ma olay� da ba�lat
					anaForm.AracKutusu.AracDegistir(AracTipi.Tasima);
					tasima = new TasimaOlayi(this, seciliKatman.SeciliSekil,new Nokta(e.X, e.Y));
					tasima.TasimaSonrasi = AracTipi.Secme;
				}
			}
			else
			{   //�ekil �izimi ile alakal�
				if(e.Button==MouseButtons.Left)
				{   //�izime bir nokta daha ekle
					if(cizim!=null)
						cizim.NoktaEkle(gercekNokta(new Nokta(e.X, e.Y)));
				}
				else if(e.Button==MouseButtons.Right && cizim!=null)
				{ //halen �izim a�amas�ndaki �izimi iptal et
					cizim.IptalEt();
				}
			}
		}

		private void pbCizimAlani_MouseMove(object sender, MouseEventArgs e)
		{
			//Ekrandaki 'Mouse Koordinatlar�' bilgisini g�ncelle�tir
			Nokta gercek = gercekNokta((new Nokta(e.X, e.Y)));
			anaForm.MouseX = gercek.x * 10.0f; // 1 cm = 10 mm oldu�undan 10 ile �arp�l�yor
			anaForm.MouseY = gercek.y * 10.0f;

			if(anaForm.AracKutusu.SeciliArac.Tip==AracTipi.Tasima)
			{//ta��ma olay�n� ger�ekle�tir
				if(e.Button==MouseButtons.Left && tasima!=null)
					tasima.Tasi(new Nokta(e.X, e.Y));
			}
			else if(anaForm.AracKutusu.SeciliArac.Gorevi==AracGorevi.Cizim)
			{//�izim olay�n� ger�ekle�tir
				if(cizim!=null)
					cizim.NoktaEkleOnizleme(gercek);
			}
		}

		private void pbCizimAlani_MouseUp(object sender, MouseEventArgs e)
		{
			if(anaForm.AracKutusu.SeciliArac.Tip==AracTipi.Tasima)
			{	//bir ta��ma olay� varsa bunu tamamla ve tasima de�i�kenini null yap
				if(tasima!=null)
				{
					tasima.Tamamla();
					tasima = null;
				}
			}
		}
		#endregion
	}
}