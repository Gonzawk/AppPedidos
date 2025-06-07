import { useEffect, useState } from "react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { useModal } from "@/context/ModalProvider";
import API from "@/api/axiosInstance";

type ClienteForm = {
  nombre: string;
  apellido: string;
  telefono: string;
  email: string;
};

export default function PerfilCliente() {
  const [modoEdicion, setModoEdicion] = useState(false);
  const [form, setForm] = useState<ClienteForm>({
    nombre: "",
    apellido: "",
    telefono: "",
    email: "",
  });

  const { showModal } = useModal();

  useEffect(() => {
    API.get("/cliente/miperfil")
      .then((res) => setForm(res.data))
      .catch(() => showModal("error", "No se pudo obtener la información del perfil."));
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async () => {
    try {
      await API.put("/cliente/miperfil", form);
      showModal("confirm", "Perfil actualizado correctamente.");
      setModoEdicion(false);
    } catch {
      showModal("error", "Error al actualizar perfil.");
    }
  };

  return (
    <div className="p-4 max-w-2xl mx-auto space-y-6">
      <h1 className="text-3xl font-bold text-center">Tu Perfil</h1>

      <div className="space-y-4 bg-white p-6 rounded-xl shadow">
        <div className="flex justify-between items-center">
          <h2 className="text-xl font-semibold">Información Personal</h2>
          <Button
  className="border border-gray-300 text-gray-700 bg-white hover:bg-gray-100"
  onClick={() => setModoEdicion((prev) => !prev)}
>
  {modoEdicion ? "Cancelar" : "Editar"}
</Button>

        </div>

        <div>
          <label className="block mb-1 font-medium">Nombre</label>
          <Input
            name="nombre"
            value={form.nombre}
            onChange={handleChange}
            disabled={!modoEdicion}
            placeholder="Ej: Juan"
          />
        </div>

        <div>
          <label className="block mb-1 font-medium">Apellido</label>
          <Input
            name="apellido"
            value={form.apellido}
            onChange={handleChange}
            disabled={!modoEdicion}
            placeholder="Ej: Pérez"
          />
        </div>

        <div>
          <label className="block mb-1 font-medium">Teléfono</label>
          <Input
            name="telefono"
            value={form.telefono}
            onChange={handleChange}
            disabled={!modoEdicion}
            placeholder="Ej: 341-5551234"
          />
        </div>

        <div>
          <label className="block mb-1 font-medium">Email</label>
          <Input
            name="email"
            value={form.email}
            onChange={handleChange}
            disabled
          />
        </div>

        {modoEdicion && (
          <div className="pt-4 text-center">
            <Button onClick={handleSubmit}>Guardar Cambios</Button>
          </div>
        )}
      </div>
    </div>
  );
}
