using System;

namespace iboCAD
{
	// Yapýlan bir deðiþikliði saklamak, geri ve ileri almak amacýyla oluþturulan
	// 'deðiþiklik' sýnýflarýnýn asli iþlemlerini gerçekleþtirmekte kullanýlan
	// ortak (ayný isimli) fonksyonlara sahip olmasý ve bu þekilde 'çok biçimli'
	// (polymorphic) olmasý için geliþtirilen ARAYÜZ
	public interface IDegisiklik
	{
		// bir deðiþikliði uygulamak için
		void Uygula(frmCizim cizimFormu);
		// yapýlmýþ bir iþlemi/deðiþikliði geri almak için
		void IptalEt(frmCizim cizimFormu);
	}
}
