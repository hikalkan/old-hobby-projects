Module mdlSabitler

    'Veritabanýna baðlantý cümlesi
    Public Const BaglantiCumlesi As String = "Driver={MySQL ODBC 3.51 Driver};" & _
    "Server=localhost;Database=anket;uid=root;pwd=himysql;option=3"

    'HOCALARIN YETKÝ DÜZEYLERÝ
    'Sadece kendi dersi için yapýlan anketleri görebilir
    Public Const HYD_KendiDersi As Byte = 1
    'Tüm Dersler içim tüm anketleri görebilir
    Public Const HYD_TumDersler As Byte = 2
    'Öðrenci ekleme/silme.. de yapabilir
    Public Const HYD_Editor As Byte = 3
    'Tüm yetkilere sahip
    Public Const HYD_Yonetici As Byte = 4

    'ANKET TÜRLERÝ
    'Genel sorulardan oluþan anketler
    Public Const AT_Genel As Byte = 1
    'Derse özel sorularda oluþan anketler
    Public Const AT_Ders As Byte = 2

End Module
