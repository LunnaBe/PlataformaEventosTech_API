namespace PlataformaEventosTech_API.Models
{
    public class Evento
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Conteudo { get; set; }

        public string Data_Hora { get; set; }

        public string Localizacao { get; set; }

        public double Preco { get; set; }
    }
}
