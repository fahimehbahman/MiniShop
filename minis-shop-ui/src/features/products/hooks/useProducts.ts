import { useQuery } from "@tanstack/react-query";
import { getProducts } from "../api/productApi";

type Product = {
  productId: string;
  name: string;
  price: number;
  stock: number;
};

export const useProducts = (search: string) => {

 //react query has 3 retry in default
  const query = useQuery<Product[]>({
    queryKey: ["products" , search],
    queryFn: ( { signal } ) => getProducts(search, signal ),
    enabled: search !== undefined,
    staleTime: 1000 * 60 * 5
  });

  return {
    products: query.data ?? [], // if query.data is null or undefined return []
    loading: query.isLoading,
    error: query.error
  };

};