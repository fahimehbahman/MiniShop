import React from "react";
import { render, screen } from "@testing-library/react";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { getProducts } from "../api/productApi";
import ProductListPage from "./ProductListPage";
import { jest } from "@jest/globals";
import "@testing-library/jest-dom";
import { MemoryRouter } from "react-router-dom";


jest.mock("../api/productApi");

const mockedGetProducts = getProducts as jest.MockedFunction<typeof getProducts>;

describe("ProductListPage Integration", () => {

  const queryClient = new QueryClient({
    defaultOptions: {
      queries: { retry: false }
    }
  });

  const wrapper = ({ children }: { children: React.ReactNode }) => (
    <MemoryRouter>
      <QueryClientProvider client={queryClient}>
        {children}
      </QueryClientProvider>
    </MemoryRouter>
  );

  test("renders products from API", async () => {

    mockedGetProducts.mockResolvedValue([
      { productId: "1", name: "Laptop", price: 1000, stock: 5 }
    ]);

    render(<ProductListPage />, { wrapper });

    const productName = await screen.findByText("Laptop");

    expect(productName).toBeInTheDocument();

  });

});
