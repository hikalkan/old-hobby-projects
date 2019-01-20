Program Hanoi_Kulesi;
Uses Crt,Ekstra;
Type
KuleTipi= Array[1..20] of Byte;
Var
A,B,C   : KuleTipi;
As,Bs,Cs: Byte;
n       : Byte;
i       : Byte;
Adim    : LongInt;
Bekleme : Byte;
Procedure KuleCiz;
Var
i,j:Byte;
Begin
Clrscr;
Gotoxy(19,24);write('A');
Gotoxy(40,24);write('B');
Gotoxy(61,24);write('C');
Gotoxy(10,23);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
Gotoxy(31,23);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
Gotoxy(52,23);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
gotoxy(2,2);write('Adm Says: ',adim);
adim:=adim+1;
for j:=14 to 22 do begin
gotoxy(19,j);write('³');
gotoxy(40,j);write('³');
gotoxy(61,j);write('³');
end;
if as=0 then begin gotoxy(19,23);write('Á');end;
if bs=0 then begin gotoxy(40,23);write('Á');end;
if cs=0 then begin gotoxy(61,23);write('Á');end;
For i:=1 to As do begin
Gotoxy(19-a[i],23-i);
for j:=1 to a[i]*2+1 do write('Ü');
End;
For i:=1 to Bs do begin
Gotoxy(40-b[i],23-i);
for j:=1 to b[i]*2+1 do write('Ü');
End;
For i:=1 to Cs do begin
Gotoxy(61-c[i],23-i);
for j:=1 to c[i]*2+1 do write('Ü');
End;
Bekle(Bekleme);
End;
Procedure Kule(n:Byte;Var Kaynak,Hedef,Ara:KuleTipi;Var KaynakS,HedefS,AraS:Byte);
Begin
If n=1 Then Begin
HedefS:=HedefS+1;
Hedef[HedefS]:=Kaynak[KaynakS];
KaynakS:=KaynakS-1;
KuleCiz;
End Else Begin
Kule(n-1,Kaynak,Ara,Hedef,KaynakS,AraS,HedefS);
HedefS:=HedefS+1;
Hedef[HedefS]:=Kaynak[KaynakS];
KaynakS:=KaynakS-1;
KuleCiz;
Kule(n-1,Ara,Hedef,Kaynak,AraS,HedefS,KaynakS);
End;
End;
Begin Clrscr;
Repeat
Write('Kulede kac adet disk olsun (1..8) : ');Readln(n);
Writeln;
Writeln('Asagidaki bekleme sresi bir diskin bir kuleden digerine tasinmasi');
Writeln('sonrasindaki M˜L˜SAN˜YE cinsinden bekleme sresidir.');
Writeln('Not: 100 Milisaniye = 1 saniye.');
Writeln;
Write('Animasyonun bekleme sresini giriniz (1..100)  : ');Readln(Bekleme);
Writeln;
Writeln('HATALI G˜R˜!!! LšTFEN TEKRAR G˜R˜N...');Writeln;
Until (n in[1..8]) and (Bekleme in [1..100]);
Clrscr;
curoff;
Adim:=0;
For i:=n downto 1 do A[n-i+1]:=i;
As:=n;
Bs:=0;
Cs:=0;
KuleCiz;
Kule(n,A,C,B,As,Cs,Bs);
curon;
Gotoxy(2,4);Write('Bitti. Devam etmek i‡in bir tuŸa basnz...');
BirTusBekle;
End.