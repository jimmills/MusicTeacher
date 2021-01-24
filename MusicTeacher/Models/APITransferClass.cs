using System;
using System.Collections.Generic;

namespace MusicTeacher.Models
{
    //For enabling HATEOAS by providing a link collection on objects returned by the API
    public abstract class APITransferClass
    {
        public List<Link> Links { get; set; }

        public APITransferClass()
        {
            Links = new List<Link>();
        }

        public void AddLink(string href, string rel, string type)
        {
            Links.Add(new Link(href, rel, type));
        }

        public void AddLink(Link link) => Links.Add(link);
    }
}
