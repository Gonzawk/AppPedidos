import { Home, Settings, Clock, ShoppingCart } from "lucide-react";
import { useEffect, useState } from "react";
import { useAuth } from "../auth/AuthProvider";
import axios from "../api/axiosInstance";
import LayoutBase from "./LayoutBase";

type LocalData = { nombre: string };

export default function LayoutLocal() {
  const { logout, token } = useAuth();
  const [local, setLocal] = useState<LocalData | null>(null);

  useEffect(() => {
    if (!token) return;
    axios.get("/local/mio", {
      headers: { Authorization: `Bearer ${token}` },
    })
    .then((res) => setLocal(res.data))
    .catch(() => setLocal(null));
  }, [token]);

  const navItems = [
    { to: "/local/dashboard", icon: <Home size={20} />, label: "Inicio" },
    { to: "/local/pedidos", icon: <ShoppingCart size={20} />, label: "Pedidos" },
    { to: "/local/negocio", icon: <Settings size={20} />, label: "Negocio" },
    { to: "/local/horarios", icon: <Clock size={20} />, label: "Horarios" },
  ];

  return <LayoutBase title={local?.nombre ?? "Local"} navItems={navItems} onLogout={logout} />;
}
