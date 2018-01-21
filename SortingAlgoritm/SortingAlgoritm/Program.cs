using System;

namespace SortingAlgoritm
{
    class Program
    {
        static void Main(string[] args)
        {
            const int max = 500;
            double[] arr = new double[max];
            double[] arr2 = new double[max];
            int nBulb;
            int nQuick;
            int nB = max * max;
            var nQ = max*Math.Log(max);

            //заполняем массив случайными числами
            Random rd = new Random();
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i] = rd.Next(1, 1001);
                arr2[i] = arr[i];
            }
            
            Console.WriteLine("The array before sorting:");
            foreach (double x in arr)
            {
                Console.Write(x + " ");
            }
            
            //сортировка
            nQuick = Sorting.SortingQuick(arr, 0, arr.Length - 1);
            nBulb = Sorting.SortingBulb(arr2);


            Console.WriteLine("\n\nnQuick: N = " + nQuick + "\n\nThe array after sorting:");
            Console.WriteLine("\nРасчетная сложность nlog(n) = " + nQ);
            foreach (double x in arr)
            {
                Console.Write(x + " ");
            }

            Console.WriteLine("\n");

            Console.WriteLine("\nBulb: N = " + nBulb + "\n\nThe array after sorting:");
            Console.WriteLine("\nРасчетная сложность N^2 = " + nB);
            foreach (double x in arr2)
            {
                Console.Write(x + " ");
            }

            Console.WriteLine("\n\nPress the <Enter> key");


            Console.WriteLine("\n\nnQuick: N = " + nQuick);
            Console.WriteLine("\nРасчетная сложность nlog(n) = " + nQ);

            Console.WriteLine("\nBulb: N = " + nBulb);
            Console.WriteLine("\nРасчетная сложность N^2 = " + nB);

            Console.ReadLine();

        }
        
        // Из массива выбирается элемент arr[i]. 
        // в качестве этого элемента берется центральный элемент массива (можно брать случайный)
        // Остальные элементы распределяются таким образом, чтобы слева от arr[i] оказались все элементы, меньшие или равные arr[i]. 
        // Элементы, большие или равные arr[i], помещаются справа
        class Sorting
        {
            public static int NQuick { get; set; }
            public static int NBulb { get; set; }

            public static int SortingQuick(double[] arr, long first, long last)
            {
                long indexMidle = (last - first) / 2 + first;
                double valueMidle = arr[indexMidle];
                double temp;
                long i = first;
                long j = last;
                
                // цикл по всем элементам от краев к середине 
                while (i <= j)
                {
                    // ищем индекс i левого элемента, большего valueMidle 
                    while (arr[i] < valueMidle && i <= last)
                    {
                        ++i;
                        NQuick++;
                    }

                    // ищем индекс j правого элемента, меньшего valueMidle 
                    while (arr[j] > valueMidle && j >= first)
                    {
                        --j;
                        NQuick++;
                    }

                    // меняем местами найденные элементы 
                    // перекидываем большее влево от середины, а меньшее вправо
                    if (i <= j)
                    {
                        temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                        ++i; --j;
       //                 NQuick++;
       //                 NQuick++;
                    }
                }

                // если i и j встертились и не дошли до краев, то рекурсивно повторяем для 2-х половинок, где индексы встретились 
                if (j > first) SortingQuick(arr, first, j);
                if (i < last) SortingQuick(arr, i, last);

                return NQuick;
            }

            public static int SortingBulb(double[] arr2)
            {
                bool swapped;
                double temp;

                do
                {
                    swapped = false;
                    for (int i = 1; i < arr2.Length; i++)
                    {
                        NBulb++;
                        if (arr2[i - 1] > arr2[i])
                        {
                            temp = arr2[i - 1];
                            arr2[i-1] = arr2[i];
                            arr2[i] = temp;
                            swapped = true;
         //                   NBulb++;
         //                   NBulb++;
                        }
                    }
                } while (swapped != false);

                return NBulb;
            }
        }
    }
}
