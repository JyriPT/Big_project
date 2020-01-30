using System;

namespace big_project
{
    class Program
    {
        static void Main(string[] args)
        {
            //Aloitetaan tarkistamalla mitä ohjelman funktiota halutaan käyttää
            //Käyttäjä pidetään do while- loopin sisällä kunnes valitsee hyväksyttävän arvon, toistetaan ohjeistus jokaisen kertauksen yhteydessä
            //Käyttäjän syöte muutetaan string arvosta (userInput) int arvoon (userSelect), jotta sitä on helpompi käsitellä
            string userInput = String.Empty;
            int userSelect;
            do
            {
                Console.WriteLine("Please select function to use:");
                Console.WriteLine("1 to check validity of code");
                Console.WriteLine("2 to generate final digit of a code");
                Console.WriteLine("3 to generate multiple codes");

                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int i) == true)
                {
                    if (i == 1 || i == 2 || i == 3)
                    {
                        userSelect = i;
                        break;
                    }
                }

                Console.WriteLine("Invalid selection, must be number between 1 and 3");
                Console.WriteLine();

            } while (true);

            //Kutsutaan funktiota, jota käyttäjä pyysi userSelect arvoa hyödyntäen
            if (userSelect == 1)
            {
                CheckCode();

            } else if (userSelect == 2)
            {
                GenerateDigit();

            } else if (userSelect == 3)
            {
                Console.WriteLine("Numbers will be printed to .txt file.");
                Console.WriteLine();
                GenerateMany();
            }
        }

        //Funktio koodin tarkistamista varten
        static void CheckCode()
        {
            bool codeTrue = false;
            string userInput;
            string userCode = String.Empty;

            //Pyydetään käyttäjältä numeroa tarkistettavaksi
            Console.WriteLine("Please enter code:");
            userInput = Console.ReadLine().Trim();

            //Poistetaan käyttäjän antamasta syötteestä syötteen keskellä olevat välit
            if (userInput.IndexOf(" ") != -1)
            {
                do
                {
                    userInput = userInput.Remove(userInput.IndexOf(" "), 1);
                } while (userInput.IndexOf(" ") != -1);
            }
            

            //Muutetaan käyttäjän antama arvo int muotoon, jos ei onnistu niin koodi hylätään
            //Tällöin poistetaan ylimääräiset alkunollat
            if (int.TryParse(userInput, out int i) == true)
            {
                userCode = i.ToString();

            } else 
            {
                codeTrue = false;
            }

            //Tarkistetaan koodin pituus
            //Hylätään jos ei täsmää
            if (userCode.Length <= 20 && userCode.Length >= 4)
            {
                //Tarkistetaan koodin oikeellisuus
                char[] codeProcess = userCode.ToCharArray();
                //Otetaan annetun koodin tarkastusnumero talteen
                char numCheck = codeProcess[userCode.Length - 1];
                //Poistetaan annetusta koodista tarkastusnumero, math funkiota varten
                string process = String.Empty;
                foreach (char j in codeProcess)
                {
                    process += j.ToString();
                }
                process.Remove(0, 1);
                
                //Lasketaan annetun koding tarkistusnumero
                double mathCode = CalcCheck(process);

                //Verrataan alkuperäisen koodin tarkastusnumeroa oikeelliseen
                if (mathCode == char.GetNumericValue(numCheck))
                {
                    codeTrue = true;
                    
                }

            } else
            {
                codeTrue = false;
            }
            //Tulostetaan tarkistuksen lopputulos codeTrue arvon perusteella
            if (codeTrue == true)
            {
                Console.WriteLine($"Code is valid.");
                PrintOut(userInput);

            } else
            {
                Console.WriteLine($"Code is NOT a valid code.");
                PrintOut(userInput);
                
            }

        }

        //Funktio koodin täyden koodin tekemistä varten
        static void GenerateDigit()
        {
            string userInput;

            //Pyydetään käyttäjältä koodia, varmistetaan että se on oikean pituinen ja sisältää vain numeroita
            //Samalla poistetaan syötteestä turhat välit ja etunollat
            //Looppii avautuu kun käyttäjä antaa hyväksyttävän syötteen
            do
            {
                Console.WriteLine("Please provide code to complete:");
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int i) == true)
                {
                    userInput = i.ToString();

                    if (userInput.Length >= 3 && userInput.Length <= 19)
                    {
                        break;
                    }
                }

                Console.WriteLine("Invalid input, code must be 3-19 characters long and only contain numbers.");
                Console.WriteLine();

            } while (true);

            //Hankitaan koodille tarkistusluku, muotoillaan sopivaksi ja käytetään math() 
            //Käytetään uussia muuttujia alkuperäisen syötteen säilyttämiseksi, helpottaa tulostusta
            
            double numCheck = CalcCheck(userInput);

            //Lisätään tarkistusnumero annettuun koodiin ja tulostetaan
            userInput += numCheck.ToString();
            PrintOut(userInput);
            
        }

        //Funktio usean koodin tulostamista varten
        static void GenerateMany()
        {
            int count = 0;
            string userInput = String.Empty;

            //Pyydetään käyttäjältä tuotettavien koodien määrä ja koodin pätkä, josta käydä rakentamaan
            //Käyttäjä päästetään loopista ulos kun molemmat arvot ovat hyväksyttäviä.
            do
            {
                Console.WriteLine("How many codes do you wish to generate?");
                userInput = Console.ReadLine().Trim();

                if (int.TryParse(userInput, out count) == true)
                {
                    Console.WriteLine("Please provide a base to work from:");
                    userInput = Console.ReadLine().Trim();

                    if (int.TryParse(userInput, out int i) == true)
                    {
                        break;
                    }
                }

                Console.WriteLine("Error; Invalid input. Use only numbers, make sure base provided has 10 digits or less.");

            } while (true);

            //Varmistetaan että annettu aloituskohta on tarpeeksi pitkä
            if (userInput.Length < 3)
            {
                do
                {
                    userInput += 1;
                } while (userInput.Length < 3);
            }

            //Tulostetaan pyydetyt koodit, tallennetaan tekstitiedostoon "output" kansiossa
            string path = @"../../../../referencenumber.txt";
            System.IO.File.WriteAllText(path, String.Empty);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                int countProgress = 0;
                for (int j = count; j > 0; j--)
                {
                    if (countProgress == 0)
                    {
                        userInput += CalcCheck(userInput);
                        PrintOut(userInput);
                        file.WriteLine(userInput);
                        Console.WriteLine();

                        countProgress++;
                        userInput = userInput.Remove(userInput.Length - 1);
                        userInput += countProgress.ToString();
                    }
                    else
                    {
                        userInput += CalcCheck(userInput);
                        PrintOut(userInput);
                        file.WriteLine(userInput);
                        Console.WriteLine();

                        countProgress++;
                        userInput = userInput.Remove(userInput.Length - 2);

                        if (countProgress == 10)
                        {
                            userInput += 1;
                            countProgress = 0;
                        }

                        userInput += countProgress;
                    }

                }
            }

        }

        //Tarkistusluvun laskennan käsittelevä funktio
        //Palauttaa tarkistusnumeron annetulle koodille
        public static double CalcCheck(string userInput)
        {
            char[] codeProcess = userInput.ToCharArray();
            Array.Reverse(codeProcess);
            double mathCode;

            double mathTrack = 0;
            int counter = 0;
            int multiplier = 7;

            foreach (char number in codeProcess)
            {
                if (multiplier == 7)
                {
                    mathTrack += Char.GetNumericValue(codeProcess[counter]) * multiplier;
                }
                else if (multiplier == 3)
                {
                    mathTrack += Char.GetNumericValue(codeProcess[counter]) * multiplier;
                }
                else if (multiplier == 1)
                {
                    mathTrack += Char.GetNumericValue(codeProcess[counter]) * multiplier;
                }

                if (multiplier == 7)
                {
                    multiplier = 3;
                }
                else if (multiplier == 3)
                {
                    multiplier = 1;
                }
                else if (multiplier == 1)
                {
                    multiplier = 7;
                }
                counter++;
            }

            mathCode = RoundUp(mathTrack) - mathTrack;
            return mathCode;
        }
        //Luvun ylöspyöristämiseen käytetty funktio, tarvitaan tarkistusluvun luontia/tarkistusta varten
        static double RoundUp(double round)
        {
            if (round % 10 == 0)
            {
                return round;
            } else
            {
                return (10 - round % 10) + round;
            }
        }

        //Lopullisen koodin tulostusta varten, formatoi kirjoitusasun
        static void PrintOut(string code)
        {
            int length = code.Length;
            char[] printable = code.ToCharArray();
            int counter = 0;

            for (Math.DivRem(length, 5, out int i); i > 0; i--)
            {
                Console.Write(printable[counter].ToString());

                counter++;
            }

            do
            {
                Console.Write(" ");

                for (int j = 0; j < 5; j++)
                {
                    Console.Write(printable[counter].ToString());
                    counter++;
                }
            } while (counter != length);
        }
    }
}
