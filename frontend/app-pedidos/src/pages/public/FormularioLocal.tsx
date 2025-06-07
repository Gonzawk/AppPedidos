import { useState } from "react";
import { useNavigate } from "react-router-dom";
import API from "@/api/axiosInstance";
import { useModal } from "@/context/ModalProvider";

export default function FormularioLocal() {
  const { showModal } = useModal();
  const navigate = useNavigate();

  const [form, setForm] = useState({
    email: "",
    password: "",
    nombre: "",
    telefono: "",
    direccion: "",
    coordenadas: "",
    logoUrl: "",
  });

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async () => {
    setLoading(true);
    setError("");
    try {
      const res = await API.post("/auth/registro-local", form);
      showModal("confirm", res.data || "Solicitud enviada correctamente.");
      setForm({
        email: "",
        password: "",
        nombre: "",
        telefono: "",
        direccion: "",
        coordenadas: "",
        logoUrl: "",
      });
    } catch (err: any) {
      console.error("API error:", err.response?.data);
      const msg =
        err.response?.data?.title ||
        err.response?.data?.message ||
        "Error al enviar la solicitud.";
      setError(msg);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-white px-4">
      <div className="bg-white p-8 rounded-2xl shadow-xl w-full max-w-xl border border-gray-200 space-y-6">
        <h2 className="text-2xl font-bold text-center text-gray-800">Registro de Local</h2>
        <p className="text-center text-gray-500 text-sm">
          Completá este formulario para solicitar la activación de tu negocio.
        </p>

        {error && (
          <div className="bg-red-100 text-red-700 p-2 rounded text-sm">{error}</div>
        )}

        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          <input
            type="email"
            name="email"
            placeholder="Correo electrónico"
            value={form.email}
            onChange={handleChange}
            className="px-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <input
            type="password"
            name="password"
            placeholder="Contraseña"
            value={form.password}
            onChange={handleChange}
            className="px-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <input
            name="nombre"
            placeholder="Nombre del local"
            value={form.nombre}
            onChange={handleChange}
            className="px-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <input
            name="telefono"
            placeholder="Teléfono"
            value={form.telefono}
            onChange={handleChange}
            className="px-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <input
            name="direccion"
            placeholder="Dirección"
            value={form.direccion}
            onChange={handleChange}
            className="px-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <input
            name="coordenadas"
            placeholder="Coordenadas (lat,lng)"
            value={form.coordenadas}
            onChange={handleChange}
            className="px-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <input
            name="logoUrl"
            placeholder="URL del logo"
            value={form.logoUrl}
            onChange={handleChange}
            className="px-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-blue-500 col-span-full"
          />
        </div>

        <button
          onClick={handleSubmit}
          disabled={loading}
          className="w-full bg-blue-600 text-white py-2 rounded-xl hover:bg-blue-700 transition duration-200"
        >
          {loading ? "Enviando..." : "Enviar solicitud"}
        </button>

        <button
          onClick={() => navigate("/login")}
          className="w-full border border-blue-600 text-blue-600 py-2 rounded-xl hover:bg-blue-50 transition duration-200"
        >
          Ingresar
        </button>
      </div>
    </div>
  );
}
