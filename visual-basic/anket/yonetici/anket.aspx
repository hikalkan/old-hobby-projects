<%@ Page Language="vb" AutoEventWireup="false" Codebehind="anket.aspx.vb" Inherits="anket.anket"%>
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
		<form id="frmAnket" method="post" runat="server">
			<div id="divAdim1" runat="server">
				<TABLE id="tblAnketBilgileri" cellSpacing="1" cellPadding="1" width="90%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b>:: 
									Anket Ekle ::</b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Açýklama:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtAciklama" MaxLength="255" Width="200" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Soru 
									Sayýsý:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtSoruSayisi" MaxLength="2" Width="40px" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Bir 
									soru için azami seçenek sayýsý:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtSecenekSayisi" MaxLength="1" Width="40px" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Yýl:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:dropdownlist id="ddlYil" runat="server" Width="88px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Dönem:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:dropdownlist id="ddlDonem" runat="server" Width="88px">
								<asp:ListItem Value="0" Selected="True">Se&#231;..</asp:ListItem>
								<asp:ListItem Value="1">1. D&#246;nem</asp:ListItem>
								<asp:ListItem Value="2">2. D&#246;nem</asp:ListItem>
								<asp:ListItem Value="3">Yaz Okulu</asp:ListItem>
							</asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b></b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:button id="btn1Devam" Runat="server" Text="Devam >>"></asp:button></td>
					</tr>
				</TABLE>
				<p class="Girintili"><b><u>Açýklamalar</u>:</b> <STRONG>'Açýklama'</STRONG> alanýna 
					girdiðiniz metin anketin en baþýnda gözükecektir. Buraya kýsa bir açýklama 
					metni girebilirsiniz. <STRONG>'Soru Sayýsý'</STRONG> kýsmýna ankette olacak 
					soru sayýsýný girmelisiniz. Bu sayý 1 ile 99 arasýnda olabilir. Bir sonraki 
					sayfada girdiðiniz sayý kadar soru girme alaný gelecektir. Bu da demek oluyor 
					ki anketi daha önceden planlamalý ve tam olarak hazýrladýktan sonra siteye 
					kaydetmelisiniz. <STRONG>'Bir soru içim azami seçenek sayýsý'</STRONG> alanýna 
					hazýrladýðýnýz ankette en fazla seçenek sayýsýna sahip sorunun seçenek sayýsýný 
					girin. Bu sayý 1 ile 9 arasýnda olabilir. <STRONG>'Yýl' </STRONG>alanýna bu 
					anketin geçerli olduðu yýlý seçiniz. Bu yýl her sene için Güz döneminin 
					baþladýðý yýl olmadýlýr ve Güz, Bahar ve Yaz okulu dönemleri için ayný 
					olmalýdýr.<STRONG> 'Dönem'</STRONG> kýsmýna bu anketin geçerli olduðu dönemi 
					girin. Bu anket girdiðiniz dönemin dersleri için geçerli olacaktýr.</p>
			</div>
			<asp:placeholder id="phOrta" Runat="server"></asp:placeholder>
			<p align="center"><asp:button id="btnAnketiTamamla" runat="server" Text="Tamamla" Visible="False"></asp:button></p>
			<asp:label id="lblHata" runat="server"></asp:label></form>
	</body>
</HTML>
