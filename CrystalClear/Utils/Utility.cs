using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CrystalClear.Utils
{
    public class Utility
    {
        public static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.]+");
            return regex.IsMatch(text);
        }

        public static List<string> GetExplanationsForLevels()
        {
            return new List<string>
            {
                "",
                "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.",
                "Contrary to popular belief, Lorem Ipsum is not simply random text.",
                "There are many variations of passages of Lorem Ipsum available.",
                "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s."
            };
        }
    }
}
