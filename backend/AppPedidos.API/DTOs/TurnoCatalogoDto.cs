namespace AppPedidos.API.DTOs
{
    public class TurnoCatalogoDTO
    {
        public int NumeroTurno { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
    }

}
