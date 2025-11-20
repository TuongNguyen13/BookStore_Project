using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Table("Supplier")]
[Microsoft.EntityFrameworkCore.Index("SupplierCode", Name = "UQ__Supplier__44BE981B14526FF8", IsUnique = true)]
public partial class Supplier
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

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

    [InverseProperty("Supplier")]
    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
