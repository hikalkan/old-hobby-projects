Module mdlFonksyonlar
    'Dönüþüm fonksyonlarý
    Public Function Virgul2Nokta(ByVal sayi As Double) As String
        Virgul2Nokta = sayi.ToString.Replace(",", ".")
    End Function
    Public Function SayiDuzgunYaz(ByVal sayi As Double, ByVal Ondalik As Byte) As String
        SayiDuzgunYaz = CStr(CInt(Math.Round(sayi * (10 ^ Ondalik))) / (10 ^ Ondalik))
    End Function
    'Bilgi geçerliliði kontrolü
    Public Function SayisalMi(ByVal ifade As String, ByVal EnKucukDeger As Long, ByVal EnBuyukDeger As Long) As Boolean
        If ifade = "" Then
            Return False
        Else
            If Not IsNumeric(ifade) Then
                Return False
            Else
                If (CInt(ifade) < EnKucukDeger) Or (CInt(ifade) > EnBuyukDeger) Then
                    Return False
                Else
                    If CInt(ifade) = Val(ifade) Then 'tam sayý mý?
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        End If
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
    Public Function BilgiKontroluGenel(ByVal Veri As String, ByVal Uzunluk As Short, ByVal BosKabul As Boolean) As Boolean
        Dim i As Integer
        Dim UygunKarakterler As String
        UygunKarakterler = "abcçdefgðhýijklmnoöpqrsþtuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789 _.,?;:'""!^+-*/(){}[]&%$#-@<>~\"
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
    Public Function TarihGecerliMi(ByVal Tarih As String) As Boolean
        If Tarih = "" Then Return False
        If Tarih.Length < 8 Or Tarih.Length > 10 Then Return False
        Dim AKS As Byte = 0 ' Ayýrma Karakteri (/) Sayýsý
        Dim HarfNo As Byte
        For HarfNo = 0 To Tarih.Length - 1
            If Tarih.Chars(HarfNo) = "/" Then AKS += 1
        Next
        If AKS <> 2 Then Return False
        Dim Parcalar() As String = Tarih.Split("/")
        'Ay 1-12 arasý mý?
        If Not SayisalMi(Parcalar(1), 1, 12) Then Return False
        'Yýl 1900-2000 arasý mý?
        If Not SayisalMi(Parcalar(2), 1900, 2000) Then Return False
        'Gün deðeri doðru mu?
        Dim Gunler() As Byte = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        If CInt(Parcalar(2)) Mod 4 = 0 Then Gunler(1) = 29
        If Not SayisalMi(Parcalar(0), 1, Gunler(CInt(Parcalar(1)) - 1)) Then Return False
        Return True
    End Function
    Public Function HtmlKodla(ByRef Metin As String) As String
        Metin = Replace(Metin, ">", "&gt")
        Metin = Replace(Metin, "<", "&lt")
        Return Metin
    End Function
    'Tarih fonksyonlarý
    Public Function Tarih2MySqlTarih(ByVal Tarih As String) As String
        Dim Parcalar() As String = Tarih.Split("/")
        Return (Parcalar(2) & "-" & Parcalar(1) & "-" & Parcalar(0))
    End Function
    Public Function MySqlTarih2Tarih(ByVal Tarih As String) As String
        Dim Parcalar() As String = Tarih.Split("-")
        Return (Parcalar(2) & "/" & Parcalar(1) & "/" & Parcalar(0))
    End Function
    Public Function TarihTurkceFormat(ByVal Tarih As Date) As String
        Return Format(Tarih, "dd/MM/yyyy")
    End Function
    'Program ile alakalý dönüþüm fonksyonlarý
    Public Function YetkiSayi2Yazi(ByVal YetkiDuzeyi As Byte) As String
        Select Case YetkiDuzeyi
            Case HYD_KendiDersi
                Return "Sadece kendi girdiði derslerin anket sonuçlarýný görebilir"
            Case HYD_TumDersler
                Return "Tüm derslerin anket sonuçlarýný görebilir"
            Case HYD_Editor
                Return "Tüm derslerin anket sonuçlarýný görebilir, öðrenci ve ders ekleme/silme yapabilir"
            Case HYD_Yonetici
                Return "Sýnýrsýz yetkili"
        End Select
    End Function
    Public Function MetinKisalt(ByVal Metin As String, ByVal Uzunluk As Short) As String
        Dim Sonuc As String = ""
        If Metin <> "" Then
            Sonuc = Metin
            If Sonuc.Length > Uzunluk Then
                Sonuc = Microsoft.VisualBasic.Left(Sonuc, Uzunluk - 3)
                Sonuc += "..."
            End If
        End If
        Return Sonuc
    End Function
    Public Function DonemMetin(ByVal Donem As Byte) As String
        Select Case Donem
            Case 1
                Return "1. Dönem"
            Case 2
                Return "2. Dönem"
            Case 3
                Return "Yaz Okulu"
            Case Else
                Return "?"
        End Select
    End Function
    Public Function DonemMetinKisa(ByVal Donem As Byte) As String
        Select Case Donem
            Case 1
                Return "1.D."
            Case 2
                Return "2.D."
            Case 3
                Return "Y.O."
            Case Else
                Return "?"
        End Select
    End Function
End Module
