using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Restaurante")]
    public class Restaurante
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        public virtual ICollection<Prato> Prato { get; set; }
    }
}
