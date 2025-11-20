using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[PrimaryKey("ReceiptId", "ProductId")]
[Table("ReceiptDetail")]
public partial class ReceiptDetail
{
    [Key]
    [Column("ReceiptID")]
    public int ReceiptId { get; set; }

    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public double UnitPrice { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ReceiptDetails")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("ReceiptId")]
    [InverseProperty("ReceiptDetails")]
    public virtual Receipt Receipt { get; set; } = null!;
}
