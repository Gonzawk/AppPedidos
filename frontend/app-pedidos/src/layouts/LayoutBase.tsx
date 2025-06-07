import { Outlet, NavLink } from "react-router-dom";
import { LogOut } from "lucide-react";

type NavItem = {
  to: string;
  icon: React.ReactNode; // Alternativa más general y común que JSX.Element
  label: string;
};


type LayoutBaseProps = {
  title: string;
  navItems: NavItem[];
  onLogout: () => void;
};

export default function LayoutBase({ title, navItems, onLogout }: LayoutBaseProps) {
  return (
    <div className="flex flex-col min-h-screen bg-gray-50 md:flex-row">
      {/* Sidebar Desktop */}
      <aside className="w-64 bg-white border-r shadow-sm hidden md:flex flex-col">
        <div className="p-6 text-xl font-bold text-primary">{title}</div>
        <nav className="flex-1 px-4 space-y-2">
          {navItems.map(({ to, icon, label }) => (
            <NavLink
              key={to}
              to={to}
              className={({ isActive }) =>
                `flex items-center gap-3 px-4 py-2 rounded-lg hover:bg-gray-100 transition ${
                  isActive ? "bg-gray-200 text-primary font-semibold" : "text-gray-700"
                }`
              }
            >
              {icon}
              {label}
            </NavLink>
          ))}
        </nav>
        <button
          onClick={onLogout}
          className="m-4 flex items-center gap-2 px-4 py-2 rounded-md bg-red-100 text-red-600 hover:bg-red-200"
        >
          <LogOut size={18} />
          Cerrar sesión
        </button>
      </aside>

      {/* Main Content */}
      <div className="flex-1 flex flex-col pb-16 md:pb-0">
        <header className="h-16 bg-white border-b px-6 flex items-center justify-between shadow-sm">
          <div className="text-lg font-semibold text-gray-800">{title}</div>
          <div className="w-8 h-8 rounded-full bg-gray-300" />
        </header>
        <main className="flex-1 p-4 overflow-y-auto">
          <Outlet />
        </main>
      </div>

      {/* Bottom Navbar Mobile */}
      <nav className="fixed bottom-0 inset-x-0 bg-white border-t flex justify-around items-center h-16 md:hidden shadow-md">
        {navItems.map(({ to, icon, label }) => (
          <NavLink
            key={to}
            to={to}
            className={({ isActive }) =>
              `flex flex-col items-center text-xs ${
                isActive ? "text-primary font-semibold" : "text-gray-500"
              }`
            }
          >
            {icon}
            {label}
          </NavLink>
        ))}
        <button
          onClick={onLogout}
          className="flex flex-col items-center text-xs text-red-500"
        >
          <LogOut size={20} />
          Salir
        </button>
      </nav>
    </div>
  );
}
