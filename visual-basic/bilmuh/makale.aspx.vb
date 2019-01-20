Public Class makale
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents divMakaleYazFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents frmMakaleYaz As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents divKategoriEkleFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents frmKategoriEkle As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents txtKategoriAd As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKategoriAciklama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKategoriIkon As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnKategoriEkle As System.Web.UI.WebControls.Button
    Protected WithEvents lblMakaleKategoriAd As System.Web.UI.WebControls.Label
    Protected WithEvents txtMakaleBaslik As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMakaleAciklama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMakaleIcerik As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnMakaleEkle As System.Web.UI.WebControls.Button
    Protected WithEvents chkMakaleHtmlKullan As System.Web.UI.WebControls.CheckBox
    Protected WithEvents frmOrta As System.Web.UI.HtmlControls.HtmlForm

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
        divMakaleYazFormu.Visible = False
        divKategoriEkleFormu.Visible = False
        If BilgiKontrolu(Request.QueryString("Sayfa"), 50, False) Then
            Dim Sayfa As String = Request.QueryString("Sayfa")
            Select Case Sayfa
                Case "Kategoriler"
                    KategorileriGoster()
                Case "Listele"
                    If SayisalMi(Request.QueryString("KategoriNo"), 1, 1000000) Then
                        MakaleleriListele(CInt(Request.QueryString("KategoriNo")))
                    End If
                Case "Oku"
                    If SayisalMi(Request.QueryString("MakaleNo"), 1, 2000000000) Then
                        MakaleOku(CInt(Request.QueryString("MakaleNo")))
                    End If
                Case "MakaleYaz"
                    If Session("YetkiDuzeyi") > YD_Uye Then
                        If SayisalMi(Request.QueryString("KategoriNo"), 1, 1000000) Then
                            divMakaleYazFormu.Visible = True
                            MakaleKategoriAdiniYaz(CInt(Request.QueryString("KategoriNo")))
                        End If
                    End If
                Case "KategoriEkle"
                    If Session("YetkiDuzeyi") = YD_Kurucu Then
                        divKategoriEkleFormu.Visible = True
                    End If
                Case "Sil"
                    If Session("YetkiDuzeyi") > YD_Uye Then
                        If SayisalMi(Request.QueryString("MakaleNo"), 1, 2000000000) Then
                            MakaleSil(CInt(Request.QueryString("MakaleNo")))
                        End If
                    End If
            End Select
        End If
    End Sub
    Private Sub MakaleSil(ByVal MakaleNo As Integer)
        Dim KategoriNo As Integer = 0
        Try
            baglanti.Open()
            'Makale'nin kategorisi bulunuyor
            sorgu = "SELECT KategoriNo FROM Makaleler WHERE MakaleNo=" & MakaleNo
            komut.CommandText = sorgu
            okuyucu = komut.ExecuteReader
            If okuyucu.Read Then
                KategoriNo = okuyucu("KategoriNo")
            Else
                Response.Write("Makale bulunamadý!")
                Response.End()
            End If
            okuyucu.Close()
            'Makale veritabanýndan siliniyor
            sorgu = "DELETE FROM Makaleler WHERE MakaleNo=" & MakaleNo
            komut.CommandText = sorgu
            komut.ExecuteNonQuery()
            'Makale'nin kategorisindeki Makale Sayýsý azaltýlýyor
            sorgu = "UPDATE MakaleKategorileri SET MakaleSayisi=MakaleSayisi-1 WHERE MakaleKategoriNo=" & KategoriNo
            komut.CommandText = sorgu
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        Response.Redirect("makale.aspx?Sayfa=Kategoriler")
    End Sub
    Private Sub MakaleKategoriAdiniYaz(ByVal KategoriNo As Integer)
        sorgu = "SELECT Ad FROM MakaleKategorileri WHERE MakaleKategoriNo=" & KategoriNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            okuyucu = komut.ExecuteReader
            If okuyucu.Read Then
                lblMakaleKategoriAd.Text = okuyucu("Ad")
            Else
                Response.Write("Makale eklemek için bir önce kategoriye girmelisiniz!")
                divMakaleYazFormu.Visible = False
                Response.End()
            End If
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
    End Sub
    Private Sub MakaleOku(ByVal MakaleNo As Integer)
        Dim Baslik, Icerik, Yazan As String
        Dim Tarih As New Date
        Dim UyeNo, OkunmaSayisi As Integer
        Dim ToplamPuan, PuanVerenSayisi As Integer
        sorgu = "SELECT Baslik,Icerik,Tarih,UyeNo,OkunmaSayisi,ToplamPuan,PuanVerenSayisi FROM Makaleler WHERE MakaleNo=" & MakaleNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            okuyucu = komut.ExecuteReader
            If okuyucu.Read Then
                Baslik = okuyucu("Baslik")
                Icerik = okuyucu("Icerik")
                UyeNo = okuyucu("UyeNo")
                Tarih = MySqlUzunTarihAl(okuyucu("Tarih"))
                OkunmaSayisi = okuyucu("OkunmaSayisi")
                PuanVerenSayisi = okuyucu("PuanVerenSayisi")
                ToplamPuan = okuyucu("ToplamPuan")
            Else
                Response.Write("Makale bulunamadý!")
                Response.End()
            End If
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        'Makaleyi Yazanýn adý bulunuyor..
        sorgu = "SELECT Lakap FROM Uyeler WHERE UyeNo=" & UyeNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            okuyucu = komut.ExecuteReader
            If okuyucu.Read Then
                Yazan = okuyucu("Lakap")
            Else
                Yazan = "?"
            End If
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        'Okunma Sayýsý artýrýlýyor..
        sorgu = "UPDATE Makaleler SET OkunmaSayisi=OkunmaSayisi+1 WHERE MakaleNo=" & MakaleNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        'Puan hesaplanýyor
        Dim Puan As String
        If PuanVerenSayisi = 0 Then
            Puan = "??"
        Else
            Puan = SayiDuzgunYaz(ToplamPuan / PuanVerenSayisi, 1)
        End If
        'Makale yazýlýyor..
        Dim kod As String
        kod = kod & "<Table align='center' width='100%' cellpadding='3' cellspacing='1' bgcolor='#444444' border='0'>"
        kod = kod & "<tr>"
        kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#294963'>"
        kod = kod & "<p style='color:#FFFFFF'><b>" & KarakterKodCoz(Baslik) & "</b>"
        If Session("YetkiDuzeyi") >= YD_Yonetici Then
            kod = kod & " (<a href='makale.aspx?Sayfa=Sil&MakaleNo=" & MakaleNo & "'><font style='color:#DDDDDD'>Sil</font></a>)"
        End If
        kod = kod & "</p>"
        kod = kod & "</td>"
        kod = kod & "</tr>"
        kod = kod & "<tr>"
        kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
        kod = kod & "<p class='Normal'>" & KarakterKodCoz(Icerik) & "</p>"
        kod = kod & "</td>"
        kod = kod & "</tr>"
        kod = kod & "<tr>"
        kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#919CB6'>"
        kod = kod & "<p style='color:#FFFFFF'>Tarih: <b>" & Tarih.ToLongDateString & "</b>; Okunma Sayýsý:<b>" & (OkunmaSayisi + 1).ToString & "</b>; Puan:<b>" & Puan & "</b>; Yazan:<a href='uyelik.aspx?Sayfa=UyeGoster&UyeNo=" & UyeNo & "'><b>" & Yazan & "</b></a></p>"
        kod = kod & "</td>"
        kod = kod & "</tr>"
        kod = kod & "</table>"
        phOrta.Controls.Add(New LiteralControl(kod))
        kod = ""
        'Puanlama için gerekli kontrol oluþturuluyor. 
        Dim OyVerilenMakaleler As String
        Try
            OyVerilenMakaleler = Request.Cookies("BilMuhSakarya")("OyVerilenMakaleler")
        Catch ex As Exception
            OyVerilenMakaleler = ""
        End Try
        Dim BuMakaleStr As String
        BuMakaleStr = "{" & MakaleNo.ToString & "}"
        If Not (InStr(OyVerilenMakaleler, BuMakaleStr) > 0) Then
            kod = "<p class='Sade' align='center'>Bu makaleye puan ver: "
            phOrta.Controls.Add(New LiteralControl(kod))
            Dim ddlPuan As New DropDownList
            Dim ddi As New ListItem
            ddi.Text = "seç"
            ddi.Value = "0"
            ddi.Selected = True
            ddlPuan.Items.Add(ddi)
            Dim i As Byte
            For i = 1 To 10
                ddi = New ListItem
                ddi.Text = i.ToString
                ddi.Value = i.ToString
                ddlPuan.Items.Add(ddi)
            Next
            ddlPuan.AutoPostBack = True
            AddHandler ddlPuan.SelectedIndexChanged, AddressOf ddlPuan_Degistir
            phOrta.Controls.Add(ddlPuan)
            kod = "</p>"
        End If
        kod = kod & "<p class='Sade' align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
        phOrta.Visible = True
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub ddlPuan_Degistir(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Secilen As String = sender.SelectedItem.Value
        Dim Puan As Byte
        Dim MakaleNo As Integer
        If SayisalMi(Secilen, 1, 10) And SayisalMi(Request.QueryString("MakaleNo"), 1, 2000000000) Then
            Puan = CInt(Secilen)
            MakaleNo = CInt(Request.QueryString("MakaleNo"))
            Dim OyVerilenMakaleler As String
            Try
                OyVerilenMakaleler = Request.Cookies("BilMuhSakarya")("OyVerilenMakaleler")
            Catch ex As Exception
                OyVerilenMakaleler = ""
            End Try
            Dim BuMakaleStr As String
            BuMakaleStr = "{" & MakaleNo & "}"
            If Not (InStr(OyVerilenMakaleler, BuMakaleStr) > 0) Then
                sorgu = "UPDATE Makaleler SET ToplamPuan=ToplamPuan+" & Puan & ",PuanVerenSayisi=PuanVerenSayisi+1 WHERE MakaleNo=" & MakaleNo
                komut.CommandText = sorgu
                Try
                    baglanti.Open()
                    komut.ExecuteNonQuery()
                    baglanti.Close()
                Catch ex As Exception
                    Response.Write("Veritabanýna baðlanýlamadý!")
                    Response.End()
                End Try
                OyVerilenMakaleler = OyVerilenMakaleler & BuMakaleStr
                Response.Cookies("BilMuhSakarya")("OyVerilenMakaleler") = OyVerilenMakaleler
                Response.Cookies("BilMuhSakarya").Expires = DateAdd(DateInterval.Day, 30, Date.Now)
            End If
            sender.Enabled = False
        End If
    End Sub
    Private Sub MakaleleriListele(ByVal MakaleKategoriNo As Integer)
        sorgu = "SELECT * FROM Makaleler WHERE KategoriNo=" & MakaleKategoriNo & " ORDER BY Tarih DESC"
        komut.CommandText = sorgu
        Dim MakaleTablosu As New DataTable
        Dim Makale As DataRow
        Try
            adaptor.Fill(MakaleTablosu)
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        Dim kod As String
        kod = kod & "<Table align='center' width='100%' cellpadding='0' cellspacing='3' border='0'>"
        If Session("YetkiDuzeyi") > YD_Uye Then
            kod = kod & "<tr><td width='100%'>"
            kod = kod & "<p class='Sade' align='right'><a href='makale.aspx?Sayfa=MakaleYaz&KategoriNo=" & MakaleKategoriNo & "'>Makale Yaz</a></p>"
            kod = kod & "</td></tr>"
        End If
        For Each Makale In MakaleTablosu.Rows
            kod = kod & "<tr><td width='100%'>"
            kod = kod & "<Table align='center' width='100%' cellpadding='1' cellspacing='1' bgcolor='#444444' border='0'>"
            kod = kod & "<tr>"
            kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#294963' valign='center'>"
            kod = kod & "<p style='color:#FFFFFF'>&nbsp<img src='images/diger/sagaok.gif' height='11px' width='15px' border='0'> <b>" & KarakterKodCoz(Makale("Baslik")) & "</b></p>"
            kod = kod & "</td>"
            kod = kod & "</tr>"
            kod = kod & "<tr>"
            kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
            kod = kod & "<p class='Normal'>" & KarakterKodCoz(Makale("Aciklama")) & "</p>"
            kod = kod & "</td>"
            kod = kod & "</tr>"
            kod = kod & "<tr>"
            kod = kod & "<td width='100%' bgcolor='#919CB6'>"
            kod = kod & "<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>"
            kod = kod & "<tr><td class='Sade' width='50%' align='left'>"
            kod = kod & "<p style='color:#FFFFFF'><b>" & MySqlUzunTarihAl(Makale("Tarih")).ToLongDateString & "</b></p>"
            kod = kod & "</td><td class='Sade' width='50%' align='right' valign='center'>"
            kod = kod & "<a href='makale.aspx?Sayfa=Oku&MakaleNo=" & Makale("MakaleNo") & "'><img src='images/diger/devam.gif' height='15px' width='65px' border='0'></a>"
            kod = kod & "</td></tr>"
            kod = kod & "</table>"
            kod = kod & "</td>"
            kod = kod & "</tr>"
            kod = kod & "</table>"
            kod = kod & "</td></tr>"
        Next
        If MakaleTablosu.Rows.Count = 0 Then
            kod = kod & "<tr><td width='100%'>"
            kod = kod & "<p class='Normal'>Bu kategori hakkýnda henüz makale yazýlmalýþ.</p>"
            kod = kod & "</td></tr>"
        End If
        kod = kod & "</table>"
        kod = kod & "<p align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
        phOrta.Visible = True
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub KategorileriGoster()
        sorgu = "SELECT * FROM MakaleKategorileri ORDER BY Ad"
        komut.CommandText = sorgu
        Dim KategoriTablosu As New DataTable
        Dim Kategori As DataRow
        Try
            adaptor.Fill(KategoriTablosu)
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        Dim kod As String
        kod = kod & "<tr><td width='100%'><p class='Normal'>Bu bölümde çeþitli konularda teknik yazýlar var. Siz de yazdýðýnýz makalelerin yayýnlanmasýný istiyorsanýz hi_kalkan@yahoo.com adresine gönderin.</p></td></tr>"
        kod = kod & "<Table align='center' width='100%' cellpadding='0' cellspacing='10' border='0'>"
        If Session("YetkiDuzeyi") = YD_Kurucu Then
            kod = kod & "<tr><td width='100%'>"
            kod = kod & "<p class='Sade' align='right'><a href='makale.aspx?Sayfa=KategoriEkle'>Kategori ekle</a></p>"
            kod = kod & "</td></tr>"
        End If
        For Each Kategori In KategoriTablosu.Rows
            kod = kod & "<tr>"
            kod = kod & "<td width='100%'>"
            kod = kod & "<Table width='100%' align='center' border='0' cellspacing='0' cellpadding='2'>"
            kod = kod & "<tr><td width='70px' rowspan='2' align='center' valign='top'>"
            kod = kod & "<a href='makale.aspx?Sayfa=Listele&KategoriNo=" & Kategori("MakaleKategoriNo") & "'><img src='images/ikonlar/" & Kategori("Ikon") & "' border='0'></a>"
            kod = kod & "</td><td class='Sade' width='*' bgcolor='#294963'><p style='color:#FFFFFF'><b>" & KarakterKodCoz(Kategori("Ad")) & " (" & Kategori("MakaleSayisi") & ")</b></p></td></tr>"
            kod = kod & "<tr><td class='Sade' width='*' bgcolor='#ebebff'><p class='Normal'>" & KarakterKodCoz(Kategori("Aciklama")) & "</p></td></tr>"
            kod = kod & "</Table>"
            kod = kod & "</td>"
            kod = kod & "</tr>"
        Next
        kod = kod & "</Table>"
        kod = kod & "<p align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
        phOrta.Visible = True
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub btnKategoriEkle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKategoriEkle.Click
        If Session("YetkiDuzeyi") <> YD_Kurucu Then
            Response.Write("Bu iþlem için yetkiniz yok.")
            Response.End()
        End If
        Dim Ad, Aciklama, Ikon As String
        Ad = KarakterKodla(HtmlKodla(txtKategoriAd.Text))
        Aciklama = KarakterKodla(HtmlKodla(txtKategoriAciklama.Text))
        Ikon = KarakterKodla(HtmlKodla(txtKategoriIkon.Text))
        sorgu = "INSERT INTO MakaleKategorileri(Ad,Aciklama,MakaleSayisi,Ikon) " & _
        "Values('" & Ad & "','" & Aciklama & "',0,'" & Ikon & "')"
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!" & ex.Message)
            Response.End()
        End Try
        Response.Redirect("makale.aspx?Sayfa=Kategoriler")
    End Sub
    Private Sub btnMakaleEkle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMakaleEkle.Click
        'Yetki kontrol ediliyor
        If Session("YetkiDuzeyi") < YD_Yonetici Then
            Response.Write("Bu iþlem için yetkiniz yok.")
            Response.End()
        End If
        'Formdan gelen veriler alýnýyor
        Dim Baslik, Aciklama, Icerik As String
        Dim KategoriNo, UyeNo As Integer
        If SayisalMi(Request.QueryString("KategoriNo"), 1, 1000000) Then
            KategoriNo = CInt(Request.QueryString("KategoriNo"))
        Else
            Response.Write("Makale kategorisi yok!")
            Response.End()
        End If
        If SayisalMi(Session("UyeNo"), 1, 2000000000) Then
            UyeNo = CInt(Session("UyeNo"))
        Else
            Response.Write("Zaman aþýmýndan dolayý oturum kapatýldý. Tekrar üye giriþi yapýn!")
            Response.End()
        End If
        Baslik = txtMakaleBaslik.Text
        Aciklama = txtMakaleAciklama.Text
        Icerik = txtMakaleIcerik.Text
        If Not chkMakaleHtmlKullan.Checked Then
            Baslik = HtmlKodla(Baslik)
            Aciklama = HtmlKodla(Aciklama)
            Icerik = HtmlKodla(Icerik)
        End If
        Baslik = KarakterKodla(Baslik)
        Aciklama = KarakterKodla(Aciklama)
        Icerik = KarakterKodla(Icerik)
        'Makale veritabanýna yazýlýyor
        sorgu = "INSERT INTO Makaleler(KategoriNo,Baslik,Aciklama,Icerik,UyeNo,Tarih,OkunmaSayisi,ToplamPuan,PuanVerenSayisi) " & _
        "Values(" & KategoriNo & ",'" & Baslik & "','" & Aciklama & "','" & Icerik & "'," & UyeNo & ",'" & MySqlUzunTarihYaz(Date.Now) & "',0,0,0)"
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        'Bu kategorideki makale sayýsý artýrýlýyor
        sorgu = "UPDATE MakaleKategorileri SET MakaleSayisi=MakaleSayisi+1 WHERE MakaleKategoriNo=" & KategoriNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Makale veritabanýna yazýldý ama kategorideki makale sayýsý artýrýlamadý!")
            Response.End()
        End Try
        Response.Redirect("makale.aspx?Sayfa=Listele&KategoriNo=" & KategoriNo)
    End Sub
End Class
