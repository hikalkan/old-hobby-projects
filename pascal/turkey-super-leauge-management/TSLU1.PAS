UNIT TSLU1;
{$IFDEF FPC}{$MODE TP}{$PACKRECORDS 1}{$ENDIF}
INTERFACE
USES {$IFDEF FPC}TSLCRT{$ELSE}CRT{$ENDIF},DOS;
TYPE
FDT=RECORD
FIKS:ARRAY[1..34,1..9,1..2] OF BYTE;
END;
haftakayittipi=record
skor:array[1..9,1..2] of byte;
end;
FUNCTION FIKSTUR(H,M,A:BYTE):BYTE;
PROCEDURE MACSONUCUYAZ(Hafta,M,evgol,depgol:BYTE);
function macsonucu(h,m,a:byte):byte;
IMPLEMENTATION
FUNCTION FIKSTUR;
VAR
FD:FDT;
DF:FILE OF FDT;
BEGIN
ASSIGN(DF,'FIKS.TSL');RESET(DF);SEEK(DF,0);READ(DF,FD);
FIKSTUR:=FD.FIKS[H,M,A];
CLOSE(DF);
END;
PROCEDURE MACSONUCUYAZ;
VAR
kayit:haftakayittipi;
dosya:FILE OF haftakayittipi;
BEGIN
ASSIGN(Dosya,'macsonuc.tsl');reset(dosya);seek(dosya,hafta);read(dosya,kayit);
kayit.skor[m,1]:=evgol;kayit.skor[m,2]:=depgol;
write(dosya,kayit);
close(dosya);
END;
FUNCTION MACSONUCU;
VAR
kayit:haftakayittipi;
dosya:FILE OF haftakayittipi;
BEGIN
ASSIGN(DOSYA,'macsonuc.tsl');RESET(DOSYA);seek(dosya,h);read(dosya,kayit);
macsonucu:=kayit.skor[m,a];
close(dosya);
END;
END.