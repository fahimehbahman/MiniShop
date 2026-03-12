import React  from "react"

export type Column<T> = {
    header: string  //un chizi ke dar ui mibini
    accessor: keyof T // name vagheii column dar object
  }

type Props<T> = {
 data:T[]
 columns: Column<T>[]
 renderActions?: (row: T) => React.ReactNode
}

export default function Table<T>({
data,
columns,
renderActions
}: Props<T>) {


return(
<table>
<thead>
    <tr>
        {columns.map(col=> <th>
            {col.header}
        </th>)}
       {renderActions && <th>Actions</th>} 
    </tr>
</thead>
<tbody>
    {data.map( (row,index)=> (
      <tr key={index}>
        {columns.map(col => (<td>{String(row[col.accessor])}</td>))}
         {renderActions && <td>renderActions(row)</td>} 

      </tr>

    ) )

    }
</tbody>
</table>
)}