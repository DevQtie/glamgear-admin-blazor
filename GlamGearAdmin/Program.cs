using GlamGearAdmin.Components;
using Microsoft.EntityFrameworkCore;
using GlamGearAdmin.Data.SQLite;
using GlamGearAdmin.Data.SQLServer;
using GlamGearAdmin.Components.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using GlamGearAdmin.Data.SQLiteAuth;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<BlazorSQLServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlazorSQLServerContext") ?? throw new InvalidOperationException("Connection string 'BlazorSQLServerContext' not found.")));
builder.Services.AddDbContextFactory<BlazorAuthContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlazorSqliteAuthContext") ?? throw new InvalidOperationException("Connection string 'BlazorSQLServerContext' not found.")));
builder.Services.AddDbContextFactory<BlazorWebAppAdminContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlazorWebAppAdminContext") ?? throw new InvalidOperationException("Connection string 'BlazorWebAppAdminContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IdentityUserAccessor>();

builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BlazorAuthContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<IdentityUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.MapAdditionalIdentityEndpoints(); ;

app.Run();