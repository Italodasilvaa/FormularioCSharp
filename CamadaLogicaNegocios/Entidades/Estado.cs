namespace CamadaLogicaNegocios.Entidades
{
    public class Estado
    {
        public int IdEstado { get; set; }

        public string Descricao { get; set; }

        public string Uf { get; set; }


        public Estado()
        {

        }

        public Estado(int idEstado, string descricao, string uf)
        {
            IdEstado = idEstado;
            Descricao = descricao;
            Uf = uf;
        }
    }
}
