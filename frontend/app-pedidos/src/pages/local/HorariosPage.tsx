import { useEffect, useState } from "react";
import { getTurnos, actualizarTurnos } from "@/api/turnoService";
import type { TurnoDto } from "@/api/turnoService";
import { Button } from "@/components/ui/button";
import { useModal } from "@/context/ModalProvider";

const dias = [
  "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"
];

export default function Horarios() {
  const [turnos, setTurnos] = useState<TurnoDto[]>([]);
  const { showModal } = useModal();

  useEffect(() => {
    getTurnos().then(data => {
      const completos: TurnoDto[] = [];

      for (let dia = 0; dia < 7; dia++) {
        const turnosDia = data.filter(t => t.diaSemana === dia);

        for (let num = 0; num <= 1; num++) {
          const existente = turnosDia.find(t => t.numeroTurno === num);
          completos.push(existente ?? {
            id: 0,
            diaSemana: dia,
            numeroTurno: num,
            horaApertura: "",
            horaCierre: ""
          });
        }
      }

      setTurnos(completos);
    });
  }, []);

  const handleChange = (dia: number, turno: number, field: keyof TurnoDto, value: string) => {
    setTurnos(prev =>
      prev.map(t =>
        t.diaSemana === dia && t.numeroTurno === turno
          ? { ...t, [field]: value }
          : t
      )
    );
  };

  const esHorarioValido = (apertura: string, cierre: string) => {
    if (!apertura || !cierre) return false;

    const [aH, aM] = apertura.split(":").map(Number);
    const [cH, cM] = cierre.split(":").map(Number);

    const totalA = aH * 60 + aM;
    const totalC = cH * 60 + cM;

    return totalA !== totalC;
  };

  const handleSubmit = async () => {
    const datosValidos = turnos.filter(t =>
      /^([01]\d|2[0-3]):[0-5]\d(:[0-5]\d)?$/.test(t.horaApertura) &&
      /^([01]\d|2[0-3]):[0-5]\d(:[0-5]\d)?$/.test(t.horaCierre) &&
      esHorarioValido(t.horaApertura, t.horaCierre)
    );

    if (datosValidos.length === 0) {
      showModal("error", "No hay turnos válidos para guardar.");
      return;
    }

    console.log("Datos enviados al backend:", datosValidos);

    try {
      await actualizarTurnos(datosValidos);
      showModal("confirm", "Turnos guardados correctamente.");
    } catch (err) {
      showModal("error", "Error al guardar turnos. Intentalo más tarde.");
    }
  };

  return (
    <div className="p-4 max-w-6xl mx-auto space-y-6">
      <h1 className="text-3xl font-bold text-center">Gestión de Horarios</h1>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {[...Array(7)].map((_, dia) => (
          <div
            key={dia}
            className="rounded-2xl shadow-md border p-4 space-y-4 bg-white"
          >
            <h2 className="text-xl font-semibold text-center">{dias[dia]}</h2>

            {[0, 1].map(num => {
              const turno = turnos.find(t => t.diaSemana === dia && t.numeroTurno === num);
              if (!turno) return null;

              return (
                <div key={num} className="space-y-1">
                  <div className="text-sm font-medium">Turno {num + 1}</div>
                  <div className="grid grid-cols-2 gap-2">
                    <input
                      type="time"
                      value={turno.horaApertura}
                      onChange={(e) =>
                        handleChange(dia, num, "horaApertura", e.target.value)
                      }
                      className="border p-2 rounded w-full"
                    />
                    <input
                      type="time"
                      value={turno.horaCierre}
                      onChange={(e) =>
                        handleChange(dia, num, "horaCierre", e.target.value)
                      }
                      className="border p-2 rounded w-full"
                    />
                  </div>
                </div>
              );
            })}
          </div>
        ))}
      </div>

      <div className="text-center pt-4">
        <Button onClick={handleSubmit}>Guardar Turnos</Button>
      </div>
    </div>
  );
}
