using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Table("Account")]
[Microsoft.EntityFrameworkCore.Index("EmployeeCode", Name = "UQ__Account__1F642548F9EAAB03", IsUnique = true)]
[Microsoft.EntityFrameworkCore.Index("Username", Name = "UQ__Account__536C85E4655FBD4F", IsUnique = true)]
public partial class Account
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string UserCode { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Pass { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string EmployeeCode { get; set; } = null!;

    [ForeignKey("EmployeeCode")]
    [InverseProperty("Account")]
    public virtual Employee EmployeeCodeNavigation { get; set; } = null!;
}
