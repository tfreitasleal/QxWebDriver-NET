using System.Collections.Generic;
using JavaScriptExecutor = OpenQA.Selenium.IJavaScriptExecutor;

/* ************************************************************************

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

************************************************************************ */

namespace Qooxdoo.WebDriver.Resources
{
    public class JavaScriptRunner
    {
        public JavaScriptRunner(JavaScriptExecutor jsExecutor)
        {
            exec = jsExecutor;
            exec.ExecuteScript(Namespace + " = {};");
        }

        protected internal JavaScriptExecutor exec;

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
            return exec.ExecuteScript(call, args);
        }

        public virtual void DefineFunction(string scriptId)
        {
            string fqFunctionName = Namespace + "." + scriptId;
            string function = "function() {" + JavaScript.Instance.GetValue(scriptId) + "}";
            string script = fqFunctionName + " = " + function;
            exec.ExecuteScript(script);
            CreatedFunctions.Add(scriptId);
        }

    }

}