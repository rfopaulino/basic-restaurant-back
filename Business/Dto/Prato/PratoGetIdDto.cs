namespace Business.Dto.Prato
{
    public class PratoGetIdDto
    {
        public int Id { get; set; }
        public int IdRestaurante { get; set; }
        public string NomePrato { get; set; }
        public decimal Preco { get; set; }
    }
}
