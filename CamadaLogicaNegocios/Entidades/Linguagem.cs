namespace CamadaLogicaNegocios.Entidades
{
    public class Linguagem
    {
        public int IdLinguagem { get; set; }
        public string Descricao { get; set; }

       public Linguagem() { 

        }
        public Linguagem(int idLinguagem, string descricao)
        {
            IdLinguagem = idLinguagem;
            Descricao = descricao;
        }
    }
}
