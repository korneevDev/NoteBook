using System.Text.RegularExpressions;

namespace NoteBookLib.FileHandler
{
    public interface IPathFormatter
    {

        string Format(string path);

        public class Base : IPathFormatter
        {
            public string Format(string path)
            {
                if (!File.Exists(path))
                    return path;

                string[] pathParts = path.Split('.');

                if (pathParts.Length < 2)
                {
                    throw new ArgumentException("Illegal path (no extension)");
                }

                int extensionIndex = pathParts.Length - 1;
                int lastNamePart = pathParts.Length - 2;

                if (Regex.IsMatch(path, "^.*\\([0-9]+\\)\\..*$"))
                {
                    string[] parts = pathParts[lastNamePart].Split("(");
                    parts[parts.Length - 1] =
                        parts[parts.Length - 1].Substring(0, parts[parts.Length - 1].Length - 1);
                    int number = int.Parse(parts[parts.Length - 1]);

                    parts[parts.Length - 1] = "(" + (number + 1) + ")";
                    pathParts[lastNamePart] = string.Join("", parts);
                }
                else
                {
                    pathParts[lastNamePart] = pathParts[lastNamePart] + "(1)";
                }
                pathParts[extensionIndex] = "." + pathParts[extensionIndex];

                return string.Join("", pathParts);


            }
        }
    }
}

