import { useState, useEffect } from "react";
import { Dialog, DialogContent } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import API from "@/api/axiosInstance";

interface Props {
  open: boolean;
  onClose: () => void;
}

type Modificador = {
  id: number;
  nombre: string;
  precioExtra: number;
};

export default function ModalModificadores({ open, onClose }: Props) {
  const [modificadores, setModificadores] = useState<Modificador[]>([]);
  const [nombre, setNombre] = useState("");
  const [precioExtra, setPrecioExtra] = useState("");
  const [mensaje, setMensaje] = useState("");
  const [editandoId, setEditandoId] = useState<number | null>(null);

  const fetchModificadores = async () => {
    try {
      const res = await API.get("/modificador");
      setModificadores(res.data);
    } catch (err) {
      console.error(err);
    }
  };

  const handleGuardar = async () => {
    try {
      if (editandoId) {
        await API.put(`/modificador/${editandoId}`, {
          nombre,
          precioExtra: parseFloat(precioExtra)
        });
        setMensaje("Modificador actualizado.");
      } else {
        await API.post("/modificador", {
          nombre,
          precioExtra: parseFloat(precioExtra)
        });
        setMensaje("Modificador creado con éxito.");
      }

      setNombre("");
      setPrecioExtra("");
      setEditandoId(null);
      await fetchModificadores();
    } catch (err) {
      console.error(err);
      setMensaje("Error al guardar el modificador.");
    }
  };

  const cargarParaEditar = (mod: Modificador) => {
    setEditandoId(mod.id);
    setNombre(mod.nombre);
    setPrecioExtra(mod.precioExtra.toString());
  };

  useEffect(() => {
    if (open) fetchModificadores();
    else {
      setNombre("");
      setPrecioExtra("");
      setMensaje("");
      setEditandoId(null);
    }
  }, [open]);

  return (
    <Dialog open={open} onOpenChange={onClose}>
      <DialogContent className="max-w-md w-full space-y-6 p-6">
        <div>
          <h2 className="text-xl font-bold text-gray-800">Gestión de Modificadores</h2>
          <p className="text-sm text-gray-500">Creá o modificá elementos personalizables para productos.</p>
        </div>

        <div className="space-y-2">
          <label className="block text-sm font-medium text-gray-700">Nombre</label>
          <Input value={nombre} onChange={(e) => setNombre(e.target.value)} placeholder="Ej: Extra queso" />

          <label className="block text-sm font-medium text-gray-700">Precio Extra</label>
          <Input
            type="number"
            value={precioExtra}
            onChange={(e) => setPrecioExtra(e.target.value)}
            placeholder="Ej: 200"
          />
        </div>

      <div className="flex justify-between">
  {editandoId && (
    <Button variant="outline" onClick={() => {
      setEditandoId(null);
      setNombre("");
      setPrecioExtra("");
      setMensaje("");
    }}>
      Cancelar edición
    </Button>
  )}
  <Button onClick={handleGuardar}>{editandoId ? "Actualizar" : "Guardar"}</Button>
</div>


        {mensaje && <p className="text-sm text-blue-600 text-center">{mensaje}</p>}

        {modificadores.length > 0 && (
          <div className="pt-4">
            <h3 className="text-sm font-semibold text-gray-600 mb-2">Modificadores existentes</h3>
            <ul className="space-y-1 max-h-40 overflow-auto">
              {modificadores.map(mod => (
                <li
                  key={mod.id}
                  onClick={() => cargarParaEditar(mod)}
                  className="p-2 rounded hover:bg-gray-100 cursor-pointer flex justify-between items-center text-sm border"
                >
                  <span>{mod.nombre}</span>
                  <span className="text-gray-500">+${mod.precioExtra}</span>
                </li>
              ))}
            </ul>
          </div>
        )}
      </DialogContent>
    </Dialog>
  );
}
