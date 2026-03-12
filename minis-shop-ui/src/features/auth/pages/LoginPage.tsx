import { useState } from "react";
import { useAuth } from "../context/useAuth";
import { useNavigate } from "react-router-dom";
import styles from "./LoginPage.module.scss";
import PageLayout from "../../../app/PageLayout";


export default function LoginPage() {

    const [userName, setUserName] = useState("");
    const [pageError, setPageError] = useState("");
    const { login } = useAuth();
    const navigate = useNavigate();

  const handleLogin = async () => {
    try {
            await login(userName);
            alert("Login successful");
            navigate("/products")
          } catch {
            setPageError("Login failed");
          }
  };

  return  (

    <PageLayout>

        <h2 className={styles.title}>Login</h2>

        <div className={styles.form}>

          <input
            className={styles.input}
            placeholder="Username"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
          />

          {pageError && (
            <p className={styles.error}>{pageError}</p>
          )}

          <button
            className={styles.button}
            onClick={handleLogin}
          >
            Login
          </button>

        </div>
    </PageLayout>

  );
}