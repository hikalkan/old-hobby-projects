<%@ Page Language="vb" AutoEventWireup="false" Codebehind="hocalar.aspx.vb" Inherits="anket.hocalar"%>
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
		<form id="frmHoca" method="post" runat="server">
			<div id="divHocaEklemeFormu" runat="server">
				<TABLE id="tblHocaEklemeFormu" cellSpacing="1" cellPadding="1" width="90%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b>:: 
									Hoca Ekle ::</b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" style="HEIGHT: 26px" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Ünvan:</b></font></td>
						<td class="DuzYazi" style="HEIGHT: 26px" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtUnvan" Width="200px" MaxLength="20" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" style="HEIGHT: 26px" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Ad:</b></font></td>
						<td class="DuzYazi" style="HEIGHT: 26px" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtAd" Width="200px" MaxLength="20" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Soyad:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtSoyad" Width="200px" MaxLength="20" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>E-posta:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtEposta" Width="200px" MaxLength="100" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Doðum 
									Tarihi:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtDogumTarihi" Width="200px" MaxLength="10" Runat="server"></asp:textbox>(gg/aa/yyyy)</td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Kullanýcý 
									Adý:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtKullaniciAdi" Width="200px" MaxLength="10" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" vAlign="top" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Yetki 
									Düzeyi:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtYetkiDuzeyi" Width="50px" MaxLength="1" Runat="server"></asp:textbox><br>
							1: Sadece kendi dersinin anketlerini görebilir.<br>
							2: Tüm anketleri görebilir.<br>
							3: Öðrenci ve Ders ekleme/silme/görme yapabilir.<br>
							4: Tüm yetkiler.
						</td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:button id="btnEkle" Runat="server" Text="Ekle"></asp:button></td>
					</tr>
				</TABLE>
			</div>
			<div id="divHocaAyrintisi" runat="server">
				<TABLE id="tblHocaAyrintisi" cellSpacing="1" cellPadding="1" width="90%" align="center"
					bgColor="#000000">
					<tr>
						<td class="DuzYazi" align="center" width="50%" bgColor="#0099cc" colSpan="2"><font color="#ffffff"><b><asp:Label ID="lblAd" Runat="server"></asp:Label></b></font></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>E-posta:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblEposta" Runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Doðum 
									Tarihi:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblDogumTarihi" Runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Kullanýcý 
									Adý:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblKullaniciAdi" Runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="DuzYazi" vAlign="top" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Yetki 
									Düzeyi:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:Label ID="lblYetkiDuzeyi" Runat="server"></asp:Label>
						</td>
					</tr>
				</TABLE>
			</div>
			<asp:placeholder id="phOrta" runat="server"></asp:placeholder>
			<p class="DuzYazi" align="center"><asp:label id="lblHata" Runat="server"></asp:label></p>
		</form>
	</body>
</HTML>
