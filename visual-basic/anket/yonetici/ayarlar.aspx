<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ayarlar.aspx.vb" Inherits="anket.ayarlar"%>
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
		<form id="Form1" method="post" runat="server">
			<div id="divAyarFormu" runat="server">
				<TABLE id="tblDersEklemeFormu" cellSpacing="1" cellPadding="1" width="90%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b>:: 
									Ayarlar ::</b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" style="HEIGHT: 26px" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Öðrenim 
									Yýlý:</b></font></td>
						<td class="DuzYazi" style="HEIGHT: 26px" align="left" width="50%" bgColor="#99bbff">
							<asp:DropDownList id="ddlOgretimYili" runat="server"></asp:DropDownList></td>
					</tr>
					<tr>
						<td class="DuzYazi" style="HEIGHT: 26px" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Dönem:</b></font></td>
						<td class="DuzYazi" style="HEIGHT: 26px" align="left" width="50%" bgColor="#99bbff">
							<asp:DropDownList id="ddlDonem" runat="server">
								<asp:ListItem Value="1">1. D&#246;nem</asp:ListItem>
								<asp:ListItem Value="2">2. D&#246;nem</asp:ListItem>
								<asp:ListItem Value="3">Yaz Okulu</asp:ListItem>
							</asp:DropDownList></td>
					</tr>
					<tr>
						<td class="DuzYazi" style="HEIGHT: 26px" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Anket 
									Durumu:</b></font></td>
						<td class="DuzYazi" style="HEIGHT: 26px" align="left" width="50%" bgColor="#99bbff">
							<asp:DropDownList id="ddlAnketDurumu" runat="server">
								<asp:ListItem Value="1">A&#231;ýk</asp:ListItem>
								<asp:ListItem Value="0">Kapalý</asp:ListItem>
							</asp:DropDownList></td>
					</tr>
					<tr>
						<td class="DuzYazi" style="HEIGHT: 26px" align="right" width="50%" bgColor="#0099cc"></td>
						<td class="DuzYazi" style="HEIGHT: 26px" align="left" width="50%" bgColor="#0099cc"><asp:Button ID="btnAyarlariKaydet" Text="Deðiþtir" Runat="server"></asp:Button></td>
					</tr>
				</TABLE>
			</div>
			<asp:PlaceHolder ID="phOrta" Runat="server"></asp:PlaceHolder>
			<p align="center" class="DuzYazi"><asp:Label ID="lblHata" Runat="server"></asp:Label></p>
		</form>
	</body>
</HTML>
