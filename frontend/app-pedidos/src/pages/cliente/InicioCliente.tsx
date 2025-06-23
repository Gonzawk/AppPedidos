import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import axios from "../../api/axiosInstance";

type Local = {
  localId: number;
  nombre: string;
  logoUrl: string;
  slug: string; // <-- Agregamos el slug
};

type Categoria = {
  nombre: string;
  imagen: string;
  ruta: string;
};

export default function InicioCliente() {
  const navigate = useNavigate();

  const categorias: Categoria[] = [
    { nombre: "Hamburguesas", imagen: "/assets/categorias/hamburguesas.png", ruta: "/categoria/hamburguesas" },
    { nombre: "Pizzas", imagen: "/assets/categorias/pizzas.png", ruta: "/categoria/pizzas" },
    { nombre: "Sushi", imagen: "/assets/categorias/sushi.png", ruta: "/categoria/sushi" },
    { nombre: "Ensaladas", imagen: "/assets/categorias/ensaladas.png", ruta: "/categoria/ensaladas" },
    { nombre: "Postres", imagen: "/assets/categorias/postres.png", ruta: "/categoria/postres" },
  ];

  const [locales, setLocales] = useState<Local[]>([]);

  useEffect(() => {
    const fetchLocales = async () => {
      try {
        const response = await axios.get("/public/catalogo");
        setLocales(response.data);
      } catch (error) {
        console.error("Error al obtener el catálogo:", error);
      }
    };

    fetchLocales();
  }, []);

  return (
    <div className="p-4 bg-gray-50 min-h-screen">
      {/* Barra de búsqueda */}
      <div className="mb-4">
        <input
          type="text"
          placeholder="Buscar locales o productos"
          className="w-full px-4 py-2 border border-gray-300 rounded-full focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      {/* Categorías */}
      <div className="mb-6">
        <h2 className="text-lg font-semibold mb-2">Categorías</h2>
        <div className="flex space-x-4 overflow-x-auto">
          {categorias.map((cat) => (
            <div
              key={cat.nombre}
              onClick={() => navigate(cat.ruta)}
              className="flex-shrink-0 w-24 h-24 bg-white rounded-lg shadow flex flex-col items-center justify-center cursor-pointer hover:shadow-lg transition"
            >
              <img
                src={cat.imagen}
                alt={cat.nombre}
                className="w-10 h-10 mb-1 object-contain"
              />
              <span className="text-xs text-center">{cat.nombre}</span>
            </div>
          ))}
        </div>
      </div>

      {/* Locales */}
      <div>
        <h2 className="text-lg font-semibold mb-2">Locales disponibles</h2>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
          {locales.map((local) => (
            <div
              key={local.localId}
              className="bg-white rounded-lg shadow-md overflow-hidden cursor-pointer"
              onClick={() => navigate(`/local/${local.slug}`)} // <-- navegación dinámica por slug
            >
              <img
                src={local.logoUrl || "/default-local.jpg"}
                alt={local.nombre}
                className="w-full h-32 object-cover"
              />
              <div className="p-4">
                <h3 className="text-md font-semibold">{local.nombre}</h3>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
