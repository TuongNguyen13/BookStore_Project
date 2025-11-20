using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Table("Account")]
[Microsoft.EntityFrameworkCore.Index("EmployeeId", Name = "UQ_Account_EmployeeID", IsUnique = true)]
[Microsoft.EntityFrameworkCore.Index("Username", Name = "UQ__Account__536C85E4CE7A7C6E", IsUnique = true)]
public partial class Account
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Pass { get; set; } = null!;

    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Account")]
    public virtual Employee Employee { get; set; } = null!;
}
