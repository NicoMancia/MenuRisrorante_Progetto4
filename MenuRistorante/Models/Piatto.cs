namespace MenuRistorante.Models
{
    public class Piatto
    {
        public int PiattoId { get; set; }
        public string Nome { get; set; }
        public decimal Prezzo { get; set; }
        //public int IDPortata { get; set; }
        public string NomePortata { get; set; }

        public Piatto(int IDPiatto, string nome, decimal prezzo, string nomePortata)
        {
            this.PiattoId = IDPiatto;
            this.Nome = nome;
            this.Prezzo = prezzo;
            this.NomePortata = nomePortata;
        }

        public Piatto(string nomePortata)
        {
            this.NomePortata = nomePortata;
        }

        public Piatto(string nome, decimal prezzo)
        {
            this.Nome = nome;
            this.Prezzo = prezzo;
        }
    }
}
