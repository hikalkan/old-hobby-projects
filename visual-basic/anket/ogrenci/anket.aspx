<%@ Page Language="vb" AutoEventWireup="false" Codebehind="anket.aspx.vb" Inherits="anket.anket1"  validateRequest=false %>
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
			<asp:placeholder id="phOrta" Runat="server"></asp:placeholder>
			<p align="center"><asp:button id="btnAnketTamam" Runat="server" Text="Tamam"></asp:button>
				<asp:Button id="btnAnketTamam_Genel" runat="server" Text="Tamam"></asp:Button></p>
			<P align="center"><asp:label id="lblHata" runat="server"></asp:label></P>
		</form>
	</body>
</HTML>
