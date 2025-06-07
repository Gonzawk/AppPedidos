import { useEffect, useState } from "react";
import { PlusCircle } from "lucide-react";
import CategoriaCard from "./components/CategoriaCard";
import ModalCategoria from "./components/ModalCategoria";
import ModalProducto from "./components/ModalProducto";
import ModalModificadores from "./components/ModalModificadores";
import { Button } from "@/components/ui/button";
import API from "@/api/axiosInstance";

// Tipos
export type Producto = {
  id: number;
  nombre: string;
  descripcion: string;
  precio: number;
  imagenUrl?: string;
  activo: boolean;
  categoriaId: number;
};

export type Categoria = {
  id: number;
  nombre: string;
  productos: Producto[];
};

export default function MenuPage() {
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [modalCategoriaOpen, setModalCategoriaOpen] = useState(false);
  const [modalProductoOpen, setModalProductoOpen] = useState(false);
  const [modalModificadoresOpen, setModalModificadoresOpen] = useState(false);
  const [productoSeleccionado, setProductoSeleccionado] = useState<Producto | null>(null);

  const fetchCategoriasYProductos = async () => {
    try {
      const catRes = await API.get("/categoria");
      const prodRes = await API.get("/producto");

      const productos = prodRes.data;

      const categoriasConProductos = catRes.data.map((cat: any) => ({
        ...cat,
        productos: productos.filter((p: any) => p.categoriaId === cat.id),
      }));

      setCategorias(categoriasConProductos);
    } catch (error) {
      console.error("Error al cargar categorías o productos:", error);
    }
  };

  useEffect(() => {
    fetchCategoriasYProductos();
  }, []);

  return (
    <div className="grid gap-4 md:grid-cols-2 md:items-center">
      <div className="text-center md:text-left">
        <h1 className="text-4xl font-bold text-gray-800">Administrar Menú</h1>
        <p className="text-gray-500">
          Gestioná tus categorías y productos de forma rápida
        </p>
      </div>

      <div className="flex justify-center md:justify-end gap-4 flex-wrap">
        <Button onClick={() => setModalCategoriaOpen(true)}>
          <PlusCircle className="mr-2" size={18} />
          Nueva Categoría
        </Button>
        <Button
          variant="outline"
          onClick={() => {
            setProductoSeleccionado(null);
            setModalProductoOpen(true);
          }}
        >
          <PlusCircle className="mr-2" size={18} />
          Nuevo Producto
        </Button>
        <Button variant="outline" onClick={() => setModalModificadoresOpen(true)}>
          <PlusCircle className="mr-2" size={18} />
          Administrar Modificadores
        </Button>
      </div>

      <div className="space-y-6 md:col-span-2">
        {categorias.map((cat) => (
          <CategoriaCard
            key={cat.id}
            categoria={cat}
            onProductoClick={(producto: Producto) => {
              setProductoSeleccionado(producto);
              setModalProductoOpen(true);
            }}
          />
        ))}
      </div>

      <ModalCategoria
        open={modalCategoriaOpen}
        onClose={() => setModalCategoriaOpen(false)}
        onCategoriaCreada={fetchCategoriasYProductos}
      />
      <ModalProducto
        open={modalProductoOpen}
        onClose={() => {
          setModalProductoOpen(false);
          setProductoSeleccionado(null);
        }}
        categorias={categorias}
        productoInicial={productoSeleccionado}
        onProductoCreado={fetchCategoriasYProductos}
      />
      <ModalModificadores
        open={modalModificadoresOpen}
        onClose={() => setModalModificadoresOpen(false)}
      />
    </div>
  );
}
