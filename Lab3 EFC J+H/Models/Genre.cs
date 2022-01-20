using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab3_EFC_J_H.Models
{
    [Table("genres", Schema = "music")]
    public partial class Genre
    {
        public Genre()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        public int GenreId { get; set; }
        [StringLength(120)]
        public string? Name { get; set; }

        [InverseProperty(nameof(Track.Genre))]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
