using System.ComponentModel.DataAnnotations;

namespace Tienda_PrograVI.Models
{
    public class Cliente
    {
        [Key]
        public int Id_cliente { get; set; }
        
        [Required(ErrorMessage = "El nombre es ")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(100, ErrorMessage = "El correo no puede tener más de 100 caracteres.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(20, ErrorMessage = "La contrasea no puede tener más de 20 caracteres.")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "La direccion es obligatoria")]
        [StringLength(100, ErrorMessage = "La direccion no puede tener más de 100 caracteres.")]
        public string Direccion { get; set; }


    }
}
