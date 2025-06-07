import axios from "@/api/axiosInstance";

export const getMiLocal = async () => {
  const res = await axios.get("/local/mio");
  return res.data;
};

export const actualizarLocal = async (data: {
  nombre: string;
  telefono: string;
  direccion: string;
  coordenadas: string;
  logoUrl: string;
}) => {
  const res = await axios.put("/local", data);
  return res.data;
};
