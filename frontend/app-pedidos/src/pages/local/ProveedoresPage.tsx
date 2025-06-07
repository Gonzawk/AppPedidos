// src/pages/local/ProveedoresPage.tsx
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

interface Proveedor {
  id: number;
  nombre: string;
  telefono: string;
  email: string;
  direccion: string;
}

export default function ProveedoresPage() {
  const [proveedores, setProveedores] = useState<Proveedor[]>([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<Proveedor | null>(null);
  const [form, setForm] = useState<Omit<Proveedor, "id">>({
    nombre: "",
    telefono: "",
    email: "",
    direccion: "",
  });
  const navigate = useNavigate();

  const fetch = async () => {
    const res = await API.get("/proveedor");
    setProveedores(res.data);
  };

  useEffect(() => {
    fetch();
  }, []);

  const openNew = () => {
    setEditing(null);
    setForm({ nombre: "", telefono: "", email: "", direccion: "" });
    setModalOpen(true);
  };

  const openEdit = (p: Proveedor) => {
    setEditing(p);
    setForm({
      nombre: p.nombre,
      telefono: p.telefono,
      email: p.email,
      direccion: p.direccion,
    });
    setModalOpen(true);
  };

  const handleSave = async () => {
    try {
      if (editing) {
        await API.put(`/proveedor/${editing.id}`, form);
      } else {
        await API.post("/proveedor", form);
      }
      setModalOpen(false);
      fetch();
    } catch (err) {
      console.error("Error al guardar proveedor", err);
    }
  };

  const handleDelete = async (id: number) => {
    if (confirm("¿Eliminar este proveedor?")) {
      await API.delete(`/proveedor/${id}`);
      fetch();
    }
  };

  return (
    <div className="p-4 sm:p-6 space-y-6 max-w-6xl mx-auto">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <h1 className="text-3xl font-bold text-gray-800">📇 Proveedores</h1>
        <div className="flex gap-2 flex-wrap">
          <Button onClick={openNew}>➕ Agregar Proveedor</Button>
          <Button variant="outline" onClick={() => navigate("/local/compras")}>
            📦 Ir a Compras
          </Button>
        </div>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {proveedores.map((p) => (
          <div key={p.id} className="p-4 bg-white rounded-xl shadow space-y-1 border border-gray-200">
            <h2 className="text-xl font-semibold text-gray-800">{p.nombre}</h2>
            <p className="text-gray-500 text-sm">📞 {p.telefono}</p>
            <p className="text-gray-500 text-sm">📧 {p.email}</p>
            <p className="text-gray-500 text-sm">📍 {p.direccion}</p>
            <div className="flex flex-wrap gap-2 pt-2">
              <Button variant="outline" onClick={() => openEdit(p)}>
                ✏️ Editar
              </Button>
              <Button variant="outline" onClick={() => handleDelete(p.id)}>
                🗑️ Borrar
              </Button>
            </div>
          </div>
        ))}
      </div>

      <Dialog open={modalOpen} onOpenChange={setModalOpen}>
        <DialogContent className="sm:max-w-md">
          <DialogHeader>
            <DialogTitle>
              {editing ? "Editar Proveedor" : "Agregar Proveedor"}
            </DialogTitle>
          </DialogHeader>
          <div className="space-y-4">
            <Input
              placeholder="Nombre"
              value={form.nombre}
              onChange={(e) => setForm({ ...form, nombre: e.target.value })}
            />
            <Input
              placeholder="Teléfono"
              value={form.telefono}
              onChange={(e) => setForm({ ...form, telefono: e.target.value })}
            />
            <Input
              placeholder="Email"
              value={form.email}
              onChange={(e) => setForm({ ...form, email: e.target.value })}
            />
            <Input
              placeholder="Dirección"
              value={form.direccion}
              onChange={(e) => setForm({ ...form, direccion: e.target.value })}
            />
          </div>
          <DialogFooter className="pt-4">
            <Button onClick={handleSave}>
              {editing ? "Actualizar" : "Guardar"}
            </Button>
            <DialogClose asChild>
              <Button variant="outline">Cancelar</Button>
            </DialogClose>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
