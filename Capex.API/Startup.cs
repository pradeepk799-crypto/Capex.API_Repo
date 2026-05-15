using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;

using System.Diagnostics;
using System.Net;
using System.Security.Principal;
using System.Windows.Input;

using Capex.Models.Common;
using Capex.API.Filters;
using SimpleInjector;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Capex.API.Repository;
using Capex.Models.ResponseModel;
using static Capex.Models.Common.APIResult;
using Serilog;
using Capex.Utilities;
using Capex.Business.Common.DBLogger;
using Microsoft.IdentityModel.Tokens;
using Capex.Business.Common.JWTToken;
using Capex.Models.RequestModel;

using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Capex.Utilities.Common;
using NuGet.Common;
using YamlDotNet.Serialization;
using Microsoft.AspNetCore.Identity;
using Capex.Infrastructure.Common;

namespace Capex.API
{
    public class Startup
    {
        /// <summary>
        /// The application settings.
        /// </summary>
        private AppSettings appSettings;
        /// <summary>
        /// The container.
        /// </summary>
        private readonly Container container = new Container();
        /// <summary>
        /// Get or set requestTime.
        /// </summary>
        private DateTime requestTime { get; set; }
        /// <summary>
        /// responseTime
        /// </summary>
        private DateTime responseTime { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add serices to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //IdentityModelEventSource.ShowPII = true;
            
            var appSettingsSection = this.Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            this.appSettings = appSettingsSection.Get<AppSettings>();
            services.AddSwaggerGen();
            //services.AddLogging(loggingBuilder => loggingBuilder.AddDbLogger(Configuration));
            var configurationBuilder = new ConfigurationBuilder();
            var _LdapUrl = Configuration.GetSection("LdapURL");
            services.Configure<LdapURL>(_LdapUrl);
        
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", builder =>
                {
                    builder.WithOrigins(Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>())
                           .WithMethods(Configuration.GetSection("CorsSettings:AllowedMethods").Get<string[]>())
                           .WithHeaders(Configuration.GetSection("CorsSettings:AllowedHeaders").Get<string[]>());
                });
            });

            // Other configurations...
        


        // services.AddSession();



        services.AddControllers(config =>
            {
                //config.Filters.Add(new LogRequestResponseAttribute(this.appSettings, this.container));
                //config.Filters.Add(new SessionIDActionFilter());
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })

            .AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddMemoryCache();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHttpContextAccessor();

            // DI Config
            DIConfig.ConfigureServicesConfig(services, this.container);


            //configure jwt authentication

           var key = Encoding.ASCII.GetBytes(this.appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.Events = new JwtBearerEvents()
                {
                    
                    OnAuthenticationFailed = c =>
                    {
                        try
                        {
                            Log.Debug("OnAuthenticationFailed : " + c.Exception.Message);
                            string token = c.Request.Headers["Authorization"].ToString();
                            var bodyStr = "";
                            var req = c.Request;



                            // Arguments: Stream, Encoding, detect encoding, buffer size 
                            // AND, the most important: keep stream opened
                            using (StreamReader reader
                                      = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                            {
                                bodyStr = reader.ReadToEnd();
                            }

                            // Parse the JSON string into a JsonDocument object
                            JsonDocument jsonDoc = JsonDocument.Parse(bodyStr);

                            // Get the root element of the JSON document
                            JsonElement root = jsonDoc.RootElement;

                            var tokenHandler = new JwtSecurityTokenHandler();
                            var jwtToken = tokenHandler.ReadJwtToken(token.Replace("Bearer", "").Trim());

                            var payload = jwtToken.Payload;


                            DateTime nbfDate = payload.Nbf.HasValue ? DateTimeOffset.FromUnixTimeSeconds(payload.Nbf.Value).DateTime : DateTime.MinValue;
                            DateTime expDate = payload.Exp.HasValue ? DateTimeOffset.FromUnixTimeSeconds(payload.Exp.Value).DateTime : DateTime.MinValue;
                            DateTime iatDate = payload.Iat.HasValue ? DateTimeOffset.FromUnixTimeSeconds(payload.Iat.Value).DateTime : DateTime.MinValue;

                            string userID = payload["unique_name"].ToString();

                            var Obj = JsonConvert.SerializeObject(new
                            {
                                type = "OnAuthenticationFailed",
                                userId = userID,
                                nbfDate = nbfDate,
                                expDate = expDate,
                                iatDate = iatDate,
                                token = token,
                                exception = c.Exception.Message
                            });




                            // Rewind, so the core is not lost when it looks at the body for the request                        
                            return Task.CompletedTask;
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.StackTrace);
                            return Task.CompletedTask;
                        }

                    },
                    OnTokenValidated = async ctx =>
                    {
                        
                        Debug.WriteLine("token: " + ctx.SecurityToken.ToString());
                        //return Task.CompletedTask;
                    }

                };
                x.TokenValidationParameters = TokenAuth.GetTokenValidationParameters();
            });

            services.AddAuthorization(options => options.AddPolicy("Bearer", policy => policy.AddRequirements(new BearerRequirement())));

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = Configuration["Jwt:Audience"],
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //    };
            //});
            // Swagger Config
            //SwaggerConfig.ConfigureServicesConfig(services);
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                    new BadRequestObjectResult(actionContext.ModelState);
            });
            services.AddScoped<JwtTokenGenerationHelper, JwtTokenGenerationHelper>();
            services.AddScoped<JwtTokenValidiateHelper, JwtTokenValidiateHelper>();          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                string userID = "", officeId = "", roleId;
                //Log.Logger = LogHelper.GetInstance();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                else
                {
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.Use(async (context, next) =>
                {
                    var sw = new Stopwatch();
                    this.requestTime = DateTime.Now;
                    var RouteData = context.Request.Path.Value.Split("/");
                    var controllerName = RouteData[1];
                    var ActionName = RouteData[2];
                    string token = context.Request.Headers["Authorization"].ToString();
                    if (token != "")
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var jwtToken = tokenHandler.ReadJwtToken(token.Replace("Bearer", "").Trim());

                        var payload = jwtToken.Payload;
                        userID = payload["UserId"].ToString();
                        officeId = payload["UserOfficeId"].ToString();
                        roleId = payload["UserRoleId"].ToString();
                        using (StreamReader stream = new StreamReader(context.Request.Body))
                        {
                            string originalContent = "";
                            originalContent = await stream.ReadToEndAsync();
                            if (originalContent != "" && !originalContent.Contains("otherAppData") && !originalContent.Contains("Filepath") && CommonUtility.IsJsonValid(originalContent))
                            {
                                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(originalContent);
                                jsonObj["UserId"] = userID;
                                if (officeId != "")
                                {
                                    jsonObj["UserOfficeId"] = officeId;

                                }

                                if (roleId != null)
                                {
                                    jsonObj["UserRoleId"] = roleId;

                                }
                                else
                                {
                                    jsonObj["UserRoleId"] = null;
                                }
                                string json = JsonConvert.SerializeObject(jsonObj);
                                var requestData = Encoding.UTF8.GetBytes(json);
                                context.Request.Body = new MemoryStream(requestData);
                                context.Request.ContentLength = context.Request.Body.Length;

                            }
                            else
                            {
                                var requestData = Encoding.UTF8.GetBytes(originalContent);
                                context.Request.Body = new MemoryStream(requestData);
                                context.Request.ContentLength = context.Request.Body.Length;
                            }

                        }
                    }
                    sw.Start();
                    await next.Invoke(context);
                    sw.Stop();
                    this.responseTime = DateTime.Now;

                    if (!string.IsNullOrWhiteSpace(this.appSettings.APITookTime) && Convert.ToInt64(this.appSettings.APITookTime) < sw.ElapsedMilliseconds)
                    {
                        Log.Debug(string.Format(context.Request.Path + " APITookTIME | {0}", sw.ElapsedMilliseconds));
                    }
                    

                });
                app.UseCors("AllowAngularApp");
                app.UseHttpsRedirection();
                app.UseCookiePolicy();


                // Swagger Configuration Begin
                app.UseStaticFiles();
                //Enable sessions
               // app.UseSession();

               
                // DI Configuration
                DIConfig.ConfigureConfig(app, container);

                // Swagger Configuration

                // Always verify the container
                this.container.Verify();
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
                

                app.UseAuthentication();

               

                // Added to handle Exceptions globally.
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;
                    string message = string.Empty;
                    var result = JsonConvert.SerializeObject(new ApiResult<ResponseModelBase> { Status = false, Message = exception.Message, ResponseData = null });
                    Log.Error("Error  Message:" + exception.Message + "Stack Trace : " + exception.StackTrace);
                    context.Response.ContentType = "application/json";
                    if (exception.Message == "401")
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await context.Response.WriteAsync(string.Empty);
                    }
                    else if (exception.Message == "403.1")
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        result = JsonConvert.SerializeObject(new ApiResult<ResponseModelBase> { Status = false, Message = "Forbidden: Execute access is denied", ErrorCode = "ERR401.3", ResponseData = null });
                        await context.Response.WriteAsync(result);
                    }

                    else
                    {
                        if (exception.Message.Contains("Invalid Value of"))
                        {

                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            result = JsonConvert.SerializeObject(new ApiResult<ResponseModelBase> { Status = false, Message = exception.Message, ErrorCode = "Err100043", ResponseData = null });
                            await context.Response.WriteAsync(result);
                        }
                        else
                            await context.Response.WriteAsync(result);
                    }
                }));
                app.UseSerilogRequestLogging(options =>
                {
                    options.EnrichDiagnosticContext = PushSeriLogProperties;
                });
                app.UseRouting();
                app.UseAuthorization();
                
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
            catch (Exception ex)
            {
                Log.Error("Error Message", ex);
                throw;
            }
        }
        public void PushSeriLogProperties(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            diagnosticContext.Set("UserId", "");
        }
    }
}
