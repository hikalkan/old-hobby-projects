Unit ekstra;
{

Turbo Pascal iáin Ekstra Unit Version 1.0

---> Bu unit ve iáerisindeki tÅm Procedure ve Function'lar
Halil òbrahim Kalkan tarafçndan yapçlmçütçr. Bu yazçyç ve dißer aáçklama
yazçlarçnç silmemek ve yardçm dosyasçnç da yançnda vermek üartçyla
her tÅrlÅ kullançmç, daßçtçmç ve kopyalanmasç serbesttir. Ancak satçlmasç
yasaktçr.

---> Unit iáindeki biráok fonksyonun ve procedure'Ån îrneßini exornek.pas
dosyasçnda bulabilirsiniz.

---> Kçsa yardçm iáin ekyardim.txt dosyasçna bakabilirsiniz.

---> Bana ulaümak iáin;

     òsim        : Halil òbrahim Kalkan
     e-posta     : hikalkan@kalkan.cjb.net
     web adresi  : http://www.kalkan.cjb.net
}
INTERFACE
USES CRT,DOS;
Type
Menu_Tipi=Array[1..23] Of String;
Procedure curon;
{òmleá'in ekranda gîrÅlmesini saßlar}
Procedure curoff;
{òmleá'in ekranda gîrÅlmemesini saßlar}
Function BuyukHarf(s:string):string;
{Verilen bir string ifadenin bÅtÅn harflerini bÅyÅk harfe áevirir. En înemli
farkç tÅrkáe karekterleri de tançr. ôrneßin:
BuyukHarf('SaßoLasçn, iyi ki varsçn') fonksyonu "SA¶OLASIN, òYò Kò VARSIN" deßerini verir}
Procedure EkranTipi(Var Ekran:Word);
{Bilgisayarçn Ekran tipini(renkli ya da renksiz) Ekran deßiükenine aktarçr.
Tek baüçna kullançm bir iüe yaramaz. Bunu EkranSakla ve EkranGetir prosedÅr-
leri iáin yaptçm.}
Procedure EkranSakla(Var ekr:pointer);
{O anki ekranç ekr deßiükenine aktarçr. Daha sonra Ekrangetir prosedÅrÅ ile
saklanan ekran tekrar getirilir. En sçk kullançlan prosedÅrlerden birisidir.
EKR deßiükeni pointer bir deßiükendir ve programçn baüçnda var etiketiyle
pointer olarak tançmanmçü olmasç gerekir.}
Procedure EkranGetir(Var ekr:pointer);
{Daha înceden EkranSakla prosedÅrÅ ile ekr deßiükenine saklanmçü olan
ekran gîrÅntÅsÅnÅ tekrar ekrana getirir.}
Procedure Renk(Y,Z:Byte);
{Hem yazç hem de zemin rengini beraber deßiütirmek iáin kullançlabilir.
Y: yazç rengi ve Z: Zemin rengi. ôrneßin;
Renk(White,Black); ya da Renk(15,0); òkisi de aynç iüi yapar.}
Procedure Cerceve(Sek:Char;X1,Y1,X2,Y2,R:Byte;golge:boolean);
{Ekrana dikdîrtgen áizer. Deßiükenler;
Sek: 'T' ise tek, 'C' ise áift áizgiyle áizer.
X1,Y1 sol Åst, X2,Y2 saß alt kordinatlarç ve R: Äeráevenin Rengini belirler.
golge: Äeráevenin gîlgesinin olmasçnç saßlar. Ancak eßer x2 deßeri 80 ise
bunun mutlaka "false" olmasç lazçm. Gîlge, eßer arkaplanda yazç birüeyler
varsa, iüe yarayabilir.
ôrneßin Cerceve('C',1,1,80,20,white,false); ya da Cerceve('C',10,7,40,14,white,true); gibi..}
Function Tab(C,S:Byte):Char;
{òmleái òstenilen kordinatlara gîtÅrÅ. Gotoxy'den farkç Write deyimi
iáerisinde kullançlabilmesidir. ôrneßin;
Gotoxy(1,1);Write('Halil òbrahim',tab(30,1),'Kalkan');
Äok kullançlabilecek (benim áok kullandçßçm) bir fonksyondur.}
Procedure Sil(X,Y,X1,Y1:Byte);
{Ekranda istenilen dikdîrtgensel alanç temizler.
x,y sol Åst, x1,y2 ise saß alt kîüedir.}
Procedure Bekle(Zaman:Longint);
{istenilen zaman kadar bekler(programç duraksatçr).
Zaman: MiliSaniye Cinsinden ve 1 saniye=100 MiliSaniyedir.}
Procedure Bip(frekans,zaman:Integer);
{Bilgisayarçn HoparlîrÅnden Bip Sesi áçkarçr. Ama bu ses kullançcçlarçn pek
hoüuna gitmez(!) Deßiükenler;
frekans: Sesin frekansç. 1 ile 6000 arasçnda bir deßer olabilir.
Zaman: Sesin uzunlußu. MiliSaniye Cinsinden.}
procedure TusBekle(t2:char);
{Kullançcçnçn klavyeden t2 tuüuna basmasçnç bekler.
ôrneßin TusBekle(#13); komutu Enter'a basçlmasçnç Bekler.}
Procedure BirTusBekle;
{Kullançcçnçn herhangi bir tuüa basmasçnç bekler.}
Function BasilanTus:char;
{Kullançcçnçn bir tuüa basmasçnç bekler ve basçlan tuüun deßerini verir.
Mesela;
if BasilanTus in['S','s'] then clrscr;
komutu bir tuüa basçlmasçnç bekler ve eßer s'ye veya S'ye basçlmçüsa ekranç
temizler.}
Procedure MesajVer(Mesaj:String;Tc:Char);
{Kullançcçya Sadece "TAMAM" seáeneßi olan bir mesaj iletir ve Enter'a
basçlmasçnç bekler. Deßiükenler;
Mesaj: Gîsterilecek Mesaj. En fazla 74 karekter uzunlußunda olabilir.
tc: Mesaj áeráevesinin tek mi yoksa áift áizgili mi olacaßçnç belirler.
't' veya 'T' iáin tek, 'c' veya 'C' iáin ise áift áizgili áeráeve áizer}
Function Onayla(Mesaj:String;Tc:Char;Baslangic:Byte):Boolean;
{Kullançcçya Bir Mesaj gîsterir ve kullançcçdan EVET ya da HAYIR'ç seámesini
ister. Saß ve Sol ok tuülarçyla ESC tuüu áalçüçr. ESC'ye basçnca ya da HAYIR'ç
seáince sonuá false, EVET'i seáince ise true olur. Deßiükenler;
Mesaj: Gîsterilecek Mesaj. En fazla 74 karekter uzunlußunda olabilir.
tc: Mesaj áeráevesinin tek mi yoksa áift áizgili mi olacaßçnç belirler.
't' veya 'T' iáin tek, 'c' veya 'C' iáin ise áift áizgili áeráeve áizer
Baslangic: 1 ya da 0 olabilir. 1 ise ilk aktif seáenek Evet, 0 ise hayçr olur.}
Procedure BilgiOku(Soru:String;Var Son:String;Sut,Sat:Byte;Uz:Byte;Duzenle:Boolean);
{Kullançcçdan bir string deßiüken alçr. Readln komutundan áok daha fonksyonel
ve kullançülç bir komuttur.
Bilgi giriüi sçrasçnda Delete, Insert, Home, End, saß-sol ok gibi tuülar
kullançlabilir. Deßiükenlerin gîrevleri üunlardçr:
Soru: Kullançcçya Sorulacak Soru.
Son: Kullançcçnçn cevabçnç tutan deßiüken. Programçn baüçnda var etiketiyle
tançmlanmçü bir String deßiüken olmasç lazçm.
Sut,Sat: Sorunun sorulacaßç kordinatlar.
Uz: Cevabçn olabileceßi maksimum uzunluk.
Duzenle: Eßer daha înce deßeri olan bir deßiükenin iáerißini dÅzelttirmek
istiyorsançz true yapçn. Ancak genellikle False kullançlçr.
ôrnek: Bilgioku('Adçnçz:',ad,20,5,40,false);}
Procedure Ioku(X,Y:Byte;Var Sonuc:Longint;Enaz,Limit:Longint);
{Kullançcçdan pozitif bir tamsayç almayç saßlar. Rakamlar dçüçnda sadece
delete tuüu áalçüçr. Bu üekilde kullançcç harf giremez. Deßiükenler:
X,Y: Kordinatlar.
Sonuc: Kullançcçnçn cevabçnç tutan deßiüken. Programçn baüçnda var etiketiyle
tançmlanmçü bir Longint deßiüken olmasç lazçm.
Enaz: Girilebilecek minimum sayç.
Limit: Girilebilecek maksimum sayç.
ôrnek: Gotoxy(1,1);Write('4 ile 73 arasç bir sayç gir: ');Ioku(30,1,sayi,4,73);}
Procedure Menuyap(Mi:String;Menu:Menu_Tipi;Tc:Char;En:Byte;RenkC,RenkY:Byte;X,Y:Byte;Escape,EkrSakla:Boolean;Var Secim:Byte);
{menu isimli dizi deßiükenin elemanlarçndan bir menÅ oluüturur ve bir seáenek
seáildißi zaman programa secim deßiükeniyle bunu aktarçr. Deßiükenler:
Mi: MenÅnÅn baülçßçnda gîrÅlmesini istedißiniz isim.
Menu: menu_tipi tipinde (yani maksimum 23 elemanlç) bir dizi deßiüken. BÅtÅn
elemanlarç stringlerden oluüuyor. Bu procedure'u kullanmadan înce menÅnÅn
elemanlarç menu[1]:='Yeni Oyun'; menu[2]:='....'; gibi tançmlanmasç lazçm.
Tc: MenÅnÅn áeráevesinin tek veya áift olmasçnç belirleyen deßiüken.
Äift iáin 'C' veya 'c' , tek iáin 'T' veya 't' kullançlçr.
En: Efekt numarasç. MenÅnÅn ekranda efektli olarak áçkmasç iáin. 0,1 veya 2
deßerleri alabilir. 0 efektsiz áçkar. Dißerlerini deneyebilirsiniz. Efekt
sayçsçnç daha sonra artçracaßçm.
RenkC: MenÅ'nÅn Äeráevesinin Rengi.
RenkY: MenÅ'nÅn Elemanlarçnçn Rengi.
X,Y: MenÅ'nÅn sol Åst kîüesinin kordinatlarç.
Escape: ESC'ye basçnca MenÅ'den áçkçlçp áçkçlmayacaßçnç belirleyebilirsiniz.
True yaparsançz ESC'ye basçnca menÅ'den áçkar ve SECIM deßiükeninin deßeri 0
olur. False yaparsançz ESC tuüu áalçümaz.
EkrSakla: True Olursa MenÅ'den bir eleman seáildikten sonra ekren menÅ áizil-
meden înceki haline dîner. False olursa îylece kalçr.
Secim: Kullançcç Enter'a basçp da bir seáeneßi seáince prosedÅr sonlançr ve
seáilen elemançn sçra numarasçnç secim deßiükenine aktarçr. Programçn baüçnda
var etiketiyle tançmlanmçü bir Byte deßiüken olmasç lazçm.}
Procedure KayanYazi(X,Y:Byte;S1:String;Ar,Hiz:Byte);
{Ekrançn istenilen yerinden istenilen yazçyç belirli aralçk iáerisinde
kayarak geáirir. Deßiükenler;
X,Y: Baülangçá kordinatlarç.
S1: Yazçlacak yazç.
Ar: Gîsterilecek aralçk.
Hçz: Kayma hçzç. 1 en hçzlç, 100 en yavaü.
Benim tavsiyem 10 idealdir. ôrnek program kodu:
Kayanyazi(10,1,'Bu program Halil òbrahim Kalkan Tarafçndan yapçlmçütçr. Web sitem: http://www.kalkan.cjb.net',30,10);
Not: Yazç kayarken bir tuüa basçlçrsa iptal edilebilir.}
Function DosyaVarMi(DosyaAdi: String): Boolean;
{Bir dosyançn var olup olmadçßçnç konrol eder. Eßer varsa deßeri true, yoksa
deßeri false olur. ôrneßin;
if dosyavarmi('deneme.txt') then writeln('MÅjde! Dosya Varmçü'); gibi..}
Procedure HighScor(D:String;P:Longint);
{Kullanmasç son derece kolay bir highscore (yÅksek puan) prosedÅrÅ.
Oyunlarçnçzda ya da baüka programlarçnçzda áok kolay bir üekilde
kullanabilirsiniz. Deßiükenler;
D: Highscor dosyasçnçn adç. Eßer daha înce bîyle bir dosya varsa yeni scor
bu dosyadaki kayçtlarla karüçlaütçrçlçr ve eßer dereceye girmiüse kullançcçdan
isim ister. òsmi yazçnca Top 15 listesini gîsterir. Daha înce bu dosya yoksa
yeni dosya yapar ve iáine ilk sçraya bu oyuncuyu koyar.
P: Oyuncunun yaptçßç puan.
ôrneßin;
Highcrore('skor.dat',5000);
komutu skor.dat dosyasçnçn olup olmadçßçna bakar. Eßer varsa iáerisindeki
derecelerden 5000'den kÅáÅk olan varsa yeni skoru o sçraya koyar ve
dißerlerini bir sçra aüaßç kaydçrçr. Eßer bîyle bir dosya yoksa oluüturur ve
ilk isim olarak da oyuncudan bir isim alçr ve bunu 1. sçraya koyar.}
Function int2str(Sayi:LongInt):String;
{Sayçsal bir deßeri String ifadeye áevirip sonucu verir.}
Function str2int(s:String):LongInt;
{String bir deßeri Sayçsal ifadeye áevirip sonucu verir.}
Function Us(s,u:Integer):LongInt;
{s sayçsçnçn u. kuvvetinin deßerini verir}
Function strTersi(Normal:String):String;
IMPLEMENTATION

procedure curon;assembler;
asm
   mov ah,01h
   mov ch,06h
   mov cl,07h
   int 10h
end;

procedure curoff;assembler;
asm
   mov ah,01h
   mov ch,20h
   int 10h
end;

Function BuyukHarf(s:string):string;
var
a:byte;
sb:string;
begin
for a:=1 to length(s) do begin
case s[a] of
'a','b','c','d','e','f','g','h','j','k','l','m','n','o','p','q','r','s','t','u','v','y','x','z':s[a]:=chr(ord(s[a])-32);
'ü':s[a]:='û';
'i':s[a]:='ò';
'ç':s[a]:='I';
'ß':s[a]:='¶';
'Å':s[a]:='ö';
'î':s[a]:='ô';
'á':s[a]:='Ä';
end;
end;
BuyukHarf:=s;
end;

Procedure EkranTipi(Var Ekran:Word);
Begin
if Mem[$0000:$049]=7 then ekran:=$B000 else ekran:=$B800;
end;

Procedure EkranSakla(Var ekr:pointer);
var ekrtip:word;
begin
Ekrantipi(ekrtip);
Getmem(Ekr,4000);
Move(Ptr(Ekrtip,0)^,Ekr^,4000);
end;

Procedure EkranGetir(Var ekr:pointer);
var ekrtip:word;
begin
Ekrantipi(ekrtip);
Move(Ekr^,Ptr(Ekrtip,0)^,4000);
Freemem(Ekr,4000);
end;

PROCEDURE RENK;
BEGIN
TEXTCOLOR(Y);TEXTBACKGROUND(Z);
END;

PROCEDURE CERCEVE;
VAR
I:INTEGER;
CIZGI:STRING[80];
BEGIN
RENK(R,0);SEK:=UPCASE(SEK);
CASE SEK OF
'T':BEGIN
FILLCHAR(CIZGI,X2-X1,#196);
CIZGI[0]:=CHAR(X2-X1-1);
GOTOXY(X1,Y1);WRITE(CHR(218),CIZGI,CHR(191));
FOR I:=Y1+1 TO Y2-1 DO BEGIN
GOTOXY(X1,I);WRITELN(CHR(179));
GOTOXY(X2,I);WRITELN(CHR(179));
END;
GOTOXY(X1,Y2);WRITE(#192,CIZGI,#217);
END;
'C':BEGIN
FILLCHAR(CIZGI,X2-X1,#205);
CIZGI[0]:=CHAR(X2-X1-1);
GOTOXY(X1,Y1);WRITE(CHR(201),CIZGI,CHR(187));
FOR I:=Y1+1 TO Y2-1 DO BEGIN
GOTOXY(X1,I);WRITELN(CHR(186));
GOTOXY(X2,I);WRITELN(CHR(186));
END;
GOTOXY(X1,Y2);WRITE(CHR(200),CIZGI,CHR(188));
END;
END;
if golge then for i:=y1+1 to y2 do begin
gotoxy(x2+1,i);write(' ');
end;
END;

FUNCTION TAB;
BEGIN
WRITE(#8);
IF C>79 THEN C:=79;
IF C<1 THEN C:=1;
IF S>25 THEN C:=25;
IF S<1 THEN C:=1;
GOTOXY(C+1,S);
TAB:=#8;
END;

PROCEDURE SIL;
VAR A,B:BYTE;
BEGIN
FOR A:=X TO X1 DO FOR B:=Y TO Y1 DO begin gotoxy(a,b);WRITE(' ');end;
END;

PROCEDURE BEKLE;
VAR
SA,DK,SN,SL:WORD;
ZMN1,ZMN2:LONGINT;
BEGIN
GetTime(SA,DK,SN,SL);
ZMN1:=SL+SN*100+DK*6000+SA*360000;
REPEAT
GetTime(SA,DK,SN,SL);
ZMN2:=SL+SN*100+DK*6000+SA*360000;
UNTIL ((ZMN2-ZMN1)>ZAMAN) or ((zmn1-zmn2)>zaman);
END;

Procedure Bip;
Begin
Sound(frekans);bekle(zaman);Nosound;
end;

procedure TUSbekle;
var t1:char;
BEGIN
repeat
t1:=READKEY;
until t1=t2;
END;

Procedure BirTusBekle;
var t:char;
begin
t:=readkey;
end;

Function BasilanTus:char;
var t:char;
begin
t:=readkey;
BasilanTus:=t;
end;

Procedure mesajver;
var
ekran:pointer;
genislik:byte;
begin
EkranSakla(Ekran);
if length(mesaj)>10 then genislik:=length(mesaj)+4 else genislik:=14;
Sil(40-round(genislik/2),11,40+round(genislik/2)+1,14);
cerceve(tc,40-round(genislik/2),10,40+round(genislik/2),14,lightblue,true);
renk(lightgray,0);Gotoxy(40-round(length(mesaj)/2),11);write(Mesaj);
renk(white,lightred);gotoxy(37,13);write(' TAMAM ');
tusbekle(#13);renk(lightgray,0);
EkranGetir(Ekran);
end;

Function Onayla(Mesaj:string;tc:char;baslangic:byte):boolean;
var
ekran:pointer;
genislik:byte;
durum:byte;
tus:char;
x,y:byte;
begin
x:=wherex;y:=wherey;
EkranSakla(Ekran);
if baslangic in[0,1] then durum:=baslangic else durum:=1;
if length(mesaj)>24 then genislik:=length(mesaj)+4 else genislik:=28;
Sil(40-round(genislik/2),11,40+round(genislik/2)+1,14);
cerceve(tc,40-round(genislik/2),10,40+round(genislik/2),14,lightblue,true);
renk(lightgray,0);Gotoxy(40-round(length(mesaj)/2),11);write(Mesaj);
gotoxy(33,13);write(' EVET     HAYIR ');
renk(white,lightred);
if durum=1 then begin gotoxy(33,13);write(' EVET ');end;
if durum=0 then begin gotoxy(42,13);write(' HAYIR ');end;
repeat
tus:=readkey;
if (tus=#75) or (tus=#77) then BEGIN
durum:=1-durum;
renk(lightgray,0);gotoxy(33,13);write(' EVET     HAYIR ');
renk(white,lightred);
if durum=1 then begin gotoxy(33,13);write(' EVET ');end;
if durum=0 then begin gotoxy(42,13);write(' HAYIR ');end;
END;
until tus in[#27,#13];
if durum=1 then Onayla:=True;
if (durum=0) or (tus=#27) then Onayla:=False;
renk(lightgray,0);
EkranGetir(Ekran);
gotoxy(x,y);
end;

PROCEDURE BILGIOKU;
VAR
I,POZ,GC,GA:BYTE;
S:STRING;
TUS:CHAR;
INS:BOOLEAN;
PROCEDURE CUR(STYPE:CHAR);
VAR R:REGISTERS;
BEGIN
CASE STYPE OF
'B':BEGIN
WITH R DO BEGIN
AH:=$01;CH:=0;CL:=7;
END;
END;
'K':BEGIN
WITH R DO BEGIN
AH:=$01;CH:=6;CL:=7;
END;
END;
END;
INTR($10,R);
END;
FUNCTION HARF(C:CHAR):BOOLEAN;
BEGIN
IF C IN[#32..#254] THEN
HARF:=TRUE ELSE HARF:=FALSE;
END;
BEGIN
GOTOXY(SUT,SAT);WRITE(SORU,'[','':UZ,']');
SUT:=SUT+LENGTH(SORU)+1;
GOTOXY(SUT,SAT);
POZ:=1;
IF DUZENLE THEN S:=SON ELSE BEGIN S:='';SON:='';END;
FOR I:=LENGTH(S)+1 TO UZ DO S:=S+' ';
INS:=TRUE;
REPEAT
GOTOXY(SUT,SAT);WRITE(S);GOTOXY(POZ+SUT-1,SAT);
TUS:=READKEY;
IF TUS=#0 THEN BEGIN
TUS:=READKEY;
CASE TUS OF
#75,^S : IF POZ>1 THEN POZ:=POZ-1; {Sol Ok Tuüu}
#77,^D : IF POZ<UZ THEN POZ:=POZ+1; {Saß Ok Tuüu}
#82:BEGIN {Insert Tuüu}
INS:=NOT INS;
IF INS THEN CUR('K') ELSE CUR('B');
END;
#83:BEGIN DELETE(S,POZ,1);S:=S+' ';END; {Delete Tuüu}
#71:POZ:=1; {Home Tuüu}
#73:POZ:=1; {Page Up Tuüu}
#81:POZ:=UZ; {Page Down Tuüu}
#79:begin {End Tuüu}
poz:=1;
for ga:=1 to uz do if s[ga]<>' ' then POZ:=ga;
if (poz>1) and (poz<uz) then poz:=poz+1;
end;
END;
END ELSE BEGIN
IF TUS IN[#13,#27] THEN BEGIN
POZ:=1;
ga:=0;
FOR GC:=UZ DOWNTO 1 DO IF S[GC]<>' ' THEN BEGIN GA:=GC;BREAK;END;
SON:='';
IF LENGTH(S)>0 THEN FOR GC:=1 TO GA DO SON:=SON+S[GC];
EXIT;
END;
IF TUS=#8 THEN BEGIN IF POZ>1 THEN begin DELETE(S,POZ-1,1);S:=S+' ';POZ:=POZ-1;end;CONTINUE;END;
IF HARF(TUS) THEN BEGIN
IF INS=FALSE THEN BEGIN
S[POZ]:=TUS;
IF POZ<UZ THEN POZ:=POZ+1;
END;
END;
IF INS=TRUE THEN BEGIN
DELETE(S,UZ,1);
INSERT(TUS,S,POZ);
IF POZ<UZ THEN POZ:=POZ+1;
END;
END;
UNTIL 1=2;
END;{bilgioku}

PROCEDURE IOKU;
LABEL ENBAS;
VAR
INT:BYTE;
TUS:CHAR;
SON:longint;
BEGIN
ENBAS:;
GOTOXY(X,Y);WRITE('[          ]');
GOTOXY(X+10,Y);
SON:=0;
REPEAT
TUS:=READKEY;
IF TUS=#0 THEN BEGIN
tus:=readkey;
CASE TUS OF
#83:BEGIN
IF SON>0 THEN SON:=SON DIV 10;
END;
END;
END ELSE
CASE TUS OF
'0'..'9':BEGIN
IF SON<=99999999 THEN SON:=SON*10+(ORD(TUS)-48);
END;
END;
GOTOXY(X+1,Y);WRITE(SON:10)
UNTIL (TUS=#13);
IF (SON>LIMIT) OR (SON<ENAZ) THEN GOTO ENBAS;
SONUC:=SON;
END;{IOKU SON}

Procedure Menuyap;
label bas,son;
var
a,b:byte;
eu,es:byte;{eu:En uzun elemançn uzunlußu,es:eleman sayçsç}
solust,sagust,solalt,sagalt,sagsol,altust:char;{Äizgiler}
ok:byte;{Seáili Seáeneßin yerini tutar}
tus:char;
ekranim:pointer;
procedure okkoy;
begin
textcolor(0);textbackground(15);
gotoxy(x+2,y+ok);write(menu[ok]);
if eu>length(menu[ok]) then write(' ':(eu-length(menu[ok])));
textcolor(renkY);textbackground(0);
end;
procedure okkaldir;
begin
textcolor(renkY);textbackground(0);
gotoxy(x+2,y+ok);write(menu[ok]);
if eu>length(menu[ok]) then write(' ':(eu-length(menu[ok])));
end;
begin
if EkrSakla then EkranSakla(ekranim);
textcolor(renkC);
if tc in['t','T'] then begin
solust:='⁄';solalt:='¿';
sagust:='ø';sagalt:='Ÿ';
sagsol:='≥';altust:='ƒ';
mi:='ƒ'+mi;
end else begin
solust:='…';solalt:='»';
sagust:='ª';sagalt:='º';
sagsol:='∫';altust:='Õ';
end;{if then else}
for a:=1 to 23 do if menu[a]<>'' then es:=a;
eu:=0;
for a:=1 to es do if length(menu[a])>eu then eu:=length(menu[a]);
if en=0 then begin
gotoxy(x,y);write(solust);for a:=1 to eu+2 do write(altust);write(sagust);
for a:=y+1 to y+es do begin gotoxy(x+eu+3,a);write(sagsol,' ');end;gotoxy(x+eu+3,y+es+1);write(sagalt,' ');
for a:=x+2+eu downto x+1 do begin gotoxy(a,y+es+1);write(altust);end;gotoxy(x,y+es+1);write(solalt);
for a:=y+es downto y+1 do begin gotoxy(x,a);write(sagsol);end;
textcolor(renkY);textbackground(0);
for a:=1 to es do begin
gotoxy(x+1,a+y);write(' ':(eu+2));
gotoxy(x+2,a+y);write(menu[a]);
end;
textcolor(renkC);textbackground(0);
gotoxy(x+2,y);write(mi);
end;
if en=1 then begin
gotoxy(x,y);write(solust);for a:=1 to eu+2 do begin bekle(1);write(altust);end;bekle(1);write(sagust);
for a:=y+1 to y+es do begin bekle(1);gotoxy(x+eu+3,a);write(sagsol,' ');end;bekle(1);gotoxy(x+eu+3,y+es+1);write(sagalt,' ');
for a:=x+2+eu downto x+1 do begin bekle(1);gotoxy(a,y+es+1);write(altust);end;bekle(1);gotoxy(x,y+es+1);write(solalt);
for a:=y+es downto y+1 do begin bekle(1);gotoxy(x,a);write(sagsol);end;
textcolor(renkY);textbackground(0);
for a:=1 to es do begin bekle(3);
gotoxy(x+1,a+y);write(' ':(eu+2));
gotoxy(x+2,a+y);write(menu[a]);
end;
textcolor(renkC);textbackground(0);
gotoxy(x+2,y);write(mi);
end;
if en=2 then begin
mi:=altust+mi;
gotoxy(x,y);write(solust);
gotoxy(x,y+1);write(solalt);
for a:=x+1 to x+2+eu do begin
gotoxy(a,y);write(altust,sagust);
gotoxy(a,y+1);write(altust,sagalt);
if length(mi)>=a-x then begin gotoxy(a,y);write(mi[a-x]);end;
bekle(3);
end;{for}
for a:=y+1 to y+es do begin
gotoxy(x,a);write(sagsol);gotoxy(x,a+1);write(solalt);
gotoxy(x+1,a);write(' ':(eu+2));
gotoxy(x+3+eu,a);write(sagsol,' ');gotoxy(x+3+eu,a+1);write(sagalt,' ');
for b:=x+1 to x+eu+2 do begin gotoxy(b,a+1);write(altust);end;
textcolor(renkY);textbackground(0);
gotoxy(x+1,a);write(' ':(eu+2));
gotoxy(x+2,a);write(menu[a-y]);
bekle(3);
textcolor(renkC);textbackground(0);
end;{for}
end;{if}
if (secim>0) and (secim<=es) then ok:=secim else ok:=1;
okkoy;
BAS:tus:=readkey;
case tus of
#27:if escape then begin secim:=0; goto son; end;
#13:begin secim:=ok; goto son; end;
#71:begin okkaldir; OK:=1; okkoy;end;
#79:begin okkaldir; OK:=ES; okkoy;end;
#72:begin okkaldir; IF OK=1 THEN OK:=ES ELSE OK:=OK-1; okkoy; end;
#80:begin okkaldir; IF OK=ES THEN OK:=1 ELSE OK:=OK+1; okkoy; end;
end;{case}
goto bas;
son:;
if EkrSakla then EkranGetir(ekranim);
end;{Procedure}

PROCEDURE KAYANYAZI;
label iptal;
VAR
A,YER:INTEGER;
S,ST:STRING;
BEGIN
ST:=S1;
FOR A:=1 TO AR DO ST:=' '+ST+' ';
S:='';FOR A:=1 TO AR DO S:=S+' ';
gotoxy(x-1,y);write('€');
gotoxy(x+ar,y);write('€');
FOR YER:=1 TO LENGTH(ST)-AR+1 DO BEGIN
FOR A:=1 TO AR DO S[A]:=ST[A+YER-1];
gotoxy(x,y);write(s);
bekle(hiz);
if keypressed then begin Birtusbekle; goto iptal;end;
END;
iptal:;
END;

function DosyaVarMi(DosyaAdi: String): Boolean;
var
  F: file;
begin
  {$I-}
  Assign(F, DosyaAdi);
  Reset(F);
  Close(F);
  {$I+}
  DosyaVarMi := (IOResult = 0) and (DosyaAdi <> '');
end;

PROCEDURE HIGHSCOR(D:STRING;P:LONGINT);
LABEL SON;
TYPE
KAYIT_TIPI=RECORD
IS:ARRAY[1..15] OF STRING;
PN:ARRAY [1..15] OF LONGINT;
END;
VAR
KAYIT:KAYIT_TIPI;
DOSYA:FILE OF KAYIT_TIPI;
X,Y,Z:BYTE;
 ISM:STRING;
PROCEDURE DOSYAKONTROL;
VAR X:BYTE;
BEGIN
IF dosyavarmi(D)=FALSE THEN BEGIN
ASSIGN(DOSYA,D);
REWRITE(DOSYA);
SEEK(DOSYA,0);
FOR X:=1 TO 15 DO BEGIN KAYIT.IS[X]:=' - ';KAYIT.PN[X]:=0;END;
WRITE(DOSYA,KAYIT);
CLOSE(DOSYA);
END;{if Dosyavarmi..}
END;{DOSYAKONTROL}
BEGIN
DOSYAKONTROL;
ASSIGN(DOSYA,D);RESET(DOSYA);
SEEK(DOSYA,0);read(dosya,kayit);
FOR X:=1 TO 15 DO BEGIN
IF P>KAYIT.PN[X] THEN BEGIN CLRSCR;
WRITEln('Top 15 Listesine Girmeye Hak Kazandçnçz...');writeln;
bilgioku('LÅtfen òsminizi Giriniz.....: ',ism,1,2,40,false);
IF X=15 THEN BEGIN
KAYIT.PN[15]:=P;KAYIT.IS[15]:=ISM;
seek(dosya,0);WRITE(DOSYA,KAYIT);GOTO SON;
END;{IF X=15..}
IF X=14 THEN BEGIN
KAYIT.PN[15]:=KAYIT.PN[14];KAYIT.PN[14]:=P;
KAYIT.IS[15]:=KAYIT.IS[14];KAYIT.IS[14]:=ISM;
seek(dosya,0);WRITE(DOSYA,KAYIT);GOTO SON;
END;{IF X=14..}
FOR Y:=15 DOWNTO (X+1) DO BEGIN
KAYIT.PN[Y]:=KAYIT.PN[Y-1];KAYIT.IS[Y]:=KAYIT.IS[Y-1];
END;{FOR Y:=15..}
KAYIT.PN[X]:=P;KAYIT.IS[X]:=ISM;
seek(dosya,0);WRITE(DOSYA,KAYIT);goto son;
END;{IF P>KAY..}
END;{FOR X:=1..}
SON:;SEEK(DOSYA,0);read(dosya,kayit);CLRSCR;
WRITELN('TOP 15 LISTESI');WRITELN;
WRITELN('SR                  òSòM                PUAN');
WRITELN('--    ------------------------------ ----------');
FOR X:=1 TO 15 DO BEGIN
WRITELN(X:2,' -> ',KAYIT.IS[X]:30,' ',KAYIT.PN[X]:10);
END;
WRITE('Äçkçü iáin Enter''a Basçnçz...');tusbekle(#13);
CLOSE(DOSYA);
END;{highscor}

Function int2str;
Var
Sozel:String;
Begin
Str(Sayi,Sozel);
int2str:=Sozel;
End;{int2str}

Function str2int;
Var
say:Longint;
hat:Word;
Begin
Val(s,say,hat);
str2int:=say;
End;

Function Us;
Begin
If u=0 Then Us:=1 Else Begin
If u=1 Then Us:=s else Us:=s*Us(s,u-1);
End;
End;

Function strTersi;
Var
i               : Byte;
Tersi           : String;
Begin
Tersi:='';
For i:=length(Normal) downto 1 do Tersi:=Tersi+Normal[i];
strTersi:=Tersi;
End;

begin
end.