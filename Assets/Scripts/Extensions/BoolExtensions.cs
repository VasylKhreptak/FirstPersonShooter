namespace Extensions
{
    public static class BoolExtensions
    {
        public static int Sign(this bool value) => value ? 1 : -1;
    }
}