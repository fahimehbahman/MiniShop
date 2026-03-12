import type {ReactNode}  from "react";

type Props = {
  children: ReactNode;
};

export default function PageLayout({ children }: Props) {

  return (

    <div className="page-center">

      <div className="page-card">

        {children}

      </div>

    </div>

  );

}