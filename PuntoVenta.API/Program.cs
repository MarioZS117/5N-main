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


// Se Agrega el Servicio de Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregar el Metodo que me permite trabajar controladores
builder.Services.AddControllers();

var app = builder.Build();

//Mapear los controladores
app.MapControllers();

// Valida si el codigo esta en modo de pruebas o produccion
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Corre la aplicacion
app.Run();