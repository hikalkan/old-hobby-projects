<%@ Page Language="vb" AutoEventWireup="false" Codebehind="seminer.aspx.vb" Inherits="bilmuh.seminer"%>
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
		<form id="frmSeminerGenel" runat="server">
			<table width="600" align="center" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td align="center" width="600"><img src="images/baslik/seminer.gif"></td>
				</tr>
				<tr>
					<td width="600">
						<asp:PlaceHolder ID="phOrta" Runat="server" Visible="True"></asp:PlaceHolder>
						<div id="divSeminerEklemeFormu" runat="server">
							<table align="center" width="90%" bgColor="#555555" border="0" cellpadding="1" cellspacing="1">
								<tr>
									<td width="100%" class="Sade" align="center" bgcolor="#eeeeff" colspan="2">
										... <b>Seminer Ekle</b> ...
									</td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Konu (100):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtKonu" Runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Ýçerik (255):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtIcerik" Runat="server" Width="200px" MaxLength="255"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Konuþmacý (40):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtKonusmaci" Runat="server" Width="200px" MaxLength="40"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Konuþmacý Hakkýnda (255):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtKonusmaciHakkinda" Runat="server" Width="200px" MaxLength="255"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Tarih:</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:dropdownlist id="ddlGun" Runat="server">
											<asp:ListItem Value="0" Selected="True">G&#252;n</asp:ListItem>
										</asp:dropdownlist><asp:dropdownlist id="ddlay" Runat="server">
											<asp:ListItem Value="0">Ay</asp:ListItem>
										</asp:dropdownlist><asp:dropdownlist id="ddlYil" Runat="server">
											<asp:ListItem Value="0">Yýl</asp:ListItem>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Saat (24):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtSaat" Runat="server" Width="200px" MaxLength="2"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Yer (100):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtYer" Runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Davetliler (50):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtDavetliler" Runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff" style="HEIGHT: 25px">
										<P align="right">Süre:</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff" style="HEIGHT: 25px"><asp:TextBox ID="txtSure" Runat="server" Width="200px" MaxLength="1"></asp:TextBox>
										saat</td>
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
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:Button ID="btnSeminerEkleTamam" Runat="server" Text="Tamam"></asp:Button></td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
