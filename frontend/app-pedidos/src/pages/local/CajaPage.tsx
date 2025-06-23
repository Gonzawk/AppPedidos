// src/pages/local/CajaPage.tsx
import { useEffect, useState } from 'react';
import { format } from 'date-fns';
import type { CajaResponse } from "@/api/cajaService";
import { getCajaPorFecha } from "@/api/cajaService";
import { Button } from '@/components/ui/button';
import { Card, CardContent } from '@/components/ui/card';

export default function CajaPage() {
  const localId = 1; // reemplazá con ID real
  const [fecha, setFecha] = useState(new Date());
  const [data, setData] = useState<CajaResponse | null>(null);

  useEffect(() => {
    getCajaPorFecha(localId, fecha).then(setData);
  }, [fecha]);

  const safeFormat = (value: number | undefined) =>
    typeof value === "number" ? value.toFixed(2) : "0.00";

  return (
    <div className="p-4 space-y-4">
      <h1 className="text-xl font-semibold text-center">Caja del día</h1>

      <div className="flex justify-center">
        <input
          type="date"
          value={format(fecha, 'yyyy-MM-dd')}
          onChange={(e) => setFecha(new Date(e.target.value))}
          className="border px-2 py-1 rounded"
        />
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <Card><CardContent><div>Total ventas</div><div className="font-bold">${safeFormat(data?.totalVentas)}</div></CardContent></Card>
        <Card><CardContent><div>Gastos variables</div><div className="font-bold">${safeFormat(data?.totalGastos)}</div></CardContent></Card>
        <Card><CardContent><div>Gastos fijos prorrateo</div><div className="font-bold">${safeFormat(data?.totalFijosProrrateado)}</div></CardContent></Card>
        <Card><CardContent><div>Caja final</div><div className="font-bold">${safeFormat(data?.cajaFinal)}</div></CardContent></Card>
      </div>

      <div className="flex justify-around pt-4">
        <Button variant="outline">Registrar gasto variable</Button>
        <Button variant="outline">Registrar gasto fijo</Button>
      </div>
    </div>
  );
}
