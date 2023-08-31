using Login_Registro_de_Usuarios.Datas;
using Login_Registro_de_Usuarios.Negocio;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddSingleton(new Context(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.AddScoped<ValidacionInicio>();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}"
        );
});

app.Run();
