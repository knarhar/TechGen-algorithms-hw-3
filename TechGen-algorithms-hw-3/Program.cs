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
        }
    }
}
