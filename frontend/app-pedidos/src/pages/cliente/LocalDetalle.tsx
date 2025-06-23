import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "../../api/axiosInstance";

type Modificador = {
  modificadorId: number;
  nombre: string;
  precioExtra: number;
};

type Producto = {
  productoId: number;
  nombre: string;
  descripcion: string;
  precio: number;
  imagenUrl: string;
  modificadores: Modificador[];
};

type Local = {
  localId: number;
  nombre: string;
  direccion: string;
  telefono: string;
  logoUrl: string;
  productos: Producto[];
};

export default function LocalDetalle() {
  const { slug } = useParams();
  const [local, setLocal] = useState<Local | null>(null);

  useEffect(() => {
    const fetchLocal = async () => {
      try {
        const res = await axios.get(`/public/catalogo/${slug}`);
        setLocal(res.data);
      } catch (err) {
        console.error("Error al cargar el local:", err);
      }
    };

    fetchLocal();
  }, [slug]);

  if (!local) return <div className="p-4">Cargando...</div>;

  return (
    <div className="p-4">
      <div className="flex items-center mb-4">
        <img src={local.logoUrl || "/default-local.jpg"} alt={local.nombre} className="w-16 h-16 rounded-full mr-4" />
        <div>
          <h1 className="text-2xl font-bold">{local.nombre}</h1>
          <p className="text-sm text-gray-500">{local.direccion}</p>
          <p className="text-sm text-gray-500">{local.telefono}</p>
        </div>
      </div>

      <div>
        <h2 className="text-lg font-semibold mb-2">Productos disponibles</h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
          {local.productos.map((producto) => (
            <div key={producto.productoId} className="bg-white rounded-lg shadow-md p-4">
              <img src={producto.imagenUrl || "/default-product.jpg"} alt={producto.nombre} className="w-full h-32 object-cover rounded" />
              <h3 className="mt-2 text-md font-semibold">{producto.nombre}</h3>
              <p className="text-sm text-gray-500">{producto.descripcion}</p>
              <p className="text-green-600 font-bold">${producto.precio.toFixed(2)}</p>
              <button className="mt-2 bg-blue-500 text-white px-4 py-1 rounded hover:bg-blue-600">
                Agregar
              </button>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
