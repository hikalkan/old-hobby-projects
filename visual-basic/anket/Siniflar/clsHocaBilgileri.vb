Imports System.Data.Odbc
Public Class clsHocaBilgileri
    'Private De�i�kenler
    Private Komut As OdbcCommand
    Private Okuyucu As OdbcDataReader
    'Public De�i�kenler
    Public HocaNo As System.Int32
    Public Unvan As System.String
    Public Ad As System.String
    Public Soyad As System.String
    Public KullaniciAdi As System.String
    Public Eposta As System.String
    Public DogumTarihi As System.String
    Public Sifre As System.String
    Public YetkiDuzeyi As System.Int16
    Public OnaylandiMi As System.Byte
    'Kurucu Fonksyon
    Public Sub New()
        Komut = New OdbcCommand
        Komut.Connection = Baglanti
    End Sub
    'Yok edici fonksyon
    Protected Overrides Sub Finalize()
        If Not (Komut Is Nothing) Then Komut.Dispose()
        MyBase.Finalize()
    End Sub
    'Veritaban�na Kay�t Yazan Fonksyon
    Public Function VeriTabaninaKaydet() As Boolean
        Dim Basarili As Boolean = True
        Dim Sorgu As String
        Sorgu = "INSERT INTO Hocalar(" & _
        "Unvan," & _
        "Ad," & _
        "Soyad," & _
        "Eposta," & _
        "DogumTarihi," & _
        "KullaniciAdi," & _
        "Sifre," & _
        "YetkiDuzeyi," & _
        "OnaylandiMi) Values(" & _
        "'" & Unvan & "'," & _
        "'" & Ad & "'," & _
        "'" & Soyad & "'," & _
        "'" & Eposta & "'," & _
        "'" & DogumTarihi & "'," & _
        "'" & KullaniciAdi & "'," & _
        "'" & Sifre & "'," & _
        YetkiDuzeyi & "," & _
        OnaylandiMi & ")"
        Komut.CommandText = Sorgu
        Try
            Komut.ExecuteNonQuery()
        Catch ex As Exception
            Basarili = False
        End Try
        Return Basarili
    End Function
    ' Veritaban�ndan Kay�tlar� Alan Fonksyon
    Public Function VeriTabanindanAl(ByVal KullaniciAdi As String, ByVal Sifre As String) As Boolean
        Dim Basarili As Boolean = False
        Dim Sorgu As String
        Sorgu = "SELECT * FROM Hocalar WHERE KullaniciAdi='" & KullaniciAdi & "'" & " AND Sifre='" & Sifre & "'"
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                HocaNo = Okuyucu("HocaNo")
                Unvan = Okuyucu("Unvan")
                Ad = Okuyucu("Ad")
                Soyad = Okuyucu("Soyad")
                Eposta = Okuyucu("Eposta")
                DogumTarihi = Okuyucu("DogumTarihi")
                KullaniciAdi = Okuyucu("KullaniciAdi")
                YetkiDuzeyi = Okuyucu("YetkiDuzeyi")
                Sifre = Okuyucu("Sifre")
                OnaylandiMi = Okuyucu("OnaylandiMi")
                Basarili = True
            End If
        Catch ex As Exception
            'bo�
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        Return Basarili
    End Function
    '�ifre De�i�tirmek i�in
    Public Function SifeGuncelle() As Boolean
        Dim Basarili As Boolean = True
        Dim Sorgu As String
        Sorgu = "UPDATE Hocalar SET " & _
        "Sifre = '" & Sifre & "',OnaylandiMi=1 WHERE HocaNo=" & HocaNo
        Komut.CommandText = Sorgu
        Try
            Komut.ExecuteNonQuery()
        Catch ex As Exception
            Basarili = False
        End Try
        Return Basarili
    End Function
    Public Function VeriTabanindanAl() As Boolean
        Dim Basarili As Boolean = False
        Dim Sorgu As String
        Sorgu = "SELECT * FROM Hocalar WHERE HocaNo=" & HocaNo
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                Unvan = Okuyucu("Unvan")
                Ad = Okuyucu("Ad")
                Soyad = Okuyucu("Soyad")
                Eposta = Okuyucu("Eposta")
                DogumTarihi = Okuyucu("DogumTarihi")
                KullaniciAdi = Okuyucu("KullaniciAdi")
                YetkiDuzeyi = Okuyucu("YetkiDuzeyi")
                OnaylandiMi = Okuyucu("OnaylandiMi")
                Sifre = Okuyucu("Sifre")
                Basarili = True
            End If
        Catch ex As Exception
            'bo�
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        Return Basarili
    End Function
    'Kullan�c� ad�na g�re bir hocan�n veritaban�nda olup olmad���na bakar
    'D�n�� de�erleri; 0: Yok, 1: Var, 2: Veritaban�na ba�lant� hatas� 
    Public Function VeritabanindaVarMi() As Byte
        Dim Sonuc As Byte = 0
        Dim Sorgu As String
        Sorgu = "SELECT Ad FROM Hocalar WHERE KullaniciAdi='" & KullaniciAdi & "'"
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                Sonuc = 1
            End If
        Catch ex As Exception
            Sonuc = 2
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        Return Sonuc
    End Function
End Class
