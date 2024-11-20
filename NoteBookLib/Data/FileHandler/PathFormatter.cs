using System.Text.RegularExpressions;

namespace NoteBookLib.Data.FileHandler
{
    public partial interface IPathFormatter
    {

        string FormatRepitedPath(string path);

        public partial class Base : IPathFormatter
        {
            public string FormatRepitedPath(string path)
            {

                string[] pathParts = path.Split('.');

                int lastNamePart = pathParts.Length - 1;

                if (FilePattern().IsMatch(path))
                {
                    string[] parts = pathParts[lastNamePart].Split("(");
                    parts[^1] = parts[^1][..(parts[^1].Length - 1)];
                    int number = int.Parse(parts[^1]);

                    parts[^1] = "(" + (number + 1) + ")";
                    pathParts[lastNamePart] = string.Join("", parts);
                }
                else
                {
                    pathParts[lastNamePart] = pathParts[lastNamePart] + "(1)";
                }

                return string.Join("", pathParts);


            }

            [GeneratedRegex("^.*\\([0-9]+\\)$")]
            private static partial Regex FilePattern();
        }
    }
}

