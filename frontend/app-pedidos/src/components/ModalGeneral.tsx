import { useModal } from "@/context/ModalProvider";

export default function ModalGeneral() {
  const { modal, closeModal } = useModal();

  if (!modal.open) return null;

  const styles = modal.type === "error"
    ? "bg-red-100 border-red-400 text-red-700"
    : "bg-green-100 border-green-400 text-green-700";

  return (
    <div className="fixed inset-0 bg-black/30 flex items-center justify-center z-50">
      <div className={`p-4 rounded shadow border ${styles} max-w-md w-full mx-4`}>
        <div className="text-center space-y-2">
          <h2 className="text-lg font-bold">
            {modal.type === "error" ? "Error" : "Confirmación"}
          </h2>
          <p>{modal.message}</p>
          <button
            onClick={closeModal}
            className="mt-3 bg-gray-700 text-white px-4 py-1 rounded hover:bg-gray-800"
          >
            Cerrar
          </button>
        </div>
      </div>
    </div>
  );
}
