<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Uyelik.aspx.vb" Inherits="bilmuh.Uyelik"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Bilgisayar Kul�b� Hakk�nda</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Content-Language" content="tr">
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1254">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="5" rightMargin="0">
		<form id="UyelikFormu" runat="server">
			<div id="divUyelikFormu" runat="server">
				<table cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
					<tr>
						<td class="Sade" align="center" width="100%"><IMG src="images/baslik/Uyelik.gif"><br>
							Sitemize �ye olmak i�in a�a��daki formu doldurup tamam butonuna t�klay�n.
						</td>
					</tr>
					<tr>
						<td width="100%">
							<table cellSpacing="1" cellPadding="1" width="90%" align="center" bgColor="#555555" border="0">
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"><b>Ad�n�z</b>:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtAd" MaxLength="20" Runat="server" Width="200px"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"><b>Soyad�n�z</b>:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtSoyad" MaxLength="20" Runat="server" Width="200px"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"><b>E-Posta</b>:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtEposta" MaxLength="50" Runat="server" Width="200px"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff">(varsa) Web Sitesi:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtWebSite" MaxLength="100" Runat="server" Width="200px"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" vAlign="top" align="right" width="50%" bgColor="#eeeeff">Lakap 
										(Takma Ad)*:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtLakap" MaxLength="30" Runat="server" Width="200px"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff">Do�um Tarihi:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:dropdownlist id="ddlGun" Runat="server">
											<asp:ListItem Value="0" Selected="True">G�n</asp:ListItem>
										</asp:dropdownlist><asp:dropdownlist id="ddlAy" Runat="server">
											<asp:ListItem Value="0">Ay</asp:ListItem>
										</asp:dropdownlist><asp:dropdownlist id="ddlYil" Runat="server">
											<asp:ListItem Value="0">Y�l</asp:ListItem>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"><b>Kullan�c� Ad�</b>:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtKullaniciAdi" MaxLength="25" Runat="server" Width="200px"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"><b>�ifre</b>:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtSifre" MaxLength="25" Runat="server" Width="200px" TextMode="Password"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"><b>�ifre (Tekrar)</b>:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtSifreTekrar" MaxLength="25" Runat="server" Width="200px" TextMode="Password"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"></td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:button id="btnTamam" Runat="server" Text="Tamam"></asp:button></td>
								</tr>
							</table>
							<p class="Normal">*Bu takma ad sitedeki i�lemlerinizde sizi temsil eder. Bo� 
								b�rak�rsan�z isminiz kullan�lacakt�r.
							</p>
						</td>
					</tr>
				</table>
			</div>
			<div id="divUyeGirisi" runat="server">
				<table cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
					<tr>
						<td class="Sade" align="center" width="100%"><IMG src="images/baslik/Uyegiris.gif"><br>
							�ye Giri�i i�in kullan�c� ad� ve �ifrenizi girip tamam butonuna t�klay�n.
						</td>
					</tr>
					<tr>
						<td width="100%">
							<table cellSpacing="1" cellPadding="1" width="90%" align="center" bgColor="#555555" border="0">
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"><b>Kullan�c� Ad�</b>:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtUGKullaniciAdi" MaxLength="20" Runat="server" Width="200px"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"><b>�ifre</b>:</td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:textbox id="txtUGSifre" MaxLength="20" Runat="server" Width="200px" TextMode="Password"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Sade" align="right" width="50%" bgColor="#eeeeff"></td>
									<td class="Sade" align="left" width="50%" bgColor="#ddddff"><asp:button id="btnUGGiris" Runat="server" Text="Tamam"></asp:button></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<p class="Sade" align="center"><a href="uyelik.aspx?Sayfa=UyelikFormu">�ye de�ilseniz �ye olmak i�in buraya t�klay�n.</a></p>
			</div>
			<div id="divUyeBilgisi" runat="server">
				<table cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
					<tr>
						<td class="Sade" align="center" width="100%"><IMG src="images/baslik/Uyebilgi.gif"></td>
					</tr>
					<tr>
						<td width="100%">
							<asp:PlaceHolder ID="phUyeBilgisi" Runat="server" Visible="True"></asp:PlaceHolder>
							<p align="center"><a href="javascript:history.back()"><img src="images/diger/geridon.gif" border="0"></a></p>
						</td>
					</tr>
				</table>
			</div>
			<asp:PlaceHolder ID="phHata" Runat="server" Visible="False"></asp:PlaceHolder>
		</form>
	</body>
</HTML>
