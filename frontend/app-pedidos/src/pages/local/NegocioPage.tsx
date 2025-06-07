import { useEffect, useState } from "react";
import { getMiLocal, actualizarLocal } from "@/api/localService";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { useModal } from "@/context/ModalProvider";
import { useNavigate } from "react-router-dom";

type Seccion = "inicio" | "info" | "menu" | "finanzas" | "deliverys";

export default function NegociosPage() {
  const [form, setForm] = useState({
    nombre: "",
    telefono: "",
    direccion: "",
    coordenadas: "",
    logoUrl: ""
  });

  const [seccion, setSeccion] = useState<Seccion>("inicio");
  const { showModal } = useModal();
  const navigate = useNavigate();

  useEffect(() => {
    getMiLocal().then(data => {
      if (data) {
        setForm({
          nombre: data.nombre,
          telefono: data.telefono,
          direccion: data.direccion,
          coordenadas: data.coordenadas,
          logoUrl: data.logoUrl
        });
      }
    });
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async () => {
    try {
      await actualizarLocal(form);
      showModal("confirm", "Datos del local actualizados.");
    } catch {
      showModal("error", "No se pudo actualizar el local.");
    }
  };

  return (
    <div className="p-4 max-w-4xl mx-auto space-y-6">
      <h1 className="text-3xl font-bold text-center">Gestión del Negocio</h1>

      {seccion === "inicio" && (
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          <button
            onClick={() => setSeccion("info")}
            className="bg-blue-600 text-white p-4 rounded-xl shadow hover:bg-blue-700 transition font-medium"
          >
            🏪 Información del Local
          </button>
          <button
            onClick={() => navigate("/local/menu")}
            className="bg-green-600 text-white p-4 rounded-xl shadow hover:bg-green-700 transition font-medium"
          >
            🍽 Menú / Productos
          </button>
          <button
  onClick={() => navigate("/local/proveedores")}
  className="bg-teal-600 text-white p-4 rounded-xl shadow hover:bg-teal-700 transition font-medium"
>
  🧾 Proveedores / Compras
</button>

          <button
           onClick={() => navigate("/local/insumos")}
             className="bg-orange-600 text-white p-4 rounded-xl shadow hover:bg-orange-700 transition font-medium"
            >
            🏭 Producción / Insumos
           </button>
           
          <button
            onClick={() => navigate("/local/finanzas")}
            className="bg-yellow-600 text-white p-4 rounded-xl shadow hover:bg-yellow-700 transition font-medium"
          >
            💰 Caja / Finanzas
          </button>
          <button
            onClick={() => navigate("/local/deliverys")}
            className="bg-purple-600 text-white p-4 rounded-xl shadow hover:bg-purple-700 transition font-medium"
          >
            🚴‍♂️ Repartidores
          </button>
        </div>
      )}

      {seccion === "info" && (
        <div className="space-y-4 bg-white p-6 rounded-xl shadow">
          <div className="flex justify-between items-center">
            <h2 className="text-xl font-bold">Información del Local</h2>
           <Button className="border border-gray-300 text-gray-700 bg-white hover:bg-gray-100" onClick={() => setSeccion("inicio")}>

              ⬅ Volver
            </Button>
          </div>

          {form.logoUrl && (
            <div className="flex justify-center">
              <img
                src={form.logoUrl}
                alt="Logo del negocio"
                className="w-32 h-32 rounded-full shadow-md object-cover border"
              />
            </div>
          )}

          <div>
            <label className="block mb-1 font-medium">Nombre del Local</label>
            <Input
              name="nombre"
              value={form.nombre}
              onChange={handleChange}
              placeholder="Ej: Panadería San Juan"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium">Teléfono</label>
            <Input
              name="telefono"
              value={form.telefono}
              onChange={handleChange}
              placeholder="Ej: 341-5551234"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium">Dirección</label>
            <Input
              name="direccion"
              value={form.direccion}
              onChange={handleChange}
              placeholder="Ej: Av. Pellegrini 1234"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium">Coordenadas</label>
            <Input
              name="coordenadas"
              value={form.coordenadas}
              onChange={handleChange}
              placeholder="Ej: -32.9442,-60.6505"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium">URL del Logo</label>
            <Input
              name="logoUrl"
              value={form.logoUrl}
              onChange={handleChange}
              placeholder="https://misitio.com/logo.png"
            />
          </div>

          <div className="pt-4 text-center">
            <Button onClick={handleSubmit}>Guardar Cambios</Button>
          </div>
        </div>
      )}
    </div>
  );
}
