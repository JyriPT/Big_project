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
            userInput = Console.ReadLine().Trim();

            //Poistetaan käyttäjän antamasta syötteestä syötteen keskellä olevat välit
            do
            {
                userInput = userInput.Remove(userInput.IndexOf(" "), 1);

                if (userInput.IndexOf(" ") == -1)
                {
                    break;
                }
            } while (true);

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
                //Käännetään koodi toisin päin, otetaan tarkistusnumero talteen ja käydään kertomaan
                char[] codeProcess = userCode.ToCharArray();
                Array.Reverse(codeProcess);
                //Otetaan annetun koodin tarkastusnumero talteen
                char numCheck = codeProcess[0];
                //Poistetaan annetusta koodista tarkastusnumero, math funkiota varten
                string process = String.Empty;
                foreach (char j in codeProcess)
                {
                    process += j.ToString();
                }
                process.Remove(0, 1);
                codeProcess = process.ToCharArray();
                
                //Lasketaan annetun koding tarkistusnumero
                double mathCode = math(codeProcess);

                //Verrataan alkuperäisen koodin tarkastusnumeroa oikeelliseen
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
        static void generateDigit()
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
            char[] code = userInput.ToCharArray();
            Array.Reverse(code);
            double numCheck = math(code);

            //Lisätään tarkistusnumero annettuun koodiin ja tulostetaan
            userInput += numCheck.ToString();
            Console.WriteLine($"Your new complete code is {userInput}.");
            
        }

        //Funktio usean koodin tulostamista varten
        static void generateMany()
        {

        }

        //Tarkistusluvun laskennan käsittelevä funktio
        public static double math(char[] codeProcess)
        {
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

            mathCode = roundUp(mathTrack) - mathTrack;
            return mathCode;
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
