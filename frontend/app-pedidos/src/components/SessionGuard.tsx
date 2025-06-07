import React from "react";
import { useAuth } from "../auth/AuthProvider";
import { Navigate } from "react-router-dom";

interface Props {
  children: React.ReactNode;
}

export default function SessionGuard({ children }: Props) {
  const { token, isLoading } = useAuth();

  if (isLoading) return <p>Cargando...</p>;
  if (token) return <Navigate to="/" />;

  return <>{children}</>;
}
