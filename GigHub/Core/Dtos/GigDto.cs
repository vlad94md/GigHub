using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }

        public bool IsCancel { get; set; }

        public UserDto Artist { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public GenreDto Genre { get; set; }
    }
}