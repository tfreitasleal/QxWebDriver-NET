using System.Linq;

namespace Wisej.Qooxdoo.WebDriver
{
    public partial class By
    {
        /// <summary>
        /// Convert a widget namespace (including the widget's name) to a <see cref="ByQxh"/> locator string.
        /// </summary>
        /// <param name="namespace">The locator string.</param>
        /// <returns>The converted locator string.</returns>
        public static string Namespace(string @namespace)
        {
            var parts = @namespace.Split('.');

            var result = string.Join(@"/", parts.Select(part => string.Format("[@name={0}]", part)).ToArray());

            return result;
        }
    }
}