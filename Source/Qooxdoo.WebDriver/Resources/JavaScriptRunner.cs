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

namespace Qooxdoo.WebDriver.Resources
{
    public class JavaScriptRunner
    {
        public JavaScriptRunner(IJavaScriptExecutor jsExecutor)
        {
            Exec = jsExecutor;
            Exec.ExecuteScript(Namespace + " = {};");
        }

        protected internal IJavaScriptExecutor Exec;

        internal static string Namespace = "qxwebdriver";

        internal IList<string> CreatedFunctions = new List<string>();

        public virtual object RunScript(string scriptId, params object[] args)
        {
            if (!CreatedFunctions.Contains(scriptId))
            {
                DefineFunction(scriptId);
            }

            string fqFunctionName = Namespace + "." + scriptId;
            string call = "return " + fqFunctionName + ".apply(this, arguments);";
            return Exec.ExecuteScript(call, args);
        }

        public virtual void DefineFunction(string scriptId)
        {
            string fqFunctionName = Namespace + "." + scriptId;
            string function = "function() {" + JavaScript.Instance.GetValue(scriptId) + "}";
            string script = fqFunctionName + " = " + function;
            Exec.ExecuteScript(script);
            CreatedFunctions.Add(scriptId);
        }
    }
}