using GameTools;
using System;
using System.Collections.Generic;

namespace RandomTester
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;

            while (run)
            {
                bool validNumberEntered = false;
                int nRuns = 0;

                while (validNumberEntered == false)
                {
                    Console.Write("Number of times to run the simulation? ");
                    string runs = Console.ReadLine();
                    validNumberEntered = int.TryParse(runs, out nRuns);

                    if (validNumberEntered == false)
                    {
                        Console.WriteLine("Invalid number entered, press any key to continue..");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

                List<int> rollsFour = new List<int>();
                List<int> rollsSix = new List<int>();
                List<int> rollsEight = new List<int>();
                List<int> rollsTen = new List<int>();
                List<int> rollsTwelve = new List<int>();
                List<int> rollsTwenty = new List<int>();

                int[] arrMaxValues = new[] {
                (int)Dice.DieType.d4,
                (int)Dice.DieType.d6,
                (int)Dice.DieType.d8,
                (int)Dice.DieType.d10,
                (int)Dice.DieType.d12,
                (int)Dice.DieType.d20 };

                for (int i = 0; i < nRuns; i++)
                {
                    int[] rolls = Dice.Roll(arrMaxValues);

                    int four = rolls[0];
                    int six = rolls[1];
                    int eight = rolls[2];
                    int ten = rolls[3];
                    int twelve = rolls[4];
                    int twenty = rolls[5];

                    rollsFour.Add(four);
                    rollsSix.Add(six);
                    rollsEight.Add(eight);
                    rollsTen.Add(ten);
                    rollsTwelve.Add(twelve);
                    rollsTwenty.Add(twenty);

                    Console.WriteLine($"Roll #{i + 1}:\td4:{four}\td6:{six}\td8:{eight}\td10:{ten}\td12:{twelve}\td20:{twenty}");
                }

                string finalStats = "";
                finalStats += RollStats(rollsFour, Dice.DieType.d4) + TAB;
                finalStats += RollStats(rollsSix, Dice.DieType.d6) + TAB;
                finalStats += RollStats(rollsEight, Dice.DieType.d8) + TAB;
                finalStats += RollStats(rollsTen, Dice.DieType.d10) + TAB;
                finalStats += RollStats(rollsTwelve, Dice.DieType.d12) + TAB;
                finalStats += RollStats(rollsTwenty, Dice.DieType.d20);

                Console.WriteLine(finalStats);
                Console.Write("\nRerun y/n? ");
                ConsoleKeyInfo key = Console.ReadKey(true);
                run = key.Key == ConsoleKey.Y;
                Console.Clear();
            }
        }

        const string ROLL_STAT_FORMAT = "d{0}:{1} {2}% {3}/{4}";
        const string TAB = "\t";
        /// <summary>
        /// Returns a formatted string of stats for the list of rolls provided. 
        /// d{0}: the type of die (six-sided, eight-sided, etc)
        /// {1} the roll which occurred most often.
        /// {2} the ratio of this roll to total rolls (as percentage)
        /// {3} frequency of this roll
        /// {4} total number of all rolls
        /// </summary>
        /// <param name="rolls"></param>
        /// <param name="diceType"></param>
        /// <returns>"d{0}:{1} {2}% {3}/{4}"</returns>
        private static string RollStats(List<int> rolls, Dice.DieType diceType)
        {
            var stats = OccursMost(rolls, diceType);
            int mostCommonRoll = stats.Item1;
            int rollFrequency = stats.Item2;
            var frequencyPercentage = (double)rollFrequency / rolls.Count * 100.0;

            return string.Format(ROLL_STAT_FORMAT, (int)diceType, mostCommonRoll, frequencyPercentage.ToString("#.##"), rollFrequency, rolls.Count);
        }
        /// <summary>
        /// Determine which roll of the die occured most often and its frequency in the given list.
        /// </summary>
        /// <param name="rolls"></param>
        /// <param name="diceType"></param>
        /// <returns>Item1: most often occurring roll. Item2: frequency of roll</returns>
        private static Tuple<int, int> OccursMost(List<int> rolls, Dice.DieType diceType)
        {
            //
            // isolate the distinct values in a given list to prevent unnecessary loops
            //
            List<int> distinctRolls = new List<int>();

            for (int i = 0; i < rolls.Count; i++)
                if (!distinctRolls.Contains(rolls[i]))
                    distinctRolls.Add(rolls[i]);

            int mostOftenOccurringRoll = 0;
            int mostOftenOccurringRollFrequency = 0;

            //
            // count the occurrences of each roll, one-by-one. 
            // if the next roll occurred more than the previous roll,
            // this new roll is set as mostOftenOccurringRoll, along with how often the roll occurred.
            //
            for (int i = 0; i < distinctRolls.Count; i++)
            {
                int currentRoll = distinctRolls[i];
                int currentRollFrequency = 0;

                for (int j = 0; j < rolls.Count; j++)
                {
                    if (rolls[j] == currentRoll)
                        currentRollFrequency++;
                }

                if (currentRollFrequency > mostOftenOccurringRollFrequency)
                {
                    mostOftenOccurringRoll = currentRoll;
                    mostOftenOccurringRollFrequency = currentRollFrequency;
                }
            }
            return new Tuple<int, int>(mostOftenOccurringRoll, mostOftenOccurringRollFrequency);
        }
    }
}
