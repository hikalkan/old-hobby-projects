Imports System.Data.Odbc
Public Class clsAyarlar
    'Private deðiþkenler
    Private _OgretimYili As Short = 0
    Private _Donem As Byte = 0
    Private _AnketAktif As Byte = 2
    'Public Özellikler
    Public Property OgretimYili() As Short
        Get
            If _OgretimYili > 0 Then
                Return _OgretimYili
            Else
                If AyarlariAl() Then
                    Return _OgretimYili
                Else
                    Return 0
                End If
            End If
        End Get
        Set(ByVal Value As Short)
            _OgretimYili = Value
        End Set
    End Property
    Public Property Donem() As Byte
        Get
            If _Donem > 0 Then
                Return _Donem
            Else
                If AyarlariAl() Then
                    Return _Donem
                Else
                    Return 0
                End If
            End If
        End Get
        Set(ByVal Value As Byte)
            _Donem = Value
        End Set
    End Property
    Public Property AnketAktif() As Byte
        Get
            If _AnketAktif < 2 Then
                Return _AnketAktif
            Else
                If AyarlariAl() Then
                    Return _AnketAktif
                Else
                    Return 2
                End If
            End If
        End Get
        Set(ByVal Value As Byte)
            _AnketAktif = Value
        End Set
    End Property
    Private Function AyarlariAl() As Boolean
        Dim Sorgu As String
        Dim Basarili As Boolean = True
        Sorgu = "SELECT AyarAdi,Deger FROM Ayarlar"
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim tblAyarlar As New DataTable
        Dim drAyar As DataRow
        Try
            Adaptor.Fill(tblAyarlar)
        Catch ex As Exception
            Basarili = False
        End Try
        If tblAyarlar.Rows.Count > 0 Then
            For Each drAyar In tblAyarlar.Rows
                Select Case drAyar("AyarAdi")
                    Case "SimdikiYil"
                        _OgretimYili = CShort(drAyar("Deger"))
                    Case "SimdikiDonem"
                        _Donem = CByte(drAyar("Deger"))
                    Case "AnketAktif"
                        _AnketAktif = CByte(drAyar("Deger"))
                End Select
            Next
        Else
            Basarili = False
        End If
        Return Basarili
    End Function
    Public Function AyarlariKaydet() As Boolean
        Dim Sorgu As String
        Dim Basarili As Boolean = True
        Dim Komut As New OdbcCommand
        Komut.Connection = Baglanti
        Try
            Sorgu = "UPDATE Ayarlar SET Deger='" & _OgretimYili.ToString & "' WHERE AyarAdi='SimdikiYil'"
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            Sorgu = "UPDATE Ayarlar SET Deger='" & _Donem.ToString & "' WHERE AyarAdi='SimdikiDonem'"
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            Sorgu = "UPDATE Ayarlar SET Deger='" & _AnketAktif.ToString & "' WHERE AyarAdi='AnketAktif'"
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
        Catch ex As Exception
            Basarili = False
        End Try
        Return Basarili
    End Function
End Class
