type Props = {
    onEdit?: () => void
    onDelete?: () => void
  }
  
  export default function RowActions({ onEdit, onDelete }: Props) {
  
    return (
  
      <div style={{ display: "flex", gap: "8px" }}>
  
        {onEdit && (
          <button
            style={{
              padding: "4px 8px",
              background: "#2563eb",
              color: "white",
              border: "none",
              borderRadius: "4px"
            }}
            onClick={onEdit}
          >
            Edit
          </button>
        )}
  
        {onDelete && (
          <button
            style={{
              padding: "4px 8px",
              background: "#dc2626",
              color: "white",
              border: "none",
              borderRadius: "4px"
            }}
            onClick={onDelete}
          >
            Delete
          </button>
        )}
  
      </div>
  
    )
  }