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
i:Byte;
Begin
Clrscr;
Gotoxy(31,24);write('A');
Gotoxy(41,24);write('B');
Gotoxy(51,24);write('C');
Gotoxy(28,23);write('컴컴컴');
Gotoxy(38,23);write('컴컴컴');
Gotoxy(48,23);write('컴컴컴');
gotoxy(1,24);write('Ad뛪 Say뛱�: ',adim);
adim:=adim+1;
For i:=1 to As do begin
Gotoxy(30,23-i);write(a[i]:2);
End;
For i:=1 to Bs do begin
Gotoxy(40,23-i);write(B[i]:2);
End;
For i:=1 to Cs do begin
Gotoxy(50,23-i);write(c[i]:2);
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
Writeln('Asagidaki bekleme s걊esi bir diskin bir kuleden digerine tasinmasi');
Writeln('sonrasindaki M쁋쁓AN쁚E cinsinden bekleme s걊esidir.');
Writeln('Not: 100 Milisaniye = 1 saniye.');
Writeln;
Write('Animasyonun bekleme s걊esini giriniz (1..100)  : ');Readln(Bekleme);
Writeln;
Writeln('HATALI G쁒삛!!! L숿FEN TEKRAR G쁒쁍...');Writeln;
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
Gotoxy(2,2);Write('Bitti. Devam etmek i뇁n bir tu웏 bas뛫뛺...');
BirTusBekle;
End.