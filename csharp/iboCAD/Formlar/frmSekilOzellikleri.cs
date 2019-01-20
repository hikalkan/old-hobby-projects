using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace iboCAD
{
	public class frmSekilOzellikleri : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtAd;
		private System.Windows.Forms.Button btnTamam;
		private System.ComponentModel.Container components = null;
		private frmAna anaForm;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblIsim;
		private Sekil sekil;
		public frmSekilOzellikleri(frmAna anaForm, Sekil sekil)
		{
			this.anaForm = anaForm;
			this.sekil = sekil;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtAd = new System.Windows.Forms.TextBox();
			this.lblIsim = new System.Windows.Forms.Label();
			this.btnTamam = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtAd
			// 
			this.txtAd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtAd.Location = new System.Drawing.Point(72, 8);
			this.txtAd.MaxLength = 50;
			this.txtAd.Name = "txtAd";
			this.txtAd.Size = new System.Drawing.Size(200, 20);
			this.txtAd.TabIndex = 0;
			this.txtAd.Text = "";
			// 
			// lblIsim
			// 
			this.lblIsim.BackColor = System.Drawing.Color.SteelBlue;
			this.lblIsim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblIsim.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(162)));
			this.lblIsim.ForeColor = System.Drawing.Color.White;
			this.lblIsim.Location = new System.Drawing.Point(8, 8);
			this.lblIsim.Name = "lblIsim";
			this.lblIsim.Size = new System.Drawing.Size(64, 20);
			this.lblIsim.TabIndex = 1;
			this.lblIsim.Text = "Ýsim:";
			this.lblIsim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnTamam
			// 
			this.btnTamam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTamam.Location = new System.Drawing.Point(8, 32);
			this.btnTamam.Name = "btnTamam";
			this.btnTamam.Size = new System.Drawing.Size(264, 23);
			this.btnTamam.TabIndex = 2;
			this.btnTamam.Text = "Tamam";
			this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.txtAd);
			this.panel1.Controls.Add(this.lblIsim);
			this.panel1.Controls.Add(this.btnTamam);
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(280, 64);
			this.panel1.TabIndex = 3;
			// 
			// frmSekilOzellikleri
			// 
			this.AcceptButton = this.btnTamam;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(298, 80);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSekilOzellikleri";
			this.Text = "Þekil Özellikleri";
			this.Load += new System.EventHandler(this.frmSekilOzellikleri_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void frmSekilOzellikleri_Load(object sender, System.EventArgs e)
		{
			Text = "'" + sekil.isim + "' özellikleri";
			txtAd.Text = sekil.isim;
		}
		private void btnTamam_Click(object sender, System.EventArgs e)
		{
			sekil.isim = txtAd.Text;
			anaForm.ListeleriGuncelle();
			this.Close();
		}
	}
}
