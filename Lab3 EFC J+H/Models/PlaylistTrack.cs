using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab3_EFC_J_H.Models
{
    [Table("playlist_track", Schema = "music")]
    public partial class PlaylistTrack
    {
        [Key]
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }
    }
}
