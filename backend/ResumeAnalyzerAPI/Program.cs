using Microsoft.EntityFrameworkCore;
using ResumeAnalyzerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ResumeAnalyzerAPI.Services.IAnalysisService, ResumeAnalyzerAPI.Services.AnalysisService>();
builder.Services.AddDbContext<ResumeAnalyzerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

const string AllowedOrigins = "_ReactApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowedOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Vite default port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(AllowedOrigins);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
