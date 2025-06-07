import { GoogleLogin } from "@react-oauth/google";
import { useAuth } from "./AuthProvider";
import API from "../api/axiosInstance";

export default function GoogleLoginButton() {
  const { login } = useAuth();

  const handleSuccess = async (credentialResponse: any) => {
    const idToken = credentialResponse.credential;
    const res = await API.post("/auth/google", { idToken });

    const token = res.data.token;
    const rol = JSON.parse(atob(token.split(".")[1])).role;
    login(token, rol);
  };

  return (
    <GoogleLogin
      onSuccess={handleSuccess}
      onError={() => console.log("Google Login fallido")}
    />
  );
}
