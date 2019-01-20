<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="anket._default"%>
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
		<form id="Form1" method="post" runat="server">
			<div id="divEditor" runat="server">Öðrenci
				<ul>
					<li>
						<A href="ogrenci.aspx?Sayfa=EklemeFormu">Ekle</A>
					<li>
						<A href="ogrenci.aspx?Sayfa=Listele&amp;SayfaNo=1">Listele</A>
					<li>
						<A href="ogrenci.aspx?Sayfa=Mezuniyet">Mezuniyet</A>
					</li>
				</ul>
				Dersler
				<ul>
					<li>
						<A href="dersler.aspx?Sayfa=EklemeFormu">Ekle</A>
					<li>
						<A href="dersler.aspx?Sayfa=Listele">Listele</A>
					<li>
						<A href="dersler.aspx?Sayfa=OgrenciSecimi&amp;Adim=DersSecimi">Dersi alan 
							öðrencileri seç</A>
					</li>
				</ul>
			</div>
			<div id="divYonetici" runat="server">Hoca
				<ul>
					<li>
						<A href="hocalar.aspx?Sayfa=EklemeFormu">Ekle</A>
					<li>
						<A href="hocalar.aspx?Sayfa=Listele">Listele</A>
					</li>
				</ul>
				Anketler
				<ul>
					<li>
						<A href="anket.aspx?Sayfa=Ekle&amp;Adim=1">Ekle</A>
					</li>
					<li>
						<A href="anket2.aspx?Sayfa=Listele">Listele/Sil</A>
					</li>
					<li>
						<A href="anket2.aspx?Sayfa=AnketGor_Liste">Genel Anketlere Bak</A>
					</li>
				</ul>
				Ayarlar
				<ul>
					<li>
						<A href="ayarlar.aspx?Sayfa=Degistir">Deðiþtir</A>
					</li>
				</ul>
			</div>
			Diðer
			<ul>
				<li>
					<A href="../default.aspx?Sayfa=Cikis">Çýkýþ</A>
				</li>
			</ul>
		</form>
	</body>
</HTML>
