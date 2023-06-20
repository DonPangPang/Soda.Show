using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Soda.AutoMapper;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi;
using Soda.Show.WebApi.Data;
using Soda.Show.WebApi.Domain;
using Soda.Show.WebApi.Extensions;
using Soda.Show.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(setup =>
    {
        setup.ReturnHttpNotAcceptable = true;
        // 添加XML setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
        // 将默认格式改为XML setup.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter());
    }).AddNewtonsoftJson(setup =>
        {
            setup.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver();
        })
        /*添加XML*/.AddXmlDataContractSerializerFormatters()
        .ConfigureApiBehaviorOptions(setup =>
        {
            setup.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Type = "http://www.baidu.com",
                    Title = "有错误",
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Detail = "请看详细信息",
                    Instance = context.HttpContext.Request.Path
                };

                problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                return new UnprocessableEntityObjectResult(problemDetails)
                {
                    ContentTypes = { "application/problem+json" }
                };
            };
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(opts =>
{
    opts.CreateMap<Account, VAccount>().ReverseMap();
    opts.CreateMap<Blog, VBlog>().ReverseMap();
    opts.CreateMap<FileResource, VFileResource>().ReverseMap();
    opts.CreateMap<User, VUser>().ReverseMap();
    opts.CreateMap<VersionRecord, VVersionRecord>().ReverseMap();
    opts.CreateMap<Tag, VTag>().ReverseMap();
    opts.CreateMap<Group, VGroup>().ReverseMap();
}, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDb();
builder.Services.AddServices();

builder.Services.AddScoped(typeof(ISodaService<,>), typeof(SodaService<,>));

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