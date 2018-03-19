using System;

namespace GameTools
{
    public static class Dice
    {
        private static Random _dice;
        public static int Roll(int maxValue)
        {
            if (_dice == null)
                _dice = new Random();

            return _dice.Next(1, maxValue + 1);
        }
        public static int[] Roll(params int[] maxValues)
        {
            int size = maxValues.Length;
            int[] values = new int[size];

            for (int i = 0; i < size; i++)
            {
                values[i] = Roll(maxValues[i]);
            }
            return values;
        }

        public enum DieType
        {
            d4 = 4,
            d6 = 6,
            d8 = 8,
            d10 = 10,
            d12 = 12,
            d20 = 20
        }
    }
}
