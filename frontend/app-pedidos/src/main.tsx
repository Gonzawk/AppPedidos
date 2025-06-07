import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import "./index.css";
import { AuthProvider } from "./auth/AuthProvider";
import { GoogleOAuthProvider } from "@react-oauth/google";
import { ModalProvider } from "@/context/ModalProvider";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <GoogleOAuthProvider clientId="526321036516-queqsdo19m4pc5dkdp7rfu9i371ndhfj.apps.googleusercontent.com">
      <AuthProvider>
        <ModalProvider>
          <App />
        </ModalProvider>
      </AuthProvider>
    </GoogleOAuthProvider>
  </React.StrictMode>
);
