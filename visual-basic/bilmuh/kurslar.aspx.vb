Public Class kurslar
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents txtKonu As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIcerik As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtYer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEkBilgiler As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlBitGun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlBitAy As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlBitYil As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtAnlatici As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAnlaticiHakkinda As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlBasGun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlBasAy As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlBasYil As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtBasvuru As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKatilimSartlari As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDersSaati As System.Web.UI.WebControls.TextBox
    Protected WithEvents divKursEklemeFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents btnKursEkleTamam As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim baglanti As New Odbc.OdbcConnection("Driver={MySQL ODBC 3.51 Driver};Server=127.0.0.1;Database=bilmuh;uid=hikalkan;pwd=himysql;option=35")
    Dim sorgu As String = ""
    Dim komut As New Odbc.OdbcCommand(sorgu, baglanti)
    Dim okuyucu As Odbc.OdbcDataReader
    Dim adaptor As New Odbc.OdbcDataAdapter(komut)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        divKursEklemeFormu.Visible = False
        If BilgiKontrolu(Request.QueryString("Sayfa"), 50, False) Then
            Dim Sayfa As String = Request.QueryString("Sayfa")
            Select Case Sayfa
                Case "Listele"
                    KurslariListele()
                Case "Ekle"
                    If Session("YetkiDuzeyi") >= YD_Yonetici Then
                        divKursEklemeFormu.Visible = True
                        FormdaTarihiAyarla()
                    Else
                        Response.Write("Bu i�lem i�in yetkiniz yok!")
                        Response.End()
                    End If
                Case "Ayrintilar"
                    If SayisalMi(Request.QueryString("KursNo"), 1, 2000000000) Then
                        KursAyrintisi(CInt(Request.QueryString("KursNo")))
                    End If
                Case "Gecmis"
                    GecmisKurslariListele()
                Case "Sil"
                    If Session("YetkiDuzeyi") >= YD_Yonetici Then
                        If SayisalMi(Request.QueryString("KursNo"), 1, 2000000000) Then
                            KursSil(CInt(Request.QueryString("KursNo")))
                        End If
                    End If
                Case Else
                    Response.Write("Parametre hatas�! Bu sayfaya do�rudan sayfa ad� yazarak girmeye �al��may�n!")
                    Response.End()
            End Select
        Else
            Response.Write("Bu sayfaya do�rudan sayfa ad� yazarak girmeye �al��may�n!")
            Response.End()
        End If
    End Sub
    Private Sub KursAyrintisi(ByVal KursNo As Integer)
        sorgu = "SELECT * FROM Kurslar WHERE KursNo=" & KursNo
        komut.CommandText = sorgu
        Dim kod As String = ""
        Try
            baglanti.Open()
            okuyucu = komut.ExecuteReader
            If okuyucu.Read Then
                kod = kod & "<Table align='center' width='100%' cellpadding='1' cellspacing='1' bgcolor='#444444' border='0'>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#294963' valign='center'>"
                kod = kod & "<p style='color:#FFFFFF'>&nbsp<img src='images/diger/sagaok.gif' height='11px' width='15px' border='0'> <b>" & KarakterKodCoz(okuyucu("Konu")) & "</b>"
                If Session("YetkiDuzeyi") >= YD_Yonetici Then
                    kod = kod & " (<a href='kurslar.aspx?Sayfa=Sil&KursNo=" & KursNo & "'><font style='color:#DDDDDD'>Sil</font></a>)"
                End If
                kod = kod & "</p></td>"
                kod = kod & "</tr><tr><td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
                kod = kod & "<p class='Sade' align='Center'><b><u>��ER�K</u></b></p>"
                kod = kod & "<p class='Sade'>" & okuyucu("Icerik") & "</p>"
                kod = kod & "</td></tr><tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
                kod = kod & "<p class='Sade' align='Center'><b><u>D��ER B�LG�LER</u></b></p>"
                kod = kod & "<p class='Normal'><b><u>Anlat�c�:</u></b> " & KarakterKodCoz(okuyucu("Anlatici")) & " (" & KarakterKodCoz(okuyucu("AnlaticiHakkinda")) & ")</p>"
                kod = kod & "<p class='Normal'><b><u>Tarih:</u></b> " & MySqlUzunTarihAl(okuyucu("BaslangicTarihi")).ToLongDateString & " - " & MySqlUzunTarihAl(okuyucu("BitisTarihi")).ToLongDateString & " aras�.</p>"
                kod = kod & "<p class='Normal'><b><u>Haftalik Ders Saati:</u></b> " & okuyucu("HaftalikDersSaati") & " saat.</p>"
                kod = kod & "<p class='Normal'><b><u>Yer:</u></b> " & KarakterKodCoz(okuyucu("Yer")) & "</p>"
                kod = kod & "<p class='Normal'><b><u>Ba�vuru:</u></b> " & KarakterKodCoz(okuyucu("Basvuru")) & "</p>"
                kod = kod & "<p class='Normal'><b><u>Kat�l�m �artlar�:</u></b> " & KarakterKodCoz(okuyucu("KatilimSartlari")) & "</p>"
                If okuyucu("EkBilgiler") <> "-" Then
                    kod = kod & "<p class='Normal'><b><u>Ek bilgiler:</u></b> " & KarakterKodCoz(okuyucu("EkBilgiler")) & "</p>"
                End If
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "</table>"
            Else
                Response.Write("Seminer veritaban�nda bulunamad�!")
                Response.End()
            End If
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritaban�na ba�lan�lamad�!" & ex.Message)
            Response.End()
        End Try
        kod = kod & "<p align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
        phOrta.Visible = True
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub KurslariListele()
        Dim Kurslar As New DataTable
        Dim Kurs As DataRow
        Dim kod As String = ""
        phOrta.Visible = True
        'Sayfan�n kodu olu�turuluyor
        kod = kod & "<Table align='center' width='100%' cellpadding='0' cellspacing='3' border='0'>"
        If Session("YetkiDuzeyi") >= YD_Yonetici Then
            kod = kod & "<tr><td width='100%'>"
            kod = kod & "<p class='Sade' align='Right'><a href='kurslar.aspx?Sayfa=Ekle'>Kurs ekle</a></p>"
            kod = kod & "</td></tr>"
        End If
        ' ### Gelecekteki Kurslar ###
        sorgu = "SELECT * FROM Kurslar WHERE BaslangicTarihi>'" & _
        MySqlUzunTarihYaz(Date.Now) & "' ORDER BY BaslangicTarihi DESC"
        komut.CommandText = sorgu
        Try
            adaptor.Fill(Kurslar)
        Catch ex As Exception
            Response.Write("Veritaban�na ba�lan�lamad�!" & ex.Message)
            Response.End()
        End Try
        If Kurslar.Rows.Count > 0 Then
            kod = kod & "<tr><td width='100%'>"
            kod = kod & "<p class='Sade' align='Center'><b><u><font style='color:#294963'><img src='images/baslik/gelecekteki_kurslar.gif'></u></b></p>"
            kod = kod & "</td></tr>"
            For Each Kurs In Kurslar.Rows
                kod = kod & "<tr><td width='100%'>"
                kod = kod & "<Table align='center' width='100%' cellpadding='1' cellspacing='1' bgcolor='#444444' border='0'>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#294963' valign='center'>"
                kod = kod & "<p style='color:#FFFFFF'>&nbsp<img src='images/diger/sagaok.gif' height='11px' width='15px' border='0'> <b>" & KarakterKodCoz(Kurs("Konu")) & "</b></p>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
                kod = kod & "<p class='Sade'><b><u>Ba�lang�� Tarihi:</u></b> " & MySqlUzunTarihAl(Kurs("BaslangicTarihi")).ToLongDateString
                kod = kod & "<br><b><u>Ba�vuru:</u></b> " & KarakterKodCoz(Kurs("Basvuru")) & "</p>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "<tr>"
                kod = kod & "<td width='100%' bgcolor='#919CB6'>"
                kod = kod & "<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>"
                kod = kod & "<tr><td class='Sade' width='50%' align='left'>"
                kod = kod & "<p style='color:#FFFFFF'><b></b></p>"
                kod = kod & "</td><td class='Sade' width='50%' align='right' valign='center'>"
                kod = kod & "<a href='kurslar.aspx?Sayfa=Ayrintilar&KursNo=" & Kurs("KursNo") & "'><img src='images/diger/ayrinti.gif' height='15px' width='74px' border='0'></a>"
                kod = kod & "</td></tr>"
                kod = kod & "</table>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "</table>"
                kod = kod & "</td></tr>"
            Next
        End If
        phOrta.Controls.Add(New LiteralControl(kod))
        Kurslar.Dispose()
        kod = ""
        ' ### �u anda devam eden Kurslar ###
        Kurslar = New DataTable
        sorgu = "SELECT * FROM Kurslar WHERE BaslangicTarihi<='" & _
        MySqlUzunTarihYaz(Date.Now) & "' AND BitisTarihi>='" & MySqlUzunTarihYaz(Date.Now) & _
        "' ORDER BY BaslangicTarihi DESC"
        komut.CommandText = sorgu
        Try
            adaptor.Fill(Kurslar)
        Catch ex As Exception
            Response.Write("Veritaban�na ba�lan�lamad�!" & ex.Message)
            Response.End()
        End Try
        If Kurslar.Rows.Count > 0 Then
            kod = kod & "<tr><td width='100%'>"
            kod = kod & "<p class='Sade' align='Center'><b><u><img src='images/baslik/devam_eden_kurslar.gif'></u></b></p>"
            kod = kod & "</td></tr>"
            For Each Kurs In Kurslar.Rows
                kod = kod & "<tr><td width='100%'>"
                kod = kod & "<Table align='center' width='100%' cellpadding='1' cellspacing='1' bgcolor='#444444' border='0'>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#294963' valign='center'>"
                kod = kod & "<p style='color:#FFFFFF'>&nbsp<img src='images/diger/sagaok.gif' height='11px' width='15px' border='0'> <b>" & KarakterKodCoz(Kurs("Konu")) & "</b></p>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
                kod = kod & "<p class='Sade'><b><u>Ba�lang�� Tarihi:</u></b> " & MySqlUzunTarihAl(Kurs("BaslangicTarihi")).ToLongDateString
                kod = kod & "<br><b><u>Biti� Tarihi:</u></b> " & MySqlUzunTarihAl(Kurs("BitisTarihi")).ToLongDateString
                kod = kod & "<br><b><u>Ba�vuru:</u></b> " & KarakterKodCoz(Kurs("Basvuru")) & "</p>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "<tr>"
                kod = kod & "<td width='100%' bgcolor='#919CB6'>"
                kod = kod & "<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>"
                kod = kod & "<tr><td class='Sade' width='50%' align='left'>"
                kod = kod & "<p style='color:#FFFFFF'><b></b></p>"
                kod = kod & "</td><td class='Sade' width='50%' align='right' valign='center'>"
                kod = kod & "<a href='kurslar.aspx?Sayfa=Ayrintilar&KursNo=" & Kurs("KursNo") & "'><img src='images/diger/ayrinti.gif' height='15px' width='74px' border='0'></a>"
                kod = kod & "</td></tr>"
                kod = kod & "</table>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "</table>"
                kod = kod & "</td></tr>"
            Next
        End If
        phOrta.Controls.Add(New LiteralControl(kod))
        Kurslar.Dispose()
        kod = ""
        kod = kod & "<tr><td width='100%'>"
        kod = kod & "<p class='Sade' align='Center'><a href='kurslar.aspx?Sayfa=Gecmis'>Ge�mi� kurslar� g�rmek i�in buraya t�klay�n.</a></p>"
        kod = kod & "</td></tr>"
        kod = kod & "</table>"
        kod = kod & "<p align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub FormdaTarihiAyarla()
        Dim i As Integer = 0
        Dim li As ListItem
        'G�nler
        For i = 1 To 31
            li = New ListItem
            li.Value = i
            li.Text = i.ToString
            ddlBasGun.Items.Add(li)
            ddlBitGun.Items.Add(li)
        Next
        'Aylar
        Dim Aylar() As String = {"Ocak", "�ubat", "Mart", "Nisan", "May�s", _
        "Haziran", "Temmuz", "A�ustos", "Eyl�l", "Ekim", "Kas�m", "Aral�k"}
        For i = 1 To 12
            li = New ListItem
            li.Value = i
            li.Text = Aylar(i - 1)
            ddlBasAy.Items.Add(li)
            ddlBitAy.Items.Add(li)
        Next
        'Y�llar
        For i = 2005 To 2020
            li = New ListItem
            li.Value = i
            li.Text = i.ToString
            ddlBasYil.Items.Add(li)
            ddlBitYil.Items.Add(li)
        Next
    End Sub
    'Kurs Ekle Sayfas�nda Tamam butonuna bas�l�nca yap�lacak i�lemlerin tan�mland��� prosed�r :)
    Private Sub btnKursEkleTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKursEkleTamam.Click
        Dim Konu, Icerik, Anlatici, AnlaticiHakkinda, Yer, _
        Basvuru, KatilimSartlari, EkBilgiler As String
        Dim BaslangicTarihi, BitisTarihi As String
        Dim HaftalikDersSaati As Byte = 0

        Dim AydakiGunler() As Byte = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        If Date.Now.Year Mod 4 = 0 Then '�ubat ayarlamas�
            AydakiGunler(1) = 29
        End If

        Konu = txtKonu.Text
        Icerik = txtIcerik.Text
        Anlatici = txtAnlatici.Text
        AnlaticiHakkinda = txtAnlaticiHakkinda.Text
        Yer = txtYer.Text
        Basvuru = txtBasvuru.Text
        KatilimSartlari = txtKatilimSartlari.Text
        If txtEkBilgiler.Text.Length > 0 Then
            EkBilgiler = txtEkBilgiler.Text
        Else
            EkBilgiler = "-"
        End If

        If SayisalMi(txtDersSaati.Text, 0, 23) Then
            HaftalikDersSaati = CInt(txtDersSaati.Text)
        Else
            Response.Write("Haftalik Ders Saati say�sal olmal�.")
            Response.End()
        End If
        'Ba�lang�� tarihi
        If SayisalMi(ddlBasGun.SelectedItem.Value, 1, 31) And _
        SayisalMi(ddlBasAy.SelectedItem.Value, 1, 12) And _
        SayisalMi(ddlBasYil.SelectedItem.Value, 2005, 2020) Then
            If AydakiGunler(ddlBasAy.SelectedItem.Value - 1) < ddlBasGun.SelectedItem.Value Then
                Response.Write("Tarih ve Saat Bilgisini kontrol edin!")
                Response.End()
            Else
                BaslangicTarihi = ddlBasYil.SelectedItem.Value.ToString & "-" & _
                ikiBasamakli(ddlBasAy.SelectedItem.Value) & "-" & _
                ikiBasamakli(ddlBasGun.SelectedItem.Value) & " 00:00:00"
            End If
        Else
            Response.Write("Ba�lang�� tarihini kontrol edin!")
            Response.End()
        End If
        'Biti� tarihi
        If SayisalMi(ddlBitGun.SelectedItem.Value, 1, 31) And _
        SayisalMi(ddlBitAy.SelectedItem.Value, 1, 12) And _
        SayisalMi(ddlBitYil.SelectedItem.Value, 2005, 2020) Then
            If AydakiGunler(ddlBitAy.SelectedItem.Value - 1) < ddlBitGun.SelectedItem.Value Then
                Response.Write("Biti� tarihini kontrol edin!")
                Response.End()
            Else
                BitisTarihi = ddlBitYil.SelectedItem.Value.ToString & "-" & _
                ikiBasamakli(ddlBitAy.SelectedItem.Value) & "-" & _
                ikiBasamakli(ddlBitGun.SelectedItem.Value) & " 00:00:00"
            End If
        Else
            Response.Write("Tarih ve Saat Bilgisini kontrol edin!")
            Response.End()
        End If

        'Veritaban�na yaz�l�yor
        sorgu = "INSERT INTO Kurslar(Konu,Icerik,Anlatici,AnlaticiHakkinda," & _
        "BaslangicTarihi,BitisTarihi,HaftalikDersSaati,Yer,Basvuru,KatilimSartlari," & _
        "EkBilgiler) Values('" & KarakterKodla(Konu) & "','" & KarakterKodla(Icerik) & "','" & KarakterKodla(Anlatici) & "','" & _
        KarakterKodla(AnlaticiHakkinda) & "','" & BaslangicTarihi & "','" & BitisTarihi & "'," & _
        HaftalikDersSaati & ",'" & KarakterKodla(Yer) & "','" & KarakterKodla(Basvuru) & "','" & KarakterKodla(KatilimSartlari) & _
        "','" & KarakterKodla(EkBilgiler) & "')"
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            sorgu = "INSERT INTO Duyurular(Baslik,Metin,Tarih,KategoriNo) " & _
            "Values('" & Konu & " Kursu','" & MySqlUzunTarihAl(BaslangicTarihi).ToShortDateString & " ve " & _
            MySqlUzunTarihAl(BitisTarihi).ToShortDateString & " tarihleri aras�nda " & Konu & " kursu verilecektir." & _
            " Ayr�nt�lar i�in devam et ba�lant�s�na t�klay�n.','" & MySqlUzunTarihYaz(Date.Now) & _
            "'," & KN_Kurs & ")"
            komut.CommandText = sorgu
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritaban�na ba�lan�lamad�!")
            Response.End()
        End Try
        Response.Redirect("kurslar.aspx?Sayfa=Listele")
    End Sub
    Private Sub GecmisKurslariListele()
        sorgu = "SELECT KursNo,Konu FROM Kurslar WHERE BitisTarihi<'" & MySqlUzunTarihYaz(Date.Now) & "' ORDER BY BitisTarihi DESC"
        komut.CommandText = sorgu
        Dim Kurslar As New DataTable
        Dim Kurs As DataRow
        Try
            adaptor.Fill(Kurslar)
        Catch ex As Exception
            Response.Write("Veritaban�na ba�lan�lamad�!" & ex.Message)
            Response.End()
        End Try
        'Kurslar tablosu olu�turuluyor
        Dim tablo As New Tablom
        Dim bicim As New TablomBicim
        'Bi�im �zellikleri
        bicim.TabloArkaRenk = "#000000"
        bicim.BaslikArkaRenk = "#294963"
        bicim.BaslikBoyut = "12px"
        bicim.BaslikFont = "Verdana"
        bicim.BaslikRenk = "#FFFFFF"
        bicim.KolonBaslikArkaRenk = "#919CB6"
        bicim.KolonBaslikYaziRenk = "#FFFFFF"
        'bicim.SatirArkaRenk1 = "#ebebeb"
        bicim.SatirYaziRenk1 = "#000000"
        'bicim.SatirArkaRenk2 = "#dddddd"
        bicim.SatirYaziRenk2 = "#000000"
        'Tablo ba�l��� ve bi�im atamas�
        tablo.Bicim = bicim
        tablo.Baslik = "G e � m i � &nbsp K u r s l a r"
        'Kolonlar olu�turuluyor
        Dim kolon As Kolonum
        kolon = New Kolonum
        kolon.Baslik = "Kurs Ad�"
        kolon.Genislik = "70%"
        tablo.KolonEkle(kolon)
        'Sat�rlar olu�turuluyor
        Dim satir As Satirim
        Dim hucre As Hucrem
        For Each Kurs In Kurslar.Rows
            satir = New Satirim
            hucre = New Hucrem
            hucre.Metin = "<a href='kurslar.aspx?Sayfa=Ayrintilar&KursNo=" & Kurs("KursNo") & "'><Font style='color:'>" & Kurs("Konu") & "</font></a>"
            satir.HucreEkle(hucre)
            tablo.SatirEkle(satir)
        Next
        'Html kodu �retiliyor
        tablo.HtmlKoduUret(False)
        phOrta.Controls.Add(New LiteralControl(tablo.HtmlKodu))
        Dim kod As String
        kod = "<p align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub KursSil(ByVal KursNo As Integer)
        sorgu = "DELETE FROM Kurslar WHERE KursNo=" & KursNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritaban�na ba�lan�lamad�!")
            Response.End()
        End Try
        Response.Redirect("kurslar.aspx?Sayfa=Listele")
    End Sub
End Class
