using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.Mobile
{
    public static class TextHelpers
    {
        /// <summary>
        /// Changes the provided word from a plural form to a singular form.
        /// </summary>
        public static Func<string, string> Singularize =
            original => original.EndsWith("ies")
                ? original.TrimEnd('s').TrimEnd('e').TrimEnd('i') + "y"
                : original.TrimEnd('s');
    }
}
