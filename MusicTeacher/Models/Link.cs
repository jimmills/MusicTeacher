﻿using System;
namespace MusicTeacher.Models
{
    public class Link
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Type { get; set; }

        public Link(string href, string rel, string type)
        {
            Href = href;
            Rel = rel;
            Type = type;
        }
    }
}
