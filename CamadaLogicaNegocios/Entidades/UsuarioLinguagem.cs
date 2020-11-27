namespace CamadaLogicaNegocios.Entidades
{
    public class UsuarioLinguagem
    {
        public Usuario Usuario { get; set; }
        public Linguagem Linguagem { get; set; }

        
        public UsuarioLinguagem()
        {

        }
   

        public  UsuarioLinguagem(Usuario usuario , Linguagem linguagem)
        {
            Usuario = usuario;
            Linguagem = linguagem;
        }
    }
}
