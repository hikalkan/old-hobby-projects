Imports System.Data.Odbc
Public Class anket1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents btnAnketTamam As System.Web.UI.WebControls.Button
    Protected WithEvents btnAnketTamam_Genel As System.Web.UI.WebControls.Button

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
        'Giri� yap�lmam��sa ana sayfaya y�nlendiriliyor
        If Not SayisalMi(Session("OgrenciNo"), 1, 1000000000) Then
            Response.Redirect("../default.aspx")
        End If
        btnAnketTamam.Visible = False
        btnAnketTamam_Genel.Visible = False
        Dim Sayfa As String = Request.QueryString("Sayfa")
        Dim Tur As String = Request.QueryString("Tur")
        If BilgiKontrolu(Sayfa, 50, False) Then
            Select Case Sayfa
                Case "AnketDoldur"
                    Select Case Tur
                        Case "Genel"
                            AnketiHazirla_Genel()
                        Case Else
                            AnketiHazirla()
                    End Select
            End Select
        Else
            lblHata.Text = "Adres �ubu�una do�rudan adres yazarak bu sayfaya girmeye �al��may�n."
        End If
    End Sub
    '��rencinin doldurmas� i�in anket formunu haz�rlar (Ders i�in)
    Private Sub AnketiHazirla()
        '�nce Anket doldurma zaman� olup olmad���na bak�l�yor
        If Session("AnketAktif") <> 1 Then
            lblHata.Text = "�u anda anket dolduramazs�n�z!<br>"
            Exit Sub
        End If
        'Yoksa devam et..
        Dim OgrenciNo As Long = 0
        Dim DersNo As Long = 0
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        Dim DersAd As String
        If SayisalMi(Session("OgrenciNo"), 1, 1000000000) Then
            OgrenciNo = CInt(Session("OgrenciNo"))
        End If
        If SayisalMi(Request.QueryString("DersNo"), 1, 1000000000) Then
            DersNo = CInt(Request.QueryString("DersNo"))
        End If
        If SayisalMi(Session("OgretimYili"), 2000, 3000) Then
            Yil = CShort(Session("OgretimYili"))
        End If
        If SayisalMi(Session("Donem"), 1, 3) Then
            Donem = CByte(Session("Donem"))
        End If
        If OgrenciNo = 0 Or DersNo = 0 Or Yil = 0 Or Donem = 0 Then
            lblHata.Text = "�ye giri�i yapmam��s�n�z ya da bir ders se�memi�siniz."
            Exit Sub
        End If
        '�ye giri�i yap�lm��sa, anketi doldurup doldurmad���na bak�l�yor
        Dim Sorgu As String = "SELECT DersOgrenciIliskileri.AnketDoldurulduMu,Dersler.Ad AS DersAd" & _
        " FROM DersOgrenciIliskileri,Dersler WHERE" & _
        " DersOgrenciIliskileri.DersNo=Dersler.DersNo AND" & _
        " DersOgrenciIliskileri.DersNo=" & DersNo & _
        " AND DersOgrenciIliskileri.OgrenciNo=" & OgrenciNo & _
        " AND Dersler.Yil=" & Yil & _
        " AND Dersler.Donem=" & Donem
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim AnketDoldurulduMu As Byte = 0
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�!<br>"
            Exit Sub
        End Try
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                AnketDoldurulduMu = Okuyucu("AnketDoldurulduMu")
                DersAd = Okuyucu("DersAd")
            Else
                AnketDoldurulduMu = 2
            End If
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
            AnketDoldurulduMu = 3
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If AnketDoldurulduMu = 1 Then
            lblHata.Text = "Bu anketi doldurdunuz."
            Baglanti.Close()
            Exit Sub
        End If
        If AnketDoldurulduMu = 2 Then
            lblHata.Text = "Bu dersi alm�yorsunuz ya da �u anda dolduramazs�n�z.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        If AnketDoldurulduMu = 3 Then
            lblHata.Text = "Veritaban�ndan anket bilgileri al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Hata yok. Anket haz�rlan�yor...
        Dim Aciklama As String
        Dim AnketNo As Long = 0
        Dim Sorular As New DataTable
        Dim Secenekler As DataTable
        Dim Secenek As DataRow
        Dim rbl As RadioButtonList
        Dim rb As RadioButton
        Dim li As ListItem
        Dim Soru As DataRow
        Dim Hata As Boolean = False
        'Anket bilgisi al�n�yor
        Sorgu = "SELECT AnketNo,Aciklama FROM Anketler WHERE Yil=" & Yil & " AND Donem=" & Donem
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                AnketNo = Okuyucu("AnketNo")
                Aciklama = Okuyucu("Aciklama")
            Else
                Hata = True
            End If
        Catch ex As Exception
            Hata = True
        Finally
            Okuyucu.Close()
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Anketi sorular� al�n�yor
        Sorgu = "SELECT AnketSoruNo,SoruSirasi,Metin FROM AnketSorulari WHERE AnketNo=" & AnketNo & " AND SoruTuru=2 ORDER BY SoruSirasi"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Sorular)
        Catch ex As Exception
            Hata = True
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        Dim Kod As String
        Dim SatirNo As Byte = 0
        Kod = "<Table align='Center' width='910px' cellpadding='0' cellspacing='5' border='0'>"
        Kod += "<tr><td><h2>" & DersAd & "<h2></td></tr>"
        Kod += "<tr><td><p Class='Girintili'>" & Aciklama & "</p></td></tr>"
        phOrta.Controls.Add(New LiteralControl(Kod))
        For Each Soru In Sorular.Rows
            SatirNo += 1
            Kod = "<tr><td><table border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: collapse"" bordercolor=""#111111"" id=""AutoNumber1"" width=""900px"">" & Chr(13) & _
            "  <tr>" & Chr(13) & _
            "    <td width=""4px"" height=""24"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_01.gif"" width=""4"" height=""24""></td>" & Chr(13) & _
            "    <td width=""29px"" height=""24"" background=""../grafik/tablo1/anket_02.gif"" align=""center"">" & Chr(13) & _
            "    <p class=""DuzYazi""><font color=""#FFFFFF""><b>" & Chr(13) & _
            SatirNo & Chr(13) & _
            "    </b></font></p></td>" & Chr(13) & _
            "    <td width=""4px"" height=""24""><img border=""0"" src=""../grafik/tablo1/anket_04.gif"" width=""4"" height=""24""></td>" & Chr(13) & _
            "    <td width=""850px"" height=""24"" background=""../grafik/tablo1/anket_05.gif"">" & Chr(13) & _
            "    <p class=""DuzYazi""><font color=""#000000""><b>" & Chr(13) & _
            Soru("Metin") & Chr(13) & _
            "    </b></font></p></td>" & Chr(13) & _
            "    <td width=""13px"" height=""24"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_07.gif"" width=""13"" height=""24""></td>" & Chr(13) & _
            "  </tr>" & Chr(13) & _
            "  <tr>" & Chr(13) & _
            "    <td width=""4px"" background=""../grafik/tablo1/anket_08.gif""></td>" & Chr(13) & _
            "    <td width=""883px"" colspan=""3"" bgcolor=""#82A2CA""><p class=""DuzYazi""><font color=""#000000"">" & Chr(13)
            phOrta.Controls.Add(New LiteralControl(Kod))
            'Soru ��klar�na g�re se�enekler olu�turuluyor
            Secenekler = New DataTable
            Sorgu = "SELECT AnketSoruSecenekNo,SayisalDeger,Metin FROM AnketSoruSecenekleri WHERE AnketSoruNo=" & Soru("AnketSoruNo") & " ORDER BY SayisalDeger"
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(Secenekler)
            Catch ex As Exception
                Hata = True
            End Try
            If Hata Then
                lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
                Baglanti.Close()
                Exit Sub
            End If
            rbl = New RadioButtonList
            rbl.ID = "rbSoru" & Soru("AnketSoruNo")
            rbl.Font.Name = "Verdana"
            rbl.Font.Size = New Web.UI.WebControls.FontUnit("12px")
            rbl.CellPadding = 0
            rbl.CellSpacing = 0
            rbl.RepeatDirection = RepeatDirection.Horizontal
            rbl.ToolTip = Soru("Metin")
            For Each Secenek In Secenekler.Rows
                li = New ListItem
                li.Text = Secenek("Metin")
                li.Value = Secenek("AnketSoruSecenekNo")
                rbl.Items.Add(li)
            Next
            phOrta.Controls.Add(rbl)
            Kod = "    </font></p></td>" & Chr(13) & _
            "    <td width=""13px"" background=""../grafik/tablo1/anket_11.gif""></td>" & Chr(13) & _
            "  </tr>" & Chr(13) & _
            "  <tr>" & Chr(13) & _
            "    <td width=""883px"" colspan=""4"" height=""12px"" background=""../grafik/tablo1/anket_15.gif"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_14.gif"" width=""11"" height=""12""></td>" & Chr(13) & _
            "    <td width=""13px"" height=""12px"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_17.gif"" width=""13"" height=""12""></td>" & Chr(13) & _
            "  </tr>" & Chr(13) & _
            "</table></td></tr>"
            phOrta.Controls.Add(New LiteralControl(Kod))
        Next
        'Yorum alan� olu�turuluyor...
        Kod = "<tr><td><table border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: collapse"" bordercolor=""#111111"" id=""AutoNumber1"" width=""900px"">" & Chr(13) & _
        "  <tr>" & Chr(13) & _
        "    <td width=""4px"" height=""24"">" & Chr(13) & _
        "    <img border=""0"" src=""../grafik/tablo1/anket_01.gif"" width=""4"" height=""24""></td>" & Chr(13) & _
        "    <td width=""29px"" height=""24"" background=""../grafik/tablo1/anket_02.gif"" align=""center"">" & Chr(13) & _
        "    <p class=""DuzYazi""><font color=""#FFFFFF""><b>" & Chr(13) & _
        (SatirNo + 1).ToString & Chr(13) & _
        "    </b></font></p></td>" & Chr(13) & _
        "    <td width=""4px"" height=""24""><img border=""0"" src=""../grafik/tablo1/anket_04.gif"" width=""4"" height=""24""></td>" & Chr(13) & _
        "    <td width=""850px"" height=""24"" background=""../grafik/tablo1/anket_05.gif"">" & Chr(13) & _
        "    <p class=""DuzYazi""><font color=""#000000""><b>" & Chr(13) & _
        "A�a��ya bu ders ya da Hoca hakk�nda g�r��, dilek ya da �ikayetlerinizi yazabilirisiniz." & Chr(13) & _
        "    </b></font></p></td>" & Chr(13) & _
        "    <td width=""13px"" height=""24"">" & Chr(13) & _
        "    <img border=""0"" src=""../grafik/tablo1/anket_07.gif"" width=""13"" height=""24""></td>" & Chr(13) & _
        "  </tr>" & Chr(13) & _
        "  <tr>" & Chr(13) & _
        "    <td width=""4px"" background=""../grafik/tablo1/anket_08.gif""></td>" & Chr(13) & _
        "    <td width=""883px"" colspan=""3"" bgcolor=""#82A2CA""><p class=""DuzYazi"" Align=""Center""><font color=""#000000"">" & Chr(13)
        phOrta.Controls.Add(New LiteralControl(Kod))
        'textbox olu�turuluyor
        Dim txtYorum As New TextBox
        txtYorum.TextMode = TextBoxMode.MultiLine
        txtYorum.Width = New UI.WebControls.Unit("800px")
        txtYorum.Height = New UI.WebControls.Unit("200px")
        txtYorum.ID = "txtYorum_Alani"
        txtYorum.MaxLength = 2000
        phOrta.Controls.Add(txtYorum)
        Kod = "    </font></p></td>" & Chr(13) & _
        "    <td width=""13px"" background=""../grafik/tablo1/anket_11.gif""></td>" & Chr(13) & _
        "  </tr>" & Chr(13) & _
        "  <tr>" & Chr(13) & _
        "    <td width=""883px"" colspan=""4"" height=""12px"" background=""../grafik/tablo1/anket_15.gif"">" & Chr(13) & _
        "    <img border=""0"" src=""../grafik/tablo1/anket_14.gif"" width=""11"" height=""12""></td>" & Chr(13) & _
        "    <td width=""13px"" height=""12px"">" & Chr(13) & _
        "    <img border=""0"" src=""../grafik/tablo1/anket_17.gif"" width=""13"" height=""12""></td>" & Chr(13) & _
        "  </tr>" & Chr(13) & _
        "</table></td></tr>"
        phOrta.Controls.Add(New LiteralControl(Kod))
        phOrta.Controls.Add(New LiteralControl("</table>"))
        btnAnketTamam.Visible = True
        Try
            Baglanti.Close()
        Catch ex As Exception
            'hata
        End Try
    End Sub
    '��rencinin doldurmas� i�in anket formunu haz�rlar (Genel Anket)
    Private Sub AnketiHazirla_Genel()
        '�nce Anket doldurma zaman� olup olmad���na bak�l�yor
        If Session("AnketAktif") <> 1 Then
            lblHata.Text = "�u anda anket dolduramazs�n�z!<br>"
            Exit Sub
        End If
        'Yoksa devam et..
        Dim OgrenciNo As Long = 0
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        If SayisalMi(Session("OgrenciNo"), 1, 1000000000) Then
            OgrenciNo = CInt(Session("OgrenciNo"))
        End If
        If SayisalMi(Session("OgretimYili"), 2000, 3000) Then
            Yil = CShort(Session("OgretimYili"))
        End If
        If SayisalMi(Session("Donem"), 1, 3) Then
            Donem = CByte(Session("Donem"))
        End If
        If OgrenciNo = 0 Or Yil = 0 Or Donem = 0 Then
            lblHata.Text = "�ye giri�i yapmam��s�n�z."
            Exit Sub
        End If
        '�ye giri�i yap�lm��sa, anketi doldurup doldurmad���na bak�l�yor
        Dim Sorgu As String
        Sorgu = "SELECT GADNo FROM GenelAnketDolduranlar " & _
        "WHERE OgrenciNo=" & OgrenciNo & _
        " AND Yil=" & Yil & " AND Donem=" & Donem
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)

        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim AnketDoldurulduMu As Byte = 0
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�!<br>"
            Exit Sub
        End Try
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                AnketDoldurulduMu = 1
            Else
                AnketDoldurulduMu = 0
            End If
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
            AnketDoldurulduMu = 2
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If AnketDoldurulduMu = 1 Then
            lblHata.Text = "Bu anketi doldurdunuz."
            Baglanti.Close()
            Exit Sub
        End If
        If AnketDoldurulduMu = 2 Then
            lblHata.Text = "Veritaban�ndan anket bilgileri al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Hata yok. Anket haz�rlan�yor...
        Dim Aciklama As String
        Dim AnketNo As Long = 0
        Dim Sorular As New DataTable
        Dim Secenekler As DataTable
        Dim Secenek As DataRow
        Dim rbl As RadioButtonList
        Dim rb As RadioButton
        Dim li As ListItem
        Dim Soru As DataRow
        Dim Hata As Boolean = False
        'Anket bilgisi al�n�yor
        Sorgu = "SELECT AnketNo,Aciklama FROM Anketler WHERE Yil=" & Yil & " AND Donem=" & Donem
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                AnketNo = Okuyucu("AnketNo")
                Aciklama = Okuyucu("Aciklama")
            Else
                Hata = True
            End If
        Catch ex As Exception
            Hata = True
        Finally
            Okuyucu.Close()
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Anketi sorular� al�n�yor
        Sorgu = "SELECT AnketSoruNo,SoruSirasi,Metin FROM AnketSorulari WHERE AnketNo=" & AnketNo & " AND SoruTuru=1 ORDER BY SoruSirasi"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Sorular)
        Catch ex As Exception
            Hata = True
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        Dim Kod As String
        Dim SatirNo As Byte = 0
        Kod = "<Table align='Center' width='910px' cellpadding='0' cellspacing='5' border='0'>"
        Kod += "<tr><td><h2>Genel Anket<h2></td></tr>"
        Kod += "<tr><td><p Class='Girintili'>" & Aciklama & "</p></td></tr>"
        phOrta.Controls.Add(New LiteralControl(Kod))
        For Each Soru In Sorular.Rows
            SatirNo += 1
            Kod = "<tr><td><table border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: collapse"" bordercolor=""#111111"" id=""AutoNumber1"" width=""900px"">" & Chr(13) & _
            "  <tr>" & Chr(13) & _
            "    <td width=""4px"" height=""24"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_01.gif"" width=""4"" height=""24""></td>" & Chr(13) & _
            "    <td width=""29px"" height=""24"" background=""../grafik/tablo1/anket_02.gif"" align=""center"">" & Chr(13) & _
            "    <p class=""DuzYazi""><font color=""#FFFFFF""><b>" & Chr(13) & _
            SatirNo & Chr(13) & _
            "    </b></font></p></td>" & Chr(13) & _
            "    <td width=""4px"" height=""24""><img border=""0"" src=""../grafik/tablo1/anket_04.gif"" width=""4"" height=""24""></td>" & Chr(13) & _
            "    <td width=""850px"" height=""24"" background=""../grafik/tablo1/anket_05.gif"">" & Chr(13) & _
            "    <p class=""DuzYazi""><font color=""#000000""><b>" & Chr(13) & _
            Soru("Metin") & Chr(13) & _
            "    </b></font></p></td>" & Chr(13) & _
            "    <td width=""13px"" height=""24"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_07.gif"" width=""13"" height=""24""></td>" & Chr(13) & _
            "  </tr>" & Chr(13) & _
            "  <tr>" & Chr(13) & _
            "    <td width=""4px"" background=""../grafik/tablo1/anket_08.gif""></td>" & Chr(13) & _
            "    <td width=""883px"" colspan=""3"" bgcolor=""#82A2CA""><p class=""DuzYazi""><font color=""#000000"">" & Chr(13)
            phOrta.Controls.Add(New LiteralControl(Kod))
            'Soru ��klar�na g�re se�enekler olu�turuluyor
            Secenekler = New DataTable
            Sorgu = "SELECT AnketSoruSecenekNo,SayisalDeger,Metin FROM AnketSoruSecenekleri WHERE AnketSoruNo=" & Soru("AnketSoruNo") & " ORDER BY SayisalDeger"
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(Secenekler)
            Catch ex As Exception
                Hata = True
            End Try
            If Hata Then
                lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
                Baglanti.Close()
                Exit Sub
            End If
            rbl = New RadioButtonList
            rbl.ID = "rbSoru" & Soru("AnketSoruNo")
            rbl.Font.Name = "Verdana"
            rbl.Font.Size = New Web.UI.WebControls.FontUnit("12px")
            rbl.CellPadding = 0
            rbl.CellSpacing = 0
            rbl.RepeatDirection = RepeatDirection.Horizontal
            rbl.ToolTip = Soru("Metin")
            For Each Secenek In Secenekler.Rows
                li = New ListItem
                li.Text = Secenek("Metin")
                li.Value = Secenek("AnketSoruSecenekNo")
                rbl.Items.Add(li)
            Next
            phOrta.Controls.Add(rbl)
            Kod = "    </font></p></td>" & Chr(13) & _
            "    <td width=""13px"" background=""../grafik/tablo1/anket_11.gif""></td>" & Chr(13) & _
            "  </tr>" & Chr(13) & _
            "  <tr>" & Chr(13) & _
            "    <td width=""883px"" colspan=""4"" height=""12px"" background=""../grafik/tablo1/anket_15.gif"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_14.gif"" width=""11"" height=""12""></td>" & Chr(13) & _
            "    <td width=""13px"" height=""12px"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_17.gif"" width=""13"" height=""12""></td>" & Chr(13) & _
            "  </tr>" & Chr(13) & _
            "</table></td></tr>"
            phOrta.Controls.Add(New LiteralControl(Kod))
        Next
        'Yorum alan� olu�turuluyor...
        Kod = "<tr><td><table border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse: collapse"" bordercolor=""#111111"" id=""AutoNumber1"" width=""900px"">" & Chr(13) & _
        "  <tr>" & Chr(13) & _
        "    <td width=""4px"" height=""24"">" & Chr(13) & _
        "    <img border=""0"" src=""../grafik/tablo1/anket_01.gif"" width=""4"" height=""24""></td>" & Chr(13) & _
        "    <td width=""29px"" height=""24"" background=""../grafik/tablo1/anket_02.gif"" align=""center"">" & Chr(13) & _
        "    <p class=""DuzYazi""><font color=""#FFFFFF""><b>" & Chr(13) & _
        (SatirNo + 1).ToString & Chr(13) & _
        "    </b></font></p></td>" & Chr(13) & _
        "    <td width=""4px"" height=""24""><img border=""0"" src=""../grafik/tablo1/anket_04.gif"" width=""4"" height=""24""></td>" & Chr(13) & _
        "    <td width=""850px"" height=""24"" background=""../grafik/tablo1/anket_05.gif"">" & Chr(13) & _
        "    <p class=""DuzYazi""><font color=""#000000""><b>" & Chr(13) & _
        "A�a��ya b�l�m�m�z ve okulumuz hakk�ndaki genel g�r��lerinizi ve �nerilerinizi yazabilirsiniz." & Chr(13) & _
        "    </b></font></p></td>" & Chr(13) & _
        "    <td width=""13px"" height=""24"">" & Chr(13) & _
        "    <img border=""0"" src=""../grafik/tablo1/anket_07.gif"" width=""13"" height=""24""></td>" & Chr(13) & _
        "  </tr>" & Chr(13) & _
        "  <tr>" & Chr(13) & _
        "    <td width=""4px"" background=""../grafik/tablo1/anket_08.gif""></td>" & Chr(13) & _
        "    <td width=""883px"" colspan=""3"" bgcolor=""#82A2CA""><p class=""DuzYazi"" Align=""Center""><font color=""#000000"">" & Chr(13)
        phOrta.Controls.Add(New LiteralControl(Kod))
        'textbox olu�turuluyor
        Dim txtYorum As New TextBox
        txtYorum.TextMode = TextBoxMode.MultiLine
        txtYorum.Width = New UI.WebControls.Unit("800px")
        txtYorum.Height = New UI.WebControls.Unit("200px")
        txtYorum.ID = "txtYorum_Alani"
        txtYorum.MaxLength = 2000
        phOrta.Controls.Add(txtYorum)
        Kod = "    </font></p></td>" & Chr(13) & _
        "    <td width=""13px"" background=""../grafik/tablo1/anket_11.gif""></td>" & Chr(13) & _
        "  </tr>" & Chr(13) & _
        "  <tr>" & Chr(13) & _
        "    <td width=""883px"" colspan=""4"" height=""12px"" background=""../grafik/tablo1/anket_15.gif"">" & Chr(13) & _
        "    <img border=""0"" src=""../grafik/tablo1/anket_14.gif"" width=""11"" height=""12""></td>" & Chr(13) & _
        "    <td width=""13px"" height=""12px"">" & Chr(13) & _
        "    <img border=""0"" src=""../grafik/tablo1/anket_17.gif"" width=""13"" height=""12""></td>" & Chr(13) & _
        "  </tr>" & Chr(13) & _
        "</table></td></tr>"
        phOrta.Controls.Add(New LiteralControl(Kod))
        phOrta.Controls.Add(New LiteralControl("</table>"))
        btnAnketTamam_Genel.Visible = True
        Try
            Baglanti.Close()
        Catch ex As Exception
            'hata
        End Try
    End Sub
    'Doldurulan anket veritaban�na kay�t ediliyor (Ders i�in)
    Private Sub btnAnketTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnketTamam.Click
        lblHata.Text = ""
        Dim OgrenciNo As Long = 0
        Dim DersNo As Long = 0
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        Dim DersAd As String
        If SayisalMi(Session("OgrenciNo"), 1, 1000000000) Then
            OgrenciNo = CInt(Session("OgrenciNo"))
        End If
        If SayisalMi(Request.QueryString("DersNo"), 1, 1000000000) Then
            DersNo = CInt(Request.QueryString("DersNo"))
        End If
        If SayisalMi(Session("OgretimYili"), 2000, 3000) Then
            Yil = CShort(Session("OgretimYili"))
        End If
        If SayisalMi(Session("Donem"), 1, 3) Then
            Donem = CByte(Session("Donem"))
        End If
        If OgrenciNo = 0 Or DersNo = 0 Or Yil = 0 Or Donem = 0 Then
            lblHata.Text = "�ye giri�i yapmam��s�n�z ya da bir ders se�memi�siniz."
            Exit Sub
        End If
        '�ye giri�i yap�lm��sa, anketi doldurup doldurmad���na bak�l�yor
        Dim Sorgu As String = "SELECT DersOgrenciIliskileri.DersOgrenciIliskiNo, DersOgrenciIliskileri.AnketDoldurulduMu" & _
        " FROM DersOgrenciIliskileri,Dersler WHERE" & _
        " DersOgrenciIliskileri.DersNo=Dersler.DersNo AND" & _
        " DersOgrenciIliskileri.DersNo=" & DersNo & _
        " AND DersOgrenciIliskileri.OgrenciNo=" & OgrenciNo & _
        " AND Dersler.Yil=" & Yil & _
        " AND Dersler.Donem=" & Donem
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim AnketDoldurulduMu As Byte = 0
        Dim DersOgrenciIliskiNo As Long = 0
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�!<br>"
            Exit Sub
        End Try
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                AnketDoldurulduMu = Okuyucu("AnketDoldurulduMu")
                DersOgrenciIliskiNo = Okuyucu("DersOgrenciIliskiNo")
            Else
                AnketDoldurulduMu = 2
            End If
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
            AnketDoldurulduMu = 3
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If AnketDoldurulduMu = 1 Then
            lblHata.Text = "Bu anketi doldurdunuz."
            Baglanti.Close()
            Exit Sub
        End If
        If AnketDoldurulduMu = 2 Then
            lblHata.Text = "Bu dersi alm�yorsunuz ya da �u anda dolduramazs�n�z.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        If AnketDoldurulduMu = 3 Then
            lblHata.Text = "Veritaban�ndan anket bilgileri al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Anketi doldurabilir.. �imdi anketi do�ru doldurmu� mu diye bak�l�yor
        'Anket numaras� al�n�yor
        Dim AnketNo As Long
        Dim Hata As Boolean = False
        Sorgu = "SELECT AnketNo FROM Anketler WHERE Yil=" & Yil & " AND Donem=" & Donem
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                AnketNo = Okuyucu("AnketNo")
            Else
                Hata = True
            End If
        Catch ex As Exception
            Hata = True
        Finally
            Okuyucu.Close()
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Sorular al�n�yor
        Dim Sorular As New DataTable
        Dim Secenekler As New DataTable
        Dim Soru, Secenek As DataRow
        'Anketi sorular� al�n�yor
        Sorgu = "SELECT AnketSoruNo FROM AnketSorulari WHERE AnketNo=" & AnketNo & " AND SoruTuru=2 ORDER BY SoruSirasi"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Sorular)
        Catch ex As Exception
            Hata = True
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Soru se�enekleri al�n�yor
        Sorgu = "SELECT AnketSoruSecenekNo FROM AnketSoruSecenekleri WHERE AnketNo=" & AnketNo & " ORDER BY AnketSoruNo"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Secenekler)
        Catch ex As Exception
            Hata = True
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Sorulara verilen cevaplar al�n�yor
        Dim KA As String
        Dim Cevaplar As New Collection
        For Each Soru In Sorular.Rows
            KA = "rbSoru" & Soru("AnketSoruNo")
            Dim aaa As String
            If Request.Form(KA) <> "" Then
                If SayisalMi(Request.Form(KA), 1, 1000000000) Then
                    Cevaplar.Add(CStr(Request.Form(KA)), CStr(Soru("AnketSoruNo")))
                Else
                    Hata = True
                End If
            Else
                Hata = True
            End If
            If Hata Then
                lblHata.Text = "L�tfen Cevaplar� Eksiksiz Doldurun.<br>"
                Baglanti.Close()
                Exit Sub
            End If
        Next
        'T�m cevaplar al�nd�, �imdi cevaplar�n ge�erlili�i kontrol edilyor
        Dim SecenekNo As Long
        Dim Var As Boolean = False
        For Each SecenekNo In Cevaplar
            Var = False
            For Each Secenek In Secenekler.Rows
                If SecenekNo = Secenek("AnketSoruSecenekNo") Then
                    Var = True
                End If
            Next
            If Not Var Then
                lblHata.Text = "Verilen cevaplar ge�erli de�il!"
                Baglanti.Close()
                Exit Sub
            End If
        Next
        'Yorum alan� kontrol ediliyor
        Dim Yorum As String = Request.Form("txtYorum_Alani").Replace("'", "''")
        If Yorum <> "" Then
            If Yorum.Length > 2000 Then
                lblHata.Text = "Yorum alan�na fazla karekter girilmi�."
                Baglanti.Close()
                Exit Sub
            End If
            Yorum = HtmlKodla(Yorum)
        Else
            Yorum = "bo� b�rak�lm��"
        End If
        'T�m cevaplar ge�erli, art�k veritaban�na yaz�l�yor
        Dim CevapMetni As String = ""
        Dim ilk As Boolean = True
        For Each SecenekNo In Cevaplar
            If ilk Then
                CevapMetni = SecenekNo.ToString
                ilk = False
            Else
                CevapMetni += "," & SecenekNo.ToString
            End If
        Next
        Dim Basarili As Boolean = False
        Try
            Sorgu = "INSERT INTO AnketCevaplar VALUES(NULL," & AnketNo & "," & DersNo & ",2,'" & CevapMetni & "','" & Yorum & "')"
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            Sorgu = "UPDATE DersOgrenciIliskileri SET AnketDoldurulduMu=1 WHERE DersOgrenciIliskiNo=" & DersOgrenciIliskiNo
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            lblHata.Text = "Anketi ba�ar�yla doldurdunuz."
            Basarili = True
        Catch ex As Exception
            lblHata.Text = "Veritaban�na anket bilgileri yaz�lamad�."
        End Try
        Try
            Baglanti.Close()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�!<br>"
            Exit Sub
        End Try
        If Basarili Then
            Response.Redirect("default.aspx")
        End If
    End Sub
    'Doldurulan anket veritaban�na kay�t ediliyor (Genel Anket)
    Private Sub btnAnketTamam_Genel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnketTamam_Genel.Click
        lblHata.Text = ""
        Dim OgrenciNo As Long = 0
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        If SayisalMi(Session("OgrenciNo"), 1, 1000000000) Then
            OgrenciNo = CInt(Session("OgrenciNo"))
        End If
        If SayisalMi(Session("OgretimYili"), 2000, 3000) Then
            Yil = CShort(Session("OgretimYili"))
        End If
        If SayisalMi(Session("Donem"), 1, 3) Then
            Donem = CByte(Session("Donem"))
        End If
        If OgrenciNo = 0 Or Yil = 0 Or Donem = 0 Then
            lblHata.Text = "�ye giri�i yapmam��s�n�z."
            Exit Sub
        End If
        '�ye giri�i yap�lm��sa, anketi doldurup doldurmad���na bak�l�yor
        Dim Sorgu As String
        Sorgu = "SELECT GADNo FROM GenelAnketDolduranlar " & _
        "WHERE OgrenciNo=" & OgrenciNo & _
        " AND Yil=" & Yil & " AND Donem=" & Donem
        Dim Komut As New OdbcCommand(Sorgu, Baglanti)

        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim AnketDoldurulduMu As Byte = 0
        Dim DersOgrenciIliskiNo As Long = 0
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�!<br>"
            Exit Sub
        End Try
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                AnketDoldurulduMu = 1
            Else
                AnketDoldurulduMu = 0
            End If
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�.<br>"
            AnketDoldurulduMu = 2
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If AnketDoldurulduMu = 1 Then
            lblHata.Text = "Bu anketi doldurdunuz."
            Baglanti.Close()
            Exit Sub
        End If
        If AnketDoldurulduMu = 2 Then
            lblHata.Text = "Veritaban�ndan anket bilgileri al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Anketi doldurabilir.. �imdi anketi do�ru doldurmu� mu diye bak�l�yor
        'Anket numaras� al�n�yor
        Dim AnketNo As Long
        Dim Hata As Boolean = False
        Sorgu = "SELECT AnketNo FROM Anketler WHERE Yil=" & Yil & " AND Donem=" & Donem
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                AnketNo = Okuyucu("AnketNo")
            Else
                Hata = True
            End If
        Catch ex As Exception
            Hata = True
        Finally
            Okuyucu.Close()
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Sorular al�n�yor
        Dim Sorular As New DataTable
        Dim Secenekler As New DataTable
        Dim Soru, Secenek As DataRow
        'Anketin sorular� al�n�yor
        Sorgu = "SELECT AnketSoruNo FROM AnketSorulari WHERE AnketNo=" & AnketNo & " AND SoruTuru=1 ORDER BY SoruSirasi"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Sorular)
        Catch ex As Exception
            Hata = True
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Soru se�enekleri al�n�yor
        Sorgu = "SELECT AnketSoruSecenekNo FROM AnketSoruSecenekleri WHERE AnketNo=" & AnketNo & " ORDER BY AnketSoruNo"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(Secenekler)
        Catch ex As Exception
            Hata = True
        End Try
        If Hata Then
            lblHata.Text = "Anket bilgileri veritaban�ndan al�namad�.<br>"
            Baglanti.Close()
            Exit Sub
        End If
        'Sorulara verilen cevaplar al�n�yor
        Dim KA As String
        Dim Cevaplar As New Collection
        For Each Soru In Sorular.Rows
            KA = "rbSoru" & Soru("AnketSoruNo")
            Dim aaa As String
            If Request.Form(KA) <> "" Then
                If SayisalMi(Request.Form(KA), 1, 1000000000) Then
                    Cevaplar.Add(CStr(Request.Form(KA)), CStr(Soru("AnketSoruNo")))
                Else
                    Hata = True
                End If
            Else
                Hata = True
            End If
            If Hata Then
                lblHata.Text = "L�tfen Cevaplar� Eksiksiz Doldurun.<br>"
                Baglanti.Close()
                Exit Sub
            End If
        Next
        'T�m cevaplar al�nd�, �imdi cevaplar�n ge�erlili�i kontrol edilyor
        Dim SecenekNo As Long
        Dim Var As Boolean = False
        For Each SecenekNo In Cevaplar
            Var = False
            For Each Secenek In Secenekler.Rows
                If SecenekNo = Secenek("AnketSoruSecenekNo") Then
                    Var = True
                End If
            Next
            If Not Var Then
                lblHata.Text = "Verilen cevaplar ge�erli de�il!"
                Baglanti.Close()
                Exit Sub
            End If
        Next
        'Yorum alan� kontrol ediliyor
        Dim Yorum As String = Request.Form("txtYorum_Alani").Replace("'", "''")
        If Yorum <> "" Then
            If Yorum.Length > 2000 Then
                lblHata.Text = "Yorum alan�na fazla karekter girilmi�."
                Baglanti.Close()
                Exit Sub
            End If
            Yorum = HtmlKodla(Yorum)
        Else
            Yorum = "bo� b�rak�lm��"
        End If
        'T�m cevaplar ge�erli, art�k veritaban�na yaz�l�yor
        Dim CevapMetni As String = ""
        Dim ilk As Boolean = True
        For Each SecenekNo In Cevaplar
            If ilk Then
                CevapMetni = SecenekNo.ToString
                ilk = False
            Else
                CevapMetni += "," & SecenekNo.ToString
            End If
        Next
        Dim Basarili As Boolean = False
        Try
            Sorgu = "INSERT INTO AnketCevaplar VALUES(NULL," & AnketNo & ",0,1,'" & CevapMetni & "','" & Yorum & "')"
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            Sorgu = "INSERT INTO GenelAnketDolduranlar VALUES(NULL," & OgrenciNo & "," & _
            Yil & "," & Donem & ")"
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            lblHata.Text = "Anketi ba�ar�yla doldurdunuz."
            Basarili = True
        Catch ex As Exception
            lblHata.Text = "Veritaban�na anket bilgileri yaz�lamad�."
        End Try
        Try
            Baglanti.Close()
        Catch ex As Exception
            lblHata.Text = "Veritaban�na ba�lan�lamad�!<br>"
            Exit Sub
        End Try
        If Basarili Then
            Response.Redirect("default.aspx")
        End If
    End Sub
End Class
