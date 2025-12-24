using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Serialization;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Assignment;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Content;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// Make appsettings.json available 
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("YPLMSCors", policy =>
//    {
//        policy.WithOrigins(allowedOrigins)
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials();
//    });
//});

builder.Services.AddCors(options =>
{


    // Optionally you can create a more permissive policy for development
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()   // Allow any origin
                   .AllowAnyHeader()   // Allow any header
                   .AllowAnyMethod();  // Allow any method
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication("Bearer")
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])
            )
        };
    });

builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ApplicationMapper));

builder.Services.AddSingleton<IActivityAssignmentAdaptor<ActivityAssignment>, ActivityAssignmentAdaptor>();
builder.Services.AddSingleton<IClientDAM<Client>, ClientDAM>();
builder.Services.AddSingleton<IContentModuleAdaptor<ContentModule>, ContentModuleAdaptor>();
builder.Services.AddSingleton<IClientFeatureDAM<ClientFeature>, ClientFeatureDAM>();
builder.Services.AddSingleton<IContentModuleSessionDAM<ContentModuleSession>, ContentModuleSessionDAM>();
builder.Services.AddSingleton<IContentModuleTrackingAdaptor<ContentModuleTracking>, ContentModuleTrackingAdaptor>();
builder.Services.AddSingleton<ILearnerDAM<Learner>, LearnerDAM>();
builder.Services.AddSingleton<ILessonTrackingSerializer, ScoTrackingSerializer>();
builder.Services.AddSingleton<IAdminRoleAdaptor<AdminRole>, AdminRoleAdaptor>();
builder.Services.AddSingleton<IBulkImportAdaptor<BulkImport>, BulkImportAdaptor>();
builder.Services.AddSingleton<IContentModuleTrackingManager<ContentModuleSession>, YPLMS2._0.API.DataAccessManager.ContentModuleTrackingManager>();
builder.Services.AddSingleton<IImportDefinitionAdaptor<ImportDefination>, ImportDefinitionAdaptor>();
builder.Services.AddSingleton<IImportHistoryAdaptor<ImportHistory>, ImportHistoryAdaptor>();
builder.Services.AddSingleton<IPasswordPolicyAdaptor<PasswordPolicyConfiguration>, PasswordPolicyAdaptor>();
builder.Services.AddSingleton<IOTPAdaptor<OTP>, OTPAdaptor>();
builder.Services.AddSingleton<IRuleRoleScopeAdaptor<RuleRoleScope>, RuleRoleScopeAdaptor>();
builder.Services.AddSingleton<IStudentListAdaptor<StudentList>, StudentListAdaptor>();
builder.Services.AddSingleton<IGroupRuleAdaptor<GroupRule>, GroupRuleAdaptor>();
builder.Services.AddSingleton<IContentModuleMappingAdaptor<ContentModuleMapping>, ContentModuleMappingAdaptor>();
builder.Services.AddSingleton<ICourseConfigurationAdaptor<CourseConfiguration>, CourseConfigurationAdaptor>();
builder.Services.AddSingleton<IAutoEmailTemplateSettingAdaptor<AutoEmailTemplateSetting>, AutoEmailTemplateSettingAdaptor>();
builder.Services.AddSingleton<IBusinessRuleUsersAdaptor<BusinessRuleUsers>, BusinessRuleUsersAdaptor>();
builder.Services.AddSingleton<ILanguageDAM<Language>, LanguageDAM>();
builder.Services.AddSingleton<IAssetLibraryAdaptor<AssetLibrary>, AssetLibraryAdaptor>();
builder.Services.AddSingleton<IOrganizationLevelUnitDAM<OrganizationLevelUnit>, OrganizationLevelUnitDAM>();
builder.Services.AddSingleton<IOrganizationLevelDAM<OrganizationLevel>, OrganizationLevelDAM>();
builder.Services.AddSingleton<IAssetAdaptor<Asset>, AssetAdaptor>();
builder.Services.AddSingleton<IContentModuleTrackingRepository, YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking.ContentModuleTrackingManager>();
builder.Services.AddSingleton<ICoursePlayerAssignmentManager, CoursePlayerAssignmentManager>();
builder.Services.AddSingleton<IAdminFeaturesAdaptor<AdminFeatures>, AdminFeaturesAdaptor>();
//builder.Services.AddSingleton<ICourseAssignmentRepository<ActivityAssignment>, ActivityAssignmentAdaptor>();
//builder.Services.AddSingleton<IContentModuleRetriever, FilesystemContentModuleRetriever>();
//builder.Services.AddSingleton<IContentModuleRepository, ContentModuleAdaptor>();
//builder.Services.AddSingleton<ICacheProvider, HttpContextCacheProvider>();
//builder.Services.AddSingleton<ICourseConfigurationRepository, CourseConfigurationAdaptor>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<SQLObject>();
builder.Services.AddTransient<SQLHelper>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Common>();
builder.Services.AddScoped<TokenValidationMiddleware>();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
//app.UseCors("YPLMSCors");
app.UseCors("AllowAllOrigins");

app.UseRouting();                
app.UseAuthentication();
//app.UseMiddleware<YPLMS2._0.API.DataAccessManager.TokenValidationMiddleware>();
app.UseAuthorization();          

app.MapControllers();            
app.Run();
//app.UseDeveloperExceptionPage();



