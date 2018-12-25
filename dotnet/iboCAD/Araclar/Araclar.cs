using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace iboCAD
{
	//Araç kutusunu temsil eder
	public class Araclar : System.Windows.Forms.UserControl
	{
		//Olaylar
		public delegate void aracDegisti();
		public event aracDegisti AracDegisti;
		//Araçlar
		private Arac secme;
		private Arac tasima;
		private Arac dikdortgen;
		private Arac cember;
		private Arac elips;
		private Arac dogru;
		private Arac dogruSerisi;
		private Arac seciliArac;
		//otomatik nesneler
		private System.Windows.Forms.PictureBox pbDogruSerisi;
		private System.Windows.Forms.PictureBox pbDogru;
		private System.Windows.Forms.PictureBox pbElips;
		private System.Windows.Forms.PictureBox pbCember;
		private System.Windows.Forms.PictureBox pbDikdortgen;
		private System.Windows.Forms.ToolTip ipucuKutusu;
		private System.Windows.Forms.PictureBox pbTasima;
		private System.Windows.Forms.PictureBox pbSecme;
		private System.ComponentModel.IContainer components;
	
		public Araclar()
		{
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
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pbDogruSerisi = new System.Windows.Forms.PictureBox();
			this.pbDogru = new System.Windows.Forms.PictureBox();
			this.pbElips = new System.Windows.Forms.PictureBox();
			this.pbCember = new System.Windows.Forms.PictureBox();
			this.pbDikdortgen = new System.Windows.Forms.PictureBox();
			this.ipucuKutusu = new System.Windows.Forms.ToolTip(this.components);
			this.pbTasima = new System.Windows.Forms.PictureBox();
			this.pbSecme = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// pbDogruSerisi
			// 
			this.pbDogruSerisi.Location = new System.Drawing.Point(0, 180);
			this.pbDogruSerisi.Name = "pbDogruSerisi";
			this.pbDogruSerisi.Size = new System.Drawing.Size(41, 28);
			this.pbDogruSerisi.TabIndex = 16;
			this.pbDogruSerisi.TabStop = false;
			this.ipucuKutusu.SetToolTip(this.pbDogruSerisi, "Doðru serisi");
			// 
			// pbDogru
			// 
			this.pbDogru.Location = new System.Drawing.Point(0, 154);
			this.pbDogru.Name = "pbDogru";
			this.pbDogru.Size = new System.Drawing.Size(41, 26);
			this.pbDogru.TabIndex = 15;
			this.pbDogru.TabStop = false;
			this.ipucuKutusu.SetToolTip(this.pbDogru, "Doðru");
			// 
			// pbElips
			// 
			this.pbElips.Location = new System.Drawing.Point(0, 126);
			this.pbElips.Name = "pbElips";
			this.pbElips.Size = new System.Drawing.Size(41, 28);
			this.pbElips.TabIndex = 14;
			this.pbElips.TabStop = false;
			this.ipucuKutusu.SetToolTip(this.pbElips, "Elips");
			// 
			// pbCember
			// 
			this.pbCember.Location = new System.Drawing.Point(0, 93);
			this.pbCember.Name = "pbCember";
			this.pbCember.Size = new System.Drawing.Size(41, 33);
			this.pbCember.TabIndex = 13;
			this.pbCember.TabStop = false;
			this.ipucuKutusu.SetToolTip(this.pbCember, "Çember");
			// 
			// pbDikdortgen
			// 
			this.pbDikdortgen.Location = new System.Drawing.Point(0, 62);
			this.pbDikdortgen.Name = "pbDikdortgen";
			this.pbDikdortgen.Size = new System.Drawing.Size(41, 31);
			this.pbDikdortgen.TabIndex = 12;
			this.pbDikdortgen.TabStop = false;
			this.ipucuKutusu.SetToolTip(this.pbDikdortgen, "Dikdörtgen");
			// 
			// pbTasima
			// 
			this.pbTasima.Location = new System.Drawing.Point(0, 31);
			this.pbTasima.Name = "pbTasima";
			this.pbTasima.Size = new System.Drawing.Size(41, 31);
			this.pbTasima.TabIndex = 17;
			this.pbTasima.TabStop = false;
			this.ipucuKutusu.SetToolTip(this.pbTasima, "Taþýma");
			// 
			// pbSecme
			// 
			this.pbSecme.Location = new System.Drawing.Point(0, 0);
			this.pbSecme.Name = "pbSecme";
			this.pbSecme.Size = new System.Drawing.Size(41, 31);
			this.pbSecme.TabIndex = 18;
			this.pbSecme.TabStop = false;
			this.ipucuKutusu.SetToolTip(this.pbSecme, "Seçme");
			// 
			// Araclar
			// 
			this.Controls.Add(this.pbSecme);
			this.Controls.Add(this.pbTasima);
			this.Controls.Add(this.pbDogruSerisi);
			this.Controls.Add(this.pbDogru);
			this.Controls.Add(this.pbElips);
			this.Controls.Add(this.pbCember);
			this.Controls.Add(this.pbDikdortgen);
			this.Name = "Araclar";
			this.Size = new System.Drawing.Size(41, 215);
			this.ResumeLayout(false);

		}
		#endregion
		public void AracResimleriniGetir()
		{
			secme		= new Arac(this, pbSecme, AracTipi.Secme, "secme_normal.gif", "secme_normal.gif", "secme_secili.gif");
			tasima		= new Arac(this, pbTasima, AracTipi.Tasima, "tasima_normal.gif", "tasima_normal.gif", "tasima_secili.gif");
			dikdortgen	= new Arac(this, pbDikdortgen, AracTipi.Dikdortgen, "dikdortgen_normal.gif", "dikdortgen_ustunde.gif", "dikdortgen_secili.gif");
			cember		= new Arac(this, pbCember, AracTipi.Cember, "cember_normal.gif", "cember_ustunde.gif", "cember_secili.gif");
			elips		= new Arac(this, pbElips, AracTipi.Elips, "elips_normal.gif", "elips_ustunde.gif", "elips_secili.gif");
			dogru		= new Arac(this, pbDogru, AracTipi.Dogru, "dogru_normal.gif", "dogru_ustunde.gif", "dogru_secili.gif");
			dogruSerisi	= new Arac(this, pbDogruSerisi, AracTipi.DogruSerisi, "dogruSerisi_normal.gif", "dogruSerisi_ustunde.gif", "dogruSerisi_secili.gif");
			SeciliArac	= secme;
		}
		public Arac SeciliArac
		{
			get
			{
				return seciliArac;
            }
			set
			{
				if(seciliArac!=null)
					seciliArac.Secili = false;
				seciliArac = value;
				if(seciliArac!=null)
				{
					seciliArac.Secili = true;
					AracDegisti(); //araç deðiþti olayýný tetikle
				}
			}
		}
		public void AracDegistir(AracTipi secilecekArac)
		{
			switch(secilecekArac)
			{
				case AracTipi.Tasima:
					SeciliArac = tasima;
					break;
				case AracTipi.Secme:
					SeciliArac = secme;
					break;
				case AracTipi.Cember:
					SeciliArac = cember;
					break;
				case AracTipi.Dikdortgen:
					SeciliArac = dikdortgen;
					break;
				case AracTipi.Dogru:
					SeciliArac = dogru;
					break;
				case AracTipi.DogruSerisi:
					SeciliArac = dogruSerisi;
					break;
				case AracTipi.Elips:
					SeciliArac = elips;
					break;					
			}
		}
	}
}
