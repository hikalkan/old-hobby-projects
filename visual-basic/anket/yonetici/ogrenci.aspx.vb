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
            '�stenilen sayfa ad� QueryString'den al�n�yor
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
    'Sayfa a��ld���nda yap�lmas�n� istenen baz� ayarlamalar
    Private Sub OnAyarlamalar()
        divOgrenciEklemeFormu.Visible = False
    End Sub
    'Kullan�nc� giri�i yap�l�p yap�lmad���na bak�l�yor
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
    'Veritaban�na yeni bir ��renci eklemek i�in gerekli formu g�steren prosed�r
    Private Sub EklemeFormu()
        divOgrenciEklemeFormu.Visible = True
    End Sub
    'Hatal� Parametre giri�inde hata mesaj� verir
    Private Sub ParametreHatasi()
        lblHata.Text = "<p class='Girintili'>Parametre hatas�. L�tfen adres �ubu�una el ile parametre giri�i yapmay�n.</p>"
    End Sub
    'Formdan Gelen Verileri Veritaban�na Yaz
    Private Sub btnEkle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEkle.Click
        'Gelen verilerde hata olup olmad��� denetleniyor
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
            lblHata.Text += "E-posta adresi ge�ersiz.<br>"
        End If
        If Not TarihGecerliMi(txtDogumTarihi.Text) Then
            Hata = True
            lblHata.Text += "Do�um tarihi uygun bi�imde girilmemi�.<br>"
        End If
        If Hata Then
            lblHata.Text += "L�tfen yukar�daki hatalar� d�zeltin ve tekrar deneyin."
            Exit Sub
        End If
        'E�er formdan gelen verilerde herhangi bir hata yoksa veritaban�na yaz�l�yor
        Dim Ogrenci As New clsOgrenciBilgileri
        Ogrenci.Ad = txtAd.Text
        Ogrenci.Soyad = txtSoyad.Text
        Ogrenci.Numara = txtNumara.Text
        Ogrenci.DogumTarihi = Tarih2MySqlTarih(txtDogumTarihi.Text)
        Ogrenci.Eposta = txtEposta.Text
        Ogrenci.OnaylandiMi = 0
        '��renciye rastgele bir �ifre atan�yor...
        Dim Sifre As Long
        Randomize()
        Sifre = Math.Round(Rnd() * 1000000) + 1000000
        Ogrenci.Sifre = Sifre.ToString
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�."
            Exit Sub
        End Try
        'Ayn� numaral� bir ��renci veritaban�nda var m� diye bak�l�yor
        Dim OgrVarMi As Byte
        OgrVarMi = Ogrenci.VeritabanindaVarMi
        Select Case OgrVarMi
            Case 1
                lblHata.Text = "Ayn� numaral� ba�ka bir ��renci veritaban�nda var.<br>"
                lblHata.Text += "L�tfen yukar�daki hatalar� d�zeltin ve tekrar deneyin."
            Case 2
                lblHata.Text = "Veritaban�na ba�lan�lamad�."
        End Select
        If OgrVarMi = 0 Then
            If Ogrenci.VeriTabaninaKaydet() Then
                lblHata.Text = txtNumara.Text & " Numaral� ��renci veritaban�na kaydedildi."
                txtAd.Text = ""
                txtSoyad.Text = ""
                txtNumara.Text = ""
                txtDogumTarihi.Text = ""
                txtEposta.Text = ""
            Else
                lblHata.Text = "Veritaban�na kaydedilemedi."
            End If
        End If
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
    'T�m ��rencileri listele
    Private Sub Listele(Optional ByVal Mezun As Boolean = False)
        'Veritaban� de�i�kenleri
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Dim Adaptor As New OdbcDataAdapter
        Dim Ogrenciler As New DataSet("Ogrenciler")
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
        If Mezun Then
            Sorgu = "SELECT OgrenciNo,Ad,Soyad,Numara,Eposta,DogumTarihi,Sifre,OnaylandiMi FROM Ogrenciler WHERE MezunOlduMu=1 ORDER BY Numara"
        Else
            Sorgu = "SELECT OgrenciNo,Ad,Soyad,Numara,Eposta,DogumTarihi,Sifre,OnaylandiMi FROM Ogrenciler WHERE MezunOlduMu=0 ORDER BY Numara"
        End If
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Ogrenciler, (SayfaNo - 1) * BirSayfadakiKayitSayisi, BirSayfadakiKayitSayisi, "Ogrenciler")
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
            If Not (Komut Is Nothing) Then Komut.Dispose()
            Baglanti.Close()
            Exit Sub
        End Try
        'kay�tlar� g�stermek i�in html tablosu olu�turuluyor...
        Dim Ogrenci As DataRow
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
        If Mezun Then
            Tablo.Baslik = "M E Z U N &nbsp � � R E N C � L E R"
        Else
            Tablo.Baslik = "� � R E N C � L E R"
        End If
        'Kolonlar olu�turuluyor
        Kolon = New clsKolonum
        Kolon.Baslik = "S�ra"
        Kolon.Genislik = "5%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Numara"
        Kolon.Genislik = "15%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Ad� Soyad�"
        Kolon.Genislik = "25%"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Do�um Tarihi"
        Kolon.Genislik = "15%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "E-posta"
        Kolon.Genislik = "25%"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "�ifre"
        Kolon.Genislik = "10%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = ""
        Kolon.Genislik = "5%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        'Sat�rlar olu�turuluyor
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
        'Alt taraftaki di�er sayfalar� g�sterme ba�lant�lar� olu�turuluyor
        Dim MezunOlduMu As Byte
        If Mezun Then MezunOlduMu = 1 Else MezunOlduMu = 0
        Dim kod As String = "<p class='DuzYazi' align='Center'>"
        If SayfaSayisi > 1 Then
            Dim i As Short
            If SayfaNo > 1 Then
                kod += "&lt&lt <a href='ogrenci.aspx?Sayfa=Listele&Mezun=" & MezunOlduMu & "&SayfaNo=" & SayfaNo - 1 & "'>�nceki</a>] "
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
            kod = "<p class='DuzYazi' align='Center'>[<a href='ogrenci.aspx?Sayfa=Listele&SayfaNo=1&Mezun=0'>�u anda ��renimine devam eden ��rencileri g�ster</a>]</p>"
        Else
            kod = "<p class='DuzYazi' align='Center'>[<a href='ogrenci.aspx?Sayfa=Listele&SayfaNo=1&Mezun=1'>Mezun olmu� ��rencileri g�ster</a>]</p>"
        End If
        phOrta.Controls.Add(New LiteralControl(kod))
        'Veritaban�na ba�lant� kapan�yor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
    'Bir ��renciyi veritaban�ndan silmek i�in
    Private Sub OgrenciSil()
        If Request.QueryString("OgrenciNo") <> "" And Request.QueryString("SayfaNo") <> "" Then
            If SayisalMi(Request.QueryString("OgrenciNo"), 1, 1000000) And SayisalMi(Request.QueryString("SayfaNo"), 1, 1000) Then
                Dim Ogrenci As New clsOgrenciBilgileri
                Ogrenci.OgrenciNo = CInt(Request.QueryString("OgrenciNo"))
                Try
                    Baglanti.Open()
                Catch ex As Exception
                    'bo�
                End Try
                Ogrenci.VeriTabanindanKayitSil()
                Try
                    Baglanti.Close()
                Catch ex As Exception
                    'bo�
                End Try
                Response.Redirect("ogrenci.aspx?Sayfa=Listele&SayfaNo=" & Request.QueryString("SayfaNo"))
            End If
        End If
    End Sub
    'Mezun olan ��rencilerin durumunu ayarlamak i�in
    Private Sub Mezuniyet()
        'Veritaban� nesneleri
        Dim Sorgu As String
        Sorgu = "SELECT OgrenciNo,CONCAT(Numara,' ',Ad,' ',Soyad) AS OgrenciAd FROM Ogrenciler WHERE MezunOlduMu=0 ORDER BY Numara"
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim tblOgrenciler As New DataTable
        Dim drOgrenci As DataRow
        'Ba�lant� a��l�yor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lam�yor.<br>"
            Exit Sub
        End Try
        'Veritaban�ndan ��renci bilgileri al�n�yor
        Try
            Adaptor.Fill(tblOgrenciler)
        Catch ex As Exception
            lblHata.Text = "Veritaban�ndan ��renci bilgileri al�namad�."
            Baglantiyi_Kapat()
            Exit Sub
        End Try
        'Mesaj yazd�r�l�yor
        phOrta.Controls.Add(New LiteralControl("<p class='Girintili'>Mezun olan ��rencileri i�aretleyip tamam butonuna bas�n�z.</p>"))
        '��renciler cblOgrenciler listesine ekleniyor
        cblOgrenciler.Visible = True
        Dim liOgrenci As ListItem
        For Each drOgrenci In tblOgrenciler.Rows
            liOgrenci = New ListItem
            liOgrenci.Value = drOgrenci("OgrenciNo")
            liOgrenci.Text = drOgrenci("OgrenciAd")
            cblOgrenciler.Items.Add(liOgrenci)
        Next
        'tamam butonu g�steriliyor
        btnTamam.Visible = True
        'Ba�lant� kapan�yor
        Baglantiyi_Kapat()
    End Sub
    'Ba�lant�y� kapatmak i�in
    Private Sub Baglantiyi_Kapat()
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
    '��aretlenen ��rencileri mezun etmek i�in prosed�r
    Private Sub btnTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTamam.Click
        'Veritaban� nesneleri
        Dim Sorgu As String
        Dim Komut As New OdbcCommand("", Baglanti)
        'Ba�lant� a��l�yor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lam�yor.<br>"
            Exit Sub
        End Try
        'Listede i�aretli olanlar veritaban�nda Mezun olarak ayarlan�yor
        Dim liOgrenci As ListItem
        Sorgu = "UPDATE Ogrenciler SET MezunOlduMu=1 WHERE OgrenciNo="
        For Each liOgrenci In cblOgrenciler.Items
            If liOgrenci.Selected Then
                Komut.CommandText = Sorgu & liOgrenci.Value
                Komut.ExecuteNonQuery()
            End If
        Next
        'Ba�lant� kapan�yor
        Baglantiyi_Kapat()
        'Y�netici sayfas�na y�nlendiriliyor
        Response.Redirect("default.aspx")
    End Sub
End Class
