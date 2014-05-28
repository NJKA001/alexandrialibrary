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
            all.Add(Artist.A_M_Sartor);
            all.Add(Artist.Alexandr_Shaldin);
            all.Add(Artist.Alexandru_Sabo);
            all.Add(Artist.Allison_Theus);
            all.Add(Artist.Andrew_Olson);
            all.Add(Artist.Angela_Sung);
            all.Add(Artist.Anna_Christenson);
            all.Add(Artist.Ben_Zweifel);
            all.Add(Artist.Cristi_Balanescu);
            all.Add(Artist.Daarken);
            all.Add(Artist.Daryl_Mandryk);
            all.Add(Artist.David_A_Nash);
            all.Add(Artist.David_Horne);
            all.Add(Artist.David_Lecossu);
            all.Add(Artist.Drew_Whitmore);
            all.Add(Artist.Empty_Room_Studios);
            all.Add(Artist.Erfian_Asafat);
            all.Add(Artist.Even_Mehl_Amundsen);
            all.Add(Artist.Florian_Stitz);
            all.Add(Artist.Frank_Walls);
            all.Add(Artist.Fredrik_Dahl_Tyskerud);
            all.Add(Artist.Gabrielle_Portal);
            all.Add(Artist.Guido_Kuip);
            all.Add(Artist.Igor_Kieryluk);
            all.Add(Artist.Ijur);
            all.Add(Artist.Ilich_Henriquez);
            all.Add(Artist.Jason_Ward);
            all.Add(Artist.Jeff_Himmelman);
            all.Add(Artist.Jen_Zee);
            all.Add(Artist.Joe_Wilson);
            all.Add(Artist.John_Stanko);
            all.Add(Artist.John_Wigley);
            all.Add(Artist.Katherine_Dinger);
            all.Add(Artist.Kaya);
            all.Add(Artist.Kristina_Gehrmann);
            all.Add(Artist.Leonardo_Borazio);
            all.Add(Artist.Lius_Lasahido);
            all.Add(Artist.Loren_Fetterman);
            all.Add(Artist.Lucas_Graciano);
            all.Add(Artist.Magali_Villeneuve);
            all.Add(Artist.Marc_Scheff);
            all.Add(Artist.Marco_Caradonna);
            all.Add(Artist.Margaret_Hardy);
            all.Add(Artist.Mark_Winters);
            all.Add(Artist.Mathias_Kollros);
            all.Add(Artist.Matthew_Starbuck);
            all.Add(Artist.Mike_Nash);
            all.Add(Artist.Nicholas_Cloister);
            all.Add(Artist.Nikolay_Stoyanov);
            all.Add(Artist.Rio_Sabda);
            all.Add(Artist.Ryan_Barger);
            all.Add(Artist.Salvador_Trakal);
            all.Add(Artist.Sandara_Tang);
            all.Add(Artist.Santiago_Villa);
            all.Add(Artist.Sara_Biddle);
            all.Add(Artist.Soul_Core);
            all.Add(Artist.Stacey_Diana_Clark);
            all.Add(Artist.Tiziano_Baracchi);
            all.Add(Artist.Tom_Garden);
            all.Add(Artist.Tony_Foti);
            all.Add(Artist.West_Clendinning);
            all.Add(Artist.Winona_Nelson);
            all.Add(Artist.Yoann_Boissonnet);

            all.Add(Artist.Lin_Bo);
            all.Add(Artist.Stu_Barnes);
            all.Add(Artist.Anthony_Palumbo);
            all.Add(Artist.Jake_Murray);
            all.Add(Artist.Carolina_Eade);
            all.Add(Artist.Timo_Karhula);
            all.Add(Artist.Andrew_Johanson);
            all.Add(Artist.Dleoblack);
            all.Add(Artist.Michael_Rasmussen);
            all.Add(Artist.Noah_Bradley);
            all.Add(Artist.Mike_Capprotti);
            all.Add(Artist.Roman_V_Papsuev);
        }

        private static List<Artist> all = new List<Artist>();

        public static List<Artist> All()
        {
            return all.OrderBy(a => a.Name).ToList();
        }

        public static Artist A_M_Sartor = new Artist { Name = "A. M. Sartor", URL = "http://amsartor.com" };
        public static Artist Alexandr_Shaldin = new Artist { Name = "Alexandr Shaldin", URL = "http://twilight30.cgsociety.org/gallery" };
        public static Artist Alexandru_Sabo = new Artist { Name = "Alexandru Sabo", URL = "http://alexandrusabo.ro" };
        public static Artist Allison_Theus = new Artist { Name = "Allison Theus", URL = "http://beastofoblivion.deviantart.com" };
        public static Artist Andrew_Johanson = new Artist { Name = "Andrew Johanson", URL = "http://andrewjohanson.blogspot.com" };
        public static Artist Andrew_Olson = new Artist { Name = "Andrew Olson", URL = "http://mysticaldonkey1.deviantart.com" };
        public static Artist Angela_Sung = new Artist { Name = "Angela Sung", URL = "http://flyingmilkpig.deviantart.com" };
        public static Artist Anna_Christenson = new Artist { Name = "Anna Christenson", URL = "http://freshpaint.deviantart.com" };
        public static Artist Anthony_Palumbo = new Artist { Name = "Anthony Palumbo", URL = "http://anthonypalumboillustration.com" };
        public static Artist Ben_Zweifel = new Artist { Name = "Ben Zweifel", URL = "http://benzweifel.com" };
        public static Artist Carolina_Eade = new Artist { Name = "Carolina Eade", URL = "http://carolina-eade.deviantart.com/" };
        public static Artist Cristi_Balanescu = new Artist { Name = "Cristi Balanescu", URL = "http://cristi-b.deviantart.com" };
        public static Artist Daarken = new Artist { Name = "Daarken", URL = "http://daarken.deviantart.com" };
        public static Artist Daryl_Mandryk = new Artist { Name = "Daryl Mandryk", URL = "http://www.mandrykart.com" };
        public static Artist David_Horne = new Artist { Name = "David Horne", URL = "http://www.epilogue.net/gallery/davidhorne" };
        public static Artist David_Lecossu = new Artist { Name = "David Lecossu", URL = "http://d--co.deviantart.com/" };
        public static Artist David_A_Nash = new Artist { Name = "David A. Nash", URL = "http://davidnashart.blogspot.com" };
        public static Artist Dleoblack = new Artist { Name = "Dleoblack", URL = "http://dleoblack.deviantart.com/" };
        public static Artist Drew_Whitmore = new Artist { Name = "Drew Whitmore", URL = "http://toasty.deviantart.com" };
        public static Artist Empty_Room_Studios = new Artist { Name = "Empty Room Studios", URL = "http://empty-room-studios.deviantart.com" };
        public static Artist Erfian_Asafat = new Artist { Name = "Erfian Asafat", URL = "http://masterchomic.deviantart.com" };
        public static Artist Even_Mehl_Amundsen = new Artist { Name = "Even Mehl Amundsen", URL = "http://mischeviouslittleelf.deviantart.com" };
        public static Artist Florian_Stitz = new Artist { Name = "Florian Stitz", URL = "http://fstitz.deviantart.com" };
        public static Artist Frank_Walls = new Artist { Name = "Frank Walls", URL = "http://frank-walls.deviantart.com" };
        public static Artist Fredrik_Dahl_Tyskerud = new Artist { Name = "Fredrik Dahl Tyskerud", URL = "http://dcept.deviantart.com/" };
        public static Artist Gabrielle_Portal = new Artist { Name = "Gabrielle Portal", URL = "http://gabrielleportaldesign.blogspot.com" };
        public static Artist Guido_Kuip = new Artist { Name = "Guido Kuip", URL = "http://yoitisi.deviantart.com" };
        public static Artist Igor_Kieryluk = new Artist { Name = "Igor Kieryluk", URL = "http://igorkieryluk.deviantart.com" };
        public static Artist Ilich_Henriquez = new Artist { Name = "Ilich Henriquez", URL = "http://ilacha.deviantart.com/" };
        public static Artist Ijur = new Artist { Name = "Ijur", URL = "http://ijur.deviantart.com" };
        public static Artist Jake_Murray = new Artist { Name = "Jake Murray", URL = "http://www.jakemurrayart.blogspot.com" };
        public static Artist Jason_Ward = new Artist { Name = "Jason Ward", URL = "http://jasonwardart.com" };
        public static Artist Jeff_Himmelman = new Artist { Name = "Jeff Himmelman", URL = "http://jhimmelman.deviantart.com" };
        public static Artist Jen_Zee = new Artist { Name = "Jen Zee", URL = "http://jenzee.deviantart.com" };
        public static Artist Joe_Wilson = new Artist { Name = "Joe Wilson", URL = "http://jwilsonillustration.deviantart.com" };
        public static Artist John_Stanko = new Artist { Name = "John Stanko", URL = "http://stankoillustration.com" };
        public static Artist John_Wigley = new Artist { Name = "John Wigley", URL = "http://wiggers123.deviantart.com" };
        public static Artist Katherine_Dinger = new Artist { Name = "Katherine Dinger", URL = "http://jezebel.deviantart.com" };
        public static Artist Kaya = new Artist { Name = "Kaya", URL = "" };
        public static Artist Kristina_Gehrmann = new Artist { Name = "Kristina Gehrmann", URL = "http://kristinagehrmann.deviantart.com/" };
        public static Artist Leonardo_Borazio = new Artist { Name = "Leonardo Borazio", URL = "http://dleoblack.deviantart.com" };
        public static Artist Lin_Bo = new Artist { Name = "Lin Bo", URL = "http://0bo.deviantart.com/" };
        public static Artist Lius_Lasahido = new Artist { Name = "Lius Lasahido", URL = "http://lasahido.deviantart.com" };
        public static Artist Loren_Fetterman = new Artist { Name = "Loren Fetterman", URL = "http://loren86.deviantart.com" };
        public static Artist Lucas_Graciano = new Artist { Name = "Lucas Graciano", URL = "http://lucasgraciano.deviantart.com/" };
        public static Artist Magali_Villeneuve = new Artist { Name = "Magali Villeneuve", URL = "http://magali-villeneuve.blogspot.com" };
        public static Artist Marc_Scheff = new Artist { Name = "Marc Scheff", URL = "http://www.marcscheff.com" };
        public static Artist Marco_Caradonna = new Artist { Name = "Marco Caradonna", URL = "http://ming1918.deviantart.com" };
        public static Artist Margaret_Hardy = new Artist { Name = "Margaret Hardy", URL = "http://kiwikitty37.deviantart.com" };
        public static Artist Mark_Winters = new Artist { Name = "Mark Winters", URL = "http://markwinters.deviantart.com" };
        public static Artist Mathias_Kollros = new Artist { Name = "Mathias Kollros", URL = "http://guterrez.deviantart.com" };
        public static Artist Matthew_Starbuck = new Artist { Name = "Matthew Starbuck", URL = "http://faxtar.deviantart.com" };
        public static Artist Michael_Rasmussen = new Artist { Name = "Michael Rasmussen", URL = "http://www.rasmussenillustration.com/" };
        public static Artist Mike_Capprotti = new Artist { Name = "Mike Capprotti", URL = "http://capprotti.deviantart.com" };
        public static Artist Mike_Nash = new Artist { Name = "Mike Nash", URL = "http://www.mike-nash.com/HOME.html" };
        public static Artist Nicholas_Cloister = new Artist { Name = "Nicholas Cloister", URL = "http://cloister.deviantart.com" };
        public static Artist Nikolay_Stoyanov = new Artist { Name = "Nikolay Stoyanov", URL = "http://nstoyanov.deviantart.com" };
        public static Artist Noah_Bradley = new Artist { Name = "Noah Bradley", URL = "http://noahbradley.deviantart.com" };
        public static Artist Rio_Sabda = new Artist { Name = "Rio Sabda", URL = "http://kepondangkuning.deviantart.com/" };
        public static Artist Roman_V_Papsuev = new Artist { Name = "Roman V. Papsuev", URL = "" };
        public static Artist Ryan_Barger = new Artist { Name = "Ryan Barger", URL = "http://ryanbarger.deviantart.com" };
        public static Artist Salvador_Trakal = new Artist { Name = "Salvador Trakal", URL = "http://saturnoarg.deviantart.com" };
        public static Artist Sandara_Tang = new Artist { Name = "Sandara Tang", URL = "http://sandara.deviantart.com" };
        public static Artist Santiago_Villa = new Artist { Name = "Santiago Villa", URL = "http://www.billich.deviantart.com" };
        public static Artist Sara_Biddle = new Artist { Name = "Sara Biddle", URL = "http://mckadesinsanity.deviantart.com" };
        public static Artist Soul_Core = new Artist { Name = "Soul Core", URL = "http://en.amokanet.ru" };
        public static Artist Stacey_Diana_Clark = new Artist { Name = "Stacey Diana Clark", URL = "http://staceydiana.blogspot.com" };
        public static Artist Stu_Barnes = new Artist { Name = "Stu Barnes", URL = "http://stuvsillustration.blogspot.com" };
        public static Artist Timo_Karhula = new Artist { Name = "Timo Karhula", URL = "http://tmza.deviantart.com/" };
        public static Artist Tiziano_Baracchi = new Artist { Name = "Tiziano Baracchi", URL = "http://thaldir.deviantart.com" };
        public static Artist Tom_Garden = new Artist { Name = "Tom Garden", URL = " http://tgconceptart.deviantart.com" };
        public static Artist Tony_Foti = new Artist { Name = "Tony Foti", URL = "http://anthonyfoti.deviantart.com" };
        public static Artist West_Clendinning = new Artist { Name = "West Clendinning", URL = "http://vomisalabs.blogspot.com" };
        public static Artist Winona_Nelson = new Artist { Name = "Winona Nelson", URL = "http://winonanelson.blogspot.com" };
        public static Artist Yoann_Boissonnet = new Artist { Name = "Yoann Boissonnet", URL = "http://yoannboissonnet.carbonmade.com" };
    }
}