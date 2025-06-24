using System.ComponentModel.DataAnnotations;

namespace Tienda_PrograVI.Models
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }

        [Required]
        public string Tipo_categoria { get; set; }

        public ICollection<Producto> Producto{ get; set; }
    }
}
