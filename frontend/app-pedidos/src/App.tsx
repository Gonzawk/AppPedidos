import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginPage from "./auth/LoginPage";
import Home from "./pages/Home";
import Dashboard from "./pages/Dashboard";
import ProtectedRoute from "./components/ProtectedRoute";
import SessionGuard from "./components/SessionGuard";

import LayoutLocal from "./layouts/LayoutLocal";
import NegocioPage from "./pages/local/NegocioPage";
import HorariosPage from "./pages/local/HorariosPage";
import PedidosPage from "./pages/local/PedidosPage"
import MenuPage from "./pages/local/MenuPage";


import LayoutCliente from "./layouts/LayoutCliente";
import InicioCliente from "./pages/cliente/InicioCliente";
import PedidosCliente from "./pages/cliente/PedidosCliente";
import PerfilCliente from "./pages/cliente/PerfilCliente";

import FormularioLocal from "../src/pages/public/FormularioLocal";


import ModalGeneral from "@/components/ModalGeneral";
import InsumosPage from "./pages/local/InsumosPage";
import ProduccionPage from "./pages/local/ProduccionPage";
import ProveedoresPage from "./pages/local/ProveedoresPage";
import ComprasPage from "./pages/local/ComprasPage";
import LocalDetalle from "./pages/cliente/LocalDetalle";
import CajaPage from "./pages/local/CajaPage";

export default function App() {
  return (
    <BrowserRouter>
      <ModalGeneral />
     <Routes>
  {/* Login público */}
  <Route
    path="/login"
    element={
      <SessionGuard>
        <LoginPage />
      </SessionGuard>
    }
  />

  {/* Registro Local Público */}
  <Route path="/registro-local" element={<FormularioLocal />} />

        {/* Redirección según rol */}
        <Route
          path="/"
          element={
            <ProtectedRoute>
              <Home />
            </ProtectedRoute>
          }
        />

        <Route
  element={
    <ProtectedRoute roles={["local"]}>
      <LayoutLocal />
    </ProtectedRoute>
  }
>
  <Route path="/local/dashboard" element={<Dashboard />} />
  <Route path="/local/negocio" element={<NegocioPage />} />
  <Route path="/local/horarios" element={<HorariosPage />} />
  <Route path="/local/pedidos" element={<PedidosPage />} />
  <Route path="/local/menu" element={<MenuPage />} /> {/* <-- Nueva ruta */}
  <Route path="/local/insumos" element={<InsumosPage/>} />
  <Route path="/local/produccion" element={<ProduccionPage/>} />
  <Route path="/local/proveedores" element={<ProveedoresPage/>} />
  <Route path="/local/compras" element={<ComprasPage/>} />
  <Route path="/local/caja" element={<CajaPage/>} />
</Route>


        {/* Rutas Cliente con su layout */}
        <Route
          element={
            <ProtectedRoute roles={["cliente"]}>
              <LayoutCliente />
            </ProtectedRoute>
          }
        >
          <Route path="/inicio" element={<InicioCliente />} />
          <Route path="/pedidos" element={<PedidosCliente />} />
          <Route path="/perfil" element={<PerfilCliente />} />
          <Route path="/local/:slug" element={<LocalDetalle />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
