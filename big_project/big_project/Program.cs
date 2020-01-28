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
                checkCode();

            } else if (userSelect == 2)
            {
                generateDigit();

            } else if (userSelect == 3)
            {
                generateMany();
            }
        }

        //Funktio koodin tarkistamista varten
        static void checkCode()
        {
            bool codeTrue = false;
            string userInput;
            string userCode = String.Empty;

            //Pyydetään käyttäjältä numeroa tarkistettavaksi
            Console.WriteLine("Please enter code:");
            userInput = Console.ReadLine();

            //Muutetaan käyttäjän antama arvo int muotoon, jos ei onnistu niin koodi hylätään
            //Tällöin poistetaan ylimääräiset välit ja alkunollat
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
                //Käännetään koodi toisin päin, otetaan tarkistusnumero talteen ja käydään kertomaan
                char[] codeProcess = userCode.ToCharArray();
                Array.Reverse(codeProcess);
                char numCheck = codeProcess[0];
                double mathCode = 0;

                double mathTrack = 0;
                int counter = 1;
                int multiplier = 7;

                for (int j = userCode.Length - 1; j > 0; j--)
                {
                    
                    if (multiplier == 7)
                    {
                        mathTrack += Char.GetNumericValue(codeProcess[counter]) * multiplier;
                    } else if (multiplier == 3)
                    {
                        mathTrack += Char.GetNumericValue(codeProcess[counter]) * multiplier;
                    } else if (multiplier == 1)
                    {
                        mathTrack += Char.GetNumericValue(codeProcess[counter]) * multiplier;
                    }

                    if (multiplier == 7)
                    {
                        multiplier = 3;
                    } else if (multiplier == 3)
                    {
                        multiplier = 1;
                    } else if (multiplier == 1)
                    {
                        multiplier = 7;
                    }
                    Console.WriteLine(mathTrack);
                    counter++;

                }

                mathCode = roundUp(mathTrack) - mathTrack;

                Console.WriteLine(mathCode);
                Console.WriteLine(numCheck);

                //Prosessoidaan kertolaskusta tullut arvo
                if (mathCode == char.GetNumericValue(numCheck))
                {
                    codeTrue = true;
                    
                }

            } else
            {
                codeTrue = false;
            }
            //REMEMBER TO DO FORMATTING
            //Tulostetaan tarkistuksen lopputulos codeTrue arvon perusteella
            if (codeTrue == true)
            {
                Console.WriteLine($"Code {userInput} is valid.");
            } else
            {
                Console.WriteLine($"Code {userInput} is not a valid code.");
            }

        }

        //Funktio koodin tarkistusluvun luomista varten
        static string generateDigit()
        {
            string giveBack = String.Empty;

            return giveBack;
        }

        //Funktio usean koodin tulostamista varten
        static void generateMany()
        {

        }

        //Luvun ylöspyöristämiseen käytetty funktio, tarvitaan tarkistusluvun luontia/tarkistusta varten
        static double roundUp(double round)
        {
            if (round % 10 == 0)
            {
                return round;
            } else
            {
                return (10 - round % 10) + round;
            }
        }
    }
}
