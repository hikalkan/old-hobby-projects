Imports System.Data.Odbc
Public Class anket
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtAciklama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSoruSayisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSecenekSayisi As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlYil As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlDonem As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btn1Devam As System.Web.UI.WebControls.Button
    Protected WithEvents divAdim1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents btnAnketiTamamla As System.Web.UI.WebControls.Button

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
            Dim Sayfa As String
            Sayfa = Request.QueryString("Sayfa")
            Select Case Sayfa
                Case "Ekle"
                    AnketEkle()
                Case Else
                    ParametreHatasi()
            End Select
        Else
            Response.Redirect("../default.aspx")
        End If
    End Sub
    'A��l�� ayarlamalar�
    Private Sub OnAyarlamalar()
        divAdim1.Visible = False
        If Not Page.IsPostBack Then
            Dim eleman As ListItem
            Dim Yil As Short
            For Yil = Date.Now.Year - 5 To Date.Now.Year + 10
                eleman = New ListItem
                eleman.Text = Yil
                eleman.Value = Yil
                If Yil = Date.Now.Year Then eleman.Selected = True Else eleman.Selected = False
                ddlYil.Items.Add(eleman)
            Next
        End If
    End Sub
    'Hatal� Parametre giri�inde hata mesaj� verir
    Private Sub ParametreHatasi()
        lblHata.Text = "<p class='Girintili'>Parametre hatas�. L�tfen adres �ubu�una el ile parametre giri�i yapmay�n.</p>"
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
    'ANKET EKLEME
    'Ad�m 1 -> Anket bilgilerinin al�nmas�
    Private Sub AnketEkle()
        divAdim1.Visible = True
    End Sub
    Private Sub btn1Devam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1Devam.Click
        'Girilen bilgilerin do�rulu�u denetleniyor
        Dim Hata As Boolean = False
        lblHata.Text = ""
        If Not BilgiKontroluGenel(txtAciklama.Text, 255, True) Then
            lblHata.Text += "A��klama metninde uygun olmayan karakterler var.<br>"
            Hata = True
        End If
        If Not SayisalMi(txtSoruSayisi.Text, 1, 99) Then
            lblHata.Text += "Soru say�s� 1 ile 99 aras�nda olmal�d�r.<br>"
            Hata = True
        End If
        If Not SayisalMi(txtSecenekSayisi.Text, 1, 9) Then
            lblHata.Text += "Soru say�s� 1 ile 9 aras�nda olmal�d�r.<br>"
            Hata = True
        End If
        If Not SayisalMi(ddlYil.SelectedItem.Value, Date.Now.Year - 5, Date.Now.Year + 10) Then
            lblHata.Text += "Y�l de�eri " & Date.Now.Year - 5 & " ile " & Date.Now.Year + 10 & " aras�nda olmal�d�r.<br>"
            Hata = True
        End If
        If Not SayisalMi(ddlDonem.SelectedItem.Value, 1, 3) Then
            lblHata.Text += "D�nem se�memi�siniz.<br>"
            Hata = True
        End If
        If Hata Then
            lblHata.Text += "L�tfen yukar�daki hatalar� d�zeltin ve tekrar deneyin."
            Exit Sub
        End If
        'Girilen verilerin tamam� do�ru ise sorular� almak i�in form olu�turuluyor...
        Dim SoruSayisi As Byte = CByte(txtSoruSayisi.Text)
        Dim SecenekSayisi As Byte = CByte(txtSecenekSayisi.Text)
        Dim Yil As Short = CShort(ddlYil.SelectedItem.Value)
        Dim Donem As Byte = CByte(ddlDonem.SelectedItem.Value)
        Dim Kod As String = ""
        Dim SoruNo, SecenekNo As Byte
        Dim tb As TextBox
        Dim rb As RadioButton
        Kod += "<table width='90%' align='center' cellspacing='5' cellpadding='0'>" & _
        "<tr>" & _
        "<td width='100%'>"
        For SoruNo = 1 To SoruSayisi
            Kod += "<table width='100%' align='center' cellspacing='1' cellpadding='1' bgcolor='#000000'>" & Chr(13) & _
            "<tr>" & Chr(13) & _
            "<td width='100%' colspan=""2"" class='DuzYazi' align=""center"" bgcolor='#294963'><font color=""#ffffff""><B>SORU " & SoruNo & "</B></font></td>" & Chr(13) & _
            "</tr>" & Chr(13) & _
            "<tr>" & Chr(13) & _
            "<td class='DuzYazi' align='right' bgcolor='#919cb6' width='50%'><font color=""#ffffff"">Soru Metni:</font></td>" & Chr(13) & _
            "<td class='DuzYazi' align='left' bgcolor='#919cb6' width='50%'><font color=""#ffffff"">"
            phOrta.Controls.Add(New LiteralControl(Kod))
            Kod = ""
            'Soru Metni
            tb = New TextBox
            tb.ID = "txt" & SoruNo & "SoruMetni"
            tb.Width = New System.Web.UI.WebControls.Unit(200)
            tb.MaxLength = 255
            tb.Text = ""
            tb.Visible = True
            phOrta.Controls.Add(tb)
            Kod += "</font></td>" & Chr(13) & _
            "</tr>" & Chr(13) & _
            "<tr>" & Chr(13) & _
            "<td class='DuzYazi' align='right' bgcolor='#919cb6' width='50%'><font color=""#ffffff"">Soru T�r�:</font></td>" & Chr(13) & _
            "<td class='DuzYazi' align='left' bgcolor='#919cb6' width='50%'><font color=""#ffffff"">"
            phOrta.Controls.Add(New LiteralControl(Kod))
            Kod = ""
            'Soru T�r� se�imi
            rb = New RadioButton
            rb.ID = "rb" & SoruNo & "Genel"
            rb.GroupName = "rb" & SoruNo & "DersGenel"
            rb.Text = "Derse �zel"
            rb.Checked = True
            rb.Visible = True
            phOrta.Controls.Add(rb)
            phOrta.Controls.Add(New LiteralControl("<br>"))
            rb = New RadioButton
            rb.ID = "rb" & SoruNo & "Ders"
            rb.GroupName = "rb" & SoruNo & "DersGenel"
            rb.Text = "Genel"
            rb.Checked = False
            rb.Visible = True
            phOrta.Controls.Add(rb)
            Kod += "</font></td>" & Chr(13) & _
            "</tr>" & Chr(13) & _
            "<tr>" & Chr(13) & _
            "<td width='100%' colspan=""2"" class='DuzYazi' align=""center"" bgcolor='#919cb6'><font color=""#ffffff"">Se�enekler</font></td>" & Chr(13) & _
            "</tr>"
            For SecenekNo = 1 To SecenekSayisi
                Kod += "<tr>" & Chr(13) & _
                "<td class='DuzYazi' align='right' bgcolor='#919cb6' width='50%'><font color=""#ffffff"">" & SecenekNo & ". Se�enek Metni / Say�sal De�eri:</font></td>" & Chr(13) & _
                "<td class='DuzYazi' align='left' bgcolor='#919cb6' width='50%'><font color=""#ffffff"">"
                phOrta.Controls.Add(New LiteralControl(Kod))
                Kod = ""
                'Se�enek Metni ve Say�sal De�eri
                tb = New TextBox
                tb.ID = "txt" & SoruNo & "_" & SecenekNo & "SecenekMetni"
                tb.Width = New System.Web.UI.WebControls.Unit(200)
                tb.MaxLength = 50
                tb.Text = ""
                tb.Visible = True
                phOrta.Controls.Add(tb)
                phOrta.Controls.Add(New LiteralControl(" / "))
                tb = New TextBox
                tb.ID = "txt" & SoruNo & "_" & SecenekNo & "SecenekSayisalDeger"
                tb.Width = New System.Web.UI.WebControls.Unit(30)
                tb.MaxLength = 3
                tb.Text = ""
                tb.Visible = True
                phOrta.Controls.Add(tb)
                Kod += "</font></td>" & Chr(13) & _
                "</tr>"
            Next
            Kod += "</table>"
        Next
        Kod += "</td>" & Chr(13) & _
        "</tr>" & Chr(13) & _
        "</table>"
        phOrta.Controls.Add(New LiteralControl(Kod))
        btnAnketiTamamla.Visible = True
    End Sub
    Private Sub btnAnketiTamamla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnketiTamamla.Click
        'Girilen bilgilerin do�rulu�u denetleniyor
        Dim Hata As Boolean = False
        lblHata.Text = ""
        'Anket bilgileri kontrol ediliyor
        If Not BilgiKontroluGenel(txtAciklama.Text, 255, True) Then
            lblHata.Text += "A��klama metninde uygun olmayan karakterler var.<br>"
            Hata = True
        End If
        If Not SayisalMi(txtSoruSayisi.Text, 1, 99) Then
            lblHata.Text += "Soru say�s� 1 ile 99 aras�nda olmal�d�r.<br>"
            Hata = True
        End If
        If Not SayisalMi(txtSecenekSayisi.Text, 1, 9) Then
            lblHata.Text += "Soru say�s� 1 ile 9 aras�nda olmal�d�r.<br>"
            Hata = True
        End If
        If Not SayisalMi(ddlYil.SelectedItem.Value, Date.Now.Year - 5, Date.Now.Year + 10) Then
            lblHata.Text += "Y�l de�eri " & Date.Now.Year - 5 & " ile " & Date.Now.Year + 10 & " aras�nda olmal�d�r.<br>"
            Hata = True
        End If
        If Not SayisalMi(ddlDonem.SelectedItem.Value, 1, 3) Then
            lblHata.Text += "D�nem se�memi�siniz.<br>"
            Hata = True
        End If
        If Hata Then
            lblHata.Text += "L�tfen yukar�daki hatalar� d�zeltin ve tekrar deneyin."
            Exit Sub
        End If
        'do�ruysa de�i�kenlere atan�yor
        Dim SoruSayisi As Byte = CByte(txtSoruSayisi.Text)
        Dim SecenekSayisi As Byte = CByte(txtSecenekSayisi.Text)
        Dim Yil As Short = CShort(ddlYil.SelectedItem.Value)
        Dim Donem As Byte = CByte(ddlDonem.SelectedItem.Value)
        Dim Aciklama As String = txtAciklama.Text.Trim.Replace("'", "''")
        Dim SoruNo As Byte
        Dim KontrolAdi As String
        Dim SecenekNo As Byte
        'Soru bilgileri kontrol ediliyor
        Dim SS As Byte = 0
        Try
            For SoruNo = 1 To SoruSayisi
                'Soru Metni
                KontrolAdi = "txt" & SoruNo.ToString & "SoruMetni"
                If Not BilgiKontroluGenel(Request.Form(KontrolAdi), 255, False) Then
                    Hata = True
                    Exit For
                End If
                'Se�enekler
                SS = 0 ' Se�enek Say�s�
                For SecenekNo = 1 To SecenekSayisi
                    'Metin
                    KontrolAdi = "txt" & SoruNo & "_" & SecenekNo & "SecenekMetni"
                    If Request.Form("txt" & SoruNo & "_" & SecenekNo & "SecenekMetni") <> "" _
                    And Request.Form("txt" & SoruNo & "_" & SecenekNo & "SecenekSayisalDeger") <> "" _
                    Then
                        SS += 1
                    End If
                    If Not BilgiKontroluGenel(Request.Form(KontrolAdi), 255, True) Then
                        Hata = True
                        Exit For
                    End If
                    'Say�sal de�er
                    KontrolAdi = "txt" & SoruNo & "_" & SecenekNo & "SecenekSayisalDeger"
                    If Request.Form(KontrolAdi) <> "" Then
                        If Not SayisalMi(Request.Form(KontrolAdi), 0, 100) Then
                            Hata = True
                            Exit For
                        End If
                    End If
                Next
                'E�er bu soru i�in s�f�r adet se�enek girilmi�se..
                If SS = 0 Then
                    Hata = True
                    Exit For
                End If
                If Hata Then Exit For
            Next
        Catch ex As Exception
            Hata = True
        End Try
        If Hata Then
            lblHata.Text += "Soru bilgilerinde uygun olmayan karakterler var ya da hatal� girilmi�."
            Exit Sub
        End If
        'Veritaban� ba�lant�s� tan�mlan�yor ve a��l�yor
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�."
            Exit Sub
        End Try
        Komut.Connection = Baglanti
        'Ayn� y�l ve d�nem i�in ba�ka anket olup olmad���na bak�l�yor
        Sorgu = "SELECT AnketNo FROM Anketler WHERE Yil=" & Yil & " AND Donem=" & Donem
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                lblHata.Text = Yil & " y�l�n�n " & Donem & ". d�nemi i�in zaten anket var. Ayn� d�nem i�in en fazla 1 anket tan�mlanabilir."
                Hata = True
            End If
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�!"
            Hata = True
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If Hata Then ' hata varsa prosed�r� sonland�r
            Try
                If Not (Komut Is Nothing) Then Komut.Dispose()
                Baglanti.Close()
            Catch ex As Exception
                'bo�
            End Try
            Exit Sub
        End If
        'Hi�bir hata yoksa Anket bilgileri veritaban�na yaz�l�yor
        Try
            '(1) Anketler tablosuna anket kaydediliyor
            Sorgu = "INSERT INTO Anketler Values(NULL,'" & Aciklama & "'," & Yil & "," & Donem & ")"
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            '(2) Kaydedilen anketin AnketNo'su al�n�yor
            Dim AnketNo As Long = 0
            Sorgu = "SELECT LAST_INSERT_ID() as SonEklenen"
            Komut.CommandText = Sorgu
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                AnketNo = Okuyucu("SonEklenen")
            End If
            Okuyucu.Close()
            '(3) Anket Sorular� Kaydediliyor
            Dim AnketSoruNo As Long = 0
            Dim SoruMetni As String = ""
            Dim SoruTuru As Byte = 0
            Dim SecenekMetin As String = ""
            Dim SecenekSayisalDeger As Byte = 0
            Dim KA1 As String = ""
            Dim KA2 As String = ""
            For SoruNo = 1 To SoruSayisi
                KA1 = "txt" & SoruNo.ToString & "SoruMetni"
                SoruMetni = Request.Form(KA1)
                KA1 = "rb" & SoruNo & "DersGenel"
                KA2 = "rb" & SoruNo & "Genel"
                If Request.Form(KA1) = KA2 Then
                    SoruTuru = 2
                Else
                    SoruTuru = 1
                End If
                '(3.1) Soru bilgileri kaydediliyor
                Sorgu = "INSERT INTO AnketSorulari VALUES(NULL," & AnketNo & "," & _
                        SoruNo & "," & SoruTuru & ",'" & SoruMetni.Replace("'", "''") & "')"
                Komut.CommandText = Sorgu
                Komut.ExecuteNonQuery()
                Sorgu = "SELECT LAST_INSERT_ID() as SonEklenen"
                Komut.CommandText = Sorgu
                '(3.2) Eklenen sorunun AnketSoruNo'su al�n�yor
                Okuyucu = Komut.ExecuteReader
                If Okuyucu.Read Then
                    AnketSoruNo = Okuyucu("SonEklenen")
                End If
                Okuyucu.Close()
                '(3.3) Bu Sorunun se�enekleri kaydediliyor
                For SecenekNo = 1 To SecenekSayisi
                    KA1 = "txt" & SoruNo & "_" & SecenekNo & "SecenekMetni"
                    If Request.Form(KA1) = "" Then
                        Exit For
                    End If
                    SecenekMetin = Request.Form(KA1)
                    KA1 = "txt" & SoruNo & "_" & SecenekNo & "SecenekSayisalDeger"
                    If Request.Form(KA1) = "" Then
                        Exit For
                    End If
                    SecenekSayisalDeger = Request.Form(KA1)
                    Sorgu = "INSERT INTO AnketSoruSecenekleri VALUES(NULL," & _
                            AnketNo & "," & AnketSoruNo & "," & SecenekSayisalDeger & ",'" & _
                            SecenekMetin & "')"
                    Komut.CommandText = Sorgu
                    Komut.ExecuteNonQuery()
                Next SecenekNo
            Next SoruNo
        Catch ex As Exception
            lblHata.Text = "D�KKAT!!! Anket Bilgileri Veritaban�na Kaydedilemedi ya da Hatal� Kaydedildi!"
            Hata = True
        End Try
        Baglanti.Close()
        If Not Hata Then
            Response.Redirect("default.aspx")
        Else
            btnAnketiTamamla.Visible = False
        End If
    End Sub
End Class
