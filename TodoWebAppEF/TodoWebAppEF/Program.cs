using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoWebAppEF.Data;

var builder = WebApplication.CreateBuilder(args);

// MVC サービス追加
builder.Services.AddControllersWithViews();

// SQLite データベース
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite("Data Source=todos.db"));

// Identity ユーザー認証
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<TodoContext>();

var app = builder.Build();

// ミドルウェア設定
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// ルーティング
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");
app.MapRazorPages(); // Identity Razor Pages

app.Run();
