using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace iboCAD
{
	/// <summary>
	/// Summary description for frmHakkinda.
	/// </summary>
	public class frmHakkinda : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label7;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblProgramlayan;
		private System.Windows.Forms.Label lblEposta;
		private System.Windows.Forms.Label lblSonGuncelleme;
		private System.Windows.Forms.LinkLabel llkalkancjbnet;
		private System.Windows.Forms.Label lblProgramAdi;
		private frmAna anaForm;
		public frmHakkinda(frmAna anaForm)
		{
			this.anaForm = anaForm;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblProgramAdi = new System.Windows.Forms.Label();
			this.lblProgramlayan = new System.Windows.Forms.Label();
			this.lblEposta = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblSonGuncelleme = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.llkalkancjbnet = new System.Windows.Forms.LinkLabel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.LightSlateGray;
			this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(0, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Programlayan:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.LightSlateGray;
			this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(0, 33);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(160, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "E-posta:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblProgramAdi
			// 
			this.lblProgramAdi.BackColor = System.Drawing.Color.LightSkyBlue;
			this.lblProgramAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblProgramAdi.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lblProgramAdi.ForeColor = System.Drawing.Color.Black;
			this.lblProgramAdi.Location = new System.Drawing.Point(0, 0);
			this.lblProgramAdi.Name = "lblProgramAdi";
			this.lblProgramAdi.Size = new System.Drawing.Size(360, 16);
			this.lblProgramAdi.TabIndex = 2;
			this.lblProgramAdi.Text = "Program Baþlýðý";
			this.lblProgramAdi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblProgramlayan
			// 
			this.lblProgramlayan.BackColor = System.Drawing.Color.SteelBlue;
			this.lblProgramlayan.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lblProgramlayan.ForeColor = System.Drawing.Color.White;
			this.lblProgramlayan.Location = new System.Drawing.Point(160, 16);
			this.lblProgramlayan.Name = "lblProgramlayan";
			this.lblProgramlayan.Size = new System.Drawing.Size(200, 16);
			this.lblProgramlayan.TabIndex = 3;
			this.lblProgramlayan.Text = "Halil Ýbrahim Kalkan";
			this.lblProgramlayan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblEposta
			// 
			this.lblEposta.BackColor = System.Drawing.Color.SteelBlue;
			this.lblEposta.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lblEposta.ForeColor = System.Drawing.Color.White;
			this.lblEposta.Location = new System.Drawing.Point(160, 33);
			this.lblEposta.Name = "lblEposta";
			this.lblEposta.Size = new System.Drawing.Size(200, 16);
			this.lblEposta.TabIndex = 4;
			this.lblEposta.Text = "hi_kalkan@yahoo.com";
			this.lblEposta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Silver;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lblSonGuncelleme);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.lblEposta);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.lblProgramlayan);
			this.panel1.Controls.Add(this.lblProgramAdi);
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(360, 68);
			this.panel1.TabIndex = 5;
			// 
			// lblSonGuncelleme
			// 
			this.lblSonGuncelleme.BackColor = System.Drawing.Color.SteelBlue;
			this.lblSonGuncelleme.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lblSonGuncelleme.ForeColor = System.Drawing.Color.White;
			this.lblSonGuncelleme.Location = new System.Drawing.Point(160, 50);
			this.lblSonGuncelleme.Name = "lblSonGuncelleme";
			this.lblSonGuncelleme.Size = new System.Drawing.Size(200, 16);
			this.lblSonGuncelleme.TabIndex = 6;
			this.lblSonGuncelleme.Text = "Tarih";
			this.lblSonGuncelleme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.BackColor = System.Drawing.Color.LightSlateGray;
			this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.label7.ForeColor = System.Drawing.Color.White;
			this.label7.Location = new System.Drawing.Point(0, 50);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(160, 16);
			this.label7.TabIndex = 5;
			this.label7.Text = "Son güncelleme:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// llkalkancjbnet
			// 
			this.llkalkancjbnet.ActiveLinkColor = System.Drawing.Color.White;
			this.llkalkancjbnet.BackColor = System.Drawing.Color.LightSkyBlue;
			this.llkalkancjbnet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.llkalkancjbnet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.llkalkancjbnet.ForeColor = System.Drawing.Color.White;
			this.llkalkancjbnet.LinkColor = System.Drawing.Color.Black;
			this.llkalkancjbnet.Location = new System.Drawing.Point(8, 80);
			this.llkalkancjbnet.Name = "llkalkancjbnet";
			this.llkalkancjbnet.Size = new System.Drawing.Size(360, 16);
			this.llkalkancjbnet.TabIndex = 7;
			this.llkalkancjbnet.TabStop = true;
			this.llkalkancjbnet.Text = "http://www.kalkan.cjb.net";
			this.llkalkancjbnet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// frmHakkinda
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(376, 102);
			this.Controls.Add(this.llkalkancjbnet);
			this.Controls.Add(this.panel1);
			this.Name = "frmHakkinda";
			this.Text = "Pencere Baþlýðý";
			this.Load += new System.EventHandler(this.frmHakkinda_Load);
			this.Closed += new System.EventHandler(this.frmHakkinda_Closed);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmHakkinda_Load(object sender, EventArgs e)
		{
			Text					= frmAna.ProgramAdi+" Hakkýnda";
			lblProgramAdi.Text		= frmAna.ProgramAdi+" v"+frmAna.Versiyon;
			lblSonGuncelleme.Text	= frmAna.SonGuncellemeTarihi;
			anaForm.HakkindaAktif	= false;
		}

		private void frmHakkinda_Closed(object sender, EventArgs e)
		{
			anaForm.HakkindaAktif = true;
		}
	}
}
