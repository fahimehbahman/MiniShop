import { useState } from "react";
import { useProducts } from "../hooks/useProducts";
import { useDebounce } from "../../../shared/hooks/useDebounce";
import Table from "../../../shared/component/table/Table";
import type { Column } from "../../../shared/component/table/Table";

export default function ProductListPage() {

  const [search, setSearch] = useState("");
  const debouncedSearch = useDebounce(search, 4000);

  console.log("debouncedSearch:", debouncedSearch);

  const { products, loading, error } = useProducts(debouncedSearch);

  if (loading)
    return <p>Loading products...</p>;

  if (error)
    return <p>{error.message}</p>;

    type Product = {
        productId: string
        name: string
        price: number
        stock: number
      }
    const columns: Column<Product>[] = [
        { header: "Name", accessor: "name" },
        { header: "Price", accessor: "price" },
        { header: "Stock", accessor: "stock" }
      ]
  return (
    <div style={{ padding: "40px" }}>

      <h1>Products</h1>

      <input
        placeholder="Search products..."
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        style={{
          padding: "8px",
          width: "300px",
          marginBottom: "20px"
        }}
      />

      {!products.length && <p>No products found</p>}

        <Table data={products} columns={columns}>          
        </Table>
    </div>
  );
}