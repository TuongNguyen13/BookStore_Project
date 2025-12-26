using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Table("Supplier")]
public partial class Supplier
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string SupplierCode { get; set; } = null!;

    [StringLength(255)]
    public string SupplierName { get; set; } = null!;

    [StringLength(255)]
    public string? SupplierAddress { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? SupplierPhone { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? SupplierEmail { get; set; }

    [InverseProperty("SupplierCodeNavigation")]
    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
