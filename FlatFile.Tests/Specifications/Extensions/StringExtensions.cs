namespace FlatFile.Tests.Specifications.Extensions
{
    public static class StringExtensions
    {
        public static int? ToNullableInt32(this string s)
        {
            int i;
            if (int.TryParse(s, out i))
            {
                return i;
            }
            return null;
        } 
    }
}