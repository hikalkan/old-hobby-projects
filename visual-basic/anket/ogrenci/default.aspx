<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="anket._default1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sakarya Üniversitesi Bilgisayar Mühendsiliði Bölümü Anket Sitesi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<p class="DuzYazi" align="center">
			[<a href="../default.aspx?Sayfa=Cikis">Çýkýþ</a>]
			<asp:Label ID="lblGeri" Runat="server"></asp:Label>
		</p>
		<form id="Form1" method="post" runat="server">
			<div id="divAnaSayfa" runat="server">
				<TABLE id="tblOgrenciAyrintisi" cellSpacing="1" cellPadding="1" width="90%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b><asp:Label ID="lblAd" Runat="server"></asp:Label></b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Numara:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblNumara" Runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Doðum 
									Tarihi:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblDogumTarihi" Runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b> E-posta:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblEposta" Runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="center" colspan="2" width="100%" bgColor="#99bbff"><a href='default.aspx?Sayfa=SifreDegistir'><font color="#000000">Þifreni 
									deðiþtir</font></a></td>
					</tr>
				</TABLE>
				<p></p>
				<P>
					<asp:PlaceHolder ID="phOrta" Runat="server"></asp:PlaceHolder><BR>
				</P>
			</div>
			<div id="divSifreDegistir" runat="server">
				<TABLE id="tblDifreDegistir" cellSpacing="1" cellPadding="1" width="50%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b>:: 
									Þifre Deðiþtir ::</b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Eski 
									Þifreniz:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtSifreE" Runat="server" MaxLength="20" Width="100px" TextMode="Password"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Yeni 
									Þifreniz:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtSifreY" Runat="server" MaxLength="20" Width="100px" TextMode="Password"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Yeni 
									Þifreniz (tekrar):</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtSifreYT" Runat="server" MaxLength="20" Width="100px" TextMode="Password"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="center" colspan="2" width="100%" bgColor="#99bbff"><asp:Button ID="btnSifreDegistirTamam" Text="Deðiþtir" Runat="server"></asp:Button></td>
					</tr>
				</TABLE>
			</div>
			<p align="center"><asp:Label id="lblHata" runat="server"></asp:Label></p>
		</form>
	</body>
</HTML>
