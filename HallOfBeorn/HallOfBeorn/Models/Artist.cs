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

            //Shadows of Mirkwood
            all.Add(Artist.Felicia_Cano);
            all.Add(Artist.Joko_Mulyono);
            all.Add(Artist.Jarreau_Wimberly);
            all.Add(Artist.Brian_Valenzuela);
            all.Add(Artist.Toni_Justamante_Jacobs);

            all.Add(Artist.Paul_Guzenko);
            all.Add(Artist.Andrew_Silver);
            all.Add(Artist.Taufiq);
            all.Add(Artist.Vicki_Pangestu);
            all.Add(Artist.John_Matson);

            all.Add(Artist.Anna_Mohrbacher);
            all.Add(Artist.Brandon_Leach);
            all.Add(Artist.Julia_Laud);
            all.Add(Artist.Fandy_Sugiarto);

            all.Add(Artist.Vincent_Proce);
            all.Add(Artist.Aaron_B_Miller);
            all.Add(Artist.Bill_Corbett);

            all.Add(Artist.Stephanie_M_Brown);
            all.Add(Artist.Lindsey_Messecar);
            all.Add(Artist.Henning_Ludvigsen);

            all.Add(Artist.Dmitri_Bielak);
            all.Add(Artist.Mark_Poole);

            //Khazad-dum
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

            //Heirs of Númenor
            all.Add(Artist.Titus_Lunter);
            all.Add(Artist.Jeff_Lee_Johnson);
            all.Add(Artist.Emrah_Elmasli);
            all.Add(Artist.Matt_Smith);
            all.Add(Artist.Marcia_George_Begdan);
            all.Add(Artist.Derek_Zabrocki);
            all.Add(Artist.Lorraine_Schleter);
            all.Add(Artist.Anna_Steinbauer);
            all.Add(Artist.Aurelien_Hubert);
            all.Add(Artist.Adam_Lane);
            all.Add(Artist.Ellyson_Ferrari_Lifante);
            all.Add(Artist.Trudi_Castle);
            all.Add(Artist.Jim_Pavelec);
            all.Add(Artist.Dean_Spencer);
            all.Add(Artist.Piya_Wannachaiwong);
            all.Add(Artist.David_Kegg);
            all.Add(Artist.Emilio_Rodriguez);
            all.Add(Artist.Anton_Kokarev);
            all.Add(Artist.Suzanne_Helmigh);
            all.Add(Artist.Greg_Opalinski);
            all.Add(Artist.Asahi);
            all.Add(Artist.Caravan_Studio);
            all.Add(Artist.Ignacio_Bazan_Lazcano);
            all.Add(Artist.Emile_Denis);
            all.Add(Artist.C_B_Sorge);
            all.Add(Artist.Dan_Masso);
            all.Add(Artist.David_Gaillet);

            /*
            //The Voice of Isengard
            all.Add(Artist.Joel_Hustak);
            all.Add(Artist.Alvaro_Calvo_Escudero);
            all.Add(Artist.Alexandre_Dainche);
            all.Add(Artist.Matt_Stewart);
            all.Add(Artist.Nacho_Molina);
            all.Add(Artist.Alyn_Spiller);
            all.Add(Artist.John_Gravato);
            all.Add(Artist.Sara_Betsy);
            all.Add(Artist.Regis_Moulun);
            all.Add(Artist.Rafal_Hrynkiewicz);
            all.Add(Artist.Brent_Hollowel);
            all.Add(Artist.Sarah_Morris);
            all.Add(Artist.Diego_Gisbert_Llorens);
            all.Add(Artist.Sabin_Boykinov);
            all.Add(Artist.Sebastian_Giacobino);
            all.Add(Artist.Owen_William_Weber);
            all.Add(Artist.Sandra_Duchiewicz);
            */

            /*
            //The Hobbit: Over Hill and Under Hill
            all.Add(Artist.Blake_Henriksen);
            all.Add(Artist.Chris_Rahn);
            all.Add(Artist.David_Demaret);
            all.Add(Artist.Carmen_Cianelli);
            all.Add(Artist.Johann_Bodin);
            all.Add(Artist.Stephane_Gantiez);
            all.Add(Artist.Chun_Lo);
            all.Add(Artist.Adam_Schumpert);
            all.Add(Artist.Helmutt);
            */

            /*
            //The Hobbit: On the Doorstep
            all.Add(Artist.Christina_Davis);
            all.Add(Artist.Taylor_Ingvarsson);
            all.Add(Artist.Melissa_Findley);
            all.Add(Artist.Melanie_Maier);
            all.Add(Artist.Sidharth_Chatursedi);
            all.Add(Artist.Alex_Stone);
            all.Add(Artist.Christopher_Burdett);
            all.Add(Artist.Eric_Braddock);
            all.Add(Artist.Sara_K_Diesel);
            */

            //Khazad-dum Nightmare
            all.Add(Artist.Guillaume_Ducos);
            all.Add(Artist.Mark_Bulahao);
            all.Add(Artist.Katy_Grierson);
            all.Add(Artist.Anthony_Feliciano);
            all.Add(Artist.Juan_Carlos_Barquet);
            all.Add(Artist.Rick_Price);
            all.Add(Artist.Gabriel_Verdon);
            all.Add(Artist.Mark_Behm);
            
        }

        private static List<Artist> all = new List<Artist>();

        public static List<Artist> All()
        {
            return all.OrderBy(a => a.Name).ToList();
        }

        public static Artist Aaron_B_Miller = new Artist { Name = "Aaron B. Miller", URL = "http://aaronmiller.deviantart.com" };
        public static Artist A_M_Sartor = new Artist { Name = "A. M. Sartor", URL = "http://amsartor.com" };
        public static Artist Adam_Lane = new Artist { Name = "Adam Lane", URL = "http://www.adamlaneart.com/#home" };
        public static Artist Adam_Schumpert = new Artist { Name = "Adam Schumpert", URL = "http://aschumpert.deviantart.com" };
        public static Artist Alex_Stone = new Artist { Name = "Alex Stone", URL = "http://alexstoneart.deviantart.com" };
        public static Artist Alexandr_Shaldin = new Artist { Name = "Alexandr Shaldin", URL = "http://twilight30.cgsociety.org/gallery" };
        public static Artist Alexandre_Dainche = new Artist { Name = "Alexandre Dainche", URL = "http://www.imaginales.fr/alexandre-dainche" };
        public static Artist Alexandru_Sabo = new Artist { Name = "Alexandru Sabo", URL = "http://alexandrusabo.ro" };
        public static Artist Allison_Theus = new Artist { Name = "Allison Theus", URL = "http://beastofoblivion.deviantart.com" };
        public static Artist Alvaro_Calvo_Escudero = new Artist { Name = "Álvaro Calvo Escudero", URL = "http://escudero.deviantart.com" };
        public static Artist Alyn_Spiller = new Artist { Name = "Alyn Spiller", URL = "http://niltrace.deviantart.com" };
        public static Artist Andrew_Johanson = new Artist { Name = "Andrew Johanson", URL = "http://andrewjohanson.blogspot.com" };
        public static Artist Andrew_Olson = new Artist { Name = "Andrew Olson", URL = "http://mysticaldonkey1.deviantart.com" };
        public static Artist Andrew_Silver = new Artist { Name = "Andrew Silver", URL = "http://drewsil.deviantart.com" };
        public static Artist Angela_Sung = new Artist { Name = "Angela Sung", URL = "http://flyingmilkpig.deviantart.com" };
        public static Artist Anna_Christenson = new Artist { Name = "Anna Christenson", URL = "http://freshpaint.deviantart.com" };
        public static Artist Anna_Mohrbacher = new Artist { Name = "Anna Mohrbacher", URL = "http://aniamohrbacher.deviantart.com" };
        public static Artist Anna_Steinbauer = new Artist { Name = "Anna Steinbauer", URL = "http://depingo.deviantart.com" };
        public static Artist Anthony_Feliciano = new Artist { Name = "Anthony Feliciano", URL = "http://hurr-hurr-hurr.deviantart.com" };
        public static Artist Anthony_Palumbo = new Artist { Name = "Anthony Palumbo", URL = "http://anthonypalumboillustration.com" };
        public static Artist Anton_Kokarev = new Artist { Name = "Anton Kokarev", URL = "http://kanartist.deviantart.com" };
        public static Artist Asahi = new Artist { Name = "Asahi", URL = "http://asahisuperdry.deviantart.com" };
        public static Artist Aurelien_Hubert = new Artist { Name = "Aurelien Hubert", URL = "http://aurelien-hubert-ash.blogspot.com" };
        public static Artist Ben_Zweifel = new Artist { Name = "Ben Zweifel", URL = "http://benzweifel.com" };
        public static Artist Bill_Corbett = new Artist { Name = "Bill Corbett", URL = "http://billcorbett.deviantart.com" };
        public static Artist Blake_Henriksen = new Artist { Name = "Blake Henriksen", URL = "http://pinkhavok.deviantart.com" };
        public static Artist Brandon_Leach = new Artist { Name = "Brandon Leach", URL = "http://b-nine.deviantart.com" };
        public static Artist Brent_Hollowel = new Artist { Name = "Brent Hollowel", URL = "http://brenthollowellart.blogspot.com" };
        public static Artist Brian_Valenzuela = new Artist { Name = "Brian Valenzuela", URL = "http://bval05.deviantart.com" };
        public static Artist C_B_Sorge = new Artist { Name = "C. B. Sorge", URL = "http://psychohazard.deviantart.com" };
        public static Artist Caravan_Studio = new Artist { Name = "Caravan Studio", URL = "http://caravanstudio.com" };
        public static Artist Carmen_Cianelli = new Artist { Name = "Carmen Cianelti", URL = "http://arteche.deviantart.com" };
        public static Artist Carolina_Eade = new Artist { Name = "Carolina Eade", URL = "http://carolina-eade.deviantart.com" };
        public static Artist Chris_Rahn = new Artist { Name = "Chris Rahn", URL = "http://www.rahnart.com" };
        public static Artist Christina_Davis = new Artist { Name = "Christina Davis", URL = "http://sentinel13.deviantart.com" };
        public static Artist Christopher_Burdett = new Artist { Name = "Christopher Burdett", URL = "http://christopherburdett.deviantart.com" };
        public static Artist Chun_Lo = new Artist { Name = "Chun Lo", URL = "http://chunlo.deviantart.com" };
        public static Artist Cristi_Balanescu = new Artist { Name = "Cristi Balanescu", URL = "http://cristi-b.deviantart.com" };
        public static Artist Daarken = new Artist { Name = "Daarken", URL = "http://daarken.deviantart.com" };
        public static Artist Dan_Masso = new Artist { Name = "Dan Masso", URL = "http://danmasso.com" };
        public static Artist Daryl_Mandryk = new Artist { Name = "Daryl Mandryk", URL = "http://www.mandrykart.com" };
        public static Artist David_Demaret = new Artist { Name = "David Demaret", URL = "http://moonxels.deviantart.com" };
        public static Artist David_Gaillet = new Artist { Name = "David Gaillet", URL = "http://davidgaillet.deviantart.com" };
        public static Artist David_Horne = new Artist { Name = "David Horne", URL = "http://www.epilogue.net/gallery/davidhorne" };
        public static Artist David_Kegg = new Artist { Name = "David Kegg", URL = "http://david-kegg.deviantart.com" };
        public static Artist David_Lecossu = new Artist { Name = "David Lecossu", URL = "http://d--co.deviantart.com" };
        public static Artist David_A_Nash = new Artist { Name = "David A. Nash", URL = "http://davidnashart.blogspot.com" };
        public static Artist Dean_Spencer = new Artist { Name = "Dean Spencer", URL = "http://www.deanspencerart.com" };
        public static Artist Derek_Zabrocki = new Artist { Name = "Derek Zabrocki", URL = "http://daroz.deviantart.com" };
        public static Artist Diego_Gisbert_Llorens = new Artist { Name = "Diego Gisbert Llorens", URL = "http://diegogisbertllorens.deviantart.com" };
        public static Artist Dleoblack = new Artist { Name = "Dleoblack", URL = "http://dleoblack.deviantart.com" };
        public static Artist Dmitri_Bielak = new Artist { Name = "Dmitri Bielak", URL = "http://bielakdimitri.canalblog.com" };
        public static Artist Drew_Whitmore = new Artist { Name = "Drew Whitmore", URL = "http://toasty.deviantart.com" };
        public static Artist Ellyson_Ferrari_Lifante = new Artist { Name = "Ellyson Ferrari Lifante", URL = "http://ellysonfl.deviantart.com" };
        public static Artist Emile_Denis = new Artist { Name = "Emile Denis", URL = "http://trishkell.deviantart.com" };
        public static Artist Emilio_Rodriguez = new Artist { Name = "Emilio Rodriguez", URL = "http://lazonartistica.blogspot.com" };
        public static Artist Empty_Room_Studios = new Artist { Name = "Empty Room Studios", URL = "http://empty-room-studios.deviantart.com" };
        public static Artist Emrah_Elmasli = new Artist { Name = "Emrah Elmasli", URL = "http://emrahelmasli.tumblr.com" };
        public static Artist Erfian_Asafat = new Artist { Name = "Erfian Asafat", URL = "http://masterchomic.deviantart.com" };
        public static Artist Eric_Braddock = new Artist { Name = "Eric Braddock", URL = "http://ericbraddock.deviantart.com" };
        public static Artist Even_Mehl_Amundsen = new Artist { Name = "Even Mehl Amundsen", URL = "http://mischeviouslittleelf.deviantart.com" };
        public static Artist Fandy_Sugiarto = new Artist { Name = "Fandy Sugiarto", URL = "https://www.google.com/search?q=fandy+sugiarto+fantasy+art" };
        public static Artist Felicia_Cano = new Artist { Name = "Felicia Cano", URL = "http://feliciacano.deviantart.com" };
        public static Artist Florian_Stitz = new Artist { Name = "Florian Stitz", URL = "http://fstitz.deviantart.com" };
        public static Artist Frank_Walls = new Artist { Name = "Frank Walls", URL = "http://frank-walls.deviantart.com" };
        public static Artist Fredrik_Dahl_Tyskerud = new Artist { Name = "Fredrik Dahl Tyskerud", URL = "http://dcept.deviantart.com" };
        public static Artist Gabriel_Verdon = new Artist { Name = "Gabriel Verdon", URL = "http://gabriel-verdon.deviantart.com" };
        public static Artist Gabrielle_Portal = new Artist { Name = "Gabrielle Portal", URL = "http://gabrielleportaldesign.blogspot.com" };
        public static Artist Greg_Opalinski = new Artist { Name = "Greg Opalinski", URL = "http://greg-opalinski.deviantart.com" };
        public static Artist Guido_Kuip = new Artist { Name = "Guido Kuip", URL = "http://yoitisi.deviantart.com" };
        public static Artist Guillaume_Ducos = new Artist { Name = "Guillaume Ducos", URL = "http://herckeim.deviantart.com" };
        public static Artist Helmutt = new Artist { Name = "Helmutt", URL = "http://helmuttt.deviantart.com" };
        public static Artist Henning_Ludvigsen = new Artist { Name = "Henning Ludvigsen", URL = "http://henning.deviantart.com" };
        public static Artist Ignacio_Bazan_Lazcano = new Artist { Name = "Ignacio Bazan Lazcano", URL = "http://neisbeis.deviantart.com" };
        public static Artist Igor_Kieryluk = new Artist { Name = "Igor Kieryluk", URL = "http://igorkieryluk.deviantart.com" };
        public static Artist Ilich_Henriquez = new Artist { Name = "Ilich Henriquez", URL = "http://ilacha.deviantart.com" };
        public static Artist Ijur = new Artist { Name = "Ijur", URL = "http://ijur.deviantart.com" };
        public static Artist Jake_Murray = new Artist { Name = "Jake Murray", URL = "http://www.jakemurrayart.blogspot.com" };
        public static Artist Jarreau_Wimberly = new Artist { Name = "Jarreau Wimberly", URL = "http://reau.deviantart.com" };
        public static Artist Jason_Ward = new Artist { Name = "Jason Ward", URL = "http://jasonwardart.com" };
        public static Artist Jeff_Himmelman = new Artist { Name = "Jeff Himmelman", URL = "http://jhimmelman.deviantart.com" };
        public static Artist Jeff_Lee_Johnson = new Artist { Name = "Jeff Lee Johnson", URL = "http://jeffleejohnsonart.com/jeffleejohnsonart.com/home.html" };
        public static Artist Jen_Zee = new Artist { Name = "Jen Zee", URL = "http://jenzee.deviantart.com" };
        public static Artist Jim_Pavelec = new Artist { Name = "Jim Pavelec", URL = "http://www.jimpavelec.com" };
        public static Artist Joe_Wilson = new Artist { Name = "Joe Wilson", URL = "http://jwilsonillustration.deviantart.com" };
        public static Artist Joel_Hustak = new Artist { Name = "Joel Hustak", URL = "http://www.joelhustak.com" };
        public static Artist Johann_Bodin = new Artist { Name = "Johann Bodin", URL = "http://yozartwork.deviantart.com" };
        public static Artist John_Gravato = new Artist { Name = "John Gravato", URL = "http://www.conceptboxstudios.com" };
        public static Artist John_Matson = new Artist { Name = "John Matson", URL = "http://matsony.deviantart.com" };
        public static Artist John_Stanko = new Artist { Name = "John Stanko", URL = "http://stankoillustration.com" };
        public static Artist John_Wigley = new Artist { Name = "John Wigley", URL = "http://wiggers123.deviantart.com" };
        public static Artist Joko_Mulyono = new Artist { Name = "Joko Mulyono", URL = "http://jokomulyono.deviantart.com" };
        public static Artist Juan_Carlos_Barquet = new Artist { Name = "Juan Carlos Barquet", URL = "http://jcbarquet.deviantart.com" };
        public static Artist Julia_Laud = new Artist { Name = "Julia Laud", URL = "http://julialaud.deviantart.com" };
        public static Artist Katherine_Dinger = new Artist { Name = "Katherine Dinger", URL = "http://jezebel.deviantart.com" };
        public static Artist Katy_Grierson = new Artist { Name = "Katy Grierson", URL = "http://kovah.deviantart.com" };
        public static Artist Kaya = new Artist { Name = "Kaya", URL = "http://gkb3rk.deviantart.com" };
        public static Artist Kristina_Gehrmann = new Artist { Name = "Kristina Gehrmann", URL = "http://kristinagehrmann.deviantart.com" };
        public static Artist Leonardo_Borazio = new Artist { Name = "Leonardo Borazio", URL = "http://dleoblack.deviantart.com" };
        public static Artist Lin_Bo = new Artist { Name = "Lin Bo", URL = "http://0bo.deviantart.com/" };
        public static Artist Lindsey_Messecar = new Artist { Name = "Lindsey Messecar", URL = "http://lmessecar.deviantart.com" };
        public static Artist Lius_Lasahido = new Artist { Name = "Lius Lasahido", URL = "http://lasahido.deviantart.com" };
        public static Artist Loren_Fetterman = new Artist { Name = "Loren Fetterman", URL = "http://loren86.deviantart.com" };
        public static Artist Lorraine_Schleter = new Artist { Name = "Lorraine Schleter", URL = "http://lorraine-schleter.deviantart.com" };
        public static Artist Lucas_Graciano = new Artist { Name = "Lucas Graciano", URL = "http://lucasgraciano.deviantart.com" };
        public static Artist Magali_Villeneuve = new Artist { Name = "Magali Villeneuve", URL = "http://magali-villeneuve.blogspot.com" };
        public static Artist Marc_Scheff = new Artist { Name = "Marc Scheff", URL = "http://www.marcscheff.com" };
        public static Artist Marcia_George_Begdan = new Artist { Name = "Marcia George-Begdan", URL = "https://www.google.com/search?q=marcia+george-bogdan+art" };
        public static Artist Marco_Caradonna = new Artist { Name = "Marco Caradonna", URL = "http://ming1918.deviantart.com" };
        public static Artist Margaret_Hardy = new Artist { Name = "Margaret Hardy", URL = "http://kiwikitty37.deviantart.com" };
        public static Artist Mark_Behm = new Artist { Name = "Mark Behm", URL = "http://www.markbehm.com" };
        public static Artist Mark_Bulahao = new Artist { Name = "Mark Bulahao", URL = "http://markbulahao.deviantart.com" };
        public static Artist Mark_Poole = new Artist { Name = "Mark Poole", URL = "http://www.markpoole.net" };
        public static Artist Mark_Winters = new Artist { Name = "Mark Winters", URL = "http://markwinters.deviantart.com" };
        public static Artist Mathias_Kollros = new Artist { Name = "Mathias Kollros", URL = "http://guterrez.deviantart.com" };
        public static Artist Matt_Smith = new Artist { Name = "Matt Smith", URL = "https://www.google.com/search?q=matt+smith+fantasy+art" };
        public static Artist Matt_Stewart = new Artist { Name = "Matt Stewart", URL = "http://mattstewartartblog.blogspot.com" };
        public static Artist Matthew_Starbuck = new Artist { Name = "Matthew Starbuck", URL = "http://faxtar.deviantart.com" };
        public static Artist Melanie_Maier = new Artist { Name = "Melanie Maier", URL = "http://melaniemaier.deviantart.com" };
        public static Artist Melissa_Findley = new Artist { Name = "Melissa Findley", URL = "http://melissafindley.deviantart.com" };
        public static Artist Michael_Rasmussen = new Artist { Name = "Michael Rasmussen", URL = "http://www.rasmussenillustration.com" };
        public static Artist Mike_Capprotti = new Artist { Name = "Mike Capprotti", URL = "http://capprotti.deviantart.com" };
        public static Artist Mike_Nash = new Artist { Name = "Mike Nash", URL = "http://www.mike-nash.com/HOME.html" };
        public static Artist Nacho_Molina = new Artist { Name = "Nacho Molina", URL = "http://nachomolina.deviantart.com" };
        public static Artist Nicholas_Cloister = new Artist { Name = "Nicholas Cloister", URL = "http://cloister.deviantart.com" };
        public static Artist Nikolay_Stoyanov = new Artist { Name = "Nikolay Stoyanov", URL = "http://nstoyanov.deviantart.com" };
        public static Artist Noah_Bradley = new Artist { Name = "Noah Bradley", URL = "http://noahbradley.deviantart.com" };
        public static Artist Owen_William_Weber = new Artist { Name = "Owen William Weber", URL = "http://oweber.blogspot.com" };
        public static Artist Paul_Guzenko = new Artist { Name = "Paul Guzenko", URL = "http://www.guzboroda.com/?author=1" };
        public static Artist Piya_Wannachaiwong = new Artist { Name = "Piya Wannachaiwong", URL = "http://www.piyastudios.com" };
        public static Artist Rafal_Hrynkiewicz = new Artist { Name = "Rafal Hrynkiewicz", URL = "http://mcf.deviantart.com" };
        public static Artist Regis_Moulun = new Artist { Name = "Régis Moulun", URL = "http://moulunerie.deviantart.com" };
        public static Artist Rick_Price = new Artist { Name = "Rick Price", URL = "https://www.google.com/search?q=rick+price+fantasy+art" };
        public static Artist Rio_Sabda = new Artist { Name = "Rio Sabda", URL = "http://kepondangkuning.deviantart.com" };
        public static Artist Roman_V_Papsuev = new Artist { Name = "Roman V. Papsuev", URL = "http://en.amokanet.ru" };
        public static Artist Ryan_Barger = new Artist { Name = "Ryan Barger", URL = "http://ryanbarger.deviantart.com" };
        public static Artist Sabin_Boykinov = new Artist { Name = "Sabin Boykinov", URL = "http://sabin-boykinov.deviantart.com" };
        public static Artist Salvador_Trakal = new Artist { Name = "Salvador Trakal", URL = "http://saturnoarg.deviantart.com" };
        public static Artist Sandara_Tang = new Artist { Name = "Sandara Tang", URL = "http://sandara.deviantart.com" };
        public static Artist Sandra_Duchiewicz = new Artist { Name = "Sandra Duchiewicz", URL = "http://telthona.deviantart.com" };
        public static Artist Santiago_Villa = new Artist { Name = "Santiago Villa", URL = "http://www.billich.deviantart.com" };
        public static Artist Sara_Betsy = new Artist { Name = "Sara Betsy", URL = "http://sarabetsyillustration.blogspot.com" };
        public static Artist Sara_Biddle = new Artist { Name = "Sara Biddle", URL = "http://mckadesinsanity.deviantart.com" };
        public static Artist Sara_K_Diesel = new Artist { Name = "Sara K. Diesel", URL = "http://skdiesel.deviantart.com" };
        public static Artist Sarah_Morris = new Artist { Name = "Sarah Morris", URL = "https://www.google.com/search?q=sarah+morris+fantasy+art" };
        public static Artist Sebastian_Giacobino = new Artist { Name = "Sebastian Giacobino", URL = "http://giacobino.deviantart.com" };
        public static Artist Sidharth_Chatursedi = new Artist { Name = "Sidharth Chatursedi", URL = "http://sidharthchaturvedi.deviantart.com" };
        public static Artist Soul_Core = new Artist { Name = "Soul Core", URL = "http://en.amokanet.ru" };
        public static Artist Stacey_Diana_Clark = new Artist { Name = "Stacey Diana Clark", URL = "http://staceydiana.blogspot.com" };
        public static Artist Stephane_Gantiez = new Artist { Name = "Stephane Gantiez", URL = "http://lodin111.deviantart.com" };
        public static Artist Stephanie_M_Brown = new Artist { Name = "Stephanie M. Brown", URL = "https://www.google.com/search?q=stephanie+m.+brown+art" };
        public static Artist Stu_Barnes = new Artist { Name = "Stu Barnes", URL = "http://stuvsillustration.blogspot.com" };
        public static Artist Suzanne_Helmigh = new Artist { Name = "Suzanne Helmigh", URL = "http://suzanne-helmigh.deviantart.com" };
        public static Artist Taufiq = new Artist { Name = "Taufiq", URL = "http://toviz.deviantart.com" };
        public static Artist Taylor_Ingvarsson = new Artist { Name = "Taylor Ingvarsson", URL = "http://www.artoftaylor.com" };
        public static Artist Timo_Karhula = new Artist { Name = "Timo Karhula", URL = "http://tmza.deviantart.com" };
        public static Artist Titus_Lunter = new Artist { Name = "Titus Lunter", URL = "http://tituslunter.deviantart.com" };
        public static Artist Tiziano_Baracchi = new Artist { Name = "Tiziano Baracchi", URL = "http://thaldir.deviantart.com" };
        public static Artist Tom_Garden = new Artist { Name = "Tom Garden", URL = " http://tgconceptart.deviantart.com" };
        public static Artist Toni_Justamante_Jacobs = new Artist { Name = "Toni Justamante Jacobs", URL = "http://artofjustaman.deviantart.com" };
        public static Artist Tony_Foti = new Artist { Name = "Tony Foti", URL = "http://anthonyfoti.deviantart.com" };
        public static Artist Trudi_Castle = new Artist { Name = "Trudi Castle", URL = "http://www.artcastles.com/trudi/commerical.html" };
        public static Artist Vicki_Pangestu = new Artist { Name = "Vicki Pangestu", URL = "http://thiever.deviantart.com" };
        public static Artist Vincent_Proce = new Artist { Name = "Vincent Proce", URL = "http://vincentproceart.blogspot.com" };
        public static Artist West_Clendinning = new Artist { Name = "West Clendinning", URL = "http://vomisalabs.blogspot.com" };
        public static Artist Winona_Nelson = new Artist { Name = "Winona Nelson", URL = "http://winonanelson.blogspot.com" };
        public static Artist Yoann_Boissonnet = new Artist { Name = "Yoann Boissonnet", URL = "http://yoannboissonnet.carbonmade.com" };
    }
}