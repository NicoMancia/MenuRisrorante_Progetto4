namespace MenuRistorante.Models
{
    public class Portata
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Portata(int id, string name)
        {
            this.Id = id;
            this.Name = name;   
        }
    }
}
