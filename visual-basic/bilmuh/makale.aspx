<%@ Page Language="vb" AutoEventWireup="false" Codebehind="makale.aspx.vb" Inherits="bilmuh.makale" validateRequest="false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sakarya Üniversitesi Bilgisayar Kulübü</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" Type="text/css" href="Styles.css">
	</HEAD>
	<body topmargin="5" leftmargin="0" rightmargin="0">
		<form id="frmOrta" method="post" runat="server">
			<table width="600" align="center" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td align="center" width="600"><img src="images/baslik/makale.gif"></td>
				</tr>
				<tr>
					<td width="600">
						<asp:PlaceHolder ID="phOrta" Runat="server"></asp:PlaceHolder>
						<div id="divMakaleYazFormu" runat="server">
							<table align="center" width="90%" bgColor="#555555" border="0" cellpadding="1" cellspacing="1">
								<tr>
									<td width="100%" class="Sade" align="center" bgcolor="#eeeeff" colspan="2">
										... <b>
											<asp:Label id="lblMakaleKategoriAd" Runat="server" Visible="True"></asp:Label>
											Kategorisine Makale Ekle</b> ...
									</td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Makale Baþlýðý (100):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtMakaleBaslik" Runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Açýklama (255):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtMakaleAciklama" Runat="server" Width="200px" MaxLength="255"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff" valign="top">
										<P align="right">Makaleyi buraya yazýn:</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtMakaleIcerik" Runat="server" Width="360px" MaxLength="60000" TextMode="MultiLine"
											Height="192px"></asp:TextBox><br>
										<asp:CheckBox ID="chkMakaleHtmlKullan" Runat="server" Text="Html taglarýný kabul et" Checked="True"></asp:CheckBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="right" bgcolor="#eeeeff" valign="top">
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:Button ID="btnMakaleEkle" Runat="server" Text="Tamam"></asp:Button></td>
								</tr>
							</table>
						</div>
						<div id="divKategoriEkleFormu" runat="server">
							<table align="center" width="90%" bgColor="#555555" border="0" cellpadding="1" cellspacing="1">
								<tr>
									<td width="100%" class="Sade" align="center" bgcolor="#eeeeff" colspan="2">
										... <b>Makale Kategorisi Ekle</b> ...
									</td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Kategori Adý (50):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtKategoriAd" Runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Açýklama (255):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtKategoriAciklama" Runat="server" Width="200px" MaxLength="255"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Ýkon (15):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtKategoriIkon" Runat="server" Width="200px" MaxLength="15"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="right" bgcolor="#eeeeff" valign="top">
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:Button ID="btnKategoriEkle" Runat="server" Text="Tamam"></asp:Button></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
