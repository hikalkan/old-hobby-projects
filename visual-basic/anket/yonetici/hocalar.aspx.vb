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
            '�stenilen sayfa ad� QueryString'den al�n�yor
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
    'Sayfa a��ld���nda yap�lmas�n� istenen baz� ayarlamalar
    Private Sub OnAyarlamalar()
        divHocaEklemeFormu.Visible = False
        divHocaAyrintisi.Visible = False
    End Sub
    'Kullan�nc� giri�i yap�l�p yap�lmad���na bak�l�yor
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
    'Veritaban�na yeni bir hoca eklemek i�in gerekli formu g�steren prosed�r
    Private Sub EklemeFormu()
        divHocaEklemeFormu.Visible = True
    End Sub
    'Hatal� Parametre giri�inde hata mesaj� verir
    Private Sub ParametreHatasi()
        lblHata.Text = "<p class='Girintili'>Parametre hatas�. L�tfen adres �ubu�una el ile parametre giri�i yapmay�n.</p>"
    End Sub
    'veritaban�na hocay� ekleyen k�s�m..
    Private Sub btnEkle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEkle.Click
        'Gelen verilerde hata olup olmad��� denetleniyor
        Dim Hata As Boolean = False
        lblHata.Text = ""
        If Not BilgiKontrolu(txtUnvan.Text, 20, False) Then
            Hata = True
            lblHata.Text += "�nvan bilgisinde uygun olmayan karakterler var.<br>"
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
            lblHata.Text += "E-posta adresi ge�ersiz.<br>"
        End If
        If Not TarihGecerliMi(txtDogumTarihi.Text) Then
            Hata = True
            lblHata.Text += "Do�um tarihi uygun bi�imde girilmemi�.<br>"
        End If
        If Not BilgiKontrolu(txtKullaniciAdi.Text, 20, False) Then
            Hata = True
            lblHata.Text += "Kullan�c� Ad� bilgisinde uygun olmayan karakterler var.<br>"
        End If
        If Not SayisalMi(txtYetkiDuzeyi.Text, HYD_KendiDersi, HYD_Yonetici) Then
            Hata = True
            lblHata.Text += "Yetki d�zeyi alan� " & HYD_KendiDersi & " ile " & HYD_Yonetici & " aras�nda olmal�d�r.<br>"
        End If
        If Hata Then
            lblHata.Text += "L�tfen yukar�daki hatalar� d�zeltin ve tekrar deneyin."
            Exit Sub
        End If
        'E�er formdan gelen verilerde herhangi bir hata yoksa veritaban�na yaz�l�yor
        Dim Hoca As New clsHocaBilgileri
        Hoca.Unvan = txtUnvan.Text
        Hoca.Ad = txtAd.Text
        Hoca.Soyad = txtSoyad.Text
        Hoca.Eposta = txtEposta.Text
        Hoca.DogumTarihi = Tarih2MySqlTarih(txtDogumTarihi.Text)
        Hoca.KullaniciAdi = txtKullaniciAdi.Text
        Hoca.YetkiDuzeyi = txtYetkiDuzeyi.Text
        Hoca.OnaylandiMi = 0
        '��renciye rastgele bir �ifre atan�yor...
        Dim Sifre As Long
        Randomize()
        Sifre = Math.Round(Rnd() * 1000000) + 1000000
        Hoca.Sifre = Sifre.ToString
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�."
            Exit Sub
        End Try
        'Ayn� Kullan�c� Ad�na sahip bir hoca veritaban�nda var m� diye bak�l�yor
        Dim HocaVarMi As Byte
        HocaVarMi = Hoca.VeritabanindaVarMi
        Select Case HocaVarMi
            Case 1
                lblHata.Text = "Ayn� kullan�c� ad�na sahip ba�ka bir Hoca veritaban�nda var.<br>"
                lblHata.Text += "L�tfen yukar�daki hatalar� d�zeltin ve tekrar deneyin."
            Case 2
                lblHata.Text = "Veritaban�na ba�lan�lamad�."
        End Select
        If HocaVarMi = 0 Then
            If Hoca.VeriTabaninaKaydet() Then
                lblHata.Text = txtAd.Text & " " & txtSoyad.Text & " isimli Hoca veritaban�na kaydedildi."
                txtUnvan.Text = ""
                txtAd.Text = ""
                txtSoyad.Text = ""
                txtEposta.Text = ""
                txtDogumTarihi.Text = ""
                txtKullaniciAdi.Text = ""
                txtYetkiDuzeyi.Text = ""
            Else
                lblHata.Text = "Veritaban�na kaydedilemedi."
            End If
        End If
        'Ba�lant� kapat�l�yor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
    'T�m hocalar� listeler
    Private Sub Listele()
        'Veritaban� de�i�kenleri
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Dim Adaptor As New OdbcDataAdapter
        Dim Hocalar As New DataSet("Hocalar")

        Komut.Connection = Baglanti
        Adaptor.SelectCommand = Komut
        'Di�er De�i�kenler..
        Dim BirSayfadakiKayitSayisi As Short = 50
        Dim KayitSayisi As Long = 0
        Dim SayfaSayisi As Short = 0
        Dim SayfaNo As Short = 1
        'Sayfa Numaras� al�n�yor...
        If Request.QueryString("SayfaNo") <> "" Then
            If SayisalMi(Request.QueryString("SayfaNo"), 1, 1000) Then
                SayfaNo = CInt(Request.QueryString("SayfaNo"))
            End If
        End If
        'Veritaban�na ba�lant� a��l�yor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
            Exit Sub
        End Try
        'Veritaban�ndan kay�t say�s� al�n�yor
        Sorgu = "SELECT Count(*) As KayitSayisi FROM Hocalar"
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                KayitSayisi = Okuyucu("KayitSayisi")
            End If
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If KayitSayisi = 0 Then
            lblHata.Text = "Veritaban�nda hi� kay�t yok.<br>"
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
        'Veritaban�ndan kay�tlar al�n�yor
        Sorgu = "SELECT HocaNo,Unvan,Ad,Soyad,Eposta,YetkiDuzeyi,Sifre,OnaylandiMi FROM Hocalar ORDER BY Ad"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Hocalar, (SayfaNo - 1) * BirSayfadakiKayitSayisi, BirSayfadakiKayitSayisi, "Hocalar")
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
            If Not (Komut Is Nothing) Then Komut.Dispose()
            Baglanti.Close()
            Exit Sub
        End Try
        'kay�tlar� g�stermek i�in html tablosu olu�turuluyor...
        Dim Hoca As DataRow
        Dim Tablo As New clsTablom
        Dim Bicim As New clsTablomBicim
        Dim Kolon As clsKolonum
        Dim Satir As clsSatirim
        Dim Hucre As clsHucrem
        'Bi�im �zellikleri
        Bicim.TabloArkaRenk = "#000000"
        Bicim.BaslikArkaRenk = "#294963"
        Bicim.BaslikBoyut = "12px"
        Bicim.BaslikFont = "Verdana"
        Bicim.BaslikRenk = "#FFFFFF"
        Bicim.KolonBaslikArkaRenk = "#919CB6"
        Bicim.KolonBaslikYaziRenk = "#FFFFFF"
        Bicim.SatirYaziRenk1 = "#000000"
        Bicim.SatirYaziRenk2 = "#000000"
        'Tablo ba�l��� ve bi�im atamas�
        Tablo.Bicim = Bicim
        Tablo.Baslik = "H O C A L A R"
        'Kolonlar olu�turuluyor
        Kolon = New clsKolonum
        Kolon.Baslik = "Ad� Soyad�"
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
        Kolon.Baslik = "�ifre"
        Kolon.Genislik = "10%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        'Sat�rlar olu�turuluyor
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
        'Alt taraftaki di�er sayfalar� g�sterme ba�lant�lar� olu�turuluyor
        Dim kod As String = "<p class='DuzYazi' align='Center'>"
        If SayfaSayisi > 1 Then
            Dim i As Short
            If SayfaNo > 1 Then
                kod += "&lt&lt <a href='hocalar.aspx?Sayfa=Listele&SayfaNo=" & SayfaNo - 1 & "'>�nceki</a>] "
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
        'Veritaban�na ba�lant� kapan�yor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
    'Herhangi bir hocan�n ayr�nt�s�n� g�sterir
    Private Sub HocaAyrinti()
        Dim HocaNo As Long = 0
        If Request.QueryString("HocaNo") <> "" Then
            If SayisalMi(Request.QueryString("HocaNo"), 1, 1000000) Then
                HocaNo = CInt(Request.QueryString("HocaNo"))
            End If
        End If
        'HocaNo parametresi hatal� girilmi�se hata mesaj� ver
        If HocaNo = 0 Then
            lblHata.Text = "Hata! Adres �ubu�una el ile parametre girmeyin."
            Exit Sub
        Else
            divHocaAyrintisi.Visible = True
        End If
        'Veritaban�na ba�lan
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�."
            Exit Sub
        End Try
        'Hocan�n bilgilerini veritaban�ndan al
        Dim Hoca As New clsHocaBilgileri
        Hoca.HocaNo = HocaNo
        If Hoca.VeriTabanindanAl() Then ' Hoca Bilgilerini yaz..
            'Ki�isel bilgileri..
            lblAd.Text = Hoca.Unvan & " " & Hoca.Ad & " " & Hoca.Soyad
            lblEposta.Text = Hoca.Eposta
            lblDogumTarihi.Text = MySqlTarih2Tarih(Hoca.DogumTarihi)
            lblKullaniciAdi.Text = Hoca.KullaniciAdi
            lblYetkiDuzeyi.Text = Hoca.YetkiDuzeyi & " (" & YetkiSayi2Yazi(Hoca.YetkiDuzeyi) & ")"
            'Ald��� derslerin listesi veritaban�ndan al�n�yor
            Dim Dersler As New DataTable
            Dim Ders As DataRow
            Dim Sorgu As String = "SELECT * FROM Dersler WHERE HocaNo=" & Hoca.HocaNo & " ORDER BY Yil DESC, Sinif ASC, Donem ASC, Ad ASC"
            Dim Komut As New OdbcCommand(Sorgu, Baglanti)
            Dim Adaptor As New OdbcDataAdapter(Komut)
            Try
                Adaptor.Fill(Dersler)
            Catch ex As Exception
                lblHata.Text = "Dersleri almak i�in veritaban�na ba�lan�lamad�."
            End Try
            If Dersler.Rows.Count > 0 Then
                Dim Tablo As New clsTablom
                Dim Bicim As New clsTablomBicim
                Dim Kolon As clsKolonum
                Dim Satir As clsSatirim
                Dim Hucre As clsHucrem
                'Bi�im �zellikleri
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
                'Tablo ba�l��� ve bi�im atamas�
                Tablo.Bicim = Bicim
                Tablo.Baslik = "Verdi�i Dersler"
                'Kolonlar olu�turuluyor
                Kolon = New clsKolonum
                Kolon.Baslik = "Ders Ad�"
                Kolon.Genislik = "70%"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Y�l"
                Kolon.Genislik = "10%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "S�n�f"
                Kolon.Genislik = "10%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "D�nem"
                Kolon.Genislik = "10%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                'Sat�rlar olu�turuluyor
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
            Else 'yani bu hocan�n �zerinde hi� ders yoksa
                phOrta.Controls.Add(New LiteralControl("<p class='DuzYazi' align='Center'>Bu Hocan�n �zerinde hi� ders yok.</p>"))
            End If
        End If
        'ba�lant�y� kapat
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
End Class
