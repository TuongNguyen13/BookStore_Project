using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Microsoft.EntityFrameworkCore.Index("PublisherCode", Name = "UQ__Publishe__DFB88E29DF41AD7C", IsUnique = true)]
public partial class Publisher
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string PublisherCode { get; set; } = null!;

    [StringLength(255)]
    public string PublisherName { get; set; } = null!;

    [StringLength(255)]
    public string? Address { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [InverseProperty("Publisher")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
