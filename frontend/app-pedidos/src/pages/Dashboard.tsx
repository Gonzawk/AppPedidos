import { useEffect, useState } from "react";
import axios from "../api/axiosInstance";
import { useAuth } from "../auth/AuthProvider";
import { Loader2 } from "lucide-react";
import { useNavigate } from "react-router-dom";

type Local = {
  nombre: string;
  direccion: string;
  telefono: string;
  logoUrl?: string;
};

export default function Dashboard() {
  const { token } = useAuth();
  const navigate = useNavigate();

  const [local, setLocal] = useState<Local | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!token) return;

    setLoading(true);
    axios
      .get("/Local/mio", { headers: { Authorization: `Bearer ${token}` } })
      .then((res) => setLocal(res.data))
      .catch(() => setLocal(null))
      .finally(() => setLoading(false));
  }, [token]);

  if (loading || !local) {
    return (
      <div className="flex items-center justify-center h-64">
        <Loader2 className="animate-spin w-6 h-6 text-primary" />
      </div>
    );
  }

  return (
    <div className="p-4 max-w-6xl mx-auto space-y-6">
      <h1 className="text-3xl font-bold text-center text-primary">Panel del Local</h1>

      <div className="bg-white rounded-2xl shadow p-6 flex flex-col md:flex-row items-center gap-6">
        {local.logoUrl && (
          <img
            src={local.logoUrl}
            alt="Logo del local"
            className="w-24 h-24 rounded-full object-cover border shadow"
          />
        )}
        <div className="text-center md:text-left space-y-1">
          <h2 className="text-2xl font-bold text-gray-800">{local.nombre}</h2>
          <p className="text-gray-600">📍 {local.direccion}</p>
          <p className="text-gray-600">📞 {local.telefono}</p>
        </div>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <button
          onClick={() => navigate("/local/negocio")}
          className="bg-blue-600 text-white p-4 rounded-xl shadow hover:bg-blue-700 transition font-medium"
        >
          🛠 Configurar Negocio
        </button>
        <button
          onClick={() => navigate("/local/horarios")}
          className="bg-indigo-600 text-white p-4 rounded-xl shadow hover:bg-indigo-700 transition font-medium"
        >
          🕐 Gestionar Turnos
        </button>
      </div>
    </div>
  );
}
