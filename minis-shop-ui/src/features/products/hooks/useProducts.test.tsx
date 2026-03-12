import React from "react";
import { renderHook, waitFor } from "@testing-library/react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { useProducts } from "./useProducts";
import { getProducts } from "../api/productApi";
import { jest } from "@jest/globals";

jest.mock("../api/productApi"); // kole module va hameye method hash mock shode 

const mockedGetProducts = getProducts as jest.MockedFunction<typeof getProducts>; // methode moshakhas ro mock mikone ta befahmim che dadeii barmigardone

// dakhele describe mitoni chandin block test dashte bashi 
describe("useProducts", () => {

  const queryClient = new QueryClient({
    defaultOptions: {
      queries: {
        retry: false
      }
    }
  });

  const wrapper = ({ children }: { children: React.ReactNode }) => (
    <QueryClientProvider client={queryClient}>
      {children}
    </QueryClientProvider>
  );

  test("returns products when api succeeds", async () => {

    mockedGetProducts.mockResolvedValue([
      { productId: "1", name: "Laptop", price: 1000, stock: 5 }
    ]);

    //alan wrapper ro be onvane wrapper midom be use product 
    const { result } = renderHook(() => useProducts(), { wrapper });

    await waitFor(() => {
      expect(result.current.products.length).toBe(1);
    });

    expect(result.current.products[0].name).toBe("Laptop");

  });

});