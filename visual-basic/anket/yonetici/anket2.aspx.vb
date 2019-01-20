Imports System.Data.Odbc
Public Class anket21
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents lblHata As System.Web.UI.WebControls.Label

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
        If GirisYapilmisMi() Then
            Dim Sayfa As String
            Sayfa = Request.QueryString("Sayfa")
            Select Case Sayfa
                Case "Listele"
                    AnketleriListele()
                Case "Sil"
                    Dim AnketNo As Long
                    If SayisalMi(Request.QueryString("AnketNo"), 1, 1000000000) Then
                        AnketNo = CInt(Request.QueryString("AnketNo"))
                        AnketSil(AnketNo)
                    End If
                Case "AnketGor_Liste"
                    AnketGor_Liste()
                Case "AnketGor"
                    Dim AnketNo As Long
                    If SayisalMi(Request.QueryString("AnketNo"), 1, 1000000000) Then
                        AnketNo = CInt(Request.QueryString("AnketNo"))
                        AnketGor(AnketNo)
                    End If
                Case "AnketGor_TekTek"
                    Dim CevapNo As Long
                    If SayisalMi(Request.QueryString("CevapNo"), 1, 1000000000) Then
                        CevapNo = Request.QueryString("CevapNo")
                        AnketGor_TekTek(CevapNo)
                    End If
                Case Else
                    ParametreHatasi()
            End Select
        Else
            Response.Redirect("../default.aspx")
        End If
    End Sub
    'Kullanýncý giriþi yapýlýp yapýlmadýðýna bakýlýyor
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
    'Hatalý Parametre giriþinde hata mesajý verir
    Private Sub ParametreHatasi()
        lblHata.Text = "<p class='Girintili'>Parametre hatasý. Lütfen adres çubuðuna el ile parametre giriþi yapmayýn.</p>"
    End Sub
    Private Sub AnketleriListele()
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Komut.Connection = Baglanti
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim tblAnketler As New DataTable
        Dim drAnket As DataRow
        Sorgu = "SELECT * FROM Anketler ORDER BY Yil DESC,Donem DESC"
        Komut.CommandText = Sorgu
        'Baðlantý açýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý."
            Exit Sub
        End Try
        'Veritabanýndan tüm anketler alýnýyor
        Try
            Adaptor.Fill(tblAnketler)
        Catch ex As Exception
            lblHata.Text = "Anket bilgileri veritabanýndan alýnamadý."
            Baglantiyi_Kapat()
            Exit Sub
        End Try
        Baglantiyi_Kapat()
        'Anketler gösteriliyor
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
        'Tablo baþlýðý ve biçim atamasý
        Tablo.Bicim = Bicim
        Tablo.Baslik = "A N K E T L E R"
        'Kolonlar oluþturuluyor
        Kolon = New clsKolonum
        Kolon.Baslik = "Açýklama"
        Kolon.Genislik = "70%"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Yýl"
        Kolon.Genislik = "10%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Dönem"
        Kolon.Genislik = "10%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = ""
        Kolon.Genislik = "10%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        'Satýrlar oluþturuluyor
        For Each drAnket In tblAnketler.Rows
            Satir = New clsSatirim
            Hucre = New clsHucrem
            Hucre.Metin = MetinKisalt(drAnket("Aciklama"), 50)
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = drAnket("Yil")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = drAnket("Donem")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = "<a href='anket2.aspx?Sayfa=Sil&AnketNo=" & drAnket("AnketNo") & "'>Sil</a>"
            Satir.HucreEkle(Hucre)
            Tablo.SatirEkle(Satir)
        Next
        Tablo.HtmlKoduUret()
        phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
    End Sub
    Private Sub AnketSil(ByVal AnketNo As Long)
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Komut.Connection = Baglanti
        'Baðlantý açýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý."
            Exit Sub
        End Try
        'Bu ankete þimdiye kadar hiç cevap verilmiþ mi diye bakýlýyor
        Dim CevapVar As Byte = 0
        Sorgu = "SELECT COUNT(*) AS CevapSayisi FROM AnketCevaplar WHERE AnketNo=" & AnketNo
        Try
            Komut.CommandText = Sorgu
            Okuyucu = Komut.ExecuteReader
            If Okuyucu.Read Then
                If Okuyucu("CevapSayisi") > 0 Then
                    CevapVar = 1
                End If
            End If
        Catch ex As Exception
            CevapVar = 2 'hata
        Finally
            If Not (Okuyucu Is Nothing) Then Okuyucu.Close()
        End Try
        Select Case CevapVar
            Case 1
                lblHata.Text = "<p class='Girintili'>Bu anketi silemezsiniz çünkü bu ankete cevap verilmiþ.</p>"
                Baglantiyi_Kapat()
                Exit Sub
            Case 2
                lblHata.Text = "Veritabanýna baðlanýlamadý."
                Baglantiyi_Kapat()
                Exit Sub
        End Select
        'Anket bilgileri veritabanýndan siliniyor
        Try
            Sorgu = "DELETE FROM Anketler WHERE AnketNo=" & AnketNo
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            Sorgu = "DELETE FROM AnketSorulari WHERE AnketNo=" & AnketNo
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
            Sorgu = "DELETE FROM AnketSoruSecenekleri WHERE AnketNo=" & AnketNo
            Komut.CommandText = Sorgu
            Komut.ExecuteNonQuery()
        Catch ex As Exception
            lblHata.Text = "Anketi silerken bir hata oluþtu, veritabanýna baðlanýlamamýþ olabilir."
            Baglantiyi_Kapat()
            Exit Sub
        End Try
        Baglantiyi_Kapat()
        Response.Redirect("anket2.aspx?Sayfa=Listele")
    End Sub
    Private Sub AnketGor_Liste()
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Komut.Connection = Baglanti
        Dim Adaptor As New OdbcDataAdapter(Komut)
        Dim tblAnketler As New DataTable
        Dim drAnket As DataRow
        Sorgu = "SELECT AnketNo,Yil,Donem FROM Anketler ORDER BY Yil DESC,Donem DESC"
        Komut.CommandText = Sorgu
        'Baðlantý açýlýyor
        Try
            Baglanti.Open()
        Catch ex As Exception
            lblHata.Text = "Veritabanýna baðlanýlamadý."
            Exit Sub
        End Try
        'Veritabanýndan tüm anketler alýnýyor
        Try
            Adaptor.Fill(tblAnketler)
        Catch ex As Exception
            lblHata.Text = "Anket bilgileri veritabanýndan alýnamadý."
            Baglantiyi_Kapat()
            Exit Sub
        End Try
        Baglantiyi_Kapat()
        'Anketler gösteriliyor
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
        Bicim.Genislik = "50%"
        'Tablo baþlýðý ve biçim atamasý
        Tablo.Bicim = Bicim
        Tablo.Baslik = "A N K E T L E R"
        'Kolonlar oluþturuluyor
        Kolon = New clsKolonum
        Kolon.Baslik = "No"
        Kolon.Genislik = "10%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Yýl"
        Kolon.Genislik = "25%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = "Dönem"
        Kolon.Genislik = "25%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        Kolon = New clsKolonum
        Kolon.Baslik = ""
        Kolon.Genislik = "40%"
        Kolon.icerikHizalama = "center"
        Tablo.KolonEkle(Kolon)
        'Satýrlar oluþturuluyor
        Dim SatirNo As Long = 0
        For Each drAnket In tblAnketler.Rows
            SatirNo += 1
            Satir = New clsSatirim
            Hucre = New clsHucrem
            Hucre.Metin = SatirNo.ToString
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = drAnket("Yil")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = drAnket("Donem")
            Satir.HucreEkle(Hucre)
            Hucre = New clsHucrem
            Hucre.Metin = "<a href='anket2.aspx?Sayfa=AnketGor&AnketNo=" & drAnket("AnketNo") & "'>Genel Anketi Gör</a>"
            Satir.HucreEkle(Hucre)
            Tablo.SatirEkle(Satir)
        Next
        Tablo.HtmlKoduUret()
        phOrta.Controls.Add(New LiteralControl(Tablo.HtmlKodu))
    End Sub
    Private Sub AnketGor(ByVal AnketNo As Long)
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        Dim YetkiDuzeyi As Byte
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Komut.Connection = Baglanti
        Dim Adaptor As New OdbcDataAdapter(Komut)
        If SayisalMi(Session("YetkiDuzeyi"), HYD_Yonetici, HYD_Yonetici) Then
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
        Dim Anket As New clsGenelAnket
        Dim Soru As clsAnketSorusu
        Dim Secenek As clsAnketSoruSecenekleri
        Anket.AnketNo = AnketNo
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
            Tablo.Baslik = "Anket Sonuçlarý"
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
            Sorgu = "SELECT CevapNo FROM AnketCevaplar WHERE AnketNo=" & AnketNo & " AND Tur=1 ORDER BY CevapNo"
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
    End Sub
    Private Sub AnketGor_TekTek(ByVal CevapNo As Long)
        Dim Yil As Short = 0
        Dim Donem As Byte = 0
        Dim YetkiDuzeyi As Byte
        Dim Sorgu As String
        Dim Komut As New OdbcCommand
        Komut.Connection = Baglanti
        Dim Adaptor As New OdbcDataAdapter(Komut)
        If SayisalMi(Session("YetkiDuzeyi"), HYD_Yonetici, HYD_Yonetici) Then
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
        'Þimdi anket bilgileri alýnýyor
        Dim Anket As New clsGenelTekAnket
        Dim Soru As clsAnketSorusu
        Dim Secenek As clsAnketSoruSecenekleri
        Anket.CevapNo = CevapNo
        If Anket.VeritabanindanAl Then
            'Þimdi Anket Gösteriliyor...
            Dim Kod, Kod2 As String
            Dim SatirNo As Byte = 0
            Kod = "<Table align='Center' width='910px' cellpadding='0' cellspacing='5' border='0'>"
            Kod += "<tr><td><h2>Genel Anket<h2></td></tr>"
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
    End Sub
    Private Sub ddlAnketCevaplari_SeciliOgeDegistiginde(ByVal Sender As Object, ByVal E As EventArgs)
        Dim ddlAnketCevaplari As DropDownList = Sender
        If ddlAnketCevaplari.SelectedIndex > 0 Then
            If SayisalMi(ddlAnketCevaplari.SelectedItem.Value, 1, 1000000000) Then
                Response.Redirect("anket2.aspx?Sayfa=AnketGor_TekTek&CevapNo=" & ddlAnketCevaplari.SelectedItem.Value)
            End If
        End If
    End Sub
    Private Sub Baglantiyi_Kapat()
        Try
            Baglanti.Close()
        Catch ex As Exception
            'boþ
        End Try
    End Sub
End Class