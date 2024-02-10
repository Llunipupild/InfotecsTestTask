using Microsoft.EntityFrameworkCore;
using TestTask.Database.Context;
using TestTask.Repository.Files;
using TestTask.Repository.Results;
using TestTask.Repository.Values;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ScienceExpirementApi.xml"));
});
builder.Services.AddScoped<IFilesRepository, FilesRepository>();
builder.Services.AddScoped<IValuesRepository, ValuesRepository>();
builder.Services.AddScoped<IResultsRepository, ResultsRepository>();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("InfotecsTestTaskDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
