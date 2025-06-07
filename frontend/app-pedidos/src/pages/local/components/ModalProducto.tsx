import { useState, useEffect } from "react";
import { Dialog, DialogContent } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import API from "@/api/axiosInstance";
import type { Producto } from "../MenuPage";

interface Props {
  open: boolean;
  onClose: () => void;
  categorias: { id: number; nombre: string }[];
  onProductoCreado: () => void;
  productoInicial?: Producto | null;
}

type Modificador = {
  id: number;
  nombre: string;
  precioExtra: number;
};

export default function ModalProducto({
  open,
  onClose,
  categorias,
  onProductoCreado,
  productoInicial
}: Props) {
  const [form, setForm] = useState({
    nombre: "",
    descripcion: "",
    precio: "",
    imagenUrl: "",
    categoriaId: ""
  });

  const [modificadores, setModificadores] = useState<Modificador[]>([]);
  const [seleccionados, setSeleccionados] = useState<number[]>([]);
  const [error, setError] = useState("");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const toggleModificador = (id: number) => {
    setSeleccionados((prev) =>
      prev.includes(id) ? prev.filter((m) => m !== id) : [...prev, id]
    );
  };

  const handleSubmit = async () => {
    try {
      setError("");

      if (productoInicial?.id) {
        await API.put(`/producto/${productoInicial.id}`, {
          nombre: form.nombre,
          descripcion: form.descripcion,
          precio: parseFloat(form.precio),
          imagenUrl: form.imagenUrl,
          categoriaId: parseInt(form.categoriaId)
        });

        await API.post(`/producto/${productoInicial.id}/modificadores`, seleccionados);
      } else {
        const prodRes = await API.post("/producto", {
          nombre: form.nombre,
          descripcion: form.descripcion,
          precio: parseFloat(form.precio),
          imagenUrl: form.imagenUrl,
          categoriaId: parseInt(form.categoriaId)
        });

        const productoId = prodRes.data.id;

        if (seleccionados.length > 0) {
          await API.post(`/producto/${productoId}/modificadores`, seleccionados);
        }
      }

      onProductoCreado();
      onClose();
    } catch (err: any) {
      console.error(err);
      setError("Error al guardar el producto");
    }
  };

  useEffect(() => {
    if (open) {
      API.get("/modificador")
        .then(res => setModificadores(res.data))
        .catch(() => setModificadores([]));

      if (productoInicial) {
        setForm({
          nombre: productoInicial.nombre,
          descripcion: productoInicial.descripcion,
          precio: productoInicial.precio.toString(),
          imagenUrl: productoInicial.imagenUrl || "",
          categoriaId: productoInicial.categoriaId?.toString() || ""
        });

        API.get(`/producto/${productoInicial.id}/modificadores`)
          .then(res => setSeleccionados(res.data.map((m: Modificador) => m.id)))
          .catch(() => setSeleccionados([]));
      }
    } else {
      setForm({
        nombre: "",
        descripcion: "",
        precio: "",
        imagenUrl: "",
        categoriaId: ""
      });
      setSeleccionados([]);
      setError("");
    }
  }, [open, productoInicial]);

  return (
    <Dialog open={open} onOpenChange={onClose}>
      <DialogContent className="max-w-md w-full space-y-6 p-6">
        <div>
          <h2 className="text-xl font-bold text-gray-800 text-center">Producto</h2>
          <p className="text-sm text-gray-500 text-center">Completá los datos del producto.</p>
        </div>
        <div className="space-y-2">
          <label className="block text-sm font-medium text-gray-700">Nombre</label>
          <Input name="nombre" value={form.nombre} onChange={handleChange} placeholder="Ej: Muzzarella" />
          <label className="block text-sm font-medium text-gray-700">Descripción</label>
          <Input name="descripcion" value={form.descripcion} onChange={handleChange} placeholder="Descripción breve" />
          <label className="block text-sm font-medium text-gray-700">Precio</label>
          <Input name="precio" type="number" value={form.precio} onChange={handleChange} placeholder="1500" />
          <label className="block text-sm font-medium text-gray-700">URL Imagen</label>
          <Input name="imagenUrl" value={form.imagenUrl} onChange={handleChange} placeholder="https://..." />
          <label className="block text-sm font-medium text-gray-700">Categoría</label>
          <select name="categoriaId" value={form.categoriaId} onChange={handleChange} className="w-full border rounded px-3 py-2">
            <option value="">Seleccioná una categoría</option>
            {categorias.map(cat => (
              <option key={cat.id} value={cat.id}>{cat.nombre}</option>
            ))}
          </select>

          {modificadores.length > 0 && (
            <div className="pt-4">
              <label className="block text-sm font-medium text-gray-700 mb-1">Modificadores</label>
              <div className="grid grid-cols-2 gap-2">
                {modificadores.map(mod => (
                  <label key={mod.id} className="flex items-center space-x-2 text-sm">
                    <input
                      type="checkbox"
                      checked={seleccionados.includes(mod.id)}
                      onChange={() => toggleModificador(mod.id)}
                    />
                    <span>{mod.nombre} (+${mod.precioExtra})</span>
                  </label>
                ))}
              </div>
            </div>
          )}
        </div>
        {error && <p className="text-sm text-red-600">{error}</p>}
        <div className="flex justify-center">
          <Button onClick={handleSubmit}>Guardar</Button>
        </div>
      </DialogContent>
    </Dialog>
  );
}
