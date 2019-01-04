namespace Assets.ScriptTools
{
    public static class ArrayTryGetFirstItemExtension
    {
        public static bool TryGetFirstItem<T>(this T[] array, out T value)
        {
            if (array.Length == 0)
            {
                value = default;
                return false;
            }

            value = array[0];
            return true;
        }
    }
}