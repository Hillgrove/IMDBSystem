namespace ConsoleUI.Helpers
{
    public static class ConsoleFormatter
    {
        public static void DisplayMenuHeader(string title = "Menu")
        {
            const int indent = 5;
            const int maxWidth = 70;
            const int availableWidth = maxWidth - indent;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(new string('=', maxWidth));

            var remaining = title;
            while (remaining.Length > 0)
            {
                int breakIndex = remaining.Length > availableWidth
                    ? remaining.LastIndexOf(' ', availableWidth)
                    : remaining.Length;

                if (breakIndex == -1 || breakIndex == 0)
                    breakIndex = availableWidth;

                string line = remaining.Substring(0, breakIndex).Trim();
                Console.WriteLine(new string(' ', indent) + line);
                remaining = remaining.Substring(breakIndex).TrimStart();
            }

            Console.WriteLine(new string('=', maxWidth));
            Console.WriteLine();
        }

        public static void PrintWrappedLine(string text, int maxWidth = 70, int indent = 5)
        {
            int labelWidth = text.IndexOf(':') + 2;
            int availableWidth = maxWidth - labelWidth - indent;

            if (text.Length <= maxWidth)
            {
                Console.WriteLine(new string(' ', indent) + text);
                return;
            }

            int breakIndex = text.LastIndexOf(' ', maxWidth);
            if (breakIndex == -1) breakIndex = maxWidth;
            Console.WriteLine(new string(' ', indent) + text.Substring(0, breakIndex));
            text = text.Substring(breakIndex).TrimStart();

            while (text.Length > 0)
            {
                breakIndex = text.Length > availableWidth ? text.LastIndexOf(' ', availableWidth) : text.Length;
                if (breakIndex == -1) breakIndex = availableWidth;
                Console.WriteLine(new string(' ', indent + labelWidth) + text.Substring(0, breakIndex).Trim());
                text = text.Substring(breakIndex).TrimStart();
            }
        }
    }
}
