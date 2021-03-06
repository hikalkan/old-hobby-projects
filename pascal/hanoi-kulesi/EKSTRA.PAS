Unit ekstra;
{

Turbo Pascal i�in Ekstra Unit Version 1.0

---> Bu unit ve i�erisindeki t�m Procedure ve Function'lar
Halil �brahim Kalkan taraf�ndan yap�lm��t�r. Bu yaz�y� ve di�er a��klama
yaz�lar�n� silmemek ve yard�m dosyas�n� da yan�nda vermek �art�yla
her t�rl� kullan�m�, da��t�m� ve kopyalanmas� serbesttir. Ancak sat�lmas�
yasakt�r.

---> Unit i�indeki bir�ok fonksyonun ve procedure'�n �rne�ini exornek.pas
dosyas�nda bulabilirsiniz.

---> K�sa yard�m i�in ekyardim.txt dosyas�na bakabilirsiniz.

---> Bana ula�mak i�in;

     �sim        : Halil �brahim Kalkan
     e-posta     : hikalkan@kalkan.cjb.net
     web adresi  : http://www.kalkan.cjb.net
}
INTERFACE
USES CRT,DOS;
Type
Menu_Tipi=Array[1..23] Of String;
Procedure curon;
{�mle�'in ekranda g�r�lmesini sa�lar}
Procedure curoff;
{�mle�'in ekranda g�r�lmemesini sa�lar}
Function BuyukHarf(s:string):string;
{Verilen bir string ifadenin b�t�n harflerini b�y�k harfe �evirir. En �nemli
fark� t�rk�e karekterleri de tan�r. �rne�in:
BuyukHarf('Sa�oLas�n, iyi ki vars�n') fonksyonu "SA�OLASIN, �Y� K� VARSIN" de�erini verir}
Procedure EkranTipi(Var Ekran:Word);
{Bilgisayar�n Ekran tipini(renkli ya da renksiz) Ekran de�i�kenine aktar�r.
Tek ba��na kullan�m bir i�e yaramaz. Bunu EkranSakla ve EkranGetir prosed�r-
leri i�in yapt�m.}
Procedure EkranSakla(Var ekr:pointer);
{O anki ekran� ekr de�i�kenine aktar�r. Daha sonra Ekrangetir prosed�r� ile
saklanan ekran tekrar getirilir. En s�k kullan�lan prosed�rlerden birisidir.
EKR de�i�keni pointer bir de�i�kendir ve program�n ba��nda var etiketiyle
pointer olarak tan�manm�� olmas� gerekir.}
Procedure EkranGetir(Var ekr:pointer);
{Daha �nceden EkranSakla prosed�r� ile ekr de�i�kenine saklanm�� olan
ekran g�r�nt�s�n� tekrar ekrana getirir.}
Procedure Renk(Y,Z:Byte);
{Hem yaz� hem de zemin rengini beraber de�i�tirmek i�in kullan�labilir.
Y: yaz� rengi ve Z: Zemin rengi. �rne�in;
Renk(White,Black); ya da Renk(15,0); �kisi de ayn� i�i yapar.}
Procedure Cerceve(Sek:Char;X1,Y1,X2,Y2,R:Byte;golge:boolean);
{Ekrana dikd�rtgen �izer. De�i�kenler;
Sek: 'T' ise tek, 'C' ise �ift �izgiyle �izer.
X1,Y1 sol �st, X2,Y2 sa� alt kordinatlar� ve R: �er�evenin Rengini belirler.
golge: �er�evenin g�lgesinin olmas�n� sa�lar. Ancak e�er x2 de�eri 80 ise
bunun mutlaka "false" olmas� laz�m. G�lge, e�er arkaplanda yaz� bir�eyler
varsa, i�e yarayabilir.
�rne�in Cerceve('C',1,1,80,20,white,false); ya da Cerceve('C',10,7,40,14,white,true); gibi..}
Function Tab(C,S:Byte):Char;
{�mle�i �stenilen kordinatlara g�t�r�. Gotoxy'den fark� Write deyimi
i�erisinde kullan�labilmesidir. �rne�in;
Gotoxy(1,1);Write('Halil �brahim',tab(30,1),'Kalkan');
�ok kullan�labilecek (benim �ok kulland���m) bir fonksyondur.}
Procedure Sil(X,Y,X1,Y1:Byte);
{Ekranda istenilen dikd�rtgensel alan� temizler.
x,y sol �st, x1,y2 ise sa� alt k��edir.}
Procedure Bekle(Zaman:Longint);
{istenilen zaman kadar bekler(program� duraksat�r).
Zaman: MiliSaniye Cinsinden ve 1 saniye=100 MiliSaniyedir.}
Procedure Bip(frekans,zaman:Integer);
{Bilgisayar�n Hoparl�r�nden Bip Sesi ��kar�r. Ama bu ses kullan�c�lar�n pek
ho�una gitmez(!) De�i�kenler;
frekans: Sesin frekans�. 1 ile 6000 aras�nda bir de�er olabilir.
Zaman: Sesin uzunlu�u. MiliSaniye Cinsinden.}
procedure TusBekle(t2:char);
{Kullan�c�n�n klavyeden t2 tu�una basmas�n� bekler.
�rne�in TusBekle(#13); komutu Enter'a bas�lmas�n� Bekler.}
Procedure BirTusBekle;
{Kullan�c�n�n herhangi bir tu�a basmas�n� bekler.}
Function BasilanTus:char;
{Kullan�c�n�n bir tu�a basmas�n� bekler ve bas�lan tu�un de�erini verir.
Mesela;
if BasilanTus in['S','s'] then clrscr;
komutu bir tu�a bas�lmas�n� bekler ve e�er s'ye veya S'ye bas�lm��sa ekran�
temizler.}
Procedure MesajVer(Mesaj:String;Tc:Char);
{Kullan�c�ya Sadece "TAMAM" se�ene�i olan bir mesaj iletir ve Enter'a
bas�lmas�n� bekler. De�i�kenler;
Mesaj: G�sterilecek Mesaj. En fazla 74 karekter uzunlu�unda olabilir.
tc: Mesaj �er�evesinin tek mi yoksa �ift �izgili mi olaca��n� belirler.
't' veya 'T' i�in tek, 'c' veya 'C' i�in ise �ift �izgili �er�eve �izer}
Function Onayla(Mesaj:String;Tc:Char;Baslangic:Byte):Boolean;
{Kullan�c�ya Bir Mesaj g�sterir ve kullan�c�dan EVET ya da HAYIR'� se�mesini
ister. Sa� ve Sol ok tu�lar�yla ESC tu�u �al���r. ESC'ye bas�nca ya da HAYIR'�
se�ince sonu� false, EVET'i se�ince ise true olur. De�i�kenler;
Mesaj: G�sterilecek Mesaj. En fazla 74 karekter uzunlu�unda olabilir.
tc: Mesaj �er�evesinin tek mi yoksa �ift �izgili mi olaca��n� belirler.
't' veya 'T' i�in tek, 'c' veya 'C' i�in ise �ift �izgili �er�eve �izer
Baslangic: 1 ya da 0 olabilir. 1 ise ilk aktif se�enek Evet, 0 ise hay�r olur.}
Procedure BilgiOku(Soru:String;Var Son:String;Sut,Sat:Byte;Uz:Byte;Duzenle:Boolean);
{Kullan�c�dan bir string de�i�ken al�r. Readln komutundan �ok daha fonksyonel
ve kullan��l� bir komuttur.
Bilgi giri�i s�ras�nda Delete, Insert, Home, End, sa�-sol ok gibi tu�lar
kullan�labilir. De�i�kenlerin g�revleri �unlard�r:
Soru: Kullan�c�ya Sorulacak Soru.
Son: Kullan�c�n�n cevab�n� tutan de�i�ken. Program�n ba��nda var etiketiyle
tan�mlanm�� bir String de�i�ken olmas� laz�m.
Sut,Sat: Sorunun sorulaca�� kordinatlar.
Uz: Cevab�n olabilece�i maksimum uzunluk.
Duzenle: E�er daha �nce de�eri olan bir de�i�kenin i�eri�ini d�zelttirmek
istiyorsan�z true yap�n. Ancak genellikle False kullan�l�r.
�rnek: Bilgioku('Ad�n�z:',ad,20,5,40,false);}
Procedure Ioku(X,Y:Byte;Var Sonuc:Longint;Enaz,Limit:Longint);
{Kullan�c�dan pozitif bir tamsay� almay� sa�lar. Rakamlar d���nda sadece
delete tu�u �al���r. Bu �ekilde kullan�c� harf giremez. De�i�kenler:
X,Y: Kordinatlar.
Sonuc: Kullan�c�n�n cevab�n� tutan de�i�ken. Program�n ba��nda var etiketiyle
tan�mlanm�� bir Longint de�i�ken olmas� laz�m.
Enaz: Girilebilecek minimum say�.
Limit: Girilebilecek maksimum say�.
�rnek: Gotoxy(1,1);Write('4 ile 73 aras� bir say� gir: ');Ioku(30,1,sayi,4,73);}
Procedure Menuyap(Mi:String;Menu:Menu_Tipi;Tc:Char;En:Byte;RenkC,RenkY:Byte;X,Y:Byte;Escape,EkrSakla:Boolean;Var Secim:Byte);
{menu isimli dizi de�i�kenin elemanlar�ndan bir men� olu�turur ve bir se�enek
se�ildi�i zaman programa secim de�i�keniyle bunu aktar�r. De�i�kenler:
Mi: Men�n�n ba�l���nda g�r�lmesini istedi�iniz isim.
Menu: menu_tipi tipinde (yani maksimum 23 elemanl�) bir dizi de�i�ken. B�t�n
elemanlar� stringlerden olu�uyor. Bu procedure'u kullanmadan �nce men�n�n
elemanlar� menu[1]:='Yeni Oyun'; menu[2]:='....'; gibi tan�mlanmas� laz�m.
Tc: Men�n�n �er�evesinin tek veya �ift olmas�n� belirleyen de�i�ken.
�ift i�in 'C' veya 'c' , tek i�in 'T' veya 't' kullan�l�r.
En: Efekt numaras�. Men�n�n ekranda efektli olarak ��kmas� i�in. 0,1 veya 2
de�erleri alabilir. 0 efektsiz ��kar. Di�erlerini deneyebilirsiniz. Efekt
say�s�n� daha sonra art�raca��m.
RenkC: Men�'n�n �er�evesinin Rengi.
RenkY: Men�'n�n Elemanlar�n�n Rengi.
X,Y: Men�'n�n sol �st k��esinin kordinatlar�.
Escape: ESC'ye bas�nca Men�'den ��k�l�p ��k�lmayaca��n� belirleyebilirsiniz.
True yaparsan�z ESC'ye bas�nca men�'den ��kar ve SECIM de�i�keninin de�eri 0
olur. False yaparsan�z ESC tu�u �al��maz.
EkrSakla: True Olursa Men�'den bir eleman se�ildikten sonra ekren men� �izil-
meden �nceki haline d�ner. False olursa �ylece kal�r.
Secim: Kullan�c� Enter'a bas�p da bir se�ene�i se�ince prosed�r sonlan�r ve
se�ilen eleman�n s�ra numaras�n� secim de�i�kenine aktar�r. Program�n ba��nda
var etiketiyle tan�mlanm�� bir Byte de�i�ken olmas� laz�m.}
Procedure KayanYazi(X,Y:Byte;S1:String;Ar,Hiz:Byte);
{Ekran�n istenilen yerinden istenilen yaz�y� belirli aral�k i�erisinde
kayarak ge�irir. De�i�kenler;
X,Y: Ba�lang�� kordinatlar�.
S1: Yaz�lacak yaz�.
Ar: G�sterilecek aral�k.
H�z: Kayma h�z�. 1 en h�zl�, 100 en yava�.
Benim tavsiyem 10 idealdir. �rnek program kodu:
Kayanyazi(10,1,'Bu program Halil �brahim Kalkan Taraf�ndan yap�lm��t�r. Web sitem: http://www.kalkan.cjb.net',30,10);
Not: Yaz� kayarken bir tu�a bas�l�rsa iptal edilebilir.}
Function DosyaVarMi(DosyaAdi: String): Boolean;
{Bir dosyan�n var olup olmad���n� konrol eder. E�er varsa de�eri true, yoksa
de�eri false olur. �rne�in;
if dosyavarmi('deneme.txt') then writeln('M�jde! Dosya Varm��'); gibi..}
Procedure HighScor(D:String;P:Longint);
{Kullanmas� son derece kolay bir highscore (y�ksek puan) prosed�r�.
Oyunlar�n�zda ya da ba�ka programlar�n�zda �ok kolay bir �ekilde
kullanabilirsiniz. De�i�kenler;
D: Highscor dosyas�n�n ad�. E�er daha �nce b�yle bir dosya varsa yeni scor
bu dosyadaki kay�tlarla kar��la�t�r�l�r ve e�er dereceye girmi�se kullan�c�dan
isim ister. �smi yaz�nca Top 15 listesini g�sterir. Daha �nce bu dosya yoksa
yeni dosya yapar ve i�ine ilk s�raya bu oyuncuyu koyar.
P: Oyuncunun yapt��� puan.
�rne�in;
Highcrore('skor.dat',5000);
komutu skor.dat dosyas�n�n olup olmad���na bakar. E�er varsa i�erisindeki
derecelerden 5000'den k���k olan varsa yeni skoru o s�raya koyar ve
di�erlerini bir s�ra a�a�� kayd�r�r. E�er b�yle bir dosya yoksa olu�turur ve
ilk isim olarak da oyuncudan bir isim al�r ve bunu 1. s�raya koyar.}
Function int2str(Sayi:LongInt):String;
{Say�sal bir de�eri String ifadeye �evirip sonucu verir.}
Function str2int(s:String):LongInt;
{String bir de�eri Say�sal ifadeye �evirip sonucu verir.}
Function Us(s,u:Integer):LongInt;
{s say�s�n�n u. kuvvetinin de�erini verir}
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
'�':s[a]:='�';
'i':s[a]:='�';
'�':s[a]:='I';
'�':s[a]:='�';
'�':s[a]:='�';
'�':s[a]:='�';
'�':s[a]:='�';
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
#75,^S : IF POZ>1 THEN POZ:=POZ-1; {Sol Ok Tu�u}
#77,^D : IF POZ<UZ THEN POZ:=POZ+1; {Sa� Ok Tu�u}
#82:BEGIN {Insert Tu�u}
INS:=NOT INS;
IF INS THEN CUR('K') ELSE CUR('B');
END;
#83:BEGIN DELETE(S,POZ,1);S:=S+' ';END; {Delete Tu�u}
#71:POZ:=1; {Home Tu�u}
#73:POZ:=1; {Page Up Tu�u}
#81:POZ:=UZ; {Page Down Tu�u}
#79:begin {End Tu�u}
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
eu,es:byte;{eu:En uzun eleman�n uzunlu�u,es:eleman say�s�}
solust,sagust,solalt,sagalt,sagsol,altust:char;{�izgiler}
ok:byte;{Se�ili Se�ene�in yerini tutar}
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
solust:='�';solalt:='�';
sagust:='�';sagalt:='�';
sagsol:='�';altust:='�';
mi:='�'+mi;
end else begin
solust:='�';solalt:='�';
sagust:='�';sagalt:='�';
sagsol:='�';altust:='�';
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
gotoxy(x-1,y);write('�');
gotoxy(x+ar,y);write('�');
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
WRITEln('Top 15 Listesine Girmeye Hak Kazand�n�z...');writeln;
bilgioku('L�tfen �sminizi Giriniz.....: ',ism,1,2,40,false);
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
WRITELN('SR                  �S�M                PUAN');
WRITELN('--    ------------------------------ ----------');
FOR X:=1 TO 15 DO BEGIN
WRITELN(X:2,' -> ',KAYIT.IS[X]:30,' ',KAYIT.PN[X]:10);
END;
WRITE('��k�� i�in Enter''a Bas�n�z...');tusbekle(#13);
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