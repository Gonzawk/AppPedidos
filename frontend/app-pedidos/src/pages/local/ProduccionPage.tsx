import { useEffect, useState } from "react";
import API from "@/api/axiosInstance";
import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter, DialogClose } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { useNavigate } from "react-router-dom";

interface Producto { id: number; nombre: string; }
interface Insumo { id: number; nombre: string; precioUnitario: number; }
interface DetalleProduccion { insumoId: number; insumoNombre: string; cantidadUtilizada: number; precioUnitario: number; }
interface Produccion {
  id: number;
  fecha: string;
  cantidadProducida: number;
  producto: { id: number; nombre: string };
  detalles: {
    insumoId: number;
    insumoNombre: string;
    cantidadUtilizada: number;
    precioUnitario: number;
    subtotal: number;
  }[];
  total: number;
  costoUnitario: number;
}

export default function ProduccionPage() {
  const [productos, setProductos] = useState<Producto[]>([]);
  const [insumos, setInsumos] = useState<Insumo[]>([]);
  const [producciones, setProducciones] = useState<Produccion[]>([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [expandido, setExpandido] = useState<number | null>(null);
  const navigate = useNavigate();

  const [productoId, setProductoId] = useState<string>("");
  const [cantidadProducida, setCantidadProducida] = useState<string>("");
  const [detalles, setDetalles] = useState<DetalleProduccion[]>([]);

  useEffect(() => {
    API.get("/producto").then(res => setProductos(res.data));
    API.get("/insumo").then(res => setInsumos(res.data));
    API.get("/produccion").then(res => setProducciones(res.data));
  }, []);

  const agregarInsumo = () => {
    setDetalles([...detalles, { insumoId: 0, insumoNombre: "", cantidadUtilizada: 0, precioUnitario: 0 }]);
  };

  const actualizarDetalle = (index: number, campo: keyof DetalleProduccion, valor: any) => {
    const nuevos = [...detalles];
    if (campo === "insumoId") {
      const insumo = insumos.find(i => i.id === parseInt(valor));
      nuevos[index].insumoId = insumo?.id || 0;
      nuevos[index].insumoNombre = insumo?.nombre || "";
      nuevos[index].precioUnitario = insumo?.precioUnitario || 0;
    } else if (campo === "cantidadUtilizada") {
      nuevos[index].cantidadUtilizada = parseFloat(valor);
    }
    setDetalles(nuevos);
  };

  const eliminarDetalle = (index: number) => {
    const nuevos = [...detalles];
    nuevos.splice(index, 1);
    setDetalles(nuevos);
  };

  const registrarProduccion = async () => {
    try {
      const payload = {
        productoId: parseInt(productoId),
        cantidadProducida: parseInt(cantidadProducida),
        fecha: new Date().toISOString(),
        detalles
      };
      await API.post("/produccion", payload);
      setModalOpen(false);
      setProductoId("");
      setCantidadProducida("");
      setDetalles([]);
      const res = await API.get("/produccion");
      setProducciones(res.data);
    } catch (error) {
      console.error("Error al registrar producción:", error);
    }
  };

  return (
    <div className="p-4 sm:p-6 space-y-6 max-w-6xl mx-auto">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <h1 className="text-3xl font-bold text-gray-800">🏭 Producciones</h1>
        <div className="flex gap-2 flex-wrap">
          <Button onClick={() => setModalOpen(true)}>➕ Nueva Producción</Button>
          <Button variant="outline" onClick={() => navigate("/local/insumos")}>📦 Ir a Insumos</Button>
        </div>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {producciones.map(p => (
          <div key={p.id} className="p-4 bg-white rounded-xl shadow space-y-1 border border-gray-200">
            <h2 className="text-xl font-semibold text-gray-800">{p.producto.nombre}</h2>
            <p className="text-gray-500 text-sm">Fecha: {new Date(p.fecha).toLocaleString()}</p>
            <p className="text-gray-500 text-sm">Cantidad producida: <strong>{p.cantidadProducida}</strong></p>
            <p className="text-gray-500 text-sm">Costo total: <strong>${p.total.toFixed(2)}</strong></p>
            <p className="text-gray-500 text-sm">Costo por unidad: <strong>${p.costoUnitario.toFixed(2)}</strong></p>
            <div className="pt-2">
              <Button variant="outline" onClick={() => setExpandido(expandido === p.id ? null : p.id)}>
                {expandido === p.id ? "Ocultar detalles" : "Ver detalles"}
              </Button>
              {expandido === p.id && (
                <div className="text-sm pt-2 space-y-1 border-t mt-2">
                  <strong>Insumos utilizados:</strong>
                  {p.detalles.map(d => (
                    <div key={d.insumoId}>
                      {d.insumoNombre} - {d.cantidadUtilizada} u. x ${d.precioUnitario.toFixed(2)} = ${d.subtotal.toFixed(2)}
                    </div>
                  ))}
                </div>
              )}
            </div>
          </div>
        ))}
      </div>

      <Dialog open={modalOpen} onOpenChange={setModalOpen}>
        <DialogContent className="sm:max-w-xl">
          <DialogHeader>
            <DialogTitle>Cargar Producción</DialogTitle>
          </DialogHeader>
          <div className="space-y-4">
            <Select onValueChange={setProductoId} value={productoId}>
              <SelectTrigger><SelectValue placeholder="Seleccioná producto" /></SelectTrigger>
              <SelectContent>
                {productos.map(p => <SelectItem key={p.id} value={p.id.toString()}>{p.nombre}</SelectItem>)}
              </SelectContent>
            </Select>
            <Input type="number" placeholder="Cantidad producida" value={cantidadProducida} onChange={e => setCantidadProducida(e.target.value)} />
            <div className="space-y-2">
              {detalles.map((d, idx) => (
                <div key={idx} className="grid grid-cols-4 gap-2 items-center">
                  <Select onValueChange={(v) => actualizarDetalle(idx, "insumoId", v)} value={d.insumoId.toString()}>
                    <SelectTrigger><SelectValue placeholder="Insumo" /></SelectTrigger>
                    <SelectContent>
                      {insumos.map(i => <SelectItem key={i.id} value={i.id.toString()}>{i.nombre}</SelectItem>)}
                    </SelectContent>
                  </Select>
                  <Input type="number" value={d.cantidadUtilizada.toString()} onChange={e => actualizarDetalle(idx, "cantidadUtilizada", e.target.value)} placeholder="Cantidad" />
                  <p className="text-sm text-gray-600">${d.precioUnitario}</p>
                  <Button variant="outline" onClick={() => eliminarDetalle(idx)}>🗑️</Button>
                </div>
              ))}
              <Button variant="outline" onClick={agregarInsumo}>➕ Agregar insumo</Button>
            </div>
          </div>
          <DialogFooter className="pt-4">
            <Button onClick={registrarProduccion}>✅ Registrar</Button>
            <DialogClose asChild>
              <Button variant="outline">Cancelar</Button>
            </DialogClose>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
