using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        public static string MaintenanceTime = "Bakımdayız.";
        public static string ProductsListed = "Ürünler listelendi.";
        public static string ProductCountOfCategoryError = "Kategoride en fazla 10 ürün olabilir.";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var.";
        public static string CategoryLimitExceded = "15'ten fazla kategori çeşidi olamaz.";
        public static string AuthorizationDenied = "Yetkinizi bulunmamaktadır.";
        public  static string UserRegistered = " Kayıt başarılı.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Parola hatası.";
        public static string SuccessfulLogin = "Başarılı giriş.";
        public static string AccessTokenCreated = "Jeton başarılı şekilde oluşturuldu.";
        public static string UserAlreadyExists = "Kullanıcı mevcut.";
    }
}
