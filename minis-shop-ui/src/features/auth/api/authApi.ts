import apiClient from "../../../app/apiClient";

export const login = async (userName: string) => {

    const res = await apiClient.post("/auth/login", {
      userName
    });
    return res.data.data;
  };
  
  export const logout = async () => {
    await apiClient.post("/auth/logout");
  };
  
  export const me = async () => {
    const res = await apiClient.get("/auth/me");
    return res.data;
  };