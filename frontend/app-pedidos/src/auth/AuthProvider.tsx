import { createContext, useContext, useEffect, useState } from "react";

interface AuthContextType {
  token: string | null;
  rol: string | null;
  login: (token: string, rol: string) => void;
  logout: () => void;
  isLoading: boolean;
}

const AuthContext = createContext<AuthContextType>(null!);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [token, setToken] = useState<string | null>(null);
  const [rol, setRol] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const t = localStorage.getItem("token");
    const r = localStorage.getItem("rol");
    if (t && r) {
      setToken(t);
      setRol(r);
    }
    setIsLoading(false);
  }, []);

  const login = (t: string, r: string) => {
    localStorage.setItem("token", t);
    localStorage.setItem("rol", r);
    setToken(t);
    setRol(r);
  };

  const logout = () => {
    localStorage.clear();
    setToken(null);
    setRol(null);
  };

  return (
    <AuthContext.Provider value={{ token, rol, login, logout, isLoading }}>
      {children}
    </AuthContext.Provider>
  );
}

export const useAuth = () => useContext(AuthContext);
