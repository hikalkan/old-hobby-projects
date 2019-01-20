Imports System.Data.Odbc
'SORU BÝLGÝLERÝ ÝLE ALAKALI SINIFLAR
Public Class clsAnketSoruSecenekleri
    Public SayisalDeger As Byte
    Public Metin As String
    Public OySayisi As Long = 0
End Class
Public Class clsAnketSorusu
    Public AnketSoruNo As Long
    Public SoruSirasi As Byte
    Public SoruTuru As Byte
    Public SoruMetni As String
    Public AsgariPuan As Byte = 100 'Minimum
    Public AzamiPuan As Byte = 0 'Maksimum
    Public Ortalama As Double = 0
    Public Secenekler As New Collection
End Class
'DERS ANKETÝ
Public Class clsDersAnket
    'Veritabaný nesneleri
    Private Sorgu As String
    Private Komut As New OdbcCommand
    Private Adaptor As New OdbcDataAdapter
    'Private nesneler
    Private _AnketNo As Long = 0
    Private _DersNo As Long = 0
    Private _Aciklama As String
    Private _Sorular As New Collection
    Private _SoruSayisi As Byte = 0
    Private _CevapSayisi As Long = 0
    'Diðer Private deðiþkenler
    Private Yil As Short
    Private Donem As Byte
    'Kurucu Fonksyonlar
    Public Sub New()
        Komut.Connection = Baglanti
        Adaptor.SelectCommand = Komut
    End Sub
    Public Sub New(ByVal Anket_No As Long)
        _AnketNo = Anket_No
        Komut.Connection = Baglanti
        Adaptor.SelectCommand = Komut
    End Sub
    'Public Özellikler
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
    Public ReadOnly Property Aciklama() As String
        Get
            Return _Aciklama
        End Get
    End Property
    Public ReadOnly Property SoruSayisi() As Byte
        Get
            Return _SoruSayisi
        End Get
    End Property
    Public ReadOnly Property CevapSayisi() As Long
        Get
            Return _CevapSayisi
        End Get
    End Property
    Public ReadOnly Property Sorular() As Collection
        Get
            Return _Sorular
        End Get
    End Property
    'Private Fonksyonlar
    Private Function AnketBilgileriniAl() As Boolean
        Dim Sonuc As Boolean = True
        'Anketler tablosundan
        Sorgu = "SELECT Aciklama,Yil,Donem FROM Anketler WHERE AnketNo=" & _AnketNo
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                _Aciklama = Okuyucu("Aciklama")
                Yil = Okuyucu("Yil")
                Donem = Okuyucu("Donem")
            Else
                Sonuc = False
            End If
        Catch ex As Exception
            Sonuc = False
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        'AnketSorulari tablosundan
        Dim tblSorular As New DataTable
        If Sonuc Then
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
                Dim birSoru As clsAnketSorusu
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
        End If
        tblSorular.Dispose()
        'AnketSoruSeçenekleri Tablosunlar
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
                Dim birSecenek As clsAnketSoruSecenekleri
                Dim birSoru As clsAnketSorusu
                For Each drSecenek In tblSoruSecenekleri.Rows
                    birSecenek = New clsAnketSoruSecenekleri
                    birSecenek.SayisalDeger = drSecenek("SayisalDeger")
                    birSecenek.Metin = drSecenek("Metin")
                    birSoru = _Sorular(CStr(drSecenek("AnketSoruNo")))
                    birSoru.Secenekler.Add(birSecenek, CStr(drSecenek("AnketSoruSecenekNo")))
                    If birsoru.AsgariPuan > birSecenek.SayisalDeger Then
                        birsoru.AsgariPuan = birSecenek.SayisalDeger
                    End If
                    If birsoru.AzamiPuan < birSecenek.SayisalDeger Then
                        birsoru.AzamiPuan = birSecenek.SayisalDeger
                    End If
                Next
            End If
        End If
        Return Sonuc
    End Function
    Private Function AnketCevaplariniAl() As Boolean
        Dim Sonuc As Boolean = True
        'AnketCevaplar Tablosundan kayýtlar alýnýyor
        Dim tblCevaplar As New DataTable
        Sorgu = "SELECT Cevaplar FROM AnketCevaplar WHERE AnketNo=" & _AnketNo & " AND DersNo=" & _DersNo & " AND Tur=2"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(tblCevaplar)
        Catch ex As Exception
            Sonuc = False
        End Try
        'Alýnan Cevaplar Deðerlendiriliyor
        Dim birSoru As clsAnketSorusu
        Dim birSecenek As clsAnketSoruSecenekleri
        If Sonuc And tblCevaplar.Rows.Count > 0 Then
            _CevapSayisi = tblCevaplar.Rows.Count
            Dim drCevap As DataRow
            Dim strSecenekNolar(_SoruSayisi) As String
            Dim i As Byte
            Dim CevapMetni As String
            For Each drCevap In tblCevaplar.Rows
                CevapMetni = drCevap("Cevaplar")
                strSecenekNolar = CevapMetni.Split(",")
                For i = 0 To _SoruSayisi - 1
                    birSoru = _Sorular(i + 1)
                    birSecenek = birSoru.Secenekler(strSecenekNolar(i))
                    birSecenek.OySayisi += 1
                Next i
            Next 'For Each drCevap
        End If 'If Sonuc And tblCevaplar.Rows.Count > 0
        'Ortalamalar hesaplanýyor
        If _CevapSayisi > 0 Then
            Dim Toplam As Long
            For Each birSoru In _Sorular
                Toplam = 0
                For Each birSecenek In birSoru.Secenekler
                    Toplam += birSecenek.OySayisi * birSecenek.SayisalDeger
                Next
                birSoru.Ortalama = Toplam / _CevapSayisi
            Next
        End If
        Return Sonuc
    End Function
    Public Function VeriTabanindanAl()
        Try
            If AnketBilgileriniAl() And AnketCevaplariniAl() Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
'GENEL ANKET
Public Class clsGenelAnket
    'Veritabaný nesneleri
    Private Sorgu As String
    Private Komut As New OdbcCommand
    Private Adaptor As New OdbcDataAdapter
    'Private nesneler
    Private _AnketNo As Long = 0
    Private _Aciklama As String
    Private _Sorular As New Collection
    Private _SoruSayisi As Byte = 0
    Private _CevapSayisi As Long = 0
    'Diðer Private deðiþkenler
    Private Yil As Short
    Private Donem As Byte
    'Kurucu Fonksyonlar
    Public Sub New()
        Komut.Connection = Baglanti
        Adaptor.SelectCommand = Komut
    End Sub
    Public Sub New(ByVal Anket_No As Long)
        _AnketNo = Anket_No
        Komut.Connection = Baglanti
        Adaptor.SelectCommand = Komut
    End Sub
    'Public Özellikler
    Public Property AnketNo() As Long
        Get
            Return _AnketNo
        End Get
        Set(ByVal Value As Long)
            _AnketNo = Value
        End Set
    End Property
    Public ReadOnly Property Aciklama() As String
        Get
            Return _Aciklama
        End Get
    End Property
    Public ReadOnly Property SoruSayisi() As Byte
        Get
            Return _SoruSayisi
        End Get
    End Property
    Public ReadOnly Property CevapSayisi() As Long
        Get
            Return _CevapSayisi
        End Get
    End Property
    Public ReadOnly Property Sorular() As Collection
        Get
            Return _Sorular
        End Get
    End Property
    'Private Fonksyonlar
    Private Function AnketBilgileriniAl() As Boolean
        Dim Sonuc As Boolean = True
        'Anketler tablosundan
        Sorgu = "SELECT Aciklama,Yil,Donem FROM Anketler WHERE AnketNo=" & _AnketNo
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                _Aciklama = Okuyucu("Aciklama")
                Yil = Okuyucu("Yil")
                Donem = Okuyucu("Donem")
            Else
                Sonuc = False
            End If
        Catch ex As Exception
            Sonuc = False
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        'AnketSorulari tablosundan
        Dim tblSorular As New DataTable
        If Sonuc Then
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
                Dim birSoru As clsAnketSorusu
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
        End If
        tblSorular.Dispose()
        'AnketSoruSeçenekleri Tablosundan
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
                Dim birSecenek As clsAnketSoruSecenekleri
                Dim birSoru As clsAnketSorusu
                For Each drSecenek In tblSoruSecenekleri.Rows
                    birSecenek = New clsAnketSoruSecenekleri
                    birSecenek.SayisalDeger = drSecenek("SayisalDeger")
                    birSecenek.Metin = drSecenek("Metin")
                    birSoru = _Sorular(CStr(drSecenek("AnketSoruNo")))
                    birSoru.Secenekler.Add(birSecenek, CStr(drSecenek("AnketSoruSecenekNo")))
                    If birsoru.AsgariPuan > birSecenek.SayisalDeger Then
                        birsoru.AsgariPuan = birSecenek.SayisalDeger
                    End If
                    If birsoru.AzamiPuan < birSecenek.SayisalDeger Then
                        birsoru.AzamiPuan = birSecenek.SayisalDeger
                    End If
                Next
            End If
        End If
        Return Sonuc
    End Function
    Private Function AnketCevaplariniAl() As Boolean
        Dim Sonuc As Boolean = True
        'AnketCevaplar Tablosundan kayýtlar alýnýyor
        Dim tblCevaplar As New DataTable
        Sorgu = "SELECT Cevaplar FROM AnketCevaplar WHERE AnketNo=" & _AnketNo & " AND Tur=1"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(tblCevaplar)
        Catch ex As Exception
            Sonuc = False
        End Try
        'Alýnan Cevaplar Deðerlendiriliyor
        Dim birSoru As clsAnketSorusu
        Dim birSecenek As clsAnketSoruSecenekleri
        If Sonuc And tblCevaplar.Rows.Count > 0 Then
            _CevapSayisi = tblCevaplar.Rows.Count
            Dim drCevap As DataRow
            Dim strSecenekNolar(_SoruSayisi) As String
            Dim i As Byte
            Dim CevapMetni As String
            For Each drCevap In tblCevaplar.Rows
                CevapMetni = drCevap("Cevaplar")
                strSecenekNolar = CevapMetni.Split(",")
                For i = 0 To _SoruSayisi - 1
                    birSoru = _Sorular(i + 1)
                    birSecenek = birSoru.Secenekler(strSecenekNolar(i))
                    birSecenek.OySayisi += 1
                Next i
            Next 'For Each drCevap
        End If 'If Sonuc And tblCevaplar.Rows.Count > 0
        'Ortalamalar hesaplanýyor
        If _CevapSayisi > 0 Then
            Dim Toplam As Long
            For Each birSoru In _Sorular
                Toplam = 0
                For Each birSecenek In birSoru.Secenekler
                    Toplam += birSecenek.OySayisi * birSecenek.SayisalDeger
                Next
                birSoru.Ortalama = Toplam / _CevapSayisi
            Next
        End If
        Return Sonuc
    End Function
    Public Function VeriTabanindanAl()
        Try
            If AnketBilgileriniAl() And AnketCevaplariniAl() Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class

