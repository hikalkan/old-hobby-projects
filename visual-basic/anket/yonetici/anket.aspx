<%@ Page Language="vb" AutoEventWireup="false" Codebehind="anket.aspx.vb" Inherits="anket.anket"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sakarya �niversitesi Bilgisayar M�hendsili�i B�l�m� Anket Sitesi</title>
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
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>A��klama:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtAciklama" MaxLength="255" Width="200" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Soru 
									Say�s�:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtSoruSayisi" MaxLength="2" Width="40px" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Bir 
									soru i�in azami se�enek say�s�:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:textbox id="txtSecenekSayisi" MaxLength="1" Width="40px" Runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>Y�l:</b></font></td>
						<td class="DuzYazi" align="left" width="50%" bgColor="#99bbff"><asp:dropdownlist id="ddlYil" runat="server" Width="88px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td class="DuzYazi" align="right" width="50%" bgColor="#0099cc"><font color="#ffffff"><b>D�nem:</b></font></td>
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
				<p class="Girintili"><b><u>A��klamalar</u>:</b> <STRONG>'A��klama'</STRONG> alan�na 
					girdi�iniz metin anketin en ba��nda g�z�kecektir. Buraya k�sa bir a��klama 
					metni girebilirsiniz. <STRONG>'Soru Say�s�'</STRONG> k�sm�na ankette olacak 
					soru say�s�n� girmelisiniz. Bu say� 1 ile 99 aras�nda olabilir. Bir sonraki 
					sayfada girdi�iniz say� kadar soru girme alan� gelecektir. Bu da demek oluyor 
					ki anketi daha �nceden planlamal� ve tam olarak haz�rlad�ktan sonra siteye 
					kaydetmelisiniz. <STRONG>'Bir soru i�im azami se�enek say�s�'</STRONG> alan�na 
					haz�rlad���n�z ankette en fazla se�enek say�s�na sahip sorunun se�enek say�s�n� 
					girin. Bu say� 1 ile 9 aras�nda olabilir. <STRONG>'Y�l' </STRONG>alan�na bu 
					anketin ge�erli oldu�u y�l� se�iniz. Bu y�l her sene i�in G�z d�neminin 
					ba�lad��� y�l olmad�l�r ve G�z, Bahar ve Yaz okulu d�nemleri i�in ayn� 
					olmal�d�r.<STRONG> 'D�nem'</STRONG> k�sm�na bu anketin ge�erli oldu�u d�nemi 
					girin. Bu anket girdi�iniz d�nemin dersleri i�in ge�erli olacakt�r.</p>
			</div>
			<asp:placeholder id="phOrta" Runat="server"></asp:placeholder>
			<p align="center"><asp:button id="btnAnketiTamamla" runat="server" Text="Tamamla" Visible="False"></asp:button></p>
			<asp:label id="lblHata" runat="server"></asp:label></form>
	</body>
</HTML>
