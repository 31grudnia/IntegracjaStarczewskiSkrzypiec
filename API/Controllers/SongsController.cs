using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Json;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Controllers
{
    public class SongsController : BaseApiController
    {

         private readonly DataContext _context;

        public SongsController(DataContext context)
        {
            _context = context;
        }

          [HttpGet("getSongsBaza")]
        public async Task<ActionResult<IEnumerable<Entities.Song>>> GetSongsBaza(){
            
            return await _context.Songs.ToListAsync();
        }

        

         [HttpGet("getSongsBaza/{nazwa}")]
        public async Task<ActionResult<IEnumerable<Entities.Song>>> GetSongsBazaNazwa(string nazwa){
            
            return await _context.Songs.Where(a => a.artist == nazwa).ToListAsync();
        }

        [HttpGet("zlicz")]
        public List<ListaWykres> zliczPiosenki(){
            List<SzablonSong> songs = new List<SzablonSong>();
            List<ListaWykres> listaWykres = new List<ListaWykres>();
           
            using (StreamReader r = new StreamReader("./Json/Assets/data.json"))
            {
                string json = r.ReadToEnd();
                songs = JsonSerializer.Deserialize<List<SzablonSong>>(json);
            }
            var czyBylo = false;
            foreach(SzablonSong s in songs){
                czyBylo=false;
                foreach(ListaWykres lw in listaWykres){
                    if(lw.year.Equals(s.year)){
                        lw.liczba++;
                        czyBylo=true;
                    }
                    
                }
                if(!czyBylo){
                    ListaWykres temp = new ListaWykres();
                    temp.year = s.year;
                    temp.liczba = 1;
                        listaWykres.Add(temp);
                    }
            }

            foreach(ListaWykres lw in listaWykres){
                
                Console.WriteLine(lw.year + " "+ lw.liczba);

            }
            listaWykres.Sort((p, q) => p.year.CompareTo(q.year));
            return listaWykres;
        }

     
        
    }
}