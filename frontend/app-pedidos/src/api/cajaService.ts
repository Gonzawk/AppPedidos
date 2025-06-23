import axios from 'axios';
import { format } from 'date-fns';

export interface CajaResponse {
  id: number;
  localId: number;
  fecha: string;
  totalVentas: number;
  totalGastos: number;
  totalFijosProrrateado: number;
  cajaFinal: number;
}

export async function getCajaPorFecha(localId: number, fecha: Date): Promise<CajaResponse | null> {
  const formatted = format(fecha, 'yyyy-MM-dd');
  const res = await axios.get<CajaResponse[]>(`/api/caja/${localId}/cajas?desde=${formatted}&hasta=${formatted}`);
  return res.data.length ? res.data[0] : null;
}
