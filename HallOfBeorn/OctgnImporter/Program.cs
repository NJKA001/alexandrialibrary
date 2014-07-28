using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using HallOfBeorn.Models;
using HallOfBeorn.Models.OCTGN;

namespace OctgnImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            const string rootPath = @"C:\Users\Dan\Documents\OCTGN\GameDatabase\a21af4e8-be4b-4cda-a6b6-534f9717391f\Sets\";
            const string fileName = "set.xml";

            var serializer = new XmlSerializer(typeof(OctgnSet));

            try
            {
                foreach (var dir in Directory.GetDirectories(rootPath))
                {
                    var filePath = Path.Combine(dir, fileName);
                    if (!File.Exists(filePath))
                        continue;

                    if (!filePath.Contains("dd60d037-3c50-4bd3-b994-2732b8218d7e"))
                        continue;

                    using (var stream = new FileStream(filePath, FileMode.Open))
                    //using (var reader = new StreamReader(stream))
                    {
                        var set = serializer.Deserialize(stream) as OctgnSet;

                        if (set != null)
                        {
                            Console.WriteLine("Valid Set: " + filePath.Substring(filePath.Length - 12, 12));
                            ProcessSet(set);
                        }
                    }
                    //serializer.d
                }

                Console.WriteLine("Press ENTER to close the importer");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static CardType GetCardType(string cardTypeProp)
        {
            var cardType = CardType.None;
            Enum.TryParse(cardTypeProp, out cardType);

            return cardType;
        }

        private static Sphere GetSphere(string sphereProp)
        {
            var sphere = Sphere.None;
            Enum.TryParse(sphereProp, out sphere);

            return sphere;
        }

        private static string GetText(string text)
        {
            var escapedQuote = new StringBuilder();
            escapedQuote.Append(@"\");
            escapedQuote.Append('"');

            return text
                .Replace("Ò", "Willpower")
                .Replace("Û", "Attack")
                .Replace("Ú", "Defense")
                .Replace("$", "Threat")
                .Replace("Ì", "Leadership")
                .Replace("Ï", "Tactics")
                .Replace("Ê", "Spirit")
                .Replace("Î", "Lore")
                .Replace("\r\n", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty)
                .Replace("\"", "'");
        }

        private static void ProcessCard(StringBuilder sb, OctgnCard card, string encounterSet, uint number)
        {
            var typeProp = card.Properties.Where(x => x.Name == "Type").FirstOrDefault();
            if (typeProp == null || typeProp.Value == null)
                return;

            var type = CardType.None;
            type = GetCardType(typeProp.Value);

            sb.AppendLine("            Cards.Add(new Card() {");
            sb.AppendFormat("                Title = \"{0}\",\r\n", card.Name);
            sb.AppendLine("                ImageType = ImageType.Png,");
            sb.AppendFormat("                Id = \"{0}\",\r\n", card.Id);
            
            foreach (var property in card.Properties)
            {
                switch (property.Name)
                {
                    case "Unique":
                        {
                            if (!string.IsNullOrEmpty(property.Value))
                                sb.AppendLine("                IsUnique = true,");
                            //else sb.AppendLine("                IsUnique = false,");
                        }
                        break;
                    case "Type":
                        {
                            if (type != CardType.None)
                                sb.AppendFormat("                CardType = CardType.{0},\r\n", type);
                        }
                        break;
                    case "Sphere":
                        {
                            var sphere = GetSphere(property.Value);

                            if (sphere != Sphere.None)
                                sb.AppendFormat("                Sphere = Sphere.{0},\r\n", sphere);
                        }
                        break;
                    case "Cost":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            if (type == CardType.Hero)
                            {
                                sb.AppendFormat("                ThreatCost = {0},\r\n", value);
                            }
                            else
                            {
                                sb.AppendFormat("                ResourceCost = {0},\r\n", value);
                            }
                        }
                        break;
                    case "Engagement Cost":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            sb.AppendFormat("                EngagementCost = {0},\r\n", value);
                        }
                        break;
                    case "Threat":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            sb.AppendFormat("                Threat = {0},\r\n", value);
                        }
                        break;
                    case "Willpower":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            sb.AppendFormat("                Willpower = {0},\r\n", value);
                        }
                        break;
                    case "Attack":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            sb.AppendFormat("                Attack = {0},\r\n", value);
                        }
                        break;
                    case "Defense":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            sb.AppendFormat("                Defense = {0},\r\n", value);
                        }
                        break;
                    case "Health":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            sb.AppendFormat("                HitPoints = {0},\r\n", value);
                        }
                        break;
                    case "Quest Points":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            sb.AppendFormat("                QuestPoints = {0},\r\n", value);
                        }
                        break;
                    case "Traits":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            string[] traits = property.Value.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            if (traits != null && traits.Length > 0)
                            {
                                var traitList = new StringBuilder();
                                for (var i = 0; i < traits.Length; i++)
                                {
                                    if (i > 0)
                                        traitList.Append(", ");

                                    traitList.AppendFormat("\"{0}.\"", traits[i]);
                                }

                                sb.Append("                Traits = new List<string>() { ");
                                sb.Append(traitList.ToString());
                                sb.AppendLine(" },"); 
                            }
                        }
                        break;
                    case "Keywords":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            string[] keywords = property.Value.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            if (keywords != null && keywords.Length > 0)
                            {
                                var keywordsList = new StringBuilder();
                                for (var i = 0; i < keywords.Length; i++)
                                {
                                    if (i > 0)
                                        keywordsList.Append(", ");

                                    keywordsList.AppendFormat("\"{0}.\"", keywords[i]);
                                }

                                sb.Append("                Keywords = new List<string>() { ");
                                sb.Append(keywordsList.ToString());
                                sb.AppendLine(" },");
                            }
                        }
                        break;
                    case "Text":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            sb.AppendFormat("                Text = \"{0}\",\r\n", GetText(property.Value));
                        }
                        break;
                    case "Shadow":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            sb.AppendFormat("                Shadow = \"{0}\",\r\n", GetText(property.Value));
                        }
                        break;
                    case "Encounter Set":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            sb.AppendFormat("                EncounterSet = \"{0}\",\r\n", property.Value);
                        }
                        break;
                    case "Victory Points":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            sb.AppendFormat("                VictoryPoints = {0},\r\n", value);
                        }
                        break;
                    case "Quantity":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            byte value = 0;
                            byte.TryParse(property.Value, out value);

                            sb.AppendFormat("                Quantity = {0},\r\n", value);
                        }
                        break;
                    case "Setup":
                        {
                            if (string.IsNullOrEmpty(property.Value))
                                continue;

                            sb.AppendFormat("                Setup = \"{0}\",\r\n", GetText(property.Value));
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown Property: " + property.Name);
                        break;
                }
            }

            sb.AppendFormat("                Number = {0}\r\n", number);
            //sb.AppendFormat("                EncounterSet = \"{0}\"", encounterSet);
            sb.AppendLine("            });");
        }

        private static void ProcessSet(OctgnSet set)
        {
            try
            {
                Console.WriteLine("Name: " + set.Name);

                if (set.Name == "Markers and Tokens") //|| set.Name.StartsWith("Custom Set"))
                {
                    Console.WriteLine();
                    return;
                }

                var sb = new StringBuilder();
                var outputFile = string.Format("{0}.cs", set.Name.Replace(' ', '_').Replace('-', '_').Replace(':', '_').Replace("'", string.Empty).Replace('ú', 'u').Replace('î', 'i').Replace("__", "_"));
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "cs", outputFile);
                var className = set.Name.Replace(" ", string.Empty).Replace("-", string.Empty).Replace(":", string.Empty).Replace("'", string.Empty).Replace('ú', 'u').Replace('î', 'i');

                Console.WriteLine("Path : " + path);
                Console.WriteLine("Class: " + className);
                Console.WriteLine();

                sb.AppendLine("using System;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine("using HallOfBeorn;");
                sb.AppendLine("using HallOfBeorn.Models;");
                sb.AppendLine();
                sb.AppendLine("namespace HallOfBeorn.Models.Sets");
                sb.AppendLine("{");
                sb.AppendFormat("    public class {0} : CardSet\r\n", className);
                sb.AppendLine("    {");
                sb.AppendLine("        protected override void Initialize()");
                sb.AppendLine("        {");

                uint number = 0;
                foreach (var card in set.Cards)
                {
                    number++;
                    ProcessCard(sb, card, set.Name, number);
                }

                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("}");

                File.WriteAllText(path, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not create class for set: " + set.Name);
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
