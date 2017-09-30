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

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

/*using JSONArray = org.json.simple.JSONArray;
using JSONObject = org.json.simple.JSONObject;
using JSONParser = org.json.simple.parser.JSONParser;
using ParseException = org.json.simple.parser.ParseException;*/

namespace Qooxdoo.WebDriver.UI.Table
{
    public class Table : Core.WidgetImpl, IScrollable
    {
        public Table(IWebElement element, QxWebDriver webDriver) : base(element, webDriver)
        {
        }

        public virtual IList<string> HeaderLabels
        {
            get
            {
                IList<IWebElement> children = HeaderCells;

                IList<string> labels = new List<string>();

                using (IEnumerator<IWebElement> itr = children.GetEnumerator())
                {
                    while (itr.MoveNext())
                    {
                        IWebElement child = itr.Current;
                        IWebElement label =
                            child.FindElement(OpenQA.Selenium.By.XPath("div[not(contains(@style, 'background-image'))]"));
                        IWidget labelWidget = Driver.GetWidgetForElement(label);
                        labels.Add((string) labelWidget.GetPropertyValue("value"));
                        //labels.add(label.getText());
                    }
                }

                return labels;
            }
        }

        protected internal virtual IList<IWebElement> HeaderCells
        {
            get
            {
                IWidget header = FindWidget(By.Qxh("*/qx.ui.table.pane.Header"));
                IList<IWebElement> cells =
                    header.ContentElement.FindElements(OpenQA.Selenium.By.XPath("div[starts-with(@class, 'qx-table-header-cell')]"));

                return cells;
            }
        }

        public virtual IWidget GetHeaderCell(string label)
        {
            IList<IWebElement> children = HeaderCells;
            IWidget widget = null;

            using (IEnumerator<IWebElement> itr = children.GetEnumerator())
            {
                while (itr.MoveNext())
                {
                    IWebElement child = itr.Current;
                    if (label.Equals(child.Text))
                    {
                        widget = Driver.GetWidgetForElement(child);
                        break;
                    }
                }
            }

            return widget;
        }

        public virtual IWidget GetHeaderCell(int index)
        {
            IList<IWebElement> children = HeaderCells;
            IWidget widget = null;

            int i = -1;
            using (IEnumerator<IWebElement> itr = children.GetEnumerator())
            {
                while (itr.MoveNext())
                {
                    i++;
                    IWebElement child = itr.Current;
                    if (i == index)
                    {
                        widget = Driver.GetWidgetForElement(child);
                        break;
                    }
                }
            }

            return widget;
        }

        public virtual IWidget ColumnMenuButton
        {
            get
            {
                IWidget scroller = Scroller;
                IWebElement button = scroller.ContentElement.FindElement(
                    OpenQA.Selenium.By.XPath("div/div[contains(@class, 'qx-table-header-column-button')]"));
                return Driver.GetWidgetForElement(button);
            }
        }

        public virtual Pane.Scroller Scroller
        {
            get { return (Pane.Scroller) FindWidget(By.Qxh("*/qx.ui.table.pane.Scroller")); }
        }

        public void ScrollTo(string direction, int? position)
        {
            Scroller.ScrollTo(direction, position);
        }

        public IWidget ScrollToChild(string direction, OpenQA.Selenium.By locator)
        {
            return Scroller.ScrollToChild(direction, locator);
        }

        public long? GetMaximum(string direction)
        {
            return Scroller.GetMaximum(direction);
        }

        public long? GetScrollPosition(string direction)
        {
            return Scroller.GetScrollPosition(direction);
        }

        public virtual IWebElement ScrollToRow(int? rowIndex)
        {
            return Scroller.ScrollToRow(rowIndex);
        }

        public virtual IWebElement GetCellByText(string text)
        {
            string cellPath = "//div[contains(@class, 'qooxdoo-table-cell') and text()='" + text + "']";
            ScrollToChild("y", OpenQA.Selenium.By.XPath(cellPath));
            return FindElement(OpenQA.Selenium.By.XPath(cellPath));
        }

        /// <summary>
        /// Return the text in the given cell of the table.
        /// </summary>
        /// <param name="rowIdx"> Row index (from 0) </param>
        /// <param name="colIdx"> Column index (from 0) </param>
        /// <returns> Text in cell </returns>
        public virtual string GetCellText(long rowIdx, long colIdx)
        {
            return GetCellElement(rowIdx, colIdx).Text;
        }

        public virtual IWebElement GetCellElement(long rowIdx, long colIdx)
        {
            string cellPath;
            if (Classname.Equals("qx.ui.treevirtual.TreeVirtual"))
            {
                //
                // Hierarchy:
                // * TreeVirtual div (content element)
                //   * composite div
                //     * scroller div (tree column)
                //       * composite div (for header)
                //       * clipper div
                //         * pane div
                //           * anonymous div
                //             * row div
                //               * div.qooxdoo-table-cell
                //     * scroller (other columns)
                //       * clipper div
                //         * pane div
                //           * anonymous div
                //             * row div
                //               * div.qooxdoo-table-cell
                //
                // TODO: handle meta columns: this code assumes [1, -1] for metaColumntCounts
                //System.out.println(this.getPropertyValueAsJson("metaColumnCounts"));
                if (colIdx == 0)
                {
                    cellPath = "./div[1]/div[1]/div[2]//div[contains(@class, 'qooxdoo-table-cell')]/" +
                               "parent::div[count(preceding-sibling::div) = " + (rowIdx) + "]/" + "div";
                }
                else
                {
                    cellPath = "./div[1]/div[2]/div[2]//div[contains(@class, 'qooxdoo-table-cell')]/" +
                               "parent::div[count(preceding-sibling::div) = " + (rowIdx) + "]/" + "div[position() = " + (colIdx) + "]";
                }
            }
            else
            {
                cellPath = ".//div[contains(@class, 'qooxdoo-table-cell')]/" +
                           "parent::div[count(preceding-sibling::div) = " + (rowIdx) + "]/" + "div[position() = " + (colIdx + 1) + "]";
            }
            return FindElement(OpenQA.Selenium.By.XPath(cellPath));
        }

        /// <summary>
        /// Return the index of the row containing the supplied text <code>text</code>
        /// at column <code>colIdx</code>.
        /// </summary>
        /// <param name="colIdx"> Index of column (from 0) that should contain the text </param>
        /// <param name="text"> Text to search for </param>
        /// <returns> The row index (from 0) or -1 if the text was not found </returns>
        public virtual long GetRowIndexForCellText(long colIdx, string text)
        {
            string cellPath;
            if (Classname.Equals("qx.ui.treevirtual.TreeVirtual"))
            {
                // Hierarchy:
                // * TreeVirtual div (content element)
                //   * composite div
                //     * scroller div (tree column)
                //       * composite div (for header)
                //       * clipper div
                //         * pane div
                //           * anonymous div
                //             * row div
                //               * div.qooxdoo-table-cell
                //     * scroller (other columns)
                //       * clipper div
                //         * pane div
                //           * anonymous div
                //             * row div
                //               * div.qooxdoo-table-cell
                //
                // TODO: handle meta columns: this code assumes [1, -1] for metaColumnCounts
                //System.out.println(this.getPropertyValueAsJson("metaColumnCounts"));
                if (colIdx == 0)
                {
                    cellPath = "./div[1]/div[1]/div[2]//div[contains(@class, 'qooxdoo-table-cell')]";
                }
                else
                {
                    cellPath = "./div[1]/div[2]/div[2]//div[contains(@class, 'qooxdoo-table-cell') and position() = " + colIdx + "]";
                }
            }
            else
            {
                cellPath = ".//div[contains(@class, 'qooxdoo-table-cell') and position() = " + (colIdx + 1) + "]";
            }
            IList<IWebElement> els = FindElements(OpenQA.Selenium.By.XPath(cellPath));

            for (int rowIdx = 0; rowIdx < els.Count; rowIdx++)
            {
                string s = els[rowIdx].Text.Trim();
                if (text.Equals(s))
                {
                    return rowIdx;
                }
            }
            return -1L;
        }

        /// <summary>
        /// Return a list of indexes of rows containing the supplied text <code>text</code>
        /// at column <code>colIdx</code>.
        /// </summary>
        /// <param name="colIdx"> Index of column (from 0) that should contain the text </param>
        /// <param name="text"> Text to search for </param>
        /// <returns> The a list of row indexes containing the text </returns>
        public virtual IList<long?> GetRowIndexesForCellText(long colIdx, string text)
        {
            string cellPath;
            if (Classname.Equals("qx.ui.treevirtual.TreeVirtual"))
            {
                // Hierarchy:
                // * TreeVirtual div (content element)
                //   * composite div
                //     * scroller div (tree column)
                //       * composite div (for header)
                //       * clipper div
                //         * pane div
                //           * anonymous div
                //             * row div
                //               * div.qooxdoo-table-cell
                //     * scroller (other columns)
                //       * clipper div
                //         * pane div
                //           * anonymous div
                //             * row div
                //               * div.qooxdoo-table-cell
                //
                // TODO: handle meta columns: this code assumes [1, -1] for metaColumnCounts
                //System.out.println(this.getPropertyValueAsJson("metaColumnCounts"));
                if (colIdx == 0)
                {
                    cellPath = "./div[1]/div[1]/div[2]//div[contains(@class, 'qooxdoo-table-cell')]";
                }
                else
                {
                    cellPath = "./div[1]/div[2]/div[2]//div[contains(@class, 'qooxdoo-table-cell') and position() = " + colIdx + "]";
                }
            }
            else
            {
                cellPath = ".//div[contains(@class, 'qooxdoo-table-cell') and position() = " + (colIdx + 1) + "]";
            }
            IList<IWebElement> els = FindElements(OpenQA.Selenium.By.XPath(cellPath));
            IList<long?> rowIdxs = new List<long?>();
            for (int rowIdx = 0; rowIdx < els.Count; rowIdx++)
            {
                string s = els[rowIdx].Text.Trim();
                if (text.Equals(s))
                {
                    rowIdxs.Add((long) rowIdx);
                }
            }
            return rowIdxs;
        }

        public virtual IList<Dictionary<string, long?>> SelectedRanges
        {
            get
            {
                string json = (string) JsRunner.RunScript("getTableSelectedRanges", contentElement);
                //JSONParser parser = new JSONParser();
                IList<Dictionary<string, long?>> ranges = null;

                object obj;
                try
                {
                    ranges = new List<Dictionary<string, long?>>();

                    JArray jArray = JArray.Parse(json);
                    using (IEnumerator<JToken> itr = jArray.GetEnumerator())
                    {
                        while (itr.MoveNext())
                        {
                            var rangeMap = itr.Current;
                            var range = new Dictionary<string, long?>();
                            if (rangeMap != null)
                            {
                                range["minIndex"] = (long?) rangeMap["minIndex"];
                                range["maxIndex"] = (long?) rangeMap["maxIndex"];
                                ranges.Add(range);
                            }
                        }
                    }

                    // Java converted code
                    /*JObject jObject = JObject.Parse(json);
                    obj = parser.parse(json);
                    JSONArray array = (JSONArray) obj;
                    using (IEnumerator<JSONObject> itr = array.GetEnumerator())
                    {
                        while (itr.MoveNext())
                        {
                            JSONObject rangeMap = itr.Current;
                            Dictionary<string, long?> range = new Dictionary<string, long?>();
                            range["minIndex"] = (long?) rangeMap.get("minIndex");
                            range["maxIndex"] = (long?) rangeMap.get("maxIndex");
                            ranges.Add(range);
                        }
                    }*/
                }
                //catch (ParseException e)
                catch (JsonException e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                }

                return ranges;
            }
        }

        public virtual IWidget CellEditor
        {
            get
            {
                IWidget focusIndicator =
                    Scroller.FindWidget(By.Qxh("qx.ui.table.pane.Clipper/qx.ui.table.pane.FocusIndicator"));
                IWidget editor = focusIndicator.FindWidget(By.Qxh("child[0]"));
                if (editor.Classname.Equals("qx.ui.container.Composite"))
                {
                    editor = editor.FindWidget(By.Qxh("child[0]"));
                }

                return editor;
            }
        }

        public virtual long? RowCount
        {
            get
            {
                long? result = (long?) JsRunner.RunScript("getRowCount", contentElement);
                return result;
            }
        }

        /// <summary>
        /// Select the table row at position <code>rowIdx</code>.
        /// </summary>
        /// <param name="rowIdx"> the index of the row to select </param>
        public virtual void SelectRow(long? rowIdx)
        {
            JsRunner.RunScript("selectTableRow", contentElement, rowIdx);
        }

        public virtual long? ColumnCount
        {
            get
            {
                long? result = (long?) JsRunner.RunScript("getColumnCount", contentElement);
                return result;
            }
        }

        /// <summary>
        /// Select the table row at position <code>rowIdx</code>.
        /// </summary>
        /// <param name="rowIdx"> the index of the row to select </param>
        public virtual void SetNodeOpened(long? rowIdx, bool? opened)
        {
            long? result = (long?) JsRunner.RunScript("setTreeNodeOpened", contentElement, rowIdx, opened);
        }
    }
}