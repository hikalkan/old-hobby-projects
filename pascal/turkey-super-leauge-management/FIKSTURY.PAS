Program deneme;
uses tslu1,crt,dos;
var
sat,sut,i,j,k:Integer;
dosya:text;
begin
clrscr;
assign(dosya,'fikstur.txt');
rewrite(dosya);
for i:=1 to 34 do begin
for j:=1 to 9 do begin
writeln(dosya,fikstur(i,j,1),'-',fikstur(i,j,2));
end;
end;
close(dosya);
end.