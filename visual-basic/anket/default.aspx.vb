Imports System.Data.Odbc
Public Class WebForm1
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents imbGiris As System.Web.UI.WebControls.ImageButton
    Protected WithEvents txtKullaniciAdi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSifre As System.Web.UI.WebControls.TextBox

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
        Dim Sayfa As String = Request.QueryString("Sayfa")
        Select Case Sayfa
            Case "Cikis"
                Cikis()
        End Select
    End Sub
    Private Sub Cikis()
        Session("KullaniciTuru") = ""
        Session("OgrenciNo") = ""
        Session("Sifre") = ""
        Session("Numara") = ""
        Session("HocaNo") = ""
        Session("KullaniciAdi") = ""
        Session("YetkiDuzeyi") = ""
        Session("Anketaktif") = 0
    End Sub
    Private Sub imbGiris_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imbGiris.Click
        'Session de�i�kenleri temizleniyor
        Cikis()
        'Girilen bilgilerin ge�erlili�i denetleniyor
        Dim KullaniciAdi, Sifre As String
        Dim KullaniciVar As Boolean = False
        lblHata.Text = ""
        If (Not BilgiKontrolu(txtKullaniciAdi.Text, 20, False)) Or (Not BilgiKontrolu(txtSifre.Text, 20, False)) Then
            lblHata.Text = "Kullan�c� ad� ve/veya �ifre ge�ersiz.<br>"
            Exit Sub
        Else
            KullaniciAdi = txtKullaniciAdi.Text.Trim
            Sifre = txtSifre.Text.Trim
        End If
        Dim Sorgu As String = "SELECT * FROM Ogrenciler WHERE Numara='" & KullaniciAdi & "' AND Sifre='" & Sifre & "'"
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)

        'Ba�lant� a��l�yor
        Try
            Baglanti.Open()
        Catch ex As Exception
            'hata
        End Try
        '��renciler tablosunda kay�t olup olmamas�na bak�l�yor
        Dim Ogrenci As New clsOgrenciBilgileri
        If Ogrenci.VeriTabanindanAl(KullaniciAdi, Sifre) Then
            KullaniciVar = True
            Session("KullaniciTuru") = "Ogrenci"
            Session("OgrenciNo") = Ogrenci.OgrenciNo
            Session("Sifre") = Ogrenci.Sifre
            Session("Numara") = Ogrenci.Numara
        End If
        'E�er ��renciler tablosunda kay�t yoksa bu sefer hocalar tablosuna bak�l�yor..
        If Not KullaniciVar Then
            Dim Hoca As New clsHocaBilgileri
            If Hoca.VeriTabanindanAl(KullaniciAdi, Sifre) Then
                KullaniciVar = True
                Session("KullaniciTuru") = "Hoca"
                Session("HocaNo") = Hoca.HocaNo
                Session("Sifre") = Hoca.Sifre
                Session("KullaniciAdi") = Hoca.KullaniciAdi
                Session("YetkiDuzeyi") = Hoca.YetkiDuzeyi
            End If
        End If
        'E�er kullan�c� giri�i yap�lm��sa D�nem ve ��retim y�l� bilgileri de veritaban�ndan al�n�yor
        If KullaniciVar Then
            Dim Ayarlar As New clsAyarlar
            Session("OgretimYili") = Ayarlar.OgretimYili
            Session("Donem") = Ayarlar.Donem
            Session("AnketAktif") = Ayarlar.AnketAktif
            If (Not SayisalMi(Session("OgretimYili"), Date.Now.Year - 5, Date.Now.Year + 10)) _
            Or (Not SayisalMi(Session("Donem"), 1, 3)) _
            Or (Not SayisalMi(Session("AnketAktif"), 0, 1)) Then
                KullaniciVar = False
                lblHata.Text = "Veritaban�na ba�lant� kurulamad�.<br>"
                Cikis()
            End If
        End If
        'Ba�lant� kapan�yor
        Try
            Baglanti.Close()
        Catch ex As Exception
            'hata
        End Try
        'E�er kullan�c� giri�i yap�lm��sa ilgili sayfaya y�nlendir
        If KullaniciVar Then
            Select Case Session("KullaniciTuru")
                Case "Ogrenci"
                    Response.Redirect("ogrenci/default.aspx")
                Case "Hoca"
                    Response.Redirect("hoca/default.aspx")
            End Select
        Else ' kullan�c� bulunamad� ise..
            lblHata.Text += "Kullan�c� ad� ve/veya �ifre hatal�."
        End If
    End Sub
End Class
