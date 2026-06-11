using ElectronicStore.Application.Commands.Brands;
using ElectronicStore.Application.Commands.Carts;
using ElectronicStore.Application.Commands.Products;
using ElectronicStore.Application.Commands.Reviews;
using ElectronicStore.Application.Commands.Users;
using ElectronicStore.Application.Queries.Brands;
using ElectronicStore.Application.Queries.Products;
using ElectronicStore.Application.Queries.Purchases;
using ElectronicStore.Application.Queries.Users;
using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Contracts.Repositories;
using ElectronocStore.Infrastructure.Db;
using ElectronocStore.Infrastructure.Db.Repositories;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendDevPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "http://localhost:5174")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Command handlers
builder.Services.AddScoped<IRequestHandler<CreateBrandCommand, Created>, CreateBrandCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateBrandCommand, ErrorOr<Updated>>, UpdateBrandCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteBrandCommand, ErrorOr<Deleted>>, DeleteBrandCommandHandler>();
builder.Services.AddScoped<IRequestHandler<AddCartItemCommand, ErrorOr<Updated>>, AddCartItemCommandHandler>();
builder.Services.AddScoped<IRequestHandler<BuyCommand, ErrorOr<Success>>, BuyCommandHandler>();
builder.Services.AddScoped<IRequestHandler<ClearCartCommand, ErrorOr<Updated>>, ClearCartCommandHandler>();
builder.Services.AddScoped<IRequestHandler<RemoveCartItemCommand, ErrorOr<Updated>>, RemoveCartItemCommandHandler>();
builder.Services.AddScoped<IRequestHandler<AddProductAmountCommand, ErrorOr<Updated>>, AddProductAmountCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateProductCommand, ErrorOr<Created>>, CreateProductCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateProductInfoCommand, ErrorOr<Updated>>, UpdateProductInfoCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteProductCommand, ErrorOr<Deleted>>, DeleteProductCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateReviewCommand, ErrorOr<Created>>, CreateReviewCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateUserCommand, ErrorOr<Created>>, CreateUserCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>, DeleteUserCommandHandler>();
builder.Services.AddScoped<IRequestHandler<TopUpUserBalanceCommand, ErrorOr<Updated>>, TopUpUserBalanceCommandHandler>();

// Queries handlers
builder.Services.AddScoped<IRequestHandler<GetBrandByIdQuery, ErrorOr<BrandResponse>>, GetBrandByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetBrandsQuery, IReadOnlyCollection<BrandResponse>>, GetBrandsQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>, GetProductByIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<SearchProductsQuery, IReadOnlyCollection<ProductResponse>>, SearchProductQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetPurchasesByUserIdQuery, IReadOnlyCollection<PurchaseResponse>>, GetPurchasesByUserIdQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetLatestPurchasesQuery, IReadOnlyCollection<PurchaseResponse>>, GetLatestPurchasesQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllUsersQuery, IReadOnlyCollection<UserResponse>>, GetAllUsersQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetUserByIdQuery, ErrorOr<UserResponse>>, GetUserByIdQueryHandler>();

// Ifrastructure services
builder.Services.AddDbContext<ElectronicStoreDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRespository>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ElectronicStoreDbContext>();

    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontendDevPolicy");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
