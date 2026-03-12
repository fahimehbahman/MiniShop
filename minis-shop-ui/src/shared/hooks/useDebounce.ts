import { useEffect, useState } from "react";

//meghdar search ro migire andazeye delay pishe khodesh negah midare bad mide biron
export function useDebounce<T>(value: T, delay: number) {

  const [debouncedValue, setDebouncedValue] = useState(value);

  useEffect(() => {

    const handler = setTimeout(() => {
      setDebouncedValue(value);
    }, delay);

    return () => clearTimeout(handler);

  }, [value, delay]);

  return debouncedValue;
}