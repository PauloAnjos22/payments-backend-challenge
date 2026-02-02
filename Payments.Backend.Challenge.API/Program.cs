using Microsoft.EntityFrameworkCore;
using Payments.Backend.Challenge.Application.Interfaces;
using Payments.Backend.Challenge.Application.UseCases;
using Payments.Backend.Challenge.Domain.Interfaces;
using Payments.Backend.Challenge.Domain.Services;
using Payments.Backend.Challenge.Infrastructure.Persistence;
using Payments.Backend.Challenge.Infrastructure.Repositories;
using Payments.Backend.Challenge.Infrastructure.Security;
using Payments.Backend.Challenge.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Db context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Security (password hasher)
builder.Services.AddScoped<IPasswordHasher, AspNetIdentityPasswordHasher>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();

// Domain Services
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<UserUniquesPolicy>();

// Use cases
builder.Services.AddScoped<IPaymentCoordinator, PaymentCoordinator>();
builder.Services.AddScoped<IRegisterUser, RegisterUser>();

// Unit of work
builder.Services.AddScoped<IEfUnitOfWork, EfUnitOfWork>();

// Mock services
builder.Services.AddHttpClient<IExternalAuthorizationMock, ExternalAuthorizationMock>(client =>
{
    client.BaseAddress = new Uri("https://util.devi.tools/api/v2/authorize");
});

builder.Services.AddHttpClient<IExternalNotificationMock, ExternalNotificationMock>(client =>
{
    client.BaseAddress = new Uri("https://util.devi.tools/api/v1/notify");
});

builder.Services.AddControllers();

var app = builder.Build();

// Auto run migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.MapControllers();

app.Run();
