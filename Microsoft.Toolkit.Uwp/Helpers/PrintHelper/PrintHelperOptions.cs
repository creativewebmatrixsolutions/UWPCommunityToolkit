﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Printing;

namespace Microsoft.Toolkit.Uwp.Helpers
{
    /// <summary>
    /// Public class to store settings applicable to a print task
    /// </summary>
    public class PrintHelperOptions
    {
        /// <summary>
        /// Gets or sets the bordering option for the print task.
        /// </summary>
        public PrintBordering Bordering { get; set; }

        /// <summary>
        /// Gets or sets the media type option for the print task.
        /// </summary>
        public PrintMediaType MediaType { get; set; }

        /// <summary>
        /// Gets or sets the media size option of the print task.
        /// </summary>
        public PrintMediaSize MediaSize { get; set; }

        /// <summary>
        /// Gets or sets the hole punch option of the print task.
        /// </summary>
        public PrintHolePunch HolePunch { get; set; }

        /// <summary>
        /// Gets or sets the binding option for the print task.
        /// </summary>
        public PrintBinding Binding { get; set; }

        /// <summary>
        /// Gets or sets the duplex option of the print task.
        /// </summary>
        public PrintDuplex Duplex { get; set; }

        /// <summary>
        /// Gets or sets the color mode option of the print task.
        /// </summary>
        public PrintColorMode ColorMode { get; set; }

        /// <summary>
        /// Gets or sets the collation option of the print tasks.
        /// </summary>
        public PrintCollation Collation { get; set; }

        /// <summary>
        /// Gets or sets the print quality option for the print task.
        /// </summary>
        public PrintQuality PrintQuality { get; set; }

        /// <summary>
        /// Gets or sets the staple option for the print task.
        /// </summary>
        public PrintStaple Staple { get; set; }

        /// <summary>
        /// Gets or sets the orientation option for the print task.
        /// </summary>
        public PrintOrientation Orientation { get; set; }

        /// <summary>
        /// Gets the options that will be displayed in the printing dialog
        /// </summary>
        public IList<string> DisplayedOptions { get; private set; }

        /// <summary>
        /// Gets the possible display options
        /// </summary>
        private IEnumerable<string> _possibleDisplayOptions;

        /// <summary>
        /// Gets or sets a value indicating whether the default displayed options should be kept.
        /// Defaults to true
        /// </summary>
        public bool ExtendDisplayedOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintHelperOptions"/> class.
        /// </summary>
        /// <param name="extendDisplayedOptions">Boolean used to set up the <see cref="ExtendDisplayedOptions"/> property</param>
        public PrintHelperOptions(bool extendDisplayedOptions = true)
        {
            ExtendDisplayedOptions = extendDisplayedOptions;
            DisplayedOptions = new List<string>();

            InitializePossibleDisplayOptions();
        }

        /// <summary>
        /// Adds a display option
        /// </summary>
        /// <param name="displayOption">Display option to add. Must be a part of the <see cref="StandardPrintTaskOptions"/> class</param>
        public void AddDisplayOption(string displayOption)
        {
            if (!_possibleDisplayOptions.Contains(displayOption))
            {
                throw new ArgumentException("The display option must be a part of the StandardPrintTaskOptions class");
            }

            if (DisplayedOptions.Contains(displayOption))
            {
                return;
            }

            DisplayedOptions.Add(displayOption);
        }

        /// <summary>
        /// Removes a display option
        /// </summary>
        /// <param name="displayOption">Display option to add. Must be a part of the <see cref="StandardPrintTaskOptions"/> class</param>
        public void RemoveDisplayOption(string displayOption)
        {
            if (!_possibleDisplayOptions.Contains(displayOption))
            {
                throw new ArgumentException("The display option must be a part of the StandardPrintTaskOptions class");
            }

            if (!DisplayedOptions.Contains(displayOption))
            {
                return;
            }

            DisplayedOptions.Remove(displayOption);
        }

        private void InitializePossibleDisplayOptions()
        {
            _possibleDisplayOptions = typeof(StandardPrintTaskOptions).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                                                                      .Where(p => p.PropertyType == typeof(string))
                                                                      .Select(p => (string)p.GetValue(null));
        }

        private void InitializeDefaultOptions()
        {
            Bordering = PrintBordering.Default;
            MediaSize = PrintMediaSize.Default;
            MediaType = PrintMediaType.Default;
            HolePunch = PrintHolePunch.Default;
            Binding = PrintBinding.Default;
            Duplex = PrintDuplex.Default;
            ColorMode = PrintColorMode.Default;
            Collation = PrintCollation.Default;
            PrintQuality = PrintQuality.Default;
            Staple = PrintStaple.Default;
            Orientation = PrintOrientation.Default;
        }
    }
}
