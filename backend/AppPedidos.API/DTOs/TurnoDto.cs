namespace AppPedidos.API.DTOs
{
    public class TurnoDto
    {
        public int Id { get; set; }
        public int DiaSemana { get; set; }
        public int NumeroTurno { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
    }

    public class TurnoCreateDto
    {
        public int DiaSemana { get; set; }
        public int NumeroTurno { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
    }

}
