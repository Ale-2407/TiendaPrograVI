using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda_PrograVI.Models
{
    public class Producto
    {
        [Key]
        public int Id_producto { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Seleccione una categoría.")]
        public int Id_categoria { get; set; }

        [ForeignKey("Id_categoria")]
        public Categoria? Categoria { get; set; }  
    }
}
