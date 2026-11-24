UNIT TSLUNIT;
{$IFDEF FPC}{$MODE TP}{$PACKRECORDS 1}{$ENDIF}
INTERFACE
USES {$IFDEF FPC}TSLCRT{$ELSE}CRT{$ENDIF},DOS;
type
MENU_TIPI=ARRAY[1..20] OF STRING[40];
EKRAN_TIPI=ARRAY[1..4000] OF CHAR;
procedure curon;
procedure curoff;
PROCEDURE EKRAN_SAKLA(SD:STRING;SN:BYTE);
PROCEDURE EKRAN_GETiR(SD:STRING;SN:BYTE);
PROCEDURE BEKLE(ZAMAN:LONGINT);
PROCEDURE RENK(Y,Z:BYTE);
PROCEDURE CERCEVE(SEK : CHAR; X1,Y1,X2,Y2,R:BYTE);
PROCEDURE BILGIOKU(SORU:STRING;VAR SON:STRING;SUT,SAT:BYTE;UZ:BYTE;DUZENLE:BOOLEAN);
PROCEDURE IOKU(X,Y:BYTE;VAR SONUC:byte;LIMIT,ENAZ:BYTE);
procedure TUSbekle(t2:char);
PROCEDURE MENUYAP(MI:STRING;MN:MENU_TIPI;VAR X:BYTE;TC,TB:BYTE;esc:boolean);
Procedure Menuyap2(mi:string;menu:menu_tipi;tc:char;en:byte;renk:byte;x,y:byte;escape:boolean;var secim:byte);
{mi:Men ˜smi,menu:Mennn elemanlar,tc:‡er‡evenin tek veya ‡ift olmas,
en:efekt numaras, x,y:sutun ve satr numaras, escape: esc ‡alŸsn m?
secim:se‡ilen se‡ene§in numaras, procedure tarafndan belirlenecek}
IMPLEMENTATION
{$IFDEF FPC}
procedure curon;
begin
cursoron;
end;
procedure curoff;
begin
cursoroff;
end;
{$ELSE}
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
{$ENDIF}
PROCEDURE EKRAN_SAKLA;
VAR
EKRAN_MODU:WORD;
DOSYA:FILE OF EKRAN_TIPI;
EKRAN:EKRAN_TIPI;
BEGIN
IF LO(LASTMODE)=7 THEN EKRAN_MODU:=$B000 ELSE EKRAN_MODU:=$B800;
ASSIGN(DOSYA,SD);RESET(DOSYA);
SEEK(DOSYA,SN);
{$IFNDEF FPC}MOVE(PTR(EKRAN_MODU,0)^,EKRAN,4000);{$ENDIF}
WRITE(DOSYA,EKRAN);
CLOSE(DOSYA);
END;
PROCEDURE EKRAN_GETiR;
VAR
EKRAN_MODU:WORD;
DOSYA:FILE OF EKRAN_TIPI;
EKRAN:EKRAN_TIPI;
BEGIN
IF LO(LASTMODE)=7 THEN EKRAN_MODU:=$B000 ELSE EKRAN_MODU:=$B800;
ASSIGN(DOSYA,SD);RESET(DOSYA);
SEEK(DOSYA,SN);
READ(DOSYA,EKRAN);
{$IFNDEF FPC}MOVE(EKRAN,PTR(EKRAN_MODU,0)^,4000);{$ENDIF}
CLOSE(DOSYA);
END;
PROCEDURE BEKLE;
VAR
SA,DK,SN,SL:WORD;
ZMN1,ZMN2:LONGINT;
BEGIN
GetTime(SA,DK,SN,SL);
ZMN1:=SL+SN*100+DK*6000+SA*360000;
REPEAT
{$IFDEF FPC}DELAY(1);{$ENDIF}{Islemciyi bosuna mesgul etmesin}
GetTime(SA,DK,SN,SL);
ZMN2:=SL+SN*100+DK*6000+SA*360000;
UNTIL ((ZMN2-ZMN1)>ZAMAN) or ((zmn1-zmn2)>zaman);
END;
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
END;
PROCEDURE BILGIOKU;
VAR
I,POZ,GC,GA:BYTE;
S:STRING;
TUS:CHAR;
INS:BOOLEAN;
PROCEDURE CUR(STYPE:CHAR);
{$IFDEF FPC}
BEGIN
IF STYPE='B' THEN CURSORBIG ELSE CURSORON;
END;
{$ELSE}
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
{$ENDIF}
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
#75,^S : IF POZ>1 THEN POZ:=POZ-1;
#77,^D : IF POZ<UZ THEN POZ:=POZ+1;
#82:BEGIN
INS:=NOT INS;
IF INS THEN CUR('K') ELSE CUR('B');
END;
#83:BEGIN DELETE(S,POZ,1);S:=S+' ';END;
#71:POZ:=1;
#79:POZ:=UZ;
END;
END ELSE BEGIN
IF TUS IN[#13,#27] THEN BEGIN
POZ:=1;
FOR GC:=UZ DOWNTO 1 DO IF S[GC]<>' ' THEN BEGIN GA:=GC;BREAK;END;
SON:='';
IF LENGTH(S)>0 THEN FOR GC:=1 TO GA DO SON:=SON+S[GC];
EXIT;
END;
IF TUS=#8 THEN BEGIN DELETE(S,POZ-1,1);S:=S+' ';IF POZ>1 THEN POZ:=POZ-1;CONTINUE;END;
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
SON:BYTE;
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
procedure TUSbekle;
var t1:char;
BEGIN
repeat
t1:=READKEY;
until t1=t2;
END;
{-----------------------------------------------------------}
PROCEDURE MENUYAP(MI:STRING;MN:MENU_TIPI;VAR X:BYTE;TC,TB:BYTE;esc:boolean);
LABEL ENBAS,BITTI;
VAR
SS,OK,EU:BYTE;
TUS:CHAR;
PROCEDURE MH;{MENUYU HAZIRLA}
VAR
A:BYTE;
BEGIN
TEXTCOLOR(TC);TEXTBACKGROUND(TB);
FOR A:=1 TO SS DO BEGIN
GOTOXY(round((80-eu)/2),A+2);WRITE('(',CHAR(64+A),') ',MN[A],' ':(eu-length(mn[a])+1));
END;
END;
PROCEDURE OKKOY;
BEGIN
GOTOXY(round((80-eu)/2),OK+2);
TEXTCOLOR(BLACK);TEXTBACKGROUND(WHITE);
IF TB=WHITE THEN BEGIN
TEXTCOLOR(WHITE);TEXTBACKGROUND(BLACK);
END;
if length(mn[ok])<eu then WRITE('(',CHAR(64+OK),') ',MN[OK],' ':(eu-LENGTH(MN[OK])))
else WRITE('(',CHAR(64+OK),') ',MN[OK]);
TEXTCOLOR(TC);TEXTBACKGROUND(TB);
END;
{Meny hazrla son}
PROCEDURE DS;{DEGERLERI SIFIRLA}
var a:byte;
BEGIN
SS:=1;
FOR A:=1 TO 20 DO IF LENGTH(MN[A])>0 THEN SS:=A;
OK:=1;
eu:=0;
for a:=1 to ss do begin
if length(mn[a])>eu then eu:=length(mn[a]);
end;
END;
BEGIN curoff;clrscr;
DS;cerceve('C',round((80-eu)/2)-2,2,round((80-eu)/2)+eu+5,ss+3,tc);
GOTOXY(ROUND((80-LENGTH(MI))/2)+2,1);WRITE(MI);
ENBAS:MH;OKKOY;
TUS:=READKEY;
CASE TUS OF
#71:OK:=1;
#79:OK:=SS;
#72:IF OK=1 THEN OK:=SS ELSE OK:=OK-1;
#80:IF OK=SS THEN OK:=1 ELSE OK:=OK+1;
#13:BEGIN X:=OK;GOTO BITTI;END;
{$IFNDEF FPC}
'A'..'Z':IF (ORD(TUS))<=(64+SS) THEN OK:=ORD(TUS)-64;
{$ENDIF}
'a'..'z':IF (ORD(TUS))<=(96+SS) THEN OK:=ORD(TUS)-96;
#27:IF ESC THEN BEGIN X:=0;GOTO BITTI;END;
{$IFDEF FPC}{FPC cakisan case etiketlerini kabul etmez (#71,#72,#79,#80 = G,H,O,P)}
ELSE IF (TUS IN ['A'..'Z']) AND ((ORD(TUS))<=(64+SS)) THEN OK:=ORD(TUS)-64;
{$ENDIF}
END;
GOTO ENBAS;
BITTI:;
curon;
END;
Procedure Menuyap2;
label bas,son;
var
a,b:byte;
eu,es:byte;{eu:En uzun elemann uzunlu§u,es:eleman says}
solust,sagust,solalt,sagalt,sagsol,altust:char;{€izgiler}
ok:byte;{Se‡ili Se‡ene§in yerini tutar}
tus:char;
procedure okkoy;
begin
textcolor(0);textbackground(15);
gotoxy(x+2,y+ok);write(menu[ok]);
if eu>length(menu[ok]) then write(' ':(eu-length(menu[ok])));
textcolor(renk);textbackground(0);
end;
procedure okkaldir;
begin
textcolor(renk);textbackground(0);
gotoxy(x+2,y+ok);write(menu[ok]);
if eu>length(menu[ok]) then write(' ':(eu-length(menu[ok])));
end;
begin textcolor(renk);
if tc in['t','T'] then begin
solust:='Ú';solalt:='À';
sagust:='¿';sagalt:='Ù';
sagsol:='³';altust:='Ä';
mi:='Ä'+mi;
end else begin
solust:='É';solalt:='È';
sagust:='»';sagalt:='¼';
sagsol:='º';altust:='Í';
end;{if then else}
for a:=1 to 20 do if menu[a]<>'' then es:=a;
eu:=0;
for a:=1 to es do if length(menu[a])>eu then eu:=length(menu[a]);
if en=0 then begin
gotoxy(x,y);write(solust);for a:=1 to eu+2 do write(altust);write(sagust);
for a:=y+1 to y+es do begin gotoxy(x+eu+3,a);write(sagsol);end;gotoxy(x+eu+3,y+es+1);write(sagalt);
for a:=x+2+eu downto x+1 do begin gotoxy(a,y+es+1);write(altust);end;gotoxy(x,y+es+1);write(solalt);
for a:=y+es downto y+1 do begin gotoxy(x,a);write(sagsol);end;
for a:=1 to es do begin
gotoxy(x+1,a+y);write(' ':(eu+2));
gotoxy(x+2,a+y);write(menu[a]);
end;
gotoxy(x+2,y);write(mi);
end;
if en=1 then begin
gotoxy(x,y);write(solust);for a:=1 to eu+2 do begin delay(100);write(altust);end;delay(100);write(sagust);
for a:=y+1 to y+es do begin delay(100);gotoxy(x+eu+3,a);write(sagsol);end;delay(100);gotoxy(x+eu+3,y+es+1);write(sagalt);
for a:=x+2+eu downto x+1 do begin delay(100);gotoxy(a,y+es+1);write(altust);end;delay(100);gotoxy(x,y+es+1);write(solalt);
for a:=y+es downto y+1 do begin delay(100);gotoxy(x,a);write(sagsol);end;
for a:=1 to es do begin bekle(3);
gotoxy(x+1,a+y);write(' ':(eu+2));
gotoxy(x+2,a+y);write(menu[a]);
end;
gotoxy(x+2,y);write(mi);
end;
if en=2 then begin
mi:='Í'+mi;
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
gotoxy(x+3+eu,a);write(sagsol);gotoxy(x+3+eu,a+1);write(sagalt);
for b:=x+1 to x+eu+2 do begin gotoxy(b,a+1);write(altust);end;
gotoxy(x+1,a);write(' ':(eu+2));
gotoxy(x+2,a);write(menu[a-y]);
bekle(3);
end;{for}
end;{if}
ok:=1;okkoy;
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
end;{Procedure}
END.
