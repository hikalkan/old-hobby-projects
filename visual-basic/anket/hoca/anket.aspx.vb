Imports System.Data.Odbc
Public Class anket2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
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
    Dim HocaNo, DersNo, CevapNo As Long
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Sayfa As String = Request.QueryString("Sayfa")
        If SayisalMi(Session("HocaNo"), 1, 1000000000) And Session("KullaniciTuru") = "Hoca" Then
            HocaNo = Session("HocaNo")
            If SayisalMi(Request.QueryString("DersNo"), 1, 1000000000) Then
                DersNo = Request.QueryString("DersNo")
                Select Case Sayfa
                    Case "Ortalama"
                        Ortalama_Degerlendirme()
                    Case "TekTek"
                        If SayisalMi(Request.QueryString("CevapNo"), 1, 1000000000) Then
                            CevapNo = Request.QueryString("CevapNo")
                            TekTek_Degerlendirme()
                        End If
                    Case Else
                        ParametreHatasi()
                End Select
            Else
                lblHata.Text = "Bir ders seçmelisiniz."
            End If
        Else
            Response.Redirect("../default.aspx")
        End If
    End Sub
    Private Sub OnAyarlamalar()
    End Sub
    'Hatalý Parametre giriþinde hata mesajý verir
    Private Sub ParametreHatasi()
        lblHata.Text = "<p class='Girintili'>Parametre hatasý. Lütfen adres çubuðuna el ile parametre giriþi yapmayýn.</p>"
    End Sub
    Private Sub Ortalama_Degerlendirme()
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        Dim DersAd As String
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
        Sorgu = "SELECT HocaNo,Ad,Yil,Donem FROM Dersler WHERE DersNo=" & DersNo
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                DersAd = Okuyucu("Ad")
                Yil = Okuyucu("Yil")
                Donem = Okuyucu("Donem")
                If HocaNo = Okuyucu("HocaNo") Then
                    KendiDersiMi = True
                End If
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
                Baglantiyi_Kapat()
                Exit Sub
        End Select
        'Anket bilgileri veritabanýndan alýnýyor
        Dim AnketNo As Long = 0
        Sorgu = "SELECT AnketNo FROM Anketler WHERE Yil=" & Yil & " AND Donem=" & Donem
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader()
            If Okuyucu.Read Then
                AnketNo = Okuyucu("AnketNo")
            End If
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamýyor."
            Baglantiyi_Kapat()
            Exit Sub
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        If AnketNo = 0 Then
            lblHata.Text = "Bu ders için anket hazýrlanmamýþ.(" & Yil & " yýlý " & DonemMetin(Donem) & " için)"
            Baglanti.Close()
            Exit Sub
        End If
        Dim Anket As New clsDersAnket
        Dim Soru As clsAnketSorusu
        Dim Secenek As clsAnketSoruSecenekleri
        Anket.AnketNo = AnketNo
        Anket.DersNo = DersNo
        If Anket.VeriTabanindanAl() Then
            'Anket bilgileri gösteriliyor
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
            Tablo.Baslik = DersAd & " Dersi Anket Sonuçlarý"
            'Kolonlar oluþturuluyor
            Kolon = New clsKolonum
            Kolon.Baslik = "Soru"
            Kolon.Genislik = "55%"
            Kolon.icerikHizalama = "Left"
            Tablo.KolonEkle(Kolon)
            Kolon = New clsKolonum
            Kolon.Baslik = "Ort"
            Kolon.Genislik = "5%"
            Kolon.icerikHizalama = "center"
            Tablo.KolonEkle(Kolon)
            Kolon = New clsKolonum
            Kolon.Baslik = ""
            Kolon.Genislik = "40%"
            Kolon.icerikHizalama = "Left"
            Tablo.KolonEkle(Kolon)
            'Satýrlar oluþturuluyor
            Dim Genislik As Short
            Dim Renkler() As String = {"#9900FF", "#6600FF", "#3300FF", "#0000FF", "#0000CC", _
                                       "#3300CC", "#6600CC", "#3300CC", "#0000CC", "#0000FF"}
            Dim RenkNo As Byte
            Dim SoruNo As Byte = 0
            For Each Soru In Anket.Sorular
                Satir = New clsSatirim
                'Soru Metni ve seçenekler
                SoruNo += 1
                Hucre = New clsHucrem
                Hucre.Metin = "<b>[" & Format(SoruNo, "00") & "] " & Soru.SoruMetni & "</b>"
                RenkNo = 0
                For Each Secenek In Soru.Secenekler
                    Hucre.Metin += "<br><Font Color='" & Renkler(RenkNo) & "'>" & _
                    Secenek.Metin & "(" & Secenek.SayisalDeger & "); Oy Sayýsý: " & _
                    Secenek.OySayisi & "</Font>"
                    RenkNo += 1
                Next
                Satir.HucreEkle(Hucre)
                'Ortalama Puan
                Hucre = New clsHucrem
                Hucre.Metin = SayiDuzgunYaz(Soru.Ortalama, 2)
                Satir.HucreEkle(Hucre)
                'Çubuk grafiði
                'Çubuðun boyu hesaplanýyor
                If Soru.AzamiPuan > Soru.AsgariPuan Then
                    Genislik = CShort(((Soru.Ortalama - Soru.AsgariPuan) / (Soru.AzamiPuan - Soru.AsgariPuan)) * 250) + 14
                Else
                    Genislik = 100
                End If
                Hucre = New clsHucrem
                Hucre.Metin = "&nbsp<img src='../grafik/diger/cubuk_sol.gif' height='12' width='3'>" & _
                "<img src='../grafik/diger/cubuk_orta.gif' height='12' width='" & Genislik & "'>" & _
                "<img src='../grafik/diger/cubuk_ag.gif' height='12' width='3'>"
                Satir.HucreEkle(Hucre)
                Tablo.SatirEkle(Satir)
            Next 'For Each Soru
            Tablo.HtmlKoduUret()
            phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
            phOrta.Controls.Add(New LiteralControl("<p class='DuzYazi' Align='Center'>Anketi Dolduran Öðrenci Sayýsý: <b>" & Anket.CevapSayisi & "</b></p>"))
            'Anketleri tek tek görebilmek için DropDownList nesnesi oluþturuluyor
            Dim TabloKodu As String
            TabloKodu = "<p></p>" & _
            "<Table Width='90%' align='center' cellpadding='1' cellspacing='1' bgcolor='#000000'>" & _
            "<tr><td align='center' bgcolor='#DDDDDD' width='100%' class='DuzYazi'>Doldurulan anketleri tek tek görmek için: "
            phOrta.Controls.Add(New LiteralControl(TabloKodu))
            Dim ddlAnketCevaplari As New DropDownList
            Dim tblAnketCevaplari As New DataTable
            Dim drAnketCevap As DataRow
            Dim ListeElemani As ListItem
            ddlAnketCevaplari.ID = "ddlAnketCevaplari"
            ListeElemani = New ListItem("Seçin..", "0")
            ddlAnketCevaplari.Items.Add(ListeElemani)
            'ddlanketCevaplari dolduruluyor
            Sorgu = "SELECT CevapNo FROM AnketCevaplar WHERE AnketNo=" & AnketNo & " AND DersNo=" & DersNo & " AND Tur=2 ORDER BY CevapNo"
            Komut.CommandText = Sorgu
            Try
                Adaptor.Fill(tblAnketCevaplari)
                Dim AnketCevapNo As Long = 0
                For Each drAnketCevap In tblAnketCevaplari.Rows
                    AnketCevapNo += 1
                    ListeElemani = New ListItem(AnketCevapNo.ToString, drAnketCevap("CevapNo"))
                    ddlAnketCevaplari.Items.Add(ListeElemani)
                Next
            Catch ex As Exception
                'boþ
            End Try
            ddlAnketCevaplari.AutoPostBack = True
            AddHandler ddlAnketCevaplari.SelectedIndexChanged, AddressOf ddlAnketCevaplari_SeciliOgeDegistiginde
            phOrta.Controls.Add(ddlAnketCevaplari)
            TabloKodu = "</td></tr>" & _
            "</Table><p></p>"
            phOrta.Controls.Add(New LiteralControl(TabloKodu))
        Else
            lblHata.Text = "Anket bilgileri veritabanýndan alýnamadý."
            Baglantiyi_Kapat()
            Exit Sub
        End If
        'Baðlantý kapatýlýyor
        Baglantiyi_Kapat()
        lblGeri.Text = "[<a href='dersler.aspx?Sayfa=DersBilgileri&DersNo=" & DersNo & "'>Geri</a>]"
    End Sub
    Private Sub ddlAnketCevaplari_SeciliOgeDegistiginde(ByVal Sender As Object, ByVal E As EventArgs)
        Dim ddlAnketCevaplari As DropDownList = Sender
        If ddlAnketCevaplari.SelectedIndex > 0 Then
            If SayisalMi(ddlAnketCevaplari.SelectedItem.Value, 1, 1000000000) Then
                Response.Redirect("anket.aspx?Sayfa=TekTek&DersNo=" & Request.QueryString("DersNo") & "&CevapNo=" & ddlAnketCevaplari.SelectedItem.Value)
            End If
        End If
    End Sub
    Private Sub TekTek_Degerlendirme()
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        Dim DersAd As String
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
        Sorgu = "SELECT Ad,Yil,Donem FROM Dersler WHERE DersNo=" & DersNo & " AND HocaNo=" & HocaNo
        Komut.CommandText = Sorgu
        Try
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                DersAd = Okuyucu("Ad")
                Yil = Okuyucu("Yil")
                Donem = Okuyucu("Donem")
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
                Baglantiyi_Kapat()
                Exit Sub
        End Select
        'Hoca bu dersi görebilir. Þimdi anket bilgileri alýnýyor
        Dim Anket As New clsTekAnket
        Dim Soru As clsAnketSorusu
        Dim Secenek As clsAnketSoruSecenekleri
        Anket.CevapNo = CevapNo
        Anket.DersNo = DersNo
        If Anket.VeritabanindanAl Then
            'Þimdi Anket Gösteriliyor...
            Dim Kod, Kod2 As String
            Dim SatirNo As Byte = 0
            Kod = "<Table align='Center' width='910px' cellpadding='0' cellspacing='5' border='0'>"
            Kod += "<tr><td><h2>" & DersAd & "<h2></td></tr>"
            phOrta.Controls.Add(New LiteralControl(Kod))
            For Each Soru In Anket.Sorular
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
                Soru.SoruMetni & Chr(13) & _
                "    </b></font></p></td>" & Chr(13) & _
                "    <td width=""13px"" height=""24"">" & Chr(13) & _
                "    <img border=""0"" src=""../grafik/tablo1/anket_07.gif"" width=""13"" height=""24""></td>" & Chr(13) & _
                "  </tr>" & Chr(13) & _
                "  <tr>" & Chr(13) & _
                "    <td width=""4px"" background=""../grafik/tablo1/anket_08.gif""></td>" & Chr(13) & _
                "    <td width=""883px"" colspan=""3"" bgcolor=""#82A2CA""><p class=""DuzYazi""><font color=""#000000"">" & Chr(13)
                phOrta.Controls.Add(New LiteralControl(Kod))
                'Soru þýklarýna göre seçenekler oluþturuluyor
                Kod2 = ""
                For Each Secenek In Soru.Secenekler
                    If Secenek.OySayisi = 1 Then 'Seçili seçenek
                        Kod2 += "<Font Color='#FFFFFF'><b>(" & Secenek.SayisalDeger & ") " & Secenek.Metin & "</b></Font>"
                    Else 'Seçili olmayan seçenek
                        Kod2 += "(" & Secenek.SayisalDeger & ") " & Secenek.Metin
                    End If
                    Kod2 += "&nbsp&nbsp&nbsp&nbsp&nbsp"
                Next
                phOrta.Controls.Add(New LiteralControl(Kod2))
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
            'Yorum alaný oluþturuluyor...
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
            "Görüþ, dilek ya da þikayetler:" & Chr(13) & _
            "    </b></font></p></td>" & Chr(13) & _
            "    <td width=""13px"" height=""24"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_07.gif"" width=""13"" height=""24""></td>" & Chr(13) & _
            "  </tr>" & Chr(13) & _
            "  <tr>" & Chr(13) & _
            "    <td width=""4px"" background=""../grafik/tablo1/anket_08.gif""></td>" & Chr(13) & _
            "    <td width=""883px"" colspan=""3"" bgcolor=""#82A2CA""><p class=""DuzYazi"" Align=""Center""><font color=""#000000"">" & Chr(13)
            phOrta.Controls.Add(New LiteralControl(Kod))
            Kod2 = "<p class='Girintili'><Font Color='#FFFFFF'>" & Anket.Yorum & "</p>"
            phOrta.Controls.Add(New LiteralControl(Kod2))
            Kod = "    </font></p></td>" & Chr(13) & _
            "    <td width=""13px"" background=""../grafik/tablo1/anket_11.gif""></td>" & Chr(13) & _
            "  </tr>" & Chr(13) & _
            "  <tr>" & Chr(13) & _
            "    <td width=""883px"" colspan=""4"" height=""12px"" background=""../grafik/tablo1/anket_15.gif"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_14.gif"" width=""11"" height=""12""></td>" & Chr(13) & _
            "    <td width=""13px"" height=""12px"">" & Chr(13) & _
            "    <img border=""0"" src=""../grafik/tablo1/anket_17.gif"" width=""13"" height=""12""></td>" & Chr(13) & _
            "  </tr>" & Chr(13) & _
            "</table></td></tr></table><p class='DuzYazi' align='Center'>Cevap No: " & CevapNo & "</p>"
            phOrta.Controls.Add(New LiteralControl(Kod))
        Else
            lblHata.Text = "Veritabanýndan anket bilgileri alýnamadý."
        End If
        Baglantiyi_Kapat()
        lblGeri.Text = "[<a href='anket.aspx?Sayfa=Ortalama&DersNo=" & DersNo & "'>Geri</a>]"
    End Sub
    Private Sub Baglantiyi_Kapat()
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
End Class
