namespace Assets.Script_Tools
{
    public static class ReferenceTryGetValueExtension
    {
        public static bool TryGetValue<T>(this T n, out T o) where T: class
        {
            o = n;
            return n != null;
        }
    }
}