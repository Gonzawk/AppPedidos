// src/pages/local/ComprasPage.tsx
import { useEffect, useState } from "react";
import { Button } from "@/components/ui/button";
import { useNavigate } from "react-router-dom";
import API from "@/api/axiosInstance";
import { format } from "date-fns";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogClose,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";

interface Compra {
  id: number;
  proveedorNombre: string;
  fecha: string;
  total: number;
}

interface DetalleCompra {
  insumoNombre: string;
  cantidad: number;
  precioUnitario: number;
}

interface Proveedor {
  id: number;
  nombre: string;
}

interface Insumo {
  id: number;
  nombre: string;
  unidad: string;
}

type DetalleEditable = {
  insumoId: string;
  cantidad: string;
  precioUnitario: string;
};

export default function ComprasPage() {
  const [compras, setCompras] = useState<Compra[]>([]);
  const [detalles, setDetalles] = useState<DetalleCompra[]>([]);
  const [verDetallesId, setVerDetallesId] = useState<number | null>(null);
  const [modalAbierto, setModalAbierto] = useState(false);
  const [proveedores, setProveedores] = useState<Proveedor[]>([]);
  const [insumos, setInsumos] = useState<Insumo[]>([]);
  const [proveedorId, setProveedorId] = useState("");
  const [nuevosDetalles, setNuevosDetalles] = useState<DetalleEditable[]>([]);

  const navigate = useNavigate();

  useEffect(() => {
    cargarCompras();
    API.get("/proveedor").then(r => setProveedores(r.data));
    API.get("/insumo").then(r => setInsumos(r.data));
  }, []);

  const cargarCompras = async () => {
    try {
      const res = await API.get("/compra");
      setCompras(res.data);
    } catch (error) {
      console.error("Error al cargar compras", error);
    }
  };

  const verDetalles = async (id: number) => {
    try {
      const res = await API.get(`/detallecompra/${id}`);
      setDetalles(res.data);
      setVerDetallesId(id);
    } catch (err) {
      console.error("Error al cargar detalles", err);
    }
  };

  const cerrarDetalles = () => setVerDetallesId(null);

  const handleNuevoDetalleChange = (
    index: number,
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setNuevosDetalles(prev => {
      const nuevos = [...prev];
      nuevos[index] = {
        ...nuevos[index],
        [name]: value
      };
      return nuevos;
    });
  };

  const agregarDetalle = () => {
    setNuevosDetalles([...nuevosDetalles, { insumoId: "", cantidad: "", precioUnitario: "" }]);
  };

  const cargarCompra = async () => {
    const detallesFormateados = nuevosDetalles.map(d => ({
      insumoId: parseInt(d.insumoId),
      cantidad: parseFloat(d.cantidad),
      precioUnitario: parseFloat(d.precioUnitario)
    }));

    const body = {
      proveedorId: parseInt(proveedorId),
      detalles: detallesFormateados
    };

    console.log("Enviando compra:", JSON.stringify(body, null, 2));

    try {
      const res = await API.post("/compra", body);
      console.log("Compra creada:", res.data);
      setModalAbierto(false);
      setProveedorId("");
      setNuevosDetalles([]);
      cargarCompras();
    } catch (error: any) {
      if (error.response) {
        console.error("Backend error status:", error.response.status);
        console.error("Backend error data:", error.response.data);
        if (error.response.data?.errors) {
          const mensaje = Object.entries(error.response.data.errors)
            .map(([campo, errores]) => `${campo}: ${(errores as string[]).join(", ")}`)
            .join("\n");
          alert("Errores de validación:\n" + mensaje);
        } else {
          alert("Error al guardar compra:\n" + JSON.stringify(error.response.data, null, 2));
        }
      } else {
        console.error("Network or client error:", error.message);
        alert("Error de red o del cliente: " + error.message);
      }
    }
  };

  return (
    <div className="p-4 sm:p-6 space-y-6 max-w-6xl mx-auto">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <h1 className="text-3xl font-bold text-gray-800">🧾 Compras</h1>
        <div className="flex gap-2 flex-wrap justify-center w-full sm:w-auto">
          <Button variant="outline" onClick={() => navigate("/local/proveedores")}>⬅ Volver a Proveedores</Button>
          <Button onClick={() => setModalAbierto(true)}>➕ Nueva Compra</Button>
        </div>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {compras.map((c) => (
          <div key={c.id} className="p-4 bg-white rounded-xl shadow space-y-2 border border-gray-200">
            <div className="flex justify-between items-center">
              <div>
                <h2 className="text-xl font-semibold text-gray-800">{c.proveedorNombre}</h2>
                <p className="text-gray-500 text-sm">
                  Fecha: <strong>{format(new Date(c.fecha), "dd/MM/yyyy")}</strong>
                </p>
                <p className="text-gray-500 text-sm">
                  Total: <strong>${c.total.toFixed(2)}</strong>
                </p>
              </div>
              <Button onClick={() => verDetalles(c.id)} variant="outline">
                📋 Detalles
              </Button>
            </div>

            {verDetallesId === c.id && (
              <div className="mt-4 border-t pt-2">
                <h3 className="font-semibold text-gray-700 mb-2">Detalle de la compra:</h3>
                <ul className="text-sm text-gray-700 space-y-1">
                  {detalles.map((d, i) => (
                    <li key={i} className="flex justify-between">
                      <span>{d.insumoNombre}</span>
                      <span>
                        {d.cantidad} × ${d.precioUnitario.toFixed(2)} = ${(d.cantidad * d.precioUnitario).toFixed(2)}
                      </span>
                    </li>
                  ))}
                </ul>
                <div className="text-right mt-2">
                  <Button variant="outline" onClick={cerrarDetalles}>
                    ✖ Cerrar
                  </Button>
                </div>
              </div>
            )}
          </div>
        ))}
      </div>

      <Dialog open={modalAbierto} onOpenChange={setModalAbierto}>
        <DialogContent className="sm:max-w-2xl">
          <DialogHeader>
            <DialogTitle>Registrar nueva compra</DialogTitle>
            <DialogClose className="absolute right-4 top-4 text-gray-400 hover:text-gray-700">✖</DialogClose>
          </DialogHeader>
          <div className="space-y-4">
            <div>
              <label className="block font-medium mb-1">Proveedor</label>
              <select
                name="proveedorId"
                value={proveedorId}
                onChange={(e) => setProveedorId(e.target.value)}
                className="w-full border rounded px-3 py-2"
              >
                <option value="">Seleccionar proveedor</option>
                {proveedores.map((p) => (
                  <option key={p.id} value={p.id}>
                    {p.nombre}
                  </option>
                ))}
              </select>
            </div>

            {nuevosDetalles.map((d, index) => (
              <div key={index} className="grid grid-cols-1 sm:grid-cols-4 gap-2">
                <select
                  name="insumoId"
                  value={d.insumoId}
                  onChange={(e) => handleNuevoDetalleChange(index, e)}
                  className="border rounded px-2 py-1"
                >
                  <option value="">Insumo</option>
                  {insumos.map((i) => (
                    <option key={i.id} value={i.id}>
                      {i.nombre} ({i.unidad})
                    </option>
                  ))}
                </select>
                <Input
                  name="cantidad"
                  type="number"
                  value={d.cantidad}
                  onChange={(e) => handleNuevoDetalleChange(index, e)}
                  placeholder="Cantidad"
                />
                <Input
                  name="precioUnitario"
                  type="number"
                  value={d.precioUnitario}
                  onChange={(e) => handleNuevoDetalleChange(index, e)}
                  placeholder="Precio"
                />
              </div>
            ))}

            <div className="flex justify-between pt-4">
              <Button onClick={agregarDetalle} variant="outline">
                ➕ Agregar Insumo
              </Button>
              <Button onClick={cargarCompra} disabled={!proveedorId || nuevosDetalles.length === 0}>
                💾 Guardar Compra
              </Button>
            </div>
          </div>
        </DialogContent>
      </Dialog>
    </div>
  );
}
