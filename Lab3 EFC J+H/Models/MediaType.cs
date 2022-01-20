using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab3_EFC_J_H.Models
{
    [Table("media_types", Schema = "music")]
    public partial class MediaType
    {
        public MediaType()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        public int MediaTypeId { get; set; }
        [StringLength(120)]
        public string? Name { get; set; }

        [InverseProperty(nameof(Track.MediaType))]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
