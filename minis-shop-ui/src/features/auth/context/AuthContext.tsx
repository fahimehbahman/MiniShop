import { createContext, useState, useEffect } from "react";
import * as authApi from "../api/authApi";

type AuthContextType = {
  user: any;
  isAuthenticated: boolean;
  login: (userName: string) => Promise<void>;
  logout: () => Promise<void>;
};

export const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: any) => {

  const [user, setUser] = useState<any>(null);

  const login = async (userName: string) => {
    const user = await authApi.login(userName);
    setUser(user);
  };

  const logout = async () => {
    await authApi.logout();
    setUser(null);
  };

  const loadUser = async () => {
    try {
      const user = await authApi.me();
      setUser(user);
    } catch {
      setUser(null);
    }
  };

  useEffect(() => {
    loadUser();
  }, []);

  return (
    <AuthContext.Provider
      value={{
        user,
        isAuthenticated: !!user,
        login,
        logout
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};