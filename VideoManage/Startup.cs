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
            //    //ȫ��ע����Ȩ������
            //    config.Filters.Add(typeof(MyAuthorizeFilter));
            //});
            //������ʣ���ʱ��
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetPreflightMaxAge(TimeSpan.FromDays(10))));
            services.AddScoped<DbContext, VideoContext>();
            //services.AddScoped<VideoService>();
            services.AddScoped<UploadService>();
            //services.AddScoped<UserService>();
            //services.AddScoped<HouseService>();
            services.AddDbContext<VideoContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")));

            //ע��AutoMapper����
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(c=>c.AddProfile(new AutoMapperProfile()));

            //���÷���ע��
            services.AddScopeServices(typeof(BaseService).Assembly);

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "��Ƶ����ϵͳ", Version = "v1" });
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), xmlFile);//AppContext.BaseDirectory
                    // ����xmlע��. �÷����ڶ����������ÿ�������ע�ͣ�Ĭ��Ϊfalse
                    c.IncludeXmlComments(xmlPath);
                    #region Swagger��չ-��������Token����
                    //��ʽ1
                    c.OperationFilter<AddResponseHeadersFilter>();
                    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                    //��Token��������ͷ�д��ݵ���̨
                    c.OperationFilter<SecurityRequirementsOperationFilter>();
                    //ָ�����Ʊ���ΪOAuth2����ΪSecurityRequirementsOperationFilterĬ��securitySchemaNameָ��Ϊoauth2
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Jwt��֤��Ȩ���������������'Bearer Token'(Bearer��Token֮����һ���ո�)",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                    #endregion

                }
            );


            #region ����jwt
            //��������Ϣ��ȡ������������Էŵ������ļ��У�ͬһ��ȡ������ֱ���ڳ�����д����
            //��Կ����������token��Ҫ��Կ�����������ἰ��ǩ�����ǿ���Կ,����Ҫ���ڵ���16λ
            string secret = AppSettings.JWT.secret;// "baichaqinghuanwubieshi";
            //ǩ���ţ�����˭�䷢��
            string issuer = AppSettings.JWT.issuer;// "issuer";
            //�����ߣ��Ǹ�˭�õ�
            string audience = AppSettings.JWT.audience;// "videos";
            //ע�������ʾָ��Ϊbearer
            services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                //����jwt��Ϣ
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //�Ƿ���֤��Կ
                    ValidateIssuerSigningKey = true,
                    //ָ����Կ
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    //�Ƿ���֤�䷢��
                    ValidateIssuer = true,
                    //ָ���䷢��
                    ValidIssuer = issuer,
                    //�Ƿ���֤������
                    ValidateAudience = true,
                    //ָ��������
                    ValidAudience = audience,
                    //���ñ���Ҫ�еĳ�ʱʱ��
                    RequireExpirationTime = true,
                    //���ñ�����֤��ʱ
                    ValidateLifetime = true,
                    //��������ʱ�䣬��������Ϊ0ʱ����������Чʱ�䵽�ڣ�������ʱЧ
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    //�˴�ΪȨ����֤ʧ�ܺ󴥷����¼�
                    OnChallenge = context =>
                    {
                        //�˴�����Ϊ��ֹ.net coreĬ�ϵķ������ͺ����ݽ��������Ҫ������
                        context.HandleResponse();
                        //�Զ����Լ���Ҫ���صĽ��
                        var payload = JsonConvert.SerializeObject(new Result { code = "401", msg = "�û���֤ʧ�ܣ������µ�¼" });
                        //�Զ��巵�ص���������
                        context.Response.ContentType = "application/json";
                        //�Զ��巵��״̬��
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        //���json���
                        context.Response.WriteAsync(payload);
                        return Task.FromResult(0);
                    }
                };
            });
            #endregion

            #region session
            //ʹ��session
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //�����ڴ滺��(�ò�������AddSession()����ǰʹ��)
            services.AddDistributedMemoryCache();//����session֮ǰ����������ڴ�
            services.AddSession(options =>
            {
                options.Cookie.Name = AppSettings.Session.Name;
                options.IdleTimeout = System.TimeSpan.FromMinutes(Convert.ToDouble(AppSettings.Session.TimeOut));//����session����ʱ��
                options.Cookie.HttpOnly = true;//���������������ͨ��js��ø�cookie��ֵ
            });
            #endregion


            services.AddAuthorization();
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            //     {
            //         //��¼·�������ǵ��û���ͼ������Դ��δ���������֤ʱ�����򽫻Ὣ�����ض���������·��
            //         //o.LoginPath = new PathString("/Account/Login");
            //         //��ֹ����·�������û���ͼ������Դʱ����δͨ������Դ���κ���Ȩ���ԣ����󽫱��ض���������·����
            //         //o.AccessDeniedPath = new PathString("/Account/Login");
            //         //o.SlidingExpiration = true;
            //     });
        }

        //Autofac�Զ�ע��
        public void ConfigureContainer(ContainerBuilder builder) 
        {
            //ҵ���߼������ڳ��������ռ�
            Assembly service = Assembly.Load("VideoManage.Service");//ע��webapiҪ���ýӿں��࣬��Ȼ��߶�����
            //�Զ�ע��
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

            //�������
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
