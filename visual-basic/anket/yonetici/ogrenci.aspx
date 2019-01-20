<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ogrenci.aspx.vb" Inherits="anket.ogrenci"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sakarya Üniversitesi Bilgisayar Mühendsiliði Bölümü Anket Sitesi</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<p class="DuzYazi" align="center">
			[<a href="default.aspx">Yönetici Bölümü</a>]
		</p>
		<form id="OgrenciFormu" method="post" runat="server">
			<div id="divOgrenciEklemeFormu" runat="server">
				<TABLE id="tblOgrenciEklemeFormu" cellSpacing="1" cellPadding="1" width="90%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b>:: 
									Öðrenci Ekle ::</b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Numara:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtNumara" Runat="server" MaxLength="12" Width="200px"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc" style="HEIGHT: 26px"><font color="#ffffff"><b>Ad:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff" style="HEIGHT: 26px"><asp:TextBox ID="txtAd" Runat="server" MaxLength="20" Width="200px"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Soyad:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtSoyad" Runat="server" MaxLength="20" Width="200px"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>E-posta:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtEposta" Runat="server" MaxLength="100" Width="200px"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Doðum 
									Tarihi:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:TextBox ID="txtDogumTarihi" Runat="server" MaxLength="10" Width="200px"></asp:TextBox>
							(gg/aa/yyyy)</td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Button ID="btnEkle" Runat="server" Text="Ekle"></asp:Button></td>
					</tr>
				</TABLE>
			</div>
			<asp:PlaceHolder ID="phOrta" runat="server"></asp:PlaceHolder>
			<p align="center">
				<asp:CheckBoxList id="cblOgrenciler" runat="server" Width="800px" RepeatColumns="3" Visible="False"
					Font-Names="Verdana" Font-Size="12px"></asp:CheckBoxList></p>
			<p align="center">
				<asp:Button id="btnTamam" runat="server" Text="Tamam" Visible="False"></asp:Button>
			</p>
			<p align="center" class="DuzYazi">
				<asp:label id="lblHata" Runat="server"></asp:label></p>
		</form>
	</body>
</HTML>
