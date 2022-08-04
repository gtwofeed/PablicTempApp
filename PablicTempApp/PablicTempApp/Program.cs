internal class Program
{
    internal static void Main_Task()
    {
        int count = int.Parse(Console.ReadLine());
        bool[] resultList = new bool[count];
        for (int step = 0; step < count; step++)
        {
            int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int y = input[0]; // строки
            int x = input[1]; // столбцы

            char[,] chPole = new char[y + 2, x + 4];
            bool[,] boolPole = new bool[y + 2, x + 4];

            // собираем матрицу
            for (int i = 1; i < chPole.GetUpperBound(0); i++)
            {
                string line = Console.ReadLine();
                for (int j = 0, add = 2; j < line.Length; j++)
                {
                    if (line[j] != '.') chPole[i, add++] = line[j];
                    else chPole[i, add++] = '\0';
                }
            }

            int[,] nawList = new int[35, 2];
            int[,] nextList = new int[35, 2];
            nawList[0, 0] = 1;
            nawList[0, 1] = 2;// точка старта метода проверки
            bool[] chekChar = new bool[100];
            bool chekResult = true;
            int chekint = 0;
            bool chekbool = false;

            for (int i = 1; i < boolPole.GetUpperBound(0); i++)
            {
                for (int j = 2; j < boolPole.GetUpperBound(1); j++)
                {
                    chekint = chPole[i, j]; //чек значений
                    chekbool = boolPole[i, j]; // чек значений
                    if (!boolPole[i, j] && chPole[i, j] != 0)
                    {
                        boolPole[i, j] = true;
                        if (chekChar[chPole[i, j]])
                        {
                            chekResult = false;
                            break;
                        }
                        else
                        {
                            nawList[0, 0] = i;
                            nawList[0, 1] = j;
                        }
                        while (nawList[0, 0] != 0)
                        {
                            // сбор удачных ячеек в некст массив
                            ChekGex(nawList, nextList, chPole, boolPole); // метод проверки по кругу
                            ArrayMoving(nextList, nawList);
                        }
                        chekChar[chPole[i, j]] = true;
                    }
                }
                if (!chekResult) break;
            }
            resultList[step] = chekResult;

        }
        foreach (var item in resultList)
        {
            Console.WriteLine(item ? "YES" : "NO");
        }
    }
    internal static void ChekGex(int[,] nawList, int[,] nextList, char[,] chPole, bool[,] boolPole)
    {
        for (int i = 0, inew = 0; nawList[i, 0] != 0; i++)
        {
            int xFocus = nawList[i, 1];
            int yFocus = nawList[i, 0];
            char chek = chPole[yFocus, xFocus];
            if (chek == chPole[yFocus, xFocus + 2] && !boolPole[yFocus, xFocus + 2])
            {
                boolPole[yFocus, xFocus + 2] = true;
                nextList[inew, 0] = yFocus;
                nextList[inew++, 1] = xFocus + 2;
            }
            if (chek == chPole[yFocus, xFocus - 2] && !boolPole[yFocus, xFocus - 2])
            {
                boolPole[yFocus, xFocus - 2] = true;
                nextList[inew, 0] = yFocus;
                nextList[inew++, 1] = xFocus - 2;
            }
            if (chek == chPole[yFocus + 1, xFocus + 1] && !boolPole[yFocus + 1, xFocus + 1])
            {
                boolPole[yFocus + 1, xFocus + 1] = true;
                nextList[inew, 0] = yFocus + 1;
                nextList[inew++, 1] = xFocus + 1;
            }
            if (chek == chPole[yFocus + 1, xFocus - 1] && !boolPole[yFocus + 1, xFocus - 1])
            {
                boolPole[yFocus + 1, xFocus - 1] = true;
                nextList[inew, 0] = yFocus + 1;
                nextList[inew++, 1] = xFocus - 1;
            }
            if (chek == chPole[yFocus - 1, xFocus + 1] && !boolPole[yFocus - 1, xFocus + 1])
            {
                boolPole[yFocus - 1, xFocus + 1] = true;
                nextList[inew, 0] = yFocus - 1;
                nextList[inew++, 1] = xFocus + 1;
            }
            if (chek == chPole[yFocus - 1, xFocus - 1] && !boolPole[yFocus - 1, xFocus - 1])
            {
                boolPole[yFocus - 1, xFocus - 1] = true;
                nextList[inew, 0] = yFocus - 1;
                nextList[inew++, 1] = xFocus - 1;
            }
        }
    }
    internal static void ArrayMoving(int[,] nextList, int[,] nawList)
    {
        bool chek = false;
        for (int y = 0; y <= nextList.GetUpperBound(0); y++)
        {
            for (int x = 0; x <= nextList.GetUpperBound(1); x++)
            {
                if (nextList[y, x] == 0 && nawList[y, x] == 0)
                {
                    chek = true;
                    break;
                }
                nawList[y, x] = nextList[y, x];
                nextList[y, x] = 0;
            }
            if (chek) break;
        }
    }

}