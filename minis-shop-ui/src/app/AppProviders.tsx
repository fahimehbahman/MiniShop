import type { ReactNode }  from "react";
import { AuthProvider } from "../features/auth/context/AuthContext";
import { QueryClientProvider } from "@tanstack/react-query";
import { queryClient } from "./queryClient";

type Props = {
  children: ReactNode;
};

function AppProviders({ children }: Props) {
  return (
    <QueryClientProvider client={queryClient}>
    <AuthProvider>
      {children}
    </AuthProvider>
    </QueryClientProvider>
  );
}

export default AppProviders;