namespace fit_box.Models
{
    public class MarmitaIngrediente
    {
        public Guid MarmitaId { get; set; }
        public Marmita Marmita { get; set; }

        public Guid IngredienteId { get; set; }
        public Ingredientes Ingrediente { get; set; }

        public int QuantidadeEmGramas { get; set; }
    }
}