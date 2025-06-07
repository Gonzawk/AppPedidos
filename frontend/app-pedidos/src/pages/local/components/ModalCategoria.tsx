// src/pages/local/components/ModalCategoria.tsx
import { useState } from "react";
import { Dialog, DialogContent } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import API from "@/api/axiosInstance";

export default function ModalCategoria({
  open,
  onClose,
  onCategoriaCreada,
}: {
  open: boolean;
  onClose: () => void;
  onCategoriaCreada?: () => void;
}) {
  const [nombre, setNombre] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleGuardar = async () => {
    if (!nombre.trim()) {
      setError("El nombre es obligatorio.");
      return;
    }

    setLoading(true);
    try {
      await API.post("/categoria", { nombre });
      setNombre("");
      onClose();
      onCategoriaCreada?.();
    } catch (err) {
      console.error("Error al crear categoría:", err);
      setError("No se pudo guardar la categoría.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onOpenChange={onClose}>
      <DialogContent className="max-w-md w-full space-y-6 p-6 mx-auto">
        <div>
          <h2 className="text-xl font-bold text-gray-800">Nueva Categoría</h2>
          <p className="text-sm text-gray-500">Completá el nombre de la categoría para tu menú.</p>
        </div>
        <div className="space-y-2">
          <label className="block text-sm font-medium text-gray-700">Nombre</label>
          <Input
            placeholder="Ej: Pizzas, Bebidas, Postres"
            value={nombre}
            onChange={(e) => setNombre(e.target.value)}
          />
          {error && <p className="text-sm text-red-600">{error}</p>}
        </div>
        <div className="flex justify-end">
          <Button onClick={handleGuardar} disabled={loading}>
            {loading ? "Guardando..." : "Guardar"}
          </Button>
        </div>
      </DialogContent>
    </Dialog>
  );
}
