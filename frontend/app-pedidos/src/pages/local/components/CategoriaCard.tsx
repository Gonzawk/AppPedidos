import ProductoCard from "./ProductoCard";
import type { Producto } from "../MenuPage";

interface Props {
  categoria: {
    id: number;
    nombre: string;
    productos: Producto[];
  };
  onProductoClick?: (producto: Producto) => void;
}

export default function CategoriaCard({ categoria, onProductoClick }: Props) {
  return (
    <div>
      <h2 className="text-xl font-semibold mt-6 mb-2">{categoria.nombre}</h2>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
        {categoria.productos.map((prod) => (
          <div
            key={prod.id}
            onClick={() => onProductoClick?.(prod)}
            className="cursor-pointer"
          >
            <ProductoCard producto={prod} />
          </div>
        ))}
      </div>
    </div>
  );
}
