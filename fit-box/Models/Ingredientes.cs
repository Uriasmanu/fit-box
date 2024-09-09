namespace fit_box.Models
{
    public class Ingredientes
    {
        public Guid Id { get; set; }
        public string NameIngrediente { get; set; }
        public int QuantidadeEmGramas { get; set; }

        // Many-to-Many relationship
        public ICollection<Marmita> Marmitas { get; set; } = new List<Marmita>();

    }

}
