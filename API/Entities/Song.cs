using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string title { get; set; }

        public string artist { get; set; }

        public string year { get; set; }
        
    }
}