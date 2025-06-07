namespace AppPedidos.API.DTOs
{
    public class HorarioDto
    {
        public int Id { get; set; }
        public int DiaSemana { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
    }

}
