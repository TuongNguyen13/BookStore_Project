using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Table("Customer")]
[Microsoft.EntityFrameworkCore.Index("PhoneNumber", Name = "UQ__Customer__85FB4E38320F84BF", IsUnique = true)]
public partial class Customer
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string CustomerCode { get; set; } = null!;

    [StringLength(255)]
    public string CustomerName { get; set; } = null!;

    [StringLength(20)]
    public string? Gender { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? CustomerAddress { get; set; }

    [InverseProperty("CustomerCodeNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
