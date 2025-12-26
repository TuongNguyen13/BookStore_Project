using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[PrimaryKey("OrderCode", "ProductCode")]
public partial class OrderDetail
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string OrderCode { get; set; } = null!;

    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string ProductCode { get; set; } = null!;

    public int Quantity { get; set; }

    public double UnitPrice { get; set; }

    [ForeignKey("OrderCode")]
    [InverseProperty("OrderDetails")]
    public virtual Order OrderCodeNavigation { get; set; } = null!;

    [ForeignKey("ProductCode")]
    [InverseProperty("OrderDetails")]
    public virtual Product ProductCodeNavigation { get; set; } = null!;
}
