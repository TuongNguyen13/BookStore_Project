using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Table("Employee")]
[Microsoft.EntityFrameworkCore.Index("EmployeeCode", Name = "UQ__Employee__1F64254839594761", IsUnique = true)]
[Microsoft.EntityFrameworkCore.Index("Email", Name = "UQ__Employee__A9D10534E19D7F82", IsUnique = true)]
public partial class Employee
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string EmployeeCode { get; set; } = null!;

    [StringLength(255)]
    public string EmployeeName { get; set; } = null!;

    [StringLength(20)]
    public string? Gender { get; set; }

    public DateTime BirthDay { get; set; }

    [StringLength(255)]
    public string? EmployeeAddress { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [InverseProperty("Employee")]
    public virtual Account? Account { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Employee")]
    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
