Imports System
Imports System.Data
'Tablonun biçim bilgileri 
Public Class clsTablomBicim
    'TABLONUN GÖRÜNÜR ÖZELLÝKLERÝ
    'Baþlýðýn Rengi
    Private gBaslikRenk As String = "#000000"
    'Baþlýðýn Arkaplaný
    Private gBaslikArkaRenk As String = "#FFFFFF"
    'Baþlýðýn Fontu
    Private gBaslikFont As String = "Comic Sans Ms"
    'Baþlýðýn Boyutu
    Private gBaslikBoyut As String = "20px"
    'Baþlýðýn kalýnlýðý
    Private gBaslikKalin As Boolean = True

    'Kolon adlarýnýn rengi
    Private gKolonBaslikYaziRenk As String = "#000000"
    'Kolon adlarýnýn arka rengi
    Private gKolonBaslikArkaRenk As String = "#CCCCCC"
    'Kolon Baþlýðýnýn Fontu
    Private gKolonBaslikFont As String = "Verdana"
    'Kolon Baþlýðýnýn Boyutu
    Private gKolonBaslikBoyut As String = "12px"
    'Kolon Baþlýðýnýn kalýnlýðý
    Private gKolonBaslikKalin As Boolean = True

    'Satýrlarýn arkaplan rengi
    Private gSatirArkaRenk1 As String = "#EEEEEE"
    'Satýrlarýn (alternatif) arkaplan rengi
    Private gSatirArkaRenk2 As String = "#DDDDDD"
    'Satýrlarýn yazý rengi
    Private gSatirYaziRenk1 As String = "#000000"
    'Satýrlarýn (alternatif) yazý rengi
    Private gSatirYaziRenk2 As String = "#000000"

    'Tablonun geniþliði
    Private gGenislik As String = "98%"
    'Tablonun hizalanmasý
    Private gHizalama As String = "center"
    'Tablonun arkaplan rengi
    Private gTabloArkaRenk As String = "#CCCCCC"
    'Ýçeriðin yazý fontu
    Private gYaziFont As String = "Verdana"
    'Ýçeriðin yazý boyutu
    Private gYaziBoyut As String = "12px"

    ' *** ÖZELLÝKLER ***
    'Tablonun baþlýðýnýn rengini ayarlamak için
    Public Property BaslikRenk() As String
        Get
            BaslikRenk = gBaslikRenk
        End Get
        Set(ByVal YeniRenk As String)
            gBaslikRenk = YeniRenk
        End Set
    End Property
    'Tablonun baþlýðýnýn arka rengini ayarlamak için
    Public Property BaslikArkaRenk() As String
        Get
            BaslikArkaRenk = gBaslikArkaRenk
        End Get
        Set(ByVal YeniRenk As String)
            gBaslikArkaRenk = YeniRenk
        End Set
    End Property
    'Tablonun baþlýðýnýn fontu
    Public Property BaslikFont() As String
        Get
            BaslikFont = gBaslikFont
        End Get
        Set(ByVal YeniFont As String)
            gBaslikFont = YeniFont
        End Set
    End Property
    'Tablonun baþlýðýnýn boyutu
    Public Property BaslikBoyut() As String
        Get
            BaslikBoyut = gBaslikBoyut
        End Get
        Set(ByVal YeniBoyut As String)
            gBaslikBoyut = YeniBoyut
        End Set
    End Property
    'Tablonun baþlýðýnýn kalýnlýk durumu
    Public Property BaslikKalin() As Boolean
        Get
            BaslikKalin = gBaslikKalin
        End Get
        Set(ByVal YeniDeger As Boolean)
            gBaslikKalin = YeniDeger
        End Set
    End Property
    'Kolon baþlýklarý yazý rengi
    Public Property KolonBaslikYaziRenk() As String
        Get
            KolonBaslikYaziRenk = gKolonBaslikYaziRenk
        End Get
        Set(ByVal YeniRenk As String)
            gKolonBaslikYaziRenk = YeniRenk
        End Set
    End Property
    'Kolon baþlýklarý arkaplan rengi
    Public Property KolonBaslikArkaRenk() As String
        Get
            KolonBaslikArkaRenk = gKolonBaslikArkaRenk
        End Get
        Set(ByVal YeniRenk As String)
            gKolonBaslikArkaRenk = YeniRenk
        End Set
    End Property
    'Kolon baþlýðýnýn fontu
    Public Property KolonBaslikFont() As String
        Get
            KolonBaslikFont = gKolonBaslikFont
        End Get
        Set(ByVal YeniFont As String)
            gKolonBaslikFont = YeniFont
        End Set
    End Property
    'Kolon baþlýðýnýn boyutu
    Public Property KolonBaslikBoyut() As String
        Get
            KolonBaslikBoyut = gKolonBaslikBoyut
        End Get
        Set(ByVal YeniBoyut As String)
            gKolonBaslikBoyut = YeniBoyut
        End Set
    End Property
    'Kolon baþlýðýnýn kanýllýk durumu
    Public Property KolonBaslikKalin() As Boolean
        Get
            KolonBaslikKalin = gKolonBaslikKalin
        End Get
        Set(ByVal YeniDeger As Boolean)
            gKolonBaslikKalin = YeniDeger
        End Set
    End Property
    'Tablonun geniþliðini deðiþtirir veya elde eder (pixel veya yüzde)
    Public Property Genislik() As String
        Get
            Genislik = gGenislik
        End Get
        Set(ByVal YeniGenislik As String)
            gGenislik = YeniGenislik
        End Set
    End Property
    'Tablo hizalamasý
    Public Property Hizalama() As String
        Get
            Hizalama = gHizalama
        End Get
        Set(ByVal YeniHizalama As String)
            gHizalama = YeniHizalama
        End Set
    End Property
    'ArkaPlan renklerini belirlemek ve elde etmek için
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
    'Yazý renklerini belirlemek ve elde etmek için
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
    'Tablodaki normal yazýlarýn fontu
    Public Property YaziFont() As String
        Get
            YaziFont = gYaziFont
        End Get
        Set(ByVal YeniFont As String)
            gYaziFont = YeniFont
        End Set
    End Property
    'Tablodaki normal yazýlarýn boyutu
    Public Property YaziBoyut() As String
        Get
            YaziBoyut = gYaziBoyut
        End Get
        Set(ByVal YeniBoyut As String)
            gYaziBoyut = YeniBoyut
        End Set
    End Property
End Class
'Tablodaki bir hücre
Public Class clsHucrem
    'Hücrenin içeriði
    Private gMetin As String = ""
    'Hücrenin içeriðini deðiþtirmek ya da elde etmek için
    Public Property Metin() As String
        Get
            Metin = gMetin
        End Get
        Set(ByVal YeniMetin As String)
            gMetin = YeniMetin
        End Set
    End Property
End Class
'Tablonun bir satýrý
Public Class clsSatirim
    'Satýrdaki hücreler
    Private gHucreler As New Collection
    'Hücrelerin sayýsý
    Private gHucreSayisi As Byte = 0
    'Hücre sayýsýný verir
    Public ReadOnly Property HucreSayisi() As Byte
        Get
            HucreSayisi = gHucreSayisi
        End Get
    End Property
    'Hücreleri elde etmek için
    Public ReadOnly Property Hucreler() As Collection
        Get
            Hucreler = gHucreler
        End Get
    End Property
    'Satýra yeni hücre eklemek için
    Public Sub HucreEkle(ByVal YeniHucre As clsHucrem)
        'yeni hücre ekleniyor
        gHucreler.Add(YeniHucre, HucreSayisi + 1)
        'hücre sayýsý artýrýlýyor
        gHucreSayisi = gHucreSayisi + 1
    End Sub
End Class
'Kolon Sýnýfý
Public Class clsKolonum
    'Kolonun baþlýðý
    Private gBaslik As String = ""
    'Kolonun geniþliði (yüzde veya pixel)
    Private gGenislik As String = "100%"
    'içerik hizalama
    Private gicerikHizalama As String = "left"
    'kolon baþlýðý hizalama
    Private gBaslikHizalama As String = "center"
    'Baþlýk deðerini deðiþtirmek ya da elde etmek için
    Public Property Baslik() As String
        Get
            Baslik = gBaslik
        End Get
        Set(ByVal YeniBaslik As String)
            gBaslik = YeniBaslik
        End Set
    End Property
    'Geniþlik deðerini elde etmek ya da deðiþtirmek için
    Public Property Genislik() As String
        Get
            Genislik = gGenislik
        End Get
        Set(ByVal YeniGenislik As String)
            gGenislik = YeniGenislik
        End Set
    End Property
    'Ýçerik hizalama
    Public Property icerikHizalama() As String
        Get
            icerikHizalama = gicerikHizalama
        End Get
        Set(ByVal YeniHizalama As String)
            gicerikHizalama = YeniHizalama
        End Set
    End Property
    'Baþlýk hizalama
    Public Property BaslikHizalama() As String
        Get
            BaslikHizalama = gBaslikHizalama
        End Get
        Set(ByVal YeniHizalama As String)
            gBaslikHizalama = YeniHizalama
        End Set
    End Property
End Class
'Tablo sýnýfý
Public Class clsTablom
    ' *** DEÐÝÞKENLER ***
    'Tablonun ana baþlýðý
    Private gBaslik As String = ""
    'Tablonun Kolonlarý
    Private gKolonlar As New Collection
    'Tablonun kolon sayýsý
    Private gKolonSayisi As Byte = 0
    'Tablodaki Satýrlar
    Private gSatirlar As New Collection
    'Tablonun satýr sayýsý
    Private gSatirSayisi As Byte = 0
    'Tablonun biçimi
    Private gBicim As New clsTablomBicim
    'Tablonun Html kodu
    Private gHtmlKodu As String = ""

    ' *** ÖZELLÝKLER ***
    'Tablonun ana baþlýðýný deðiþtirmek için
    Public Property Baslik() As String
        Get
            Baslik = gBaslik
        End Get
        Set(ByVal YeniBaslik As String)
            gBaslik = YeniBaslik
        End Set
    End Property
    'Kolon sayýsýný verir
    Public ReadOnly Property KolonSayisi() As Byte
        Get
            KolonSayisi = gKolonSayisi
        End Get
    End Property
    'Satýr sayýsýný verir
    Public ReadOnly Property SatirSayisi() As Integer
        Get
            SatirSayisi = gSatirSayisi
        End Get
    End Property
    'Tablonun biçimini deðiþtir ya da ver
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

    ' *** YÖNTEMLER (METODLAR) ***
    'Tabloya yeni satýr eklemek için
    Public Sub SatirEkle(ByVal YeniSatir As clsSatirim)
        gSatirlar.Add(YeniSatir, SatirSayisi + 1)
        gSatirSayisi = gSatirSayisi + 1
    End Sub
    'Yeni kolon eklemek için
    Public Sub KolonEkle(ByVal YeniKolon As clsKolonum)
        'Yeni kolon ekleniyor
        gKolonlar.Add(YeniKolon, gKolonSayisi + 1)
        'Kolon sayýsý artýrýlýyor
        gKolonSayisi = gKolonSayisi + 1
    End Sub
    'Tablonun html kodunu üretir
    Public Sub HtmlKoduUret()
        'deðiþkenler
        Dim kod As String ' geçici kod
        Dim i As Integer ' sayaç deðiþkenleri
        Dim birSatir As clsSatirim ' geçici satýr
        Dim Arenk As String = gBicim.SatirArkaRenk1
        Dim Yrenk As String = gBicim.SatirYaziRenk1
        kod = "<table width='" & gBicim.Genislik & "' bgcolor='" & gBicim.TabloArkaRenk & "' align='" & gBicim.Hizalama & "'>"
        'Tablo baþlýðý yazýlýyor
        kod = kod & "<tr><td width='100%' colspan='" & gKolonSayisi & "' " & _
        "bgcolor='" & gBicim.BaslikArkaRenk & "' align='center'>"
        kod = kod & "<font face='" & gBicim.BaslikFont & "' style='font-size:" & gBicim.BaslikBoyut & ";color:" & gBicim.BaslikRenk & "'>"
        If gBicim.BaslikKalin Then kod = kod & "<b>"
        kod = kod & gBaslik 'baþlýk metni
        If gBicim.BaslikKalin Then kod = kod & "<b>"
        kod = kod & "</font></td></tr>" 'satýr kapanýþý
        'Kolon baþlýklarý yazýlýyor
        kod = kod & "<tr>"
        For i = 1 To gKolonlar.Count ' her kolon için
            'Kolon açýlýyor ve özellikleri yazýlýyor
            kod = kod & "<td width='" & gKolonlar(i).Genislik & "' bgcolor='" & gBicim.KolonBaslikArkaRenk & "' align='" & gKolonlar(i).BaslikHizalama & "'>"
            'font ayarlanýyor
            kod = kod & "<Font Face='" & gBicim.KolonBaslikFont & "' style='font-size:" & gBicim.KolonBaslikBoyut & ";color:" & gBicim.KolonBaslikYaziRenk & "'>"
            'kolonun baþlýðý yazýlýyor
            If gBicim.KolonBaslikKalin Then kod = kod & "<b>"
            kod = kod & gKolonlar(i).Baslik
            If gBicim.KolonBaslikKalin Then kod = kod & "</b>"
            'kolon kapanýyor
            kod = kod & "</font></td>"
        Next i
        kod = kod & "</tr>"
        'tablonun veri satýrlarý yazýlýyor
        For Each birSatir In gSatirlar ' her satýr için
            kod = kod & "<tr>"
            'Satýrdaki hücreler yazýlýyor
            For i = 1 To birSatir.Hucreler.Count ' her hücre için
                'hücre açýlýyor ve özellikleri yazýlýyor
                kod = kod & "<td bgcolor='" & Arenk & "' align='" & gKolonlar(i).icerikHizalama & "'>"
                'yazý þekli yazýlýyor
                kod = kod & "<Font Face='" & gBicim.YaziFont & "' style='font-size:" & gBicim.YaziBoyut & ";color:" & Yrenk & "'>"
                'veri yazýlýyor
                kod = kod & birSatir.Hucreler(i).Metin
                'hücre kapanýyor
                kod = kod & "</Font></td>"
            Next i
            kod = kod & "</tr>" 'satýr kapanýyor
            'satýr arkaplan renk deðiþtiriliyor
            If Arenk = gBicim.SatirArkaRenk1 Then Arenk = gBicim.SatirArkaRenk2 Else Arenk = gBicim.SatirArkaRenk1
            'satýr yazý rengi deðiþtiriliyor
            If Yrenk = gBicim.SatirYaziRenk1 Then Yrenk = gBicim.SatirYaziRenk2 Else Yrenk = gBicim.SatirYaziRenk1
        Next 'Each birSatir
        kod = kod & "</table>" ' tablo kapanýyor
        gHtmlKodu = kod ' geçici kod asýl deðiþkene aktarýlýyor
    End Sub
    'DataTable nesnesinden tablo oluþturmak için
    Public Sub VeriTablosuAl(ByVal VT As DataTable, Optional ByVal VTBaslik As Boolean = False)
        ' VTBaslik: True ise VT'deki Kolon adlarý otomatikmen kolon adý olarak alýnýr
        Dim i, j As Integer
        Dim genislik As Byte
        Dim birKolon As clsKolonum
        Dim BirHucre As clsHucrem
        Dim BirSatir As clsSatirim
        genislik = CInt(100 / (VT.Columns.Count))
        'Eðer Kolon baþlýðý yazýlmasý gerekse ise bu iþ yapýlýyor
        If VTBaslik Then
            gKolonlar = New Collection
            For i = 0 To VT.Columns.Count - 1 ' tüm kolonlar ekleniyor
                birKolon = New clsKolonum
                birKolon.Baslik = VT.Columns(i).ColumnName
                birKolon.Genislik = genislik & "%"
                KolonEkle(birKolon)
            Next
        End If
        'Þimdi veriler yazýlýyor
        For i = 0 To VT.Rows.Count - 1 ' Her satýr için
            BirSatir = New clsSatirim
            For j = 0 To VT.Columns.Count - 1 ' Her alan için
                BirHucre = New clsHucrem
                BirHucre.Metin = VT.Rows(i)(j)
                BirSatir.HucreEkle(BirHucre)
            Next
            SatirEkle(BirSatir)
        Next
    End Sub
End Class