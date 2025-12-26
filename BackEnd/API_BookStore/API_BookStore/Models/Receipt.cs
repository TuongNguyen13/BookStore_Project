using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Table("Receipt")]
public partial class Receipt
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string ReceiptCode { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string EmployeeCode { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string SupplierCode { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ReceiptTotal { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [StringLength(255)]
    public string? ReceiptNote { get; set; }

    [ForeignKey("EmployeeCode")]
    [InverseProperty("Receipts")]
    public virtual Employee EmployeeCodeNavigation { get; set; } = null!;

    [InverseProperty("ReceiptCodeNavigation")]
    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    [ForeignKey("SupplierCode")]
    [InverseProperty("Receipts")]
    public virtual Supplier SupplierCodeNavigation { get; set; } = null!;
}
