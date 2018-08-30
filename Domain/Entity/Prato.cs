using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Prato")]
    public class Prato
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_restaurante")]
        public int IdRestaurante { get; set; }

        [Column("nome_prato")]
        public string NomePrato { get; set; }

        [Column("preco")]
        public decimal Preco { get; set; }

        public virtual Restaurante Restaurante { get; set; }
    }
}
