import axios from "@/api/axiosInstance";

export interface TurnoDto {
  id: number;
  diaSemana: number;
  numeroTurno: number;
  horaApertura: string;
  horaCierre: string;
}

export const getTurnos = async (): Promise<TurnoDto[]> => {
  const res = await axios.get("/turno/mios");
  return res.data;
};

export const actualizarTurnos = async (data: TurnoDto[]) => {
  const res = await axios.put("/turno", data);
  return res.data;
};
