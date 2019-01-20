Imports System.Data.Odbc
Public Class clsTekAnket
    'Veritabaný nesneleri
    Private Sorgu As String
    Private Komut As New OdbcCommand
    Private Adaptor As New OdbcDataAdapter
    'Private deðiþkenler
    Private _CevapNo As Long
    Private _AnketNo As Long
    Private _DersNo As Long
    Private _Yorum As String
    Private _SoruSayisi As Byte
    Private _Sorular As New Collection
    'Public Özellikler
    Public Property CevapNo() As Long
        Get
            Return _CevapNo
        End Get
        Set(ByVal Value As Long)
            _CevapNo = Value
        End Set
    End Property
    Public Property AnketNo() As Long
        Get
            Return _AnketNo
        End Get
        Set(ByVal Value As Long)
            _AnketNo = Value
        End Set
    End Property
    Public Property DersNo() As Long
        Get
            Return _DersNo
        End Get
        Set(ByVal Value As Long)
            _DersNo = Value
        End Set
    End Property
    Public ReadOnly Property Sorular() As Collection
        Get
            Return _Sorular
        End Get
    End Property
    Public ReadOnly Property Yorum() As String
        Get
            Return _Yorum
        End Get
    End Property
    'Kurucu Fonksyon
    Public Sub New()
        Komut.Connection = Baglanti
        Adaptor.SelectCommand = Komut
    End Sub
    Public Function VeritabanindanAl() As Boolean
        Dim Sonuc As Boolean = True
        Dim birSecenek As clsAnketSoruSecenekleri
        Dim birSoru As clsAnketSorusu
        'Veritabanýndan cevap satýrý alýnýyor
        Dim strCevaplar As String
        Sorgu = "SELECT AnketNo,Cevaplar,Yorum FROM AnketCevaplar WHERE CevapNo=" & CevapNo & " AND DersNo=" & DersNo & " AND Tur=2"
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                _AnketNo = Okuyucu("AnketNo")
                _Yorum = Okuyucu("Yorum")
                strCevaplar = Okuyucu("Cevaplar")
            Else
                Sonuc = False
            End If
        Catch ex As Exception
            Sonuc = False
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        'Anket bilgileri veritabanýndan alýnýyor
        If Sonuc Then
            'AnketSorulari tablosundan
            Dim tblSorular As New DataTable
            Sorgu = "SELECT * FROM AnketSorulari WHERE AnketNo=" & AnketNo & " AND SoruTuru=2 ORDER BY SoruSirasi"
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(tblSorular)
            Catch ex As Exception
                Sonuc = False
            End Try
            'sorular koleksiyona atýlýyor
            If tblSorular.Rows.Count > 0 Then
                _SoruSayisi = tblSorular.Rows.Count
                Dim drSoru As DataRow
                For Each drSoru In tblSorular.Rows
                    birSoru = New clsAnketSorusu
                    birSoru.AnketSoruNo = drSoru("AnketSoruNo")
                    birSoru.SoruMetni = drSoru("Metin")
                    birSoru.SoruTuru = drSoru("SoruTuru")
                    birSoru.SoruSirasi = drSoru("SoruSirasi")
                    _Sorular.Add(birSoru, CStr(drSoru("AnketSoruNo")))
                Next
            End If
            tblSorular.Dispose()
        End If
        'Soru seçenekleri alýnýyor
        Dim tblSoruSecenekleri As New DataTable
        If Sonuc Then
            Sorgu = "SELECT AnketSoruSecenekleri.* FROM AnketSorulari,AnketSoruSecenekleri" & _
            " WHERE AnketSoruSecenekleri.AnketNo=" & AnketNo & _
            " AND AnketSorulari.AnketSoruNo=AnketSoruSecenekleri.AnketSoruNo AND AnketSorulari.SoruTuru=2 ORDER BY AnketSoruSecenekleri.SayisalDeger"
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(tblSoruSecenekleri)
            Catch ex As Exception
                Sonuc = False
            End Try
            'Soru seçenekleri koleksiyonlara atýlýyor
            If tblSoruSecenekleri.Rows.Count > 0 Then
                Dim drSecenek As DataRow
                For Each drSecenek In tblSoruSecenekleri.Rows
                    birSecenek = New clsAnketSoruSecenekleri
                    birSecenek.SayisalDeger = drSecenek("SayisalDeger")
                    birSecenek.Metin = drSecenek("Metin")
                    birSoru = _Sorular(CStr(drSecenek("AnketSoruNo")))
                    birSoru.Secenekler.Add(birSecenek, CStr(drSecenek("AnketSoruSecenekNo")))
                    If birSoru.AsgariPuan > birSecenek.SayisalDeger Then
                        birSoru.AsgariPuan = birSecenek.SayisalDeger
                    End If
                    If birSoru.AzamiPuan < birSecenek.SayisalDeger Then
                        birSoru.AzamiPuan = birSecenek.SayisalDeger
                    End If
                Next
            End If
        End If
        'Seçilmiþ seçenekler belirleniyor
        If Sonuc Then
            Dim i As Byte
            Dim dCevaplar() As String = strCevaplar.Split(",")
            For i = 0 To _SoruSayisi - 1
                birSoru = _Sorular(i + 1)
                birSecenek = birSoru.Secenekler(dCevaplar(i))
                birSecenek.OySayisi = 1
            Next
        End If
        Return Sonuc
    End Function
End Class
Public Class clsGenelTekAnket
    'Veritabaný nesneleri
    Private Sorgu As String
    Private Komut As New OdbcCommand
    Private Adaptor As New OdbcDataAdapter
    'Private deðiþkenler
    Private _CevapNo As Long
    Private _AnketNo As Long
    Private _Yorum As String
    Private _SoruSayisi As Byte
    Private _Sorular As New Collection
    'Public Özellikler
    Public Property CevapNo() As Long
        Get
            Return _CevapNo
        End Get
        Set(ByVal Value As Long)
            _CevapNo = Value
        End Set
    End Property
    Public Property AnketNo() As Long
        Get
            Return _AnketNo
        End Get
        Set(ByVal Value As Long)
            _AnketNo = Value
        End Set
    End Property
    Public ReadOnly Property Sorular() As Collection
        Get
            Return _Sorular
        End Get
    End Property
    Public ReadOnly Property Yorum() As String
        Get
            Return _Yorum
        End Get
    End Property
    'Kurucu Fonksyon
    Public Sub New()
        Komut.Connection = Baglanti
        Adaptor.SelectCommand = Komut
    End Sub
    Public Function VeritabanindanAl() As Boolean
        Dim Sonuc As Boolean = True
        Dim birSecenek As clsAnketSoruSecenekleri
        Dim birSoru As clsAnketSorusu
        'Veritabanýndan cevap satýrý alýnýyor
        Dim strCevaplar As String
        Sorgu = "SELECT AnketNo,Cevaplar,Yorum FROM AnketCevaplar WHERE CevapNo=" & CevapNo & " AND Tur=1"
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                _AnketNo = Okuyucu("AnketNo")
                _Yorum = Okuyucu("Yorum")
                strCevaplar = Okuyucu("Cevaplar")
            Else
                Sonuc = False
            End If
        Catch ex As Exception
            Sonuc = False
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        'Anket bilgileri veritabanýndan alýnýyor
        If Sonuc Then
            'AnketSorulari tablosundan
            Dim tblSorular As New DataTable
            Sorgu = "SELECT * FROM AnketSorulari WHERE AnketNo=" & AnketNo & " AND SoruTuru=1 ORDER BY SoruSirasi"
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(tblSorular)
            Catch ex As Exception
                Sonuc = False
            End Try
            'sorular koleksiyona atýlýyor
            If tblSorular.Rows.Count > 0 Then
                _SoruSayisi = tblSorular.Rows.Count
                Dim drSoru As DataRow
                For Each drSoru In tblSorular.Rows
                    birSoru = New clsAnketSorusu
                    birSoru.AnketSoruNo = drSoru("AnketSoruNo")
                    birSoru.SoruMetni = drSoru("Metin")
                    birSoru.SoruTuru = drSoru("SoruTuru")
                    birSoru.SoruSirasi = drSoru("SoruSirasi")
                    _Sorular.Add(birSoru, CStr(drSoru("AnketSoruNo")))
                Next
            End If
            tblSorular.Dispose()
        End If
        'Soru seçenekleri alýnýyor
        Dim tblSoruSecenekleri As New DataTable
        If Sonuc Then
            Sorgu = "SELECT AnketSoruSecenekleri.* FROM AnketSorulari,AnketSoruSecenekleri" & _
            " WHERE AnketSoruSecenekleri.AnketNo=" & AnketNo & _
            " AND AnketSorulari.AnketSoruNo=AnketSoruSecenekleri.AnketSoruNo AND AnketSorulari.SoruTuru=1 ORDER BY AnketSoruSecenekleri.SayisalDeger"
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(tblSoruSecenekleri)
            Catch ex As Exception
                Sonuc = False
            End Try
            'Soru seçenekleri koleksiyonlara atýlýyor
            If tblSoruSecenekleri.Rows.Count > 0 Then
                Dim drSecenek As DataRow
                For Each drSecenek In tblSoruSecenekleri.Rows
                    birSecenek = New clsAnketSoruSecenekleri
                    birSecenek.SayisalDeger = drSecenek("SayisalDeger")
                    birSecenek.Metin = drSecenek("Metin")
                    birSoru = _Sorular(CStr(drSecenek("AnketSoruNo")))
                    birSoru.Secenekler.Add(birSecenek, CStr(drSecenek("AnketSoruSecenekNo")))
                    If birSoru.AsgariPuan > birSecenek.SayisalDeger Then
                        birSoru.AsgariPuan = birSecenek.SayisalDeger
                    End If
                    If birSoru.AzamiPuan < birSecenek.SayisalDeger Then
                        birSoru.AzamiPuan = birSecenek.SayisalDeger
                    End If
                Next
            End If
        End If
        'Seçilmiþ seçenekler belirleniyor
        If Sonuc Then
            Dim i As Byte
            Dim dCevaplar() As String = strCevaplar.Split(",")
            For i = 0 To _SoruSayisi - 1
                birSoru = _Sorular(i + 1)
                birSecenek = birSoru.Secenekler(dCevaplar(i))
                birSecenek.OySayisi = 1
            Next
        End If
        Return Sonuc
    End Function
End Class
