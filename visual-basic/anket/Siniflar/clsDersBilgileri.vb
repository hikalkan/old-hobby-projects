Imports System.Data.Odbc
Public Class clsDersBilgileri
    'Private De�i�kenler
    Private Komut As OdbcCommand
    Private Okuyucu As OdbcDataReader
    'Public De�i�kenler
    Public DersNo As Long
    Public HocaNo As Long
    Public Ad As String
    Public Sinif As Byte
    Public Donem As Byte
    Public Yil As Short
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
    'Veritaban�na kaydeden fonksyon
    Public Function VeritabaninaKaydet() As Boolean
        Dim Basarili As Boolean = True
        Dim Sorgu As String
        Sorgu = "INSERT INTO Dersler(" & _
        "HocaNo," & _
        "Ad," & _
        "Sinif," & _
        "Donem," & _
        "Yil) Values(" & _
        HocaNo & "," & _
        "'" & Ad & "'," & _
        Sinif & "," & _
        Donem & "," & _
        Yil & ")"
        Komut.CommandText = Sorgu
        Try
            Komut.ExecuteNonQuery()
        Catch ex As Exception
            Basarili = False
        End Try
        Return Basarili
    End Function
    'Veritaban�nda bu dersin olup olmad���na bakar..
    'D�n�� de�erleri; 0: Yok, 1: Var, 2: Veritaban�na ba�lant� hatas� 
    Public Function VeritabanindaVarMi() As Byte
        Dim Sonuc As Byte = 0
        Dim Sorgu As String
        Sorgu = "SELECT DersNo FROM Dersler WHERE Ad='" & Ad & "' AND Yil=" & Yil & " AND Donem=" & Donem
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
