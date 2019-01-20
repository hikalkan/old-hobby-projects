Imports System
Imports System.Data
'Tablonun bi�im bilgileri 
Public Class clsTablomBicim
    'TABLONUN G�R�N�R �ZELL�KLER�
    'Ba�l���n Rengi
    Private gBaslikRenk As String = "#000000"
    'Ba�l���n Arkaplan�
    Private gBaslikArkaRenk As String = "#FFFFFF"
    'Ba�l���n Fontu
    Private gBaslikFont As String = "Comic Sans Ms"
    'Ba�l���n Boyutu
    Private gBaslikBoyut As String = "20px"
    'Ba�l���n kal�nl���
    Private gBaslikKalin As Boolean = True

    'Kolon adlar�n�n rengi
    Private gKolonBaslikYaziRenk As String = "#000000"
    'Kolon adlar�n�n arka rengi
    Private gKolonBaslikArkaRenk As String = "#CCCCCC"
    'Kolon Ba�l���n�n Fontu
    Private gKolonBaslikFont As String = "Verdana"
    'Kolon Ba�l���n�n Boyutu
    Private gKolonBaslikBoyut As String = "12px"
    'Kolon Ba�l���n�n kal�nl���
    Private gKolonBaslikKalin As Boolean = True

    'Sat�rlar�n arkaplan rengi
    Private gSatirArkaRenk1 As String = "#EEEEEE"
    'Sat�rlar�n (alternatif) arkaplan rengi
    Private gSatirArkaRenk2 As String = "#DDDDDD"
    'Sat�rlar�n yaz� rengi
    Private gSatirYaziRenk1 As String = "#000000"
    'Sat�rlar�n (alternatif) yaz� rengi
    Private gSatirYaziRenk2 As String = "#000000"

    'Tablonun geni�li�i
    Private gGenislik As String = "98%"
    'Tablonun hizalanmas�
    Private gHizalama As String = "center"
    'Tablonun arkaplan rengi
    Private gTabloArkaRenk As String = "#CCCCCC"
    '��eri�in yaz� fontu
    Private gYaziFont As String = "Verdana"
    '��eri�in yaz� boyutu
    Private gYaziBoyut As String = "12px"

    ' *** �ZELL�KLER ***
    'Tablonun ba�l���n�n rengini ayarlamak i�in
    Public Property BaslikRenk() As String
        Get
            BaslikRenk = gBaslikRenk
        End Get
        Set(ByVal YeniRenk As String)
            gBaslikRenk = YeniRenk
        End Set
    End Property
    'Tablonun ba�l���n�n arka rengini ayarlamak i�in
    Public Property BaslikArkaRenk() As String
        Get
            BaslikArkaRenk = gBaslikArkaRenk
        End Get
        Set(ByVal YeniRenk As String)
            gBaslikArkaRenk = YeniRenk
        End Set
    End Property
    'Tablonun ba�l���n�n fontu
    Public Property BaslikFont() As String
        Get
            BaslikFont = gBaslikFont
        End Get
        Set(ByVal YeniFont As String)
            gBaslikFont = YeniFont
        End Set
    End Property
    'Tablonun ba�l���n�n boyutu
    Public Property BaslikBoyut() As String
        Get
            BaslikBoyut = gBaslikBoyut
        End Get
        Set(ByVal YeniBoyut As String)
            gBaslikBoyut = YeniBoyut
        End Set
    End Property
    'Tablonun ba�l���n�n kal�nl�k durumu
    Public Property BaslikKalin() As Boolean
        Get
            BaslikKalin = gBaslikKalin
        End Get
        Set(ByVal YeniDeger As Boolean)
            gBaslikKalin = YeniDeger
        End Set
    End Property
    'Kolon ba�l�klar� yaz� rengi
    Public Property KolonBaslikYaziRenk() As String
        Get
            KolonBaslikYaziRenk = gKolonBaslikYaziRenk
        End Get
        Set(ByVal YeniRenk As String)
            gKolonBaslikYaziRenk = YeniRenk
        End Set
    End Property
    'Kolon ba�l�klar� arkaplan rengi
    Public Property KolonBaslikArkaRenk() As String
        Get
            KolonBaslikArkaRenk = gKolonBaslikArkaRenk
        End Get
        Set(ByVal YeniRenk As String)
            gKolonBaslikArkaRenk = YeniRenk
        End Set
    End Property
    'Kolon ba�l���n�n fontu
    Public Property KolonBaslikFont() As String
        Get
            KolonBaslikFont = gKolonBaslikFont
        End Get
        Set(ByVal YeniFont As String)
            gKolonBaslikFont = YeniFont
        End Set
    End Property
    'Kolon ba�l���n�n boyutu
    Public Property KolonBaslikBoyut() As String
        Get
            KolonBaslikBoyut = gKolonBaslikBoyut
        End Get
        Set(ByVal YeniBoyut As String)
            gKolonBaslikBoyut = YeniBoyut
        End Set
    End Property
    'Kolon ba�l���n�n kan�ll�k durumu
    Public Property KolonBaslikKalin() As Boolean
        Get
            KolonBaslikKalin = gKolonBaslikKalin
        End Get
        Set(ByVal YeniDeger As Boolean)
            gKolonBaslikKalin = YeniDeger
        End Set
    End Property
    'Tablonun geni�li�ini de�i�tirir veya elde eder (pixel veya y�zde)
    Public Property Genislik() As String
        Get
            Genislik = gGenislik
        End Get
        Set(ByVal YeniGenislik As String)
            gGenislik = YeniGenislik
        End Set
    End Property
    'Tablo hizalamas�
    Public Property Hizalama() As String
        Get
            Hizalama = gHizalama
        End Get
        Set(ByVal YeniHizalama As String)
            gHizalama = YeniHizalama
        End Set
    End Property
    'ArkaPlan renklerini belirlemek ve elde etmek i�in
    Public Property TabloArkaRenk() As String
        Get
            TabloArkaRenk = gTabloArkaRenk
        End Get
        Set(ByVal YeniRenk As String)
            gTabloArkaRenk = YeniRenk
        End Set
    End Property
    Public Property SatirArkaRenk1() As String
        Get
            SatirArkaRenk1 = gSatirArkaRenk1
        End Get
        Set(ByVal YeniRenk As String)
            gSatirArkaRenk1 = YeniRenk
        End Set
    End Property
    Public Property SatirArkaRenk2() As String
        Get
            SatirArkaRenk2 = gSatirArkaRenk2
        End Get
        Set(ByVal YeniRenk As String)
            gSatirArkaRenk2 = YeniRenk
        End Set
    End Property
    'Yaz� renklerini belirlemek ve elde etmek i�in
    Public Property SatirYaziRenk1() As String
        Get
            SatirYaziRenk1 = gSatirYaziRenk1
        End Get
        Set(ByVal YeniRenk As String)
            gSatirYaziRenk1 = YeniRenk
        End Set
    End Property
    Public Property SatirYaziRenk2() As String
        Get
            SatirYaziRenk2 = gSatirYaziRenk2
        End Get
        Set(ByVal YeniRenk As String)
            gSatirYaziRenk2 = YeniRenk
        End Set
    End Property
    'Tablodaki normal yaz�lar�n fontu
    Public Property YaziFont() As String
        Get
            YaziFont = gYaziFont
        End Get
        Set(ByVal YeniFont As String)
            gYaziFont = YeniFont
        End Set
    End Property
    'Tablodaki normal yaz�lar�n boyutu
    Public Property YaziBoyut() As String
        Get
            YaziBoyut = gYaziBoyut
        End Get
        Set(ByVal YeniBoyut As String)
            gYaziBoyut = YeniBoyut
        End Set
    End Property
End Class
'Tablodaki bir h�cre
Public Class clsHucrem
    'H�crenin i�eri�i
    Private gMetin As String = ""
    'H�crenin i�eri�ini de�i�tirmek ya da elde etmek i�in
    Public Property Metin() As String
        Get
            Metin = gMetin
        End Get
        Set(ByVal YeniMetin As String)
            gMetin = YeniMetin
        End Set
    End Property
End Class
'Tablonun bir sat�r�
Public Class clsSatirim
    'Sat�rdaki h�creler
    Private gHucreler As New Collection
    'H�crelerin say�s�
    Private gHucreSayisi As Byte = 0
    'H�cre say�s�n� verir
    Public ReadOnly Property HucreSayisi() As Byte
        Get
            HucreSayisi = gHucreSayisi
        End Get
    End Property
    'H�creleri elde etmek i�in
    Public ReadOnly Property Hucreler() As Collection
        Get
            Hucreler = gHucreler
        End Get
    End Property
    'Sat�ra yeni h�cre eklemek i�in
    Public Sub HucreEkle(ByVal YeniHucre As clsHucrem)
        'yeni h�cre ekleniyor
        gHucreler.Add(YeniHucre, HucreSayisi + 1)
        'h�cre say�s� art�r�l�yor
        gHucreSayisi = gHucreSayisi + 1
    End Sub
End Class
'Kolon S�n�f�
Public Class clsKolonum
    'Kolonun ba�l���
    Private gBaslik As String = ""
    'Kolonun geni�li�i (y�zde veya pixel)
    Private gGenislik As String = "100%"
    'i�erik hizalama
    Private gicerikHizalama As String = "left"
    'kolon ba�l��� hizalama
    Private gBaslikHizalama As String = "center"
    'Ba�l�k de�erini de�i�tirmek ya da elde etmek i�in
    Public Property Baslik() As String
        Get
            Baslik = gBaslik
        End Get
        Set(ByVal YeniBaslik As String)
            gBaslik = YeniBaslik
        End Set
    End Property
    'Geni�lik de�erini elde etmek ya da de�i�tirmek i�in
    Public Property Genislik() As String
        Get
            Genislik = gGenislik
        End Get
        Set(ByVal YeniGenislik As String)
            gGenislik = YeniGenislik
        End Set
    End Property
    '��erik hizalama
    Public Property icerikHizalama() As String
        Get
            icerikHizalama = gicerikHizalama
        End Get
        Set(ByVal YeniHizalama As String)
            gicerikHizalama = YeniHizalama
        End Set
    End Property
    'Ba�l�k hizalama
    Public Property BaslikHizalama() As String
        Get
            BaslikHizalama = gBaslikHizalama
        End Get
        Set(ByVal YeniHizalama As String)
            gBaslikHizalama = YeniHizalama
        End Set
    End Property
End Class
'Tablo s�n�f�
Public Class clsTablom
    ' *** DE���KENLER ***
    'Tablonun ana ba�l���
    Private gBaslik As String = ""
    'Tablonun Kolonlar�
    Private gKolonlar As New Collection
    'Tablonun kolon say�s�
    Private gKolonSayisi As Byte = 0
    'Tablodaki Sat�rlar
    Private gSatirlar As New Collection
    'Tablonun sat�r say�s�
    Private gSatirSayisi As Byte = 0
    'Tablonun bi�imi
    Private gBicim As New clsTablomBicim
    'Tablonun Html kodu
    Private gHtmlKodu As String = ""

    ' *** �ZELL�KLER ***
    'Tablonun ana ba�l���n� de�i�tirmek i�in
    Public Property Baslik() As String
        Get
            Baslik = gBaslik
        End Get
        Set(ByVal YeniBaslik As String)
            gBaslik = YeniBaslik
        End Set
    End Property
    'Kolon say�s�n� verir
    Public ReadOnly Property KolonSayisi() As Byte
        Get
            KolonSayisi = gKolonSayisi
        End Get
    End Property
    'Sat�r say�s�n� verir
    Public ReadOnly Property SatirSayisi() As Integer
        Get
            SatirSayisi = gSatirSayisi
        End Get
    End Property
    'Tablonun bi�imini de�i�tir ya da ver
    Public Property Bicim() As clsTablomBicim
        Get
            Bicim = gBicim
        End Get
        Set(ByVal YeniBicim As clsTablomBicim)
            gBicim = YeniBicim
        End Set
    End Property
    'Tablonun Html kodunu verir
    Public ReadOnly Property HtmlKodu() As String
        Get
            HtmlKodu = gHtmlKodu
        End Get
    End Property

    ' *** Y�NTEMLER (METODLAR) ***
    'Tabloya yeni sat�r eklemek i�in
    Public Sub SatirEkle(ByVal YeniSatir As clsSatirim)
        gSatirlar.Add(YeniSatir, SatirSayisi + 1)
        gSatirSayisi = gSatirSayisi + 1
    End Sub
    'Yeni kolon eklemek i�in
    Public Sub KolonEkle(ByVal YeniKolon As clsKolonum)
        'Yeni kolon ekleniyor
        gKolonlar.Add(YeniKolon, gKolonSayisi + 1)
        'Kolon say�s� art�r�l�yor
        gKolonSayisi = gKolonSayisi + 1
    End Sub
    'Tablonun html kodunu �retir
    Public Sub HtmlKoduUret()
        'de�i�kenler
        Dim kod As String ' ge�ici kod
        Dim i As Integer ' saya� de�i�kenleri
        Dim birSatir As clsSatirim ' ge�ici sat�r
        Dim Arenk As String = gBicim.SatirArkaRenk1
        Dim Yrenk As String = gBicim.SatirYaziRenk1
        kod = "<table width='" & gBicim.Genislik & "' bgcolor='" & gBicim.TabloArkaRenk & "' align='" & gBicim.Hizalama & "'>"
        'Tablo ba�l��� yaz�l�yor
        kod = kod & "<tr><td width='100%' colspan='" & gKolonSayisi & "' " & _
        "bgcolor='" & gBicim.BaslikArkaRenk & "' align='center'>"
        kod = kod & "<font face='" & gBicim.BaslikFont & "' style='font-size:" & gBicim.BaslikBoyut & ";color:" & gBicim.BaslikRenk & "'>"
        If gBicim.BaslikKalin Then kod = kod & "<b>"
        kod = kod & gBaslik 'ba�l�k metni
        If gBicim.BaslikKalin Then kod = kod & "<b>"
        kod = kod & "</font></td></tr>" 'sat�r kapan���
        'Kolon ba�l�klar� yaz�l�yor
        kod = kod & "<tr>"
        For i = 1 To gKolonlar.Count ' her kolon i�in
            'Kolon a��l�yor ve �zellikleri yaz�l�yor
            kod = kod & "<td width='" & gKolonlar(i).Genislik & "' bgcolor='" & gBicim.KolonBaslikArkaRenk & "' align='" & gKolonlar(i).BaslikHizalama & "'>"
            'font ayarlan�yor
            kod = kod & "<Font Face='" & gBicim.KolonBaslikFont & "' style='font-size:" & gBicim.KolonBaslikBoyut & ";color:" & gBicim.KolonBaslikYaziRenk & "'>"
            'kolonun ba�l��� yaz�l�yor
            If gBicim.KolonBaslikKalin Then kod = kod & "<b>"
            kod = kod & gKolonlar(i).Baslik
            If gBicim.KolonBaslikKalin Then kod = kod & "</b>"
            'kolon kapan�yor
            kod = kod & "</font></td>"
        Next i
        kod = kod & "</tr>"
        'tablonun veri sat�rlar� yaz�l�yor
        For Each birSatir In gSatirlar ' her sat�r i�in
            kod = kod & "<tr>"
            'Sat�rdaki h�creler yaz�l�yor
            For i = 1 To birSatir.Hucreler.Count ' her h�cre i�in
                'h�cre a��l�yor ve �zellikleri yaz�l�yor
                kod = kod & "<td bgcolor='" & Arenk & "' align='" & gKolonlar(i).icerikHizalama & "'>"
                'yaz� �ekli yaz�l�yor
                kod = kod & "<Font Face='" & gBicim.YaziFont & "' style='font-size:" & gBicim.YaziBoyut & ";color:" & Yrenk & "'>"
                'veri yaz�l�yor
                kod = kod & birSatir.Hucreler(i).Metin
                'h�cre kapan�yor
                kod = kod & "</Font></td>"
            Next i
            kod = kod & "</tr>" 'sat�r kapan�yor
            'sat�r arkaplan renk de�i�tiriliyor
            If Arenk = gBicim.SatirArkaRenk1 Then Arenk = gBicim.SatirArkaRenk2 Else Arenk = gBicim.SatirArkaRenk1
            'sat�r yaz� rengi de�i�tiriliyor
            If Yrenk = gBicim.SatirYaziRenk1 Then Yrenk = gBicim.SatirYaziRenk2 Else Yrenk = gBicim.SatirYaziRenk1
        Next 'Each birSatir
        kod = kod & "</table>" ' tablo kapan�yor
        gHtmlKodu = kod ' ge�ici kod as�l de�i�kene aktar�l�yor
    End Sub
    'DataTable nesnesinden tablo olu�turmak i�in
    Public Sub VeriTablosuAl(ByVal VT As DataTable, Optional ByVal VTBaslik As Boolean = False)
        ' VTBaslik: True ise VT'deki Kolon adlar� otomatikmen kolon ad� olarak al�n�r
        Dim i, j As Integer
        Dim genislik As Byte
        Dim birKolon As clsKolonum
        Dim BirHucre As clsHucrem
        Dim BirSatir As clsSatirim
        genislik = CInt(100 / (VT.Columns.Count))
        'E�er Kolon ba�l��� yaz�lmas� gerekse ise bu i� yap�l�yor
        If VTBaslik Then
            gKolonlar = New Collection
            For i = 0 To VT.Columns.Count - 1 ' t�m kolonlar ekleniyor
                birKolon = New clsKolonum
                birKolon.Baslik = VT.Columns(i).ColumnName
                birKolon.Genislik = genislik & "%"
                KolonEkle(birKolon)
            Next
        End If
        '�imdi veriler yaz�l�yor
        For i = 0 To VT.Rows.Count - 1 ' Her sat�r i�in
            BirSatir = New clsSatirim
            For j = 0 To VT.Columns.Count - 1 ' Her alan i�in
                BirHucre = New clsHucrem
                BirHucre.Metin = VT.Rows(i)(j)
                BirSatir.HucreEkle(BirHucre)
            Next
            SatirEkle(BirSatir)
        Next
    End Sub
End Class