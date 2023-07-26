using QuizGame.Services;
using System.Reflection;

namespace RESTAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "allowAll";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IQuizServices, QuizServices>();
            builder.Services.AddControllers(options =>
            {
                //options.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setupAction =>
            {

                //setupAction.SwaggerDoc("RestAPISpecification", new()
                //{
                //    Title = "RESTAPI",
                //    Version = "v1"
                //});

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                                name: MyAllowSpecificOrigins,
                                builder =>
                                {
                                    builder.WithOrigins("http://127.0.0.1:5500")
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
                                   
                                });
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder => 
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Something went wrong.");
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}