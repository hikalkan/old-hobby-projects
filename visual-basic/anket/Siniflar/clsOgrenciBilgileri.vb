Imports System.Data.Odbc
Public Class clsOgrenciBilgileri
    'Private De�i�kenler
    Private Komut As OdbcCommand
    Private Okuyucu As OdbcDataReader
    'Public De�i�kenler
    Public OgrenciNo As System.Int32
    Public Ad As System.String
    Public Soyad As System.String
    Public Numara As System.String
    Public Eposta As System.String
    Public DogumTarihi As System.String
    Public Sifre As System.String
    Public OnaylandiMi As System.Int16
    Public MezunOlduMu As System.Int16
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
    ' Veritaban�ndan Kay�tlar� Alan Fonksyon
    Public Function VeriTabanindanAl() As Boolean
        Dim Basarili As Boolean = False
        Dim Sorgu As String
        Sorgu = "SELECT * FROM Ogrenciler WHERE OgrenciNo='" & OgrenciNo & "'"
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                OgrenciNo = Okuyucu("OgrenciNo")
                Ad = Okuyucu("Ad")
                Soyad = Okuyucu("Soyad")
                Numara = Okuyucu("Numara")
                Eposta = Okuyucu("Eposta")
                DogumTarihi = Okuyucu("DogumTarihi")
                Sifre = Okuyucu("Sifre")
                OnaylandiMi = Okuyucu("OnaylandiMi")
                MezunOlduMu = Okuyucu("MezunOlduMu")
                Basarili = True
            End If
        Catch ex As Exception
            'bo�
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        Return Basarili
    End Function
    Public Function VeriTabanindanAl(ByVal Numara As String, ByVal Sifre As String) As Boolean
        Dim Basarili As Boolean = False
        Dim Sorgu As String
        Sorgu = "SELECT * FROM Ogrenciler WHERE Numara='" & Numara & "' AND Sifre='" & Sifre & "'"
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                OgrenciNo = Okuyucu("OgrenciNo")
                Ad = Okuyucu("Ad")
                Soyad = Okuyucu("Soyad")
                Numara = Okuyucu("Numara")
                Eposta = Okuyucu("Eposta")
                DogumTarihi = Okuyucu("DogumTarihi")
                Sifre = Okuyucu("Sifre")
                OnaylandiMi = Okuyucu("OnaylandiMi")
                MezunOlduMu = Okuyucu("MezunOlduMu")
                Basarili = True
            End If
        Catch ex As Exception
            'bo�
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        Return Basarili
    End Function
    'Veritaban�na Kay�t Yazan Fonksyon
    Public Function VeriTabaninaKaydet() As Boolean
        Dim Basarili As Boolean = True
        Dim Sorgu As String
        Sorgu = "INSERT INTO Ogrenciler(" & _
        "Ad," & _
        "Soyad," & _
        "Numara," & _
        "Eposta," & _
        "DogumTarihi," & _
        "Sifre," & _
        "OnaylandiMi," & _
        "MezunOlduMu) Values(" & _
        "'" & Ad & "'," & _
        "'" & Soyad & "'," & _
        "'" & Numara & "'," & _
        "'" & Eposta & "'," & _
        "'" & DogumTarihi & "'," & _
        "'" & Sifre & "'," & _
        OnaylandiMi & "," & _
        MezunOlduMu & ")"
        Komut.CommandText = Sorgu
        Try
            Komut.ExecuteNonQuery()
        Catch ex As Exception
            Basarili = False
        End Try
        Return Basarili
    End Function
    'Veritabanda Kay�t G�ncelleyen Fonksyon
    Public Function VeriTabanindaGuncelle() As Boolean
        Dim Basarili As Boolean = True
        Dim Sorgu As String
        Sorgu = "UPDATE Ogrenciler SET " & _
        "Ad = '" & Ad & "'," & _
        "Soyad = '" & Soyad & "'," & _
        "Numara = '" & Numara & "'," & _
        "Eposta = '" & Eposta & "'," & _
        "DogumTarihi = '" & DogumTarihi & "'," & _
        "Sifre = '" & Sifre & "'," & _
        "OnaylandiMi = " & OnaylandiMi & "," & _
        "MezunOlduMu = " & MezunOlduMu & " WHERE OgrenciNo='" & OgrenciNo & "'"
        Komut.CommandText = Sorgu
        Try
            Komut.ExecuteNonQuery()
        Catch ex As Exception
            Basarili = False
        End Try
        Return Basarili
    End Function
    '�ifre De�i�tirmek i�in
    Public Function SifeGuncelle() As Boolean
        Dim Basarili As Boolean = True
        Dim Sorgu As String
        Sorgu = "UPDATE Ogrenciler SET " & _
        "Sifre = '" & Sifre & "',OnaylandiMi=1 WHERE OgrenciNo='" & OgrenciNo & "'"
        Komut.CommandText = Sorgu
        Try
            Komut.ExecuteNonQuery()
        Catch ex As Exception
            Basarili = False
        End Try
        Return Basarili
    End Function
    'Veritabandan Kay�t Silen Fonksyon
    Public Function VeriTabanindanKayitSil() As Boolean
        Dim Basarili As Boolean = True
        Dim Sorgu As String
        Try
            Sorgu = "DELETE FROM Ogrenciler WHERE OgrenciNo=" & OgrenciNo
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            Sorgu = "DELETE FROM DersOgrenciIliskileri WHERE OgrenciNo=" & OgrenciNo
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
        Catch ex As Exception
            Basarili = False
        End Try
        Return Basarili
    End Function
    'Numaraya g�re bir ��rencinin veritaban�nda olup olmad���na bakar
    'D�n�� de�erleri; 0: Yok, 1: Var, 2: Veritaban�na ba�lant� hatas� 
    Public Function VeritabanindaVarMi() As Byte
        Dim Sonuc As Byte = 0
        Dim Sorgu As String
        Sorgu = "SELECT OgrenciNo FROM Ogrenciler WHERE Numara='" & Numara & "'"
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
