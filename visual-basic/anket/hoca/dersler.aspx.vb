Imports System.Data.Odbc
Public Class dersler1
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents divDersBilgileri As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lblHocaAd As System.Web.UI.WebControls.Label
    Protected WithEvents lblSinif As System.Web.UI.WebControls.Label
    Protected WithEvents lblDonem As System.Web.UI.WebControls.Label
    Protected WithEvents lblYil As System.Web.UI.WebControls.Label
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblDersAd As System.Web.UI.WebControls.Label
    Protected WithEvents btnTamam As System.Web.UI.WebControls.Button
    Protected WithEvents lblGeri As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim DersNo As Long = 0
    Dim HocaNo As Long = 0
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OnAyarlamalar()
        Dim Sayfa As String = Request.QueryString("Sayfa")
        If SayisalMi(Session("HocaNo"), 1, 1000000000) And Session("KullaniciTuru") = "Hoca" Then
            HocaNo = Session("HocaNo")
            If SayisalMi(Request.QueryString("DersNo"), 1, 1000000000) Then
                DersNo = Request.QueryString("DersNo")
                Select Case Sayfa
                    Case "DersBilgileri"
                        DersBilgileri()
                    Case "NotlariGir"
                        VizeFinalNotuAciklamaFormu_Hazirla()
                End Select
            Else
                lblHata.Text = "Bir ders seçmelisiniz."
            End If
        Else
            Response.Redirect("../default.aspx")
        End If
    End Sub
    Private Sub OnAyarlamalar()
        divDersBilgileri.Visible = False
        btnTamam.Visible = False
    End Sub
    Private Sub DersBilgileri()
        Dim YetkiDuzeyi As Byte
        Dim KendiDersiMi As Boolean = False
        Dim HataKodu As Byte = 0
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Komut.Connection = Baglanti
        Dim Adaptor As New OdbcDataAdapter(Komut)
        If SayisalMi(Session("YetkiDuzeyi"), HYD_KendiDersi, HYD_Yonetici) Then
            YetkiDuzeyi = Session("YetkiDuzeyi")
        Else
            Response.Redirect("../default.aspx")
        End If
        'Veritabanýna baðlanýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamýyor."
            Exit Sub
        End Try
        'Bu Hocanýn bu dersi görüp göremeyeceðine bakýlýyor
        Sorgu = "SELECT DersNo FROM Dersler WHERE DersNo=" & DersNo & " AND HocaNo=" & HocaNo
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                KendiDersiMi = True
            Else
                HataKodu = 1
            End If
        Catch ex As Exception
            HataKodu = 2
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        Select Case HataKodu
            Case 1
                If YetkiDuzeyi <= HYD_KendiDersi Then
                    lblHata.Text = "Bu dersi görme yetkiniz yok"
                    Baglanti.Close()
                    Exit Sub
                End If
            Case 2
                lblHata.Text = "Veritabanýndan ders bilgileri alýnamadý."
                Try
                    Baglanti.Close()
                Catch ex As Exception
                    'boþ
                End Try
                Exit Sub
        End Select
        'Ders bilgileri veritabanýndan alýnýyor..
        Dim DersVar As Boolean = False
        Sorgu = "Select Dersler.DersNo,Dersler.HocaNo," & _
        "CONCAT(Hocalar.Unvan,' ',Hocalar.Ad,' ',Hocalar.Soyad) AS HocaAd," & _
        "Dersler.Ad,Dersler.Sinif,Dersler.Donem,Dersler.Yil FROM Hocalar " & _
        "INNER JOIN Dersler ON Dersler.HocaNo=Hocalar.HocaNo WHERE Dersler.DersNo=" & DersNo
        Komut.CommandText = Sorgu
        Dim Ogrenciler As New DataTable("Ogrenciler")
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                lblDersAd.Text = Okuyucu("Ad")
                lblHocaAd.Text = Okuyucu("HocaAd")
                lblSinif.Text = Okuyucu("Sinif")
                lblDonem.Text = Okuyucu("Donem")
                lblYil.Text = Okuyucu("Yil")
                DersVar = True
            End If
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamýyor." & ex.Message
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If DersVar Then
            divDersBilgileri.Visible = True
            'Veritabanýndan kayýtlar alýnýyor
            Sorgu = "SELECT Ogrenciler.OgrenciNo,CONCAT(Ad,' ',Soyad) AS Ad,Numara,VizeNotu,FinalNotu " & _
            "FROM Ogrenciler,DersOgrenciIliskileri " & _
            "WHERE Ogrenciler.OgrenciNo=DersOgrenciIliskileri.OgrenciNo AND DersNo=" & DersNo & _
            " ORDER BY Numara"
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(Ogrenciler)
            Catch ex As Exception
                lblHata.Text = "Veritabanýna baðlanýlamadý.<br>"
                If Not (Komut Is Nothing) Then Komut.Dispose()
                Baglanti.Close()
                Exit Sub
            End Try
            If Ogrenciler.Rows.Count > 0 Then
                'kayýtlarý göstermek için html tablosu oluþturuluyor...
                Dim Ogrenci As DataRow
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
                Tablo.Baslik = "Bu Dersi Alan Öðrenciler"
                'Kolonlar oluþturuluyor
                Kolon = New clsKolonum
                Kolon.Baslik = "Numara"
                Kolon.Genislik = "20%"
                Kolon.icerikHizalama = "center"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Adý Soyadý"
                Kolon.Genislik = "70%"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Vize"
                Kolon.Genislik = "5%"
                Kolon.icerikHizalama = "Center"
                Tablo.KolonEkle(Kolon)
                Kolon = New clsKolonum
                Kolon.Baslik = "Final"
                Kolon.Genislik = "5%"
                Kolon.icerikHizalama = "Center"
                Tablo.KolonEkle(Kolon)
                'Satýrlar oluþturuluyor
                For Each Ogrenci In Ogrenciler.Rows
                    Satir = New clsSatirim
                    Hucre = New clsHucrem
                    Hucre.Metin = Ogrenci("Numara")
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    Hucre.Metin = Ogrenci("Ad")
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    If Ogrenci("VizeNotu") < 101 Then
                        Hucre.Metin = Ogrenci("VizeNotu")
                    Else
                        Hucre.Metin = "??"
                    End If
                    Satir.HucreEkle(Hucre)
                    Hucre = New clsHucrem
                    If Ogrenci("FinalNotu") < 101 Then
                        Hucre.Metin = Ogrenci("FinalNotu")
                    Else
                        Hucre.Metin = "??"
                    End If
                    Satir.HucreEkle(Hucre)
                    Tablo.SatirEkle(Satir)
                Next
                Tablo.HtmlKoduUret()
                Dim TabloKodu As String
                TabloKodu = "<p class='DuzYazi'></p>" & _
                "<Table Width='90%' align='center' cellpadding='1' cellspacing='1' bgcolor='#000000'>" & _
                "<tr><td align='center' bgcolor='#DDDDDD' width='100%' class='DuzYazi'><a href='anket.aspx?Sayfa=Ortalama&DersNo=" & DersNo & "'>" & _
                "- BU DERSÝN ANKETÝNÝ GÖRMEK ÝÇÝN TIKLAYIN -" & _
                "</a></td></tr>" & _
                "</Table>" & _
                "<p class='DuzYazi'></p>"
                phOrta.Controls.Add(New LiteralControl(TabloKodu))
                If KendiDersiMi Then
                    TabloKodu = "<p class='DuzYazi'></p>" & _
                    "<Table Width='90%' align='center' cellpadding='1' cellspacing='1' bgcolor='#000000'>" & _
                    "<tr><td align='center' bgcolor='#DDDDDD' width='100%' class='DuzYazi'><a href='dersler.aspx?Sayfa=NotlariGir&DersNo=" & DersNo & "'>" & _
                    "- VÝZE VE/VEYA FÝNAL NOTLARINI GÝRMEK ÝÇÝN TIKLAYIN -" & _
                    "</a></td></tr>" & _
                    "</Table>" & _
                    "<p class='DuzYazi'></p>"
                    phOrta.Controls.Add(New LiteralControl(TabloKodu))
                End If
                phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
            Else
                lblHata.Text = "Bu derse kayýtlý herhangi bir öðrenci bulunamadý."
            End If
        Else
            lblHata.Text = "Ders bulunamadý."
        End If
        'Baðlantý kapatýlýyor
        Baglantiyi_Kapat()
        lblGeri.Text = "[<a href='default.aspx'>Geri</a>]"
    End Sub
    Private Sub VizeFinalNotuAciklamaFormu_Hazirla()
        Dim YetkiDuzeyi As Byte
        Dim HataKodu As Byte = 0
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Komut.Connection = Baglanti
        Dim Adaptor As New OdbcDataAdapter(Komut)
        If SayisalMi(Session("YetkiDuzeyi"), HYD_KendiDersi, HYD_Yonetici) Then
            YetkiDuzeyi = Session("YetkiDuzeyi")
        Else
            Response.Redirect("../default.aspx")
        End If
        'Veritabanýna baðlanýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamýyor."
            Exit Sub
        End Try
        'Bu Hocanýn bu dersi görüp göremeyeceðine bakýlýyor
        Dim DersAd As String
        Sorgu = "SELECT Ad FROM Dersler WHERE DersNo=" & DersNo & " AND HocaNo=" & HocaNo
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                DersAd = Okuyucu("Ad")
                HataKodu = 0
            Else
                HataKodu = 1
            End If
        Catch ex As Exception
            HataKodu = 2
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        Select Case HataKodu
            Case 1
                lblHata.Text = "Bu dersin notlarýný sadece dersi veren hoca girebilir."
                Baglantiyi_Kapat()
                Exit Sub
            Case 2
                lblHata.Text = "Veritabanýndan ders bilgileri alýnamadý."
                Baglantiyi_Kapat()
                Exit Sub
        End Select
        'Dersi alan öðrenciler veritabanýndan alýnýyor
        Dim tblOgrenciler As New DataTable
        Dim drOgrenci As DataRow
        Sorgu = "SELECT Ogrenciler.Numara,CONCAT(Ogrenciler.Ad,' ',Ogrenciler.Soyad) AS OgrenciAd,DersOgrenciIliskileri.OgrenciNo," & _
        "DersOgrenciIliskileri.VizeNotu,DersOgrenciIliskileri.FinalNotu FROM " & _
        "Ogrenciler,DersOgrenciIliskileri WHERE DersNo=" & DersNo & " AND " & _
        "DersOgrenciIliskileri.OgrenciNo=Ogrenciler.OgrenciNo"
        Komut.CommandText = Sorgu
        Try
            Adaptor.Fill(tblOgrenciler)
        Catch ex As Exception
            lblHata.Text = "Veritabanýndan öðrenci bilgileri alýnamadý."
            Baglantiyi_Kapat()
            Exit Sub
        End Try
        If tblOgrenciler.Rows.Count <= 0 Then
            lblHata.Text = "Bu derse kayýtlý öðrenci yok."
            Baglantiyi_Kapat()
            Exit Sub
        End If
        'Sonuçlarýn girilmesi için form hazýrlanýyor
        Dim TabloKodu As String
        TabloKodu = "<Table Align='Center' CellPadding='1' CellSpacing='1' Width='90%' bgcolor='#000000'>" & _
        "<tr><td align='Center' class='DuzYazi' width='100%' bgcolor='#294963' colspan='4'><Font Color='#FFFFFF'><b>" & DersAd & " Dersi Vize ve Final Notlarý</b></Font></td></tr>" & _
        "<tr><td align='Center' class='DuzYazi' width='20%' bgcolor='#919CB6'><Font Color='#FFFFFF'><b>Numara</b></Font></td>" & _
        "<td align='Left' class='DuzYazi' width='60%' bgcolor='#919CB6'><Font Color='#FFFFFF'><b>Adý Soyadý</b></Font></td>" & _
        "<td align='Center' class='DuzYazi' width='10%' bgcolor='#919CB6'><Font Color='#FFFFFF'><b>Vize</b></Font></td>" & _
        "<td align='Center' class='DuzYazi' width='10%' bgcolor='#919CB6'><Font Color='#FFFFFF'><b>Final</b></Font></td></tr>"
        phOrta.Controls.Add(New LiteralControl(TabloKodu))
        'Her öðrenci için bir satýr oluþturuluyor
        Dim ArkaRenk As String
        Dim tb As TextBox
        For Each drOgrenci In tblOgrenciler.Rows
            If ArkaRenk = "#EEEEEE" Then ArkaRenk = "#DDDDDD" Else ArkaRenk = "#EEEEEE"
            TabloKodu = "<tr><td align='Center' class='DuzYazi' width='20%' bgcolor='" & ArkaRenk & "'>" & drOgrenci("Numara") & "</td>" & _
            "<td align='Left' class='DuzYazi' width='60%' bgcolor='" & ArkaRenk & "'>" & drOgrenci("OgrenciAd") & "</td>" & _
            "<td align='Center' class='DuzYazi' width='10%' bgcolor='" & ArkaRenk & "'>"
            phOrta.Controls.Add(New LiteralControl(TabloKodu))
            'Vize notunun girilmesi için textbox
            tb = New TextBox
            tb.ID = "txtVize" & drOgrenci("OgrenciNo")
            tb.MaxLength = 3
            tb.Width = New UI.WebControls.Unit("35px")
            If CByte(drOgrenci("VizeNotu")) < 101 Then
                tb.Text = drOgrenci("VizeNotu")
            Else
                tb.Text = ""
            End If
            phOrta.Controls.Add(tb)
            TabloKodu = "</td><td align='Center' class='DuzYazi' width='10%' bgcolor='" & ArkaRenk & "'>"
            phOrta.Controls.Add(New LiteralControl(TabloKodu))
            'Final notunun girilmesi için textbox
            tb = New TextBox
            tb.ID = "txtFinal" & drOgrenci("OgrenciNo")
            tb.MaxLength = 3
            tb.Width = New UI.WebControls.Unit("35px")
            If CByte(drOgrenci("FinalNotu")) < 101 Then
                tb.Text = drOgrenci("FinalNotu")
            Else
                tb.Text = ""
            End If
            phOrta.Controls.Add(tb)
            TabloKodu = "</td></tr>"
            phOrta.Controls.Add(New LiteralControl(TabloKodu))
        Next
        TabloKodu = "</Table>"
        phOrta.Controls.Add(New LiteralControl(TabloKodu))
        Baglantiyi_Kapat()
        btnTamam.Visible = True
        lblGeri.Text = "[<a href='dersler.aspx?Sayfa=DersBilgileri&DersNo=" & DersNo & "'>Geri</a>]"
    End Sub
    Private Sub Baglantiyi_Kapat()
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
    Private Sub btnTamam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTamam.Click
        Dim YetkiDuzeyi As Byte
        Dim HataKodu As Byte = 0
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Komut.Connection = Baglanti
        If SayisalMi(Session("YetkiDuzeyi"), HYD_KendiDersi, HYD_Yonetici) Then
            YetkiDuzeyi = Session("YetkiDuzeyi")
        Else
            Response.Redirect("../default.aspx")
        End If
        'Veritabanýna baðlanýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamýyor."
            Exit Sub
        End Try
        'Bu Hocanýn bu dersi görüp göremeyeceðine bakýlýyor
        Dim DersAd As String
        Sorgu = "SELECT Ad FROM Dersler WHERE DersNo=" & DersNo & " AND HocaNo=" & HocaNo
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                DersAd = Okuyucu("Ad")
                HataKodu = 0
            Else
                HataKodu = 1
            End If
        Catch ex As Exception
            HataKodu = 2
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        Select Case HataKodu
            Case 1
                lblHata.Text = "Bu dersin notlarýný sadece dersi veren hoca girebilir."
                Baglantiyi_Kapat()
                Exit Sub
            Case 2
                lblHata.Text = "Veritabanýndan ders bilgileri alýnamadý."
                Baglantiyi_Kapat()
                Exit Sub
        End Select
        'Dersi alan öðrenciler veritabanýndan alýnýyor
        Dim tblOgrenciler As New DataTable("DersOgrenciIliskileri")
        Dim drOgrenci As DataRow
        Dim Adaptor As New OdbcDataAdapter("SELECT DersOgrenciIliskiNo,OgrenciNo,VizeNotu,FinalNotu FROM DersOgrenciIliskileri WHERE DersNo=" & DersNo, Baglanti)
        Dim Komut_Guncelleme As New OdbcCommandBuilder(Adaptor)
        Try
            Adaptor.Fill(tblOgrenciler)
        Catch ex As Exception
            lblHata.Text = "Veritabanýndan öðrenci bilgileri alýnamadý."
            Baglantiyi_Kapat()
            Exit Sub
        End Try
        If tblOgrenciler.Rows.Count <= 0 Then
            lblHata.Text = "Bu derse kayýtlý öðrenci yok."
            Baglantiyi_Kapat()
            Exit Sub
        End If
        Dim VizeNotu, FinalNotu As Byte
        Dim KA As String
        Dim Degisti As Boolean = False
        Try
            For Each drOgrenci In tblOgrenciler.Rows
                Degisti = False
                KA = "txtVize" & drOgrenci("OgrenciNo")
                If Request.Form(KA) <> "" Then
                    If SayisalMi(Request.Form(KA), 0, 100) Then
                        VizeNotu = CByte(Request.Form(KA))
                        drOgrenci("VizeNotu") = VizeNotu
                        Degisti = True
                    End If
                End If
                KA = "txtFinal" & drOgrenci("OgrenciNo")
                If Request.Form(KA) <> "" Then
                    If SayisalMi(Request.Form(KA), 0, 100) Then
                        FinalNotu = CByte(Request.Form(KA))
                        drOgrenci("FinalNotu") = FinalNotu
                        Degisti = True
                    End If
                End If
                If Degisti Then
                    Sorgu = "UPDATE DersOgrenciIliskileri SET VizeNotu=" & drOgrenci("VizeNotu") & ",FinalNotu=" & drOgrenci("FinalNotu") & _
                    " WHERE DersOgrenciIliskiNo=" & drOgrenci("DersOgrenciIliskiNo")
                    Komut.CommandText = Sorgu
                    Komut.ExecuteNonQuery()
                End If
            Next
        Catch ex As Exception
            'boþ
        End Try
        'Deðiþiklikler veritabanýna kaydediliyor
        'Try
        '    Adaptor.Update(tblOgrenciler.GetChanges(DataRowState.Modified))
        'Catch ex As Exception
        '    'boþ
        'End Try
        Baglantiyi_Kapat()
        Response.Redirect("dersler.aspx?Sayfa=DersBilgileri&DersNo=" & Request.QueryString("DersNo"))
    End Sub
End Class
