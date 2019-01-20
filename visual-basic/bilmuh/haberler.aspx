<%@ Page Language="vb" AutoEventWireup="false" Codebehind="haberler.aspx.vb" Inherits="bilmuh.haberler" validateRequest="false"%>
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
	<body bgcolor="#ebebeb" topmargin="5" leftmargin="0">
		<table width="600" align="center" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td align="center" width="600"><img src="images/baslik/haber.gif"></td>
			</tr>
			<tr>
				<td width="600"><asp:PlaceHolder ID="phOrta" Runat="server"></asp:PlaceHolder>
					<div id="divHaberYazFormu" runat="server">
						<form id="frmHaberYaz" method="post" runat="server">
							<table align="center" width="90%" bgColor="#555555" border="0" cellpadding="1" cellspacing="1">
								<tr>
									<td width="100%" class="Sade" align="center" bgcolor="#eeeeff" colspan="2">
										... <b>Haber Yaz</b> ...
									</td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Haber baþlýðý (100):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtHaberBaslik" Runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="left" bgcolor="#eeeeff">
										<P align="right">Açýklama (255):</P>
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtHaberAciklama" Runat="server" Width="200px" MaxLength="255"></asp:TextBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="right" bgcolor="#eeeeff" valign="top">
										Haber Metni:
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:TextBox ID="txtHaberMetni" Runat="server" Width="368px" MaxLength="60000" TextMode="MultiLine"
											Height="184px"></asp:TextBox><br>
										<asp:CheckBox ID="chkHtmlKodu" Runat="server" Text="Html taglarýný kabul et" Checked="True"></asp:CheckBox></td>
								</tr>
								<tr>
									<td width="30%" class="Sade" align="right" bgcolor="#eeeeff" valign="top">
									</td>
									<td width="70%" class="Sade" align="left" bgcolor="#ddddff"><asp:Button ID="btnHaberTamam" Runat="server" Text="Tamam"></asp:Button></td>
								</tr>
							</table>
						</form>
					</div>
				</td>
			</tr>
		</table>
	</body>
</HTML>
