// src/pages/local/components/ProductoCard.tsx
import { Card, CardContent } from "@/components/ui/card";

export default function ProductoCard({ producto }: { producto: any }) {
  return (
    <Card className="shadow-md">
      <CardContent className="p-4 space-y-1">
        <h3 className="text-lg font-semibold text-gray-800">{producto.nombre}</h3>
        <p className="text-gray-500 text-sm">{producto.descripcion}</p>
        <p className="text-green-700 font-medium mt-1">${producto.precio.toFixed(2)}</p>
        <span
          className={`inline-block text-sm font-medium ${
            producto.activo ? "text-green-600" : "text-red-500"
          }`}
        >
          {producto.activo ? "Activo" : "Inactivo"}
        </span>

        {producto.modificadores?.length > 0 && (
          <div className="text-xs text-gray-600">
            Extras: {producto.modificadores.map((m: any) => m.nombre).join(", ")}
          </div>
        )}
      </CardContent>
    </Card>
  );
}
