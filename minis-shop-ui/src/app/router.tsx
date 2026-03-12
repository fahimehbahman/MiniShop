import { BrowserRouter, Routes, Route } from "react-router-dom";
import { lazy, Suspense } from "react";
const LoginPage = lazy(() => import("../features/auth/pages/LoginPage"));
const ProductListPage = lazy(() => import("../features/products/pages/ProductListPage"));

export default function Router() {

  return (
    

    <BrowserRouter>
      <Suspense fallback={<div>Loading...</div>}>
      <Routes>

        <Route path="/" element={<LoginPage />} />

        <Route path="/products" element={<ProductListPage />} />

      </Routes>
      </Suspense>
    </BrowserRouter>

  );

}