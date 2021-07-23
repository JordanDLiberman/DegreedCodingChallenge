using System;
using System.Collections.Generic;
using System.Text;

namespace DegreedCodingChallenge.Entities
{
    public class JokeSearchResponseDTO
    {
        public int Current_Page { get; set; }
        public int Limit { get; set; }
        public int Next_Page { get; set; }
        public int Previous_Page { get; set; }
        public string Search_Term { get; set; }
        public int Status { get; set; }
        public int Total_Jokes { get; set; }
        public int Total_Pages { get; set; }
        public List<JokeDTO> Results { get; set; } = new List<JokeDTO>();
    }
}
