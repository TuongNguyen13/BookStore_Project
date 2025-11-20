using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Microsoft.EntityFrameworkCore.Index("AuthorCode", Name = "UQ__Authors__E06BC2AB7633EEB1", IsUnique = true)]
public partial class Author
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string AuthorCode { get; set; } = null!;

    [StringLength(255)]
    public string AuthorName { get; set; } = null!;

    [StringLength(255)]
    public string? Bio { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Authors")]
    public virtual ICollection<Book> Products { get; set; } = new List<Book>();
}
