using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HallOfBeorn.Models
{
    public class Artist
    {
        public string Name { get; set; }
        public string URL { get; set; }

        static Artist()
        {
            all.Add(Alexandru_Sabo);
        }

        private static List<Artist> all = new List<Artist>();

        public static List<Artist> All
        {
            get { return all; }
        }

        public static Artist A_M_Sartor = new Artist { Name = "A. M. Sartor", URL = "" };
        public static Artist Alexandr_Shaldin = new Artist { Name = "Alexandr Shaldin", URL = "" };
        public static Artist Alexandru_Sabo = new Artist { Name = "Alexandru Sabo", URL = "" };
        public static Artist Andrew_Olson = new Artist { Name = "Andrew Olson", URL = "" };
        public static Artist Angela_Sung = new Artist { Name = "Angela Sung", URL = "" };
        public static Artist Anna_Christenson = new Artist { Name = "Anna Christenson", URL = "" };
        public static Artist Ben_Zweifel = new Artist { Name = "Ben Zweifel", URL = "" };
        public static Artist Darrken = new Artist { Name = "Daarken", URL = "" };
        public static Artist Daryl_Mandryk = new Artist { Name = "Daryl Mandryk", URL = "" };
        public static Artist David_Horne = new Artist { Name = "David Horne", URL = "" };
        public static Artist David_Lecossu = new Artist { Name = "David Lecossu", URL = "" };
        public static Artist David_A_Nash = new Artist { Name = "David A. Nash", URL = "" };
        public static Artist Drew_Whitmore = new Artist { Name = "Drew Whitmore", URL = "" };
        public static Artist Empty_Room_Studios = new Artist { Name = "Empty Room Studios", URL = "" };
        public static Artist Erfian_Asafat = new Artist { Name = "Erfian Asafat", URL = "http://masterchomic.deviantart.com" };
        public static Artist Even_Mehl_Amundsen = new Artist { Name = "Even Mehl Amundsen", URL = "" };
        public static Artist Florian_Stitz = new Artist { Name = "Florian Stitz", URL = "" };
        public static Artist Frank_Walls = new Artist { Name = "Frank Walls", URL = "" };
        public static Artist Fredrik_Dahl_Tyskerud = new Artist { Name = "Fredrik Dahl Tyskerud", URL = "" };
        public static Artist Gabrielle_Portal = new Artist { Name = "Gabrielle Portal", URL = "" };
        public static Artist Igor_Kieryluk = new Artist { Name = "Igor Kieryluk", URL = "" };
        public static Artist Ijur = new Artist { Name = "Ijur", URL = "" };
        public static Artist Jason_Ward = new Artist { Name = "Jason Ward", URL = "" };
        public static Artist Jeff_Himmelman = new Artist { Name = "Jeff Himmelman", URL = "" };
        public static Artist Jen_Zee = new Artist { Name = "Jen Zee", URL = "" };
        public static Artist John_Stanko = new Artist { Name = "John Stanko", URL = "" };
        public static Artist John_Wigley = new Artist { Name = "John Wigley", URL = "" };
        public static Artist Katherine_Dinger = new Artist { Name = "Katherine Dinger", URL = "" };
        public static Artist Kaya = new Artist { Name = "Kaya", URL = "" };
        public static Artist Kristina_Gehrmann = new Artist { Name = "Kristina Gehrmann", URL = "" };
        public static Artist Leonardo_Borazio = new Artist { Name = "Leonardo Borazio", URL = "" };
        public static Artist Lius_Lasahido = new Artist { Name = "Lius Lasahido", URL = "" };
        public static Artist Loren_Fetterman = new Artist { Name = "Loren Fetterman", URL = "" };
        public static Artist Lucas_Graciano = new Artist { Name = "Lucas Graciano", URL = "" };
        public static Artist Magali_Villeneuve = new Artist { Name = "Magali Villeneuve", URL = "" };
        public static Artist Marc_Scheff = new Artist { Name = "Marc Scheff", URL = "" };
        public static Artist Marco_Caradonna = new Artist { Name = "Marco Caradonna", URL = "" };
        public static Artist Margaret_Hardy = new Artist { Name = "Margaret Hardy", URL = "" };
        public static Artist Mark_Winters = new Artist { Name = "Mark Winters", URL = "" };
        public static Artist Mathias_Kollros = new Artist { Name = "Mathias Kollros", URL = "" };
        public static Artist Matthew_Starbuck = new Artist { Name = "Matthew Starbuck", URL = "" };
        public static Artist Mike_Nash = new Artist { Name = "Mike Nash", URL = "" };
        public static Artist Nicholas_Cloister = new Artist { Name = "Nicholas Cloister", URL = "" };
        public static Artist Nikolay_Stoyanov = new Artist { Name = "Nikolay Stoyanov", URL = "" };
        public static Artist Rio_Sabda = new Artist { Name = "Rio Sabda", URL = "" };
        public static Artist Ryan_Barger = new Artist { Name = "Ryan Barger", URL = "http://ryanbarger.deviantart.com" };
        public static Artist Sandra_Tang = new Artist { Name = "Sandra Tang", URL = "" };
        public static Artist Santiago_Villa = new Artist { Name = "Santiago Villa", URL = "" };
        public static Artist Sara_Biddle = new Artist { Name = "Sara Biddle", URL = "" };
        public static Artist Soul_Core = new Artist { Name = "Soul Core", URL = "" };
        public static Artist Tiziano_Baracchi = new Artist { Name = "Tiziano Baracchi", URL = "" };
        public static Artist Tom_Garden = new Artist { Name = "Tom Garden", URL = "" };
        public static Artist Tony_Foti = new Artist { Name = "Tony Foti", URL = "" };
        public static Artist West_Clendinning = new Artist { Name = "West Clendinning", URL = "" };
        public static Artist Winona_Nelson = new Artist { Name = "Winona Nelson", URL = "" };
        public static Artist Yoann_Boissonnet = new Artist { Name = "Yoann Boissonnet", URL = "" };
    }
}