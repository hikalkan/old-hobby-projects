Module mdlSabitler

    'Veritaban�na ba�lant� c�mlesi
    Public Const BaglantiCumlesi As String = "Driver={MySQL ODBC 3.51 Driver};" & _
    "Server=localhost;Database=anket;uid=root;pwd=himysql;option=3"

    'HOCALARIN YETK� D�ZEYLER�
    'Sadece kendi dersi i�in yap�lan anketleri g�rebilir
    Public Const HYD_KendiDersi As Byte = 1
    'T�m Dersler i�im t�m anketleri g�rebilir
    Public Const HYD_TumDersler As Byte = 2
    '��renci ekleme/silme.. de yapabilir
    Public Const HYD_Editor As Byte = 3
    'T�m yetkilere sahip
    Public Const HYD_Yonetici As Byte = 4

    'ANKET T�RLER�
    'Genel sorulardan olu�an anketler
    Public Const AT_Genel As Byte = 1
    'Derse �zel sorularda olu�an anketler
    Public Const AT_Ders As Byte = 2

End Module
