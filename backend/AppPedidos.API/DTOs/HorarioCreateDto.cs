namespace AppPedidos.API.DTOs
{
    public class HorarioCreateDto
    {
        public int DiaSemana { get; set; } 
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
    }

}
