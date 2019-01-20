<%@ Page Language="vb" AutoEventWireup="false" Codebehind="dersler.aspx.vb" Inherits="anket.dersler"%>
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
		<p class="DuzYazi" align="center">[<A href="default.aspx">Yönetici Bölümü</A>]
		</p>
		<form id="frmDersler" method="post" runat="server">
			<div id="divDersEklemeFormu" runat="server">
				<TABLE id="tblDersEklemeFormu" cellSpacing="1" cellPadding="1" width="90%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b>:: 
									Ders Ekle ::</b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" style="HEIGHT: 26px" vAlign="top" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Dersi 
									Veren Hoca:</b></font></td>
						<td class="DuzYazi" style="HEIGHT: 26px" align="left" width="50%" bgColor="#99bbff"><asp:listbox id="lbHocalar" runat="server" Width="328px" Height="152px"></asp:listbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" style="HEIGHT: 26px" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Dersin 
									Adý:</b></font></td>
						<td class="DuzYazi" style="HEIGHT: 26px" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtAd" Width="328px" MaxLength="50" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Sýnýf:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:dropdownlist id="ddlSinif" runat="server" Width="72px">
								<asp:ListItem Value="0">Se&#231;..</asp:ListItem>
								<asp:ListItem Value="1">1. Sýnýf</asp:ListItem>
								<asp:ListItem Value="2">2. Sýnýf</asp:ListItem>
								<asp:ListItem Value="3">3. Sýnýf</asp:ListItem>
								<asp:ListItem Value="4">4. Sýnýf</asp:ListItem>
								<asp:ListItem Value="5">Diðer</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Dönem:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:dropdownlist id="ddlDonem" runat="server" Width="72px">
								<asp:ListItem Value="0">Se&#231;</asp:ListItem>
								<asp:ListItem Value="1">1. D&#246;nem</asp:ListItem>
								<asp:ListItem Value="2">2. D&#246;nem</asp:ListItem>
								<asp:ListItem Value="3">Yaz Okulu</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Yýl:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:dropdownlist id="ddlYil" runat="server" Width="104px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:button id="btnEkle" Runat="server" Text="Ekle"></asp:button></td>
					</tr>
				</TABLE>
			</div>
			<div id="divOgrenciSecimi1" runat="server">
				<p class="DuzYazi" align="center">:: Öðretim Yýlýný Seçin ::<br>
					<asp:dropdownlist id="ddlDersYili" Runat="server" AutoPostBack="True"></asp:dropdownlist><br>
					<br>
					:: Dersi Seçin ::<br>
					<asp:listbox id="lstDersler" runat="server" Width="698px" Height="152px"></asp:listbox><br>
					<asp:button id="btnOgrenciSecimi1" Runat="server" Text="Devam et >>"></asp:button></p>
			</div>
			<div id="divDersBilgileri" runat="server">
				<TABLE id="tblOSDersAyrintisi" cellSpacing="1" cellPadding="1" width="90%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b><asp:Label ID="lblOSDersAd" Runat="server"></asp:Label></b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Dersi 
									Veren Hoca:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblOSHocaAd" Runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Sýnýf:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblOSSinif" Runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Dönem:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblOSDonem" Runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Yýl:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblOSYil" Runat="server"></asp:Label></td>
					</tr>
				</TABLE>
			</div>
			<div id="divOgrenciSecimi2" runat="server">
				<p class="DuzYazi" align="center">:: Dersi Alan Öðrencileri Seçin ::
					<asp:checkboxlist id="cblOgrenciler" Width="900px" Height="16px" Runat="server" Font-Names="Verdana"
						Font-Size="X-Small" RepeatColumns="3"></asp:checkboxlist>
					<asp:Button ID="btnOSGuncelle" Runat="Server" Text="Güncelle"></asp:Button>
				</p>
			</div>
			<asp:placeholder id="phOrta" runat="server"></asp:placeholder>
			<p class="DuzYazi" align="center"><asp:label id="lblHata" Runat="server"></asp:label></p>
		</form>
	</body>
</HTML>
