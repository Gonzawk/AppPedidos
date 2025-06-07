import React from "react";
import { Navigate } from "react-router-dom";
import { useAuth } from "../auth/AuthProvider";

export default function ProtectedRoute({
  children,
  roles = [],
}: {
  children: React.ReactNode;
  roles?: string[];
}) {
  const { token, rol } = useAuth();

  if (!token) return <Navigate to="/login" />;
  if (roles.length && !roles.includes(rol!)) return <Navigate to="/" />;

  return children;
}
