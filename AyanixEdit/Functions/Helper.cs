using System.Drawing;

namespace AyanixEdit
{
    public static class Helper
    {
        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }
    }


}
