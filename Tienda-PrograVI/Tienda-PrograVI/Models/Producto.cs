using System.ComponentModel.DataAnnotations;

namespace Tienda_PrograVI.Models
{
    public class Producto
    {
        [Key]
       public int Id_producto { get; set; }
       public string Nombre { get; set; }
       public string Descripcion { get; set; }
       public double Precio { get; set; }
       public int Stock { get; set; }
       public int  Id_Categoria {  get; set; }


    }
}
