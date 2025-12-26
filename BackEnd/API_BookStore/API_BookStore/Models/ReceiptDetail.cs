using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[PrimaryKey("ReceiptCode", "ProductCode")]
[Table("ReceiptDetail")]
public partial class ReceiptDetail
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string ReceiptCode { get; set; } = null!;

    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string ProductCode { get; set; } = null!;

    public int Quantity { get; set; }

    public double UnitPrice { get; set; }

    [ForeignKey("ProductCode")]
    [InverseProperty("ReceiptDetails")]
    public virtual Product ProductCodeNavigation { get; set; } = null!;

    [ForeignKey("ReceiptCode")]
    [InverseProperty("ReceiptDetails")]
    public virtual Receipt ReceiptCodeNavigation { get; set; } = null!;
}
