{Bu programç yapmaya 25.01.2002 Tarihide Baüladçm}
PROGRAM FTSL;{FifaCmTr TÅrkiye SÅper Ligi Ana Program}
{$IFDEF FPC}{$MODE TP}{$PACKRECORDS 1}{$ENDIF}
uses {$IFDEF FPC}wincrt,tslcrt{$ELSE}crt{$ENDIF},dos,tslunit,tslu1,graph;
type
DEFAULT_DOSYA_TiPi=RECORD
TI:STRING[20];{TAKIM òSòMLERò}
ts:string[35];{Takçmçn Satdçnçn ismi}
TR:ARRAY[1..2] OF BYTE;{Renkleri}
TFS:BYTE;{TAKIMDAKò FUTBOLCU SAYISI}
TSG:BYTE;{TAKIMIN SABòT GöCö(10..100)}
TP:LONGINT;{TAKIMIN PARASI}
FI:ARRAY[1..18] OF STRING[20];{FUTBOLCUNUN òSMò}
FM:ARRAY[1..18] OF CHAR;{FUTBOLCUNUN MEVKòò}
FP:ARRAY[1..18] OF REAL;{FUTBOLCUNUN PERFORMANSI}
END;
AKTiF_DOSYA_TiPi=RECORD
TI:STRING[20];{TAKIM òSòMLERò}
TS:string[35];{Takçmçn Stadçnçn ismi}
TR:ARRAY[1..2] OF BYTE;{Renkleri}
TFS:BYTE;{TAKIMDAKò FUTBOLCU SAYISI}
TSG:BYTE;{TAKIMIN SABòT GöCö(10..100)}
TP:LONGINT;{TAKIMIN PARASI(EN FAZLA 100.000.000 $)}
FI:ARRAY[1..18] OF STRING[20];{FUTBOLCUNUN òSMò}
FM:ARRAY[1..18] OF CHAR;{FUTBOLCUNUN MEVKòò}
FP:ARRAY[1..18] OF REAL;{FUTBOLCUNUN PERFORMANSI}
fpi:array[1..18] of real;{Futbolcularçn ilk performanslarç}
DURUM:ARRAY[1..7] OF BYTE;{Takçmçn Ligdeki durumu
1-Sçra, 2-Galibiyet, 3-Beraberlik, 4-Malubiyet, 5-Attçßç, 6-Yedißi, 7-Puanç}
END;
dosya_kaydetme_tipi=record
koi:string;{Kaydedilen oyunun ismi}
kdi:string;{Takçm bilgilerinin saklandçßç dosya ismi}
st:byte;{Seáilen ve Yînetilen Takçmçn no'su}
oh:byte;{Oynanan hafta}
db:longint;{Dakika Birimi}
sts:array[1..3] of byte;{Takçmçn Sistemi}
tops:array[1..34,1..9,1..2] of byte;{Toplu Sonuálar}
fis:array[1..18,1..18,1..6] of byte;{Futbolcu òStatislikleri}
takimim11:array[1..11] of byte;{Seáilen takçmçn aktif 11'i}
yil:longint;{aktif sezon}
otokayit:byte;{otomatik kayçt durumu}
tlk:array[1..18,1..34] of byte;{Takçmlarçn Ligdeki Konumu}
end;
var
DOSYA:FILE OF AKTIF_DOSYA_TIPI;
KAYIT:aktif_DOSYA_TIPI;
menu:menu_tipi;
secim:byte;
takimim:byte;{Seáilen Takçmçn No'su}
oynananhafta:byte;
takimlar:array[1..18] of string[20];{Sabit Kalacak Deßiüken}
takim:array[1..18] of string[20];{Sonuálara Gîre Deßiüecek deßiüken}
durum:array[1..18,1..7] of byte;{Sonuálara gîre deßiüecek}
takimim11:array[1..11] of byte;{Seáilen takçmçn aktif 11'i}
fis:array[1..18,1..18,1..6] of byte;
{fis iáin 1=Futbolcunun oynadçßç maá sayçsç 2=Attçßç gol sayçsç
3=Asist sayçsç,4=Sarç Kart sayçsç,5=Kçrmçzç kart sayçsç
6=Varsa oynayamayacaßç hafta sayçsç}
tops:array[1..34,1..9,1..2] of byte;{Toplu Sonuálar}
sistemim:array[1..3] of byte;
dakikabirimi:byte;
gucsir:array[1..18] of byte;
ot:byte;{Oyun Tipi. Yeni oyun=1 Oyun YÅkleme=2}
aoa:string;on:byte;
otokayit:byte;
yil:longint;
tlk:array[1..18,1..34] of byte;
yenisezonyapildi:boolean;
PROCEDURE OPENGRAPH;
VAR
GD,GM:INTEGER;
BEGIN
{$IFDEF FPC}GD:=VGA;GM:=VGAHI;{$ELSE}GD:=DETECT;{$ENDIF}
INITGRAPH(GD,GM,'');
END;
procedure doku(a:byte);{Dosya OKU}
begin
seek(dosya,a);read(dosya,kayit);
end;
procedure dyaz(a:byte);{Dosya YAZ}
begin
seek(dosya,a);write(dosya,kayit);
end;
procedure takimlari_al;
var a,b:byte;
begin
for a:=1 to 18 do begin
doku(a);
takimlar[a]:=kayit.ti;
takim[a]:=kayit.ti;
end;
end;
procedure takimlari_sirala;
var a,b:byte;
tis:string[20];
dogru:boolean;
procedure degistir(var ilk,ikinci:byte);
var d:byte;
begin
d:=ilk;ilk:=ikinci;ikinci:=d;
end;
begin
{Gol sayçsçna gîre sçralançyor}
repeat
dogru:=true;
for a:=1 to 17 do begin
if (durum[a,5]) < (durum[a+1,5]) then begin
for b:=1 to 7 do degistir(durum[a,b],durum[a+1,b]);
tis:=takim[a];takim[a]:=takim[a+1];takim[a+1]:=tis;
dogru:=false
end;{if (durum}
end;{for a}
until dogru;
{Avaraja gîre sçralama yapçlçyor}
repeat
dogru:=true;
for a:=1 to 17 do begin
if (durum[a,5]-durum[a,6]) < (durum[a+1,5]-durum[a+1,6]) then begin
for b:=1 to 7 do degistir(durum[a,b],durum[a+1,b]);
tis:=takim[a];takim[a]:=takim[a+1];takim[a+1]:=tis;
dogru:=false
end;{if (durum}
end;{for a}
until dogru;
{Puana gîre sçralama yapçlçyor}
repeat
dogru:=true;
for a:=1 to 17 do begin
if (durum[a,7]) < (durum[a+1,7]) then begin
for b:=1 to 7 do degistir(durum[a,b],durum[a+1,b]);
tis:=takim[a];takim[a]:=takim[a+1];takim[a+1]:=tis;
dogru:=false
end;{if (durum}
end;{for a}
until dogru;
for a:=1 to 18 do for b:=1 to 18 do begin
doku(b);if takim[a]=kayit.ti then begin tlk[b,oynananhafta]:=a;kayit.durum[1]:=a;dyaz(b);end;
end;
end;
procedure Tablogoster;
var a,b,bos:byte;
bek:char;
begin clrscr;
cerceve('T',1,2,80,23,lightgray);
renk(15,0);gotoxy(20,1);Write(oynananhafta:2,'. Hafta Puan Durumu ve Toplu Sonuálar');renk(lightgray,0);
gotoxy(3,3);Write('Takçm òsmi           Oy Gl Br Ml Att Yed Avr Pn');
gotoxy(3,4);Write('-------------------- -- -- -- -- --- --- --- --');
for a:=1 to 18 do begin
gotoxy(3,a+4);
if takim[a]=takimlar[takimim] then renk(white,blue);
write(takim[a],' ':(21-length(takim[a])),(durum[a,2]+durum[a,3]+durum[a,4]):2,
durum[a,2]:3,durum[a,3]:3,durum[a,4]:3,durum[a,5]:4,durum[a,6]:4,(durum[a,5]-durum[a,6]):4,durum[a,7]:3);
if takim[a]=takimlar[takimim] then renk(lightgray,0);
end;
gotoxy(55,3);write('Toplu Sonuálar');
gotoxy(52,4);write('--------------------');
for a:=1 to 9 do begin
for b:=1 to 2 do begin
if (length(takimlar[fikstur(oynananhafta,a,1)]))>(length(takimlar[fikstur(oynananhafta,a,2)])) then
bos:=length(takimlar[fikstur(oynananhafta,a,1)])+1 else bos:=length(takimlar[fikstur(oynananhafta,a,2)])+1;
end;
if a in[1,3,5,7,9] then begin renk(15,0);b:=52 end else begin renk(lightgray,0);b:=58; end;
gotoxy(b,a*2+3);write(takimlar[fikstur(oynananhafta,a,1)],' ':(bos-length(takimlar[fikstur(oynananhafta,a,1)])),': ',
tops[oynananhafta,a,1]);
gotoxy(b,a*2+4);write(takimlar[fikstur(oynananhafta,a,2)],' ':(bos-length(takimlar[fikstur(oynananhafta,a,2)])),': ',
tops[oynananhafta,a,2]);
end;renk(lightgray,0);
repeat
bek:=readkey;
until bek in[#13,#27];
end;
procedure takimlari_dosyada_karistir;
var a,r,ds:byte;
begin
for a:=1 to 18 do begin
doku(a);dyaz(a+18);
end;
ds:=0;
repeat
r:=random(18)+1;
doku(r+18);
if kayit.ti<>'-' then begin
ds:=ds+1;
doku(r+18);dyaz(ds);
doku(r+18);kayit.ti:='-';dyaz(r+18);
end;
until ds=18;
end;
procedure ana_menu_hazirla;
var a:byte;
begin
for a:=1 to 20 do menu[a]:='';
menu[1]:='Yeni Oyun';
menu[2]:='Oyun YÅkle';
menu[3]:='Takçm Bilgileri';
menu[4]:='Hakkçnda';
menu[5]:='Äçkçü';
end;
{-------------------------}
procedure yenioyun;
VAR
A,B:BYTE;
DD:FILE OF DEFAULT_DOSYA_TIPI;
KD:DEFAULT_DOSYA_TIPI;
begin
ASSIGN(DD,'ILKVERI.TSL');RESET(DD);ASSIGN(DOSYA,'AKTIF.TSL');RESET(DOSYA);
FOR A:=0 TO 18 DO BEGIN
SEEK(DD,A);READ(DD,KD);SEEK(DOSYA,A);
KAYIT.TI:=KD.TI;
KAYIT.TS:=KD.TS;
FOR B:=1 TO 2 DO KAYIT.TR[B]:=KD.TR[B];
KAYIT.TFS:=KD.TFS;
KAYIT.TSG:=KD.TSG;
KAYIT.TP:=KD.TP;
FOR B:=1 TO 18 DO KAYIT.FI[B]:=KD.FI[B];
FOR B:=1 TO 18 DO KAYIT.FM[B]:=KD.FM[B];
FOR B:=1 TO 18 DO KAYIT.FP[B]:=KD.FP[B];
FOR B:=1 TO 18 DO KAYIT.FPi[B]:=KD.FP[B];
FOR B:=1 TO 7 DO KAYIT.DURUM[B]:=0;
WRITE(DOSYA,KAYIT);
END;
CLOSE(DOSYA);CLOSE(DD);
yil:=2004;
end;
{-------------------------}
Procedure PDS;{Puan durumunu sçfçrlar}
var a,b,c:byte;
begin
for a:=1 to 18 do begin
doku(a);for b:=1 to 7 do kayit.durum[b]:=0;dyaz(a);
for b:=1 to 18 do for c:=1 to 6 do fis[a,b,c]:=0;
for b:=1 to 7 do durum[a,b]:=0;
for b:=1 to 34 do tlk[a,b]:=0;
end;
end;
{-------------------------}
Procedure takbil;
procedure menu_hazirla;
var a:byte;
begin
for a:=1 to 20 do menu[a]:='';
FOR A:=1 to 18 do begin
menu[a]:=takimlar[a];
end;
MENU[19]:='Äçkçü';
end;
var
x,y:byte;
guc:real;
begin curoff;clrscr;
ASSIGN(DOSYA,'AKTIF.TSL');RESET(DOSYA);
MENU_hazirla;
gotoxy(19,1);write('FifaCmTr TÅrkiye SÅper Ligi Sezon 2004/2005 Takçmarç');
for y:=1 to 18 do begin
seek(dosya,y);read(dosya,kayit);
guc:=0;for x:=1 to kayit.tfs do begin
guc:=kayit.fp[x]*kayit.fp[x]+guc;
end;
guc:=(guc/kayit.tfs)*11+kayit.tsg;
gotoxy(1,y+2);write(kayit.ti,'':(18-length(kayit.ti)),': ');
for x:=1 to round(guc/20) do write('ﬂ');
end;
gotoxy(1,24);write('Takçmlar Hakkçnda ayrçntçlç bilgi almak iáin Enter''a basçnçz...');tusbekle(#13);clrscr;
repeat
MENUYAP('FifaCmTr TÅrkiye SÅper Ligi Sezon 2004/2005 Takçm Bilgileri',menu,secim,Lightgray,black,false);
curoff;
if secim in[1..18] then begin clrscr;
seek(dosya,secim);read(dosya,kayit);
Gotoxy(round((80-length(kayit.ti))/2),1);write(kayit.ti);
gotoxy(16,2);Write('Takçm Kadrosu');cerceve('T',1,3,41,24,lightgray);
gotoxy(2,4);write('   Futbolcu ismi        Mevkii    Perf.');
gotoxy(2,5);write('   -------------------- --------- -----');
for x:=1 to kayit.tfs do begin
gotoxy(2,x+5);write(x:2,' ',kayit.fi[x],' ':(21-length(kayit.fi[x])));
case kayit.fm[x] of
'K','k':write('Kaleci    ');
'D','d':write('Defans    ');
'O','o':write('Orta Alan ');
'F','f':write('Forvet    ');
end;{case}
gotoxy(36,x+5);write(kayit.fp[x]:5:2);
end;
gotoxy(54,2);write('Dißer Bilgiler');cerceve('T',42,3,79,24,lightgray);
cerceve('T',44,4,77,6,lightgray);gotoxy(46,5);write('Stad: ',kayit.ts);
cerceve('T',44,7,77,9,lightgray);gotoxy(46,8);write('Parasç: ',kayit.tp,' $');
cerceve('T',44,10,77,23,lightgray);
textcolor(kayit.tr[1]);for x:=46 to 60 do for y:=11 to 22 do begin gotoxy(x,y);write('€');end;
textcolor(kayit.tr[2]);for x:=61 to 75 do for y:=11 to 22 do begin gotoxy(x,y);write('€');end;
textcolor(lightgray);
tusbekle(#13);
end;{if secim..}
until secim=19;
CLOSE(DOSYA);
end;
{-------------------------}
Procedure yeni_bir_sezon_hazirla;
var
a,b,c:byte;
tis:string;
begin
pds;
for a:=1 to 34 do for b:=1 to 9 do for c:=1 to 2 do tops[a,b,c]:=0;
tis:=takimlar[takimim];
takimlari_dosyada_karistir;
takimlari_al;
for a:=1 to 18 do if takimlar[a]=tis then takimim:=a;
for b:=1 to 18 do begin
doku(b);
for a:=1 to kayit.tfs do if kayit.fp[a]>0 then kayit.tp:=kayit.tp+round(kayit.fp[a]*kayit.fp[a]*2000);
kayit.tp:=kayit.tp+(kayit.tsg*10000);
dyaz(b);
end;
end;
{-------------------------}
procedure transferyap(t1,f1,t2:byte;ucret:longint);
var
a,b:byte;
fi:string[20];
fp:real;
fm:char;
fiss:array[1..6] of byte;
tu:longint;
begin
doku(t1);
fi:=kayit.fi[f1];
fm:=kayit.fm[f1];
fp:=kayit.fp[f1];
fiss[1]:=fis[t1,f1,1];fiss[2]:=fis[t1,f1,2];fiss[3]:=fis[t1,f1,3];fiss[4]:=fis[t1,f1,4];
fiss[5]:=fis[t1,f1,5];fiss[6]:=fis[t1,f1,6];
tu:=round(kayit.fp[f1]*kayit.fp[f1]*200000)+ucret;
kayit.tp:=kayit.tp+tu;
for a:=f1 to kayit.tfs-1 do begin
kayit.fi[a]:=kayit.fi[a+1];
kayit.fm[a]:=kayit.fm[a+1];
kayit.fp[a]:=kayit.fp[a+1];
kayit.fpi[a]:=kayit.fpi[a+1];
fis[t1,a,1]:=fis[t1,a+1,1];fis[t1,a,2]:=fis[t1,a+1,2];
fis[t1,a,3]:=fis[t1,a+1,3];fis[t1,a,4]:=fis[t1,a+1,4];
fis[t1,a,5]:=fis[t1,a+1,5];fis[t1,a,6]:=fis[t1,a+1,6];
end;
kayit.tfs:=kayit.tfs-1;
dyaz(t1);
doku(t2);
if fm in['d','o','O','D'] then begin
for a:=1 to kayit.tfs do if (kayit.fm[a]=fm) or (kayit.fm=upcase(fm)) then b:=a;
for a:=kayit.tfs downto b+1 do begin
kayit.fi[a+1]:=kayit.fi[a];
kayit.fm[a+1]:=kayit.fm[a];
kayit.fp[a+1]:=kayit.fp[a];
kayit.fpi[a+1]:=kayit.fpi[a];
fis[t2,a+1,1]:=fis[t2,a,1];fis[t2,a+1,2]:=fis[t2,a,2];
fis[t2,a+1,3]:=fis[t2,a,3];fis[t2,a+1,4]:=fis[t2,a,4];
fis[t2,a+1,5]:=fis[t2,a,5];fis[t2,a+1,6]:=fis[t2,a,6];
end;
kayit.fi[b+1]:=fi;
kayit.fm[b+1]:=fm;
kayit.fp[b+1]:=fp;
kayit.fpi[b+1]:=fp;
fis[t2,b+1,1]:=0;fis[t2,b+1,2]:=0;
fis[t2,b+1,3]:=0;fis[t2,b+1,4]:=fiss[4];
fis[t2,b+1,5]:=fiss[5];fis[t2,b+1,6]:=fiss[6];
end else begin
kayit.fi[kayit.tfs+1]:=fi;
kayit.fm[kayit.tfs+1]:=fm;
kayit.fp[kayit.tfs+1]:=fp;
kayit.fpi[kayit.tfs+1]:=fp;
fis[t2,kayit.tfs+1,1]:=0;fis[t2,kayit.tfs+1,2]:=0;
fis[t2,kayit.tfs+1,3]:=0;fis[t2,kayit.tfs+1,4]:=fiss[4];
fis[t2,kayit.tfs+1,5]:=fiss[5];fis[t2,kayit.tfs+1,6]:=fiss[6];
end;
kayit.tfs:=kayit.tfs+1;
kayit.tp:=kayit.tp-tu;
dyaz(t2);
end;
{-------------------------}
procedure kalecial;
var a,secim:byte;
kaa,kav:string[20];
kpa,kpv:real;
tu,kkd:longint;
sc:char;
begin
for a:=1 to 20 do menu[a]:='';
for a:=1 to 18 do begin
doku(a);
menu[a]:=kayit.fi[1]+' ('+kayit.ti+')';
end;
menu[19]:='Geri Dîn';
repeat
menuyap('Ayrçntçlç Bilgi iáin bir kaleci seáin',menu,secim,lightgray,0,true);curoff;
if (secim in[1..18]) and (secim<>takimim) then begin
doku(secim);
clrscr;
cerceve('C',15,7,65,13,lightgray);
gotoxy(31,6);write('Kaleci''nin KÅnyesi');
gotoxy(17,8);write('Kaleci Adç..............: ',kayit.fi[1]);
gotoxy(17,9);write('ûu anki KulÅbÅ..........: ',kayit.ti);
gotoxy(17,10);write('Ortalama Performansç....: ',kayit.fp[1]:5:3);
tu:=round(kayit.fp[1]*kayit.fp[1]*200000);
gotoxy(17,11);write('Transfer öcreti.........: ',tu,' $');
gotoxy(17,12);write('Maá Baüçna aldçßç para..: ',round((kayit.fp[1]*kayit.fp[1]*200000+kayit.tsg*30000)/1000),' $');
cerceve('C',20,14,60,17,lightgray);
gotoxy(22,15);write('(1) Bu Kaleciyi Transfer et');
gotoxy(22,16);write('(2) Geri Dîn');
repeat
sc:=readkey;
until sc in['1','2',#27];
if sc='1' then begin clrscr;
cerceve('C',15,5,65,16,lightgray);
cerceve('C',15,17,65,20,lightgray);
doku(takimim);
kkd:=round((kayit.fp[1]*kayit.fp[1]*200000)*0.8);
gotoxy(17,6);write('Kadronuzda yalnçzca 1 kaleci bulunabileceßinden ');
gotoxy(17,7);write('ûu anda kaleciniz olan ',kayit.fi[1],'ile');
doku(secim);
gotoxy(17,8);write(kayit.fi[1],'''i deßiütirmek zorundasçnçz. ');
doku(takimim);
if tu=kkd then kkd:=round(kkd*0.98);
gotoxy(17,9);write('Eßer bu transferi yapmak istiyorsançz');
gotoxy(17,10);if tu>kkd then
write(tu-kkd,' $ Fark îdeyeceksiniz.') else
write(kkd-tu,' $ Fark alacaksçnçz.');
gotoxy(17,12);write('Transfere devam etmek istiyormusunuz?');
gotoxy(17,14);write('(1) Evet');
gotoxy(17,15);write('(2) Hayçr');
repeat
sc:=readkey;
until sc in['1','2'];
{Transfer yapçlçyor}
doku(takimim);
if (kayit.tp<(tu-kkd)) then begin gotoxy(21,18);write('Bu Transfer iáin yeterli bÅtáeniz yok');
gotoxy(21,19);write('  Devam Etmek iáin Enter''a basçnçz');
tusbekle(#13);end;
if (sc='1') and (kayit.tp>(tu-kkd)) then begin
doku(secim);
kaa:=kayit.fi[1];
kpa:=kayit.fp[1];
doku(takimim);
kayit.tp:=kayit.tp-(tu-kkd);{Åcret alçndç}
kav:=kayit.fi[1];
kpv:=kayit.fp[1];
kayit.fi[1]:=kaa;
kayit.fp[1]:=kpa;
kayit.fpi[1]:=kpa;
dyaz(takimim);
doku(secim);
kayit.tp:=kayit.tp+(tu-kkd);{öcret îdendi}
kayit.fi[1]:=kav;
kayit.fp[1]:=kpv;
kayit.fpi[1]:=kpv;
dyaz(secim);
fis[secim,1,1]:=0;fis[secim,1,2]:=0;
fis[takimim,1,1]:=0;fis[takimim,1,2]:=0;
secim:=0;
gotoxy(23,18);write('Transfer Baüarçyla Tamamlanmçütçr');
gotoxy(23,19);write('Devam Etmek iáin Enter''a basçnçz');tusbekle(#13);
end;{Yapçldç}
end;{if sc}
end;{if secim}
until secim in[0,19];
end;{procedure kalecial}

procedure Futbolcual;
label 1;
var a,b,seca,s2,s3:byte;
ch:char;
tu:longint;
ok:boolean;
begin clrscr;
repeat
for a:=1 to 20 do menu[a]:='';
menu[1]:='Takçm kadrolarçna bak';
menu[2]:='Futbolcu ara';
menu[3]:='Geri Dîn';
menuyap('Transfer',menu,seca,lightgray,0,true);curoff;
if seca=1 then begin
repeat
for a:=1 to 20 do menu[a]:='';for a:=1 to 18 do menu[a]:=takimlar[a];menu[19]:='Geri Dîn';
menuyap('Kadrosunu Gîrmek istedißiniz Takçmç Seáin',menu,s2,lightgray,0,true);curoff;
if (s2 in[1..18]) and (s2<>takimim) then begin
repeat
for a:=1 to 20 do menu[a]:='';
doku(s2);
for a:=1 to (kayit.tfs-1) do begin
menu[a]:=kayit.fi[a+1];
for b:=1 to (21-length(kayit.fi[a+1])) do menu[a]:=menu[a]+' ';
menu[a]:=menu[a]+'['+kayit.fm[a+1]+']';
end;{for a}
menu[kayit.tfs]:='Geri Dîn';
menuyap(kayit.ti,menu,s3,lightgray,0,true);
if s3 in[1..(kayit.tfs-1)] then begin clrscr;
cerceve('C',15,6,66,13,lightgray);gotoxy(32,5);write('Futbolcu KÅnyesi');
gotoxy(17,7);write('Futbolcu Adç............: ',kayit.fi[s3+1]);
gotoxy(17,8);write('ûu anki KulÅbÅ..........: ',kayit.ti);
gotoxy(17,9);write('Mevkii..................: ');
if kayit.fm[s3+1] in['d','D'] then write('Defans');
if kayit.fm[s3+1] in['o','O'] then write('Orta Alan');
if kayit.fm[s3+1] in['f','F'] then write('Forvet');
gotoxy(17,10);write('Genel Performansç.......: ',kayit.fp[s3+1]:5:3);
tu:=round(kayit.fp[s3+1]*kayit.fp[s3+1]*200000);
gotoxy(17,11);write('Transfer öcreti.........: ',tu,' $');
gotoxy(17,12);write('Maá baüçna aldçßç para..: ',round((kayit.fp[s3+1]*kayit.fp[s3+1]*200000+kayit.tsg*30000)/1000),' $');
cerceve('C',20,14,60,17,lightgray);
gotoxy(22,15);write('(1) Bu Futbolcuyu Transfer et');
gotoxy(22,16);write('(2) Geri Dîn');
repeat ch:=readkey; until ch in['1','2',#27];
if ch='1' then begin
cerceve('C',10,18,70,22,lightgray);
doku(s2);ok:=true;
if kayit.fm[s3+1] in['d','D'] then begin
b:=0;for a:=1 to kayit.tfs do if kayit.fm[a] in['d','D'] then b:=b+1;
if b<5 then begin ok:=false;
gotoxy(12,19);write(kayit.ti,' Takçmçndan bu futbolcuyu transfer edemezsiniz.');
gotoxy(12,20);write('ÄÅnkÅ bir takçmda en az 4 Defans oyuncusu bulunmalç');
gotoxy(12,21);write('Devam etmek iáin Enter''a basçnçz...');tusbekle(#13);
end;
end;
if kayit.fm[s3+1] in['o','O'] then begin
b:=0;for a:=1 to kayit.tfs do if kayit.fm[a] in['o','O'] then b:=b+1;
if b<6 then begin ok:=false;
gotoxy(12,19);write(kayit.ti,' Takçmçndan bu futbolcuyu transfer edemezsiniz.');
gotoxy(12,20);write('ÄÅnkÅ bir takçmda en az 5 Ortasaha oyuncusu bulunmalç');
gotoxy(12,21);write('Devam etmek iáin Enter''a basçnçz...');tusbekle(#13);
end;
end;
if kayit.fm[s3+1] in['f','F'] then begin
b:=0;for a:=1 to kayit.tfs do if kayit.fm[a] in['f','F'] then b:=b+1;
if b<3 then begin ok:=false;
gotoxy(12,19);write(kayit.ti,' Takçmçndan bu futbolcuyu transfer edemezsiniz.');
gotoxy(12,20);write('ÄÅnkÅ bir takçmda en az 2 Forvet oyuncusu bulunmalç');
gotoxy(12,21);write('Devam etmek iáin Enter''a basçnçz...');tusbekle(#13);
end;
end;
if ok=false then goto 1;
doku(takimim);
if kayit.tfs=18 then begin gotoxy(12,19);write('Kadronuzda en fazla 18 futbolcu bulunabilir, Kadronuz dolu');end;
if kayit.tp<=(tu+kayit.tp*0.1) then begin gotoxy(12,19);write('Bu transfer iáin yeterli bÅtáeniz yok!!');end;
if (kayit.tfs<18) and (kayit.tp>(tu+kayit.tp*0.1)) then begin
transferyap(s2,s3+1,takimim,0);
gotoxy(12,19);write('Transfer Baüarç ile tamamlanmçütçr!!');
end;
gotoxy(12,20);write('Devam etmek iáin Enter''a basçnçz...');tusbekle(#13);
1:;
end;{if ch='1' then begin}
end;{if s3 in}
until s3 in[0,kayit.tfs];
end;{if (s2 in [1..18])}
until s2 in[0,19];
end;{if seca=1}
until seca in[0,3];
end;

procedure futbolcusat;
label tekrartakimsec;
var
a,b:byte;
satfut,sattak:byte;
gt:char;
futis:string;
tu:longint;
begin
for a:=1 to 20 do menu[a]:='';
doku(takimim);
for a:=1 to kayit.tfs-1 do begin
menu[a]:=kayit.fi[a+1];
for b:=1 to (21-length(kayit.fi[a+1])) do menu[a]:=menu[a]+' ';
menu[a]:=menu[a]+'['+kayit.fm[a+1]+']';
end;
menu[kayit.tfs]:='Geri Dîn';
menuyap('Satçlacak Futbolcu?',menu,satfut,lightgray,0,true);
If satfut in[1..(kayit.tfs-1)] Then Begin
doku(takimim);
futis:=kayit.fi[satfut+1];
tekrartakimsec:;
for a:=1 to 20 do menu[a]:='';
For a:=1 to 18 do begin
menu[a]:=takimlar[a];
End;
menu[19]:='<< òptal >>';
menuyap('Hangi takçma satacaksçnçz?',menu,sattak,lightgray,0,true);
If (sattak in[1..18]) and (sattak<>takimim) Then Begin
doku(sattak);
If kayit.tfs=18 Then begin clrscr;
gotoxy(20,12);write('Bu takçmçn kadrosu DOLU. Baüka takçm seámelisiniz!');
gotoxy(20,13);write('Devam etmek iáin bir tuüa basçn...');
gt:=reADKEY;
goto tekrartakimsec;
end;
clrscr;
gotoxy(20,12);write('[',futis,'] adlç oyuncuyu "',takimlar[sattak],'" takçmçna');
doku(takimim);
tu:=round(kayit.fp[satfut+1]*kayit.fp[satfut+1]*100000);
gotoxy(20,13);write(tu,' $ karüçlçßçnda satmak Åzeresiniz.');
gotoxy(20,15);write('Enter -> Devam et ve futbolcuyu sat');
gotoxy(20,16);write('Esc   -> Bu satçüç iptal et');
gotoxy(20,18);write('Seáiminiz... ');
gt:=readkey;
if gt=#13 Then Begin
tu:=-tu;
transferyap(takimim,satfut+1,sattak,tu);
gotoxy(20,20);write('Transfer Baüarç ile tamamlanmçütçr!!');
gotoxy(20,21);write('Devam etmek iáin bir tuüa basçn...');
gt:=reADKEY;
end;
End; {Takçm seáildiyse}
End; {Futbolcu seáildiyse}
end;

procedure parayigoster;
begin
cerceve('C',20,9,60,11,lightgray);
doku(takimim);
gotoxy(22,10);write('Transfer BÅtáeniz: ',kayit.tp,' $');
cerceve('C',22,12,58,14,lightgray);
gotoxy(23,13);write(' Devam etmek iáin Enter''a basçnçz ');tusbekle(#13);
end;
{-------------------------}
Procedure fikstur_goster(tn:byte);
label enbas,tdb;
var a,b,sn:byte;
sc:char;
begin;
tdb:;
doku(tn);sn:=1;
enbas:clrscr;
cerceve('T',1,1,50,22,lightgray);
cerceve('T',52,1,80,22,lightgray);
renk(kayit.tr[1],kayit.tr[2]);
gotoxy(round((49-(length(kayit.ti)))/2)-1,1);write(' ',kayit.ti,' ');
renk(lightgray,0);
gotoxy(40,22);write('Sayfa ',sn,'/2');
for a:=1 to 20 do begin
if (a+(sn-1)*20)<35 then begin
for b:=1 to 9 do if (fikstur(a+(sn-1)*20,b,1)=tn) or (fikstur(a+(sn-1)*20,b,2)=tn) then begin
if fikstur(a+(sn-1)*20,b,1)=tn then begin
renk(15,0);
if oynananhafta=(a+(sn-1)*20) then begin gotoxy(2,a+1);write('>');end;
gotoxy(3,a+1);write((a+(sn-1)*20):2,'. Hafta > ');
write(takimlar[fikstur(a+(sn-1)*20,b,2)]);
gotoxy(35,a+1);write('Ev  ');
if oynananhafta>(a+(sn-1)*20) then begin
write(tops[a+(sn-1)*20,b,1],':',tops[a+(sn-1)*20,b,2]);
end;{if oynana..}
end else begin
renk(lightgray,0);
if oynananhafta=(a+(sn-1)*20) then begin gotoxy(2,a+1);write('>');end;
gotoxy(3,a+1);write((a+(sn-1)*20):2,'. Hafta > ');
write(takimlar[fikstur(a+(sn-1)*20,b,1)]);
gotoxy(35,a+1);write('Dep ');
if oynananhafta>(a+(sn-1)*20) then begin
write(tops[a+(sn-1)*20,b,2],':',tops[a+(sn-1)*20,b,1]);
end;{if oynana..}
end;
end;{for if}
end;{if (a}
end;{for a}
renk(lightgray,0);
gotoxy(54,2);write('1-> Dißer Takçmlar');
gotoxy(54,3);write('2-> ônceki Sayfa  ');
gotoxy(54,4);write('3-> Sonraki Sayfa ');
gotoxy(54,5);write('4-> Geri Dîn      ');
repeat
sc:=readkey;
until sc in['1','2','3','4',#27];
if sc='1' then begin
for b:=1 to 20 do menu[b]:='';for b:=1 to 18 do menu[b]:=takimlar[b];
menuyap('Bir Takçm Seáin',menu,b,cyan,0,true);curoff;
if b>0 then tn:=b;
goto tdb;
end;
if sc='2' then begin
if sn>1 then sn:=sn-1;
goto enbas;
end;
if sc='3' then begin
if sn<2 then sn:=sn+1;
goto enbas;
end;
end;{procedure fiks..}
{-------------------------}
procedure Oyunu_Kaydet(oyun_adi:string;kn:byte);
var
d:file of dosya_kaydetme_tipi;
k:dosya_kaydetme_tipi;
s:string;
a,b,c:byte;
dos:file of aktif_dosya_tipi;
kay:aktif_dosya_tipi;
begin
assign(d,'oynkayit.tsl');reset(d);
k.koi:=oyun_adi;
str(kn,s);
s:='ko'+s+'.tsk';
k.kdi:=s;
k.st:=takimim;
k.oh:=oynananhafta-1;
k.db:=dakikabirimi;
k.yil:=yil;
k.otokayit:=otokayit;
for a:=1 to 3 do k.sts[a]:=sistemim[a];
for a:=1 to 34 do for b:=1 to 9 do for c:=1 to 2 do k.tops[a,b,c]:=tops[a,b,c];
for a:=1 to 18 do for b:=1 to 18 do for c:=1 to 6 do k.fis[a,b,c]:=fis[a,b,c];
for a:=1 to 18 do for b:=1 to 34 do k.tlk[a,b]:=tlk[a,b];
for a:=1 to 11 do k.takimim11[a]:=takimim11[a];
seek(d,kn);write(d,k);
close(d);
assign(dos,s);rewrite(dos);
for a:=1 to 18 do begin
doku(a);kay:=kayit;
seek(dos,a);write(dos,kay);
end;
close(dos);
end;
{-------------------------}
procedure oyun_yukle;
var
d:file of dosya_kaydetme_tipi;
k:dosya_kaydetme_tipi;
dos:file of aktif_dosya_tipi;
kay:aktif_dosya_tipi;
a,b,c:byte;
s:string;
begin renk(lightgray,0);clrscr;
assign(d,'oynkayit.tsl');reset(d);
for a:=1 to 20 do menu[a]:='';
for a:=0 to filesize(d)-1 do begin seek(d,a);read(d,k);menu[a+1]:=k.koi;end;
menuyap('YÅklemek istedißiniz Oyun òsmini Seáin',menu,a,lightgray,0,false);
seek(d,a-1);read(d,k);
takimim:=k.st;
aoa:=k.koi;
on:=a-1;
oynananhafta:=k.oh;
dakikabirimi:=k.db;
yil:=k.yil;
otokayit:=k.otokayit;
for a:=1 to 3 do sistemim[a]:=k.sts[a];
for a:=1 to 34 do for b:=1 to 9 do for c:=1 to 2 do tops[a,b,c]:=k.tops[a,b,c];
for a:=1 to 18 do for b:=1 to 18 do for c:=1 to 6 do fis[a,b,c]:=k.fis[a,b,c];
for a:=1 to 18 do for b:=1 to 34 do tlk[a,b]:=k.tlk[a,b];
for a:=1 to 11 do takimim11[a]:=k.takimim11[a];
s:=k.kdi;
close(d);
assign(dos,s);reset(dos);
assign(dosya,'aktif.tsl');reset(dosya);
for a:=1 to 18 do begin
seek(dos,a);read(dos,kay);
kayit:=kay;
seek(dosya,a);write(dosya,kayit);
end;
close(dos);close(dosya);
end;
{-------------------------}
procedure oynkayit;
var
a,b:byte;
s:string;
begin
cerceve('C',13,12,68,14,lightgray);
for a:=14 to 67 do begin gotoxy(a,13);write(' ');end;
oyunu_kaydet(aoa,on);
gotoxy(15,13);write('Oyun Baüarçyla Kaydedildi, LÅtfen Enter''a basçnçz...');
tusbekle(#13);
end;
{-------------------------}
Procedure istatislik(tn:byte);
var
a,b,c,d:byte;
sc:char;
efm,efg:byte;
s:string;
Procedure genelist;
var a,b,c:byte;
e,f:byte;
begin
for a:=2 to 54 do for b:=2 to 23 do begin gotoxy(a,b);write(' ');end;
Gotoxy(3,2);Write('En GolcÅ Tak.:');
Gotoxy(3,3);Write('En GolcÅ Fut.:');
Gotoxy(3,4);Write('En GollÅ maá.:');
Gotoxy(3,5);Write('En áok asist.:');
e:=1;
for a:=2 to 18 do if durum[a,5]>durum[e,5] then e:=a;
gotoxy(18,2);write(takim[e],' (',durum[e,5],' gol)');
e:=1;f:=1;
for a:=1 to 18 do for b:=1 to 18 do if fis[a,b,2]>fis[e,f,2] then begin
e:=a;f:=b;
end;
gotoxy(18,3);doku(e);write(kayit.fi[f],'(',fis[e,f,2],' gol)');
for a:=1 to 18 do for b:=1 to 18 do if fis[a,b,3]>fis[e,f,3] then begin
e:=a;f:=b;
end;
gotoxy(18,5);doku(e);write(kayit.fi[f],'(',fis[e,f,3],' asist)');
e:=1;f:=1;
for a:=1 to oynananhafta do for b:=1 to 9 do if tops[a,b,1]+tops[a,b,2]>tops[e,f,1]+tops[e,f,2] then begin e:=a;f:=b;end;
gotoxy(18,4);write(takimlar[fikstur(e,f,1)],'-',takimlar[fikstur(e,f,2)],':',tops[e,f,1],'-',tops[e,f,2]);
end;
begin
repeat curoff;
clrscr;
doku(tn);
cerceve('T',1,1,55,24,lightblue);cerceve('T',56,1,80,24,lightblue);
gotoxy(3,1);write(' ');renk(kayit.tr[1],kayit.tr[2]);write(' ',kayit.ti,' ');
renk(lightblue,0);write(' ');gotoxy(39,1);write(' òstatislikler ');
textcolor(lightgray);
Gotoxy(58,2);write('[1] Hafta-Konum Grf.');
Gotoxy(58,3);write('[2] Genel ist.');
Gotoxy(58,4);write('[3] Dißer Tkm.');
Gotoxy(58,5);write('[4] Äçkçü');
Gotoxy(3,2);Write('En Farklç Glb:');
Gotoxy(3,3);Write('En Farklç Mlb:');
Gotoxy(3,4);Write('En gollÅ maá.:');
Gotoxy(3,5);Write('En áok oyn...:');
Gotoxy(3,6);Write('En golcÅ.....:');
Gotoxy(3,7);Write('En áok Asist.:');
Gotoxy(3,8);Write('Sarç Kart....:');
Gotoxy(3,9);Write('Kçrmçzç Kart.:');
Gotoxy(3,10);Write('En iyi Drc...:');
Gotoxy(3,11);Write('En KîtÅ Drc..:');

efg:=0;
for a:=1 to oynananhafta do begin
for b:=1 to 9 do begin
if (fikstur(a,b,1)=tn) or (fikstur(a,b,2)=tn) then begin
if (fikstur(a,b,1)=tn) and (tops[a,b,1]-tops[a,b,2]>efg) then begin
efg:=tops[a,b,1]-tops[a,b,2];
gotoxy(18,2);write('                              ');
gotoxy(18,2);write(takimlar[fikstur(a,b,2)],' ',tops[a,b,1],':',tops[a,b,2]);
end;
if (fikstur(a,b,2)=tn) and (tops[a,b,2]-tops[a,b,1]>efg) then begin
efg:=tops[a,b,2]-tops[a,b,1];
gotoxy(18,2);write('                              ');
gotoxy(18,2);write(takimlar[fikstur(a,b,1)],' ',tops[a,b,2],':',tops[a,b,1]);
end;
end;
end;
end;

efm:=0;
for a:=1 to oynananhafta do begin
for b:=1 to 9 do begin
if (fikstur(a,b,1)=tn) or (fikstur(a,b,2)=tn) then begin
if (fikstur(a,b,1)=tn) and (tops[a,b,2]-tops[a,b,1]>efm) then begin
efm:=tops[a,b,2]-tops[a,b,1];
gotoxy(18,3);write('                              ');
gotoxy(18,3);write(takimlar[fikstur(a,b,2)],' ',tops[a,b,1],':',tops[a,b,2]);
end;
if (fikstur(a,b,2)=tn) and (tops[a,b,1]-tops[a,b,2]>efm) then begin
efm:=tops[a,b,1]-tops[a,b,2];
gotoxy(18,3);write('                              ');
gotoxy(18,3);write(takimlar[fikstur(a,b,1)],' ',tops[a,b,2],':',tops[a,b,1]);
end;
end;
end;
end;

efm:=0;{en gollÅ maá}
for a:=1 to oynananhafta do begin
for b:=1 to 9 do begin
if (fikstur(a,b,1)=tn) or (fikstur(a,b,2)=tn) then begin
if tops[a,b,1]+tops[a,b,2]>efm then begin
efm:=tops[a,b,1]+tops[a,b,2];
gotoxy(18,4);write('                              ');
if fikstur(a,b,1)=tn then begin
gotoxy(18,4);write(takimlar[fikstur(a,b,2)],' ',tops[a,b,1],':',tops[a,b,2]);
end;
if fikstur(a,b,2)=tn then begin
gotoxy(18,4);write(takimlar[fikstur(a,b,1)],' ',tops[a,b,2],':',tops[a,b,1]);
end;
end;
end;
end;
end;

efm:=0;
for a:=2 to kayit.tfs do if fis[tn,a,1]>efm then begin
efm:=fis[tn,a,1];
gotoxy(18,5);write('                              ');
gotoxy(18,5);write(kayit.fi[a],' (',efm,')');
end;

efm:=0;
for a:=2 to kayit.tfs do if fis[tn,a,2]>efm then begin
efm:=fis[tn,a,2];
gotoxy(18,6);write('                              ');
gotoxy(18,6);write(kayit.fi[a],' (',efm,')');
end;

efm:=0;
for a:=2 to kayit.tfs do if fis[tn,a,3]>efm then begin
efm:=fis[tn,a,3];
gotoxy(18,7);write('                              ');
gotoxy(18,7);write(kayit.fi[a],' (',efm,')');
end;
{
efm:=0;
for a:=2 to kayit.tfs do if fis[tn,a,4]>efm then begin
efm:=fis[tn,a,4];
gotoxy(18,8);write('                              ');
gotoxy(18,8);write(kayit.fi[a],' (',efm,')');
end;

efm:=0;
for a:=2 to kayit.tfs do if fis[tn,a,5]>efm then begin
efm:=fis[tn,a,5];
gotoxy(18,9);write('                              ');
gotoxy(18,9);write(kayit.fi[a],' (',efm,')');
end;
}
{En iyi ve En kîtÅ dereceleri bulunuyor}
b:=19;
for a:=1 to oynananhafta-1 do if tlk[tn,a]<=b then begin
b:=tlk[tn,a];
gotoxy(18,10);write('                 ');
gotoxy(18,10);write(b,' (',a,'. Hafta)');
end;
b:=0;

for a:=1 to oynananhafta do if tlk[tn,a]>=b then begin
b:=tlk[tn,a];
gotoxy(18,11);write('                 ');
gotoxy(18,11);write(b,' (',a,'. Hafta)');
end;
{En iyi ve En kîtÅ dereceleri bulundu}
repeat
sc:=readkey;
until sc in['1','2','3','4',#27];
if sc='1' then begin
opengraph;
SETCOLOR(lightgray);
SetTextStyle(0, 0, 2);
outtextxy(3,5,'SIRA');
outtextxy(556,460,'HAFTA');
SetTextStyle(0, 0, 3);
outtextxy(20,440,kayit.ti);
SetTextStyle(DefaultFont, HorizDir, 1);
SETCOLOR(WHITE);
for a:=1 to 214 do for b:=1 to 161 do begin
putpixel(a*4-3,b*4-2,cyan);
end;
outtextxy(17,470,'1');
for a:=1 to 18 do begin
str(a,s);
outtextxy(1,(a*25)-15,s);
end;
SETLINESTYLE(3,0,3);
for a:=2 to 34 do begin
if a<oynananhafta then line((a-1)*17+3,(tlk[tn,a-1]*25)-15,a*17+3,(tlk[tn,a]*25)-15);
str(a,s);
outtextxy((a)*17,470,s);
end;{$IFDEF FPC}repeat until wincrt.readkey=#13;{$ELSE}tusbekle(#13);{$ENDIF}
closegraph;
end;
if sc='3' then begin
for a:=1 to 20 do menu[a]:='';for a:=1 to 18 do menu[a]:=takimlar[a];
menuyap('Hangi Takçmçn òstatislißinin Gîrmek istiyorsunuz',menu,a,cyan,0,true);
if a>0 then tn:=a;
end;
if sc='2' then begin genelist;sc:=readkey;end;
until sc in['4',#27];
end;
{-------------------------}
procedure oyunu_oyna;
label tekraryaz,yenibirsezonabaslangic,oyundan_cikis;
var
ch,ch01:char;
x,y,z,dl:byte;
procedure takimsecmenu;
var a,b:byte;
strn:string;
begin
for a:=1 to 20 do menu[a]:='';
FOR A:=1 to 18 do begin
doku(a);menu[a]:=kayit.ti;
for b:=1 to 18 do if a=gucsir[b] then begin
str(b,strn);
{menu[a]:=menu[a]+' ['+strn+']';}
end;
end;
end;
procedure sti11b;{Seáilen takçmçn ilk11'ini belirler}
label tekrarbelirle,iptalet;
var a,fn,sistem:byte;
ds,os,fs:byte;ss:string;
soru:char;
function secilmemis(no,fs:byte):boolean;
var x:byte;
begin secilmemis:=true;
for x:=1 to (fs-1) do if no=takimim11[x] then secilmemis:=false;
end;
begin clrscr;curon;
doku(takimim);
for a:=1 to 20 do menu[a]:='';
menu[1]:='4-4-2';
menu[2]:='3-5-2';
menu[3]:='4-3-3';
menu[4]:='3-4-3';
menu[5]:='5-3-2';
menu[6]:='Vazgeá';
ss:=kayit.ti+' iáin sistem belirleyin';
menuyap(ss,menu,sistem,lightgray,black,true);
if sistem in[6,0] then goto iptalet;
if sistem=1 then begin ds:=4;os:=4;fs:=2;sistemim[1]:=4;sistemim[2]:=4;sistemim[3]:=2;end;
if sistem=2 then begin ds:=3;os:=5;fs:=2;sistemim[1]:=3;sistemim[2]:=5;sistemim[3]:=2;end;
if sistem=3 then begin ds:=4;os:=3;fs:=3;sistemim[1]:=4;sistemim[2]:=3;sistemim[3]:=3;end;
if sistem=4 then begin ds:=3;os:=4;fs:=3;sistemim[1]:=3;sistemim[2]:=4;sistemim[3]:=3;end;
if sistem=5 then begin ds:=5;os:=3;fs:=2;sistemim[1]:=5;sistemim[2]:=3;sistemim[3]:=2;end;
tekrarbelirle:;clrscr;
gotoxy(round((80-(length(kayit.ti)))/2),1);write(kayit.ti);
cerceve('T',1,2,44,23,lightgray);cerceve('T',46,2,80,23,lightgray);
gotoxy(13,3);write('Futbolcu Listesi');
for a:=1 to kayit.tfs do begin
gotoxy(3,a+4);write('(',a:2,') ',kayit.fi[a],' ':(21-length(kayit.fi[a])),'[',kayit.fm[a],'] ',kayit.fp[a]:5:3);
end;{for..}
gotoxy(57,3);write('Seáilen ilk 11');
for a:=1 to 11 do begin
takimim11[a]:=0;
gotoxy(48,a+4);write('(',a:2,')');
end;{for..}
takimim11[1]:=1;
gotoxy(53,5);write(kayit.fi[1]);
gotoxy(1,24);write('                                                                           ');
for a:=2 to ds+1 do begin
gotoxy(1,24);write(a-1,'. Defans Oyuncusunu seáin: ');
repeat
ioku(32,24,fn,18,2);
until secilmemis(fn,a);
takimim11[a]:=fn;
gotoxy(3,fn+4);write('----------------------------------------');
gotoxy(53,a+4);write(kayit.fi[takimim11[a]]);
end;
gotoxy(1,24);write('                                                                           ');
for a:=ds+2 to ds+os+1 do begin
gotoxy(1,24);write(a-(ds+1),'. Ortasaha Oyuncusunu seáin: ');
repeat
ioku(32,24,fn,18,2);
until secilmemis(fn,a);
takimim11[a]:=fn;
gotoxy(3,fn+4);write('----------------------------------------');
gotoxy(53,a+4);write(kayit.fi[takimim11[a]]);
end;
gotoxy(1,24);write('                                                                           ');
for a:=ds+os+2 to 11 do begin
gotoxy(1,24);write(a-(ds+os+1),'. Forvet Oyuncusunu seáin: ');
repeat
ioku(32,24,fn,18,2);
until secilmemis(fn,a);
takimim11[a]:=fn;
gotoxy(3,fn+4);write('----------------------------------------');
gotoxy(53,a+4);write(kayit.fi[takimim11[a]]);
end;
clrscr;
gotoxy(round((80-(length(kayit.ti)))/2),1);write(kayit.ti);
cerceve('T',25,2,55,14,lightgray);
renk(cyan,0);gotoxy(27,3);write('[ 1] ',kayit.fi[1]);
renk(lightgray,0);for a:=2 to ds+1 do begin
gotoxy(27,a+2);write('[',a:2,'] ',kayit.fi[takimim11[a]]);
end;
renk(lightblue,0);for a:=ds+2 to ds+os+1 do begin
gotoxy(27,a+2);write('[',a:2,'] ',kayit.fi[takimim11[a]]);
end;
renk(lightmagenta,0);for a:=ds+os+2 to 11 do begin
gotoxy(27,a+2);write('[',a:2,'] ',kayit.fi[takimim11[a]]);
end;renk(lightgray,0);
gotoxy(1,16);write('Bu ilk11''i onaylamak iáin ENTER''a, deßiütirmek istiyorsançz ESC''ye basçnçz...');
repeat
soru:=readkey;
until soru in[#13,#27];
if soru=#27 then goto tekrarbelirle;
iptalet:;
curoff;end;{-----------------sti11b----------------------}
Procedure tkg(gt:byte);{Takçm kadrosunu gîsteren prosedÅr}
label enbas;
var
x:byte;
sc:char;
begin
enbas:;
doku(gt);
cerceve('T',1,1,56,22,cyan);
renk(kayit.tr[1],kayit.tr[2]);gotoxy(round((55-(length(kayit.ti)))/2),1);write(' ',kayit.ti,' ');
renk(15,0);
gotoxy(3,2);write('Futbolcu Adç         Mevkii    MS GS AS SK KK Perf. ');
gotoxy(3,3);write('-------------------- --------- -- -- -- -- -- ------');
for x:=1 to kayit.tfs do begin
case kayit.fm[x] of
'k','K':begin renk(cyan,0);gotoxy(24,x+3);write('Kaleci');end;
'd','D':begin renk(lightgray,0);gotoxy(24,x+3);write('Defans');end;
'o','O':begin renk(lightblue,0);gotoxy(24,x+3);write('Orta Alan');end;
'f','F':begin renk(lightmagenta,0);gotoxy(24,x+3);write('Forvet');end;
end;{case}
gotoxy(3,x+3);write(kayit.fi[x]);
gotoxy(34,x+3);write(fis[gt,x,1]:2);
gotoxy(37,x+3);write(fis[gt,x,2]:2);
gotoxy(40,x+3);write(fis[gt,x,3]:2);
gotoxy(43,x+3);write(fis[gt,x,4]:2);
gotoxy(46,x+3);write(fis[gt,x,5]:2);
gotoxy(49,x+3);write(kayit.fp[x]:6:3);
end;{for}
cerceve('T',58,1,80,15,cyan);
renk(15,0);
gotoxy(60,2);write('[1] Dißer Takçmlar');
gotoxy(60,3);write('[2] Geri Dîn      ');
cerceve('T',58,16,80,22,cyan);
renk(lightgray,0);
gotoxy(60,17);write('MS: Maá Sayçsç');
gotoxy(60,18);write('GS: Gol Sayçsç');
gotoxy(60,19);write('AS: Asist Sayçsç');
gotoxy(60,20);write('SK: Sarç Kart');
gotoxy(60,21);write('KK: Kçrmçzç Kart');
repeat
sc:=readkey;
if sc='1' then begin clrscr;
for x:=1 to 20 do menu[x]:='';
for x:=1 to 18 do menu[x]:=takimlar[x];
menuyap('Bir Takçm Seáiniz',menu,x,cyan,0,false);curoff;
gt:=x;clrscr;goto enbas;
end;
until sc in['2',#27];
end;{procedure--------------------------------------------}
procedure hafta_oyna;
var macsay,a,b:byte;
satn:byte;
procedure Mac(ev,dep:string);
label maciatla;
var
t1g,t2g:array[1..8] of byte;{Takçmlarda gol atanlar}
gold1,gold2:array[1..8] of byte;{Gollerin Dakikalarç}
a,b,c,d,c1,c2:byte;
dogru:boolean;
r,g,as:integer;{Random iáin kullançlan deßiükenler}
ds,os,fs:byte;{sistemlerin sayçlarç (4-4-2 gibi)}
sistem1,sistem2:array[1..3] of byte;{Sistemler}
ber,evg,depg:integer;{gÅáleri ve beraberlik ihtimali}
dtd,evgol,depgol:byte;
ilk11:array[1..2,1..11] of byte;{Takçmlarçn sahadaki 11'leri}
chare:char;
sk:array[1..2,1..3,1..2] of byte;{Sarç Kart}
kk:array[1..2,1..2,1..2] of byte;{Kçrmçzç Kart}
{1..2 : Takçm Numarasç, 1..3,2 : Kartçn Dakikasç, 1..2 : Kartç Gîren Futbolcu}
{------------------------------------}
procedure ilkonbiribelirle;
var
a,b:byte;
function dahaoncesecilmedi(tn,s:byte):boolean;
var d:byte;
begin dahaoncesecilmedi:=true;
for d:=1 to 11 do begin
if ilk11[tn,d]>0 then if kayit.fi[s]=kayit.fi[ilk11[tn,d]] then dahaoncesecilmedi:=false;
end;
end;{function}
begin
for a:=1 to 11 do begin ilk11[1,a]:=0;ilk11[2,a]:=0;end;
ilk11[1,1]:=1;ilk11[2,1]:=1;
if ev<>takimlar[takimim] then begin
a:=random(2)+1;
if a=1 then begin ds:=3;os:=5;fs:=2;sistem1[1]:=ds;sistem1[2]:=os;sistem1[3]:=fs;end;
if a=2 then begin ds:=3;os:=5;fs:=2;sistem1[1]:=ds;sistem1[2]:=os;sistem1[3]:=fs;end;
for a:=1 to 18 do if ev=takimlar[a] then doku(a);
for a:=2 to ds+1 do begin
for b:=1 to kayit.tfs do begin
if (kayit.fm[b] in['d','D']) and (dahaoncesecilmedi(1,b)=true) then begin
if ilk11[1,a]=0 then ilk11[1,a]:=b;
if kayit.fp[b]>kayit.fp[ilk11[1,a]] then ilk11[1,a]:=b;
end;{if kayit..}
end;{for b}
end;{for a}
for a:=ds+2 to ds+os+1 do begin
for b:=1 to kayit.tfs do begin
if (kayit.fm[b] in['o','O']) and (dahaoncesecilmedi(1,b)=true) then begin
if ilk11[1,a]=0 then ilk11[1,a]:=b;
if kayit.fp[b]>kayit.fp[ilk11[1,a]] then ilk11[1,a]:=b;
end;{if kayit..}
end;{for b}
end;{for a}
for a:=ds+os+2 to ds+os+fs+1 do begin
for b:=1 to kayit.tfs do begin
if (kayit.fm[b] in['f','F']) and (dahaoncesecilmedi(1,b)=true) then begin
if ilk11[1,a]=0 then ilk11[1,a]:=b;
if kayit.fp[b]>kayit.fp[ilk11[1,a]] then ilk11[1,a]:=b;
end;{if kayit..}
end;{for b}
end;{for a}
end else begin
for a:=1 to 11 do ilk11[1,a]:=takimim11[a];
sistem1[1]:=sistemim[1];sistem1[2]:=sistemim[2];sistem1[3]:=sistemim[3];
end;{if then else...}
if dep<>takimlar[takimim] then begin
for a:=1 to 18 do if dep=takimlar[a] then doku(a);
a:=random(2)+1;
if a=1 then begin ds:=3;os:=5;fs:=2;sistem2[1]:=ds;sistem2[2]:=os;sistem2[3]:=fs;end;
if a=2 then begin ds:=3;os:=5;fs:=2;sistem2[1]:=ds;sistem2[2]:=os;sistem2[3]:=fs;end;
for a:=2 to ds+1 do begin
for b:=1 to kayit.tfs do begin
if (kayit.fm[b] in['d','D']) and (dahaoncesecilmedi(2,b)=true) then begin
if ilk11[2,a]=0 then ilk11[2,a]:=b;
if kayit.fp[b]>kayit.fp[ilk11[2,a]] then ilk11[2,a]:=b;
end;{if kayit..}
end;{for b}
end;{for a}
for a:=ds+2 to ds+os+1 do begin
for b:=1 to kayit.tfs do begin
if (kayit.fm[b] in['o','O']) and (dahaoncesecilmedi(2,b)=true) then begin
if ilk11[2,a]=0 then ilk11[2,a]:=b;
if kayit.fp[b]>kayit.fp[ilk11[2,a]] then ilk11[2,a]:=b;
end;{if kayit..}
end;{for b}
end;{for a}
for a:=ds+os+2 to ds+os+fs+1 do begin
for b:=1 to kayit.tfs do begin
if (kayit.fm[b] in['f','F']) and (dahaoncesecilmedi(2,b)=true) then begin
if ilk11[2,a]=0 then ilk11[2,a]:=b;
if kayit.fp[b]>kayit.fp[ilk11[2,a]] then ilk11[2,a]:=b;
end;{if kayit..}
end;{for b}
end;{for a}
end else begin
for a:=1 to 11 do ilk11[2,a]:=takimim11[a];
sistem2[1]:=sistemim[1];sistem2[2]:=sistemim[2];sistem2[3]:=sistemim[3];
end;{if then else}
end;{procedure ilkonbiribelirle}
{------------------------------------}
begin
ilkonbiribelirle;
if macsay=1 then begin gotoxy(27,6);write(' ':40);gotoxy(15,6);write(' CUMA      ');end;
if macsay in[2,6] then begin gotoxy(6,22);write(' ':60);gotoxy(6,22);write('Devam Etmek iáin ENTER''a basçnçz...');
if (ev<>takimlar[takimim]) and (dep<>takimlar[takimim]) and ((fikstur(oynananhafta,macsay-1,1)<>takimim) and
(fikstur(oynananhafta,macsay-1,2)<>takimim)) then tusbekle(#13);gotoxy(6,22);write(' ':60);satn:=8;
if (ev<>takimlar[takimim]) and (dep<>takimlar[takimim]) then for a:=15 to 66 do for b:=10 to 19 do begin
gotoxy(a,b);write(' ');end;end;
if macsay in[2..5] then begin gotoxy(15,6);write(' CUMARTESò ');end;
if macsay in[6..9] then begin gotoxy(15,6);write(' PAZAR     ');end;
for a:=1 to 18 do begin
doku(a);
{Ev sahibiyle ilgili bilgiler alçnçyor}
if kayit.ti=ev then begin
evg:=0;
for b:=1 to 11 do begin
evg:=evg+round(kayit.fp[ilk11[1,b]]*kayit.fp[ilk11[1,b]]);
end;{for b}
evg:=kayit.tsg*2+evg;
evg:=round(evg*1.4);{Evsahibi Takçmçn GÅcÅ artçrçlçyor %40}
end;{alçndç}
{Deplasman takçmçyla ilgili bilgiler alçnçyor}
if kayit.ti=dep then begin
depg:=0;
for b:=1 to 11 do begin
depg:=depg+round(kayit.fp[ilk11[2,b]]*kayit.fp[ilk11[2,b]]);
end;{for b}
depg:=kayit.tsg*2+depg;
end;{alçndç}
end;
if evg>=depg then ber:=round(depg*0.35) else ber:=round(evg*0.35);
r:=random(evg+ber+depg)+1;
if r<(evg+1) then begin{Evsahibi galip gelmeli}
g:=random(1000)+1;
case g of
1..300:evgol:=1;
301..600:evgol:=2;
601..800:evgol:=3;
801..950:evgol:=4;
951..993:evgol:=5;
994..997:evgol:=6;
998..999:evgol:=7;
1000:evgol:=8;
end;{case}
if ((depg/evg)>1.5) and (evgol>5) then g:=g-2;
g:=random(1000)+1;
case g of
1..300:depgol:=0;
301..600:depgol:=1;
601..800:depgol:=2;
801..950:depgol:=3;
951..993:depgol:=4;
994..997:depgol:=5;
998..999:depgol:=6;
1000:depgol:=7;
end;{case}
if evgol<=depgol then depgol:=evgol-1;
end;{if r<(evg+1)...}
if (r>evg) and (r<=(evg+ber)) then begin{Beraberlik hali}
g:=random(1000)+1;
case g of
1..400:evgol:=0;
401..700:evgol:=1;
701..890:evgol:=2;
891..980:evgol:=3;
981..999:evgol:=4;
1000:evgol:=5;
end;{case}
depgol:=evgol;
end;{if r>evg...}
if (r>(evg+ber)) then begin{Deplasman takçmçnçn galip geldißi durum}
g:=random(1000)+1;
case g of
1..350:depgol:=1;
351..630:depgol:=2;
631..810:depgol:=3;
811..980:depgol:=4;
981..996:depgol:=5;
997..1000:depgol:=6;
end;{case}
if ((evg/depg)>2.4) and (depgol>3) then depgol:=depgol-2;
g:=random(1000)+1;
case g of
1..350:evgol:=0;
351..630:evgol:=1;
631..810:evgol:=2;
811..980:evgol:=3;
981..996:evgol:=4;
997..1000:evgol:=5;
end;{case}
if evgol>=depgol then evgol:=depgol-1;
end;{if (r>(evg+ber))}
tops[oynananhafta,macsay,1]:=evgol;tops[oynananhafta,macsay,2]:=depgol;
gotoxy(4,22);write('                                                                          ');
{Seáilen takçmçn maáç oynançyor}
if (ev=takimlar[takimim]) or (dep=takimlar[takimim]) then begin
for a:=1 to 8 do begin gold1[a]:=0;gold2[a]:=0;end;
gotoxy(6,22);write(ev,'-',dep,' Maáç iáin Enter''a basçnçz...');tusbekle(#13);
gotoxy(6,22);write(' ':60);
for a:=15 to 66 do for b:=10 to 19 do begin gotoxy(a,b);write(' ');end;
gotoxy(17,10);write(ev:20,' 0 ƒƒ 0 ',dep);
for a:=1 to 18 do if takimlar[a]=ev then doku(a);
gotoxy(21,8);write(kayit.ts,' ':(35-length(kayit.ts)));
for a:=1 to 8 do t1g[a]:=0;for a:=1 to 8 do t2g[a]:=0;
for a:=1 to 18 do begin
if takimlar[a]=ev then begin
if depgol=0 then fis[a,1,2]:=fis[a,1,2]+1;
for b:=1 to 11 do fis[a,ilk11[1,b],1]:=fis[a,ilk11[1,b],1]+1;
if evgol>0 then for b:=1 to evgol do begin
g:=random(100)+1;
case g of
1..60:begin
r:=random(sistem1[3])+sistem1[1]+sistem1[2]+2;
fis[a,ilk11[1,r],2]:=fis[a,ilk11[1,r],2]+1;
t1g[b]:=r;
end;{1..10}
61..90:begin
r:=random(sistem1[2])+sistem1[1]+2;
fis[a,ilk11[1,r],2]:=fis[a,ilk11[1,r],2]+1;
t1g[b]:=r;
end;{61..90}
91..100:begin
r:=random(sistem1[1])+2;
fis[a,ilk11[1,r],2]:=fis[a,ilk11[1,r],2]+1;
t1g[b]:=r;
end;{91..100}
end;{case}
repeat
as:=random(10)+2;
until as<>t1g[b];
fis[a,ilk11[1,as],3]:=fis[a,ilk11[1,as],3]+1;
end;{for b}
end;{if takimlar..}
if takimlar[a]=dep then begin
if evgol=0 then fis[a,1,2]:=fis[a,1,2]+1;
for b:=1 to 11 do fis[a,ilk11[2,b],1]:=fis[a,ilk11[2,b],1]+1;
if depgol>0 then for b:=1 to depgol do begin
g:=random(100)+1;
case g of
1..60:begin
r:=random(sistem2[3])+sistem2[1]+sistem2[2]+2;
fis[a,ilk11[2,r],2]:=fis[a,ilk11[2,r],2]+1;
t2g[b]:=r;
end;{1..10}
61..90:begin
r:=random(sistem2[2])+sistem2[1]+2;
fis[a,ilk11[2,r],2]:=fis[a,ilk11[2,r],2]+1;
t2g[b]:=r;
end;{61..90}
91..100:begin
r:=random(sistem2[1])+2;
fis[a,ilk11[2,r],2]:=fis[a,ilk11[2,r],2]+1;
t2g[b]:=r;
end;{91..100}
end;{case}
repeat
as:=random(10)+2;
until as<>t2g[b];
fis[a,ilk11[2,as],3]:=fis[a,ilk11[2,as],3]+1;
end;{for b}
end;{if takimlar}
end;{for a}
{Gollerin dakikalarç belirleniyor}
for a:=1 to evgol do begin
repeat
dogru:=true;
gold1[a]:=random(90)+1;
if a>1 then for b:=1 to a-1 do begin
if ((gold1[b]-gold1[a]) in[0,1]) or ((gold1[a]-gold1[b]) in[0,1]) then dogru:=false;
end;
until dogru;
end;
for a:=1 to depgol do begin
repeat
dogru:=true;
gold2[a]:=random(90)+1;
if a>1 then for b:=1 to a-1 do begin
if ((gold2[b]-gold2[a]) in[0,1]) or ((gold2[a]-gold2[b]) in[0,1]) then dogru:=false;
end;
for b:=1 to evgol do begin
if ((gold1[b]-gold2[a]) in[0,1]) or ((gold2[a]-gold1[b]) in[0,1]) then dogru:=false;
end;
until dogru;
end;
{Gollerin dakikalarç belirlendi}
{Kartlar Belirleniyor
for a:=1 to 18 do if takimlar[A]=EV then begin doku(a);b:=a;end;
c1:=0;
for a:=1 to kayit.tfs do if (kayit.fm[a] in['D','d']) and (fis[b,a,6]=0) then c1:=c1+1;
c1:=c1-3;
if c1>0 then for a:=1 to c1 do begin
r:=random(1000);
if r in[800..1000] then begin
sk[1,1,1]:=random(87)+4;
end;
end;
Kartlar Belirlendi}
c1:=0;c2:=0;
gotoxy(28,6);write('òLK YARI       ');
dtd:=dakikabirimi;
for a:=1 to 90 do begin
gotoxy(16,8);write(a:2);if a<10 then begin gotoxy(16,8);write('0');end;
for b:=1 to 8 do if gold1[b]=a then begin
for c:=1 to 18 do if takimlar[c]=ev then doku(c);
c1:=c1+1;
for c:=1 to 3 do begin
bekle(20);
renk(kayit.tr[1],kayit.tr[2]);gotoxy(16,c1+10);write(kayit.fi[ilk11[1,t1g[c1]]]:20);gotoxy(37,c1+10);write(a:2);
bekle(20);
renk(lightgray,0);gotoxy(16,c1+10);write(kayit.fi[ilk11[1,t1g[c1]]]:20);gotoxy(37,c1+10);write(a:2);
end;{for c}
gotoxy(38,10);write(c1);
end;{for b}
for b:=1 to 8 do if gold2[b]=a then begin
for c:=1 to 18 do if takimlar[c]=dep then doku(c);
c2:=c2+1;
for c:=1 to 3 do begin
bekle(20);
renk(kayit.tr[1],kayit.tr[2]);gotoxy(43,c2+10);write(a);
gotoxy(46,c2+10);write(' ':20);
gotoxy(46,c2+10);write(kayit.fi[ilk11[2,t2g[c2]]]);
bekle(20);
renk(lightgray,0);gotoxy(43,c2+10);write(a);
gotoxy(46,c2+10);write(' ':20);
gotoxy(46,c2+10);write(kayit.fi[ilk11[2,t2g[c2]]]);
end;{for c}
gotoxy(43,10);write(c2);
end;{for b}
if a=45 then BEGIN gotoxy(28,6);write('òLK YARI SONUCU');
gotoxy(6,22);write('òkinci yarç iáin ENTER''a basçnçz...');tusbekle(#13);
gotoxy(6,22);write(' ':60);
gotoxy(28,6);write('òKòNCò YARI    ');end;
if a=90 then BEGIN gotoxy(28,6);write('MAÄ SONUCU     ');
gotoxy(6,22);write('Devam etmek iáin ENTER''a basçnçz...');tusbekle(#13);
gotoxy(4,22);write(' ':71);end;
if keypressed then begin
chare:=readkey;
if chare='+' then if dakikabirimi>1 then dakikabirimi:=dakikabirimi-1;
if chare='-' then if dakikabirimi<20 then dakikabirimi:=dakikabirimi+1;
end;
bekle(dakikabirimi);
end;{for a}
dakikabirimi:=dtd;
satn:=8;
for a:=15 to 66 do for b:=10 to 19 do begin gotoxy(a,b);write(' ');end;
gotoxy(21,8);write(' ':40);
gotoxy(16,8);write('  ');
gotoxy(27,6);write(' ':30);
goto maciatla;
end;{oynandç}
if macsay=1 then satn:=9 else satn:=satn+1;
gotoxy(17,1+satn);write(ev:20,' ',evgol,' ƒƒ ',depgol,' ',dep);
for a:=1 to 18 do if takimlar[a]=ev then doku(a);
gotoxy(4,22);write(' ':71);
gotoxy(37-length(ev),22);write(ev,' ',evgol,' ƒƒ ',depgol,' ',dep);
if macsay=9 then begin gotoxy(4,22);write(' ':71);gotoxy(6,22);
write('Devam Etmek iáin Enter''a Basçnçz...');tusbekle(#13);end;
{Oynayan futbolcularçn oynadçßç maá sayçsç ve attçßç gol sayçsç yazçlçyor}
for a:=1 to 18 do begin
if takimlar[a]=ev then begin
for b:=1 to 11 do fis[a,ilk11[1,b],1]:=fis[a,ilk11[1,b],1]+1;
if evgol>0 then for b:=1 to evgol do begin
g:=random(100)+1;
case g of
1..60:begin
r:=random(sistem1[3])+sistem1[1]+sistem1[2]+2;
fis[a,ilk11[1,r],2]:=fis[a,ilk11[1,r],2]+1;
end;{1..10}
61..90:begin
r:=random(sistem1[2])+sistem1[1]+2;
fis[a,ilk11[1,r],2]:=fis[a,ilk11[1,r],2]+1;
end;{61..90}
91..100:begin
r:=random(sistem1[1])+2;
fis[a,ilk11[1,r],2]:=fis[a,ilk11[1,r],2]+1;
end;{1..10}
end;{case}
repeat
as:=random(10)+2;
until as<>r;
fis[a,ilk11[1,as],3]:=fis[a,ilk11[1,as],3]+1;
end;{for b}
if depgol=0 then fis[a,1,2]:=fis[a,1,2]+1;
end;{if takimlar..}
if takimlar[a]=dep then begin
for b:=1 to 11 do fis[a,ilk11[2,b],1]:=fis[a,ilk11[2,b],1]+1;
if depgol>0 then for b:=1 to depgol do begin
g:=random(100)+1;
case g of
1..60:begin
r:=random(sistem2[3])+sistem2[1]+sistem2[2]+2;
fis[a,ilk11[2,r],2]:=fis[a,ilk11[2,r],2]+1;
end;{1..10}
61..90:begin
r:=random(sistem2[2])+sistem2[1]+2;
fis[a,ilk11[2,r],2]:=fis[a,ilk11[2,r],2]+1;
end;{61..90}
91..100:begin
r:=random(sistem2[1])+2;
fis[a,ilk11[2,r],2]:=fis[a,ilk11[2,r],2]+1;
end;{1..10}
end;{case}
repeat
as:=random(10)+2;
until as<>r;
fis[a,ilk11[2,as],3]:=fis[a,ilk11[2,as],3]+1;
end;{for b}
if evgol=0 then fis[a,1,2]:=fis[a,1,2]+1;
end;{if takimlar}
{Yazçlmçütçr...}
end;
{----}
maciatla:;
for a:=1 to 18 do if takimlar[a]=ev then doku(a);
if evgol>depgol then begin
kayit.tp:=kayit.tp+200000;
kayit.durum[2]:=kayit.durum[2]+1;
kayit.durum[7]:=kayit.durum[7]+3;
end;
if evgol=depgol then begin
kayit.tp:=kayit.tp+100000;
kayit.durum[3]:=kayit.durum[3]+1;
kayit.durum[7]:=kayit.durum[7]+1;
end;
if evgol<depgol then begin
kayit.durum[4]:=kayit.durum[4]+1;
end;
for b:=1 to 11 do begin
kayit.tp:=kayit.tp-round((kayit.fp[ilk11[1,b]]*kayit.fp[ilk11[1,b]]*200000+kayit.tsg*30000)/1000);
end;
kayit.durum[5]:=kayit.durum[5]+evgol;
kayit.durum[6]:=kayit.durum[6]+depgol;
for a:=1 to 18 do if takimlar[a]=ev then dyaz(a);
for a:=1 to 18 do if takim[a]=ev then begin
for b:=1 to 7 do durum[a,b]:=kayit.durum[b];
end;
{----}
for a:=1 to 18 do if takimlar[a]=dep then doku(a);
if depgol>evgol then begin
kayit.tp:=kayit.tp+300000;
kayit.durum[2]:=kayit.durum[2]+1;
kayit.durum[7]:=kayit.durum[7]+3;
end;
if depgol=evgol then begin
kayit.tp:=kayit.tp+150000;
kayit.durum[3]:=kayit.durum[3]+1;
kayit.durum[7]:=kayit.durum[7]+1;
end;
if depgol<evgol then begin
kayit.durum[4]:=kayit.durum[4]+1;
end;
for b:=1 to 11 do begin
kayit.tp:=kayit.tp-round((kayit.fp[ilk11[2,b]]*kayit.fp[ilk11[2,b]]*200000+kayit.tsg*30000)/700);
end;
kayit.durum[5]:=kayit.durum[5]+depgol;
kayit.durum[6]:=kayit.durum[6]+evgol;
for a:=1 to 18 do if takimlar[a]=dep then dyaz(a);
for a:=1 to 18 do if takim[a]=dep then begin
for b:=1 to 7 do durum[a,b]:=kayit.durum[b];
end;
{----}
end;{Bir mac sonu}
begin
cerceve('C',3,21,78,23,lightgray);
for a:=15 to 66 do for b:=8 to 20 do begin gotoxy(a,b);write(' ');end;
cerceve('C',14,9,67,20,lightgray);
cerceve('C',14,7,67,9,lightgray);
cerceve('C',14,5,67,7,lightgray);
gotoxy(14,7);write('Ã');gotoxy(67,7);write('π');
gotoxy(14,9);write('Ã');gotoxy(67,9);write('π');
gotoxy(19,7);write('À');gotoxy(19,8);write('∫');gotoxy(19,9);write(' ');
gotoxy(26,5);write('À');gotoxy(26,6);write('∫');gotoxy(26,7);write(' ');
for macsay:=1 to 9 do begin
mac(takimlar[fikstur(oynananhafta,macsay,1)],takimlar[fikstur(oynananhafta,macsay,2)]);
end;{for macsay..}
end;
procedure gucleri_sirala;
var x,y,z:byte;
enbuyuk:real;
gucler:array[1..18] of real;
scl:array[1..18] of byte;
begin
for x:=1 to 18 do gucler[x]:=0;
for x:=1 to 18 do scl[x]:=0;
for x:=1 to 18 do gucsir[x]:=0;
for x:=1 to 18 do begin
doku(x);
for y:=1 to kayit.tfs do gucler[x]:=gucler[x]+kayit.fp[y]*kayit.fp[y];
gucler[x]:=(gucler[x]/kayit.tfs)*11;
gucler[x]:=gucler[x]+kayit.tsg;
end;{for x}
enbuyuk:=0;
z:=0;
repeat
z:=z+1;
for x:=1 to 18 do begin
if gucler[x]>gucler[gucsir[z]] then begin
gucsir[z]:=x;
end;
end;
gucler[gucsir[z]]:=0;
until z=18;
end;{procedure gucleri_sirala}
{--------------------------------}
procedure ayarlari_degistir;
label enbas;
var
a,b:byte;
begin clrscr;
enbas:;
for a:=1 to 20 do menu[a]:='';
menu[1]:='Maáçn Oynanma hçzç Ayarç';
menu[2]:='Otomatik Kayçt Ayarç';
menu[3]:='Geri Dîn';
menuyap('Oyun Ayarlarç',menu,a,lightgray,0,true);
if a=1 then begin
menu[1]:='Äok Hçzlç';
menu[2]:='Hçzlç';
menu[3]:='Normal';
menu[4]:='Yavaü';
menu[5]:='Äok yavaü';
menu[6]:='Geri Dîn';
menuyap('Oyun Hçzçnç Seáiniz',menu,b,lightgray,0,true);
case b of
1:dakikabirimi:=4;
2:dakikabirimi:=10;
3:dakikabirimi:=20;
4:dakikabirimi:=40;
5:dakikabirimi:=100;
6,0:goto enbas;
end;
end;
if a=2 then begin
menu[1]:='Her Maá sonunda kaydet';
menu[2]:='òki Maáta bir kaydet';
menu[3]:='Dîrt Maáta bir kaydet';
menu[4]:='Sezon sonlarçnda kaydet';
menu[5]:='Hiábir zaman Kaydetme';
menu[6]:='Geri dîn';
menuyap('Kaydetme üeklini seáiniz',menu,b,lightgray,0,true);
case b of
1:otokayit:=1;
2:otokayit:=2;
3:otokayit:=4;
4:otokayit:=34;
5:otokayit:=0;
6,0:goto enbas;
end;
end;
end;
{--------------------------------}
begin
assign(dosya,'aktif.tsl');reset(dosya);
yenisezonyapildi:=false;
yenibirsezonabaslangic:;
if ot=1 then takimlari_dosyada_karistir;
takimlari_al;
if ot=1 then pds;{Puan durumu sçfçrlandç!}
gucleri_sirala;
if ot=1 then begin
takimsecmenu;menuyap('LÅtfen Yînetmek istedißiniz Takçmç Seáiniz',menu,takimim,lightgray,black,false);curoff;
for dl:=1 to 18 do begin
doku(dl);
for x:=1 to kayit.tfs do if kayit.fp[x]>0 then kayit.tp:=kayit.tp+round(kayit.fp[x]*kayit.fp[x]*2000);
kayit.tp:=kayit.tp+(kayit.tsg*10000);
dyaz(dl);
end;
for dl:=1 to 11 do takimim11[dl]:=dl;
sistemim[1]:=3;sistemim[2]:=5;sistemim[3]:=2;
oynananhafta:=0;
end;
if ot=2 then begin for x:=1 to 18 do begin
doku(x);for y:=1 to 7 do durum[x,y]:=kayit.durum[y];
end;takimlari_sirala;end;
repeat curoff;
if oynananhafta<35 then oynananhafta:=oynananhafta+1;
if yenisezonyapildi then begin oynananhafta:=oynananhafta-1;yenisezonyapildi:=false;end;
if otokayit>0 then if (oynananhafta/otokayit)=(round(oynananhafta/otokayit)) then oyunu_kaydet(aoa,on);
tekraryaz:;clrscr;
doku(takimim);
cerceve('T',1,1,80,6,lightblue);
gotoxy(round((80-(length(kayit.ti)))/2)-2,1);write(' ');
gotoxy(round((80-(length(kayit.ti)))/2)+length(kayit.ti)+1,1);write(' ');
renk(kayit.tr[1],kayit.tr[2]);
gotoxy(round((80-(length(kayit.ti)))/2)-1,1);write(' ',kayit.ti,' ');
renk(15,0);lowvideo;
gotoxy(3,2);write('(1) Sonraki Maáç Oyna (5) òlk 11''i deßiütir (9) òstatislikler');
gotoxy(3,3);write('(2) Takçm Kadrosu     (6) Transfer                            ');
gotoxy(3,4);write('(3) Puan durumu       (7) Oyunu Kaydet                        ');
gotoxy(3,5);write('(4) FikstÅr           (8) Ayarlar                             ');
renk(lightblue,0);
gotoxy(3,1);write(' FTSL 1.5 ');
gotoxy(61,1);write(' ',yil,'/',yil+1,' Sezonu ');
cerceve('T',1,7,80,10,cyan);
gotoxy(36,8);write('Sçra Oy Gl Br Ml Att Yed Avr Pn');
gotoxy(15,9);write(kayit.ti,' ':(21-length(kayit.ti)),kayit.durum[1]:3,'. '
,(kayit.durum[2]+kayit.durum[3]+kayit.durum[4]):2,' ',kayit.durum[2]:2,' ',kayit.durum[3]:2,' '
,kayit.durum[4]:2,' ',kayit.durum[5]:3,' ',kayit.durum[6]:3,' ',(kayit.durum[5]-kayit.durum[6]):3,' ',kayit.durum[7]:2);
cerceve('T',1,11,40,21,lightred);
if oynananhafta>1 then begin
gotoxy(11,11);write(' ',(oynananhafta-1):2,'. Hafta Sonuálarç ');
for x:=1 to 9 do begin
if (fikstur(oynananhafta-1,x,1)=takimim) or (fikstur(oynananhafta-1,x,2)=takimim) then renk(white,blue);
gotoxy(3,11+x);write(takimlar[fikstur(oynananhafta-1,x,1)],'-',takimlar[fikstur(oynananhafta-1,x,2)],':',
tops[oynananhafta-1,x,1],'-',tops[oynananhafta-1,x,2]);
if (fikstur(oynananhafta-1,x,1)=takimim) or (fikstur(oynananhafta-1,x,2)=takimim) then renk(lightred,0);
end;
end;{if oynananhafta..}
cerceve('T',41,11,80,21,lightred);
if oynananhafta<35 then begin
gotoxy(51,11);write(' ',oynananhafta:2,'. Hafta Programç ');
for x:=1 to 9 do begin
gotoxy(43,11+x);
if (fikstur(oynananhafta,x,1)=takimim) or (fikstur(oynananhafta,x,2)=takimim) then renk(white,blue);
write(takimlar[fikstur(oynananhafta,x,1)],'-',takimlar[fikstur(oynananhafta,x,2)]);
if (fikstur(oynananhafta,x,1)=takimim) or (fikstur(oynananhafta,x,2)=takimim) then renk(lightred,0);
end;
end;
cerceve('T',1,22,80,24,lightblue);
gotoxy(3,23);write('[F1] Yardçm');
gotoxy(68,23);write('[ESC] Äçkçü');
renk(lightgray,0);
repeat
ch:=readkey;
case ch of
'2':begin clrscr;tkg(takimim);goto tekraryaz;end;
'3':begin clrscr;if oynananhafta>1 then begin oynananhafta:=oynananhafta-1;tablogoster;oynananhafta:=oynananhafta+1;
end else tablogoster;goto tekraryaz;end;
'4':begin fikstur_goster(takimim);goto tekraryaz;end;
'5':begin sti11b;goto tekraryaz;end;
'7':begin oynkayit;goto tekraryaz;end;
'6':begin
repeat
for x:=1 to 20 do menu[x]:='';
menu[1]:='Kaleci al';
menu[2]:='Futbolcu Al';
menu[3]:='Futbolcu Sat';
menu[4]:='Transfer bÅtáene bak';
menu[5]:='Geri Dîn';
menuyap('Transfer ûeklini Seáin',menu,x,lightgray,0,true);curoff;
if x=1 then kalecial;
if x=2 then futbolcual;
if x=3 then futbolcusat;
if x=4 then parayigoster;
until x in[0,5];
goto tekraryaz;
end;
'8':begin ayarlari_degistir;curoff;goto tekraryaz;end;
'9':begin istatislik(takimim);curoff;goto tekraryaz;end;
#27:begin cerceve('C',10,10,70,12,lightgray);
gotoxy(11,11);write(' ':59);
gotoxy(12,11);write('Äçkmak istedißinizden Emin misiniz..(Evet/Hayçr)..?:');
renk(15,blue);gotoxy(49,11);write('E');gotoxy(54,11);write('H');renk(lightgray,0);
gotoxy(65,11);curon;
repeat
CH01:=READKEY;
until ch01 in['e','h','E','H'];
curoff;
if ch01 in['e','E'] then goto oyundan_cikis;
iF CH01 in['h','H'] then goto tekraryaz;
end;
end;
until ch='1';
if (oynananhafta<=34) and (ch<>'9') then begin hafta_oyna;
gotoxy(4,22);write(' ':70);gotoxy(25,22);write('...Puan Tablosu Hazçrlançyor...');
takimlari_sirala;tablogoster;end;
until (oynananhafta=35);
for x:=1 to 18 do begin
doku(x);
if kayit.durum[1]=1 then kayit.tp:=kayit.tp+20000000;
if kayit.durum[1]=2 then kayit.tp:=kayit.tp+12000000;
if kayit.durum[1]=3 then kayit.tp:=kayit.tp+7000000;
if kayit.durum[1]=4 then kayit.tp:=kayit.tp+5000000;
if kayit.durum[1]=5 then kayit.tp:=kayit.tp+3000000;
if kayit.durum[1]=6 then kayit.tp:=kayit.tp+2000000;
if kayit.durum[1]=7 then kayit.tp:=kayit.tp+1500000;
if kayit.durum[1]=8 then kayit.tp:=kayit.tp+1000000;
if kayit.durum[1]=9 then kayit.tp:=kayit.tp+750000;
if kayit.durum[1]=10 then kayit.tp:=kayit.tp+500000;
if kayit.durum[1]=11 then kayit.tp:=kayit.tp+400000;
if kayit.durum[1]=12 then kayit.tp:=kayit.tp+300000;
if kayit.durum[1]=13 then kayit.tp:=kayit.tp+200000;
if kayit.durum[1]=14 then kayit.tp:=kayit.tp+100000;
if kayit.durum[1]=15 then kayit.tp:=kayit.tp+50000;
for y:=1 to 18 do if x=gucsir[y] then z:=y;
if kayit.durum[1]=1 then begin
for y:=1 to kayit.tfs do begin
if z=1 then if (kayit.fp[y]-kayit.fpi[y])>2 then
kayit.fp[y]:=kayit.fp[y]+0.15*(fis[x,y,1]/34) else
kayit.fp[y]:=kayit.fp[y]+0.35*(fis[x,y,1]/34);
if z=2 then if (kayit.fp[y]-kayit.fpi[y])>2 then
kayit.fp[y]:=kayit.fp[y]+0.2*(fis[x,y,1]/34) else
kayit.fp[y]:=kayit.fp[y]+0.5*(fis[x,y,1]/34);
if z=3 then if (kayit.fp[y]-kayit.fpi[y])>2 then
kayit.fp[y]:=kayit.fp[y]+0.3*(fis[x,y,1]/34) else
kayit.fp[y]:=kayit.fp[y]+0.75*(fis[x,y,1]/34);
if z>3 then if (kayit.fp[y]-kayit.fpi[y])>2 then
kayit.fp[y]:=kayit.fp[y]+(z+4)*0.05*(fis[x,y,1]/34) else
kayit.fp[y]:=kayit.fp[y]+(z+4)*0.11*(fis[x,y,1]/34);
if kayit.fp[y]>10 then kayit.fp[y]:=10;
if kayit.fp[y]<2 then kayit.fp[y]:=2;
if (kayit.fp[y]-kayit.fpi[y])>3 then kayit.fp[y]:=kayit.fpi[y]+3;
if (kayit.fpi[y]-kayit.fp[y])>3 then kayit.fp[y]:=kayit.fpi[y]-3;
end;{for y}
end;{if}
if (((kayit.durum[1]-z)>1) or ((z-kayit.durum[1])>1)) and (kayit.durum[1]>1) then begin
for y:=1 to kayit.tfs do begin
if ((kayit.fp[y]-kayit.fpi[y])>2) or ((kayit.fpi[y]-kayit.fp[y])>2) then
kayit.fp[y]:=kayit.fp[y]+(((z-kayit.durum[1])/2)*0.1*(fis[x,y,1]/34)) else
kayit.fp[y]:=kayit.fp[y]+(((z-kayit.durum[1])/2)*0.2*(fis[x,y,1]/34));
if kayit.fp[y]>10 then kayit.fp[y]:=10;
if kayit.fp[y]<2 then kayit.fp[y]:=2;
if (kayit.fp[y]-kayit.fpi[y])>3 then kayit.fp[y]:=kayit.fpi[y]+3;
if (kayit.fpi[y]-kayit.fp[y])>3 then kayit.fp[y]:=kayit.fpi[y]-3;
end;{for y:=..}
end;{if ((k..}
dyaz(x);
end;{for x}
yeni_bir_sezon_hazirla;yil:=yil+1;
yenisezonyapildi:=true;
oynananhafta:=1;
goto yenibirsezonabaslangic;
oyundan_cikis:;
close(dosya);
end;
{-------------------------}
procedure oyun_adini_al;
var
d:file of dosya_kaydetme_tipi;
k:dosya_kaydetme_tipi;
a:byte;
begin clrscr;
gotoxy(25,1);write('..Yeni Bir Oyuna Hoügeldiniz..');
bilgioku('LÅtfen Oyun òsmini Giriniz..: ',aoa,1,3,38,false);
assign(d,'oynkayit.tsl');reset(d);
if filesize(d)<18 then on:=filesize(d) else begin clrscr;
for a:=0 to filesize(d) do begin
seek(d,a);read(d,k);
gotoxy(1,a+1);write((a+1):2,'-> ',k.koi);
end;
gotoxy(1,20);
writeln('Kayçtlç Oyun Sayçsç Dolu oldußundan yeni oyununuzu kaydetmek iáin yukardaki');
writeln('Kaydedilmiü oyunlardan birisini iptal etmek zorundasçnçz...                ');
writeln;
write('östÅne yazmak istedißiniz oyunun înÅndeki numarayç giriniz..:');
ioku(63,23,a,18,1);
on:=a-1;
end;
close(d);
end;
{-------------------------}
Procedure Hakkinda;
Var
bt:Char;
Begin Clrscr;
Curoff;
cerceve('T',15,10,66,17,LightRed);
Textcolor(LightBlue);
Gotoxy(55,10);Write(' FTSL v1.5 ');
Gotoxy(17,11);Write('FifaCmTr TÅrkiye SÅper Ligi Menajerlik Oyunu  ');
Gotoxy(17,12);Write('Son gÅncelleme : 06.01.2004                   ');
Gotoxy(17,13);Write('Programlayan   : Halil òbrahim Kalkan         ');
Gotoxy(17,14);Write('E-posta        : halilibrahimkalkan@yahoo.com ');
Gotoxy(17,15);Write('Web Sitesi     : http://www.FifaCmTr.com      ');
Gotoxy(17,16);Write('                 http://www.kalkan.cjb.net    ');
bt:=Readkey;
Curon;
End;
{-------------------------}
begin
window(1,1,80,26);
clrscr;
takimim:=0;oynananhafta:=0;
dakikabirimi:=10;
otokayit:=0;
randomize;
ana_menu_hazirla;
repeat
ana_menu_hazirla;
menuyap('FifaCmTr TÅrkiye SÅper Ligi Sezon 2004/2005',menu,secim,Lightgray,Black,False);
case secim of
1:begin gotoxy(25,10);write('LÅtfen Bekleyin, Oyun Hazçrlançyor...');
ot:=1;yenioyun;oyun_adini_al;curoff;oyunu_oyna;end;
2:begin ot:=2;oyun_yukle;curoff;oyunu_oyna;end;
3:begin assign(dosya,'aktif.tsl');reset(dosya);takimlari_al;close(dosya);takbil;end;
4:Hakkinda;
end;
until secim=5;
clrscr;
curon;
end.