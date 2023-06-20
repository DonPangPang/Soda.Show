using Soda.AutoMapper;
using Soda.Show.WebApi;
using Soda.Show.WebApi.Base;
using Soda.Show.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(opts =>
{
    opts.RecognizePrefixes("V");
});


builder.Services.AddDb();
builder.Services.AddServices();

// builder.Services.AddScoped(typeof(ISodaService<,>), typeof(SodaService<,>));


var app = builder.Build();

app.InitSodaMapper();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
