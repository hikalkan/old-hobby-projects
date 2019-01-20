Module fonksyonlar
    Public Function SayiDuzgunYaz(ByVal sayi As Single, ByVal Ondalik As Byte) As String
        SayiDuzgunYaz = CStr(CInt(sayi * (10 ^ Ondalik)) / (10 ^ Ondalik))
    End Function
    Public Function ikiBasamakli(ByVal sayi As Integer) As String
        If sayi > 9 Then
            Return sayi.ToString
        Else
            Return ("0" & sayi.ToString)
        End If
    End Function
    Public Function MySqlUzunTarihYaz(ByVal Tarih As Date) As String
        MySqlUzunTarihYaz = Tarih.Year & "-" & ikiBasamakli(Tarih.Month) & "-" & ikiBasamakli(Tarih.Day) & " " & ikiBasamakli(Tarih.Hour) & ":" & ikiBasamakli(Tarih.Minute) & ":" & ikiBasamakli(Tarih.Second)
    End Function
    Public Function MySqlUzunTarihAl(ByVal tarih As String) As Date
        Dim gt As New Date
        gt = tarih
        MySqlUzunTarihAl = gt
    End Function
    Public Function MysqlKisaTarihYaz(ByVal tarih As Date) As String
        MysqlKisaTarihYaz = tarih.Year & "-" & tarih.Month & "-" & tarih.Day
    End Function
    Public Function MysqlKisaTarihAl(ByVal tarih As String) As Date
        Dim gt As New Date
        gt = tarih
        MysqlKisaTarihAl = gt
    End Function
    Public Function SayisalMi(ByVal ifade As String, ByVal EnKucukDeger As Integer, ByVal EnBuyukDeger As Integer) As Boolean
        If ifade = "" Then
            Return False
        Else
            If Not IsNumeric(ifade) Then
                Return False
            Else
                If (CInt(ifade) < EnKucukDeger) Or (CInt(ifade) > EnBuyukDeger) Then
                    Return False
                Else
                    Return True
                End If
            End If
        End If
    End Function
    Public Function Virgul2Nokta(ByVal sayi As Double) As String
        Virgul2Nokta = sayi.ToString.Replace(",", ".")
    End Function
    Public Function BilgiKontrolu(ByVal Veri As String, ByVal Uzunluk As Short, ByVal BosKabul As Boolean) As Boolean
        Dim i As Integer
        Dim UygunKarakterler As String
        UygunKarakterler = "abcçdefgðhýijklmnoöpqrsþtuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789 _."

        If Veri <> "" Then
            If Veri.Length <= Uzunluk And Veri.Length > 0 Then
                For i = 0 To Veri.Length - 1
                    If InStr(UygunKarakterler, Veri.Chars(i)) = 0 Then
                        Return False
                    End If
                Next
            Else
                Return False
            End If
        Else
            Return BosKabul
        End If

        Return True
    End Function
    Public Function WebSiteKontrolu(ByVal Veri As String, ByVal Uzunluk As Short, ByVal BosKabul As Boolean) As Boolean
        Dim i As Integer
        Dim UygunKarakterler As String
        UygunKarakterler = "abcçdefgðhýijklmnoöpqrsþtuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789 _.:/~"

        If Veri <> "" Then
            If Veri.Length <= Uzunluk And Veri.Length > 0 Then
                For i = 0 To Veri.Length - 1
                    If InStr(UygunKarakterler, Veri.Chars(i)) = 0 Then
                        Return False
                    End If
                Next
            Else
                Return False
            End If
        Else
            Return BosKabul
        End If

        Return True
    End Function
    Public Function EpostaKontrolu(ByVal Veri As String) As Boolean
        Dim i As Integer
        Dim UygunKarakterler As String
        UygunKarakterler = "abcçdefgðhýijklmnoöpqrsþtuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789_@.-"

        If Veri <> "" Then
            If Veri.Length > 0 Then
                For i = 0 To Veri.Length - 1
                    If InStr(UygunKarakterler, Veri.Chars(i)) = 0 Then
                        Return False
                    End If
                Next
            Else
                Return False
            End If

            If InStr(Veri, "@") = 0 Or InStr(Veri, ".") = 0 Then
                Return False
            End If
        Else
            Return False
        End If

        Return True
    End Function
    Public Function NoktaliSayi(ByVal Sayi As Integer) As String
        Dim Metin, Sonuc As String
        Dim i, j As Short
        Metin = CStr(Sayi)
        Sonuc = ""
        j = 0
        If InStr(Metin, ".") = 0 Then
            For i = Metin.Length - 1 To 0 Step -1
                j = j + 1
                Sonuc = Metin.Chars(i) & Sonuc
                If ((j) Mod 3 = 0) And (i > 0) Then
                    Sonuc = "." & Sonuc
                End If
            Next
            Return Sonuc
        Else
            Return Metin
        End If
    End Function
    Public Function KarakterKodla(ByVal Metin As String) As String
        Dim ara As String = Metin
        ara = Replace(ara, "'", "<22>")
        ara = Replace(ara, """", "<27>")
        Return ara
    End Function
    Public Function KarakterKodCoz(ByVal Metin As String) As String
        Dim ara As String = Metin
        ara = Replace(ara, "<22>", "'")
        ara = Replace(ara, "<27>", """")
        Return ara
    End Function
    Public Function HtmlKodla(ByVal Metin As String) As String
        Dim ara As String = Metin
        ara = Replace(ara, "<", "&lt")
        ara = Replace(ara, ">", "&gt")
        ara = Replace(ara, Chr(13), Chr(13) & "<br>")
        Return ara
    End Function
    Public Function SitedekiKonumu(ByVal YetkiDuzeyi As Byte) As String
        Select Case YetkiDuzeyi
            Case 1
                Return "Üye"
            Case 2
                Return "Yönetici"
            Case 3
                Return "Kurucu"
        End Select
    End Function
End Module
