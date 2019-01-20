Program Labirent_Olusturma;
{
    Bu program Halil òbrahim Kalkan tarafçndan yapçlmçütçr.
    15.10.2004
    http://www.kalkan.cjb.net
}
Uses
 Crt,Graph;

Const
 m=30; {Bu degerler degistirilirse labirentin boyutlari degisir}
 n=20;

Var
 i,j,k                  : Integer;
 Hucreler               : Array [1..m,1..n] of Byte;
 x,y                    : Byte;
 DikeyDuvarlar                  : Array [1..m+1,1..n] of Byte;
 YatayDuvarlar                  : Array [1..m,1..n+1] of Byte;

Procedure GrafikAc;
Var
 gd,gm:Integer;
Begin
 gd:=Detect;
 Initgraph(gd,gm,'');
End;

Procedure GrafikKapat;
Begin
 CloseGraph;
End;

Procedure DegerleriSifirla;
Var
 i,j                    : Integer;
Begin
 For i:=1 to m do For j:=1 to n do Hucreler[i,j]:=0;
 Hucreler[1,n]:=1;
 for i:=1 to m+1 do For j:=1 to n do dikeyduvarlar[i,j]:=1;
 for i:=1 to m do For j:=1 to n+1 do yatayduvarlar[i,j]:=1;
 x:=1;
 y:=n;
End;

Procedure ImlecTasi(x1,y1,x2,y2:Byte);
Begin
 SetColor(0);
 setfillstyle(1,0);
 fillellipse(x1*20-9,y1*20-9,2,2);
 setcolor(15);
 setfillstyle(1,lightgray);
 fillellipse(x2*20-9,y2*20-9,2,2);
End;


Procedure Kareleri_Ciz;
Var
 i,j                    : Byte;
Begin
 setcolor(15);
 For i:=1 to m+1 do line(i*20-19,1,i*20-19,n*20+1);
 For i:=1 to n+1 do line(1,i*20-19,m*20+1,i*20-19);
End;

Procedure DikeyDuvarSil(x,y:Byte);
Begin
 Setcolor(0);
 Line(x*20-19,y*20-18,x*20-19,y*20);
 DikeyDuvarlar[x,y]:=0;
 SetColor(15);
End;

Procedure YatayDuvarSil(x,y:Byte);
Begin
 Setcolor(0);
 Line(x*20-18,y*20-19,x*20,y*20-19);
 YatayDuvarlar[x,y]:=0;
 SetColor(15);
End;

Procedure KareleriDuzenle;
Var
 i,j                    : Byte;
 yon                    : Byte;
Begin
For i:=1 to m do For j:=1 to n do Begin
 if (DikeyDuvarlar[i,j]=1) And (DikeyDuvarlar[i+1,j]=1) And
    (YatayDuvarlar[i,j]=1) And (YatayDuvarlar[i,j+1]=1) Then Begin
  yon:=random(4)+1;
  Hucreler[i,j]:=2;
  case yon of
   1:DikeyDuvarSil(i,j);
   2:DikeyDuvarSil(i+1,j);
   3:YatayDuvarSil(i,j);
   4:YatayDuvarSil(i,j+1);
  End;
 End;
End;
End;

Procedure YoluBelirle;
Var
 H                      : Array [1..m,1..n] of Byte;
 Hedef                  : Boolean;
 Yon                    : Byte;
Procedure HyiAl;
Var
 i,j                    : Byte;
Begin
 {Hucreler dizisi kopyalaniyor}
 For i:=1 to m do For j:=1 to n do H[i,j]:=Hucreler[i,j];
End;
Function BosMu(hx,hy:Byte):Boolean;
Begin
if (hx in [1..m]) and (hy in [1..n]) then
 BosMu:=(H[hx,hy]=0)
else
 BosMu:=False;
End;
Procedure HedefMi(hx,hy:Byte);
Begin
if BosMu(hx,hy) Then Begin
 If ((hx=m) And (hy=1)) then Begin
  Hedef:=True
 End Else Begin
  H[hx,hy]:=1;
  HedefMi(hx+1,hy);{sag}
  HedefMi(hx-1,hy);{sol}
  HedefMi(hx,hy+1);{alt}
  HedefMi(hx,hy-1);{ust}
 End;
End;{If BosMu..}
End;{HedefMi}
Begin
 Randomize;
 Repeat
  Repeat
  yon:=random(4)+1;
  Hedef:=False;
  HyiAl;
  case yon of
  1:HedefMi(x-1,y);
  2:HedefMi(x+1,y);
  3:HedefMi(x,y-1);
  4:HedefMi(x,y+1);
  end;
  Until Hedef;
  Case yon of
  1:Begin
   DikeyDuvarSil(x,y);
   x:=x-1;
  End;
  2:Begin
   DikeyDuvarSil(x+1,y);
   x:=x+1;
  End;
  3:Begin
   YatayDuvarSil(x,y);
   y:=y-1;
  End;
  4:Begin
   YatayDuvarSil(x,y+1);
   y:=y+1;
  End;
  End;
  Hucreler[x,y]:=1;
 Until (x=m) and (y=1);
End;

Procedure Oynat;
Var
 Tus                    : Char;
 ex,ey                  : Byte;
Begin
 x:=1;y:=n;
 imlectasi(x,y,x,y);
 Repeat
  ex:=x;ey:=y;
  repeat
  Tus:=Readkey;
  until Tus in ['w','a','s','d',#27];
  Case Tus Of
  'd':if x<m then if dikeyduvarlar[x+1,y]=0 Then x:=x+1;
  'a':if x>1 Then if dikeyduvarlar[x,y]=0 Then  x:=x-1;
  'w':if y>1 then if yatayduvarlar[x,y]=0 Then  y:=y-1;
  's':if y<n then if yatayduvarlar[x,y+1]=0 Then  y:=y+1;
  End;
  imlectasi(ex,ey,x,y);
 Until ((x=m) and (y=1)) or (Tus=#27);
End;

Begin
 GrafikAc;
 DegerleriSifirla;
 Kareleri_Ciz;
 YoluBelirle;
 KareleriDuzenle;
 Oynat;
 GrafikKapat;
End.