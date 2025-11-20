using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

[Microsoft.EntityFrameworkCore.Index("Isbn", Name = "UQ__Books__447D36EA4E25E4EA", IsUnique = true)]
public partial class Book
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("ISBN")]
    [StringLength(20)]
    [Unicode(false)]
    public string Isbn { get; set; } = null!;

    public int? PublishYear { get; set; }

    [Column("PublisherID")]
    public int? PublisherId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Book")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("PublisherId")]
    [InverseProperty("Books")]
    public virtual Publisher? Publisher { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Products")]
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
