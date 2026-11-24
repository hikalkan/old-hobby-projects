{Turbo Pascal veri dosyalarini (6 baytlik Real) Free Pascal surumunun
 kullandigi bicime (8 baytlik Double) cevirir.
 Kullanim: tslconv <kaynak klasor> <hedef klasor>}
{$MODE TP}{$PACKRECORDS 1}
program tslconv;
type
old_def=record
TI:string[20];TS:string[35];TR:array[1..2] of byte;TFS:byte;TSG:byte;TP:longint;
FI:array[1..18] of string[20];FM:array[1..18] of char;FP:array[1..18] of real48;
end;
new_def=record
TI:string[20];TS:string[35];TR:array[1..2] of byte;TFS:byte;TSG:byte;TP:longint;
FI:array[1..18] of string[20];FM:array[1..18] of char;FP:array[1..18] of double;
end;
old_akt=record
TI:string[20];TS:string[35];TR:array[1..2] of byte;TFS:byte;TSG:byte;TP:longint;
FI:array[1..18] of string[20];FM:array[1..18] of char;FP:array[1..18] of real48;
FPI:array[1..18] of real48;DURUM:array[1..7] of byte;
end;
new_akt=record
TI:string[20];TS:string[35];TR:array[1..2] of byte;TFS:byte;TSG:byte;TP:longint;
FI:array[1..18] of string[20];FM:array[1..18] of char;FP:array[1..18] of double;
FPI:array[1..18] of double;DURUM:array[1..7] of byte;
end;
var
kaynak,hedef:string;
i:integer;
s:string;

function varmi(ad:string):boolean;
var f:file;
begin
assign(f,ad);
{$I-}reset(f,1);{$I+}
if ioresult=0 then begin close(f);varmi:=true;end else varmi:=false;
end;

procedure def_cevir(ad:string);
var fi,fo:file;o:old_def;n:new_def;a,b,ks:longint;
begin
if not varmi(kaynak+ad) then exit;
assign(fi,kaynak+ad);reset(fi,1);
assign(fo,hedef+ad);rewrite(fo,1);
ks:=filesize(fi) div sizeof(o);
for a:=1 to ks do begin
blockread(fi,o,sizeof(o));
fillchar(n,sizeof(n),0);
n.TI:=o.TI;n.TS:=o.TS;n.TR[1]:=o.TR[1];n.TR[2]:=o.TR[2];
n.TFS:=o.TFS;n.TSG:=o.TSG;n.TP:=o.TP;
for b:=1 to 18 do begin
n.FI[b]:=o.FI[b];n.FM[b]:=o.FM[b];n.FP[b]:=real2double(o.FP[b]);
end;
blockwrite(fo,n,sizeof(n));
end;
close(fi);close(fo);
writeln(ad,' cevrildi (',ks,' kayit)');
end;

procedure akt_cevir(ad:string);
var fi,fo:file;o:old_akt;n:new_akt;a,b,ks:longint;
begin
if not varmi(kaynak+ad) then exit;
assign(fi,kaynak+ad);reset(fi,1);
assign(fo,hedef+ad);rewrite(fo,1);
ks:=filesize(fi) div sizeof(o);
for a:=1 to ks do begin
blockread(fi,o,sizeof(o));
fillchar(n,sizeof(n),0);
n.TI:=o.TI;n.TS:=o.TS;n.TR[1]:=o.TR[1];n.TR[2]:=o.TR[2];
n.TFS:=o.TFS;n.TSG:=o.TSG;n.TP:=o.TP;
for b:=1 to 18 do begin
n.FI[b]:=o.FI[b];n.FM[b]:=o.FM[b];
n.FP[b]:=real2double(o.FP[b]);n.FPI[b]:=real2double(o.FPI[b]);
end;
for b:=1 to 7 do n.DURUM[b]:=o.DURUM[b];
blockwrite(fo,n,sizeof(n));
end;
close(fi);close(fo);
writeln(ad,' cevrildi (',ks,' kayit)');
end;

begin
if paramcount<2 then begin
writeln('Kullanim: tslconv <kaynak klasor> <hedef klasor>');halt(1);
end;
kaynak:=paramstr(1);hedef:=paramstr(2);
if kaynak[length(kaynak)]<>'\' then kaynak:=kaynak+'\';
if hedef[length(hedef)]<>'\' then hedef:=hedef+'\';
if (sizeof(old_def)<>569) or (sizeof(old_akt)<>684) then begin
writeln('Kayit boyutlari beklenenden farkli!');halt(2);
end;
def_cevir('ILKVERI.TSL');
akt_cevir('AKTIF.TSL');
for i:=0 to 17 do begin
str(i,s);
akt_cevir('KO'+s+'.TSK');
end;
end.
