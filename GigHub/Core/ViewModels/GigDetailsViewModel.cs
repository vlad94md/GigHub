﻿using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigDetailsViewModel
    {
        public Gig Gig { get; set; }

        public bool isFollowing { get; set; }

        public bool isAttending { get; set; }
    }
}