using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Table("Receipt")]
[Microsoft.EntityFrameworkCore.Index("ReceiptCode", Name = "UQ__Receipt__1AB76D009B0A8EE3", IsUnique = true)]
public partial class Receipt
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string ReceiptCode { get; set; } = null!;

    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [Column("SupplierID")]
    public int SupplierId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Receipts")]
    public virtual Employee Employee { get; set; } = null!;

    [InverseProperty("Receipt")]
    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    [ForeignKey("SupplierId")]
    [InverseProperty("Receipts")]
    public virtual Supplier Supplier { get; set; } = null!;
}
