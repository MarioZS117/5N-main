var builder = WebApplication.CreateBuilder(args);
//Obtener Cadena de Conexion
Configuraciones.CadenaConexion=builder.Configuration.GetConnectionString("ConexionBiblioteca");

//Inyeccion de Dependencia
builder.Services.AddTransient<Iseguridad,SeguridadService>();
builder.Services.AddTransient<IUsuario,UsuarioServices>();
builder.Services.AddTransient<IPrestamo,PrestamoService>();
builder.Services.AddTransient<ILibro,LibrosService>();
builder.Services.AddTransient<IAutor,AutorService>();
builder.Services.AddTransient<IClasificaciones,ClasificacionesService>();
builder.Services.AddTransient<IGenero,GeneroService>();
builder.Services.AddTransient<ICopias,CopiasService>();
builder.Services.AddTransient<IEditorial,EditorialService>();


// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://127.0.0.1:5500") 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
//Fin


//Agregar el Metodo que me permite trabajar controladores
builder.Services.AddControllers();


var app = builder.Build();

//cambio
app.UseCors("AllowLocalhost");

app.UseAuthorization();
app.Run();

//Fin