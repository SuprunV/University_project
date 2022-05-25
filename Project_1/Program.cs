using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_1.Data;
using Project_1.Domain;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Infra;
using Project_1.Infra.Connection;
using Project_1.Infra.Initializers;
using Project_1.Infra.Party;
using Project_1.Infra.Party.Courses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(connectionString));
builder.Services.AddDbContext<UniversityDb>(o =>
    o.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IStudyProgramsRepo, StudyProgramsRepo>();
builder.Services.AddTransient<IStudentsRepo, StudentsRepo>();
builder.Services.AddTransient<ILecturersRepo, LecturersRepo>(); 
builder.Services.AddTransient<ICoursesRepo, CoursesRepo>(); 
builder.Services.AddTransient<IMyCoursesRepo, MyCoursesRepo>();
builder.Services.AddTransient<IJoinedCoursesRepo, JoinedCoursesRepo>();
builder.Services.AddTransient<ISemestersRepo, SemestersRepo>();
builder.Services.AddTransient<ICountriesRepo, CountriesRepo>();
builder.Services.AddTransient<ICourseLecturerRepo, CourseLecturersRepo>();
builder.Services.AddTransient<ISemesterCourseRepo, SemestersCoursesRepo>();
builder.Services.AddTransient<IStudyProgramsCoursesRepo, StudyProgramsCoursesRepo>();
builder.Services.AddTransient<IEnrollmentsRepo, EnrollmentsRepo>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromSeconds(6000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    GetRepo.SetService(app.Services);
    var db = scope.ServiceProvider.GetService<UniversityDb>();
    _ = (db?.Database?.EnsureCreated());
    UniversityDbInitializer.Init(db);
}
//app.UseRequestLocalization("de-DE");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
