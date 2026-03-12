import apiClient from "../../../app/apiClient";

export const  getProducts = async(
    search : string,
    signal?: AbortSignal
    )=> {


   const res = await apiClient.get("/products/GetProduct",{
    params: { search , signal }
  });
   console.log("FETCH PRODUCTS");
   return res.data;

}
