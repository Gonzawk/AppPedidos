import { createContext, useContext, useState } from "react";
import type { ReactNode } from "react";


type ModalType = "error" | "confirm";

interface ModalData {
  open: boolean;
  type: ModalType;
  message: string;
}

interface ModalContextType {
  modal: ModalData;
  showModal: (type: ModalType, message: string) => void;
  closeModal: () => void;
}

const ModalContext = createContext<ModalContextType | undefined>(undefined);

export const ModalProvider = ({ children }: { children: ReactNode }) => {
  const [modal, setModal] = useState<ModalData>({
    open: false,
    type: "confirm",
    message: "",
  });

  const showModal = (type: ModalType, message: string) => {
    setModal({ open: true, type, message });
  };

  const closeModal = () => setModal({ ...modal, open: false });

  return (
    <ModalContext.Provider value={{ modal, showModal, closeModal }}>
      {children}
    </ModalContext.Provider>
  );
};

export const useModal = () => {
  const context = useContext(ModalContext);
  if (!context) throw new Error("useModal must be used within a ModalProvider");
  return context;
};
