Public Class anasayfa
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents phOrta As System.Web.UI.WebControls.PlaceHolder

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
    const BirSayfadakiDuyuruSayisi as Byte=10
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        phOrta.Visible = True
        Dim kod As String = ""
        kod = "<Table align='center' width='100%' cellpadding='0' cellspacing='3' border='0'>"
        phOrta.Controls.Add(New LiteralControl(kod))
        DuyurulariYaz()
        kod = "</td></tr></table>"
        phOrta.Controls.Add(New LiteralControl(kod))
    End Sub
    Private Sub DuyurulariYaz()
        Dim kod As String = ""
        sorgu = "SELECT * FROM Duyurular ORDER BY Tarih DESC"
        komut.CommandText = sorgu
        Dim Duyurular As New DataSet
        Dim Duyuru As DataRow
        Try
            adaptor.Fill(Duyurular, 0, BirSayfadakiDuyuruSayisi, "Duyurular")
            For Each Duyuru In Duyurular.Tables(0).Rows
                kod = kod & "<tr><td width='100%'>"
                kod = kod & "<Table align='center' width='100%' cellpadding='1' cellspacing='1' bgcolor='#ebebff' border='0'>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%' bgcolor='#a1aCd6' valign='center'>"
                kod = kod & "<Table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td width='25px' align='center' valign='center'>"
                Select Case Duyuru("KategoriNo")
                    Case KN_Haber
                        kod = kod & "<img src='images/diger/haberlogo_kucuk.gif'> "
                    Case KN_Seminer
                        kod = kod & "<img src='images/diger/seminerlogo_kucuk.gif'> "
                    Case KN_Kurs
                        kod = kod & "<img src='images/diger/kurslogo_kucuk.gif'> "
                End Select
                kod = kod & "</td><td width='*' valign='center' align='left'>"
                kod = kod & "<p class='Sade' style='color:#FFFFFF'>"
                kod = kod & "<b>" & KarakterKodCoz(Duyuru("Baslik")) & "</b></p>"
                kod = kod & "</td></tr></table></td>"
                kod = kod & "</tr>"
                kod = kod & "<tr>"
                kod = kod & "<td class='Sade' align='left' width='100%'>"
                kod = kod & "<p class='Normal'>" & KarakterKodCoz(Duyuru("Metin")) & "</p>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "<tr>"
                kod = kod & "<td width='100%' bgcolor=''>"
                kod = kod & "<table width='100%' align='center' cellpadding='0' cellspacing='0' border='0'>"
                kod = kod & "<tr><td class='Sade' width='50%' align='left'>"
                kod = kod & "<p style='color:#666666'>" & MySqlUzunTarihAl(Duyuru("Tarih")).ToLongDateString & "</p>"
                kod = kod & "</td><td class='Sade' width='50%' align='right'>"
                Select Case Duyuru("KategoriNo")
                    Case KN_Haber
                        kod = kod & "<a href='haberler.aspx?Sayfa=Listele'>&gt&gt</a>"
                    Case KN_Seminer
                        kod = kod & "<a href='seminer.aspx?Sayfa=Listele'>&gt&gt</a>"
                    Case KN_Kurs
                        kod = kod & "<a href='kurslar.aspx?Sayfa=Listele'>&gt&gt</a>"
                End Select
                kod = kod & "</td></tr>"
                kod = kod & "</table>"
                kod = kod & "</td>"
                kod = kod & "</tr>"
                kod = kod & "</table>"
                kod = kod & "</td></tr>"
            Next
            phOrta.Controls.Add(New LiteralControl(kod))
        Catch ex As Exception
            phOrta.Controls.Add(New LiteralControl("<p class='Normal'>Veritabanýna baðlanýlamadý</p>" & ex.Message))
        End Try
    End Sub
End Class
