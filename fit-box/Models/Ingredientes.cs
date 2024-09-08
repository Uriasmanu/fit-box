namespace fit_box.Models
{
    public class Ingredientes
    {
        public Guid Id { get; set; }
        public string NameIngrediente { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public int QuantidadeEmGramas { get; set; }

        // Relacionamento com o usuário
        public Guid LoginId { get; set; }
        public Login Login { get; set; }
    }
}
