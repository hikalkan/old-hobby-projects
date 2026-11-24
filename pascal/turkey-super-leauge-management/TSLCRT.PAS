{Free Pascal / Windows icin Turbo Pascal CRT biriminin yerine gecen kucuk birim.
 FPC'nin kendi CRT birimi DOS Turkce kod sayfasindaki (CP857) karakterleri
 guncel Windows konsolunda bozuk bastigi icin yazildi: karakterler Unicode'a
 cevrilip konsolun Unicode API'siyle yazilir, klavyeden gelen Unicode
 karakterler de CP857'ye cevrilir. Ekranin sol ust 80x25'lik bolumu kullanilir.}
unit TSLCRT;
{$MODE FPC}
interface
const
Black=0;Blue=1;Green=2;Cyan=3;Red=4;Magenta=5;Brown=6;LightGray=7;
DarkGray=8;LightBlue=9;LightGreen=10;LightCyan=11;LightRed=12;
LightMagenta=13;Yellow=14;White=15;Blink=128;
var
TextAttr:byte;
LastMode:word;
procedure ClrScr;
procedure ClrEol;
procedure GotoXY(x,y:byte);
function WhereX:byte;
function WhereY:byte;
procedure Window(x1,y1,x2,y2:byte);
procedure TextColor(c:byte);
procedure TextBackground(c:byte);
procedure LowVideo;
procedure HighVideo;
procedure NormVideo;
function ReadKey:char;
function KeyPressed:boolean;
procedure Delay(ms:word);
procedure CursorOn;
procedure CursorOff;
procedure CursorBig;
implementation
uses windows;
const
{CP857 (DOS Turkce) 128..255 -> Unicode}
CP857:array[128..255] of word=(
$00C7,$00FC,$00E9,$00E2,$00E4,$00E0,$00E5,$00E7,$00EA,$00EB,$00E8,$00EF,$00EE,$0131,$00C4,$00C5,
$00C9,$00E6,$00C6,$00F4,$00F6,$00F2,$00FB,$00F9,$0130,$00D6,$00DC,$00F8,$00A3,$00D8,$015E,$015F,
$00E1,$00ED,$00F3,$00FA,$00F1,$00D1,$011E,$011F,$00BF,$00AE,$00AC,$00BD,$00BC,$00A1,$00AB,$00BB,
$2591,$2592,$2593,$2502,$2524,$00C1,$00C2,$00C0,$00A9,$2563,$2551,$2557,$255D,$00A2,$00A5,$2510,
$2514,$2534,$252C,$251C,$2500,$253C,$00E3,$00C3,$255A,$2554,$2569,$2566,$2560,$2550,$256C,$00A4,
$00BA,$00AA,$00CA,$00CB,$00C8,$20AC,$00CD,$00CE,$00CF,$2518,$250C,$2588,$2584,$00A6,$00CC,$2580,
$00D3,$00DF,$00D4,$00D2,$00F5,$00D5,$00B5,$0020,$00D7,$00DA,$00DB,$00D9,$00EC,$00FF,$00AF,$00B4,
$00AD,$00B1,$0020,$00BE,$00B6,$00A7,$00F7,$00B8,$00B0,$00A8,$00B7,$00B9,$00B3,$00B2,$25A0,$00A0);
var
hOut,hIn:THandle;
OrgX,OrgY:longint;{Kullanilan 80x25 alanin konsol tamponundaki sol ust kosesi}
ScrW,ScrH:longint;
WinX1,WinY1,WinX2,WinY2:longint;{Aktif pencere (ekrana gore, 1 tabanli)}
CurX,CurY:longint;{Imlec (ekrana gore, 1 tabanli)}
Bekleyen:char;{Genisletilmis tusun ikinci bayti}

function Koord(x,y:longint):COORD;
begin
Koord.X:=OrgX+x-1;Koord.Y:=OrgY+y-1;
end;

procedure ImlecKoy;
begin
SetConsoleCursorPosition(hOut,Koord(CurX,CurY));
end;

procedure SatirDoldur(x,y,n:longint);
var w:DWORD;
begin
if n<=0 then exit;
FillConsoleOutputCharacterW(hOut,' ',n,Koord(x,y),w);
FillConsoleOutputAttribute(hOut,TextAttr,n,Koord(x,y),w);
end;

procedure Kaydir;{Aktif pencereyi bir satir yukari kaydirir}
var r,kirp:SMALL_RECT;f:CHAR_INFO;
begin
if WinY2>WinY1 then begin
r.Left:=OrgX+WinX1-1;r.Right:=OrgX+WinX2-1;
r.Top:=OrgY+WinY1;r.Bottom:=OrgY+WinY2-1;
kirp:=r;kirp.Top:=OrgY+WinY1-1;
f.UnicodeChar:=' ';f.Attributes:=TextAttr;
ScrollConsoleScreenBufferW(hOut,r,kirp,Koord(WinX1,WinY1),f);
end;
SatirDoldur(WinX1,WinY2,WinX2-WinX1+1);
end;

procedure SatirAtla;
begin
if CurY>=WinY2 then Kaydir else inc(CurY);
end;

procedure CrtWrite(var f:TextRec);
var
i:longint;
c:char;
dizi:array[0..255] of WideChar;
du,dx:longint;
procedure Bas;
var w:DWORD;
begin
if du=0 then exit;
WriteConsoleOutputCharacterW(hOut,@dizi[0],du,Koord(dx,CurY),w);
FillConsoleOutputAttribute(hOut,TextAttr,du,Koord(dx,CurY),w);
du:=0;
end;
begin
du:=0;dx:=CurX;
for i:=0 to longint(f.BufPos)-1 do begin
c:=f.BufPtr^[i];
case c of
#7:;
#8:begin Bas;if CurX>WinX1 then dec(CurX);end;
#10:begin Bas;SatirAtla;end;
#13:begin Bas;CurX:=WinX1;end;
else begin
if du=0 then dx:=CurX;
if ord(c)>=128 then dizi[du]:=WideChar(CP857[ord(c)]) else dizi[du]:=WideChar(ord(c));
inc(du);inc(CurX);
if (CurX>WinX2) or (du>255) then begin
Bas;
if CurX>WinX2 then begin CurX:=WinX1;SatirAtla;end;
end;
end;
end;
end;
Bas;
f.BufPos:=0;
ImlecKoy;
end;

procedure CrtClose(var f:TextRec);
begin
f.Mode:=fmClosed;
end;

procedure CrtOpen(var f:TextRec);
begin
f.InOutFunc:=@CrtWrite;
f.FlushFunc:=@CrtWrite;
f.CloseFunc:=@CrtClose;
end;

procedure ClrScr;
var y:longint;
begin
for y:=WinY1 to WinY2 do SatirDoldur(WinX1,y,WinX2-WinX1+1);
CurX:=WinX1;CurY:=WinY1;
ImlecKoy;
end;

procedure ClrEol;
begin
SatirDoldur(CurX,CurY,WinX2-CurX+1);
end;

procedure GotoXY(x,y:byte);
begin
if (x<1) or (y<1) or (x>WinX2-WinX1+1) or (y>WinY2-WinY1+1) then exit;
CurX:=WinX1+x-1;CurY:=WinY1+y-1;
ImlecKoy;
end;

function WhereX:byte;
begin
WhereX:=CurX-WinX1+1;
end;

function WhereY:byte;
begin
WhereY:=CurY-WinY1+1;
end;

procedure Window(x1,y1,x2,y2:byte);
begin
if (x1<1) or (y1<1) or (x2>ScrW) or (y2>ScrH) or (x1>x2) or (y1>y2) then exit;
WinX1:=x1;WinY1:=y1;WinX2:=x2;WinY2:=y2;
CurX:=WinX1;CurY:=WinY1;
ImlecKoy;
end;

procedure TextColor(c:byte);
begin
TextAttr:=(TextAttr and $70) or (c and $0F);
end;

procedure TextBackground(c:byte);
begin
TextAttr:=(TextAttr and $0F) or ((c and 7) shl 4);
end;

procedure LowVideo;
begin
TextAttr:=TextAttr and $F7;
end;

procedure HighVideo;
begin
TextAttr:=TextAttr or 8;
end;

procedure NormVideo;
begin
TextAttr:=LightGray;
end;

{Bir konsol olayini DOS tusuna cevirir. Sonuc: 0=tus degil, 1=normal karakter (k),
 2=genisletilmis tus (k=tarama kodu)}
function TusCoz(var r:INPUT_RECORD;var k:char):longint;
var u,vk:word;i:longint;
begin
TusCoz:=0;
if r.EventType<>KEY_EVENT then exit;
if not r.Event.KeyEvent.bKeyDown then exit;
u:=word(r.Event.KeyEvent.UnicodeChar);
vk:=r.Event.KeyEvent.wVirtualKeyCode;
if u=0 then begin
case vk of
33:k:=#73;{PgUp}
34:k:=#81;{PgDn}
35:k:=#79;{End}
36:k:=#71;{Home}
37:k:=#75;{Sol}
38:k:=#72;{Yukari}
39:k:=#77;{Sag}
40:k:=#80;{Asagi}
45:k:=#82;{Ins}
46:k:=#83;{Del}
112..121:k:=chr(59+vk-112);{F1..F10}
else exit;
end;
TusCoz:=2;
exit;
end;
if u<128 then begin k:=chr(u);TusCoz:=1;exit;end;
for i:=128 to 255 do if CP857[i]=u then begin k:=chr(i);TusCoz:=1;exit;end;
end;

function ReadKey:char;
var r:INPUT_RECORD;n:DWORD;k:char;
begin
if Bekleyen<>#0 then begin ReadKey:=Bekleyen;Bekleyen:=#0;exit;end;
repeat
if not ReadConsoleInputW(hIn,r,1,n) then begin ReadKey:=#27;exit;end;
if n=1 then
case TusCoz(r,k) of
1:begin ReadKey:=k;exit;end;
2:begin Bekleyen:=k;ReadKey:=#0;exit;end;
end;
until false;
end;

function KeyPressed:boolean;
var r:INPUT_RECORD;n:DWORD;k:char;
begin
KeyPressed:=true;
if Bekleyen<>#0 then exit;
repeat
if (not PeekConsoleInputW(hIn,r,1,n)) or (n=0) then begin KeyPressed:=false;exit;end;
if TusCoz(r,k)<>0 then exit;
ReadConsoleInputW(hIn,r,1,n);{Tus olmayan olayi at}
until false;
end;

procedure Delay(ms:word);
begin
Sleep(ms);
end;

procedure ImlecAyarla(boyut:longint;gorunur:boolean);
var ci:CONSOLE_CURSOR_INFO;
begin
ci.dwSize:=boyut;ci.bVisible:=gorunur;
SetConsoleCursorInfo(hOut,ci);
end;

procedure CursorOn;
begin
ImlecAyarla(15,true);
end;

procedure CursorOff;
begin
ImlecAyarla(15,false);
end;

procedure CursorBig;
begin
ImlecAyarla(99,true);
end;

procedure Baslat;
var bi:CONSOLE_SCREEN_BUFFER_INFO;y,w,h:longint;n:DWORD;c:COORD;
begin
hOut:=GetStdHandle(STD_OUTPUT_HANDLE);
hIn:=GetStdHandle(STD_INPUT_HANDLE);
Bekleyen:=#0;LastMode:=3;TextAttr:=LightGray;
OrgX:=0;OrgY:=0;w:=80;h:=25;
if GetConsoleScreenBufferInfo(hOut,bi) then begin
OrgX:=bi.srWindow.Left;OrgY:=bi.srWindow.Top;
w:=bi.srWindow.Right-bi.srWindow.Left+1;
h:=bi.srWindow.Bottom-bi.srWindow.Top+1;
{Gorunen konsolun tamamini temizle}
for y:=0 to h-1 do begin
c.X:=OrgX;c.Y:=OrgY+y;
FillConsoleOutputCharacterW(hOut,' ',w,c,n);
FillConsoleOutputAttribute(hOut,LightGray,w,c,n);
end;
end;
if w>80 then ScrW:=80 else ScrW:=w;
if h>25 then ScrH:=25 else ScrH:=h;
WinX1:=1;WinY1:=1;WinX2:=ScrW;WinY2:=ScrH;
CurX:=1;CurY:=1;
ImlecKoy;
Assign(Output,'');
TextRec(Output).OpenFunc:=@CrtOpen;
Rewrite(Output);
end;

begin
Baslat;
end.
