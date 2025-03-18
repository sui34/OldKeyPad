using System;

namespace OldPhonePad 
{
    public class OldPhoneKeys
    {
        //Simple UI for visual
        public static void visualKeyPad()
        {

            Console.WriteLine("Instruction : Press each number one or more times to select a letter." +
                                "Press 0 for space. " +
                                "Press * to delete previous letter and" +
                                " lastly press # to send. " +
                                "Here is visual key pad below ... ");

            Console.WriteLine("----------------------");

            Console.WriteLine("| .,1? | ABC  | DEF  |");
            Console.WriteLine("|  1   |  2   |  3   |");

            Console.WriteLine("----------------------");

            Console.WriteLine("| GHI  | JKL  | MNO  |");
            Console.WriteLine("|  4   |  5   |  6   |");

            Console.WriteLine("----------------------");

            Console.WriteLine("| PQRS | TUV  | WXYZ |");
            Console.WriteLine("|  7   |  8   |  9   |");

            Console.WriteLine("----------------------");

            Console.WriteLine("| [<-] | [ ]  | [->] |");
            Console.WriteLine("|  *   |  0   |  #   |");

            Console.WriteLine("----------------------");


        }

        //Map the keys digit to relative characters
        private static Dictionary<char, string> keys = new Dictionary<char, string>
        {
            {'0', " " }, // 0 key for Space 
            {'1', "&'(" }, // 1 key for &, ', ( 
            {'2', "abc" }, // 2 key for a, b, c 
            {'3', "def" }, // 3 key for d, e, f
            {'4', "ghi" }, // 4 key for g, h, i
            {'5', "jkl" }, // 5 key for j, k, l
            {'6', "mno" }, // 6 key for m, n, o
            {'7', "pqrs" }, // 7 key for p, q, r, s
            {'8', "tuv" }, // 8 key for t, u, v
            {'9', "wxyz" } // 9 key for w, x, y, z
        };

        public static string OldPhonePad(string input) 
        {
            //Input Validation
            if (string.IsNullOrEmpty(input) || !input.EndsWith("#"))
            {
                throw new ArgumentException("Input must be number, must not be empty and must end with '#'");
            }

            string result = "";
            char previousKey = '\0'; //Tracking the recent key 
            int pressKeyCount = 0; //Counting the presses of the same key

            foreach (char c in input)
            {
                //Handling Pause
                if (c == ' ')
                {
                    //Adding result from previous key if it exits 
                    addingKeys(ref result, previousKey, pressKeyCount);
                    //Handling reset tracking after pause
                    previousKey = '\0';
                    pressKeyCount = 0;
                    continue;
                }

                //Handling send
                if (c == '#')
                {
                    break;
                }

                // Handling backspace
                if (c == '*')
                {
                    if (result.Length > 0)
                    {
                        result = result.Substring(0, result.Length - 1);
                    }
                    //Handling reset tracking after backspace
                    previousKey = '\0';
                    pressKeyCount = 0;
                    continue;
                }

                //Handling digits
                if (char.IsDigit(c))
                {
                    if (c == previousKey)
                    {
                        //Handling same key press
                        pressKeyCount++;
                    }
                    else
                    {
                        //Handling different key press and adding previous key
                        addingKeys(ref result, previousKey, pressKeyCount);
                        previousKey = c;
                        pressKeyCount = 1;
                    }
                        
                }
            }
            //adding last key if the input ended without changing keys
            addingKeys(ref result, previousKey, pressKeyCount);
            return result;
          
        }
        //Adding keys to result
        private static void addingKeys(ref string result, char key, int count)
        {
            if (key != '\0' && count > 0 && keys.ContainsKey(key))
            {
                string letters = keys[key];
                if (letters.Length > 0)
                {
                    //Handling press count to generate result base on press count
                    int letterIndex = (count - 1) % letters.Length;
                    result += letters[letterIndex];
                }
            }
        }

        //Test Case
        public static void OldPhonePadTests()
        {
            Console.WriteLine("Running test cases...");

            //Testing basic input
            string testcase_1 = OldPhonePad("2#");
            Console.WriteLine($"Test case 1: Input '2#' -> Output '{testcase_1}' [Expected: 'a']");

            //Testing multiple characters
            string testcase_2 = OldPhonePad("2 22#");
            Console.WriteLine($"Test case 2: Input '2 22#' -> Output '{testcase_2}' [Expected: 'ab']");

            //Testing for pauses
            string testcase_3 = OldPhonePad("2 2#");
            Console.WriteLine($"Test case 3: Input '2 2#' -> Output '{testcase_3}' [Expected: 'aa']");

            //Testing for delete
            string testcase_4 = OldPhonePad("22*2#");
            Console.WriteLine($"Test case 4: Input '22*2#' -> Output '{testcase_4}' [Expected: 'a']");
            
            //Testing for actual word
            string testcase_5 = OldPhonePad("4433555 555666#");
            Console.WriteLine($"Test case 5: Input '4433555 555666#' -> Output '{testcase_5}' [Expected: 'hello']");

            //Testing for actual message
            string testcase_6 = OldPhonePad("84426655099966688033366677709996668877708444633#");
            Console.WriteLine($"Test case 5: Input '84426655099966688033366677709996668877708444633#' -> Output '{testcase_6}' [Expected: 'thank you for your time']");

            Console.WriteLine("Tests completed!");

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }

        public static void Main(string[] args)
        {
            OldPhonePadTests();
            visualKeyPad();
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter key pad digits :");
                    string input = Console.ReadLine();
                    //Handling key to end the program
                    if (input.ToLower() == "exit")
                    {
                        Console.WriteLine("Program has ended. Press any key to close.");
                        break;
                    }
                    string output = OldPhonePad(input);
                    Console.WriteLine("Output: " + output);
                    Console.WriteLine("If you want to exit, Type 'exit' :");
                }
                catch (ArgumentException ex)
                {
                    //Throw error if input doesn't end with '#'
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    //Catch any unexpected errors
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }
            }
        }
    }
}