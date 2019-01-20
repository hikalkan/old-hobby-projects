Imports System.Data.Odbc
Public Class hocalar
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAd As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSoyad As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEposta As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDogumTarihi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtYetkiDuzeyi As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnEkle As System.Web.UI.WebControls.Button
    Protected WithEvents txtKullaniciAdi As System.Web.UI.WebControls.TextBox
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents divHocaEklemeFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtUnvan As System.Web.UI.WebControls.TextBox
    Protected WithEvents divHocaAyrintisi As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblAd As System.Web.UI.WebControls.Label
    Protected WithEvents lblEposta As System.Web.UI.WebControls.Label
    Protected WithEvents lblDogumTarihi As System.Web.UI.WebControls.Label
    Protected WithEvents lblKullaniciAdi As System.Web.UI.WebControls.Label
    Protected WithEvents lblYetkiDuzeyi As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private YetkiDuzeyi As Byte = 0
    Private HocaNo As Long = 0
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OnAyarlamalar()
        If GirisYapilmisMi() Then
            'Ýstenilen sayfa adý QueryString'den alýnýyor
            Dim Sayfa As String = Request.QueryString("Sayfa")
            Select Case Sayfa
                Case "EklemeFormu"
                    EklemeFormu()
                Case "Listele"
                    Listele()
                Case "Ayrinti"
                    HocaAyrinti()
                Case Else
                    ParametreHatasi()
            End Select
        Else
            Response.Redirect("../default.aspx")
        End If
    End Sub
    'Sayfa açýldýðýnda yapýlmasýný istenen bazý ayarlamalar
    Private Sub OnAyarlamalar()
        divHocaEklemeFormu.Visible = False
        divHocaAyrintisi.Visible = False
    End Sub
    'Kullanýncý giriþi yapýlýp yapýlmadýðýna bakýlýyor
    Private Function GirisYapilmisMi() As Boolean
        Dim Sonuc As Boolean = True
        Try
            If Not SayisalMi(Session("YetkiDuzeyi"), HYD_Yonetici, HYD_Yonetici) Then Sonuc = False Else YetkiDuzeyi = CByte(Session("YetkiDuzeyi"))
            If Not SayisalMi(Session("HocaNo"), 1, 1000000000) Then Sonuc = False Else HocaNo = Session("HocaNo")
            If Session("KullaniciTuru") <> "Hoca" Then Sonuc = False
        Catch ex As Exception
            Sonuc = False
        End Try
        Return Sonuc
    End Function
    'Veritabanýna yeni bir hoca eklemek için gerekli formu gösteren prosedür
    Private Sub EklemeFormu()
        divHocaEklemeFormu.Visible = True
    End Sub
    'Hatalý Parametre giriþinde hata mesajý verir
    Private Sub ParametreHatasi()
        lblHata.Text = "<p class='Girintili'>Parametre hatasý. Lütfen adres çubuðuna el ile parametre giriþi yapmayýn.</p>"
    End Sub
    'veritabanýna hocayý ekleyen kýsým..
    Private Sub btnEkle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEkle.Click
        'Gelen verilerde hata olup olmadýðý denetleniyor
        Dim Hata As Boolean = False
        lblHata.Text = ""
        If Not BilgiKontrolu(txtUnvan.Text, 20, False) Then
            Hata = True
            lblHata.Text += "Ünvan bilgisinde uygun olmayan karakterler var.<br>"
        End If
        If Not BilgiKontrolu(txtAd.Text, 20, False) Then
            Hata = True
            lblHata.Text += "Ad bilgisinde uygun olmayan karakterler var.<br>"
        End If
        If Not BilgiKontrolu(txtSoyad.Text, 20, False) Then
            Hata = True
            lblHata.Text += "Soyad bilgisinde uygun olmayan karakterler var.<br>"
        End If
        If Not EpostaKontrolu(txtEposta.Text) Then
            Hata = True
            lblHata.Text += "E-posta adresi geçersiz.<br>"
        End If
        If Not TarihGecerliMi(txtDogumTarihi.Text) Then
            Hata = True
            lblHata.Text += "Doðum tarihi uygun biçimde girilmemiþ.<br>"
        End If
        If Not BilgiKontrolu(txtKullaniciAdi.Text, 20, False) Then
            Hata = True
            lblHata.Text += "Kullanýcý Adý bilgisinde uygun olmayan karakterler var.<br>"
        End If
        If Not SayisalMi(txtYetkiDuzeyi.Text, HYD_KendiDersi, HYD_Yonetici) Then
            Hata = True
            lblHata.Text += "Yetki düzeyi alaný " & HYD_KendiDersi & " ile " & HYD_Yonetici & " arasýnda olmalýdýr.<br>"
        End If
        If Hata Then
            lblHata.Text += "Lütfen yukarýdaki hatalarý düzeltin ve tekrar deneyin."
            Exit Sub
        End If
        'Eðer formdan gelen verilerde herhangi bir hata yoksa veritabanýna yazýlýyor
        Dim Hoca As New clsHocaBilgileri
        Hoca.Unvan = txtUnvan.Text
        Hoca.Ad = txtAd.Text
        Hoca.Soyad = txtSoyad.Text
        Hoca.Eposta = txtEposta.Text
        Hoca.DogumTarihi = Tarih2MySqlTarih(txtDogumTarihi.Text)
        Hoca.KullaniciAdi = txtKullaniciAdi.Text
        Hoca.YetkiDuzeyi = txtYetkiDuzeyi.Text
        Hoca.OnaylandiMi = 0
        'Öðrenciye rastgele bir þifre atanýyor...
        Dim Sifre As Long
        Randomize()
        Sifre = Math.Round(Rnd() * 1000000) + 1000000
        Hoca.Sifre = Sifre.ToString
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý."
            Exit Sub
        End Try
        'Ayný Kullanýcý Adýna sahip bir hoca veritabanýnda var mý diye bakýlýyor
        Dim HocaVarMi As Byte
        HocaVarMi = Hoca.VeritabanindaVarMi
        Select Case HocaVarMi
            Case 1
                lblHata.Text = "Ayný kullanýcý adýna sahip baþka bir Hoca veritabanýnda var.<br>"
                lblHata.Text += "Lütfen yukarýdaki hatalarý düzeltin ve tekrar deneyin."
            Case 2
                lblHata.Text = "Veritabanýna baðlanýlamadý."
        End Select
        If HocaVarMi = 0 Then
            If Hoca.VeriTabaninaKaydet() Then
                lblHata.Text = txtAd.Text & " " & txtSoyad.Text & " isimli Hoca veritabanýna kaydedildi."
                txtUnvan.Text = ""
                txtAd.Text = ""
                txtSoyad.Text = ""
                txtEposta.Text = ""
                txtDogumTarihi.Text = ""
                txtKullaniciAdi.Text = ""
                txtYetkiDuzeyi.Text = ""
            Else
                lblHata.Text = "Veritabanýna kaydedilemedi."
            End If
        End If
        'Baðlantý kapatýlýyor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
    'Tüm hocalarý listeler
    Private Sub Listele()
        'Veritabaný deðiþkenleri
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Dim Adaptor As New OdbcDataAdapter
        Dim Hocalar As New DataSet("Hocalar")

        Komut.Connection = Baglanti
        Adaptor.SelectCommand = Komut
        'Diðer Deðiþkenler..
        Dim BirSayfadakiKayitSayisi As Short = 50
        Dim KayitSayisi As Long = 0
        Dim SayfaSayisi As Short = 0
        Dim SayfaNo As Short = 1
        'Sayfa Numarasý alýnýyor...
        If Request.QueryString("SayfaNo") <> "" Then
            If SayisalMi(Request.QueryString("SayfaNo"), 1, 1000) Then
                SayfaNo = CInt(Request.QueryString("SayfaNo"))
            End If
        End If
        'Veritabanýna baðlantý açýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý.<br>"
            Exit Sub
        End Try
        'Veritabanýndan kayýt sayýsý alýnýyor
        Sorgu = "SELECT Count(*) As KayitSayisi FROM Hocalar"
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                KayitSayisi = Okuyucu("KayitSayisi")
            End If
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý.<br>"
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If KayitSayisi = 0 Then
            lblHata.Text = "Veritabanýnda hiç kayýt yok.<br>"
            If Not (Komut Is Nothing) Then Komut.Dispose()
            Baglanti.Close()
            Exit Sub
        End If
        'Hesaplamalar..
        SayfaSayisi = KayitSayisi / BirSayfadakiKayitSayisi
        If KayitSayisi > BirSayfadakiKayitSayisi * SayfaSayisi Then
            SayfaSayisi += 1
        End If
        If SayfaNo > SayfaSayisi Then SayfaNo = 1
        'Veritabanýndan kayýtlar alýnýyor
        Sorgu = "SELECT HocaNo,Unvan,Ad,Soyad,Eposta,YetkiDuzeyi,Sifre,OnaylandiMi FROM Hocalar ORDER BY Ad"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Hocalar, (SayfaNo - 1) * BirSayfadakiKayitSayisi, BirSayfadakiKayitSayisi, "Hocalar")
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý.<br>"
            If Not (Komut Is Nothing) Then Komut.Dispose()
            Baglanti.Close()
            Exit Sub
        End Try
        'kayýtlarý göstermek için html tablosu oluþturuluyor...
        Dim Hoca As DataRow
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
        'Tablo baþlýðý ve biçim atamasý
        Tablo.Bicim = Bicim
        Tablo.Baslik = "H O C A L A R"
        'Kolonlar oluþturuluyor
        Kolon = New clsKolonum
        Kolon.Baslik = "Adý Soyadý"
        Kolon.Genislik = "40%"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "E-posta"
        Kolon.Genislik = "45%"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Yetki"
        Kolon.Genislik = "5%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Þifre"
        Kolon.Genislik = "10%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        'Satýrlar oluþturuluyor
        For Each Hoca In Hocalar.Tables(0).Rows
            Satir = New clsSatirim
            Hucre = New clsHucrem
            Hucre.Metin = "<a href='hocalar.aspx?Sayfa=Ayrinti&HocaNo=" & Hoca("HocaNo") & "'>" & Hoca("Unvan") & " " & Hoca("Ad") & " " & Hoca("Soyad") & "</a>"
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = Hoca("Eposta")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = Hoca("YetkiDuzeyi")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            If Hoca("OnaylandiMi") = 0 Then
                Hucre.Metin = Hoca("Sifre")
            Else
                Hucre.Metin = "??"
            End If
            Satir.HucreEkle(Hucre)
            Tablo.SatirEkle(Satir)
        Next
        Tablo.HtmlKoduUret()
        phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
        'Alt taraftaki diðer sayfalarý gösterme baðlantýlarý oluþturuluyor
        Dim kod As String = "<p class='DuzYazi' align='Center'>"
        If SayfaSayisi > 1 Then
            Dim i As Short
            If SayfaNo > 1 Then
                kod += "&lt&lt <a href='hocalar.aspx?Sayfa=Listele&SayfaNo=" & SayfaNo - 1 & "'>Önceki</a>] "
            End If
            For i = 1 To SayfaSayisi
                If i = SayfaNo Then
                    kod += "[" & i & "] "
                Else
                    kod += "[<a href='hocalar.aspx?Sayfa=Listele&SayfaNo=" & i & "'>" & i & "</a>] "
                End If
            Next
            If SayfaNo < SayfaSayisi Then
                kod += "[<a href='hocalar.aspx?Sayfa=Listele&SayfaNo=" & SayfaNo + 1 & "'>Sonraki</a> &gt&gt"
            End If
        End If
        kod += "</p>"
        phOrta.Controls.Add(New LiteralControl(kod))
        'Veritabanýna baðlantý kapanýyor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
    'Herhangi bir hocanýn ayrýntýsýný gösterir
    Private Sub HocaAyrinti()
        Dim HocaNo As Long = 0
        If Request.QueryString("HocaNo") <> "" Then
            If SayisalMi(Request.QueryString("HocaNo"), 1, 1000000) Then
                HocaNo = CInt(Request.QueryString("HocaNo"))
            End If
        End If
        'HocaNo parametresi hatalý girilmiþse hata mesajý ver
        If HocaNo = 0 Then
            lblHata.Text = "Hata! Adres çubuðuna el ile parametre girmeyin."
            Exit Sub
        Else
            divHocaAyrintisi.Visible = True
        End If
        'Veritabanýna baðlan
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý."
            Exit Sub
        End Try
        'Hocanýn bilgilerini veritabanýndan al
        Dim Hoca As New clsHocaBilgileri
        Hoca.HocaNo = HocaNo
        If Hoca.VeriTabanindanAl() Then ' Hoca Bilgilerini yaz..
            'Kiþisel bilgileri..
            lblAd.Text = Hoca.Unvan & " " & Hoca.Ad & " " & Hoca.Soyad
            lblEposta.Text = Hoca.Eposta
            lblDogumTarihi.Text = MySqlTarih2Tarih(Hoca.DogumTarihi)
            lblKullaniciAdi.Text = Hoca.KullaniciAdi
            lblYetkiDuzeyi.Text = Hoca.YetkiDuzeyi & " (" & YetkiSayi2Yazi(Hoca.YetkiDuzeyi) & ")"
            'Aldýðý derslerin listesi veritabanýndan alýnýyor
            Dim Dersler As New DataTable
            Dim Ders As DataRow
            Dim Sorgu As String = "SELECT * FROM Dersler WHERE HocaNo=" & Hoca.HocaNo & " ORDER BY Yil DESC, Sinif ASC, Donem ASC, Ad ASC"
            Dim Komut As New OdbcCommand(Sorgu, Baglanti)
            Dim Adaptor As New OdbcDataAdapter(Komut)
            Try
                Adaptor.Fill(Dersler)
            Catch ex As Exception
                lblHata.Text = "Dersleri almak için veritabanýna baðlanýlamadý."
            End Try
            If Dersler.Rows.Count > 0 Then
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
                Tablo.Baslik = "Verdiði Dersler"
                'Kolonlar oluþturuluyor
                Kolon = New clsKolonum
                Kolon.Baslik = "Ders Adý"
                Kolon.Genislik = "70%"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Yýl"
                Kolon.Genislik = "10%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Sýnýf"
                Kolon.Genislik = "10%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Dönem"
                Kolon.Genislik = "10%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                'Satýrlar oluþturuluyor
                For Each Ders In Dersler.Rows
                    Satir = New clsSatirim
                    Hucre = New clsHucrem
                    Hucre.Metin = "<a href='dersler.aspx?Sayfa=DersBilgileri&DersNo=" & Ders("DersNo") & "'>" & Ders("Ad") & "</a>"
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = Ders("Yil")
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = Ders("Sinif")
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = Ders("Donem")
                    Satir.HucreEkle(Hucre)
                    Tablo.SatirEkle(Satir)
                Next
                Tablo.HtmlKoduUret()
                phOrta.Controls.Add(New LiteralControl("<p></p>"))
                phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
            Else 'yani bu hocanýn üzerinde hiç ders yoksa
                phOrta.Controls.Add(New LiteralControl("<p class='DuzYazi' align='Center'>Bu Hocanýn üzerinde hiç ders yok.</p>"))
            End If
        End If
        'baðlantýyý kapat
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
End Class
