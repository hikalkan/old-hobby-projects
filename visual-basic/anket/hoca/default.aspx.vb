Imports System.Data.Odbc
Public Class _default2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents divAnaSayfa As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblHocaAd As System.Web.UI.WebControls.Label
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents txtSifreE As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSifreY As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSifreYT As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSifreDegistirTamam As System.Web.UI.WebControls.Button
    Protected WithEvents divSifreDegistir As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblGeri As System.Web.UI.WebControls.Label
    Protected WithEvents lblYonetici As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private SeciliYil As Short = 1
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OnAyarlamalar()
        If SayisalMi(Session("HocaNo"), 1, 1000000000) And Session("KullaniciTuru") = "Hoca" Then
            Dim HocaNo As Long
            HocaNo = Session("HocaNo")
            Dim Sayfa As String = Request.QueryString("Sayfa")
            Select Case Sayfa
                Case "SifreDegistir"
                    divSifreDegistir.Visible = True
                    lblGeri.Text = "[<a href='default.aspx'>Geri</a>]"
                Case Else
                    Ana_Sayfa()
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
    Private Sub Ana_Sayfa()
        Dim HocaNo As Long = 0
        Dim YetkiDuzeyi As Byte = 0
        Dim Donem As Byte = 0
        Dim Yil As Short = 0
        If SayisalMi(Session("HocaNo"), 1, 1000000000) _
        And SayisalMi(Session("YetkiDuzeyi"), HYD_KendiDersi, HYD_Yonetici) _
        And SayisalMi(Session("Donem"), 1, 3) _
        And SayisalMi(Session("OgretimYili"), Date.Now.Year - 5, Date.Now.Year + 10) Then
            HocaNo = Session("HocaNo")
            YetkiDuzeyi = Session("YetkiDuzeyi")
            If YetkiDuzeyi = HYD_Editor Or YetkiDuzeyi = HYD_Yonetici Then
                lblYonetici.Visible = True
            Else
                lblYonetici.Visible = False
            End If
            Yil = Session("OgretimYili")
            Donem = Session("Donem")
        Else
            Response.Redirect("../anasayfa.aspx")
        End If
        If SayisalMi(Request.QueryString("SeciliYil"), 1, Date.Now.Year + 10) Then
            SeciliYil = CShort(Request.QueryString("SeciliYil"))
        End If
        Dim Sorgu As String
        Dim Komut As New Odbc.OdbcCommand(Sorgu, Baglanti)
        Dim Adaptor As New OdbcDataAdapter(Komut)
        'Veritabanýna baðlan
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý."
            Exit Sub
        End Try
        'Hoca bilgileri veritabanýndan alýnýyor
        Dim Hoca As New clsHocaBilgileri
        Hoca.HocaNo = HocaNo
        If Not Hoca.VeriTabanindanAl Then
            Baglanti.Close()
            Response.Redirect("../default.aspx")
        End If
        'Hocanýn adý yazýlýyor
        lblHocaAd.Text = Hoca.Ad & " " & Hoca.Soyad
        'Aldýðý derslerin listesi veritabanýndan alýnýyor
        Dim Dersler As New DataTable
        Dim Ders As DataRow
        Select Case SeciliYil
            Case 1
                Sorgu = "SELECT * FROM Dersler WHERE HocaNo=" & Hoca.HocaNo & " AND Yil=" & Yil & " AND Donem=" & Donem & " ORDER BY Sinif ASC, Ad ASC"
            Case 2
                Sorgu = "SELECT * FROM Dersler WHERE HocaNo=" & Hoca.HocaNo & " AND Yil=" & Yil & " ORDER BY Donem DESC,Sinif ASC, Ad ASC"
            Case Else
                Sorgu = "SELECT * FROM Dersler WHERE HocaNo=" & Hoca.HocaNo & " AND Yil=" & SeciliYil & " ORDER BY Donem DESC,Sinif ASC, Ad ASC"
        End Select
        Komut.CommandText = Sorgu
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
            Tablo.Baslik = "Verdiðiniz Dersler"
            'Kolonlar oluþturuluyor
            Kolon = New clsKolonum
            Kolon.Baslik = "Ders Adý"
            Kolon.Genislik = "65%"
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
            Kolon.Genislik = "15%"
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
                Hucre.Metin = DonemMetin(Ders("Donem"))
                Satir.HucreEkle(Hucre)
                Tablo.SatirEkle(Satir)
            Next
            Tablo.HtmlKoduUret()
            phOrta.Controls.Add(New LiteralControl("<p></p>"))
            phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
        Else 'yani bu hocanýn üzerinde hiç ders yoksa
            phOrta.Controls.Add(New LiteralControl("<p class='DuzYazi' align='Center'>Seçtiðiniz dönemde herhangi bir ders vermiyorsunuz.</p>"))
        End If
        'Eðer Hoca Tüm dersleri görebilirse
        If YetkiDuzeyi > HYD_KendiDersi Then
            Dersler.Dispose()
            Dersler = New DataTable("Dersler")
            Select Case SeciliYil
                Case 1
                    Sorgu = "SELECT Dersler.*,CONCAT(Hocalar.Unvan,' ',Hocalar.Ad,' ',Hocalar.Soyad) AS HocaAd FROM Dersler,Hocalar WHERE Dersler.HocaNo<>" & Hoca.HocaNo & " AND Dersler.HocaNo=Hocalar.HocaNo AND Yil=" & Yil & " AND Donem=" & Donem & " ORDER BY Sinif ASC, Ad ASC"
                Case 2
                    Sorgu = "SELECT Dersler.*,CONCAT(Hocalar.Unvan,' ',Hocalar.Ad,' ',Hocalar.Soyad) AS HocaAd FROM Dersler,Hocalar WHERE Dersler.HocaNo<>" & Hoca.HocaNo & " AND Dersler.HocaNo=Hocalar.HocaNo AND Yil=" & Yil & " ORDER BY Donem DESC,Sinif ASC, Ad ASC"
                Case Else
                    Sorgu = "SELECT Dersler.*,CONCAT(Hocalar.Unvan,' ',Hocalar.Ad,' ',Hocalar.Soyad) AS HocaAd FROM Dersler,Hocalar WHERE Dersler.HocaNo<>" & Hoca.HocaNo & " AND Dersler.HocaNo=Hocalar.HocaNo AND Yil=" & SeciliYil & " ORDER BY Donem DESC,Sinif ASC, Ad ASC"
            End Select
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(Dersler)
            Catch ex As Exception
                lblHata.Text = "Dersleri almak için veritabanýna baðlanýlamadý."
            End Try
            If Dersler.Rows.Count > 0 Then
                phOrta.Controls.Add(New LiteralControl("<p></p>"))
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
                Tablo.Baslik = "Baþka Hocalarýn Verdiði Dersler"
                'Kolonlar oluþturuluyor
                Kolon = New clsKolonum
                Kolon.Baslik = "Ders Adý"
                Kolon.Genislik = "37%"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Hoca Adý"
                Kolon.Genislik = "28%"
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
                Kolon.Genislik = "15%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                'Satýrlar oluþturuluyor
                For Each Ders In Dersler.Rows
                    Satir = New clsSatirim
                    Hucre = New clsHucrem
                    Hucre.Metin = "<a href='dersler.aspx?Sayfa=DersBilgileri&DersNo=" & Ders("DersNo") & "'>" & Ders("Ad") & "</a>"
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = Ders("HocaAd")
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = Ders("Yil")
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = Ders("Sinif")
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = DonemMetin(Ders("Donem"))
                    Satir.HucreEkle(Hucre)
                    Tablo.SatirEkle(Satir)
                Next
                Tablo.HtmlKoduUret()
                phOrta.Controls.Add(New LiteralControl("<p></p>"))
                phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
            Else 'ders yoksa
                phOrta.Controls.Add(New LiteralControl("<p class='DuzYazi' align='Center'>Seçtiðiniz dönem için Baþka hocalarýn verdiði derslere ait kayýt bulunamadý.</p>"))
            End If
        End If
        Dim TabloKodu As String
        TabloKodu = "<p></p>" & _
        "<Table Width='90%' align='center' cellpadding='1' cellspacing='1' bgcolor='#000000'>" & _
        "<tr><td align='center' bgcolor='#DDDDDD' width='100%' class='DuzYazi'>Gösterilen Dersler: "
        phOrta.Controls.Add(New LiteralControl(TabloKodu))
        'Veritabanýndan Yýllar alýnýyor
        Dim EnBuyukYil As Short = 0
        Dim EnKucukYil As Short = 0
        Dim i As Short
        If YetkiDuzeyi > HYD_KendiDersi Then
            Sorgu = "SELECT MAX(Yil) AS EnBuyukYil,MIN(Yil) AS EnKucukYil FROM Dersler"
        Else
            Sorgu = "SELECT MAX(Yil) AS EnBuyukYil,MIN(Yil) AS EnKucukYil FROM Dersler WHERE HocaNo=" & HocaNo
        End If
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                EnBuyukYil = Okuyucu("EnBuyukYil")
                EnKucukYil = Okuyucu("EnKucukYil")
            End If
        Catch ex As Exception
            'boþ
        End Try
        Dim ddlYillar As New DropDownList
        Dim Li As ListItem
        ddlYillar.ID = "ddlYillar"
        Li = New ListItem
        Li.Text = "Bu dönem"
        Li.Value = 1
        If SeciliYil = 1 Then
            Li.Selected = True
        End If
        ddlYillar.Items.Add(Li)
        Li = New ListItem
        Li.Text = "Bu yýl"
        Li.Value = 2
        If SeciliYil = 2 Then
            Li.Selected = True
        End If
        ddlYillar.Items.Add(Li)
        If EnKucukYil > 0 And EnBuyukYil > 0 Then
            For i = EnBuyukYil To EnKucukYil Step -1
                If i <> Yil Then ' Yil: Þu andaki öðretim yýlý
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
        'baðlantýyý kapat
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
        divAnaSayfa.Visible = True
    End Sub
    Private Sub ddlYillar_SeciliOgeDegistiginde(ByVal Sender As Object, ByVal E As EventArgs)
        Dim ddlYillar As DropDownList = Sender
        If ddlYillar.SelectedIndex >= 0 Then
            If SayisalMi(ddlYillar.SelectedItem.Value, 1, Date.Now.Year + 10) Then
                Response.Redirect("default.aspx?SeciliYil=" & CShort(ddlYillar.SelectedItem.Value))
            End If
        End If
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
        Dim HocaNo As Long
        If SayisalMi(Session("HocaNo"), 1, 1000000000) Then
            HocaNo = Session("HocaNo")
        Else
            Response.Redirect("../default.aspx")
        End If
        'Baðlantýyý aç
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamýyor."
            Exit Sub
        End Try
        'Hoca bilgilerini veritabanýndan al
        Dim Hoca As New clsHocaBilgileri
        Hoca.HocaNo = HocaNo
        If Not Hoca.VeriTabanindanAl() Then
            lblHata.Text = "Veritabanýnda bulunamadý."
            Baglantiyi_Kapat()
            Exit Sub
        End If
        'Eski þifreyi karþýlaþtýr, yanlýþsa çýk
        Dim Hata As Boolean = False
        If Hoca.Sifre <> txtSifreE.Text Then
            lblHata.Text += "Eski þifrenizi hatalý girdiniz.<br>"
            Hata = True
        End If
        If txtSifreY.Text <> txtSifreYT.Text Then
            lblHata.Text += "Þifre tekrarý üstteki ile ayný deðil.<br>"
            Hata = True
        End If
        If Not Hata Then
            Hoca.Sifre = txtSifreY.Text.Trim
            Try
                If Not Hoca.SifeGuncelle() Then
                    Hata = True
                End If
            Catch ex As Exception
                Hata = True
            End Try
        End If
        Baglantiyi_Kapat()
        If Not Hata Then
            Response.Redirect("default.aspx")
        Else
            lblHata.Text += "Þifreniz deðiþtirilemedi."
        End If
    End Sub
    Private Sub Baglantiyi_Kapat()
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
End Class
