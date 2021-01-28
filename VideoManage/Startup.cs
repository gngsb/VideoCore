using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoManage.EFCore;
using Microsoft.OpenApi.Models;
using VideoManage.Service.Video;
using System.Reflection;
using System.IO;
using log4net.Repository;
using log4net;
using log4net.Config;
using VideoManage.Service.Uploads;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using VideoManage.Hosting.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using VideoManage.Constants;
using VideoManage.Constants.Configurations;
using VideoManage.Service.Property;
using AutoMapper;
using VideoManage.Service;
using Autofac;
using VideoManage.Service.Extends;

namespace VideoManage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddControllers(config=> 
            //{
            //    //全局注册授权过滤器
            //    config.Filters.Add(typeof(MyAuthorizeFilter));
            //});
            //跨域访问，临时的
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetPreflightMaxAge(TimeSpan.FromDays(10))));
            services.AddScoped<DbContext, VideoContext>();
            //services.AddScoped<VideoService>();
            services.AddScoped<UploadService>();
            //services.AddScoped<UserService>();
            //services.AddScoped<HouseService>();
            services.AddDbContext<VideoContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")));

            //注入AutoMapper服务
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(c=>c.AddProfile(new AutoMapperProfile()));

            //配置服务注入
            services.AddScopeServices(typeof(BaseService).Assembly);

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "视频管理系统", Version = "v1" });
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), xmlFile);//AppContext.BaseDirectory
                    // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false
                    c.IncludeXmlComments(xmlPath);
                    #region Swagger扩展-增加输入Token功能
                    //方式1
                    c.OperationFilter<AddResponseHeadersFilter>();
                    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                    //将Token放在请求头中传递到后台
                    c.OperationFilter<SecurityRequirementsOperationFilter>();
                    //指定名称必须为OAuth2，因为SecurityRequirementsOperationFilter默认securitySchemaName指定为oauth2
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Jwt认证授权，在输入框中输入'Bearer Token'(Bearer和Token之间有一个空格)",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                    #endregion

                }
            );


            #region 集成jwt
            //将公共信息提取出来，这里可以放到配置文件中，同一读取，以下直接在程序中写死了
            //秘钥，这是生成token需要秘钥，就是理论提及到签名的那块秘钥,必须要大于等于16位
            string secret = AppSettings.JWT.secret;// "baichaqinghuanwubieshi";
            //签发着，是由谁颁发的
            string issuer = AppSettings.JWT.issuer;// "issuer";
            //接受者，是给谁用的
            string audience = AppSettings.JWT.audience;// "videos";
            //注册服务，显示指定为bearer
            services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                //配置jwt信息
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //是否验证秘钥
                    ValidateIssuerSigningKey = true,
                    //指定秘钥
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    //是否验证颁发者
                    ValidateIssuer = true,
                    //指定颁发者
                    ValidIssuer = issuer,
                    //是否验证接受者
                    ValidateAudience = true,
                    //指定接受者
                    ValidAudience = audience,
                    //设置必须要有的超时时间
                    RequireExpirationTime = true,
                    //设置必须验证超时
                    ValidateLifetime = true,
                    //滑动过期时间，将其设置为0时，即设置有效时间到期，就马上时效
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    //此处为权限验证失败后触发的事件
                    OnChallenge = context =>
                    {
                        //此处代码为终止.net core默认的返回类型和数据结果，很重要，必须
                        context.HandleResponse();
                        //自定义自己想要返回的结果
                        var payload = JsonConvert.SerializeObject(new Result { code = "401", msg = "用户认证失败，请重新登录" });
                        //自定义返回的数据类型
                        context.Response.ContentType = "application/json";
                        //自定义返回状态码
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        //输出json结果
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    }
                };
            });
            #endregion

            #region session
            //使用session
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //启用内存缓存(该步骤需在AddSession()调用前使用)
            services.AddDistributedMemoryCache();//启用session之前必须先添加内存
            services.AddSession(options =>
            {
                options.Cookie.Name = AppSettings.Session.Name;
                options.IdleTimeout = System.TimeSpan.FromMinutes(Convert.ToDouble(AppSettings.Session.TimeOut));//设置session过期时间
                options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            });
            #endregion


            services.AddAuthorization();
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            //     {
            //         //登录路径：这是当用户试图访问资源但未经过身份验证时，程序将会将请求重定向到这个相对路径
            //         //o.LoginPath = new PathString("/Account/Login");
            //         //禁止访问路径：当用户试图访问资源时，但未通过该资源的任何授权策略，请求将被重定向到这个相对路径。
            //         //o.AccessDeniedPath = new PathString("/Account/Login");
            //         //o.SlidingExpiration = true;
            //     });
        }

        //Autofac自动注入
        public void ConfigureContainer(ContainerBuilder builder) 
        {
            //业务逻辑层所在程序集命名空间
            Assembly service = Assembly.Load("VideoManage.Service");//注：webapi要引用接口和类，不然这边读不到
            //自动注入
            builder.RegisterAssemblyTypes(service)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //跨域访问
            app.UseCors();

            app.UseSwagger();

            app.UseSession();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
