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
		public const short santim2pixel = 50; //bir santimin %100 ölçekteki pixel karþýlýðý
		public const short cetvelBoslugu = 20; // Sol ve Alttaki cetvel boþluklarý
		#endregion

		#region Genel Deðiþkenler
		private String	isim = "Yeni Çizim"; // Çizimin Adý
		private int		olcek = 100; // Görüntünün ölçeði
		private Katman	seciliKatman = null;
		#endregion

		#region Çizimle Alakalý Genel Deðiþkenler
		public  ArrayList	katmanlar = new ArrayList(); //katmanlarýn listesi
		private Bitmap		goruntu = null; // Ekrandaki görüntü bunun üstüne çizilir
		//deðiþik çizim kalemleri
		private Pen cetvelKalemi = new Pen(Color.LightGray, 1.0f); // cetvel çizgileri
		private Pen cetvelBirimKalemi = new Pen(Color.LightGray, 1.0f); // santim çizgileri
		//çizim alanýnýn ekranda gözüken sol alt köþesi
		public short solAltX = 0;
		public short solAltY = 0;
		//taþýma ve çizim olaylarýnda kullanýlan deðiþkenler
		private TasimaOlayi tasima = null;
		public  CizimOlayi  cizim;
		//çizim alanýnýn arkaplan rengi
		private Color arkaPlanRengi;
		//çizim alanýnda yapýlan deðiþiklikleri saklayan yýðýn
        private Stack gecmisDegisiklikler		= new Stack(); // geçmiþte kalanlar
		private Stack geriAlinmisDegisiklikler	= new Stack(); // 'ileri' almak için
		#endregion

		#region Otomatik Oluþan Kodlar
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
			//otomatik oluþan kodlar (form bileþenlerini oluþturur)
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

		#region Kontrollerin Olaylarý
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
			//ana formun etkin çizim formunu kendim yap
			anaForm.SeciliCizimFormu = this;
		}
		private void frmCizim_Closed(object sender, EventArgs e)
		{
			anaForm.CizimFormuCikar(this);
		}
		//yeni bir þekil çizimi tamamlandýktan hemen sonra çalýþýr ve genel olarak ekraný günceller
		private void cizim_SekilCizimiTamamlandi(Sekil yeniSekil)
		{
			yeniSekil.cizgiRengi = anaForm.CizimRengiSecimi.Color; // Þeklin rengini ayarla
			seciliKatman.sekiller.Add(yeniSekil); // Þekli seçili katmana ekle
			seciliKatman.SeciliSekil = yeniSekil; // yeni þekil seçili þekil olsun
			anaForm.ListeleriGuncelle(); //Katman ve þekil listelerini güncelle
			GoruntuyuGuncelle(); // Çizim formunu güncelle
			//Geri alma iþlemi için bu çizim iþlemini yýðýna at
            YeniDegisiklikEkle(new CizimDegisikligi(seciliKatman, yeniSekil));
		}
		#endregion

		#region Private Fonksyonlar
		private void onAyarlamalar()
		{
			//bir adet çizim olayý nesnesi oluþtur ve bu formdaki ilgili fonksyona olayý baðla
			cizim = new CizimOlayi(this, CizimTipi.Dogru);
			cizim.SekilCizimiTamamlandi += new iboCAD.CizimOlayi.sekilCizimiTamamlandi(cizim_SekilCizimiTamamlandi);
		}
		private void pencereBasligiYaz()
		{
			this.Text = isim + " (%" + olcek.ToString() + ")";
		}
		private void cetveliCiz(Graphics grafik)
		{
			//önce cetvelin altýna gelen kýsým siliniyor
			grafik.FillRectangle(new SolidBrush(arkaPlanRengi), 0, alanYuksekligi-cetvelBoslugu, alanGenisligi, alanYuksekligi); //alt
			grafik.FillRectangle(new SolidBrush(arkaPlanRengi), 0, 0, cetvelBoslugu, alanYuksekligi-cetvelBoslugu); //sol
			//soldaki cetvel çizgisi çiziliyor
			grafik.DrawLine(cetvelKalemi,cetvelBoslugu,0,cetvelBoslugu,alanYuksekligi);
			//alttaki cetvel çizgisi çiziliyor
			grafik.DrawLine(cetvelKalemi,0,alanYuksekligi-cetvelBoslugu,alanGenisligi,alanYuksekligi-cetvelBoslugu);
			//ayýrma çizgileri çiziliyor
			//Bir ayýrma çizgisi boþluðu ayný zamanda bir santim uzunluðun ekranda
			//kaç pixel olduðunu da gösterir
			int ayirmaCizgisiAraligi = (int)santim2pixel * olcek / 100;
			//sol çizgiler
			for(int i = alanYuksekligi-cetvelBoslugu; i>0; i-=ayirmaCizgisiAraligi)
				grafik.DrawLine(cetvelBirimKalemi, 5, i, cetvelBoslugu, i);
			int y1 = alanYuksekligi-5;
			int y2 = alanYuksekligi-cetvelBoslugu;
			for(int i = cetvelBoslugu; i<alanGenisligi; i+=ayirmaCizgisiAraligi)
				grafik.DrawLine(cetvelBirimKalemi, i, y1, i, y2); //alt çizgiler
		}
		//çizim alanýnýn ilk halini hazýrlar. Boþ bir katman ekler
		private void ilkCizimAlaniniHazirla()
		{
			Katman k = new Katman();
			k.isim = "ilk katman";
			SeciliKatman = k;
			katmanlar.Add(k);
		}
		//verilen bir noktanýn seçili katmandaki herhangi bir þekil civarýnda olup olmadýðýna
		//bakar. Eðer öyleyse þekle bir referans dönderir. Deðilse null dönderir
		private Sekil sekilSecimKontrolu(Nokta nokta)
		{
			float yakinlik = gercekBoy(5.0f); // 5.0 deðeri düþerse seçmek zorlaþýr (pixel cinsinden)
			if(seciliKatman!=null)
				foreach(Sekil sekil in seciliKatman.sekiller)
					if(sekil.Civarinda(nokta, yakinlik)) return sekil;
			return null;
		}
		#endregion

		#region Public Fonsyonlar
		//tüm çizimleri günceller
		public void GoruntuyuGuncelle()
		{
			//pencere baþlýðýný yenile
			pencereBasligiYaz();
			//önceki görüntüye bir referans atanýyor (en son yok etmek için)
			Bitmap eskiGoruntu = goruntu;
			//yeni bir Bitmap oluþturuluyor
			goruntu = new Bitmap(alanGenisligi,alanYuksekligi);
			//çizim yapabilmek için grafik nesnesi oluþturuluyor
			Graphics grafik = Graphics.FromImage(goruntu);
			//çizim alaný temizleniyor
			grafik.Clear(arkaPlanRengi);
			//þekiller çiziliyor
			foreach(Katman katman in katmanlar)
				foreach(Sekil sekil in katman.sekiller)
					sekil.Ciz(this, grafik, false);
			//seçili þekli kalýn çiz
			if(seciliKatman!=null)
				if(seciliKatman.SeciliSekil!=null)
					seciliKatman.SeciliSekil.Ciz(this, grafik, true);
			//cetvel çiziliyor
			cetveliCiz(grafik);			
			//çizilen görüntü picturebox'da gösteriliyor
			pbCizimAlani.Image = goruntu;
			//eski görüntü nesnesi yok ediliyor
			if(eskiGoruntu!=null) eskiGoruntu.Dispose();
		}
		//taþýma olayýnda istenmeyenSekil hariç diðerlerinin görüntüsünü verir
		public Bitmap CizimAlaniGoruntusuVer(Sekil istenmeyenSekil, bool cetvelCizilsin)
		{
			//yeni bir Bitmap oluþturuluyor
			goruntu = new Bitmap(alanGenisligi,alanYuksekligi);
			//çizim yapabilmek için grafik nesnesi oluþturuluyor
			Graphics grafik = Graphics.FromImage(goruntu);
			//çizim alaný temizleniyor
			grafik.Clear(arkaPlanRengi);
			//þekiller çiziliyor
			foreach(Katman katman in katmanlar)
				foreach(Sekil sekil in katman.sekiller)
					if(!sekil.Equals(istenmeyenSekil))
						sekil.Ciz(this, grafik, false);
			//isteniyorsa cetvel çiziliyor
			if(cetvelCizilsin) cetveliCiz(grafik);
			//sonuçta oluþan görüntü dönderiliyor
			return goruntu;
		}
		//verilen bir noktanýn çizim alanýnda pixel olarak nereye düþtüðünü verir
		public Nokta ekrandakiNokta(Nokta gercekNokta)
		{
			Nokta nokta = new Nokta();
			nokta.x = cetvelBoslugu + ((gercekNokta.x - solAltX) * santim2pixel) * ((float)olcek / 100.0f);
			nokta.y = (alanYuksekligi - cetvelBoslugu) - ((gercekNokta.y - solAltY) * santim2pixel) * ((float)olcek / 100.0f);
			return nokta;
		}
		//verilen bir uzunluðun ekrandaki boyunu verir
		public float ekrandakiBoy(float gercekBoy)
		{
			return (float) gercekBoy * santim2pixel * olcek / 100.0f;
		}
		//santim olarak verilen bir dikdörtgensel alanýn ekrandaki boyutunu verir
		public Boyut ekrandakiBoyut(Boyut gercekBoyut)
		{
			return new Boyut(ekrandakiBoy(gercekBoyut.genislik), ekrandakiBoy(gercekBoyut.yukseklik));
		}
		//verilen pixel aralýðý için santim olarak karþýlýðýný verir
		public float gercekBoy(float ekrandakiBoy)
		{
			return (float) ekrandakiBoy / (santim2pixel * olcek / 100.0f);
		}
		//verilen bir pixelin santim olarak karþýlýðýný verir
		public Nokta gercekNokta(Nokta ekrandakiNokta)
		{
			Nokta nokta = new Nokta();
			nokta.x = (((float)ekrandakiNokta.x - cetvelBoslugu) / ((float)olcek / 100.0f) / (float)santim2pixel) + (float)solAltX;
			nokta.y = (float)(alanYuksekligi-cetvelBoslugu-ekrandakiNokta.y)/((float)olcek*santim2pixel/100.0f)+solAltY;
			return nokta;
		}
		//pixel cinsinde verilen bir dikdörtgensel alanýn santim olarak boyutunu verir
		public Boyut gercekBoyut(Boyut ekrandakiBoyut)
		{
			return new Boyut(gercekBoy(ekrandakiBoyut.genislik), gercekBoy(ekrandakiBoyut.yukseklik));
		}
		//son yapýlan deðiþikliði geri alýr
		public void GeriAl()
		{
			if(gecmisDegisiklikler.Count>0)
			{
				//son yapýlan deðiþikliði yýðýndan al
				IDegisiklik degisiklik = (IDegisiklik)gecmisDegisiklikler.Pop();
				//bu deðiþikliði iptal et
				degisiklik.IptalEt(this);
				//bu deðiþikliði 'ileri alma' iþlemi için diðer yýðýna ekle
				geriAlinmisDegisiklikler.Push(degisiklik);
				//eðer geri alýnacak hiç deðiþiklik kalmamýþsa menüdeki 'Geri Al' seçeneðini pasif yap
				if(gecmisDegisiklikler.Count<=0)
					anaForm.GeriAlAktif = false;
				//menüdeki 'Ýleri Al' seçeneðini aktif yap
				anaForm.IleriAlAktif = true;
			}
		}
		//son geri alýnan deðiþikliði tekrar uygular
		public void IleriAl()
		{
			if(geriAlinmisDegisiklikler.Count>0)
			{
				//son geri alýnan deðiþikliði yýðýndan al
				IDegisiklik degisiklik = (IDegisiklik)geriAlinmisDegisiklikler.Pop();
				//bu deðiþikliði tekrar uygula
				degisiklik.Uygula(this);
				//bu deðiþikliði 'geri' iþlemi için diðer yýðýna ekle
				gecmisDegisiklikler.Push(degisiklik);
				//eðer ileri alýnacak hiç deðiþiklik kalmamýþsa menüdeki 'Ýleri Al' seçeneðini pasif yap
				if(geriAlinmisDegisiklikler.Count<=0)
					anaForm.IleriAlAktif = false;
				//menüdeki 'Geri Al' seçeneðini aktif yap
				anaForm.GeriAlAktif = true;
			}
		}
		//daha sonra geri alabilmek için yapýlan deðiþiklikleri yýðýna ekler
		public void YeniDegisiklikEkle(IDegisiklik degisiklik)
		{
			//yapýlan deðiþikliði yýðýna ekle
			gecmisDegisiklikler.Push(degisiklik);
			//ileri alma iþlemi için saklanan deðiþiklikleri sil
			geriAlinmisDegisiklikler.Clear();
			//Menüdeki 'Geri Al' seçeneðini aktif yap
			anaForm.GeriAlAktif = true;
			//Menüdeki 'Ýleri Al' seçeneðini pasif yap
			anaForm.IleriAlAktif = false;
		}
		//çizimi kaydetmek için fonksyon
		public bool Kaydet(string dosyaAdi)
		{
			return true;
		}
		//Bir dosyadan çizim bilgilerini alýr
		public bool DosyaAc(string dosyaAdi)
		{
			return true;
		}
		#endregion

		#region Public Özellikler
		//çizim alanýnýn geniþlik ve yükseklik deðerlerini verir
		public int alanGenisligi { get{ return pbCizimAlani.Width; } }
		public int alanYuksekligi { get { return this.pbCizimAlani.Height; } }
		//çizimin ölçeðini deðiþtirmek için (yakýnlaþtýrma/uzaklaþtýrmak/öðrenmek)
		public int Olcek
		{
			get 
			{ 
				return olcek;
			}
			set
			{
				olcek = value; //yeni ölçek deðeri alýnýyor
				short gorunenSantimSayisi; //ekranda gözükecek santim sayýsý
				//yatay kaydýrma çubuðuna ayarla
				gorunenSantimSayisi = Convert.ToInt16((float)(alanGenisligi-cetvelBoslugu)/((float)santim2pixel*olcek/100.0f));
				if(gorunenSantimSayisi>=100)
					yatayKaydirmaCubugu.Maximum = 0;
				else 
					yatayKaydirmaCubugu.Maximum = 100-gorunenSantimSayisi;
				yatayKaydirmaCubugu.Value = 0;
				//dikey kaydýrma çubuðuna ayarla
				gorunenSantimSayisi = Convert.ToInt16((float)(alanYuksekligi-cetvelBoslugu)/((float)santim2pixel*olcek/100.0f));
				if(gorunenSantimSayisi>=100)
					dikeyKaydirmaCubugu.Maximum = 0;
				else 
					dikeyKaydirmaCubugu.Maximum = 100-gorunenSantimSayisi;
				dikeyKaydirmaCubugu.Value = dikeyKaydirmaCubugu.Maximum;
				//yeni deðerlere göre görüntüyü güncelle
				solAltX = 0;
				solAltY = 0;
				GoruntuyuGuncelle();
			}
		}
		//formda þu anda seçili olan katman
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
		//çizim alanýnýn görüntüsünü dönderir
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
		//ana forma bu form dýþýndan referans için
		public frmAna AnaForm
		{
			get
			{
				return anaForm;
			}
		}
		//çizim alanýnýn arkaplan rengi
		public Color ArkaPlanRengi
		{
			get
			{
				return arkaPlanRengi;
			}
			set
			{
				if(!arkaPlanRengi.Equals(value))
				{   //eðer arkaplan rengi þimdikinden farklýysa
					arkaPlanRengi = value;
                    GoruntuyuGuncelle();
				}
			}
		}
		#endregion

		#region Çizim/Taþýma/Seçim Kýsmý
		private void pbCizimAlani_MouseDown(object sender, MouseEventArgs e)
		{
			if(seciliKatman==null) return; //formda seçili bir katman yoksa derhal çýk
			if(anaForm.AracKutusu.SeciliArac.Tip==AracTipi.Tasima)
			{	//taþýmayla alakalý iþlemler
				if(e.Button==MouseButtons.Left)
				{	//taþýma olayý baþlat
					if(seciliKatman!=null)
						if(seciliKatman.SeciliSekil!=null)
							tasima = new TasimaOlayi(this, seciliKatman.SeciliSekil,new Nokta(e.X, e.Y));
				}
				else if(e.Button==MouseButtons.Right && tasima!=null)
				{	//taþýma olayýný iptal et
					tasima.IptalEt();
					tasima = null;
				}
			}
			else if(anaForm.AracKutusu.SeciliArac.Tip==AracTipi.Secme && e.Button==MouseButtons.Left)
			{
				//týklanan yere göre herhangi bir þekilin seçilmesi gerekiyor mu
				Sekil secilenSekil = sekilSecimKontrolu(gercekNokta(new Nokta(e.X, e.Y)));
                if(secilenSekil!=null)
				{
					//eðer zaten seçili deðilse týklanan þekili seç
					if(!secilenSekil.Equals(seciliKatman.SeciliSekil))
						anaForm.SekilSec(seciliKatman.sekiller.IndexOf(secilenSekil));
					//ayný anda taþýma olayý da baþlat
					anaForm.AracKutusu.AracDegistir(AracTipi.Tasima);
					tasima = new TasimaOlayi(this, seciliKatman.SeciliSekil,new Nokta(e.X, e.Y));
					tasima.TasimaSonrasi = AracTipi.Secme;
				}
			}
			else
			{   //þekil çizimi ile alakalý
				if(e.Button==MouseButtons.Left)
				{   //çizime bir nokta daha ekle
					if(cizim!=null)
						cizim.NoktaEkle(gercekNokta(new Nokta(e.X, e.Y)));
				}
				else if(e.Button==MouseButtons.Right && cizim!=null)
				{ //halen çizim aþamasýndaki çizimi iptal et
					cizim.IptalEt();
				}
			}
		}

		private void pbCizimAlani_MouseMove(object sender, MouseEventArgs e)
		{
			//Ekrandaki 'Mouse Koordinatlarý' bilgisini güncelleþtir
			Nokta gercek = gercekNokta((new Nokta(e.X, e.Y)));
			anaForm.MouseX = gercek.x * 10.0f; // 1 cm = 10 mm olduðundan 10 ile çarpýlýyor
			anaForm.MouseY = gercek.y * 10.0f;

			if(anaForm.AracKutusu.SeciliArac.Tip==AracTipi.Tasima)
			{//taþýma olayýný gerçekleþtir
				if(e.Button==MouseButtons.Left && tasima!=null)
					tasima.Tasi(new Nokta(e.X, e.Y));
			}
			else if(anaForm.AracKutusu.SeciliArac.Gorevi==AracGorevi.Cizim)
			{//çizim olayýný gerçekleþtir
				if(cizim!=null)
					cizim.NoktaEkleOnizleme(gercek);
			}
		}

		private void pbCizimAlani_MouseUp(object sender, MouseEventArgs e)
		{
			if(anaForm.AracKutusu.SeciliArac.Tip==AracTipi.Tasima)
			{	//bir taþýma olayý varsa bunu tamamla ve tasima deðiþkenini null yap
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