
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebProgramlama1.Data;
using WebProgramlama1.Models;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// 1. Veritabanı Bağlantısı (Aynen kalıyor)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. IDENTITY Servislerinin Eklenmesi (BU KISIM EKLENDİ)
// ApplicationUser ve IdentityRole sınıflarını sisteme tanıtıyoruz.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Geliştirme aşamasında test kolaylığı için şifre kurallarını basitleştirelim.
    // Canlıya alırken bunları true yaparsın.
    options.Password.RequireDigit = false;           // Sayı zorunluluğu yok
    options.Password.RequireLowercase = false;       // Küçük harf zorunluluğu yok
    options.Password.RequireUppercase = false;       // Büyük harf zorunluluğu yok
    options.Password.RequireNonAlphanumeric = false; // Özel karakter (@,!,?) zorunluluğu yok
    options.Password.RequiredLength = 3;             // En az 3 karakter olsun yeter
    
    options.User.RequireUniqueEmail = true;          // Aynı email ile tekrar kayıt olunamasın
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 3. Cookie (Çerez) Ayarları (Giriş yapmamış kullanıcı nereye gitsin?)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";        // Giriş yapmamışsa buraya yönlendir
    options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisi yoksa buraya yönlendir
    options.SlidingExpiration = true;            // Kullanıcı sitede gezdikçe oturum süresi uzasın
    options.ExpireTimeSpan = TimeSpan.FromDays(7); // Oturum 7 gün açık kalsın
});

// MVC Servisleri
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// 4. Statik Dosyalar (CSS, JS ve Yüklenen Resimler için)
// MapStaticAssets .NET 9 ile geldi ama resim yükleme işlemleri için
// UseStaticFiles kullanımı hala en garanti yöntemdir.
app.UseStaticFiles(); 

app.UseRouting();

// 5. KİMLİK DOĞRULAMA SIRALAMASI (Çok Kritik!)
app.UseAuthentication(); // ÖNCE: Kimsin? (Giriş yapmış mısın?) - EKLENDİ
app.UseAuthorization();  // SONRA: Yetkin var mı? (Admin misin?)

app.MapStaticAssets(); // .NET 9 özelliği, kalabilir.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();