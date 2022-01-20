using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab3_EFC_J_H.Models
{
    [Table("tracks", Schema = "music")]
    public partial class Track
    {
        [Key]
        public int TrackId { get; set; }
        [StringLength(200)]
        public string Name { get; set; } = null!;
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }
        [StringLength(220)]
        public string? Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public double UnitPrice { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty("Tracks")]
        public virtual Album? Album { get; set; }
        [ForeignKey(nameof(GenreId))]
        [InverseProperty("Tracks")]
        public virtual Genre? Genre { get; set; }
        [ForeignKey(nameof(MediaTypeId))]
        [InverseProperty("Tracks")]
        public virtual MediaType MediaType { get; set; } = null!;
    }
}
