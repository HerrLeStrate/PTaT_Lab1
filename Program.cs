using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTaT_Lab1
{
    class Program
    {
        private static int ACE = 1;
        private static Boolean DEBUG = true;
        private static float VERSION = 1.0f;
        private static int SIZE_CARDS = 36;
        private static int MAX_SUM = 21;

        public static int getInt()
        {
            string input = Console.ReadLine();
            int result;
            if (!Int32.TryParse(input, out result))
            {
                result = 0;
            }
            return result;
        }

        public static Boolean inBorders(int value,int[] data)
        {
            return data[value] != 0;
        }

        public static int[] generateCardValues()
        {
            int[] cards = new int[Math.Max(ACE, 11)+1];
            for (int i = 0; i < cards.Length; i++) cards[i] = 0;

            if (SIZE_CARDS == 52)
            {
                for (int i = 2; i <= 5; i++)
                {
                    cards[i] += 4;
                }
            }

            /**
             * By default
             */
            for (int i = 6; i <= 10; i++) cards[i] += 4;
            for (int i = 2; i <= 4; i++) cards[i] += 4;
            cards[ACE] += 4;

            return cards;
        }

        public static int[] generateCards()
        {
            int[] cards = new int[SIZE_CARDS];

            int[] cardValues = generateCardValues();

            Random rnd = new Random();
            for (int i = 0; i < SIZE_CARDS; i++)
            {
                int value = rnd.Next(Math.Max(11, ACE) + 1);
                while (!inBorders(value,cardValues)) value = rnd.Next(Math.Max(11, ACE) + 1);
                cards[i] = value;
                cardValues[value]--;
            }

            if (DEBUG)
            {
                Console.Write("[DEBUG]");
                for(int i=0;i<cards.Length;i++) Console.Write(cards[i] + " ");
                Console.WriteLine("");
            }

            return cards;
        }

        public static Boolean notAcceptable(string cmd)
        {
            return (cmd != "y" && cmd != "n");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день, это игра 21. Версия: " + VERSION);

            Console.WriteLine("Размерность колоды: ");
            int temp_size = getInt();
            if (temp_size != 36 && temp_size != 52)
                Console.WriteLine("Некорректное число. В этой игре размерность колоды будет " + SIZE_CARDS);
            else SIZE_CARDS = temp_size;

            Console.WriteLine("Туз засчитывается за ");
            int temp_ace = getInt();
            if (temp_ace != 1 && temp_ace != 11)
                Console.WriteLine("Некорректное число. В этой игре туз засчитывается за "+ACE+" очко.");
            else ACE = temp_ace;

            int[] cards = generateCards();
            Boolean endOfGame = false;
            int cardsInHand = 0;
            int inHand = 0;

            while (!endOfGame)
            {
                Console.Write("Цена карты: " + cards[cardsInHand]);
                inHand += cards[cardsInHand];
                Console.WriteLine(". Ваша сумма: " + inHand);

                if (inHand > MAX_SUM)
                {
                    Console.WriteLine("Перебор. Вы проиграли");
                    Console.Read();
                    return;
                }

                if(inHand == MAX_SUM)
                {
                    Console.WriteLine("Победа, вы набрали ровно " + MAX_SUM);
                    Console.Read();
                    return;
                }

                Console.WriteLine("Ещё? y/n");
                string cmd = Console.ReadLine();
                cardsInHand++;

                while (notAcceptable(cmd))
                {
                    Console.WriteLine("Непонятная команда. Введите y или n");
                    cmd = Console.ReadLine();
                }

                if (cmd == "n") endOfGame = true;

            }
            Console.WriteLine("Вы закончили игру со счётом " + inHand + ". Количество взятых карт: " + cardsInHand);
            Console.Read();
            return;
        }

    }
}
