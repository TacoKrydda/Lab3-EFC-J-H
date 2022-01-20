using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab3_EFC_J_H.Models
{
    [Table("albums", Schema = "music")]
    public partial class Album
    {
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        public int AlbumId { get; set; }
        [StringLength(160)]
        public string Title { get; set; } = null!;
        public int ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        [InverseProperty("Albums")]
        public virtual Artist Artist { get; set; } = null!;
        [InverseProperty(nameof(Track.Album))]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
