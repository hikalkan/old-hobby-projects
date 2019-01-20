Public Class haberler
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents txtHaberBaslik As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHaberAciklama As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtHaberMetni As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnHaberTamam As System.Web.UI.WebControls.Button
    Protected WithEvents divHaberYazFormu As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents frmHaberYaz As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents chkHtmlKodu As System.Web.UI.WebControls.CheckBox

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
    Const BirSayfadakiHaberSayisi As Integer = 10
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        divHaberYazFormu.Visible = False
        Dim Sayfa As String = Request.QueryString("Sayfa")
        Select Case Sayfa
            Case "Listele"
                HaberleriListele()
            Case "Oku"
                If SayisalMi(Request.QueryString("HaberNo"), 1, 2000000000) Then
                    HaberOku(CInt(Request.QueryString("HaberNo")))
                End If
            Case "HaberYaz"
                If Session("YetkiDuzeyi") > YD_Uye Then
                    divHaberYazFormu.Visible = True
                Else
                    Response.Write("Bu iþlem için yetkiniz yok!")
                End If
            Case "Sil"
                If Session("YetkiDuzeyi") > YD_Uye Then
                    If SayisalMi(Request.QueryString("HaberNo"), 1, 2000000000) Then
                        HaberSil(CInt(Request.QueryString("HaberNo")))
                    End If
                Else
                    Response.Write("Bu iþlem için yetkiniz yok!")
                End If
        End Select
    End Sub
    Private Sub HaberSil(ByVal HaberNo As Integer)
        sorgu = "DELETE FROM Haberler WHERE HaberNo=" & HaberNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        Response.Redirect("haberler.aspx?Sayfa=Listele")
    End Sub
    Private Sub HaberleriListele()
        Dim kod As String = ""
        Dim SayfaNo, SayfaSayisi, HaberSayisi As Integer
        If SayisalMi(Request.QueryString("SayfaNo"), 1, 200000000) Then
            SayfaNo = CInt(Request.QueryString("SayfaNo"))
        Else
            SayfaNo = 1
        End If
        'Toplam haber sayýsý bulunuyor..
        sorgu = "SELECT Count(*) AS HaberSayisi FROM Haberler"
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            okuyucu = komut.ExecuteReader
            If okuyucu.Read Then
                HaberSayisi = okuyucu("HaberSayisi")
            Else
                Response.Write("Hiç haber yok!")
                Response.End()
            End If
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        'Haberler alýnýyor
        sorgu = "SELECT HaberNo,Baslik,Aciklama,Tarih FROM Haberler ORDER BY Tarih DESC"
        komut.CommandText = sorgu
        Dim Haberler As New DataSet
        Dim birHaber As DataRow
        Try
            adaptor.Fill(Haberler, (SayfaNo - 1) * BirSayfadakiHaberSayisi, BirSayfadakiHaberSayisi, 0)
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        'Gerekli hesaplamalar..
        SayfaSayisi = CInt(HaberSayisi / BirSayfadakiHaberSayisi)
        If HaberSayisi > BirSayfadakiHaberSayisi * SayfaSayisi Then
            SayfaSayisi += 1
        End If
        'Tablonun kodlarý oluþturuluyor...
        kod = kod & "<Table align='center' width='100%' cellpadding='0' cellspacing='3' border='0'>"
        If Session("YetkiDuzeyi") > YD_Uye Then
            kod = kod & "<tr><td class='Sade' width='100%'><p class='Sade' align='right'>"
            kod = kod & "<a href='haberler.aspx?Sayfa=HaberYaz'>Haber Yaz</a>"
            kod = kod & "</p></td></tr>"
        End If
        For Each birHaber In Haberler.Tables(0).Rows
            kod = kod & "<tr><td width='100%'>"
            kod = kod & "<Table align='center' width='100%' cellpadding='1' cellspacing='1' bgcolor='#444444' border='0'>"
            kod = kod & "<tr>"
            kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#294963'>"
            kod = kod & "<p style='color:#FFFFFF'>&nbsp<img src='images/diger/sagaok.gif' height='11px' width='15px' border='0'> <b>" & KarakterKodCoz(birHaber("Baslik")) & "</b></p>"
            kod = kod & "</td>"
            kod = kod & "</tr>"
            kod = kod & "<tr>"
            kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
            kod = kod & "<p class='Normal'>" & KarakterKodCoz(birHaber("Aciklama")) & "</p>"
            kod = kod & "</td>"
            kod = kod & "</tr>"
            kod = kod & "<tr>"
            kod = kod & "<td width='100%' bgcolor='#919CB6'>"
            kod = kod & "<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>"
            kod = kod & "<tr><td class='Sade' width='50%' align='left'>"
            kod = kod & "<p style='color:#FFFFFF'><b>" & MySqlUzunTarihAl(birHaber("Tarih")).ToLongDateString & "</b></p>"
            kod = kod & "</td><td class='Sade' width='50%' align='right'>"
            kod = kod & "<a href='haberler.aspx?Sayfa=Oku&HaberNo=" & birHaber("HaberNo") & "'><img src='images/diger/devam.gif' height='15px' width='65px' border='0'></a>"
            kod = kod & "</td></tr>"
            kod = kod & "</table>"
            kod = kod & "</td>"
            kod = kod & "</tr>"
            kod = kod & "</table>"
            kod = kod & "</td></tr>"
        Next
        'Sayfa numaralarý..
        Dim i As Integer
        kod = kod & "<tr><td class='Sade' width='100%'><p align='center'>"
        If SayfaNo > 1 Then
            kod = kod & "<a href='haberler.aspx?Sayfa=Listele&SayfaNo=" & SayfaNo - 1 & "'>&lt&lt</a> "
        End If
        For i = 1 To SayfaSayisi
            If SayfaNo <> i Then
                kod = kod & "<a href='haberler.aspx?Sayfa=Listele&SayfaNo=" & i & "'>" & i & "</a> "
            Else
                kod = kod & i & " "
            End If
        Next
        If SayfaNo < SayfaSayisi Then
            kod = kod & "<a href='haberler.aspx?Sayfa=Listele&SayfaNo=" & SayfaNo + 1 & "'>&gt&gt</a>"
        End If
        kod = kod & "<br><a href='anasayfa.aspx'><img src='images/diger/geridon.gif' border='0'></a></p>"
        kod = kod & "</td></tr>"
        kod = kod & "</table>"
        phOrta.Visible = True
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub HaberOku(ByVal HaberNo As Integer)
        Dim Baslik, Icerik, Yazan As String
        Dim Tarih As New Date
        Dim UyeNo, OkunmaSayisi As Integer
        sorgu = "SELECT Baslik,Icerik,Tarih,UyeNo,OkunmaSayisi FROM Haberler WHERE HaberNo=" & HaberNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            okuyucu = komut.ExecuteReader
            If okuyucu.Read Then
                Baslik = KarakterKodCoz(okuyucu("Baslik"))
                Icerik = KarakterKodCoz(okuyucu("Icerik"))
                UyeNo = okuyucu("UyeNo")
                Tarih = MySqlUzunTarihAl(okuyucu("Tarih"))
                OkunmaSayisi = okuyucu("OkunmaSayisi")
            Else
                Response.Write("Haber bulunamadý!")
                Response.End()
            End If
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        'Haberi Yazanýn adý bulunuyor..
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
        sorgu = "UPDATE Haberler SET OkunmaSayisi=OkunmaSayisi+1 WHERE HaberNo=" & HaberNo
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        'Haber yazýlýyor..
        Dim kod As String
        kod = kod & "<Table align='center' width='100%' cellpadding='3' cellspacing='1' bgcolor='#444444' border='0'>"
        kod = kod & "<tr>"
        kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#294963'>"
        kod = kod & "<p style='color:#FFFFFF'><b>" & Baslik & "</b>"
        If Session("YetkiDuzeyi") >= YD_Yonetici Then
            kod = kod & " (<a href='haberler.aspx?Sayfa=Sil&HaberNo=" & HaberNo & "'><font style='color:#DDDDDD'>Sil</font></a>)"
        End If
        kod = kod & "</p>"
        kod = kod & "</td>"
        kod = kod & "</tr>"
        kod = kod & "<tr>"
        kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#ebebff'>"
        kod = kod & "<p class='Normal'>" & Icerik & "</p>"
        kod = kod & "</td>"
        kod = kod & "</tr>"
        kod = kod & "<tr>"
        kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#919CB6'>"
        kod = kod & "<p style='color:#FFFFFF'>Tarih: <b>" & Tarih.ToLongDateString & "</b>; Okunma Sayýsý:<b>" & (OkunmaSayisi + 1).ToString & "</b>; Yazan:<a href='Uyelik.aspx?Sayfa=UyeGoster&UyeNo=" & UyeNo & "'><b>" & Yazan & "</b></a></p>"
        kod = kod & "</td>"
        kod = kod & "</tr>"
        kod = kod & "</table>"
        kod = kod & "<p align='center'><a href='javascript:history.back()'><img src='images/diger/geridon.gif' border='0'></a></p>"
        phOrta.Visible = True
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub btnHaberTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHaberTamam.Click
        'Yetki düzeyi kontrolü
        If Session("YetkiDuzeyi") <= YD_Uye Or Session("UyeNo") <= 0 Then
            Response.Write("Bu sayfaya girme yetkiniz yok!")
            Response.End()
        End If
        Dim Baslik, Aciklama, Metin As String
        If Not chkHtmlKodu.Checked Then
            Baslik = HtmlKodla(Baslik)
            Aciklama = HtmlKodla(Aciklama)
            Metin = HtmlKodla(Metin)
        End If
        Baslik = KarakterKodla(txtHaberBaslik.Text)
        Aciklama = KarakterKodla(txtHaberAciklama.Text)
        Metin = KarakterKodla(txtHaberMetni.Text)
        sorgu = "INSERT INTO Haberler(Baslik,Aciklama,Icerik,UyeNo,Tarih,OkunmaSayisi) " & _
        "Values('" & Baslik & "','" & Aciklama & "','" & Metin & "'," & Session("UyeNo") & _
        ",'" & MySqlUzunTarihYaz(Date.Now) & "',0)"
        komut.CommandText = sorgu
        Try
            baglanti.Open()
            komut.ExecuteNonQuery()
            sorgu = "INSERT INTO Duyurular(Baslik,Metin,Tarih,KategoriNo) " & _
            "Values('" & Baslik & "','" & Metin & "','" & MySqlUzunTarihYaz(Date.Now) & _
            "'," & KN_Haber & ")"
            komut.CommandText = sorgu
            komut.ExecuteNonQuery()
            baglanti.Close()
        Catch ex As Exception
            Response.Write("Veritabanýna baðlanýlamadý!")
            Response.End()
        End Try
        Response.Redirect("haberler.aspx?Sayfa=Listele")
    End Sub
End Class
