using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class Content
    {

        public int wordcount { get; set; }
        public string htmlcontent { get; set; }
        public int imagecount { get; set; }

        public List<string> ImageURLs { get; set; }

        public List<string> words { get; set; }

      public  Dictionary<string, int> Occurance = new Dictionary<string, int>();


    }
}