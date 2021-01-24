using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace MusicTeacher.Repos
{
    public class SelectQuery
    {
        public string Select { get; set; }
        public string From { get; set; }
        public string Where { get; set; }
        public List<String> AdditionalCriteria { get; set; }
        public string OrderBy { get; set; }
        public string Fetch { get; set; }
        public dynamic Parms { get; set; } //For holding bind parameters name of parameter should match name is query

        public SelectQuery()
        {
            AdditionalCriteria = new List<string>();
            Parms = new ExpandoObject();
        }

        public override string ToString()
        {
            //Append the components. Add a space in between just to ensure two clauses don't run together
            StringBuilder sb = new StringBuilder(Select);
            sb.Append(' ');
            sb.Append(From);
            if(!String.IsNullOrWhiteSpace(Where)) { sb.Append(' '); }
            sb.Append(Where);
            foreach(string clause in AdditionalCriteria)
            {
                sb.Append(' ');
                sb.Append(clause);
            }
            if (!String.IsNullOrWhiteSpace(OrderBy)) { sb.Append(' '); }
            sb.Append(OrderBy);
            if (!String.IsNullOrWhiteSpace(Fetch)) { sb.Append(' '); }
            sb.Append(Fetch); //for paging
            return sb.ToString();
        }

        public string ToCountString()
        {
            StringBuilder sb = new StringBuilder("Select count(*) ");
            sb.Append(From);
            if (!String.IsNullOrWhiteSpace(Where)) { sb.Append(' '); }
            sb.Append(Where);
            foreach (string clause in AdditionalCriteria)
            {
                sb.Append(' ');
                sb.Append(clause);
            }
            return sb.ToString();
        }
    }
}
