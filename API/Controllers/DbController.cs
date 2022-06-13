using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Json;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DbController : BaseApiController
    {
        private readonly DataContext _db;

   
    public DbController(DataContext db)
    {
        _db = db;
    }



        [HttpPost("dodajSong")]
        public async Task<ActionResult<Song>> dodajCeneProduktu(Song song){

           

            var songN = new Song{
                title = song.title,
                artist = song.artist,
                year = song.year,

            };
            _db.Songs.Add(songN);
            await _db.SaveChangesAsync();

            return new Song{
               
                title = songN.title,
                artist = songN.artist,
                year = songN.year

            };

        }

        [HttpPost("dodajAllSongs")]
        public async void dodajAllCeny(){

         
          
             List<Song> songs = new List<Song>();
            
               using (StreamReader r = new StreamReader("./Json/Assets/data.json"))
            {
                string json = r.ReadToEnd();
                songs = JsonSerializer.Deserialize<List<Song>>(json);
            }

            foreach(Song song in songs){
                _db.Songs.Add(song);
                await _db.SaveChangesAsync();
            }
        }

        [HttpDelete("usunSong/{idW}")]
        public  async void usunWynagrodzenie(int idW){
            var wynToRemove = _db.Songs.SingleOrDefault(x => x.Id == idW);
           if(wynToRemove != null){
            _db.Songs.Remove(wynToRemove);
            await _db.SaveChangesAsync();
            }
           
        }

        [HttpGet("saveJson")]
        public void saveToJson(){
            List<SzablonSong> newListToJson = Pobierz.Download();
            Zapisz.SaveJSON(newListToJson);
        }

         [HttpGet("saveXML")]
        public void saveToXML(){
            List<SzablonSong> newListToJson = Pobierz.Download();
            Zapisz.SaveXML(newListToJson);
        }

    }
}