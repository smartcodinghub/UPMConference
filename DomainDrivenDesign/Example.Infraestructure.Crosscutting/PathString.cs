using System;
using System.IO;

namespace Example.Infraestructure.Crosscutting
{
    /// <summary>
    /// This class provides a simple way to manage path
    /// </summary>
    public class PathString
    {
        private string url;
        public string Url => url;

        public PathString(String value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            this.url = value;
        }

        public PathString(String left, String right)
        {
            left = left ?? throw new ArgumentNullException(nameof(left));
            right = right ?? throw new ArgumentNullException(nameof(right));

            if(!left.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.InvariantCulture)) left = left + Path.DirectorySeparatorChar;

            this.url = Path.Combine(left, right);
        }

        public static PathString operator /(PathString left, PathString right)
        {
            return new PathString(left, right);
        }

        public static PathString operator /(PathString left, string right)
        {
            return new PathString(left, right);
        }

        public static PathString operator /(string left, PathString right)
        {
            return new PathString(left, right);
        }

        public static PathString operator +(PathString left, PathString right)
        {
            return new PathString(left, right);
        }

        public static PathString operator +(PathString left, string right)
        {
            return new PathString(left, right);
        }

        public static PathString operator +(string left, PathString right)
        {
            return new PathString(left, right);
        }

        public static implicit operator String(PathString path)
        {
            return path.Url;
        }

        public static implicit operator PathString(String path)
        {
            return new PathString(path);
        }

        public override string ToString() => Url;
    }
}
