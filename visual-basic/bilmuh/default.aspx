<%@ Page Language="vb" AutoEventWireup="false" Codebehind="default.aspx.vb" Inherits="bilmuh.WebForm1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sakarya Üniversitesi Bilgisayar Kulübü</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/javascript">
<!--

//Genel Deðiþkenler
var monitorSolu=0; // Monitörün en solunun x koordinatý
var monitorTepesi=10; // Monitörün en tepesinin y koordinatý
var menuSolu=0; // Menünün en solunun x koordinatý
var menuTepesi=0; // Menünün en tepesinin y koordinatý
var xpos = 0; // Mouse'un x koordinatý
var ypos = 0; // Mouse'un y koordinatý
var IE = document.all?true:false;

if (!IE) document.captureEvents(Event.MOUSEMOVE)
document.onmousemove = getMouseXY;

function getMouseXY(e) {
	if (IE) { // grab the x-y pos.s if browser is IE
		xpos = event.clientX + document.body.scrollLeft;
		ypos = event.clientY + document.body.scrollTop;
	} else {  // grab the x-y pos.s if browser is NS
		xpos = e.pageX;
		ypos = e.pageY;
	}
	if (xpos < 0) {xpos = 0;}
	if (ypos < 0) {ypos = 0;} 
	menuGizle();
}

function onAyarlamalar()
{
	var iw, ih; // inner width ve inner height
	// Ekranýn geniþlik ve Yüksekliði alýnýyor
	if (window.innerWidth == null) {
		iw = document.body.clientWidth;
		ih=document.body.clientHeight; 
	} else {
		iw = window.innerWidth;
		ih = window.innerHeight;
	}
	// Monitörün ve Menünün koordinatlarý hesaplanýyor
	monitorSolu=(iw-680)/2;
	menuTepesi=monitorTepesi+106;
	menuSolu=monitorSolu+23;
	if(monitorSolu<0)
	{
		monitorSolu=0;
	}
	
	// Stil özellikleri uygulanýyor
	divMonitor.style.position='absolute';
	divMonitor.style.left=monitorSolu;
	divMonitor.style.top=monitorTepesi;
	divMenu.style.position='absolute';
	divMenu.style.top=menuTepesi;
	divMenu.style.left=menuSolu;
}

function newImage(arg) {
	if (document.images) {
		rslt = new Image();
		rslt.src = arg;
		return rslt;
	}
}

function changeImages() {
	if (document.images && (preloadFlag == true)) {
		for (var i=0; i<changeImages.arguments.length; i+=2) {
			document[changeImages.arguments[i]].src = changeImages.arguments[i+1];
		}
	}
}

var preloadFlag = false;
function preloadImages() {
	onAyarlamalar();
	if (document.images) {
		menu_02_over = newImage("images/menu_02-over.gif");
		menu_03_over = newImage("images/menu_03-over.gif");
		menu_04_over = newImage("images/menu_04-over.gif");
		menu_05_over = newImage("images/menu_05-over.gif");
		menu_06_over = newImage("images/menu_06-over.gif");
		menu_07_over = newImage("images/menu_07-over.gif");
		menu_09_over = newImage("images/menu_09-over.gif");
		preloadFlag = true;
	}
}

function menuGoster()
{
	if(divMenu.style.visibility=='visible')
	{
		divMenu.style.visibility='hidden';
	} else {
		divMenu.style.visibility='visible';
	}
}

function menuGizle()
{
	if(!((menuSolu<=xpos) && (menuSolu+190>=xpos) && (menuTepesi<=ypos) && (menuTepesi+305>=ypos)))
	{
		divMenu.style.visibility='hidden';
	}
}

function menuGizle_Dogrudan()
{
	divMenu.style.visibility='hidden';
}
// -->
		</script>
	</HEAD>
	<body BGCOLOR="#ffffff" LEFTMARGIN="0" TOPMARGIN="0" MARGINWIDTH="0" MARGINHEIGHT="0"
		onLoad="preloadImages()">
		<div id="divMonitor" style="Z-INDEX:1;VISIBILITY:visible;POSITION:absolute">
			<!-- ImageReady Slices (ana.psd) -->
			<TABLE ID="monitorTablosu" WIDTH="680" HEIGHT="541" BORDER="0" CELLPADDING="0" CELLSPACING="0">
				<TR>
					<TD>
						<IMG SRC="images/spacer.gif" WIDTH="22" HEIGHT="1" ALT=""></TD>
					<TD>
						<IMG SRC="images/spacer.gif" WIDTH="70" HEIGHT="1" ALT=""></TD>
					<TD>
						<IMG SRC="images/spacer.gif" WIDTH="551" HEIGHT="1" ALT=""></TD>
					<TD>
						<IMG SRC="images/spacer.gif" WIDTH="15" HEIGHT="1" ALT=""></TD>
					<TD>
						<IMG SRC="images/spacer.gif" WIDTH="22" HEIGHT="1" ALT=""></TD>
				</TR>
				<TR>
					<TD COLSPAN="5">
						<IMG SRC="images/ana_01.gif" WIDTH="680" HEIGHT="22" ALT=""></TD>
				</TR>
				<TR>
					<TD COLSPAN="3">
						<IMG SRC="images/ana_02.gif" WIDTH="643" HEIGHT="16" ALT=""></TD>
					<TD><a href="anasayfa.aspx" target="frmIcerik"><IMG SRC="images/ana_03.gif" border="0" WIDTH="15" HEIGHT="16" ALT=""></a></TD>
					<TD>
						<IMG SRC="images/ana_04.gif" WIDTH="22" HEIGHT="16" ALT=""></TD>
				</TR>
				<TR>
					<TD ROWSPAN="3">
						<IMG SRC="images/ana_05.gif" WIDTH="22" HEIGHT="502" ALT=""></TD>
					<TD COLSPAN="3" WIDTH="636" HEIGHT="351">
						<div id="diviFrame" style="Z-INDEX:2;VISIBILITY:visible">
							<iframe name="frmIcerik" width="636" height="351" src="anasayfa.aspx" scroolbars="0">
							</iframe>
						</div>
					</TD>
					<TD ROWSPAN="3">
						<IMG SRC="images/ana_07.gif" WIDTH="22" HEIGHT="502" ALT=""></TD>
				</TR>
				<TR>
					<TD><a href="#" onClick="menuGoster()"><IMG SRC="images/ana_08.gif" border="0" WIDTH="70" HEIGHT="18" ALT=""></a></TD>
					<TD COLSPAN="2">
						<IMG SRC="images/ana_09.gif" WIDTH="566" HEIGHT="18" ALT=""></TD>
				</TR>
				<TR>
					<TD COLSPAN="3">
						<IMG SRC="images/ana_10.gif" WIDTH="636" HEIGHT="133" ALT=""></TD>
				</TR>
			</TABLE>
			<!-- End ImageReady Slices -->
		</div>
		<div id="divMenu" style="Z-INDEX:2;VISIBILITY:hidden;POSITION:absolute">
			<!-- ImageReady Slices (menu.psd) -->
			<table id="menuTablosu" width="190" height="283" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td rowspan="8">
						<img src="images/menu_01.gif" width="27" height="283" alt=""></td>
					<td colspan="2">
						<a href="seminer.aspx?Sayfa=Listele" onClick="menuGizle_Dogrudan()" target="frmIcerik" onmouseover="changeImages('menu_02', 'images/menu_02-over.gif'); return true;"
							onmouseout="changeImages('menu_02', 'images/menu_02.gif'); return true;" onmousedown="changeImages('menu_02', 'images/menu_02-over.gif'); return true;"
							onmouseup="changeImages('menu_02', 'images/menu_02-over.gif'); return true;"><img name="menu_02" src="images/menu_02.gif" width="163" height="41" border="0" alt=""></a></td>
				</tr>
				<tr>
					<td colspan="2">
						<a href="kurslar.aspx?Sayfa=Listele" onClick="menuGizle_Dogrudan()" target="frmIcerik" onmouseover="changeImages('menu_03', 'images/menu_03-over.gif'); return true;"
							onmouseout="changeImages('menu_03', 'images/menu_03.gif'); return true;" onmousedown="changeImages('menu_03', 'images/menu_03-over.gif'); return true;"
							onmouseup="changeImages('menu_03', 'images/menu_03-over.gif'); return true;"><img name="menu_03" src="images/menu_03.gif" width="163" height="38" border="0" alt=""></a></td>
				</tr>
				<tr>
					<td colspan="2">
						<a href="haberler.aspx?Sayfa=Listele" onClick="menuGizle_Dogrudan()" target="frmIcerik"
							onmouseover="changeImages('menu_04', 'images/menu_04-over.gif'); return true;"
							onmouseout="changeImages('menu_04', 'images/menu_04.gif'); return true;" onmousedown="changeImages('menu_04', 'images/menu_04-over.gif'); return true;"
							onmouseup="changeImages('menu_04', 'images/menu_04-over.gif'); return true;"><img name="menu_04" src="images/menu_04.gif" width="163" height="40" border="0" alt=""></a></td>
				</tr>
				<tr>
					<td colspan="2">
						<a href="makale.aspx?Sayfa=Kategoriler" onClick="menuGizle_Dogrudan()" target="frmIcerik"
							onmouseover="changeImages('menu_05', 'images/menu_05-over.gif'); return true;"
							onmouseout="changeImages('menu_05', 'images/menu_05.gif'); return true;" onmousedown="changeImages('menu_05', 'images/menu_05-over.gif'); return true;"
							onmouseup="changeImages('menu_05', 'images/menu_05-over.gif'); return true;"><img name="menu_05" src="images/menu_05.gif" width="163" height="39" border="0" alt=""></a></td>
				</tr>
				<tr>
					<td colspan="2">
						<a href="forum.aspx?Sayfa=Kategoriler" onClick="menuGizle_Dogrudan()" target="frmIcerik" onmouseover="changeImages('menu_06', 'images/menu_06-over.gif'); return true;"
							onmouseout="changeImages('menu_06', 'images/menu_06.gif'); return true;" onmousedown="changeImages('menu_06', 'images/menu_06-over.gif'); return true;"
							onmouseup="changeImages('menu_06', 'images/menu_06-over.gif'); return true;"><img name="menu_06" src="images/menu_06.gif" width="163" height="39" border="0" alt=""></a></td>
				</tr>
				<tr>
					<td colspan="2">
						<a href="kulup.aspx" onClick="menuGizle_Dogrudan()" target="frmIcerik" onmouseover="changeImages('menu_07', 'images/menu_07-over.gif'); return true;"
							onmouseout="changeImages('menu_07', 'images/menu_07.gif'); return true;" onmousedown="changeImages('menu_07', 'images/menu_07-over.gif'); return true;"
							onmouseup="changeImages('menu_07', 'images/menu_07-over.gif'); return true;"><img name="menu_07" src="images/menu_07.gif" width="163" height="39" border="0" alt=""></a></td>
				</tr>
				<tr>
					<td colspan="2">
						<img src="images/menu_08.gif" width="163" height="7" alt=""></td>
				</tr>
				<tr>
					<td>
						<a href="zdefteri.aspx?Sayfa=Oku" onClick="menuGizle_Dogrudan()" target="frmIcerik" onmouseover="changeImages('menu_09', 'images/menu_09-over.gif'); return true;"
							onmouseout="changeImages('menu_09', 'images/menu_09.gif'); return true;" onmousedown="changeImages('menu_09', 'images/menu_09-over.gif'); return true;"
							onmouseup="changeImages('menu_09', 'images/menu_09-over.gif'); return true;"><img name="menu_09" src="images/menu_09.gif" width="162" height="40" border="0" alt=""></a></td>
					<td>
						<img src="images/menu_10.gif" width="1" height="40" alt=""></td>
				</tr>
			</table>
			<!-- End ImageReady Slices -->
		</div>
	</body>
</HTML>
