Public Class Uyelik
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAd As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSoyad As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEposta As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWebSite As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLakap As System.Web.UI.WebControls.TextBox
    Protected WithEvents UyelikFormu As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents ddlGun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAy As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYil As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtSifre As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnTamam As System.Web.UI.WebControls.Button
    Protected WithEvents txtSifreTekrar As System.Web.UI.WebControls.TextBox
    Protected WithEvents divUyelikFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents phHata As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents divUyeGirisi As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtKullaniciAdi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUGKullaniciAdi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtUGSifre As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnUGGiris As System.Web.UI.WebControls.Button
    Protected WithEvents phUyeBilgisi As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents divUyeBilgisi As System.Web.UI.HtmlControls.HtmlGenericControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim Sayfa As String = ""
    Dim baglanti As New Odbc.OdbcConnection("Driver={MySQL ODBC 3.51 Driver};Server=127.0.0.1;Database=bilmuh;uid=hikalkan;pwd=himysql;option=35")
    Dim sorgu As String = ""
    Dim komut As New Odbc.OdbcCommand(sorgu, baglanti)
    Dim okuyucu As Odbc.OdbcDataReader
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        divUyelikFormu.Visible = False
        divUyeGirisi.Visible = False
        divUyeBilgisi.Visible = False
        If Request.QueryString("Sayfa") <> "" Then
            Sayfa = Request.QueryString("Sayfa")
            Select Case Sayfa
                Case "UyelikFormu"
                    UyelikFormunuAyarla()
                    divUyelikFormu.Visible = True
                Case "UyeGirisi"
                    UyeGirisi()
                Case "UyeGoster"
                    If SayisalMi(Request.QueryString("UyeNo"), 1, 1000000000) Then
                        UyeGoster(CInt(Request.QueryString("UyeNo")))
                    Else
                        Response.Write("Hatal� deneme!")
                        Response.End()
                    End If
            End Select
        Else
            Response.End()
        End If
    End Sub
    Private Sub UyelikFormunuAyarla()
        Dim i As Integer = 0
        Dim li As ListItem
        'G�nler
        For i = 1 To 31
            li = New ListItem
            li.Value = i
            li.Text = i.ToString
            ddlGun.Items.Add(li)
        Next
        'Aylar
        Dim Aylar() As String = {"Ocak", "�ubat", "Mart", "Nisan", "May�s", _
        "Haziran", "Temmuz", "A�ustos", "Eyl�l", "Ekim", "Kas�m", "Aral�k"}
        For i = 1 To 12
            li = New ListItem
            li.Value = i
            li.Text = Aylar(i - 1)
            ddlAy.Items.Add(li)
        Next
        'Y�llar
        For i = 1920 To 2005
            li = New ListItem
            li.Value = i
            li.Text = i.ToString
            ddlYil.Items.Add(li)
        Next
    End Sub
    Private Sub btnTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTamam.Click
        Dim Ad, Soyad, Eposta, WebSite, Lakap, KullaniciAdi, Sifre, SifreTekrar As String
        Dim Gun, Ay, Yil As Integer
        Dim DogumTarihi As String
        Dim SatirSayisi As Integer = 0
        Dim HataMesaji As String = ""

        Dim AydakiGunler() As Byte = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        If Date.Now.Year Mod 4 = 0 Then '�ubat ayarlamas�
            AydakiGunler(1) = 29
        End If

        'Gelen Bilgiler Kontrol ediliyor
        If Not BilgiKontrolu(txtAd.Text, 20, False) Then
            HataMesaji = HataMesaji & "Ad bilgisi hatal� girilmi�. <br>"
        End If
        If Not BilgiKontrolu(txtSoyad.Text, 20, False) Then
            HataMesaji = HataMesaji & "Soyad bilgisi hatal� girilmi�. <br>"
        End If
        If Not EpostaKontrolu(txtEposta.Text) Then
            HataMesaji = HataMesaji & "Eposta adresi hatal� girilmi�. <br>"
        End If
        If Not BilgiKontrolu(txtLakap.Text, 30, True) Then
            HataMesaji = HataMesaji & "Soyad bilgisi hatal� girilmi�. <br>"
        End If
        If Not WebSiteKontrolu(txtWebSite.Text, 100, True) Then
            HataMesaji = HataMesaji & "Web Sitesi adresi hatal� girilmi�. <br>"
        End If
        If Not BilgiKontrolu(txtKullaniciAdi.Text, 25, False) Then
            HataMesaji = HataMesaji & "Kullan�c� ad� hatal� girilmi�. <br>"
        End If
        If Not BilgiKontrolu(txtSifre.Text, 25, False) Then
            HataMesaji = HataMesaji & "�ifre hatal� girilmi�. <br>"
        End If
        If Not BilgiKontrolu(txtSifreTekrar.Text, 25, False) Then
            HataMesaji = HataMesaji & "�ifre tekrar� hatal� girilmi�. <br>"
        End If
        If HataMesaji.Length > 0 Then
            HataMesaji = HataMesaji & "Yukar�daki bilgilere harf, rakam, bo�luk ve nokta d���nda bir karakter giremezsiniz. <br>"
        End If
        If txtSifre.Text <> txtSifreTekrar.Text Then
            HataMesaji = HataMesaji & "�ifrenizi tekrar girerken hatal� girmi�siniz. <br>"
        End If
        If SayisalMi(ddlGun.SelectedItem.Value, 1, 31) And _
        SayisalMi(ddlAy.SelectedItem.Value, 1, 12) And _
        SayisalMi(ddlYil.SelectedItem.Value, 1920, 2005) Then
            Gun = CInt(ddlGun.SelectedItem.Value)
            Ay = CInt(ddlAy.SelectedItem.Value)
            Yil = CInt(ddlYil.SelectedItem.Value)
            If AydakiGunler(Ay - 1) < Gun Then
                HataMesaji = HataMesaji & "Do�um tarihini hatal� se�tiniz. <br>"
            End If
        Else
            HataMesaji = HataMesaji & "Do�um tarihini se�memi�siniz. <br>"
        End If
        'B�yle bir kullan�c� ad�n�n ve/veya email adresinin olup olmad���na bak�l�yor
        If HataMesaji.Length = 0 Then
            'kullan�c� ad�
            KullaniciAdi = txtKullaniciAdi.Text
            Eposta = txtEposta.Text
            sorgu = "SELECT Ad FROM Uyeler WHERE KullaniciAdi='" & KullaniciAdi & "'"
            komut.CommandText = sorgu
            Try
                baglanti.Open()
                okuyucu = komut.ExecuteReader()
                If okuyucu.Read Then
                    HataMesaji = HataMesaji & "Bu kullan�c� ad� ile kay�tl� ba�ka bir �yemiz var. L�tfen ba�ka bir kullan�c� ad� se�iniz. <br>"
                End If
                baglanti.Close()
            Catch ex As Exception
                HataMesaji = HataMesaji & "Sistemde hata var. Bilgileriniz veritaban�na yaz�lamad�. L�tfen daha sonra tekrar deneyin.<br>"
            End Try
            'eposta
            sorgu = "SELECT Ad FROM Uyeler WHERE Eposta='" & Eposta & "'"
            komut.CommandText = sorgu
            Try
                baglanti.Open()
                okuyucu = komut.ExecuteReader()
                If okuyucu.Read Then
                    HataMesaji = HataMesaji & "Bu E-posta Adresi ile kay�tl� ba�ka bir �yemiz var. L�tfen ba�ka bir E-posta adresi se�iniz. <br>"
                End If
                baglanti.Close()
            Catch ex As Exception
                HataMesaji = HataMesaji & "Sistemde hata var. Bilgileriniz veritaban�na yaz�lamad�.<br>"
            End Try
        End If
        'Hata yoksa veritaban�na yaz�l�yor
        If HataMesaji.Length = 0 Then
            Ad = txtAd.Text
            Soyad = txtSoyad.Text
            Eposta = txtEposta.Text
            WebSite = txtWebSite.Text
            If txtLakap.Text = "" Then
                Lakap = Ad
            Else
                Lakap = txtLakap.Text
            End If
            KullaniciAdi = txtKullaniciAdi.Text
            Sifre = txtSifre.Text
            DogumTarihi = Yil.ToString & "-" & Ay.ToString & "-" & Gun.ToString
            sorgu = "INSERT INTO Uyeler(Ad,Soyad,Eposta,WebSite,DogumTarihi," & _
            "UyelikTarihi,YetkiDuzeyi,Lakap,KullaniciAdi,Sifre,Onaylandi)" & _
            " Values('" & Ad & "','" & Soyad & "','" & Eposta & "','" & WebSite & _
            "','" & DogumTarihi & "','" & MysqlKisaTarihYaz(Date.Today) & "',1,'" & _
            Lakap & "','" & KullaniciAdi & "','" & Sifre & "',1)"
            komut.CommandText = sorgu
            Try
                baglanti.Open()
                SatirSayisi = komut.ExecuteNonQuery()
                baglanti.Close()
            Catch ex As Exception
                HataMesaji = HataMesaji & "Sistemde hata var. Bilgileriniz veritaban�na yaz�lamad�.<br>"
            End Try
        End If
        If HataMesaji.Length > 0 Then
            HataMesaji = HataMesaji & "<br>L�tfen geri d�n�p tekrar deneyin. <br>"
            phHata.Visible = True
            phHata.Controls.Add(New LiteralControl(HataMesaji))
            divUyelikFormu.Visible = False
        Else
            Response.Redirect("anasayfa.aspx")
        End If
    End Sub
    Private Sub UyeGirisi()
        divUyeGirisi.Visible = True
    End Sub
    Private Sub btnUGGiris_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUGGiris.Click
        Dim KullaniciAdi, Sifre As String
        Dim Hata As String = ""
        If BilgiKontrolu(txtUGKullaniciAdi.Text, 25, False) _
        And BilgiKontrolu(txtUGSifre.Text, 25, False) Then
            KullaniciAdi = txtUGKullaniciAdi.Text
            Sifre = txtUGSifre.Text
            sorgu = "SELECT UyeNo,Ad,KullaniciAdi,YetkiDuzeyi FROM Uyeler WHERE KullaniciAdi='" & KullaniciAdi & "' AND " & _
            "Sifre='" & Sifre & "' AND Onaylandi=1"
            komut.CommandText = sorgu
            Try
                baglanti.Open()
                okuyucu = komut.ExecuteReader
                If okuyucu.Read Then
                    'giri� yap�l�yor
                    Session("YetkiDuzeyi") = okuyucu("YetkiDuzeyi")
                    Session("KullaniciAdi") = okuyucu("KullaniciAdi")
                    Session("Ad") = okuyucu("Ad")
                    Session("UyeNo") = okuyucu("UyeNo")
                    Response.Redirect("anasayfa.aspx")
                Else
                    phHata.Visible = True
                    Hata = "<p align=""center""><b>Kullan�c� ad� ve/veya �ifre hatal�.</b></p>"
                    phHata.Controls.Add(New LiteralControl(Hata))
                End If
                baglanti.Close()
            Catch ex As Exception
                phHata.Visible = True
                Hata = "<p align=""center""><b>Veritaban�na ba�lan�lamad�.</b></p>"
                phHata.Controls.Add(New LiteralControl(Hata))
            End Try
        End If
    End Sub
    Private Sub UyeGoster(ByVal UyeNo As Integer)
        Dim Ad, Soyad, Eposta, WebSite, Lakap As String
        Dim DogumTarihi As New Date
        Dim UyelikTarihi As New Date
        Dim YetkiDuzeyi As Byte
        sorgu = "SELECT * FROM Uyeler WHERE UyeNo=" & UyeNo
        komut.CommandText = sorgu
        'Veriler al�n�yor
        Try
            baglanti.Open()
            okuyucu = komut.ExecuteReader
            If okuyucu.Read Then
                Ad = okuyucu("Ad")
                Soyad = okuyucu("Soyad")
                Eposta = okuyucu("Eposta")
                WebSite = okuyucu("WebSite")
                Lakap = okuyucu("Lakap")
                UyelikTarihi = okuyucu("UyelikTarihi")
                DogumTarihi = okuyucu("DogumTarihi")
                YetkiDuzeyi = okuyucu("YetkiDuzeyi")
            Else
                Response.Write("Bu �ye veritaban�nda bulunamad�!")
                Response.End()
            End If
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritaban�na ba�lan�lamad�!")
            Response.End()
        End Try
        'tablo olu�turuluyor
        Dim Tablo As New Tablom
        Dim Kolon As Kolonum
        Dim Hucre As Hucrem
        Dim Satir As Satirim
        Dim Bicim As New TablomBicim

        Bicim.BaslikArkaRenk = "#919CB6"
        Bicim.BaslikBoyut = "12px"
        Bicim.BaslikRenk = "#FFFFFF"
        Bicim.SatirArkaRenk1 = "#ebebff"
        Bicim.SatirArkaRenk2 = "#e0e0ff"
        Bicim.TabloArkaRenk = "#444444"
        Bicim.BaslikFont = "Verdana"

        Tablo.Baslik = Ad & " " & Soyad

        Kolon = New Kolonum
        Kolon.Baslik = ""
        Kolon.Genislik = "40%"
        Kolon.icerikHizalama = "right"
        Tablo.KolonEkle(Kolon)
        Kolon = New Kolonum
        Kolon.Baslik = ""
        Kolon.Genislik = "60%"
        Tablo.KolonEkle(Kolon)

        Satir = New Satirim
        Hucre = New Hucrem
        Hucre.Metin = "Lakab�:"
        Satir.HucreEkle(Hucre)
        Hucre = New Hucrem
        Hucre.Metin = Lakap
        Satir.HucreEkle(Hucre)
        Tablo.SatirEkle(Satir)

        Satir = New Satirim
        Hucre = New Hucrem
        Hucre.Metin = "Do�um tarihi:"
        Satir.HucreEkle(Hucre)
        Hucre = New Hucrem
        Hucre.Metin = DogumTarihi.ToLongDateString
        Satir.HucreEkle(Hucre)
        Tablo.SatirEkle(Satir)

        Satir = New Satirim
        Hucre = New Hucrem
        Hucre.Metin = "E-posta adresi:"
        Satir.HucreEkle(Hucre)
        Hucre = New Hucrem
        Hucre.Metin = Eposta
        Satir.HucreEkle(Hucre)
        Tablo.SatirEkle(Satir)

        Satir = New Satirim
        Hucre = New Hucrem
        Hucre.Metin = "Web sitesi:"
        Satir.HucreEkle(Hucre)
        Hucre = New Hucrem
        Hucre.Metin = WebSite
        Satir.HucreEkle(Hucre)
        Tablo.SatirEkle(Satir)

        Satir = New Satirim
        Hucre = New Hucrem
        Hucre.Metin = "�yelik tarihi:"
        Satir.HucreEkle(Hucre)
        Hucre = New Hucrem
        Hucre.Metin = UyelikTarihi.ToLongDateString
        Satir.HucreEkle(Hucre)
        Tablo.SatirEkle(Satir)

        Satir = New Satirim
        Hucre = New Hucrem
        Hucre.Metin = "Sitedeki konumu:"
        Satir.HucreEkle(Hucre)
        Hucre = New Hucrem
        Hucre.Metin = SitedekiKonumu(YetkiDuzeyi)
        Satir.HucreEkle(Hucre)
        Tablo.SatirEkle(Satir)

        Tablo.Bicim = Bicim

        Tablo.HtmlKoduUret(False)
        divUyeBilgisi.Visible = True
        phUyeBilgisi.Controls.Add(New LiteralControl(Tablo.HtmlKodu))

    End Sub
End Class
