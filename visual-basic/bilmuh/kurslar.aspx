<%@ Page Language="vb" AutoEventWireup="false" Codebehind="kurslar.aspx.vb" Inherits="bilmuh.kurslar"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sakarya �niversitesi Bilgisayar Kul�b�</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" Type="text/css" href="Styles.css">
	</HEAD>
	<body topmargin="5" leftmargin="0" rightmargin="0">
		<form id="frmKurslarGenel" runat="server">
			<table width="600" align="center" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td align="center" width="600"><img src="images/baslik/kurslar.gif"></td>
				</tr>
				<tr>
					<td width="600">
						<asp:PlaceHolder ID="phOrta" Runat="server" Visible="True"></asp:PlaceHolder>
						<div id="divKursEklemeFormu" runat="server">
							<table align="center" width="90%" bgColor="#555555" border="0" cellpadding="1" cellspacing="1">
								<tr>
									<td width="100%" class="Sade" align="center" bgcolor="#eeeeff" colspan="2">
										... <b>Kurs Ekle</b> ...
									</td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Konu (100):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtKonu" Runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff" valign="top">
										<P align="right">��erik (60000):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtIcerik" Runat="server" Width="352px" MaxLength="60000" TextMode="MultiLine"
											Height="120px"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Anlat�c� (40):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtAnlatici" Runat="server" Width="200px" MaxLength="40"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Anlat�c� Hakk�nda (255):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtAnlaticiHakkinda" Runat="server" Width="200px" MaxLength="255"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Ba�lang�� Tarihi:</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:dropdownlist id="ddlBasGun" Runat="server">
											<asp:ListItem Value="0" Selected="True">G&#252;n</asp:ListItem>
										</asp:dropdownlist><asp:dropdownlist id="ddlBasAy" Runat="server">
											<asp:ListItem Value="0">Ay</asp:ListItem>
										</asp:dropdownlist><asp:dropdownlist id="ddlBasYil" Runat="server">
											<asp:ListItem Value="0">Y�l</asp:ListItem>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Biti� Tarihi:</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:dropdownlist id="ddlBitGun" Runat="server">
											<asp:ListItem Value="0" Selected="True">G&#252;n</asp:ListItem>
										</asp:dropdownlist><asp:dropdownlist id="ddlBitAy" Runat="server">
											<asp:ListItem Value="0">Ay</asp:ListItem>
										</asp:dropdownlist><asp:dropdownlist id="ddlBitYil" Runat="server">
											<asp:ListItem Value="0">Y�l</asp:ListItem>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Haftal�k ders saati:</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtDersSaati" Runat="server" Width="200px" MaxLength="2"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Yer ve Saat (255):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtYer" Runat="server" Width="200px" MaxLength="255"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Ba�vuru (100):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtBasvuru" Runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff" style="HEIGHT: 25px">
										<P align="right">Kat�l�m �artlar� (255):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff" style="HEIGHT: 25px"><asp:TextBox ID="txtKatilimSartlari" Runat="server" Width="200px" MaxLength="255"></asp:TextBox>
									</td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Ek bilgiler (255):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtEkBilgiler" Runat="server" Width="200px" MaxLength="255"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="right" bgcolor="#eeeeff" valign="top">
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:Button ID="btnKursEkleTamam" Runat="server" Text="Tamam"></asp:Button></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
