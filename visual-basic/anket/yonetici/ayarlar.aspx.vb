Public Class ayarlar
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents divAyarFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents ddlDonem As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlAnketDurumu As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlOgretimYili As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnAyarlariKaydet As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private HocaNo As Long = 0
    Private YetkiDuzeyi As Byte = 0
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OnAyarlamalar()
        If GirisYapilmisMi() Then
            Dim Sayfa As String
            Sayfa = Request.QueryString("Sayfa")
            Select Case Sayfa
                Case "Degistir"
                    DegistirmeFormunuHazirla()
                Case Else
                    ParametreHatasi()
            End Select
        Else
            Response.Redirect("../default.aspx")
        End If
    End Sub
    Private Sub OnAyarlamalar()
        divAyarFormu.Visible = False
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
    'Hatal� Parametre giri�inde hata mesaj� verir
    Private Sub ParametreHatasi()
        lblHata.Text = "<p class='Girintili'>Parametre hatas�. L�tfen adres �ubu�una el ile parametre giri�i yapmay�n.</p>"
    End Sub
    'Ayarlar� de�i�tirmek i�in formu haz�rlar
    Private Sub DegistirmeFormunuHazirla()
        divAyarFormu.Visible = True
        If Not Page.IsPostBack Then
            'Ba�lant� a��l�yor
            Try
                Baglanti.Open()
            Catch ex As Exception
                lblHata.Text = "Veritaban�na ba�lan�lamad�."
                Exit Sub
            End Try
            'Veritaban�ndan ayarlar al�n�yor
            Dim Ayarlar As New clsAyarlar
            Dim Yil As Short = Ayarlar.OgretimYili
            Dim Donem As Byte = Ayarlar.Donem
            Dim Anketaktif As Byte = Ayarlar.AnketAktif
            Dim i As Short
            Dim li As ListItem
            'Ba�lant� kapan�yor
            Baglantiyi_Kapat()
            'yil listesi dolduruluyor
            For i = Date.Now.Year - 5 To Date.Now.Year + 10
                li = New ListItem(i.ToString, i.ToString)
                If i = Yil Then
                    li.Selected = True
                End If
                ddlOgretimYili.Items.Add(li)
            Next
            'D�nem listesi ayarlan�yor
            ddlDonem.Items(Donem - 1).Selected = True
            'AnketAktif listesi ayarlan�yor
            ddlAnketDurumu.Items(1 - Anketaktif).Selected = True
        End If
    End Sub
    Private Sub Baglantiyi_Kapat()
        Try
            Baglanti.Close()
        Catch ex As Exception
            'bo�
        End Try
    End Sub
    Private Sub btnAyarlariKaydet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAyarlariKaydet.Click
        'Girilen verilerin ge�erlili�ine bak�l�yor
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        Dim AnketDurumu As Byte = 2
        If (ddlOgretimYili.SelectedIndex >= 0) And (ddlDonem.SelectedIndex >= 0) And (ddlAnketDurumu.SelectedIndex >= 0) Then
            If SayisalMi(ddlOgretimYili.SelectedValue, Date.Now.Year - 5, Date.Now.Year + 10) And _
            SayisalMi(ddlDonem.SelectedValue, 1, 3) And SayisalMi(ddlAnketDurumu.SelectedValue, 0, 1) Then
                Yil = CShort(ddlOgretimYili.SelectedValue)
                Donem = CByte(ddlDonem.SelectedValue)
                AnketDurumu = CByte(ddlAnketDurumu.SelectedValue)
            End If
        End If
        If Yil = 0 Or Donem = 0 Or AnketDurumu = 2 Then
            lblHata.Text = "Girilen bilgiler ge�erli de�il."
            Exit Sub
        End If
        'Ba�lant� a��l�yor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�."
            Exit Sub
        End Try
        'Ayarlar de�i�tiriliyor
        Dim Ayarlar As New clsAyarlar
        Ayarlar.AnketAktif = AnketDurumu
        Ayarlar.Donem = Donem
        Ayarlar.OgretimYili = Yil
        If Not Ayarlar.AyarlariKaydet() Then
            lblHata.Text = "Ayarlar kaydedilemedi. Veritaban�na ula��lam�yor."
        End If
        'Ba�lant� kapat�l�yor
        Baglantiyi_Kapat()
        Response.Redirect("default.aspx")
    End Sub
End Class
