import { Link, useLocation } from "react-router-dom";
import { useAuth } from "../auth/AuthProvider";

const links = [
  { to: "/local/dashboard", label: "Dashboard" },
  { to: "/local/negocio", label: "Mi Negocio" },
  { to: "/local/horarios", label: "Horarios" },
];

export default function Sidebar() {
  const location = useLocation();
  const { logout } = useAuth();

  return (
    <aside className="w-64 bg-white shadow-md p-4 flex flex-col justify-between h-full">
      <div className="space-y-2">
        <h2 className="text-xl font-bold mb-4">Panel Local</h2>
        {links.map((link) => (
          <Link
            key={link.to}
            to={link.to}
            className={`block px-2 py-1 rounded ${
              location.pathname === link.to
                ? "bg-blue-100 text-blue-700"
                : "hover:bg-gray-100"
            }`}
          >
            {link.label}
          </Link>
        ))}
      </div>
      <button
        onClick={logout}
        className="bg-red-500 text-white px-4 py-2 rounded mt-8"
      >
        Cerrar sesión
      </button>
    </aside>
  );
}
