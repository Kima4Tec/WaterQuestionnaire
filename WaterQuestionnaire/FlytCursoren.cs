﻿using System;


namespace WaterQuestionnaire
{
    public class FlytCursoren
    {
        /// <summary>
        /// Boundary for cursor
        /// </summary>
        public static int Max { get; set; }

        /// <summary>
        /// Moving cursor up and down with boundaries
        /// </summary>
        /// <param name="explanation"></param>
        /// <param name="cursorLeft"></param>
        /// <param name="cursorTop"></param>
        public static void FlytOpogNed(string explanation, int cursorLeft, int cursorTop)
        {
            Max = 7;
            Console.SetCursorPosition(cursorLeft, cursorTop);

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (cursorTop < Max)
                    {
                        cursorTop++;
                    }

                }

                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (cursorTop > 5)
                    {
                        cursorTop--;
                    }
                }
                //User answers a question by pressing x by an answer. Check answer.
                if (key.KeyChar == 'x')
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    Console.Write("x");
                    if (cursorTop == QuestionHandler.Right)
                    {
                        Console.SetCursorPosition(4, 9);
                        Console.WriteLine("Det er rigtigt!");
                        Console.SetCursorPosition(0, 11);
                        Console.WriteLine($"{explanation}");
                        Console.ReadKey();
                        QuestionHandler.AntalRigtige++;
                        break;
                    }
                    else
                    {
                        Console.SetCursorPosition(4, 9);
                        Console.WriteLine("Det er forkert!");
                        Console.SetCursorPosition(0, 11);
                        Console.WriteLine($"{explanation}");
                        Console.ReadKey();
                        QuestionHandler.AntalForkerte++;
                        break;
                    }
                }

                Console.SetCursorPosition(cursorLeft, cursorTop);
            }
        }

    }
}
