Imports System.Data.Odbc
Public Class _default1
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblAd As System.Web.UI.WebControls.Label
    Protected WithEvents lblEposta As System.Web.UI.WebControls.Label
    Protected WithEvents lblDogumTarihi As System.Web.UI.WebControls.Label
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblNumara As System.Web.UI.WebControls.Label
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents lblSifreDegistir As System.Web.UI.WebControls.Label
    Protected WithEvents divAnaSayfa As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents divSifreDegistir As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtSifreE As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSifreY As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSifreYT As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSifreDegistirTamam As System.Web.UI.WebControls.Button
    Protected WithEvents lblGeri As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OnAyarlamalar()
        Dim OgrenciNo As Long
        If SayisalMi(Session("OgrenciNo"), 1, 1000000000) Then
            Dim Sayfa As String = Request.QueryString("Sayfa")
            OgrenciNo = CInt(Session("OgrenciNo"))
            Select Case Sayfa
                Case "SifreDegistir"
                    divSifreDegistir.Visible = True
                    lblGeri.Text = "[<a href='default.aspx'>Geri</a>]"
                Case Else
                    If Request.QueryString("TumDersler") = 1 Then
                        OgrenciBilgileriGoster(OgrenciNo, True)
                    Else
                        OgrenciBilgileriGoster(OgrenciNo)
                    End If
            End Select
        Else
            Response.Redirect("../default.aspx")
        End If
    End Sub
    Private Sub OnAyarlamalar()
        lblHata.Text = ""
        divAnaSayfa.Visible = False
        divSifreDegistir.Visible = False
    End Sub
    Private Sub OgrenciBilgileriGoster(ByVal OgrenciNo As Long, Optional ByVal TumDersler As Boolean = False)
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        If SayisalMi(Session("OgretimYili"), 2000, 3000) Then
            Yil = CShort(Session("OgretimYili"))
        End If
        If SayisalMi(Session("Donem"), 1, 3) Then
            Donem = CByte(Session("Donem"))
        End If
        If OgrenciNo = 0 Or Yil = 0 Or Donem = 0 Then
            Response.Redirect("../default.aspx")
        End If
        Dim Ogrenci As New clsOgrenciBilgileri
        Ogrenci.OgrenciNo = OgrenciNo
        'baðlantý açýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý!"
            Exit Sub
        End Try
        'Öðrenci bilgileri veritabanýndan alýnýyor
        If Not Ogrenci.VeriTabanindanAl Then 'Eðer öðrenci bulunamadýysa..
            Try
                Baglanti.Close()
            Catch ex As Exception
                'boþ
            End Try
            Response.Redirect("../default.aspx")
        End If
        divAnaSayfa.Visible = True
        'Öðrenci bulunduysa kiþisel bilgilerini göster
        lblAd.Text = Ogrenci.Ad & " " & Ogrenci.Soyad
        lblNumara.Text = Ogrenci.Numara
        lblDogumTarihi.Text = TarihTurkceFormat(Ogrenci.DogumTarihi)
        lblEposta.Text = Ogrenci.Eposta
        'Öðrencinin aldýðý derslerin listesini göster
        Dim Sorgu As String
        If TumDersler Then
            Sorgu = "SELECT Dersler.DersNo,Dersler.Ad AS DersAd, CONCAT(Hocalar.Unvan,' ',Hocalar.Ad,' ',Hocalar.Soyad) As HocaAd, DersOgrenciIliskileri.AnketDoldurulduMu," & _
            "DersOgrenciIliskileri.VizeNotu, DersOgrenciIliskileri.FinalNotu,Dersler.Yil,Dersler.Donem " & _
            "FROM DersOgrenciIliskileri, Dersler, Hocalar " & _
            "WHERE DersOgrenciIliskileri.DersNo=Dersler.DersNo AND Dersler.HocaNo=Hocalar.HocaNo AND " & _
            "DersOgrenciIliskileri.OgrenciNo=" & Ogrenci.OgrenciNo & _
            " ORDER BY Dersler.Yil DESC, Dersler.Donem DESC, DersAd ASC"
        Else
            Sorgu = "SELECT Dersler.DersNo,Dersler.Ad AS DersAd, CONCAT(Hocalar.Unvan,' ',Hocalar.Ad,' ',Hocalar.Soyad) As HocaAd, DersOgrenciIliskileri.AnketDoldurulduMu," & _
            "DersOgrenciIliskileri.VizeNotu, DersOgrenciIliskileri.FinalNotu,Dersler.Yil,Dersler.Donem " & _
            "FROM DersOgrenciIliskileri, Dersler, Hocalar " & _
            "WHERE DersOgrenciIliskileri.DersNo=Dersler.DersNo AND Dersler.HocaNo=Hocalar.HocaNo AND " & _
            "DersOgrenciIliskileri.OgrenciNo=" & Ogrenci.OgrenciNo & _
            " AND Dersler.Yil=" & CShort(Session("OgretimYili")) & " AND Dersler.Donem=" & CByte(Session("Donem")) & _
            " ORDER BY DersAd ASC"
        End If
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim tblDersler As New DataTable
        Dim drDers As DataRow
        Try
            Adaptor.Fill(tblDersler)
        Catch ex As Exception
            lblHata.Text = "Veritabanýndan ders bilgileri alýnamadý.<br>"
        End Try
        Dim TabloKodu As String
        'Eðer genel anketi doldurmadýysa bunu belirten bir mesaj göster
        If Session("AnketAktif") = 1 Then
            Dim GenelAnketDolu As Boolean = True
            Sorgu = "SELECT GADNo FROM GenelAnketDolduranlar " & _
            "WHERE OgrenciNo=" & OgrenciNo & _
            " AND Yil=" & Yil & " AND Donem=" & Donem
            Komut.CommandText = Sorgu
            Try
                Okuyucu = Komut.ExecuteReader
                If Okuyucu.Read Then
                    GenelAnketDolu = True
                Else
                    GenelAnketDolu = False
                End If
            Catch ex As Exception
                'boþ
            End Try
            If Not GenelAnketDolu Then
                TabloKodu = "<p></p>" & _
                "<Table Width='90%' align='center' cellpadding='1' cellspacing='1' bgcolor='#000000'>" & _
                "<tr><td align='center' bgcolor='#DDDDDD' width='100%' class='DuzYazi'><a href='anket.aspx?Sayfa=AnketDoldur&Tur=Genel'>" & _
                "- GENEL ANKETÝ DOLDURMAK ÝÇÝN TIKLAYIN -" & _
                "</a></td></tr>" & _
                "</Table><p></p>"
                phOrta.Controls.Add(New LiteralControl(TabloKodu))
            End If
        End If
        If tblDersler.Rows.Count > 0 Then
            'Öðrenci bilgileri gösteriliyor
            Dim Tablo As New clsTablom
            Dim Bicim As New clsTablomBicim
            Dim Kolon As clsKolonum
            Dim Satir As clsSatirim
            Dim Hucre As clsHucrem
            'Biçim özellikleri
            Bicim.TabloArkaRenk = "#000000"
            Bicim.BaslikArkaRenk = "#294963"
            Bicim.BaslikBoyut = "12px"
            Bicim.BaslikFont = "Verdana"
            Bicim.BaslikRenk = "#FFFFFF"
            Bicim.KolonBaslikArkaRenk = "#919CB6"
            Bicim.KolonBaslikYaziRenk = "#FFFFFF"
            Bicim.SatirYaziRenk1 = "#000000"
            Bicim.SatirYaziRenk2 = "#000000"
            Bicim.Genislik = "90%"
            'Tablo baþlýðý ve biçim atamasý
            Tablo.Bicim = Bicim
            Tablo.Baslik = ":: Kayýtlý Olduðunuz Dersler ::"
            'Kolonlar oluþturuluyor
            Kolon = New clsKolonum
            Kolon.Baslik = "Yýl"
            Kolon.Genislik = "5%"
            Kolon.icerikHizalama = "Center"
            Tablo.KolonEkle(Kolon)
            Kolon = New clsKolonum
            Kolon.Baslik = "Dönem"
            Kolon.Genislik = "5%"
            Kolon.icerikHizalama = "Center"
            Tablo.KolonEkle(Kolon)
            Kolon = New clsKolonum
            Kolon.Baslik = "Dersin Adý"
            Kolon.Genislik = "30%"
            Tablo.KolonEkle(Kolon)
            Kolon = New clsKolonum
            Kolon.Baslik = "Hocanýn Adý"
            Kolon.Genislik = "30%"
            Tablo.KolonEkle(Kolon)
            Kolon = New clsKolonum
            Kolon.Baslik = "Vize"
            Kolon.Genislik = "5%"
            Kolon.icerikHizalama = "center"
            Tablo.KolonEkle(Kolon)
            Kolon = New clsKolonum
            Kolon.Baslik = "Final"
            Kolon.Genislik = "5%"
            Kolon.icerikHizalama = "center"
            Tablo.KolonEkle(Kolon)
            Kolon = New clsKolonum
            Kolon.Baslik = ""
            Kolon.Genislik = "20%"
            Kolon.icerikHizalama = "center"
            Tablo.KolonEkle(Kolon)
            'Satýrlar oluþturuluyor
            For Each drDers In tblDersler.Rows
                Satir = New clsSatirim
                'Yýl
                Hucre = New clsHucrem
                Hucre.Metin = drDers("Yil")
                Satir.HucreEkle(Hucre)
                'Dönem
                Hucre = New clsHucrem
                Hucre.Metin = DonemMetinKisa(drDers("Donem"))
                Satir.HucreEkle(Hucre)
                'Dersin Adý
                Hucre = New clsHucrem
                Hucre.Metin = drDers("DersAd")
                Satir.HucreEkle(Hucre)
                'Hocanýn Adý
                Hucre = New clsHucrem
                Hucre.Metin = drDers("HocaAd")
                Satir.HucreEkle(Hucre)
                If (drDers("AnketDoldurulduMu") = 1) _
                Or (drDers("Donem") <> CByte(Session("Donem"))) _
                Or (drDers("Yil") <> CShort(Session("OgretimYili"))) _
                Or (CByte(Session("AnketAktif")) <> 1) Then 'Anketi doldurduysa ya da doldurulamazsa
                    Hucre = New clsHucrem
                    If CByte(drDers("VizeNotu")) < 101 Then 'Vize notu açýklandýysa..
                        Hucre.Metin = drDers("VizeNotu")
                    Else 'Vize Notu Açýklanmadýysa
                        Hucre.Metin = "?"
                    End If
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    If CByte(drDers("FinalNotu")) < 101 Then 'Final notu açýklandýysa..
                        Hucre.Metin = drDers("FinalNotu")
                    Else 'Final Notu Açýklanmadýysa
                        Hucre.Metin = "?"
                    End If
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    If Session("AnketAktif") <> 1 Then
                        Hucre.Metin = "<p Style='color:#999999'>Anket Kapalý</p>"
                    ElseIf drDers("AnketDoldurulduMu") = 1 Then
                        Hucre.Metin = "<p Style='color:#999999'>Anketi Doldurdunuz</p>"
                    Else
                        Hucre.Metin = "<p Style='color:#999999'>Anket Kapalý</p>"
                    End If
                    Satir.HucreEkle(Hucre)
                Else 'Anketi doldurmadýysa..
                    Hucre = New clsHucrem
                    Hucre.Metin = "?"
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = "?"
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = "<a href='anket.aspx?Sayfa=AnketDoldur&DersNo=" & drDers("DersNo") & "'>Anketi Doldur</a>"
                    Satir.HucreEkle(Hucre)
                End If 'drDers("AnketDoldurulduMu") = 1
                Tablo.SatirEkle(Satir)
            Next 'Each drDers
            Tablo.HtmlKoduUret()
            phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
        Else
            lblHata.Text = "Þu anda hiçbir derse kayýtlý gözükmüyorsunuz.<br>"
        End If
        If Not TumDersler Then
            TabloKodu = "<p></p>" & _
            "<Table Width='90%' align='center' cellpadding='1' cellspacing='1' bgcolor='#000000'>" & _
            "<tr><td align='center' bgcolor='#DDDDDD' width='100%' class='DuzYazi'><a href='default.aspx?TumDersler=1'>" & _
            "- TÜM DERSLERÝ GÖRMEK ÝÇÝN TIKLAYIN -" & _
            "</a></td></tr>" & _
            "</Table><p></p>"
        Else
            TabloKodu = "<p></p>" & _
            "<Table Width='90%' align='center' cellpadding='1' cellspacing='1' bgcolor='#000000'>" & _
            "<tr><td align='center' bgcolor='#DDDDDD' width='100%' class='DuzYazi'><a href='default.aspx'>" & _
            "- SADECE BU DÖNEMKÝ DERSLERÝ GÖRMEK ÝÇÝN TIKLAYIN -" & _
            "</a></td></tr>" & _
            "</Table><p></p>"
        End If
        phOrta.Controls.Add(New LiteralControl(TabloKodu))
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
    Private Sub btnSifreDegistirTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSifreDegistirTamam.Click
        lblHata.Text = ""
        'Formdan gelen verilerin geçerliliði kontrol ediliyor
        If (Not BilgiKontrolu(txtSifreE.Text, 20, False)) _
        Or (Not BilgiKontrolu(txtSifreY.Text, 20, False)) _
        Or (Not BilgiKontrolu(txtSifreYT.Text, 20, False)) Then
            lblHata.Text = "Girdiðiniz bilgilerde uygun olmayan karekterler var."
            Exit Sub
        End If
        Dim OgrenciNo As Long = 0
        If Not SayisalMi(Session("OgrenciNo"), 1, 1000000000) Then
            Response.Redirect("../default.aspx")
        End If
        OgrenciNo = CInt(Session("OgrenciNo"))
        Dim Ogrenci As New clsOgrenciBilgileri
        Ogrenci.OgrenciNo = OgrenciNo
        'Baðlantýyý aç
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamýyor."
            Exit Sub
        End Try
        'Öðrenci bilgilerini veritabanýndan al
        If Not Ogrenci.VeriTabanindanAl() Then
            lblHata.Text = "Veritabanýnda bulunamadý."
            Try
                Baglanti.Close()
            Catch ex As Exception
                'boþ
            End Try
            Exit Sub
        End If
        'Eski þifreyi karþýlaþtýr, yanlýþsa çýk
        Dim Hata As Boolean = False
        If Ogrenci.Sifre <> txtSifreE.Text Then
            lblHata.Text += "Eski þifrenizi hatalý girdiniz.<br>"
            Hata = True
        End If
        If txtSifreY.Text <> txtSifreYT.Text Then
            lblHata.Text += "Þifre tekrarý üstteki ile ayný deðil.<br>"
            Hata = True
        End If
        If Hata Then
            Try
                Baglanti.Close()
            Catch ex As Exception
                'boþ
            End Try
            Exit Sub
        End If
        'Þifreyi deðiþtir
        Ogrenci.Sifre = txtSifreY.Text.Trim
        If Not Ogrenci.SifeGuncelle() Then
            Hata = True
        End If
        'baðlantýyý kapat
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
        If Not Hata Then
            Response.Redirect("default.aspx")
        Else
            lblHata.Text += "Þifreniz deðiþtirilemedi."
        End If
    End Sub
End Class
