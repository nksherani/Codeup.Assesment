namespace Codeup.Assesment.Entities
{
    public class Country
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string ContinentName { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Merchant> Merchants { get; set; }

    }
}
