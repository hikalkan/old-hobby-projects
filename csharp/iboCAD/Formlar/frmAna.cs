using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace iboCAD
{
	public class frmAna : System.Windows.Forms.Form
	{
		#region Sabit Deðiþkenler
		public const String ProgramAdi			= "iboCAD Çizim Programý";
		public const String Versiyon			= "0.11";
		public const String SonGuncellemeTarihi = "11 Kasým 2005";
		#endregion

		#region Private Deðiþkenler
		//çizim formlarýnýn listesi
		private ArrayList				cizimFormlari = new ArrayList();
		//þu anda aktif olan çizim formuna referans
		private frmCizim				seciliCizimFormu = null;
		//seçili olan þeklin kalýnlýðý
		public static float seciliSekilKalinligi = 3.0f;
		//listelerin SelectedTextChanged olayýný tetiklememek için
		private bool listelerHazirlaniyor = true;
		private bool listelerTemizleniyor = true;
		//Mouse'un koordinatlarý (sað tarafta gösterilen)
		private float mouseX = 0.0f;
		private float mouseY = 0.0f;
		#endregion

		#region Otomatik Oluþan Kodlar
		private System.Windows.Forms.MainMenu anaMenu;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem mnDosya;
		private System.Windows.Forms.MenuItem mnYeniCizim;
		private System.Windows.Forms.MenuItem mnKaydet;
		private System.Windows.Forms.MenuItem mnAc;
		private System.Windows.Forms.MenuItem mnCizgi_Ac_Cikis;
		private System.Windows.Forms.MenuItem mnCikis;
		private System.Windows.Forms.MenuItem mnYardimAna;
		private System.Windows.Forms.MenuItem mnYardim;
		private System.Windows.Forms.Button btnOlcekArti;
		private System.Windows.Forms.ListBox lstSekiller;
		private System.Windows.Forms.ListBox lstKatmanlar;
		private System.Windows.Forms.Label lblKatmanlar;
		private System.Windows.Forms.Label lblSekiller;
		private System.Windows.Forms.Button btnOlcekEksi;
		private System.Windows.Forms.Button btnKatmanEkle;
		private System.Windows.Forms.Button btnKatmanAsagi;
		private System.Windows.Forms.Button btnKatmanYukarý;
		private System.Windows.Forms.Button btnKatmanSil;
		private System.Windows.Forms.Panel pnlAraclar;
		private System.Windows.Forms.Panel pnlSagTaraf;
		private Araclar araclar;
		private System.Windows.Forms.MenuItem mnSekilSil;
		private System.Windows.Forms.ContextMenu sekillerMenu;
		private System.Windows.Forms.ContextMenu katmanlarMenu;
		private System.Windows.Forms.MenuItem mnSekilOzellikler;
		private System.Windows.Forms.MenuItem mnKatmanOzellikler;
		private System.Windows.Forms.MenuItem mnKatmanSil;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label txtMouseY;
		private System.Windows.Forms.Label txtMouseX;
		private System.Windows.Forms.Label lmlMouseKoor;
		private System.Windows.Forms.Panel pnlMouseKonum;
		private System.Windows.Forms.MenuItem mnKatmanYeni;
		public System.Windows.Forms.ColorDialog CizimRengiSecimi;
		private System.Windows.Forms.Panel pnlCizimRengi;
		private System.Windows.Forms.ToolTip ipucuKutusu;
		private System.Windows.Forms.Panel pnlArkaPlanRengi;
		public System.Windows.Forms.ColorDialog ArkaPlanRengiSecimi;
		private System.Windows.Forms.StatusBar durumCubugu;
		private System.Windows.Forms.MenuItem mnDuzen;
		private System.Windows.Forms.MenuItem mnGeriAl;
		private System.Windows.Forms.MenuItem mnIleriAl;
		private System.Windows.Forms.MenuItem mnFarkliKaydet;
		private System.Windows.Forms.OpenFileDialog dosyaAcmaPenceresi;
		private System.Windows.Forms.MenuItem mnHakkinda;
		public frmAna()
		{
			InitializeComponent();
		}
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAna));
			this.anaMenu = new System.Windows.Forms.MainMenu();
			this.mnDosya = new System.Windows.Forms.MenuItem();
			this.mnYeniCizim = new System.Windows.Forms.MenuItem();
			this.mnKaydet = new System.Windows.Forms.MenuItem();
			this.mnFarkliKaydet = new System.Windows.Forms.MenuItem();
			this.mnAc = new System.Windows.Forms.MenuItem();
			this.mnCizgi_Ac_Cikis = new System.Windows.Forms.MenuItem();
			this.mnCikis = new System.Windows.Forms.MenuItem();
			this.mnDuzen = new System.Windows.Forms.MenuItem();
			this.mnGeriAl = new System.Windows.Forms.MenuItem();
			this.mnIleriAl = new System.Windows.Forms.MenuItem();
			this.mnYardimAna = new System.Windows.Forms.MenuItem();
			this.mnYardim = new System.Windows.Forms.MenuItem();
			this.mnHakkinda = new System.Windows.Forms.MenuItem();
			this.pnlAraclar = new System.Windows.Forms.Panel();
			this.araclar = new iboCAD.Araclar();
			this.btnOlcekEksi = new System.Windows.Forms.Button();
			this.btnOlcekArti = new System.Windows.Forms.Button();
			this.pnlCizimRengi = new System.Windows.Forms.Panel();
			this.pnlArkaPlanRengi = new System.Windows.Forms.Panel();
			this.pnlSagTaraf = new System.Windows.Forms.Panel();
			this.pnlMouseKonum = new System.Windows.Forms.Panel();
			this.txtMouseY = new System.Windows.Forms.Label();
			this.txtMouseX = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lmlMouseKoor = new System.Windows.Forms.Label();
			this.lblSekiller = new System.Windows.Forms.Label();
			this.lstSekiller = new System.Windows.Forms.ListBox();
			this.sekillerMenu = new System.Windows.Forms.ContextMenu();
			this.mnSekilOzellikler = new System.Windows.Forms.MenuItem();
			this.mnSekilSil = new System.Windows.Forms.MenuItem();
			this.btnKatmanEkle = new System.Windows.Forms.Button();
			this.btnKatmanAsagi = new System.Windows.Forms.Button();
			this.btnKatmanYukarý = new System.Windows.Forms.Button();
			this.lstKatmanlar = new System.Windows.Forms.ListBox();
			this.katmanlarMenu = new System.Windows.Forms.ContextMenu();
			this.mnKatmanOzellikler = new System.Windows.Forms.MenuItem();
			this.mnKatmanSil = new System.Windows.Forms.MenuItem();
			this.mnKatmanYeni = new System.Windows.Forms.MenuItem();
			this.btnKatmanSil = new System.Windows.Forms.Button();
			this.lblKatmanlar = new System.Windows.Forms.Label();
			this.CizimRengiSecimi = new System.Windows.Forms.ColorDialog();
			this.ipucuKutusu = new System.Windows.Forms.ToolTip(this.components);
			this.durumCubugu = new System.Windows.Forms.StatusBar();
			this.ArkaPlanRengiSecimi = new System.Windows.Forms.ColorDialog();
			this.dosyaAcmaPenceresi = new System.Windows.Forms.OpenFileDialog();
			this.pnlAraclar.SuspendLayout();
			this.pnlSagTaraf.SuspendLayout();
			this.pnlMouseKonum.SuspendLayout();
			this.SuspendLayout();
			// 
			// anaMenu
			// 
			this.anaMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnDosya,
																					this.mnDuzen,
																					this.mnYardimAna});
			// 
			// mnDosya
			// 
			this.mnDosya.Index = 0;
			this.mnDosya.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnYeniCizim,
																					this.mnKaydet,
																					this.mnFarkliKaydet,
																					this.mnAc,
																					this.mnCizgi_Ac_Cikis,
																					this.mnCikis});
			this.mnDosya.Text = "&Dosya";
			// 
			// mnYeniCizim
			// 
			this.mnYeniCizim.Index = 0;
			this.mnYeniCizim.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.mnYeniCizim.Text = "&Yeni Çizim";
			this.mnYeniCizim.Click += new System.EventHandler(this.mnYeniCizim_Click);
			// 
			// mnKaydet
			// 
			this.mnKaydet.Index = 1;
			this.mnKaydet.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.mnKaydet.Text = "&Kaydet";
			// 
			// mnFarkliKaydet
			// 
			this.mnFarkliKaydet.Index = 2;
			this.mnFarkliKaydet.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
			this.mnFarkliKaydet.Text = "&Farklý Kaydet";
			// 
			// mnAc
			// 
			this.mnAc.Index = 3;
			this.mnAc.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.mnAc.Text = "&Aç..";
			this.mnAc.Click += new System.EventHandler(this.mnAc_Click);
			// 
			// mnCizgi_Ac_Cikis
			// 
			this.mnCizgi_Ac_Cikis.Index = 4;
			this.mnCizgi_Ac_Cikis.Text = "-";
			// 
			// mnCikis
			// 
			this.mnCikis.Index = 5;
			this.mnCikis.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.mnCikis.Text = "&Çýkýþ";
			this.mnCikis.Click += new System.EventHandler(this.mnCikis_Click);
			// 
			// mnDuzen
			// 
			this.mnDuzen.Index = 1;
			this.mnDuzen.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnGeriAl,
																					this.mnIleriAl});
			this.mnDuzen.Text = "Dü&zen";
			// 
			// mnGeriAl
			// 
			this.mnGeriAl.Index = 0;
			this.mnGeriAl.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
			this.mnGeriAl.Text = "&Geri Al";
			this.mnGeriAl.Click += new System.EventHandler(this.mnGeriAl_Click);
			// 
			// mnIleriAl
			// 
			this.mnIleriAl.Index = 1;
			this.mnIleriAl.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
			this.mnIleriAl.Text = "Ý&leri Al";
			this.mnIleriAl.Click += new System.EventHandler(this.mnIleriAl_Click);
			// 
			// mnYardimAna
			// 
			this.mnYardimAna.Index = 2;
			this.mnYardimAna.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mnYardim,
																						this.mnHakkinda});
			this.mnYardimAna.Text = "&Yardým";
			// 
			// mnYardim
			// 
			this.mnYardim.Index = 0;
			this.mnYardim.Shortcut = System.Windows.Forms.Shortcut.F1;
			this.mnYardim.Text = "&Yardým";
			// 
			// mnHakkinda
			// 
			this.mnHakkinda.Index = 1;
			this.mnHakkinda.Shortcut = System.Windows.Forms.Shortcut.F12;
			this.mnHakkinda.Text = "&Hakkýnda..";
			this.mnHakkinda.Click += new System.EventHandler(this.mnHakkinda_Click);
			// 
			// pnlAraclar
			// 
			this.pnlAraclar.BackColor = System.Drawing.Color.SteelBlue;
			this.pnlAraclar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlAraclar.BackgroundImage")));
			this.pnlAraclar.Controls.Add(this.araclar);
			this.pnlAraclar.Controls.Add(this.btnOlcekEksi);
			this.pnlAraclar.Controls.Add(this.btnOlcekArti);
			this.pnlAraclar.Controls.Add(this.pnlCizimRengi);
			this.pnlAraclar.Controls.Add(this.pnlArkaPlanRengi);
			this.pnlAraclar.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlAraclar.Location = new System.Drawing.Point(0, 0);
			this.pnlAraclar.Name = "pnlAraclar";
			this.pnlAraclar.Size = new System.Drawing.Size(60, 569);
			this.pnlAraclar.TabIndex = 1;
			// 
			// araclar
			// 
			this.araclar.Location = new System.Drawing.Point(8, 8);
			this.araclar.Name = "araclar";
			this.araclar.SeciliArac = null;
			this.araclar.Size = new System.Drawing.Size(41, 207);
			this.araclar.TabIndex = 7;
			this.araclar.AracDegisti += new iboCAD.Araclar.aracDegisti(this.araclar_AracDegisti);
			// 
			// btnOlcekEksi
			// 
			this.btnOlcekEksi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOlcekEksi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.btnOlcekEksi.ForeColor = System.Drawing.Color.White;
			this.btnOlcekEksi.Location = new System.Drawing.Point(8, 328);
			this.btnOlcekEksi.Name = "btnOlcekEksi";
			this.btnOlcekEksi.Size = new System.Drawing.Size(40, 16);
			this.btnOlcekEksi.TabIndex = 6;
			this.btnOlcekEksi.Text = "&-";
			this.ipucuKutusu.SetToolTip(this.btnOlcekEksi, "Uzaklaþtýr");
			this.btnOlcekEksi.Click += new System.EventHandler(this.btnOlcekEksi_Click);
			// 
			// btnOlcekArti
			// 
			this.btnOlcekArti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOlcekArti.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.btnOlcekArti.ForeColor = System.Drawing.Color.White;
			this.btnOlcekArti.Location = new System.Drawing.Point(8, 304);
			this.btnOlcekArti.Name = "btnOlcekArti";
			this.btnOlcekArti.Size = new System.Drawing.Size(40, 16);
			this.btnOlcekArti.TabIndex = 3;
			this.btnOlcekArti.Text = "&+";
			this.ipucuKutusu.SetToolTip(this.btnOlcekArti, "Yakýnlaþtýr");
			this.btnOlcekArti.Click += new System.EventHandler(this.btnOlcekArti_Click);
			// 
			// pnlCizimRengi
			// 
			this.pnlCizimRengi.BackColor = System.Drawing.Color.White;
			this.pnlCizimRengi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlCizimRengi.Location = new System.Drawing.Point(8, 224);
			this.pnlCizimRengi.Name = "pnlCizimRengi";
			this.pnlCizimRengi.Size = new System.Drawing.Size(24, 24);
			this.pnlCizimRengi.TabIndex = 5;
			this.ipucuKutusu.SetToolTip(this.pnlCizimRengi, "Çizgi rengi");
			this.pnlCizimRengi.Click += new System.EventHandler(this.pnlCizimRengi_Click);
			// 
			// pnlArkaPlanRengi
			// 
			this.pnlArkaPlanRengi.BackColor = System.Drawing.Color.Black;
			this.pnlArkaPlanRengi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlArkaPlanRengi.Location = new System.Drawing.Point(24, 240);
			this.pnlArkaPlanRengi.Name = "pnlArkaPlanRengi";
			this.pnlArkaPlanRengi.Size = new System.Drawing.Size(24, 24);
			this.pnlArkaPlanRengi.TabIndex = 6;
			this.ipucuKutusu.SetToolTip(this.pnlArkaPlanRengi, "Arkaplan rengi");
			this.pnlArkaPlanRengi.Click += new System.EventHandler(this.pnlArkaPlanRengi_Click);
			// 
			// pnlSagTaraf
			// 
			this.pnlSagTaraf.BackColor = System.Drawing.Color.SteelBlue;
			this.pnlSagTaraf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlSagTaraf.BackgroundImage")));
			this.pnlSagTaraf.Controls.Add(this.pnlMouseKonum);
			this.pnlSagTaraf.Controls.Add(this.lmlMouseKoor);
			this.pnlSagTaraf.Controls.Add(this.lblSekiller);
			this.pnlSagTaraf.Controls.Add(this.lstSekiller);
			this.pnlSagTaraf.Controls.Add(this.btnKatmanEkle);
			this.pnlSagTaraf.Controls.Add(this.btnKatmanAsagi);
			this.pnlSagTaraf.Controls.Add(this.btnKatmanYukarý);
			this.pnlSagTaraf.Controls.Add(this.lstKatmanlar);
			this.pnlSagTaraf.Controls.Add(this.btnKatmanSil);
			this.pnlSagTaraf.Controls.Add(this.lblKatmanlar);
			this.pnlSagTaraf.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlSagTaraf.Location = new System.Drawing.Point(336, 0);
			this.pnlSagTaraf.Name = "pnlSagTaraf";
			this.pnlSagTaraf.Size = new System.Drawing.Size(192, 569);
			this.pnlSagTaraf.TabIndex = 3;
			// 
			// pnlMouseKonum
			// 
			this.pnlMouseKonum.BackColor = System.Drawing.Color.SteelBlue;
			this.pnlMouseKonum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlMouseKonum.Controls.Add(this.txtMouseY);
			this.pnlMouseKonum.Controls.Add(this.txtMouseX);
			this.pnlMouseKonum.Controls.Add(this.label3);
			this.pnlMouseKonum.Controls.Add(this.label2);
			this.pnlMouseKonum.Location = new System.Drawing.Point(8, 360);
			this.pnlMouseKonum.Name = "pnlMouseKonum";
			this.pnlMouseKonum.Size = new System.Drawing.Size(176, 32);
			this.pnlMouseKonum.TabIndex = 9;
			// 
			// txtMouseY
			// 
			this.txtMouseY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.txtMouseY.ForeColor = System.Drawing.Color.White;
			this.txtMouseY.Location = new System.Drawing.Point(96, 8);
			this.txtMouseY.Name = "txtMouseY";
			this.txtMouseY.Size = new System.Drawing.Size(72, 16);
			this.txtMouseY.TabIndex = 3;
			this.txtMouseY.Text = "0.0";
			this.txtMouseY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtMouseX
			// 
			this.txtMouseX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.txtMouseX.ForeColor = System.Drawing.Color.White;
			this.txtMouseX.Location = new System.Drawing.Point(24, 8);
			this.txtMouseX.Name = "txtMouseX";
			this.txtMouseX.Size = new System.Drawing.Size(56, 16);
			this.txtMouseX.TabIndex = 2;
			this.txtMouseX.Text = "0.0";
			this.txtMouseX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(80, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(16, 16);
			this.label3.TabIndex = 1;
			this.label3.Text = "Y:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "X:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lmlMouseKoor
			// 
			this.lmlMouseKoor.BackColor = System.Drawing.Color.LightBlue;
			this.lmlMouseKoor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lmlMouseKoor.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lmlMouseKoor.Location = new System.Drawing.Point(8, 344);
			this.lmlMouseKoor.Name = "lmlMouseKoor";
			this.lmlMouseKoor.Size = new System.Drawing.Size(176, 16);
			this.lmlMouseKoor.TabIndex = 8;
			this.lmlMouseKoor.Text = "Mouse Konumu (mm)";
			this.lmlMouseKoor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSekiller
			// 
			this.lblSekiller.BackColor = System.Drawing.Color.LightBlue;
			this.lblSekiller.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblSekiller.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lblSekiller.Location = new System.Drawing.Point(8, 8);
			this.lblSekiller.Name = "lblSekiller";
			this.lblSekiller.Size = new System.Drawing.Size(176, 16);
			this.lblSekiller.TabIndex = 3;
			this.lblSekiller.Text = "Þekiller";
			this.lblSekiller.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lstSekiller
			// 
			this.lstSekiller.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstSekiller.ContextMenu = this.sekillerMenu;
			this.lstSekiller.Location = new System.Drawing.Point(8, 24);
			this.lstSekiller.Name = "lstSekiller";
			this.lstSekiller.Size = new System.Drawing.Size(176, 132);
			this.lstSekiller.TabIndex = 0;
			this.lstSekiller.SelectedIndexChanged += new System.EventHandler(this.lstSekiller_SelectedIndexChanged);
			// 
			// sekillerMenu
			// 
			this.sekillerMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnSekilOzellikler,
																						 this.mnSekilSil});
			// 
			// mnSekilOzellikler
			// 
			this.mnSekilOzellikler.Index = 0;
			this.mnSekilOzellikler.Shortcut = System.Windows.Forms.Shortcut.F10;
			this.mnSekilOzellikler.Text = "Özellikler";
			this.mnSekilOzellikler.Click += new System.EventHandler(this.mnSekilOzellikler_Click);
			// 
			// mnSekilSil
			// 
			this.mnSekilSil.Index = 1;
			this.mnSekilSil.Shortcut = System.Windows.Forms.Shortcut.Del;
			this.mnSekilSil.Text = "Sil";
			this.mnSekilSil.Click += new System.EventHandler(this.mnSekilSil_Click);
			// 
			// btnKatmanEkle
			// 
			this.btnKatmanEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnKatmanEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.btnKatmanEkle.ForeColor = System.Drawing.Color.White;
			this.btnKatmanEkle.Location = new System.Drawing.Point(8, 312);
			this.btnKatmanEkle.Name = "btnKatmanEkle";
			this.btnKatmanEkle.Size = new System.Drawing.Size(48, 24);
			this.btnKatmanEkle.TabIndex = 4;
			this.btnKatmanEkle.Text = "Ekle";
			// 
			// btnKatmanAsagi
			// 
			this.btnKatmanAsagi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnKatmanAsagi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.btnKatmanAsagi.ForeColor = System.Drawing.Color.White;
			this.btnKatmanAsagi.Location = new System.Drawing.Point(152, 312);
			this.btnKatmanAsagi.Name = "btnKatmanAsagi";
			this.btnKatmanAsagi.Size = new System.Drawing.Size(32, 24);
			this.btnKatmanAsagi.TabIndex = 7;
			this.btnKatmanAsagi.Text = "Asg";
			// 
			// btnKatmanYukarý
			// 
			this.btnKatmanYukarý.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnKatmanYukarý.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.btnKatmanYukarý.ForeColor = System.Drawing.Color.White;
			this.btnKatmanYukarý.Location = new System.Drawing.Point(112, 312);
			this.btnKatmanYukarý.Name = "btnKatmanYukarý";
			this.btnKatmanYukarý.Size = new System.Drawing.Size(32, 24);
			this.btnKatmanYukarý.TabIndex = 6;
			this.btnKatmanYukarý.Text = "Ykr";
			// 
			// lstKatmanlar
			// 
			this.lstKatmanlar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstKatmanlar.ContextMenu = this.katmanlarMenu;
			this.lstKatmanlar.Location = new System.Drawing.Point(8, 176);
			this.lstKatmanlar.Name = "lstKatmanlar";
			this.lstKatmanlar.Size = new System.Drawing.Size(176, 132);
			this.lstKatmanlar.TabIndex = 1;
			this.lstKatmanlar.SelectedIndexChanged += new System.EventHandler(this.lstKatmanlar_SelectedIndexChanged);
			// 
			// katmanlarMenu
			// 
			this.katmanlarMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mnKatmanOzellikler,
																						  this.mnKatmanSil,
																						  this.mnKatmanYeni});
			// 
			// mnKatmanOzellikler
			// 
			this.mnKatmanOzellikler.Index = 0;
			this.mnKatmanOzellikler.Shortcut = System.Windows.Forms.Shortcut.F10;
			this.mnKatmanOzellikler.Text = "Özellikler";
			this.mnKatmanOzellikler.Click += new System.EventHandler(this.mnKatmanOzellikler_Click);
			// 
			// mnKatmanSil
			// 
			this.mnKatmanSil.Index = 1;
			this.mnKatmanSil.Shortcut = System.Windows.Forms.Shortcut.Del;
			this.mnKatmanSil.Text = "Sil";
			this.mnKatmanSil.Click += new System.EventHandler(this.mnKatmanSil_Click);
			// 
			// mnKatmanYeni
			// 
			this.mnKatmanYeni.Index = 2;
			this.mnKatmanYeni.Shortcut = System.Windows.Forms.Shortcut.F2;
			this.mnKatmanYeni.Text = "Yeni katman";
			this.mnKatmanYeni.Click += new System.EventHandler(this.mnKatmanYeni_Click);
			// 
			// btnKatmanSil
			// 
			this.btnKatmanSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnKatmanSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.btnKatmanSil.ForeColor = System.Drawing.Color.White;
			this.btnKatmanSil.Location = new System.Drawing.Point(64, 312);
			this.btnKatmanSil.Name = "btnKatmanSil";
			this.btnKatmanSil.Size = new System.Drawing.Size(40, 24);
			this.btnKatmanSil.TabIndex = 5;
			this.btnKatmanSil.Text = "Sil";
			// 
			// lblKatmanlar
			// 
			this.lblKatmanlar.BackColor = System.Drawing.Color.LightBlue;
			this.lblKatmanlar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblKatmanlar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lblKatmanlar.Location = new System.Drawing.Point(8, 160);
			this.lblKatmanlar.Name = "lblKatmanlar";
			this.lblKatmanlar.Size = new System.Drawing.Size(176, 16);
			this.lblKatmanlar.TabIndex = 2;
			this.lblKatmanlar.Text = "Katmanlar";
			this.lblKatmanlar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CizimRengiSecimi
			// 
			this.CizimRengiSecimi.Color = System.Drawing.Color.White;
			this.CizimRengiSecimi.FullOpen = true;
			// 
			// durumCubugu
			// 
			this.durumCubugu.Location = new System.Drawing.Point(60, 545);
			this.durumCubugu.Name = "durumCubugu";
			this.durumCubugu.Size = new System.Drawing.Size(276, 24);
			this.durumCubugu.SizingGrip = false;
			this.durumCubugu.TabIndex = 5;
			this.durumCubugu.Text = "iboCad v0.1";
			this.ipucuKutusu.SetToolTip(this.durumCubugu, "Durum çubuðu");
			// 
			// ArkaPlanRengiSecimi
			// 
			this.ArkaPlanRengiSecimi.FullOpen = true;
			// 
			// dosyaAcmaPenceresi
			// 
			this.dosyaAcmaPenceresi.FileOk += new System.ComponentModel.CancelEventHandler(this.dosyaAcmaPenceresi_FileOk);
			// 
			// frmAna
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.LightGray;
			this.ClientSize = new System.Drawing.Size(528, 569);
			this.Controls.Add(this.durumCubugu);
			this.Controls.Add(this.pnlSagTaraf);
			this.Controls.Add(this.pnlAraclar);
			this.IsMdiContainer = true;
			this.Menu = this.anaMenu;
			this.Name = "frmAna";
			this.Text = "iboCAD Çizim Programý v0.1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.anaForm_Load);
			this.pnlAraclar.ResumeLayout(false);
			this.pnlSagTaraf.ResumeLayout(false);
			this.pnlMouseKonum.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region MAIN FONKSYONU
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmAna());
		}
		#endregion

		#region Kontrollerin Olaylarý
		private void anaForm_Load(object sender, System.EventArgs e)
		{
			Text = ProgramAdi;
			durumCubugu.Text = ProgramAdi;
			araclar.AracResimleriniGetir();
		}

		private void mnCikis_Click(object sender, System.EventArgs e)
		{
			Close();
		}
		private void btnOlcekArti_Click(object sender, System.EventArgs e)
		{
			if(seciliCizimFormu!=null)
				if(seciliCizimFormu.Olcek<1000) seciliCizimFormu.Olcek += 10;
		}

		private void btnOlcekEksi_Click(object sender, System.EventArgs e)
		{
			if(seciliCizimFormu!=null)
				if(seciliCizimFormu.Olcek>10) seciliCizimFormu.Olcek -= 10;
		}

		private void lstKatmanlar_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listelerHazirlaniyor || listelerTemizleniyor) return;
			seciliCizimFormu.SeciliKatman = (Katman)seciliCizimFormu.katmanlar[lstKatmanlar.SelectedIndex];
			sekilleriListele();
			seciliCizimFormu.GoruntuyuGuncelle();
		}
		private void lstSekiller_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listelerHazirlaniyor || listelerTemizleniyor) return;
			seciliCizimFormu.SeciliKatman.SeciliSekil = (Sekil)seciliCizimFormu.SeciliKatman.sekiller[lstSekiller.SelectedIndex];
			seciliCizimFormu.GoruntuyuGuncelle();
		}
		private void pnlCizimRengi_Click(object sender, EventArgs e)
		{
			//renk seçme dialog kutusunu göster
			CizimRengiSecimi.ShowDialog();
			//seçilen rengi panelde göster
			pnlCizimRengi.BackColor = CizimRengiSecimi.Color;
		}

		private void pnlArkaPlanRengi_Click(object sender, EventArgs e)
		{
			//renk seçme dialog kutusunu göster
			ArkaPlanRengiSecimi.ShowDialog();
			//seçilen rengi panelde göster
			pnlArkaPlanRengi.BackColor = ArkaPlanRengiSecimi.Color;
			//seçili olan çizim formunun arkaplan rengini deðiþtir
			if(seciliCizimFormu!=null)
				seciliCizimFormu.ArkaPlanRengi = ArkaPlanRengiSecimi.Color;
		}
		#endregion

		#region Menü Komutlarý
		private void mnYeniCizim_Click(object sender, System.EventArgs e)
		{
			frmCizim cf = new frmCizim(this);
			cizimFormlari.Add(cf);
			cf.Show();
		}

		private void mnHakkinda_Click(object sender, System.EventArgs e)
		{
			frmHakkinda hakkinda = new frmHakkinda(this);
			hakkinda.MdiParent = this;
			hakkinda.Show();
		}

		private void mnGeriAl_Click(object sender, System.EventArgs e)
		{
			if(seciliCizimFormu!=null)
				seciliCizimFormu.GeriAl();
		}

		private void mnIleriAl_Click(object sender, System.EventArgs e)
		{
			if(seciliCizimFormu!=null)
				seciliCizimFormu.IleriAl();
		}

		private void mnSekilOzellikler_Click(object sender, System.EventArgs e)
		{
			if(seciliCizimFormu!=null)
				if(seciliCizimFormu.SeciliKatman!=null)
					if(seciliCizimFormu.SeciliKatman.SeciliSekil!=null)
					{
						frmSekilOzellikleri ozellikFormu = new frmSekilOzellikleri(this,seciliCizimFormu.SeciliKatman.SeciliSekil);
						ozellikFormu.ShowDialog();
					}
		}

		private void mnSekilSil_Click(object sender, System.EventArgs e)
		{
			if(lstSekiller.Items.Count>0)
				if(lstSekiller.SelectedIndex>-1)
					sekilSil();
		}

		private void mnKatmanOzellikler_Click(object sender, System.EventArgs e)
		{
			if(seciliCizimFormu!=null)
				if(seciliCizimFormu.SeciliKatman!=null)
				{
					frmKatmanOzellikleri ozellikFormu = new frmKatmanOzellikleri(this,seciliCizimFormu.SeciliKatman);
					ozellikFormu.ShowDialog();
				}
		}

		private void mnKatmanSil_Click(object sender, System.EventArgs e)
		{
			if(lstKatmanlar.Items.Count>0)
				if(lstKatmanlar.SelectedIndex>-1)
					katmanSil();
		}

		private void mnKatmanYeni_Click(object sender, System.EventArgs e)
		{
			if(seciliCizimFormu!=null)
			{
				//yeni katman oluþtur ve seçili forma ekle
				Katman k = new Katman();
				k.isim = "yeni katman";
				seciliCizimFormu.katmanlar.Add(k);
				//görüntüyü güncelle
				ListeleriGuncelle();
				seciliCizimFormu.GoruntuyuGuncelle();
			}
		}

		private void mnAc_Click(object sender, System.EventArgs e)
		{
			dosyaAcmaPenceresi.ShowDialog();
		}
		#endregion

		#region Private Fonksyonlar
		//seçili çizim formundaki katmanlarý listeler
		private void katmanlariListele()
		{
			listelerTemizleniyor = true;
			lstKatmanlar.Items.Clear();
			listelerTemizleniyor = false;
			if(seciliCizimFormu!=null)
				foreach(Katman k in seciliCizimFormu.katmanlar)
				{
					lstKatmanlar.Items.Add(k.isim);
					if(k.Equals(seciliCizimFormu.SeciliKatman))
						lstKatmanlar.SelectedIndex = lstKatmanlar.Items.Count-1;
				}
		}
		//çizim formundaki seçili katmandaki þekilleri listeler
		private void sekilleriListele()
		{
			listelerTemizleniyor = true;
			lstSekiller.Items.Clear();
			listelerTemizleniyor = false;
			if(seciliCizimFormu!=null)
				if(seciliCizimFormu.SeciliKatman!=null)
					if(seciliCizimFormu.SeciliKatman.sekiller!=null)
						foreach(Sekil s in seciliCizimFormu.SeciliKatman.sekiller)
						{
							lstSekiller.Items.Add(s.isim);
							if(s.Equals(seciliCizimFormu.SeciliKatman.SeciliSekil))
								lstSekiller.SelectedIndex = lstSekiller.Items.Count-1;
						}
		}
		//seçili formun seçili katmanýndan seçili þekli siler
		private void sekilSil()
		{
			if(seciliCizimFormu!=null)
				if(seciliCizimFormu.SeciliKatman!=null)
					if(seciliCizimFormu.SeciliKatman.SeciliSekil!=null)
					{
						SekilSilmeDegisikligi silme = new SekilSilmeDegisikligi(
											  seciliCizimFormu.SeciliKatman,
											  seciliCizimFormu.SeciliKatman.SeciliSekil);
						silme.Uygula(seciliCizimFormu);
						seciliCizimFormu.YeniDegisiklikEkle(silme);
					}
		}
		//seçili formdan seçili katmaný siler
		private void katmanSil()
		{
			if(seciliCizimFormu!=null)
				if(seciliCizimFormu.SeciliKatman!=null)
				{
					KatmanSilmeDegisikligi silme = new KatmanSilmeDegisikligi(
										   seciliCizimFormu,
										   seciliCizimFormu.SeciliKatman);
					silme.Uygula(seciliCizimFormu);
					seciliCizimFormu.YeniDegisiklikEkle(silme);
				}
		}
		//araç kutusunda bir araç seçildiðinde tetiklenir
		private void araclar_AracDegisti()
		{
			if(seciliCizimFormu!=null)
			{
				if(seciliCizimFormu.cizim!=null)//önceki çizimi iptal et
					seciliCizimFormu.cizim.IptalEt();
				if(seciliCizimFormu.SeciliKatman!=null) //çizim tipini ayarla
					seciliCizimFormu.cizim.Tip = araclar.SeciliArac.CizimTip;
			}
		}
		#endregion

		#region Public Fonksyonlar
		//sað taraftaki listeleri güncelle
		public void ListeleriGuncelle()
		{
			listelerHazirlaniyor = true;
			katmanlariListele();
			sekilleriListele();
			listelerHazirlaniyor = false;
		}
		//bir çizim formunu cizimFormlari listesinden çýkarýr
		public void CizimFormuCikar(frmCizim cizimFormu)
		{
			cizimFormlari.Remove(cizimFormu);
			//eðer hiç çizim formu açýk kalmamýþsa seciliCizimFormu'nu null yap
			if(cizimFormlari.Count==0)
			{	
				seciliCizimFormu = null;
				ListeleriGuncelle();
			}
		}
		//þekil listesinden index'e göre bir þekli seçmek için kullanýlýr
		public void SekilSec(int indeks)
		{
			if(lstSekiller.Items.Count>indeks)
				lstSekiller.SelectedIndex = indeks;
		}
		#endregion

		private void dosyaAcmaPenceresi_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			frmCizim cf = new frmCizim(this);
			cizimFormlari.Add(cf);
			cf.DosyaAc(dosyaAcmaPenceresi.FileName);
			cf.Show();
		}

		#region Public Özellikler
		//mouse'un koordinatlarý
		public float MouseX
		{
			get
			{
				return mouseX;
			}
			set
			{
				mouseX = value;
				txtMouseX.Text = mouseX.ToString("000.0");
			}
		}
		public float MouseY
		{
			get
			{
				return mouseY;
			}
			set
			{
				mouseY = value;
				txtMouseY.Text = mouseY.ToString("000.0");
			}
		}
		//seçili çizim formuna referans
		public frmCizim SeciliCizimFormu
		{
			get
			{
				return seciliCizimFormu;
			}
			set
			{
				seciliCizimFormu = value;
				ListeleriGuncelle();
				if(seciliCizimFormu!=null)
				{
					//seçilen formun çizim aracýný deðiþtir
					seciliCizimFormu.cizim.Tip = araclar.SeciliArac.CizimTip;
					//seçilen formun arkaplan rengini al
					ArkaPlanRengiSecimi.Color = seciliCizimFormu.ArkaPlanRengi;
					pnlArkaPlanRengi.BackColor = seciliCizimFormu.ArkaPlanRengi;
				}				
			}
		}
		//araç kutusuna referans
		public Araclar AracKutusu
		{
			get
			{
				return araclar;
			}
		}
		//seçili çizim rengi
		public Color CizimRengi
		{
			get
			{
				return CizimRengiSecimi.Color;
			}
		}
		//seçili arkaplan rengi
		public Color ArkaPlanRengi
		{
			get
			{
				return ArkaPlanRengiSecimi.Color;
			}
		}
		//Hakkýnda penceresini ayný anda birden fazla açtýrmamak için dýþarýdan eriþilebilir yapar
		public bool HakkindaAktif
		{
			get
			{
				return mnHakkinda.Enabled;
			}
			set
			{
				mnHakkinda.Enabled = value;
			}
		}
		//Geri Al ve Ýleri Al'a dýþarýdan ulaþmak için özellikler
		public bool GeriAlAktif
		{
			get
			{
				return mnGeriAl.Enabled;
			}
			set
			{
				mnGeriAl.Enabled = value;
			}
		}
		public bool IleriAlAktif
		{
			get
			{
				return mnIleriAl.Enabled;
			}
			set
			{
				mnIleriAl.Enabled = value;
			}
		}
		#endregion
	}
}
