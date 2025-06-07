import { useEffect, useState } from "react";
import API from "@/api/axiosInstance";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
  DialogClose,
} from "@/components/ui/dialog";
import { useNavigate } from "react-router-dom";

type Insumo = {
  id: number;
  nombre: string;
  unidad: string;
  precioUnitario: number;
  stock: number;
};

const UNIDADES = ["Kg", "Un", "L", "g", "ml"];

export default function InsumosPage() {
  const [insumos, setInsumos] = useState<Insumo[]>([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<Insumo | null>(null);
  const [form, setForm] = useState({ nombre: "", unidad: "" });
  const navigate = useNavigate();

  const fetch = async () => {
    try {
      const res = await API.get("/insumo");
      setInsumos(res.data);
    } catch (err) {
      console.error("Error al cargar insumos:", err);
    }
  };

  useEffect(() => {
    fetch();
  }, []);

  const openNew = () => {
    setEditing(null);
    setForm({ nombre: "", unidad: "" });
    setModalOpen(true);
  };

  const openEdit = (ins: Insumo) => {
    setEditing(ins);
    setForm({ nombre: ins.nombre, unidad: ins.unidad });
    setModalOpen(true);
  };

  const handleSubmit = async () => {
    const payload = { nombre: form.nombre, unidad: form.unidad };

    try {
      if (editing) {
        await API.put(`/insumo/${editing.id}`, payload);
      } else {
        await API.post("/insumo", payload);
      }
      fetch();
      setModalOpen(false);
    } catch (err) {
      console.error("Error guardando insumo:", err);
    }
  };

  const handleDelete = async (id: number) => {
    if (confirm("¿Eliminar este insumo?")) {
      try {
        await API.delete(`/insumo/${id}`);
        fetch();
      } catch (err) {
        console.error("Error eliminando insumo:", err);
      }
    }
  };

  return (
    <div className="p-4 sm:p-6 space-y-6 max-w-6xl mx-auto">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <h1 className="text-3xl font-bold text-gray-800">📦 Insumos</h1>
        <div className="flex gap-2 flex-wrap">
          <Button onClick={openNew}>➕ Nuevo Insumo</Button>
          <Button variant="outline" onClick={() => navigate("/local/produccion")}>
            🏭 Ir a Producción
          </Button>
        </div>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {insumos.map(ins => (
          <div key={ins.id} className="p-4 bg-white rounded-xl shadow space-y-1 border border-gray-200">
            <h2 className="text-xl font-semibold text-gray-800">{ins.nombre}</h2>
            <p className="text-gray-500 text-sm">Unidad: <strong>{ins.unidad}</strong></p>
            <p className="text-gray-500 text-sm">Precio: <strong>${ins.precioUnitario.toFixed(2)}</strong></p>
            <p className="text-gray-500 text-sm">Stock: <strong>{ins.stock} {ins.unidad}</strong></p>
            <div className="flex flex-wrap gap-2 pt-2">
              <Button variant="outline" onClick={() => openEdit(ins)}>✏️ Editar</Button>
              <Button variant="outline" onClick={() => handleDelete(ins.id)}>🗑️ Borrar</Button>
            </div>
          </div>
        ))}
      </div>

      <Dialog open={modalOpen} onOpenChange={setModalOpen}>
        <DialogContent className="sm:max-w-md">
          <DialogHeader>
            <DialogTitle>{editing ? "Editar Insumo" : "Nuevo Insumo"}</DialogTitle>
          </DialogHeader>
          <div className="space-y-3">
            <Input placeholder="Nombre" value={form.nombre} onChange={(e) => setForm({ ...form, nombre: e.target.value })} />
            <select
              value={form.unidad}
              onChange={(e) => setForm({ ...form, unidad: e.target.value })}
              className="w-full border rounded px-3 py-2"
            >
              <option value="">Seleccionar unidad</option>
              {UNIDADES.map(u => (
                <option key={u} value={u}>{u}</option>
              ))}
            </select>
          </div>
          <DialogFooter className="pt-4">
            <Button onClick={handleSubmit}>{editing ? "Actualizar" : "Guardar"}</Button>
            <DialogClose asChild>
              <Button variant="outline">Cancelar</Button>
            </DialogClose>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
