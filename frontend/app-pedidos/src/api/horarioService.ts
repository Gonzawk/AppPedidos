import axios from "@/api/axiosInstance";

export const getHorarios = async () => {
  const res = await axios.get("/horario/mios");
  return res.data;
};

export const actualizarHorarios = async (data: HorarioDto[]) => {
  const res = await axios.put("/horario", data);
  return res.data;
};

export interface HorarioDto {
  id: number;
  diaSemana: number;
  horaApertura: string;
  horaCierre: string;
}
