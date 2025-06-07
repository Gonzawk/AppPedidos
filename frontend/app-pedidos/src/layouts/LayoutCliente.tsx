import { Home, ShoppingBag, User } from "lucide-react";
import { useAuth } from "../auth/AuthProvider";
import LayoutBase from "./LayoutBase";

export default function LayoutCliente() {
  const { logout } = useAuth();

  const navItems = [
    { to: "/inicio", icon: <Home size={20} />, label: "Inicio" },
    { to: "/pedidos", icon: <ShoppingBag size={20} />, label: "Pedidos" },
    { to: "/perfil", icon: <User size={20} />, label: "Perfil" },
  ];

  return <LayoutBase title="Cliente" navItems={navItems} onLogout={logout} />;
}
