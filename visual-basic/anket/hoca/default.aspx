<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="anket._default2"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sakarya �niversitesi Bilgisayar M�hendsili�i B�l�m� Anket Sitesi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<p class="DuzYazi" align="center">
			[<a href="../default.aspx?Sayfa=Cikis">��k��</a>]
			<asp:Label ID="lblYonetici" Runat="server">[<a href="../yonetici/default.aspx">Y�netici 
					B�l�m�</a>]</asp:Label>
			<asp:Label ID="lblGeri" Runat="server"></asp:Label>
		</p>
		<form id="Form1" method="post" runat="server">
			<div id="divAnaSayfa" runat="server">
				<table width="90%" align="center" bgcolor="#000000" cellpadding="1" cellspacing="1">
					<tr>
						<td align='left' bgcolor='#dddddd' width='80%' class='DuzYazi'>
							<b>Ho� Geldiniz
								<asp:Label ID="lblHocaAd" Runat="server"></asp:Label>;</b>
						</td>
						<td align='center' bgcolor='#dddddd' width='20%' class='DuzYazi'>
							<a href="default.aspx?Sayfa=SifreDegistir"><Font color="#000000">�ifre De�i�tir</Font></a>
						</td>
					</tr>
				</table>
			</div>
			<div id="divSifreDegistir" runat="server">
				<TABLE id="tblDifreDegistir" cellSpacing="1" cellPadding="1" width="50%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b>:: 
									�ifre De�i�tir ::</b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Eski 
									�ifreniz:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtSifreE" Runat="server" MaxLength="20" Width="100px" TextMode="Password"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Yeni 
									�ifreniz:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtSifreY" Runat="server" MaxLength="20" Width="100px" TextMode="Password"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Yeni 
									�ifreniz (tekrar):</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtSifreYT" Runat="server" MaxLength="20" Width="100px" TextMode="Password"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="center" colspan="2" width="100%" bgColor="#99bbff"><asp:Button ID="btnSifreDegistirTamam" Text="De�i�tir" Runat="server"></asp:Button></td>
					</tr>
				</TABLE>
			</div>
			<asp:PlaceHolder ID="phOrta" Runat="server"></asp:PlaceHolder>
			<asp:label ID="lblHata" Runat="server"></asp:label>
		</form>
	</body>
</HTML>
