namespace AppPedidos.API.Models
{
    public class Horario
    {
        public int Id { get; set; }

        public int LocalId { get; set; }
        public Local Local { get; set; }

        public int DiaSemana { get; set; } 
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }

        public bool Activo { get; set; } = true;
    }

}
