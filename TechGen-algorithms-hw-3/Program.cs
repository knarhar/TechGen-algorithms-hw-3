namespace TechGen_algorithms_hw_3
{
    internal class Program
    {
        #region TASK 1
        private static bool Matching(char chOpen, char chClose)
        {
            switch (chOpen)
            {
                case '(':
                    if (chClose == ')')
                        return true;
                    return false;
                case '[':
                    if (chClose == ']')
                        return true;
                    return false;
                case '{':
                    if (chClose == '}')
                        return true;
                    return false;
                default:
                    return false;
            }
        }

        static bool CheckBraces(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return false;
            }

            // my stack logic used for char elements
            char[] stack = new char[text.Length];
            int top = -1;

            foreach (char ch in text)
            {
                // save the opened braces, and check on closing ones
                if (ch == '(' || ch == '[' || ch == '{')
                {
                    stack[++top] = ch;
                }
                else
                {
                    if (top == -1)
                    {
                        return false;
                    }

                    char opened = stack[top--];

                    if (!Program.Matching(opened, ch))
                    {
                        return false;
                    }
                }
            }

            return top == -1;
        }

        #endregion TASK 1


        #region TASK 2

        // Print utility

        static void Print(int[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(arr[i, j] + " ");
                }

                Console.WriteLine();
            }
        }


        // -------------------------- Recursive Method ----------------------------

        public static void FillArray(int[,] arr, (int x, int y) point, int newValue)
        {
            int oldValue = arr[point.x, point.y];

            if (oldValue == newValue)
            {
                return;
            }

            FillRecursive(arr, point, oldValue, newValue);
        }

        private static void FillRecursive(
            int[,] arr,
            (int x, int y) point,
            int oldValue,
            int newValue
        )
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            if (point.x < 0 || point.x >= rows ||
                point.y < 0 || point.y >= cols)
            {
                return;
            }

            if (arr[point.x, point.y] != oldValue)
            {
                return;
            }

            arr[point.x, point.y] = newValue;

            FillRecursive(arr, (point.x - 1, point.y), oldValue, newValue); // top
            FillRecursive(arr, (point.x + 1, point.y), oldValue, newValue); // bottom
            FillRecursive(arr, (point.x, point.y - 1), oldValue, newValue); // left col
            FillRecursive(arr, (point.x, point.y + 1), oldValue, newValue); // right col
        }

        // -----------------------------------------------------------------------

        // ------------------------- Iterative Method ----------------------------

        public static void FillArrayIterative(int[,] arr, (int x, int y) point, int newVal)
        {
            int rows = arr.GetLength(0); 
            int cols = arr.GetLength(1);

            int oldVal = arr[point.x, point.y];

            if (oldVal == newVal)
            {
                return ;
            }

            // getting the cells (maximum cell count = rows * cols)
            (int x, int y)[] cells = new (int x, int y)[rows * cols];
            int top = -1;

            cells[++top] = point; // starting from given point

            while(top >= 0)
            {
                (int x, int y) p = cells[top--];

                if (p.x < 0 || p.x >= rows ||
                    p.y < 0 || p.y >= cols)
                {
                    continue;
                }

                if (arr[p.x, p.y] != oldVal)
                {
                    continue;
                }

                arr[p.x, p.y] = newVal;

                // push to cells the starting cell's neighbors
                cells[++top] = (p.x - 1, p.y);
                cells[++top] = (p.x + 1, p.y);
                cells[++top] = (p.x, p.y - 1);
                cells[++top] = (p.x, p.y + 1);
            }
        }



        // -----------------------------------------------------------------------

        #endregion TASK 2


        static void Main()
        {
            // --------- Test Cases for Task 1 ---------
            // ● "()" → true
            // ● "([])" → true
            // ● "([)]" → false
            // ● "{[()()]}" → true
            // ● "(((" → false
            Console.WriteLine(CheckBraces("()"));
            Console.WriteLine(CheckBraces("([])"));
            Console.WriteLine(CheckBraces("([)]"));
            Console.WriteLine(CheckBraces("{[()()]}"));
            Console.WriteLine(CheckBraces("((("));
            Console.WriteLine();

            // --------- Test Case for Task 2 ---------
            int[,] arr = {
                {1, 1, 1, 0, 0, 2, 2, 2},
                {1, 5, 5, 0, 0, 2, 3, 3},
                {1, 5, 5, 5, 0, 2, 3, 3},
                {0, 5, 5, 5, 0, 2, 2, 2},
                {0, 0, 0, 0, 0, 4, 4, 4},
                {7, 7, 0, 8, 8, 4, 6, 6},
                {7, 7, 0, 8, 8, 4, 6, 6},
                {7, 7, 0, 0, 0, 4, 6, 6}
            };

            Console.WriteLine("Before:");
            Print(arr);

            // X is at row=2, col=2
            //FillArray(arr, (2, 2), 9);
            FillArrayIterative(arr, (2, 2), 9);
            Console.WriteLine("\nAfter:");
            Print(arr);

        }
    }
}
