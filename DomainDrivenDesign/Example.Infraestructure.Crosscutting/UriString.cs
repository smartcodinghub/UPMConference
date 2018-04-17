using System;

namespace Example.Infraestructure.Crosscutting
{
    /// <summary>
    /// This class provides a simple way to manage uris
    /// </summary>
    public class UriString
    {
        public Uri Uri { get; }

        private Lazy<string> url;
        public string Url => url.Value;

        public UriString(String value)
        {
            value = value ?? throw new ArgumentNullException(nameof(value));

            Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out Uri createdUri);
            this.Uri = createdUri;
            this.url = new Lazy<string>(Uri.ToString);
        }

        public UriString(String left, String right)
        {
            left = left ?? throw new ArgumentNullException(nameof(left));
            right = right ?? throw new ArgumentNullException(nameof(right));

            if(!left.EndsWith("/", StringComparison.InvariantCulture)) left = left + "/";

            this.Uri = new Uri(new Uri(left), right);
            this.url = new Lazy<string>(Uri.ToString);
        }

        public UriString(Uri left, String right)
        {
            left = left ?? throw new ArgumentNullException(nameof(left));
            right = right ?? throw new ArgumentNullException(nameof(right));

            if(!left.IsAbsoluteUri)
            {
                string l = left.ToString();
                if(!l.EndsWith("/", StringComparison.InvariantCulture)) l = l + "/";
                if(!right.StartsWith("/", StringComparison.InvariantCulture)) right = right.Substring(1);

                this.Uri = new Uri(l + right, UriKind.Relative);
            }
            else
            {
                string l = left.ToString();
                if(!l.EndsWith("/", StringComparison.InvariantCulture)) l = l + "/";
                this.Uri = new Uri(new Uri(l), right);
            }

            this.url = new Lazy<string>(Uri.ToString);
        }

        public static UriString operator /(UriString left, UriString right)
        {
            return new UriString(left.Uri, right);
        }

        public static UriString operator /(UriString left, string right)
        {
            return new UriString(left.Uri, right);
        }

        public static UriString operator /(string left, UriString right)
        {
            return new UriString(left, right);
        }

        public static UriString operator +(UriString left, UriString right)
        {
            return new UriString(left.Uri, right);
        }

        public static UriString operator +(UriString left, string right)
        {
            return new UriString(left.Uri, right);
        }

        public static UriString operator +(string left, UriString right)
        {
            return new UriString(left, right);
        }

        public static implicit operator String(UriString path)
        {
            return path.Url;
        }

        public static implicit operator UriString(String path)
        {
            return new UriString(path);
        }

        public override string ToString() => Url;
    }
}
