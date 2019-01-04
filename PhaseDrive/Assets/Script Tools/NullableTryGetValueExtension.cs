namespace Assets.ScriptTools
{
    public static class NullableTryGetValueExtension
    {
        public static bool TryGetValue<T>(this T? n, out T o) where T: struct 
        {
            if (n == null)
            {
                o = default;
                return false;
            }

            o = n.Value;
            return true;
        }
    }
}
