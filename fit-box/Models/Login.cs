namespace fit_box.Models
{
    public class Login
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Marmita> Marmitas { get; set; }
        public ICollection<Ingredientes> Ingredientes { get; set; }
    }
}
