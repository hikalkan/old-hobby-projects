Public Class seminer
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents frmSeminerGenel As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents divSeminerEklemeFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtKonu As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtIcerik As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKonusmaci As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKonusmaciHakkinda As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtYer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDavetliler As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSure As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEkBilgiler As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSeminerEkleTamam As System.Web.UI.WebControls.Button
    Protected WithEvents txtSaat As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlGun As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlay As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYil As System.Web.UI.WebControls.DropDownList

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
        divSeminerEklemeFormu.Visible = False
        If BilgiKontrolu(Request.QueryString("Sayfa"), 50, False) Then
            Dim Sayfa As String = Request.QueryString("Sayfa")
            Select Case Sayfa
                Case "Listele"
                    SeminerleriListele()
                Case "Ayrintilar"
                    If SayisalMi(Request.QueryString("SeminerNo"), 1, 2000000000) Then
                        SeminerAyrintisi(CInt(Request.QueryString("SeminerNo")))
                    End If
                Case "Ekle"
                    If Session("YetkiDuzeyi") >= YD_Yonetici Then
                        divSeminerEklemeFormu.Visible = True
                        FormdaTarihiAyarla()
                    End If
                Case "Sil"
                    If Session("YetkiDuzeyi") > YD_Uye Then
                        If SayisalMi(Request.QueryString("SeminerNo"), 1, 2000000000) Then
                            SeminerSil(CInt(Request.QueryString("SeminerNo")))
                        End If
                    Else
                        Response.Write("Bu iþlem için yetkiniz yok!")
                    End If
                Case "Gecmis"
                    GecmisSeminerleriListele()
                Case Else
                    Response.Write("Parametre hatasý! Bu sayfaya doðrudan sayfa adý yazarak girmeye çalýþmayýn!")
                    Response.End()
            End Select
        Else
            Response.Write("Bu sayfaya doðrudan sayfa adý yazarak girmeye çalýþmayýn!")
            Response.End()
        End If
    End Sub
    Private Sub SeminerSil(ByVal SeminerNo As Integer)
        sorgu = "DELETE FROM Seminerler WHERE SeminerNo=" & SeminerNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        Response.Redirect("seminer.aspx?Sayfa=Listele")
    End Sub
    Private Sub SeminerAyrintisi(ByVal SeminerNo As Integer)
        sorgu = "SELECT * FROM Seminerler WHERE SeminerNo=" & SeminerNo
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
                    kod = kod & " (<a href='seminer.aspx?Sayfa=Sil&SeminerNo=" & SeminerNo & "'><font style='color:#DDDDDD'>Sil</font></a>)"
                End If
                kod = kod & "</p></td>"
                kod = kod & "</tr>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
                kod = kod & "<p class='Normal'><b><u>Ýçerik:</u></b> " & KarakterKodCoz(okuyucu("Icerik")) & "</p>"
                kod = kod & "<p class='Normal'><b><u>Konuþmacý:</u></b> " & KarakterKodCoz(okuyucu("Konusmaci")) & " (" & KarakterKodCoz(okuyucu("KonusmaciHakkinda")) & ")</p>"
                kod = kod & "<p class='Normal'><b><u>Tarih:</u></b> " & MySqlUzunTarihAl(okuyucu("Tarih")).ToLongDateString & ", saat " & MySqlUzunTarihAl(okuyucu("Tarih")).Hour & ".</p>"
                kod = kod & "<p class='Normal'><b><u>Yer:</u></b> " & KarakterKodCoz(okuyucu("Yer")) & "</p>"
                kod = kod & "<p class='Normal'><b><u>Süre:</u></b> " & KarakterKodCoz(okuyucu("Sure")) & " saat.</p>"
                kod = kod & "<p class='Normal'><b><u>Davetliler:</u></b> " & KarakterKodCoz(okuyucu("Davetliler")) & "</p>"
                If okuyucu("EkBilgiler") <> "-" Then
                    kod = kod & "<p class='Normal'><b><u>Ek bilgiler:</u></b> " & KarakterKodCoz(okuyucu("EkBilgiler")) & "</p>"
                End If
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "</table>"
            Else
                Response.Write("Kurs veritabanýnda bulunamadý!")
                Response.End()
            End If
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        kod = kod & "<p align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
        phOrta.Visible = True
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub SeminerleriListele()
        sorgu = "SELECT SeminerNo,Konu,Icerik,Konusmaci,Tarih FROM Seminerler WHERE Tarih>='" & MySqlUzunTarihYaz(Date.Now) & "' ORDER BY Tarih"
        komut.CommandText = sorgu
        Dim Seminerler As New DataTable
        Dim Seminer As DataRow
        Try
            adaptor.Fill(Seminerler)
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        Dim kod As String = ""
        If Seminerler.Rows.Count = 0 Then
            kod = "<p class='Normal'>Þu anda planlanmýþ bir seminer yok</p>"
            If Session("YetkiDuzeyi") >= YD_Yonetici Then
                kod = kod & "<p class='Sade' align='right'><a href='seminer.aspx?Sayfa=Ekle'>Seminer ekle</a></p>"
            End If
            phOrta.Visible = True
            phOrta.Controls.Add(New LiteralControl(kod))
        Else
            kod = kod & "<Table align='center' width='100%' cellpadding='0' cellspacing='3' border='0'>"
            If Session("YetkiDuzeyi") >= YD_Yonetici Then
                kod = kod & "<tr><td width='100%'>"
                kod = kod & "<p class='Sade' align='right'><a href='seminer.aspx?Sayfa=Ekle'>Seminer ekle</a></p>"
                kod = kod & "</td></tr>"
            End If
            For Each Seminer In Seminerler.Rows
                kod = kod & "<tr><td width='100%'>"
                kod = kod & "<Table align='center' width='100%' cellpadding='1' cellspacing='1' bgcolor='#444444' border='0'>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#294963' valign='center'>"
                kod = kod & "<p style='color:#FFFFFF'>&nbsp<img src='images/diger/sagaok.gif' height='11px' width='15px' border='0'> <b>" & KarakterKodCoz(Seminer("Konu")) & "</b></p>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
                kod = kod & "<p class='Sade'><b><u>Ýçerik:</u></b> " & KarakterKodCoz(Seminer("Icerik"))
                kod = kod & "<br><b><u>Konuþmacý:</u></b> " & KarakterKodCoz(Seminer("Konusmaci")) & "</p>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "<tr>"
                kod = kod & "<td width='100%' bgcolor='#919CB6'>"
                kod = kod & "<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>"
                kod = kod & "<tr><td class='Sade' width='50%' align='left'>"
                kod = kod & "<p style='color:#FFFFFF'><b>" & MySqlUzunTarihAl(Seminer("Tarih")).ToLongDateString & ", saat " & MySqlUzunTarihAl(Seminer("Tarih")).Hour & ".</b></p>"
                kod = kod & "</td><td class='Sade' width='50%' align='right' valign='center'>"
                kod = kod & "<a href='seminer.aspx?Sayfa=Ayrintilar&SeminerNo=" & Seminer("SeminerNo") & "'><img src='images/diger/ayrinti.gif' height='15px' width='74px' border='0'></a>"
                kod = kod & "</td></tr>"
                kod = kod & "</table>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "</table>"
                kod = kod & "</td></tr>"
            Next
            kod = kod & "<tr><td width='100%'>"
            kod = kod & "<p class='Sade' align='Center'><a href='seminer.aspx?Sayfa=Gecmis'>Geçmiþ seminerleri görmek için buraya týklayýn.</a></p>"
            kod = kod & "</td></tr>"
            kod = kod & "</table>"
            kod = kod & "<p align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
            phOrta.Visible = True
            phOrta.Controls.Add(New LiteralControl(kod))
        End If
    End Sub
    Private Sub FormdaTarihiAyarla()
        Dim i As Integer = 0
        Dim li As ListItem
        'Günler
        For i = 1 To 31
            li = New ListItem
            li.Value = i
            li.Text = i.ToString
            ddlGun.Items.Add(li)
        Next
        'Aylar
        Dim Aylar() As String = {"Ocak", "Þubat", "Mart", "Nisan", "Mayýs", _
        "Haziran", "Temmuz", "Aðustos", "Eylül", "Ekim", "Kasým", "Aralýk"}
        For i = 1 To 12
            li = New ListItem
            li.Value = i
            li.Text = Aylar(i - 1)
            ddlAy.Items.Add(li)
        Next
        'Yýllar
        For i = 2005 To 2020
            li = New ListItem
            li.Value = i
            li.Text = i.ToString
            ddlYil.Items.Add(li)
        Next
    End Sub
    Private Sub btnSeminerEkleTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeminerEkleTamam.Click
        Dim Konu, Icerik, Konusmaci, KonusmaciHakkinda, Yer, Davetliler, EkBilgiler As String
        Dim Sure, Saat As Byte
        Dim Tarih As String ' Seminerin tarihi (tarih ve saatin birleþimi)

        Dim AydakiGunler() As Byte = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        If Date.Now.Year Mod 4 = 0 Then 'Þubat ayarlamasý
            AydakiGunler(1) = 29
        End If

        Konu = txtKonu.Text
        Icerik = txtIcerik.Text
        Konusmaci = txtKonusmaci.Text
        KonusmaciHakkinda = txtKonusmaciHakkinda.Text
        Yer = txtYer.Text
        Davetliler = txtDavetliler.Text
        If txtEkBilgiler.Text.Length > 0 Then
            EkBilgiler = txtEkBilgiler.Text
        Else
            EkBilgiler = "-"
        End If
        If SayisalMi(txtSure.Text, 1, 9) Then
            Sure = CInt(txtSure.Text)
        Else
            Response.Write("Süre sayýsal ve 1-9 arasýnda olmalý.")
            Response.End()
        End If
        If SayisalMi(txtSaat.Text, 0, 23) Then
            Saat = CInt(txtSaat.Text)
        Else
            Response.Write("Saat sayýsal ve 1-24 arasýnda olmalý.")
            Response.End()
        End If
        If SayisalMi(ddlGun.SelectedItem.Value, 1, 31) And _
        SayisalMi(ddlay.SelectedItem.Value, 1, 12) And _
        SayisalMi(ddlYil.SelectedItem.Value, 2005, 2020) Then
            If AydakiGunler(ddlay.SelectedItem.Value - 1) < ddlGun.SelectedItem.Value Then
                Response.Write("Tarih ve Saat Bilgisini kontrol edin!")
                Response.End()
            Else
                Tarih = ddlYil.SelectedItem.Value.ToString & "-" & _
                ikiBasamakli(ddlay.SelectedItem.Value) & "-" & _
                ikiBasamakli(ddlGun.SelectedItem.Value) & " " & _
                ikiBasamakli(Saat) & ":00:00"
            End If
        Else
            Response.Write("Tarih ve Saat Bilgisini kontrol edin!")
            Response.End()
        End If
        sorgu = "INSERT INTO Seminerler(Konu,Icerik,Konusmaci,KonusmaciHakkinda," & _
        "Tarih,Yer,Davetliler,Sure,EkBilgiler) Values('" & KarakterKodla(Konu) & "','" & _
        KarakterKodla(Icerik) & "','" & KarakterKodla(Konusmaci) & "','" & KarakterKodla(KonusmaciHakkinda) & "','" & Tarih & _
        "','" & KarakterKodla(Yer) & "','" & KarakterKodla(Davetliler) & "'," & Sure & ",'" & KarakterKodla(EkBilgiler) & "')"
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            sorgu = "INSERT INTO Duyurular(Baslik,Metin,Tarih,KategoriNo) Values('" & _
            KarakterKodla(Konu) & " Semineri','" & Date.Now.ToShortDateString & " tarihinde " & KarakterKodla(Konu) & _
            " semineri vardýr. Seminerin içeriði þöyle: " & KarakterKodla(Icerik) & "','" & _
            MySqlUzunTarihYaz(Date.Now) & "'," & KN_Seminer & ")"
            komut.CommandText = sorgu
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý" & ex.Message)
            Response.End()
        End Try
        Response.Redirect("seminer.aspx?Sayfa=Listele")
    End Sub
    Private Sub GecmisSeminerleriListele()
        sorgu = "SELECT SeminerNo,Konu,Tarih FROM Seminerler WHERE Tarih<'" & MySqlUzunTarihYaz(Date.Now) & "' ORDER BY Tarih DESC"
        komut.CommandText = sorgu
        Dim Seminerler As New DataTable
        Dim Seminer As DataRow
        Try
            adaptor.Fill(Seminerler)
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        'Seminerler tablosu oluþturuluyor
        Dim tablo As New Tablom
        Dim bicim As New TablomBicim
        'Biçim özellikleri
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
        'Tablo baþlýðý ve biçim atamasý
        tablo.Bicim = bicim
        tablo.Baslik = "G e ç m i þ &nbsp S e m i n e r l e r"
        'Kolonlar oluþturuluyor
        Dim kolon As Kolonum
        kolon = New Kolonum
        kolon.Baslik = "Tarih"
        kolon.Genislik = "30%"
        kolon.icerikHizalama = "Center"
        tablo.KolonEkle(kolon)
        kolon = New Kolonum
        kolon.Baslik = "Konu"
        kolon.Genislik = "70%"
        tablo.KolonEkle(kolon)
        'Satýrlar oluþturuluyor
        Dim satir As Satirim
        Dim hucre As Hucrem
        For Each Seminer In Seminerler.Rows
            satir = New Satirim
            hucre = New Hucrem
            hucre.Metin = MySqlUzunTarihAl(Seminer("Tarih")).ToLongDateString
            satir.HucreEkle(hucre)
            hucre = New Hucrem
            hucre.Metin = "<a href='seminer.aspx?Sayfa=Ayrintilar&SeminerNo=" & Seminer("SeminerNo") & "'><Font style='color:'>" & Seminer("Konu") & "</font></a>"
            satir.HucreEkle(hucre)
            tablo.SatirEkle(satir)
        Next
        'Html kodu üretiliyor
        tablo.HtmlKoduUret()
        phOrta.Controls.Add(New LiteralControl(tablo.HtmlKodu))
        Dim kod As String
        kod = "<p align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
End Class
