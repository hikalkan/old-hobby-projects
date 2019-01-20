<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="anket.WebForm1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sakarya Üniversitesi Bilgisayar Mühendsiliði Bölümü Anket Sitesi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="anaSayfaForm" method="post" runat="server">
			<table id="Table_01" width="410" height="333" border="0" cellpadding="0" cellspacing="0"
				align="center">
				<tr>
					<td colspan="5">
						<img src="grafik/giris/giris_01.gif" width="410" height="107" alt=""></td>
				</tr>
				<tr>
					<td rowspan="5">
						<img src="grafik/giris/giris_02.gif" width="169" height="226" alt=""></td>
					<td colspan="3" width="231" height="44" background="grafik/giris/giris_03.gif"><asp:TextBox ID="txtKullaniciAdi" Runat="server" Width="100px" MaxLength="20" BackColor="White"></asp:TextBox></td>
					<td rowspan="5">
						<img src="grafik/giris/giris_04.gif" width="10" height="226" alt=""></td>
				</tr>
				<tr>
					<td colspan="3" width="231" height="36" background="grafik/giris/giris_05.gif"><asp:TextBox ID="txtSifre" Runat="server" Width="100px" MaxLength="20" BackColor="White" TextMode="Password"></asp:TextBox></td>
				</tr>
				<tr>
					<td colspan="3">
						<img src="grafik/giris/giris_06.gif" width="231" height="24" alt=""></td>
				</tr>
				<tr>
					<td rowspan="2">
						<img src="grafik/giris/giris_07.gif" width="56" height="122" alt=""></td>
					<td>
						<asp:ImageButton id="imbGiris" runat="server" ImageUrl="grafik/giris/giris_08.gif" AlternateText="Giriþ"></asp:ImageButton></td>
					<td rowspan="2">
						<img src="grafik/giris/giris_09.gif" width="54" height="122" alt=""></td>
				</tr>
				<tr>
					<td>
						<img src="grafik/giris/giris_10.gif" width="121" height="83" alt=""></td>
				</tr>
			</table>
			<p class="DuzYazi" align="center"><asp:label id="lblHata" Runat="server"></asp:label></p>
		</form>
	</body>
</HTML>
