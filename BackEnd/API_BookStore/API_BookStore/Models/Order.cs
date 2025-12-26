using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

public partial class Order
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string OrderCode { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string CustomerCode { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? EmployeeCode { get; set; }

    [StringLength(30)]
    public string? Payment { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? OrderStatus { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OrderTotal { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [ForeignKey("CustomerCode")]
    [InverseProperty("Orders")]
    public virtual Customer CustomerCodeNavigation { get; set; } = null!;

    [ForeignKey("EmployeeCode")]
    [InverseProperty("Orders")]
    public virtual Employee? EmployeeCodeNavigation { get; set; }

    [InverseProperty("OrderCodeNavigation")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
