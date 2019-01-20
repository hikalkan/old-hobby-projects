using System;

namespace iboCAD
{
	// Yap�lan bir de�i�ikli�i saklamak, geri ve ileri almak amac�yla olu�turulan
	// 'de�i�iklik' s�n�flar�n�n asli i�lemlerini ger�ekle�tirmekte kullan�lan
	// ortak (ayn� isimli) fonksyonlara sahip olmas� ve bu �ekilde '�ok bi�imli'
	// (polymorphic) olmas� i�in geli�tirilen ARAY�Z
	public interface IDegisiklik
	{
		// bir de�i�ikli�i uygulamak i�in
		void Uygula(frmCizim cizimFormu);
		// yap�lm�� bir i�lemi/de�i�ikli�i geri almak i�in
		void IptalEt(frmCizim cizimFormu);
	}
}
