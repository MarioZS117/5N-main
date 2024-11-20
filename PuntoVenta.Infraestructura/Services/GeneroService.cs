using PuntoVenta.Models.Response;

public class GeneroService : IGenero
{
    public object GuardarGenero(Generos generos)
    {
         using (var conexion = new BDBiblioteca())
        {
            Guid llave = Guid.NewGuid();
            generos.idGenero= llave;
            conexion.Generos.Add(generos);
            return conexion.SaveChanges() == 1;
        }
    }
}