namespace util
{
    public class Util
    {
        public static void CopyBytes(byte[] destination, params byte[][] values)
        {
            int byteCounter = 0;
            for (int i = 0; i < values.Length; i++)
            {
                values[i].CopyTo(destination, byteCounter);
                byteCounter += values[i].Length;
            }
        }
    }
}