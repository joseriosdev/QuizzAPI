using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using QuizGame.Services;
using RESTAPI.CustomMiddlewares;
using Serilog;
using System.Reflection;
using System.Text;

namespace RESTAPI
{
    public class Startup
    {
        private static readonly string _devCORS = "allowAll";
        private static readonly string _prodCORS = "ReadOnlyProduction";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quiz.API", Version = "v1" });

                string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                c.AddSecurityDefinition("QuizzApiSecurity", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Input a valid token to access this API"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "QuizzApiSecurity" }
                            }, new List<string>() }
                    });
            });

            services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Authentication:Issuer"],
                        ValidAudience = Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration["Authentication:SecretForKey"]))
                    };
                }
                );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustHaveAuthorization", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("email", "admin@mail.com");
                });
            });

            services.AddHttpClient();

            services.AddCors(options =>
            {
                options.AddPolicy(
                                name: _devCORS,
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
                                });
                options.AddPolicy(
                                name: _prodCORS,
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                            .WithMethods("GET")
                                            .AllowAnyHeader();
                                });
            });

            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
                app.UseCors(_devCORS);
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
                app.UseCors(_prodCORS);
            }

            app.UseResponseCompression();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            
            
            app.UseMiddleware<LoggerMiddleware>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IQuizServices, QuizServices>();
        }

        private void RegisterRepositories(IServiceCollection services)
        {

        }
    }
}
