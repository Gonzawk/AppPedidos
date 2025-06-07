import { useState } from "react";
import { Tabs, TabsList, TabsTrigger, TabsContent } from "@/components/ui/tabs";
import { Card } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";

type Pedido = {
  id: number;
  cliente: string;
  estado: string;
  total: number;
  hora: string;
};

const estados = ["pendiente", "preparando", "listo", "en_camino", "entregado"];
const etiquetas: Record<string, string> = {
  pendiente: "Pendientes",
  preparando: "En preparación",
  listo: "Listos",
  en_camino: "En camino",
  entregado: "Entregados"
};

const mockPedidos: Pedido[] = [
  { id: 1, cliente: "Juan Pérez", estado: "pendiente", total: 1200, hora: "12:30" },
  { id: 2, cliente: "Ana Gómez", estado: "preparando", total: 850, hora: "13:10" },
  { id: 3, cliente: "Carlos Ruiz", estado: "listo", total: 950, hora: "13:40" },
  { id: 4, cliente: "Laura Díaz", estado: "en_camino", total: 1050, hora: "14:00" },
  { id: 5, cliente: "Pedro Ledesma", estado: "entregado", total: 980, hora: "11:45" },
];

export default function Pedidos() {
  const [activo, setActivo] = useState("pendiente");

  return (
    <div className="p-4 max-w-5xl mx-auto space-y-6">
      <h1 className="text-2xl md:text-3xl font-bold text-center">Gestión de Pedidos</h1>

      <Tabs defaultValue="pendiente" value={activo} onValueChange={setActivo}>
        <div className="overflow-x-auto pb-2">
          <TabsList className="min-w-max flex gap-2 w-full">
            {estados.map((estado) => (
              <TabsTrigger key={estado} value={estado} className="whitespace-nowrap">
                {etiquetas[estado]}
              </TabsTrigger>
            ))}
          </TabsList>
        </div>

        {estados.map((estado) => (
          <TabsContent key={estado} value={estado} className="space-y-4">
            {mockPedidos
              .filter((p) => p.estado === estado)
              .map((pedido) => (
                <Card
                  key={pedido.id}
                  className="p-4 shadow flex flex-col gap-2 sm:flex-row sm:justify-between sm:items-center"
                >
                  <div>
                    <h3 className="font-semibold text-lg">{pedido.cliente}</h3>
                    <p className="text-sm text-gray-500">Pedido #{pedido.id} - {pedido.hora}</p>
                  </div>
                  <div className="sm:text-right">
                    <Badge className="mb-1">{etiquetas[pedido.estado]}</Badge>
                    <p className="text-xl font-bold">${pedido.total}</p>
                  </div>
                </Card>
              ))}
          </TabsContent>
        ))}
      </Tabs>
    </div>
  );
}
    