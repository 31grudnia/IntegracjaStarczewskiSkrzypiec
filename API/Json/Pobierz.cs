using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using API.Json;

namespace API
{
    public class Pobierz
    {
       public  static List<SzablonSong> Download()
        {
             List<SzablonSong> songs = new List<SzablonSong>();
           
            using (StreamReader r = new StreamReader("./Json/Assets/data.json"))
            {
                string json = r.ReadToEnd();
                songs = JsonSerializer.Deserialize<List<SzablonSong>>(json);
            }
    
            return songs;

        }
    }
}
