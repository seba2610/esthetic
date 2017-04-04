﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esthetic.Models
{
    public class HomeModel
    {
        public string CoverMainText { get; set; }

        public string CoverDescriptionText { get; set; }

        public string CoverImagePath { get; set; }

        public bool ShowCoverGallery { get; set; }

        public int GalleryCount { get; set; }
    }
}