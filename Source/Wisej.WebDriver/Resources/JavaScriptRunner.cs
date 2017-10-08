/*************************************************************************

   qxwebdriver-java

   http://github.com/qooxdoo/qxwebdriver-java

   Copyright:
     2012-2013 1&1 Internet AG, Germany, http://www.1und1.de

   License:
     LGPL: http://www.gnu.org/licenses/lgpl.html
     EPL: http://www.eclipse.org/org/documents/epl-v10.php
     See the license.txt file in the project's top-level directory for details.

   Authors:
     * Daniel Wagner (danielwagner)

*************************************************************************/

using System.Collections.Generic;
using OpenQA.Selenium;

namespace Wisej.Qooxdoo.WebDriver.Resources
{
    /// <summary>
    /// Runs a Javascript scripts
    /// </summary>
    public class JavaScriptRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptRunner"/> class.
        /// </summary>
        /// <param name="jsExecutor">The js executor.</param>
        public JavaScriptRunner(IJavaScriptExecutor jsExecutor)
        {
            Executor = jsExecutor;
            Executor.ExecuteScript(Namespace + " = {};");
        }

        /// <summary>
        /// The Javascript executor
        /// </summary>
        protected internal IJavaScriptExecutor Executor;

        internal static string Namespace = "qxwebdriver";

        internal IList<string> CreatedFunctions = new List<string>();

        /// <summary>
        /// Runs the script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The value returned by the script.</returns>
        public virtual object RunScript(string scriptId, params object[] args)
        {
            if (!CreatedFunctions.Contains(scriptId))
            {
                DefineFunction(scriptId);
            }

            string fqFunctionName = Namespace + "." + scriptId;
            string call = "return " + fqFunctionName + ".apply(this, arguments);";
            return Executor.ExecuteScript(call, args);
        }


        /// <summary>
        /// Defines a Javascript function
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        public virtual void DefineFunction(string scriptId)
        {
            string fqFunctionName = Namespace + "." + scriptId;
            string function = "function() {" + JavaScript.Instance.GetValue(scriptId) + "}";
            string script = fqFunctionName + " = " + function;
            Executor.ExecuteScript(script);
            CreatedFunctions.Add(scriptId);
        }
    }
}