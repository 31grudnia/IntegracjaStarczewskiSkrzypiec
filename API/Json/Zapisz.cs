using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Json
{
    public class Zapisz
    {
         public static void SaveXML(List<SzablonSong> songs)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<SzablonSong>));

            string workingDirectory = Environment.CurrentDirectory;
            var path = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "/Desktop/Integracja/API/Json/Assets/SongsXML.xml";
            System.IO.FileStream file = System.IO.File.Create(path);
            ///Users/31_grudnia/Desktop/Integracja/API/Json/Assets

            writer.Serialize(file, songs);
            file.Close();
        }

        public static void SaveJSON(List<SzablonSong> songs)
        {
            string workingDirectory = Environment.CurrentDirectory;
            var path = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "/Desktop/Integracja/API/Json/Assets/SongsJSON.json";

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(path, JsonConvert.SerializeObject(songs, Newtonsoft.Json.Formatting.None));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(path))
            {
                Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

                serializer.Serialize(file, songs);
            }

         
        }
    }
}