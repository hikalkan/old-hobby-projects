Imports System.Data.Odbc
Public Class ogrenci
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAd As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSoyad As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEposta As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnEkle As System.Web.UI.WebControls.Button
    Protected WithEvents txtNumara As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDogumTarihi As System.Web.UI.WebControls.TextBox
    Protected WithEvents divOgrenciEklemeFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents cblOgrenciler As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents btnTamam As System.Web.UI.WebControls.Button

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
                    If Request.QueryString("Mezun") = 1 Then
                        Listele(True)
                    Else
                        Listele()
                    End If
                Case "OgrenciSil"
                    OgrenciSil()
                Case "Mezuniyet"
                    Mezuniyet()
                Case Else
                    ParametreHatasi()
            End Select
        Else
            Response.Redirect("../default.aspx")
        End If
    End Sub
    'Sayfa açýldýðýnda yapýlmasýný istenen bazý ayarlamalar
    Private Sub OnAyarlamalar()
        divOgrenciEklemeFormu.Visible = False
    End Sub
    'Kullanýncý giriþi yapýlýp yapýlmadýðýna bakýlýyor
    Private Function GirisYapilmisMi() As Boolean
        Dim Sonuc As Boolean = True
        Try
            If Not SayisalMi(Session("YetkiDuzeyi"), HYD_Editor, HYD_Yonetici) Then Sonuc = False Else YetkiDuzeyi = CByte(Session("YetkiDuzeyi"))
            If Not SayisalMi(Session("HocaNo"), 1, 1000000000) Then Sonuc = False Else HocaNo = Session("HocaNo")
            If Session("KullaniciTuru") <> "Hoca" Then Sonuc = False
        Catch ex As Exception
            Sonuc = False
        End Try
        Return Sonuc
    End Function
    'Veritabanýna yeni bir öðrenci eklemek için gerekli formu gösteren prosedür
    Private Sub EklemeFormu()
        divOgrenciEklemeFormu.Visible = True
    End Sub
    'Hatalý Parametre giriþinde hata mesajý verir
    Private Sub ParametreHatasi()
        lblHata.Text = "<p class='Girintili'>Parametre hatasý. Lütfen adres çubuðuna el ile parametre giriþi yapmayýn.</p>"
    End Sub
    'Formdan Gelen Verileri Veritabanýna Yaz
    Private Sub btnEkle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEkle.Click
        'Gelen verilerde hata olup olmadýðý denetleniyor
        Dim Hata As Boolean = False
        lblHata.Text = ""
        If Not BilgiKontrolu(txtAd.Text, 20, False) Then
            Hata = True
            lblHata.Text += "Ad bilgisinde uygun olmayan karakterler var.<br>"
        End If
        If Not BilgiKontrolu(txtSoyad.Text, 20, False) Then
            Hata = True
            lblHata.Text += "Soyad bilgisinde uygun olmayan karakterler var.<br>"
        End If
        If Not BilgiKontrolu(txtNumara.Text, 20, False) Then
            Hata = True
            lblHata.Text += "Numara bilgisinde uygun olmayan karakterler var.<br>"
        End If
        If Not EpostaKontrolu(txtEposta.Text) Then
            Hata = True
            lblHata.Text += "E-posta adresi geçersiz.<br>"
        End If
        If Not TarihGecerliMi(txtDogumTarihi.Text) Then
            Hata = True
            lblHata.Text += "Doðum tarihi uygun biçimde girilmemiþ.<br>"
        End If
        If Hata Then
            lblHata.Text += "Lütfen yukarýdaki hatalarý düzeltin ve tekrar deneyin."
            Exit Sub
        End If
        'Eðer formdan gelen verilerde herhangi bir hata yoksa veritabanýna yazýlýyor
        Dim Ogrenci As New clsOgrenciBilgileri
        Ogrenci.Ad = txtAd.Text
        Ogrenci.Soyad = txtSoyad.Text
        Ogrenci.Numara = txtNumara.Text
        Ogrenci.DogumTarihi = Tarih2MySqlTarih(txtDogumTarihi.Text)
        Ogrenci.Eposta = txtEposta.Text
        Ogrenci.OnaylandiMi = 0
        'Öðrenciye rastgele bir þifre atanýyor...
        Dim Sifre As Long
        Randomize()
        Sifre = Math.Round(Rnd() * 1000000) + 1000000
        Ogrenci.Sifre = Sifre.ToString
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý."
            Exit Sub
        End Try
        'Ayný numaralý bir öðrenci veritabanýnda var mý diye bakýlýyor
        Dim OgrVarMi As Byte
        OgrVarMi = Ogrenci.VeritabanindaVarMi
        Select Case OgrVarMi
            Case 1
                lblHata.Text = "Ayný numaralý baþka bir öðrenci veritabanýnda var.<br>"
                lblHata.Text += "Lütfen yukarýdaki hatalarý düzeltin ve tekrar deneyin."
            Case 2
                lblHata.Text = "Veritabanýna baðlanýlamadý."
        End Select
        If OgrVarMi = 0 Then
            If Ogrenci.VeriTabaninaKaydet() Then
                lblHata.Text = txtNumara.Text & " Numaralý öðrenci veritabanýna kaydedildi."
                txtAd.Text = ""
                txtSoyad.Text = ""
                txtNumara.Text = ""
                txtDogumTarihi.Text = ""
                txtEposta.Text = ""
            Else
                lblHata.Text = "Veritabanýna kaydedilemedi."
            End If
        End If
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
    'Tüm öðrencileri listele
    Private Sub Listele(Optional ByVal Mezun As Boolean = False)
        'Veritabaný deðiþkenleri
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Dim Adaptor As New OdbcDataAdapter
        Dim Ogrenciler As New DataSet("Ogrenciler")
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
        If Mezun Then
            Sorgu = "SELECT Count(*) As KayitSayisi FROM Ogrenciler WHERE MezunOlduMu=1"
        Else
            Sorgu = "SELECT Count(*) As KayitSayisi FROM Ogrenciler WHERE MezunOlduMu=0"
        End If
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
        If Mezun Then
            Sorgu = "SELECT OgrenciNo,Ad,Soyad,Numara,Eposta,DogumTarihi,Sifre,OnaylandiMi FROM Ogrenciler WHERE MezunOlduMu=1 ORDER BY Numara"
        Else
            Sorgu = "SELECT OgrenciNo,Ad,Soyad,Numara,Eposta,DogumTarihi,Sifre,OnaylandiMi FROM Ogrenciler WHERE MezunOlduMu=0 ORDER BY Numara"
        End If
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Ogrenciler, (SayfaNo - 1) * BirSayfadakiKayitSayisi, BirSayfadakiKayitSayisi, "Ogrenciler")
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý.<br>"
            If Not (Komut Is Nothing) Then Komut.Dispose()
            Baglanti.Close()
            Exit Sub
        End Try
        'kayýtlarý göstermek için html tablosu oluþturuluyor...
        Dim Ogrenci As DataRow
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
        If Mezun Then
            Tablo.Baslik = "M E Z U N &nbsp Ö Ð R E N C Ý L E R"
        Else
            Tablo.Baslik = "Ö Ð R E N C Ý L E R"
        End If
        'Kolonlar oluþturuluyor
        Kolon = New clsKolonum
        Kolon.Baslik = "Sýra"
        Kolon.Genislik = "5%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Numara"
        Kolon.Genislik = "15%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Adý Soyadý"
        Kolon.Genislik = "25%"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Doðum Tarihi"
        Kolon.Genislik = "15%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "E-posta"
        Kolon.Genislik = "25%"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Þifre"
        Kolon.Genislik = "10%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = ""
        Kolon.Genislik = "5%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        'Satýrlar oluþturuluyor
        Dim SatirNo As Byte = (SayfaNo - 1) * BirSayfadakiKayitSayisi
        For Each Ogrenci In Ogrenciler.Tables(0).Rows
            SatirNo += 1
            Satir = New clsSatirim
            Hucre = New clsHucrem
            Hucre.Metin = SatirNo.ToString
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = Ogrenci("Numara")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = Ogrenci("Ad") & " " & Ogrenci("Soyad")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = TarihTurkceFormat(Ogrenci("DogumTarihi"))
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = Ogrenci("Eposta")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            If Ogrenci("OnaylandiMi") = 0 Then
                Hucre.Metin = Ogrenci("Sifre")
            Else
                Hucre.Metin = "???"
            End If
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = "<a href='ogrenci.aspx?Sayfa=OgrenciSil&OgrenciNo=" & Ogrenci("OgrenciNo") & "&SayfaNo=" & SayfaNo & "'>sil</a>"
            Satir.HucreEkle(Hucre)
            Tablo.SatirEkle(Satir)
        Next
        Tablo.HtmlKoduUret()
        phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
        'Alt taraftaki diðer sayfalarý gösterme baðlantýlarý oluþturuluyor
        Dim MezunOlduMu As Byte
        If Mezun Then MezunOlduMu = 1 Else MezunOlduMu = 0
        Dim kod As String = "<p class='DuzYazi' align='Center'>"
        If SayfaSayisi > 1 Then
            Dim i As Short
            If SayfaNo > 1 Then
                kod += "&lt&lt <a href='ogrenci.aspx?Sayfa=Listele&Mezun=" & MezunOlduMu & "&SayfaNo=" & SayfaNo - 1 & "'>Önceki</a>] "
            End If
            For i = 1 To SayfaSayisi
                If i = SayfaNo Then
                    kod += "[" & i & "] "
                Else
                    kod += "[<a href='ogrenci.aspx?Sayfa=Listele&Mezun=" & MezunOlduMu & "&SayfaNo=" & i & "'>" & i & "</a>] "
                End If
            Next
            If SayfaNo < SayfaSayisi Then
                kod += "[<a href='ogrenci.aspx?Sayfa=Listele&Mezun=" & MezunOlduMu & "&SayfaNo=" & SayfaNo + 1 & "'>Sonraki</a> &gt&gt"
            End If
        End If
        kod += "</p>"
        phOrta.Controls.Add(New LiteralControl(kod))
        If Mezun Then
            kod = "<p class='DuzYazi' align='Center'>[<a href='ogrenci.aspx?Sayfa=Listele&SayfaNo=1&Mezun=0'>Þu anda öðrenimine devam eden öðrencileri göster</a>]</p>"
        Else
            kod = "<p class='DuzYazi' align='Center'>[<a href='ogrenci.aspx?Sayfa=Listele&SayfaNo=1&Mezun=1'>Mezun olmuþ öðrencileri göster</a>]</p>"
        End If
        phOrta.Controls.Add(New LiteralControl(kod))
        'Veritabanýna baðlantý kapanýyor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
    'Bir öðrenciyi veritabanýndan silmek için
    Private Sub OgrenciSil()
        If Request.QueryString("OgrenciNo") <> "" And Request.QueryString("SayfaNo") <> "" Then
            If SayisalMi(Request.QueryString("OgrenciNo"), 1, 1000000) And SayisalMi(Request.QueryString("SayfaNo"), 1, 1000) Then
                Dim Ogrenci As New clsOgrenciBilgileri
                Ogrenci.OgrenciNo = CInt(Request.QueryString("OgrenciNo"))
                Try
                    Baglanti.Open()
                Catch ex As Exception
                    'boþ
                End Try
                Ogrenci.VeriTabanindanKayitSil()
                Try
                    Baglanti.Close()
                Catch ex As Exception
                    'boþ
                End Try
                Response.Redirect("ogrenci.aspx?Sayfa=Listele&SayfaNo=" & Request.QueryString("SayfaNo"))
            End If
        End If
    End Sub
    'Mezun olan öðrencilerin durumunu ayarlamak için
    Private Sub Mezuniyet()
        'Veritabaný nesneleri
        Dim Sorgu As String
        Sorgu = "SELECT OgrenciNo,CONCAT(Numara,' ',Ad,' ',Soyad) AS OgrenciAd FROM Ogrenciler WHERE MezunOlduMu=0 ORDER BY Numara"
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim tblOgrenciler As New DataTable
        Dim drOgrenci As DataRow
        'Baðlantý açýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamýyor.<br>"
            Exit Sub
        End Try
        'Veritabanýndan öðrenci bilgileri alýnýyor
        Try
            Adaptor.Fill(tblOgrenciler)
        Catch ex As Exception
            lblHata.Text = "Veritabanýndan öðrenci bilgileri alýnamadý."
            Baglantiyi_Kapat()
            Exit Sub
        End Try
        'Mesaj yazdýrýlýyor
        phOrta.Controls.Add(New LiteralControl("<p class='Girintili'>Mezun olan öðrencileri iþaretleyip tamam butonuna basýnýz.</p>"))
        'Öðrenciler cblOgrenciler listesine ekleniyor
        cblOgrenciler.Visible = True
        Dim liOgrenci As ListItem
        For Each drOgrenci In tblOgrenciler.Rows
            liOgrenci = New ListItem
            liOgrenci.Value = drOgrenci("OgrenciNo")
            liOgrenci.Text = drOgrenci("OgrenciAd")
            cblOgrenciler.Items.Add(liOgrenci)
        Next
        'tamam butonu gösteriliyor
        btnTamam.Visible = True
        'Baðlantý kapanýyor
        Baglantiyi_Kapat()
    End Sub
    'Baðlantýyý kapatmak için
    Private Sub Baglantiyi_Kapat()
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
    'Ýþaretlenen öðrencileri mezun etmek için prosedür
    Private Sub btnTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTamam.Click
        'Veritabaný nesneleri
        Dim Sorgu As String
        Dim Komut As New OdbcCommand("", Baglanti)
        'Baðlantý açýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamýyor.<br>"
            Exit Sub
        End Try
        'Listede iþaretli olanlar veritabanýnda Mezun olarak ayarlanýyor
        Dim liOgrenci As ListItem
        Sorgu = "UPDATE Ogrenciler SET MezunOlduMu=1 WHERE OgrenciNo="
        For Each liOgrenci In cblOgrenciler.Items
            If liOgrenci.Selected Then
                Komut.CommandText = Sorgu & liOgrenci.Value
                Komut.ExecuteNonQuery()
            End If
        Next
        'Baðlantý kapanýyor
        Baglantiyi_Kapat()
        'Yönetici sayfasýna yönlendiriliyor
        Response.Redirect("default.aspx")
    End Sub
End Class
