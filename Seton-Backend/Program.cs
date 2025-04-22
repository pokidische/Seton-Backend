using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Seton_Backend;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Seton_Backend.Services;


var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

// Konfiguracja SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=seton.db"));

builder.Services.AddScoped<IJwtService, JwtService>();

// Dodanie kontrolerów i Swaggera
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Seton API", Version = "v1" });

    // ?? Konfiguracja JWT w Swaggerze
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Wpisz token JWT w formacie: Bearer {twój_token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// W³¹czenie Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    var users = DataGenerator.GenerateUsers(10);
    var notes = DataGenerator.GenerateNotes(users, 5);
    var todoItems = DataGenerator.GenerateTodoItems(notes);
    var tags = DataGenerator.GenerateTags(10);
    var noteTags = DataGenerator.GenerateNoteTags(notes, tags);
    var noteSharings = DataGenerator.GenerateNoteSharings(notes, users);
    var folders = DataGenerator.GenerateFolders(users, 2);
    var folderNotes = DataGenerator.GenerateFolderNotes(folders, notes);
    var voiceNotes = DataGenerator.GenerateVoiceNotes(notes);
    var integrations = DataGenerator.GenerateIntegrations(users);
    var noteVersions = DataGenerator.GenerateNoteVersions(notes);

    dbContext.Users.AddRange(users);
    dbContext.Notes.AddRange(notes);
    dbContext.SaveChanges();
}
