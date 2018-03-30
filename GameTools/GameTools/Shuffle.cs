using System;

namespace GameTools
{
    public static class Shuffle
    {
        /// <summary>
        /// Fisher-Yates shuffle implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] ShuffleArray<T>(T[] array)
        {
            Random random = new Random();

            for (int i = array.Length; i > 0; i--)
            {
                int index = random.Next(i);
                // Simple swap
                T a = array[index];
                array[index] = array[i - 1];
                array[i - 1] = a;
            }
            return array;
        }
    }
}
