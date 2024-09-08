namespace fit_box.DTOs
{
    public class MarmitaDto
    {
        public Guid Id { get; set; }
        public string NameMarmita { get; set; }
        public int TamanhoMarmita { get; set; }
        public Guid LoginId { get; set; }
        public List<IngredienteDto> Ingredientes { get; set; }
    }
}
