import { useAuth } from "../auth/AuthProvider";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export default function Home() {
  const { rol, logout } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (rol === "local") navigate("/local/dashboard");
    else if (rol === "admin") navigate("/admin");
    else if (rol === "cliente") navigate("/inicio");
  }, [rol, navigate]);

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold">Cargando...</h1>
    </div>
  );
}
