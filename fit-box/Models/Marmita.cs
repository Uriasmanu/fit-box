namespace fit_box.Models
{
    public class Marmita
    {
        public Guid Id { get; set; }
        public string NameMarmita { get; set; }
        public int TamanhoMarmita { get; set; }

        public Guid LoginId { get; set; }
        public Login Login { get; set; }

        public ICollection<MarmitaIngrediente> MarmitaIngredientes { get; set; }
    }
}
