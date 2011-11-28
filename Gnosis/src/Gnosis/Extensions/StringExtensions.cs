using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Gnosis
{
    public static class StringExtensions
    {
        #region MetaphoneData

        private class MetaphoneData
        {
            readonly StringBuilder _primary = new StringBuilder(5);
            readonly StringBuilder _secondary = new StringBuilder(5);


            #region Properties

            internal bool Alternative { get; set; }
            internal int PrimaryLength
            {
                get
                {
                    return _primary.Length;
                }
            }

            internal int SecondaryLength
            {
                get
                {
                    return _secondary.Length;
                }
            }

            #endregion


            internal void Add(string main)
            {
                if (main != null)
                {
                    _primary.Append(main);
                    _secondary.Append(main);
                }
            }

            internal void Add(string main, string alternative)
            {
                if (main != null)
                {
                    _primary.Append(main);
                }

                if (alternative != null)
                {
                    Alternative = true;
                    if (alternative.Trim().Length > 0)
                    {
                        _secondary.Append(alternative);
                    }
                }
                else
                {
                    if (main != null && main.Trim().Length > 0)
                    {
                        _secondary.Append(main);
                    }
                }
            }

            public override string ToString()
            {
                string ret = (Alternative ? _secondary : _primary).ToString();
                //only give back 4 char metaph
                if (ret.Length > 4)
                {
                    ret = ret.Substring(0, 4);
                }

                return ret;
            }
        }

        #endregion

        public static int ToHammingDistance(this string self, string value)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (value == null)
                throw new ArgumentNullException("value");

            if (self.Length != value.Length)
                return -1;

            var d = 0;
            for (var i = 0; i < self.Length; i++)
            {
                if (self[i] != value[i])
                    d++;
            }

            return d;
        }

        public static int ToLevenshteinDistance(this string self, string value)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (value == null)
                throw new ArgumentNullException("value");

            int n = self.Length;
            int m = value.Length;

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            int[,] d = new int[n + 1, m + 1];

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (value[j - 1] == self[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        public static string ToDoubleMetaphoneString(this string self)
        {
			MetaphoneData metaphoneData = new MetaphoneData();
			int current = 0;

			if (self.Length < 1) {
				return self;
			}
			int last = self.Length - 1; //zero based index

			string workingString = self.ToUpperInvariant() + "     ";

			bool isSlavoGermanic = (self.IndexOf('W') > -1) || (self.IndexOf('K') > -1) || (self.IndexOf("CZ", StringComparison.OrdinalIgnoreCase) > -1)
				|| (self.IndexOf("WITZ", StringComparison.OrdinalIgnoreCase) > -1);

			//skip these when at start of word
			if (workingString.StartsWith(StringComparison.OrdinalIgnoreCase, "GN", "KN", "PN", "WR", "PS")) {
				current += 1;
			}

			//Initial 'X' is pronounced 'Z' e.g. 'Xavier'
			if (workingString[0] == 'X') {
				metaphoneData.Add("S"); //'Z' maps to 'S'
				current += 1;
			}

			while ((metaphoneData.PrimaryLength < 4) || (metaphoneData.SecondaryLength < 4)) {
				if (current >= self.Length) {
					break;
				}

				switch (workingString[current]) {
					case 'A':
					case 'E':
					case 'I':
					case 'O':
					case 'U':
					case 'Y':
						if (current == 0) {
							//all init vowels now map to 'A'
							metaphoneData.Add("A");
						}
						current += 1;
						break;

					case 'B':
						//"-mb", e.g", "dumb", already skipped over...
						metaphoneData.Add("P");

						if (workingString[current + 1] == 'B') {
							current += 2;
						}
						else {
							current += 1;
						}
						break;

					case 'Ã':
						metaphoneData.Add("S");
						current += 1;
						break;

					case 'C':
						//various germanic
						if ((current > 1) && !workingString[current - 2].IsVowel() && StringAt(workingString, (current - 1), "ACH")
							&& ((workingString[current + 2] != 'I') && ((workingString[current + 2] != 'E') || StringAt(workingString, (current - 2), "BACHER", "MACHER")))) {
							metaphoneData.Add("K");
							current += 2;
							break;
						}

						//special case 'caesar'
						if ((current == 0) && StringAt(workingString, current, "CAESAR")) {
							metaphoneData.Add("S");
							current += 2;
							break;
						}

						//italian 'chianti'
						if (StringAt(workingString, current, "CHIA")) {
							metaphoneData.Add("K");
							current += 2;
							break;
						}

						if (StringAt(workingString, current, "CH")) {
							//find 'michael'
							if ((current > 0) && StringAt(workingString, current, "CHAE")) {
								metaphoneData.Add("K", "X");
								current += 2;
								break;
							}

							//greek roots e.g. 'chemistry', 'chorus'
							if ((current == 0) && (StringAt(workingString, (current + 1), "HARAC", "HARIS") || StringAt(workingString, (current + 1), "HOR", "HYM", "HIA", "HEM"))
								&& !StringAt(workingString, 0, "CHORE")) {
								metaphoneData.Add("K");
								current += 2;
								break;
							}

							//germanic, greek, or otherwise 'ch' for 'kh' sound
							if ((StringAt(workingString, 0, "VAN ", "VON ") || StringAt(workingString, 0, "SCH")) // 'architect but not 'arch', 'orchestra', 'orchid'
								|| StringAt(workingString, (current - 2), "ORCHES", "ARCHIT", "ORCHID") || StringAt(workingString, (current + 2), "T", "S")
									|| ((StringAt(workingString, (current - 1), "A", "O", "U", "E") || (current == 0)) //e.g., 'wachtler', 'wechsler', but not 'tichner'
										&& StringAt(workingString, (current + 2), "L", "R", "N", "M", "B", "H", "F", "V", "W", " "))) {
								metaphoneData.Add("K");
							}
							else {
								if (current > 0) {
									if (StringAt(workingString, 0, "MC")) {
										//e.g., "McHugh"
										metaphoneData.Add("K");
									}
									else {
										metaphoneData.Add("X", "K");
									}
								}
								else {
									metaphoneData.Add("X");
								}
							}
							current += 2;
							break;
						}
						//e.g, 'czerny'
						if (StringAt(workingString, current, "CZ") && !StringAt(workingString, (current - 2), "WICZ")) {
							metaphoneData.Add("S", "X");
							current += 2;
							break;
						}

						//e.g., 'focaccia'
						if (StringAt(workingString, (current + 1), "CIA")) {
							metaphoneData.Add("X");
							current += 3;
							break;
						}

						//double 'C', but not if e.g. 'McClellan'
						if (StringAt(workingString, current, "CC") && !((current == 1) && (workingString[0] == 'M'))) {
							//'bellocchio' but not 'bacchus'
							if (StringAt(workingString, (current + 2), "I", "E", "H") && !StringAt(workingString, (current + 2), "HU")) {
								//'accident', 'accede' 'succeed'
								if (((current == 1) && (workingString[current - 1] == 'A')) || StringAt(workingString, (current - 1), "UCCEE", "UCCES")) {
									metaphoneData.Add("KS");
								}
									//'bacci', 'bertucci', other italian
								else {
									metaphoneData.Add("X");
								}
								current += 3;
								break;
							}
							else {
//Pierce's rule
								metaphoneData.Add("K");
								current += 2;
								break;
							}
						}

						if (StringAt(workingString, current, "CK", "CG", "CQ")) {
							metaphoneData.Add("K");
							current += 2;
							break;
						}

						if (StringAt(workingString, current, "CI", "CE", "CY")) {
							//italian vs. english
							if (StringAt(workingString, current, "CIO", "CIE", "CIA")) {
								metaphoneData.Add("S", "X");
							}
							else {
								metaphoneData.Add("S");
							}
							current += 2;
							break;
						}

						//else
						metaphoneData.Add("K");

						//name sent in 'mac caffrey', 'mac gregor
						if (StringAt(workingString, (current + 1), " C", " Q", " G")) {
							current += 3;
						}
						else if (StringAt(workingString, (current + 1), "C", "K", "Q") && !StringAt(workingString, (current + 1), "CE", "CI")) {
							current += 2;
						}
						else {
							current += 1;
						}
						break;

					case 'D':
						if (StringAt(workingString, current, "DG")) {
							if (StringAt(workingString, (current + 2), "I", "E", "Y")) {
								//e.g. 'edge'
								metaphoneData.Add("J");
								current += 3;
								break;
							}
							else {
								//e.g. 'edgar'
								metaphoneData.Add("TK");
								current += 2;
								break;
							}
						}

						if (StringAt(workingString, current, "DT", "DD")) {
							metaphoneData.Add("T");
							current += 2;
							break;
						}

						//else
						metaphoneData.Add("T");
						current += 1;
						break;

					case 'F':
						if (workingString[current + 1] == 'F') {
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("F");
						break;

					case 'G':
						if (workingString[current + 1] == 'H') {
							if ((current > 0) && !workingString[current - 1].IsVowel()) {
								metaphoneData.Add("K");
								current += 2;
								break;
							}

							if (current < 3) {
								//'ghislane', ghiradelli
								if (current == 0) {
									if (workingString[current + 2] == 'I') {
										metaphoneData.Add("J");
									}
									else {
										metaphoneData.Add("K");
									}
									current += 2;
									break;
								}
							}
							//Parker's rule (with some further refinements) - e.g., 'hugh'
							if (((current > 1) && StringAt(workingString, (current - 2), "B", "H", "D")) //e.g., 'bough'
								|| ((current > 2) && StringAt(workingString, (current - 3), "B", "H", "D")) //e.g., 'broughton'
									|| ((current > 3) && StringAt(workingString, (current - 4), "B", "H"))) {
								current += 2;
								break;
							}
							else {
								//e.g., 'laugh', 'McLaughlin', 'cough', 'gough', 'rough', 'tough'
								if ((current > 2) && (workingString[current - 1] == 'U') && StringAt(workingString, (current - 3), "C", "G", "L", "R", "T")) {
									metaphoneData.Add("F");
								}
								else if ((current > 0) && workingString[current - 1] != 'I') {
									metaphoneData.Add("K");
								}

								current += 2;
								break;
							}
						}

						if (workingString[current + 1] == 'N') {
							if ((current == 1) && workingString[0].IsVowel() && !isSlavoGermanic) {
								metaphoneData.Add("KN", "N");
							}
							else
								//not e.g. 'cagney'
								if (!StringAt(workingString, (current + 2), "EY") && (workingString[current + 1] != 'Y') && !isSlavoGermanic) {
									metaphoneData.Add("N", "KN");
								}
								else {
									metaphoneData.Add("KN");
								}
							current += 2;
							break;
						}

						//'tagliaro'
						if (StringAt(workingString, (current + 1), "LI") && !isSlavoGermanic) {
							metaphoneData.Add("KL", "L");
							current += 2;
							break;
						}

						//-ges-,-gep-,-gel-, -gie- at beginning
						if ((current == 0)
							&& ((workingString[current + 1] == 'Y') || StringAt(workingString, (current + 1), "ES", "EP", "EB", "EL", "EY", "IB", "IL", "IN", "IE", "EI", "ER"))) {
							metaphoneData.Add("K", "J");
							current += 2;
							break;
						}

						// -ger-,  -gy-
						if ((StringAt(workingString, (current + 1), "ER") || (workingString[current + 1] == 'Y')) && !StringAt(workingString, 0, "DANGER", "RANGER", "MANGER")
							&& !StringAt(workingString, (current - 1), "E", "I") && !StringAt(workingString, (current - 1), "RGY", "OGY")) {
							metaphoneData.Add("K", "J");
							current += 2;
							break;
						}

						// italian e.g, 'biaggi'
						if (StringAt(workingString, (current + 1), "E", "I", "Y") || StringAt(workingString, (current - 1), "AGGI", "OGGI")) {
							//obvious germanic
							if ((StringAt(workingString, 0, "VAN ", "VON ") || StringAt(workingString, 0, "SCH")) || StringAt(workingString, (current + 1), "ET")) {
								metaphoneData.Add("K");
							}
							else
								//always soft if french ending
								if (StringAt(workingString, (current + 1), "IER ")) {
									metaphoneData.Add("J");
								}
								else {
									metaphoneData.Add("J", "K");
								}
							current += 2;
							break;
						}

						if (workingString[current + 1] == 'G') {
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("K");
						break;

					case 'H':
						//only keep if first & before vowel or btw. 2 vowels
						if (((current == 0) || workingString[current - 1].IsVowel()) && workingString[current + 1].IsVowel()) {
							metaphoneData.Add("H");
							current += 2;
						}
						else //also takes care of 'HH'
						{
							current += 1;
						}
						break;

					case 'J':
						//obvious spanish, 'jose', 'san jacinto'
						if (StringAt(workingString, current, "JOSE") || StringAt(workingString, 0, "SAN ")) {
							if (((current == 0) && (workingString[current + 4] == ' ')) || StringAt(workingString, 0, "SAN ")) {
								metaphoneData.Add("H");
							}
							else {
								metaphoneData.Add("J", "H");
							}
							current += 1;
							break;
						}

						if ((current == 0) && !StringAt(workingString, current, "JOSE")) {
							metaphoneData.Add("J", "A"); //Yankelovich/Jankelowicz
						}
						else
							//spanish pron. of e.g. 'bajador'
							if (workingString[current - 1].IsVowel() && !isSlavoGermanic && ((workingString[current + 1] == 'A') || (workingString[current + 1] == 'O'))) {
								metaphoneData.Add("J", "H");
							}
							else if (current == last) {
								metaphoneData.Add("J", " ");
							}
							else if (!StringAt(workingString, (current + 1), "L", "T", "K", "S", "N", "M", "B", "Z") && !StringAt(workingString, (current - 1), "S", "K", "L")) {
								metaphoneData.Add("J");
							}

						if (workingString[current + 1] == 'J') //it could happen!
						{
							current += 2;
						}
						else {
							current += 1;
						}
						break;

					case 'K':
						if (workingString[current + 1] == 'K') {
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("K");
						break;

					case 'L':
						if (workingString[current + 1] == 'L') {
							//spanish e.g. 'cabrillo', 'gallegos'
							if (((current == (self.Length - 3)) && StringAt(workingString, (current - 1), "ILLO", "ILLA", "ALLE"))
								|| ((StringAt(workingString, (last - 1), "AS", "OS") || StringAt(workingString, last, "A", "O")) && StringAt(workingString, (current - 1), "ALLE"))) {
								metaphoneData.Add("L", " ");
								current += 2;
								break;
							}
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("L");
						break;

					case 'M':
						if ((StringAt(workingString, (current - 1), "UMB") && (((current + 1) == last) || StringAt(workingString, (current + 2), "ER"))) //'dumb','thumb'
							|| (workingString[current + 1] == 'M')) {
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("M");
						break;

					case 'N':
						if (workingString[current + 1] == 'N') {
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("N");
						break;

					case 'Ð':
						current += 1;
						metaphoneData.Add("N");
						break;

					case 'P':
						if (workingString[current + 1] == 'H') {
							metaphoneData.Add("F");
							current += 2;
							break;
						}

						//also account for "campbell", "raspberry"
						if (StringAt(workingString, (current + 1), "P", "B")) {
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("P");
						break;

					case 'Q':
						if (workingString[current + 1] == 'Q') {
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("K");
						break;

					case 'R':
						//french e.g. 'rogier', but exclude 'hochmeier'
						if ((current == last) && !isSlavoGermanic && StringAt(workingString, (current - 2), "IE") && !StringAt(workingString, (current - 4), "ME", "MA")) {
							metaphoneData.Add("", "R");
						}
						else {
							metaphoneData.Add("R");
						}

						if (workingString[current + 1] == 'R') {
							current += 2;
						}
						else {
							current += 1;
						}
						break;

					case 'S':
						//special cases 'island', 'isle', 'carlisle', 'carlysle'
						if (StringAt(workingString, (current - 1), "ISL", "YSL")) {
							current += 1;
							break;
						}

						//special case 'sugar-'
						if ((current == 0) && StringAt(workingString, current, "SUGAR")) {
							metaphoneData.Add("X", "S");
							current += 1;
							break;
						}

						if (StringAt(workingString, current, "SH")) {
							//germanic
							if (StringAt(workingString, (current + 1), "HEIM", "HOEK", "HOLM", "HOLZ")) {
								metaphoneData.Add("S");
							}
							else {
								metaphoneData.Add("X");
							}
							current += 2;
							break;
						}

						//italian & armenian
						if (StringAt(workingString, current, "SIO", "SIA") || StringAt(workingString, current, "SIAN")) {
							if (!isSlavoGermanic) {
								metaphoneData.Add("S", "X");
							}
							else {
								metaphoneData.Add("S");
							}
							current += 3;
							break;
						}

						//german & anglicisations, e.g. 'smith' match 'schmidt', 'snider' match 'schneider'
						//also, -sz- in slavic language altho in hungarian it is pronounced 's'
						if (((current == 0) && StringAt(workingString, (current + 1), "M", "N", "L", "W")) || StringAt(workingString, (current + 1), "Z")) {
							metaphoneData.Add("S", "X");
							if (StringAt(workingString, (current + 1), "Z")) {
								current += 2;
							}
							else {
								current += 1;
							}
							break;
						}

						if (StringAt(workingString, current, "SC")) {
							//Schlesinger's rule
							if (workingString[current + 2] == 'H') {
								//dutch origin, e.g. 'school', 'schooner'
								if (StringAt(workingString, (current + 3), "OO", "ER", "EN", "UY", "ED", "EM")) {
									//'schermerhorn', 'schenker'
									if (StringAt(workingString, (current + 3), "ER", "EN")) {
										metaphoneData.Add("X", "SK");
									}
									else {
										metaphoneData.Add("SK");
									}
									current += 3;
									break;
								}
								else {
									if ((current == 0) && !workingString[3].IsVowel() && (workingString[3] != 'W')) {
										metaphoneData.Add("X", "S");
									}
									else {
										metaphoneData.Add("X");
									}
									current += 3;
									break;
								}
							}

							if (StringAt(workingString, (current + 2), "I", "E", "Y")) {
								metaphoneData.Add("S");
								current += 3;
								break;
							}
							//else
							metaphoneData.Add("SK");
							current += 3;
							break;
						}

						//french e.g. 'resnais', 'artois'
						if ((current == last) && StringAt(workingString, (current - 2), "AI", "OI")) {
							metaphoneData.Add("", "S");
						}
						else {
							metaphoneData.Add("S");
						}

						if (StringAt(workingString, (current + 1), "S", "Z")) {
							current += 2;
						}
						else {
							current += 1;
						}
						break;

					case 'T':
						if (StringAt(workingString, current, "TION")) {
							metaphoneData.Add("X");
							current += 3;
							break;
						}

						if (StringAt(workingString, current, "TIA", "TCH")) {
							metaphoneData.Add("X");
							current += 3;
							break;
						}

						if (StringAt(workingString, current, "TH") || StringAt(workingString, current, "TTH")) {
							//special case 'thomas', 'thames' or germanic
							if (StringAt(workingString, (current + 2), "OM", "AM") || StringAt(workingString, 0, "VAN ", "VON ") || StringAt(workingString, 0, "SCH")) {
								metaphoneData.Add("T");
							}
							else {
								metaphoneData.Add("O", "T");
							}
							current += 2;
							break;
						}

						if (StringAt(workingString, (current + 1), "T", "D")) {
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("T");
						break;

					case 'V':
						if (workingString[current + 1] == 'V') {
							current += 2;
						}
						else {
							current += 1;
						}
						metaphoneData.Add("F");
						break;

					case 'W':
						//can also be in middle of word
						if (StringAt(workingString, current, "WR")) {
							metaphoneData.Add("R");
							current += 2;
							break;
						}

						if ((current == 0) && (workingString[current + 1].IsVowel() || StringAt(workingString, current, "WH"))) {
							//Wasserman should match Vasserman
							if (workingString[current + 1].IsVowel()) {
								metaphoneData.Add("A", "F");
							}
							else {
								//need Uomo to match Womo
								metaphoneData.Add("A");
							}
						}

						//Arnow should match Arnoff
						if (((current == last) && workingString[current - 1].IsVowel()) || StringAt(workingString, (current - 1), "EWSKI", "EWSKY", "OWSKI", "OWSKY")
							|| StringAt(workingString, 0, "SCH")) {
							metaphoneData.Add("", "F");
							current += 1;
							break;
						}

						//polish e.g. 'filipowicz'
						if (StringAt(workingString, current, "WICZ", "WITZ")) {
							metaphoneData.Add("TS", "FX");
							current += 4;
							break;
						}

						//else skip it
						current += 1;
						break;

					case 'X':
						//french e.g. breaux
						if (!((current == last) && (StringAt(workingString, (current - 3), "IAU", "EAU") || StringAt(workingString, (current - 2), "AU", "OU")))) {
							metaphoneData.Add("KS");
						}

						if (StringAt(workingString, (current + 1), "C", "X")) {
							current += 2;
						}
						else {
							current += 1;
						}
						break;

					case 'Z':
						//chinese pinyin e.g. 'zhao'
						if (workingString[current + 1] == 'H') {
							metaphoneData.Add("J");
							current += 2;
							break;
						}
						else if (StringAt(workingString, (current + 1), "ZO", "ZI", "ZA") || (isSlavoGermanic && ((current > 0) && workingString[current - 1] != 'T'))) {
							metaphoneData.Add("S", "TS");
						}
						else {
							metaphoneData.Add("S");
						}

						if (workingString[current + 1] == 'Z') {
							current += 2;
						}
						else {
							current += 1;
						}
						break;

					default:
						current += 1;
						break;
				}
			}

			return metaphoneData.ToString();
		}

        public static string ToMd5Hash(this string self)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(self);
            return bytes.ToMd5Hash();
        }

        public static string ToSha1Hash(this string self)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(self);
            return bytes.ToSha1Hash();
        }

        public static string ToAmericanizedString(this string self)
        {
            if (string.IsNullOrEmpty(self))
                return string.Empty;

            var punctuation = new Dictionary<char, string> {
                {'!', string.Empty}, {'@', string.Empty}, {'#', string.Empty}, {'$', string.Empty}, {'%', string.Empty}, {'^', string.Empty}, {'&', string.Empty}, {'*', string.Empty}, {'(', string.Empty}, {')', string.Empty}, {'-', string.Empty}, {'_', string.Empty}, {'+', string.Empty}, {'=', string.Empty},
                {'{', string.Empty}, {'}', string.Empty}, {'[', string.Empty}, {']', string.Empty}, {'|', string.Empty}, {'\\', string.Empty}, {':', string.Empty}, {';', string.Empty}, {'\'', string.Empty}, {'"', string.Empty}, {'<', string.Empty}, {'>', string.Empty}, {',', string.Empty}, {'.', string.Empty},
                {'?', string.Empty}, {'/', string.Empty}, {'~', string.Empty}, {'`', string.Empty}
            };

            var charactersToNormalize = new Dictionary<char, string> {
                {'À', "A"}, {'Â', "A"}, {'Á', "A"}, {'Æ', "AE"}, {'Ǣ', "AE"}, {'Å', "A"},
                {'à', "A"}, {'â', "A"}, {'á', "A"}, {'æ', "AE"}, {'ǣ', "AE"}, {'ā', "A"},

                {'Ç', "C"},
                {'č', "C"},

                {'Ð', "D"},
                {'ð', "D"}, 

                {'È', "E"}, {'Ê', "E"}, {'Ë', "E"}, {'É', "E"},
                {'è', "E"}, {'ê', "E"}, {'ë', "E"}, {'ē', "E"}, {'é', "E"},

                {'Ğ', "G"}, {'ѓ', "G"},
                {'ģ', "G"}, {'ǧ', "G"},

                {'ȟ', "H"},

                {'Ï', "I"}, {'Î', "I"}, {'Í', "I"}, {'İ', "I"}, {'й', "I"}, {'ѝ', "I"},
                {'ï', "I"}, {'î', "I"}, {'ī', "I"}, {'í', "I"},
                
                {'ќ', "K"},
                {'ķ', "K"},

                {'ļ', "L"},

                {'Ñ', "N"},
                {'ñ', "N"}, {'ņ', "N"}, {'ŋ', "N"},

                {'Ô', "O"}, {'Ö', "O"}, {'Ó', "O"},
                {'ô', "O"}, {'ö', "O"}, {'ø', "O"}, {'ó', "O"}, {'õ', "O"},
                
                {'ŗ', "R"},

                {'Ş', "S"},
                {'š', "S"},

                {'Û', "U"}, {'Ù', "U"}, {'Ú', "U"}, {'Ü', "U"},
                {'û', "U"}, {'ù', "U"}, {'ū', "U"}, {'ú', "U"},
                
                {'ў', "U"},
                {'ž', "Z"}
            };

            var wordsToNormalize = new Dictionary<string, string> {
                {"THE", string.Empty}, {"A", string.Empty}, {"OF", string.Empty}, {"AT", string.Empty}, {"AND", string.Empty}, {"IN", string.Empty}, {"WITH", string.Empty}, {"BUT", string.Empty}, {"OR", string.Empty}, {"FOR", string.Empty}, {"NOR", string.Empty}, {"YET", string.Empty},
                {"ONE", "1"}, {"TWO", "2"}, {"THREE", "3"}, {"FOUR", "4"}, {"FIVE", "5"}, {"SIX", "6"}, {"SEVEN", "7"}, {"EIGHT", "8"}, {"NINE", "9"}, {"TEN", "10"},
                {"ELEVEN", "11"}, {"TWELVE", "12"}, {"THIRTEEN", "13"}, {"FOURTEEN", "14"}, {"FIFTEEN", "15"}, {"SIXTEEN", "16"}, {"SEVENTEEN", "17"}, {"EIGHTEEN", "18"}, {"NINETEEN", "19"}, {"TWENTY", "20"},
                {"HUNDRED", "00"}, {"THOUSAND", "000"}, {"MILLION", "000000"}, {"BILLION", "000000000"}, {"TRILLION", "000000000000"}
            };

            var result = new StringBuilder();
            var wordDelimiters = new string[] { " ", "\t", "\r\n", "\n" };
            var words = self.Split(wordDelimiters, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                var key = word.Trim().ToUpper();
                var noPunctuation = new StringBuilder();
                foreach (var character in key.ToCharArray())
                {
                    if (punctuation.ContainsKey(character))
                        noPunctuation.Append(punctuation[character]);
                    else
                        noPunctuation.Append(character);
                }

                key = noPunctuation.ToString();
                if (wordsToNormalize.ContainsKey(key))
                    key = wordsToNormalize[key];

                var normalized = new StringBuilder();
                foreach (var character in key.ToCharArray())
                {
                    if (charactersToNormalize.ContainsKey(character))
                        normalized.Append(charactersToNormalize[character]);
                    else
                        normalized.Append(character);
                }

                result.Append(normalized.ToString());
            }

            return result.ToString();
        }

        public static DateTime ToRfc822DateTime(this string self)
        {
            var date = DateTime.MinValue;
            Time.Rfc822DateTime.TryParse(self, out date);
            return date;
        }

        public static bool IsMixedAlphaNumeric(this string self)
        {
            if (self == null || self.Length < 2)
                return false;

            var hasAlpha = false;
            var hasNumeric = false;

            foreach (var c in self.ToCharArray())
            {
                if (Char.IsLetter(c))
                    hasAlpha = true;
                if (Char.IsDigit(c))
                    hasNumeric = true;
            }

            return hasAlpha && hasNumeric;
        }

        public static string RemoveQuotes(this string self)
        {
            if (string.IsNullOrEmpty(self))
                return self;

            if (self.StartsWith("'") && self.EndsWith("'"))
                return self.Replace("'", string.Empty);
            else if (self.StartsWith("\"") && self.EndsWith("\""))
                return self.Replace("\"", string.Empty);

            return self;
        }

        public static bool StartsWith(this string self, StringComparison comparison, params string[] strings)
        {
            foreach (string str in strings)
            {
                if (self.StartsWith(str, comparison))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool StringAt(this string self, int startIndex, params string[] strings)
        {
            if (startIndex < 0)
            {
                startIndex = 0;
            }

            foreach (string str in strings)
            {
                if (self.IndexOf(str, startIndex, StringComparison.OrdinalIgnoreCase) >= startIndex)
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<string> ToNames(this string self)
        {
            if (string.IsNullOrEmpty(self))
                return new List<string> { string.Empty };

            return self.Split(new string[]{"; "}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> ToWords(this string self)
        {
            if (string.IsNullOrEmpty(self))
                return new List<string> { string.Empty };

            //'-', ',', ';', '.', 
            return self.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ToXmlEscapedString(this string self)
        {
            if (self == null)
                return null;

            return self
                .Replace("&", "&amp;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");

        }

        public static byte[] ToHexByteArray(this string self)
        {
            var length = self.Length;
            var bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
                bytes[i / 2] = Convert.ToByte(self.Substring(i, 2), 16);
            
            return bytes;
        }

        public static bool TryParseUri(this string self, out Uri result)
        {
            if (self == null)
            {
                result = null;
                return false;
            }

            try
            {
                var uri = new Uri(self, UriKind.RelativeOrAbsolute);
                result = uri;
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public static string ElideString(this string self, int halfSize)
        {
            if (self == null)
                return null;

            var max = halfSize * 2;
            var length = self.Length;

            if (length <= max)
                return self;

            return string.Format("{0}...{1}", self.Substring(0, halfSize), self.Substring(length - halfSize, halfSize));
        }

        public static string RemoveNonAlphaNumerics(this string self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            var regex = new System.Text.RegularExpressions.Regex("[^a-zA-Z 0-9]");
            return regex.Replace(self, string.Empty);
        }

        public static string ToUrlEscapedString(this string self)
        {
            if (self == null)
                throw new ArgumentNullException("self");

            return System.Web.HttpUtility.UrlEncode(self);
        }
    }
}
