using System;
using System.IO;
using System.Text;


    public class globalVars {//contains method and variables used throughout the program
         public static string getPath() {
             string path;

            Start: Console.WriteLine("\nPlease type the path to your file");
            Console.WriteLine(" ");
            path = Console.ReadLine();
            if (File.Exists(path)) {

            // Read the file and display it line by line.
            string content = System.IO.File.ReadAllText(path);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPlain text output from file path");
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine(content);
            return path;
        } else {
            //Error message displayed if file is not found or path is typed incorrectly.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n************************************ WARNING ***********************************");
            Console.WriteLine("\n*********************** The path you entered is incorrect **********************");
            Console.ResetColor();
            Console.WriteLine("\nPlease press any key to try again.");
            Console.ReadKey();
            goto Start;
        }
    } /////// END OF globalVars /////

    class caesar {
        static void Main() //Start of program
        {
            // /////// MENU AND OTHER UI//////////
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n************************************ Welcome ***********************************");
            Console.WriteLine("\n************************************* Menu *************************************");
            Console.ResetColor();
            bool validAnswer = false;
            int userSelection = 0;

            do {
                Console.WriteLine("\n\n[1]  Encryption");
                Console.WriteLine("[2]  Decryption");
                Console.WriteLine("[3]  Frequency Analysis");
                Console.WriteLine("[4]  Quit Program");

                Console.Write("\nPlease choose an option 1-4:  ");

                bool isNumber = Int32.TryParse(Console.ReadLine(), out userSelection); //prevents exception if the user types letter.
                if (!isNumber) userSelection = -1;
                // Users menu
                switch (userSelection) {
                    case 1:
                        encryptText();
                        validAnswer = true;
                        break;
                    case 2:
                        validAnswer = true;
                        decryption();
                        break;
                    case 3:
                        validAnswer = true;
                        frequencyAnalysis();
                        break;
                    case 4:
                        validAnswer = true;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n************************************ WARNING ***********************************");
                        Console.WriteLine("\n****************** Your selection is invalid. Please try again *****************");
                        Console.ResetColor();
                        break;
                }

            } while (!validAnswer);
        } // ///////END OF MENU AND OTHER UI STUFF //////////

            // Decryption Method //
        static void decryption() {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n*********************************** Decryption *********************************");
            Console.ResetColor();
            //pulls getPath from varables class
            string path = globalVars.getPath();
            string fileContent = "";
            string encrypted_text = System.IO.File.ReadAllText(path); //String variable storestext from a file. reads text file.
            string decoded_text = " ";
            int shift = 0;
            char character = '0';
            encrypted_text = encrypted_text.ToUpper();

            char[] alphabet = new char[26] {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };

            Console.WriteLine("The encrypted text is \n{0}", encrypted_text); //Display the encrypted text

            for (int i = 0; i < alphabet.Length; i++) //loop displays 25 different shifts of decipher
            {
                decoded_text = "";
                foreach(char c in encrypted_text) {
                    character = c;

                    if (character == '\'' || character == ' ') continue;
                    if (character == '\n') continue;
                    shift = Array.IndexOf(alphabet, character) - i; // shift is the index of a character in array, Stores result in variable
                    if (shift <= 0) shift = shift + 26;
                    if (shift >= 26) shift = shift - 26;


                    decoded_text += alphabet[shift];
                }
                Console.WriteLine("\n\nShift {0} \n\n {1}", i + 1, decoded_text);
                fileContent += "Shift " + (i + 1).ToString() + "\r\n" + decoded_text + "\r\n";
            }
                // Save Decrypted Output to .TXT file - allows user to choose save path within filesystem.
            string filename;
            string savePath;
            string extension = ".txt";

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\nWhat do you want to name your file??");
            Console.ResetColor();
            filename = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Where would you like to save your file??");
            Console.ResetColor();
            savePath = Console.ReadLine();

            File.WriteAllText(savePath + filename + extension, fileContent);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("File Saved");
            Console.WriteLine(Console.Read());
        }
    } // ///////END OF DECRYPTION //////////


    /////////  Encryption Method ///////////

    static void encryptText() {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\n*********************************** Encryption *********************************");
        Console.ResetColor();
        try {
            Console.Write("\nEnter the text you wish to encrypt: ");
            string pt = Console.ReadLine();
            Console.Write("\nEnter your shift: ");
            int k = Convert.ToInt32(Console.ReadLine());
            caesar_cipher(k, pt);
           } 
            catch (Exception) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n********************* The value you entered is incorrect ***********************");
            Console.WriteLine("\n***************************** Press any key to quit ****************************");
            Console.ResetColor();
        }
    }           // caesar shift
    static void caesar_cipher(int key, string pt) {
        int size = pt.Length;
        char[] value = new char[size];
        char[] cipher = new char[size];
        for (int r = 0; r < size; r++) {
            value[r] = Convert.ToChar(pt.Substring(r, 1));
        }
        for (int re = 0; re < size; re++) {
            int count = 0;
            int a = Convert.ToInt32(value[re]);
            for (int y = 1; y <= key; y++) {
                if (count == 0) {
                    if (a == 90) {
                        a = 64;
                    } else if (a == 122) {
                        a = 96;
                    }
                    cipher[re] = Convert.ToChar(a + y);
                    count++;
                } else {
                    int b = Convert.ToInt32(cipher[re]);
                    if (b == 90) {
                        b = 64;
                    } else if (b == 122) {
                        b = 96;
                    }
                    cipher[re] = Convert.ToChar(b + 1);
                }
            }
        }
        string ciphertext = "";

        for (int p = 0; p < size; p++) {
            ciphertext = ciphertext + cipher[p].ToString();
        }
        Console.Write("\nEncrypted Text:  ");
        Console.WriteLine(ciphertext.ToUpper());

        string filename;
        string savePath;

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("\nWhat do you want to name your file??");
        Console.ResetColor();
        filename = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Where would you like to save your file??");
        Console.ResetColor();
        savePath = Console.ReadLine();

        File.WriteAllText(savePath + filename + ".txt", ciphertext);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("File Saved");
        Console.WriteLine(Console.Read());

        Console.ReadLine();
    }
    // ///////END OF ENCRYPTION //////////

    // /////// FREQUENCY ANALYSIS //////////
    static void frequencyAnalysis() {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\n******************************* Frequency Analysis *****************************");
        Console.ResetColor();
        //pulls getPath from varables class
        string path = globalVars.getPath();

        //// store frequencies array
        int[] c = new int[(int) char.MaxValue];

        //// Read text file from user input
        string s = File.ReadAllText(path);

        //// Iterate through each file
        foreach(char t in s) {
            //// increment table.
            c[(int) t]++;
        }

        // display all letters found.
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("\nFrequency");
        Console.ResetColor();
        for (int i = 0; i < (int) char.MaxValue; i++) {
            if (c[i] > 0 && char.IsLetterOrDigit((char) i)) {
                Console.WriteLine("Letter: {0}  Frequency: {1}", (char) i,
                c[i]);
            }
        }
    } // ///////END OF FREQUENCY ANALYSIS //////////
} // End of caesar class