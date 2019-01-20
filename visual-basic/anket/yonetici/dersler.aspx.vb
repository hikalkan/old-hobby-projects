Imports System.Data.Odbc
Public Class dersler
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAd As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnEkle As System.Web.UI.WebControls.Button
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents divDersEklemeFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents ddlSinif As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlDonem As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYil As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lbHocalar As System.Web.UI.WebControls.ListBox
    Protected WithEvents divOgrenciSecimi1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents divOgrenciSecimi2 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents btnOgrenciSecimi1 As System.Web.UI.WebControls.Button
    Protected WithEvents lstDersler As System.Web.UI.WebControls.ListBox
    Protected WithEvents ddlDersYili As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cblOgrenciler As System.Web.UI.WebControls.CheckBoxList
    Protected WithEvents lblOSDersAd As System.Web.UI.WebControls.Label
    Protected WithEvents lblOSHocaAd As System.Web.UI.WebControls.Label
    Protected WithEvents lblOSSinif As System.Web.UI.WebControls.Label
    Protected WithEvents lblOSDonem As System.Web.UI.WebControls.Label
    Protected WithEvents lblOSYil As System.Web.UI.WebControls.Label
    Protected WithEvents btnOSGuncelle As System.Web.UI.WebControls.Button
    Protected WithEvents divDersBilgileri As System.Web.UI.HtmlControls.HtmlGenericControl

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
                    Dim SeciliYil As Short
                    If SayisalMi(Request.QueryString("SeciliYil"), 2000, 2100) Then
                        SeciliYil = CShort(Request.QueryString("SeciliYil"))
                    ElseIf SayisalMi(Session("OgretimYili"), Date.Now.Year - 5, Date.Now.Year + 10) Then
                        SeciliYil = CShort(Session("OgretimYili"))
                    Else
                        SeciliYil = 1
                    End If
                    Listele(SeciliYil)
                Case "OgrenciSecimi"
                    Dim Adim As String = Request.QueryString("Adim")
                    Select Case Adim
                        Case "DersSecimi"
                            OgrenciSecimi_Adim1_DersSecimi()
                        Case "OgrenciSecimi"
                            OgrenciSecimi_Adim2_OgrenciSecimi()
                    End Select
                Case "DersBilgileri"
                    DersBilgileri()
                Case Else
                    ParametreHatasi()
            End Select
        Else
            Response.Redirect("../default.aspx")
        End If
    End Sub
    'Sayfa a��ld���nda yap�lmas�n� istenen baz� ayarlamalar
    Private Sub OnAyarlamalar()
        divDersEklemeFormu.Visible = False
        divOgrenciSecimi1.Visible = False
        divOgrenciSecimi2.Visible = False
        divDersBilgileri.Visible = False
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
    'Veritaban�na yeni bir hoca eklemek i�in gerekli formu g�steren prosed�r
    Private Sub EklemeFormu()
        'Ekleme formu g�r�n�r yap�l�yor
        divDersEklemeFormu.Visible = True
        If Not Page.IsPostBack Then
            Dim eleman As ListItem
            'Hocalar listesi dolduruluyor
            Dim Sorgu As String = "SELECT HocaNo,Unvan,Ad,Soyad FROM Hocalar ORDER BY Ad"
            Dim Komut As New OdbcCommand(Sorgu, Baglanti)
            Dim Adaptor As New OdbcDataAdapter(Komut)
            Dim Hocalar As New DataTable
            Dim Hoca As DataRow
            'Ba�lant� a��l�yor
            Try
                Baglanti.Open()
            Catch ex As Exception
                lblHata.Text = "Veritaban�na ba�lan�lamad�."
                Exit Sub
            End Try
            'Hoca bilgileri Hocalar tablosuna al�n�yor
            Try
                Adaptor.Fill(Hocalar)
            Catch ex As Exception
                lblHata.Text = "Veritaban�na ba�lan�lamad�."
                Exit Sub
            End Try
            eleman = New ListItem
            eleman.Text = "A�a��dan se�in.."
            eleman.Value = 0
            eleman.Selected = True
            lbHocalar.Items.Add(eleman)
            For Each Hoca In Hocalar.Rows
                eleman = New ListItem
                eleman.Text = Hoca("Unvan") & " " & Hoca("Ad") & " " & Hoca("Soyad")
                eleman.Value = Hoca("HocaNo")
                lbHocalar.Items.Add(eleman)
            Next
            '�imdiki ��retim Y�l� veritaban�ndan al�n�yor
            Dim Ayarlar As New clsAyarlar
            Dim Donem As Byte = Ayarlar.Donem
            Dim SimdikiYil As Short = Ayarlar.OgretimYili
            'Y�l listesi dolduruluyor
            Dim Yil As Short
            For Yil = Date.Now.Year - 5 To Date.Now.Year + 10
                eleman = New ListItem
                eleman.Text = Yil
                eleman.Value = Yil
                If Yil = SimdikiYil Then eleman.Selected = True Else eleman.Selected = False
                ddlYil.Items.Add(eleman)
            Next
            'D�nem ayarlan�yor
            If ddlDonem.SelectedIndex >= 0 Then
                ddlDonem.SelectedItem.Selected = False
                ddlDonem.Items(Donem).Selected = True
            End If
            Try
                Baglanti.Close()
            Catch ex As Exception
                'bo�
            End Try
        End If 'If Not Page.IsPostBack
    End Sub
    'Hatal� Parametre giri�inde hata mesaj� verir
    Private Sub ParametreHatasi()
        lblHata.Text = "<p class='Girintili'>Parametre hatas�. L�tfen adres �ubu�una el ile parametre giri�i yapmay�n.</p>"
    End Sub
    'Dersi veritaban�na kaydeden k�s�m..
    Private Sub btnEkle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEkle.Click
        'Girilen bilgilerde hata olup olmad���na bak�l�yor
        lblHata.Text = ""
        Dim Hata As Boolean = False
        If lbHocalar.SelectedItem.Value = 0 Then
            Hata = True
            lblHata.Text += "Bu ders i�in bir Hoca se�memi�siniz.<br>"
        End If
        If Not BilgiKontroluGenel(txtAd.Text, 50, False) Then
            Hata = True
            lblHata.Text += "Ders ad�nda uygun olmayan karakterler var.<br>"
        End If
        If Not SayisalMi(ddlSinif.SelectedItem.Value, 1, 5) Then
            Hata = True
            lblHata.Text += "S�n�f se�memi�siniz.<br>"
        End If
        If Not SayisalMi(ddlDonem.SelectedItem.Value, 1, 3) Then
            Hata = True
            lblHata.Text += "D�nem se�memi�siniz.<br>"
        End If
        If Not SayisalMi(ddlYil.SelectedItem.Value, Date.Now.Year - 5, Date.Now.Year + 10) Then
            Hata = True
            lblHata.Text += "Y�l� se�memi�siniz.<br>"
        End If
        If Hata Then
            lblHata.Text += "L�tfen yukar�daki hatalar� d�zeltin ve tekrar deneyin."
            Exit Sub
        End If
        'T�m veriler do�ru girilmi�se veritaban�na yaz�l�yor
        Dim Ders As New clsDersBilgileri
        Ders.HocaNo = lbHocalar.SelectedItem.Value
        Ders.Ad = txtAd.Text.Trim
        Ders.Sinif = ddlSinif.SelectedItem.Value
        Ders.Donem = ddlDonem.SelectedItem.Value
        Ders.Yil = ddlYil.SelectedItem.Value
        'Veritaban�na ba�lant� a��l�yor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�."
            Exit Sub
        End Try
        'Ayn� isme sahip ayn� y�lda ba�ka bir ders veritaban�nda var m� diye bak�l�yor
        Dim DersVarMi As Byte
        DersVarMi = Ders.VeritabanindaVarMi
        Select Case DersVarMi
            Case 1
                lblHata.Text = "Ayn� ada sahip ba�ka bir ders veritaban�nda var.<br>"
                lblHata.Text += "L�tfen yukar�daki hatalar� d�zeltin ve tekrar deneyin."
            Case 2
                lblHata.Text = "Veritaban�na ba�lan�lamad�."
        End Select
        If DersVarMi = 0 Then
            Ders.Ad = Ders.Ad.Replace("'", "''")
            If Ders.VeritabaninaKaydet Then
                lblHata.Text = txtAd.Text.Trim & " isimli ders " & ddlYil.SelectedItem.Value & " y�l� i�in veritaban�na kaydedildi."
                lbHocalar.SelectedIndex = 0
                txtAd.Text = ""
                ddlSinif.SelectedIndex = 0
                ddlDonem.SelectedIndex = 0
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
    'Se�ili Y�ldaki T�m dersler listeleniyor
    Private Sub Listele(ByVal SeciliYil As Short)
        'Veritaban� de�i�kenleri
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Dim Adaptor As New OdbcDataAdapter
        Dim Dersler As New DataSet("Dersler")
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
        Dim OgretimYili As Short = 0
        Dim Ayarlar As New clsAyarlar
        OgretimYili = Ayarlar.OgretimYili
        If SeciliYil = 1 Then SeciliYil = OgretimYili
        'Veri taban�ndan Toplam ders say�s� al�n�yor
        Sorgu = "SELECT Count(*) As KayitSayisi FROM Dersler WHERE Yil=" & SeciliYil
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader()
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
        Sorgu = "Select Dersler.DersNo,Dersler.HocaNo," & _
        "CONCAT(Hocalar.Unvan,' ',Hocalar.Ad,' ',Hocalar.Soyad) AS HocaAd," & _
        "Dersler.Ad,Dersler.Sinif,Dersler.Donem,Dersler.Yil FROM Hocalar " & _
        "INNER JOIN Dersler ON Dersler.HocaNo=Hocalar.HocaNo " & _
        "WHERE Dersler.Yil=" & SeciliYil & _
        " ORDER BY Dersler.Donem ASC,Dersler.Sinif ASC,Dersler.Ad ASC"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Dersler, (SayfaNo - 1) * BirSayfadakiKayitSayisi, BirSayfadakiKayitSayisi, "Dersler")
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
            If Not (Komut Is Nothing) Then Komut.Dispose()
            Baglanti.Close()
            Exit Sub
        End Try
        'kay�tlar� g�stermek i�in html tablosu olu�turuluyor...
        Dim Ders As DataRow
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
        Tablo.Baslik = "D E R S L E R"
        'Kolonlar olu�turuluyor
        Kolon = New clsKolonum
        Kolon.Baslik = "DersAd�"
        Kolon.Genislik = "35%"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "HocaAd�"
        Kolon.Genislik = "35%"
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
        Kolon = New clsKolonum
        Kolon.Baslik = "Y�l"
        Kolon.Genislik = "10%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        'Sat�rlar olu�turuluyor
        For Each Ders In Dersler.Tables(0).Rows
            Satir = New clsSatirim
            Hucre = New clsHucrem
            Hucre.Metin = "<a href='dersler.aspx?Sayfa=DersBilgileri&DersNo=" & Ders("DersNo") & "'>" & Ders("Ad") & " </a>"
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = "<a href='hocalar.aspx?Sayfa=Ayrinti&HocaNo=" & Ders("HocaNo") & "'>" & Ders("HocaAd") & " </a>"
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = Ders("Sinif")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = Ders("Donem")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = Ders("Yil")
            Satir.HucreEkle(Hucre)
            Tablo.SatirEkle(Satir)
        Next
        Tablo.HtmlKoduUret()
        phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
        'Alt taraftaki di�er sayfalar� g�sterme ba�lant�lar� olu�turuluyor
        Dim kod As String = "<p class='DuzYazi' align='Center'>"
        Dim i As Short
        If SayfaSayisi > 1 Then
            If SayfaNo > 1 Then
                kod += "&lt&lt <a href='dersler.aspx?Sayfa=Listele&SeciliYil=" & SeciliYil & "&SayfaNo=" & SayfaNo - 1 & "'>�nceki</a>] "
            End If
            For i = 1 To SayfaSayisi
                If i = SayfaNo Then
                    kod += "[" & i & "] "
                Else
                    kod += "[<a href='dersler.aspx?Sayfa=Listele&SeciliYil=" & SeciliYil & "&SayfaNo=" & i & "'>" & i & "</a>] "
                End If
            Next
            If SayfaNo < SayfaSayisi Then
                kod += "[<a href='dersler.aspx?Sayfa=Listele&SeciliYil=" & SeciliYil & "&SayfaNo=" & SayfaNo + 1 & "'>Sonraki</a> &gt&gt"
            End If
        End If
        kod += "</p>"
        phOrta.Controls.Add(New LiteralControl(kod))
        Dim TabloKodu As String
        TabloKodu = "<p></p>" & _
        "<Table Width='90%' align='center' cellpadding='1' cellspacing='1' bgcolor='#000000'>" & _
        "<tr><td align='center' bgcolor='#DDDDDD' width='100%' class='DuzYazi'>G�sterilen Dersler: "
        phOrta.Controls.Add(New LiteralControl(TabloKodu))
        'Veritaban�ndan Y�llar al�n�yor
        Dim EnBuyukYil As Short = 0
        Dim EnKucukYil As Short = 0
        Sorgu = "SELECT MAX(Yil) AS EnBuyukYil,MIN(Yil) AS EnKucukYil FROM Dersler"
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                EnBuyukYil = Okuyucu("EnBuyukYil")
                EnKucukYil = Okuyucu("EnKucukYil")
            End If
        Catch ex As Exception
            'bo�
        End Try
        Dim ddlYillar As New DropDownList
        Dim Li As ListItem
        ddlYillar.ID = "ddlYillar"
        Li = New ListItem
        Li.Text = "Bu y�l"
        Li.Value = 2
        If SeciliYil = 2 Then
            Li.Selected = True
        End If
        ddlYillar.Items.Add(Li)
        If EnKucukYil > 0 And EnBuyukYil > 0 Then
            For i = EnBuyukYil To EnKucukYil Step -1
                If i <> OgretimYili Then ' Yil: �u andaki ��retim y�l�
                    Li = New ListItem
                    Li.Text = i
                    Li.Value = i
                    If i = SeciliYil Then
                        Li.Selected = True
                    End If
                    ddlYillar.Items.Add(Li)
                End If
            Next
        End If
        ddlYillar.AutoPostBack = True
        AddHandler ddlYillar.SelectedIndexChanged, AddressOf ddlYillar_SeciliOgeDegistiginde
        phOrta.Controls.Add(ddlYillar)
        TabloKodu = "</td></tr>" & _
        "</Table><p></p>"
        phOrta.Controls.Add(New LiteralControl(TabloKodu))
        'Veritaban�na ba�lant� kapan�yor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
    Private Sub ddlYillar_SeciliOgeDegistiginde(ByVal Sender As Object, ByVal E As EventArgs)
        Dim ddlYillar As DropDownList = Sender
        If ddlYillar.SelectedIndex >= 0 Then
            If SayisalMi(ddlYillar.SelectedItem.Value, 1, Date.Now.Year + 10) Then
                Response.Redirect("dersler.aspx?Sayfa=Listele&SeciliYil=" & CShort(ddlYillar.SelectedItem.Value))
            End If
        End If
    End Sub
    'Bir dersi alan ��rencilerinin veritaban�na girilmesi k�sm�
    Private Sub YilaGoreDersDoldur(ByVal OgretimYili As Short)
        Dim Sorgu As String
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim tblDersler As New DataTable
        Dim birDers As DataRow
        Dim ListeElemani As ListItem
        Sorgu = "SELECT DersNo,Ad,Donem FROM Dersler WHERE Yil=" & OgretimYili & " ORDER BY Ad"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(tblDersler)
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�."
            Exit Sub
        End Try
        For Each birDers In tblDersler.Rows
            ListeElemani = New ListItem
            ListeElemani.Text = birDers("Ad") & " (" & DonemMetin(birDers("Donem")) & ")"
            ListeElemani.Value = birDers("DersNo")
            lstDersler.Items.Add(ListeElemani)
        Next
    End Sub
    Private Sub OgrenciSecimi_Adim1_DersSecimi()
        divOgrenciSecimi1.Visible = True
        Dim OgretimYili As Short = Date.Now.Year
        Dim ListeElemani As ListItem
        If Not Page.IsPostBack Then
            Dim Yil As Short
            For Yil = 2000 To OgretimYili + 10
                ListeElemani = New ListItem
                ListeElemani.Text = Yil
                ListeElemani.Value = Yil
                If Yil = OgretimYili Then ListeElemani.Selected = True Else ListeElemani.Selected = False
                ddlDersYili.Items.Add(ListeElemani)
            Next
            YilaGoreDersDoldur(OgretimYili)
        End If
    End Sub
    Private Sub btnOgrenciSecimi1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOgrenciSecimi1.Click
        If lstDersler.SelectedIndex >= 0 Then
            If SayisalMi(lstDersler.SelectedItem.Value, 1, 1000000000) Then
                Response.Redirect("dersler.aspx?Sayfa=OgrenciSecimi&Adim=OgrenciSecimi&DersNo=" & lstDersler.SelectedItem.Value)
            Else
                lblHata.Text = "Devam etmeden �nce bir ders se�melisiniz."
            End If
        Else
            lblHata.Text = "Devam etmeden �nce bir ders se�melisiniz."
        End If
    End Sub
    Private Sub ddlDersYili_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlDersYili.SelectedIndexChanged
        If ddlDersYili.SelectedIndex >= 0 Then
            If SayisalMi(ddlDersYili.SelectedItem.Value, 2000, Date.Now.Year + 10) Then
                lstDersler.Items.Clear()
                YilaGoreDersDoldur(CInt(ddlDersYili.SelectedItem.Value))
            End If
        End If
    End Sub
    Private Sub OgrenciSecimi_Adim2_OgrenciSecimi()
        'DersNo parametresi kontrol ediliyor
        Dim DersNo As Long = 0
        If Request.QueryString("DersNo") <> "" Then
            If SayisalMi(Request.QueryString("DersNo"), 1, 1000000000) Then
                DersNo = CInt(Request.QueryString("DersNo"))
            End If
        End If
        If DersNo = 0 Then
            lblHata.Text = "�nce dersi se�melisiniz."
            Exit Sub
        End If
        'Ders bilgileri veritaban�ndan al�n�yor..
        Dim DersVar As Boolean = False
        Dim Sorgu As String = "Select Dersler.DersNo,Dersler.HocaNo," & _
        "CONCAT(Hocalar.Unvan,' ',Hocalar.Ad,' ',Hocalar.Soyad) AS HocaAd," & _
        "Dersler.Ad,Dersler.Sinif,Dersler.Donem,Dersler.Yil FROM Hocalar " & _
        "INNER JOIN Dersler ON Dersler.HocaNo=Hocalar.HocaNo WHERE Dersler.DersNo=" & DersNo
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)
        'Veritaban�na ba�lan�l�yor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lam�yor."
            Exit Sub
        End Try
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                lblOSDersAd.Text = Okuyucu("Ad")
                lblOSHocaAd.Text = Okuyucu("HocaAd")
                lblOSSinif.Text = Okuyucu("Sinif")
                lblOSDonem.Text = Okuyucu("Donem")
                lblOSYil.Text = Okuyucu("Yil")
                DersVar = True
            End If
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lam�yor."
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        'Ders bilgileri bulunduysa ��rencileri getir..
        If DersVar Then
            divOgrenciSecimi2.Visible = True
            divDersBilgileri.Visible = True
            If Not Page.IsPostBack Then
                Dim Adaptor As New OdbcDataAdapter(Komut)
                Dim tblOgrenciler As New DataTable
                Dim drOgrenci As DataRow
                Sorgu = "(SELECT DersOgrenciIliskileri.OgrenciNo,CONCAT(Numara,' ',Ad,' ',Soyad) AS Adi,1 AS Var" & _
                " FROM Ogrenciler,DersOgrenciIliskileri " & _
                "WHERE (DersOgrenciIliskileri.OgrenciNo=Ogrenciler.OgrenciNo) AND DersNo=" & DersNo & " AND MezunOlduMu=0)" & _
                " UNION " & _
                "(SELECT OgrenciNo,CONCAT(Numara,' ',Ad,' ',Soyad) AS Adi,0 AS Var FROM Ogrenciler WHERE MezunOlduMu=0)" & _
                "ORDER BY Adi,Var"
                Komut.CommandText = Sorgu
                Try
                    Adaptor.Fill(tblOgrenciler)
                Catch ex As Exception
                    lblHata.Text = "��renci bilgileri veritaban�ndan al�namad�.<br>"
                End Try
                Dim OgrenciNo As Long = 0
                Dim ListeElemani As ListItem
                If tblOgrenciler.Rows.Count > 0 Then
                    For Each drOgrenci In tblOgrenciler.Rows
                        If drOgrenci("OgrenciNo") = OgrenciNo Then
                            cblOgrenciler.Items(cblOgrenciler.Items.Count - 1).Selected = True
                        Else
                            ListeElemani = New ListItem
                            ListeElemani.Text = drOgrenci("Adi")
                            ListeElemani.Value = drOgrenci("OgrenciNo")
                            ListeElemani.Selected = False
                            cblOgrenciler.Items.Add(ListeElemani)
                            OgrenciNo = drOgrenci("OgrenciNo")
                        End If 'drOgrenci("OgrenciNo") = OgrenciNo
                    Next 'Each drOgrenci
                Else
                    lblHata.Text = "Veritaban�na kay�tl� hi� ��renci bulunamad�."
                End If 'tblOgrenciler.Rows.Count > 0
            End If 'Not Page.IsPostBack
        Else
            lblHata.Text = "Ders bilgileri veritaban�ndan al�namad�."
        End If 'DersVar
        'Ba�lant� kapat�l�yor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
    Private Sub btnOSGuncelle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOSGuncelle.Click
        Dim DersNo As Long = 0
        If Request.QueryString("DersNo") <> "" Then
            If SayisalMi(Request.QueryString("DersNo"), 1, 1000000000) Then
                DersNo = CInt(Request.QueryString("DersNo"))
            End If
        End If
        If DersNo = 0 Then
            lblHata.Text = "�nce dersi se�melisiniz."
            Exit Sub
        End If
        Dim DersVar As Boolean = False
        Dim tblIliskiler As New DataTable
        Dim Sorgu As String
        Dim Adaptor As New OdbcDataAdapter("SELECT * FROM DersOgrenciIliskileri WHERE DersNo=" & DersNo, Baglanti)
        Dim Guncelleme As New OdbcCommandBuilder(Adaptor)
        Sorgu = "SELECT Ad FROM Dersler WHERE DersNo=" & DersNo
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)

        'Ba�lant� a��l�yor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lam�yor."
            Exit Sub
        End Try
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                DersVar = True
            End If
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lam�yor."
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If DersVar Then
            Dim Trans As OdbcTransaction
            Try
                Trans = Baglanti.BeginTransaction
                Komut.Transaction = Trans
                Sorgu = "DELETE FROM DersOgrenciIliskileri WHERE DersNo=" & DersNo
                Komut.CommandText = Sorgu
                Komut.ExecuteNonQuery()
                Adaptor.SelectCommand.Transaction = Trans
                Adaptor.Fill(tblIliskiler)
                Dim ListeElemani As New ListItem
                Dim yeniSatir As DataRow
                For Each ListeElemani In cblOgrenciler.Items
                    If ListeElemani.Selected Then
                        yeniSatir = tblIliskiler.NewRow()
                        yeniSatir("DersNo") = DersNo
                        yeniSatir("VizeNotu") = 101
                        yeniSatir("FinalNotu") = 101
                        yeniSatir("AnketDoldurulduMu") = 0
                        yeniSatir("OgrenciNo") = ListeElemani.Value
                        tblIliskiler.Rows.Add(yeniSatir)
                    End If
                Next
                Guncelleme.GetInsertCommand.Transaction = Trans
                Adaptor.Update(tblIliskiler)
                lblHata.Text = "G�ncellendi."
                Trans.Commit()
            Catch ex As Exception
                Trans.Rollback()
            Finally
                If Not (Komut Is Nothing) Then Komut.Dispose()
            End Try
        End If
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
    'Bir dersin bilgileri
    Private Sub DersBilgileri()
        'DersNo parametresi kontrol ediliyor
        Dim DersNo As Long = 0
        If Request.QueryString("DersNo") <> "" Then
            If SayisalMi(Request.QueryString("DersNo"), 1, 1000000000) Then
                DersNo = CInt(Request.QueryString("DersNo"))
            End If
        End If
        If DersNo = 0 Then
            lblHata.Text = "�nce dersi se�melisiniz."
            Exit Sub
        End If
        'Ders bilgileri veritaban�ndan al�n�yor..
        Dim DersVar As Boolean = False
        Dim Sorgu As String = "Select Dersler.DersNo,Dersler.HocaNo," & _
        "CONCAT(Hocalar.Unvan,' ',Hocalar.Ad,' ',Hocalar.Soyad) AS HocaAd," & _
        "Dersler.Ad,Dersler.Sinif,Dersler.Donem,Dersler.Yil FROM Hocalar " & _
        "INNER JOIN Dersler ON Dersler.HocaNo=Hocalar.HocaNo WHERE Dersler.DersNo=" & DersNo
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim Ogrenciler As New DataTable("Ogrenciler")

        'Veritaban�na ba�lan�l�yor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lam�yor."
            Exit Sub
        End Try
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                lblOSDersAd.Text = Okuyucu("Ad")
                lblOSHocaAd.Text = Okuyucu("HocaAd")
                lblOSSinif.Text = Okuyucu("Sinif")
                lblOSDonem.Text = DonemMetin(Okuyucu("Donem"))
                lblOSYil.Text = Okuyucu("Yil")
                DersVar = True
            End If
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lam�yor."
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If DersVar Then
            divDersBilgileri.Visible = True
            'Veritaban�ndan kay�tlar al�n�yor
            Sorgu = "SELECT Ogrenciler.OgrenciNo,Ad,Soyad,Numara,Eposta,DogumTarihi,OnaylandiMi " & _
            "FROM Ogrenciler,DersOgrenciIliskileri " & _
            "WHERE Ogrenciler.OgrenciNo=DersOgrenciIliskileri.OgrenciNo AND DersNo=" & DersNo & _
            " ORDER BY Numara"
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(Ogrenciler)
            Catch ex As Exception
                lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
                If Not (Komut Is Nothing) Then Komut.Dispose()
                Baglanti.Close()
                Exit Sub
            End Try
            If Ogrenciler.Rows.Count > 0 Then
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
                Bicim.Genislik = "90%"
                'Tablo ba�l��� ve bi�im atamas�
                Tablo.Bicim = Bicim
                Tablo.Baslik = "� � R E N C � L E R"
                'Kolonlar olu�turuluyor
                Kolon = New clsKolonum
                Kolon.Baslik = "Numara"
                Kolon.Genislik = "15%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Ad� Soyad�"
                Kolon.Genislik = "30%"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Do�um Tarihi"
                Kolon.Genislik = "15%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "E-posta"
                Kolon.Genislik = "35%"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Onay"
                Kolon.Genislik = "5%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                'Sat�rlar olu�turuluyor
                For Each Ogrenci In Ogrenciler.Rows
                    Satir = New clsSatirim
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
                    Hucre.Metin = Ogrenci("OnaylandiMi")
                    Satir.HucreEkle(Hucre)
                    Tablo.SatirEkle(Satir)
                Next
                Tablo.HtmlKoduUret()
                Dim TabloKodu As String
                TabloKodu = "<p class='DuzYazi'></p>" & _
                "<Table Width='90%' align='center' cellpadding='1' cellspacing='1' bgcolor='#000000'>" & _
                "<tr><td align='center' bgcolor='#DDDDDD' width='100%' class='DuzYazi'><a href='../hoca/anket.aspx?Sayfa=Ortalama&DersNo=" & DersNo & "'>" & _
                "- BU DERS�N ANKET�N� G�RMEK ���N TIKLAYIN -" & _
                "</a></td></tr>" & _
                "</Table>" & _
                "<p class='DuzYazi'></p>"
                phOrta.Controls.Add(New LiteralControl(TabloKodu))
                phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
            Else
                lblHata.Text = "Bu derse kay�tl� herhangi bir ��renci bulunamad�."
            End If
        Else
            lblHata.Text = "Ders bulunamad�."
        End If
        'Ba�lant� kapat�l�yor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
End Class
