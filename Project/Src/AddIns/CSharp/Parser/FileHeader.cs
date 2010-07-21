//-----------------------------------------------------------------------
// <copyright file="FileHeader.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// <license>
//   This source code is subject to terms and conditions of the Microsoft 
//   Public License. A copy of the license can be found in the License.html 
//   file at the root of this distribution. If you cannot locate the  
//   Microsoft Public License, please send an email to dlr@microsoft.com. 
//   By using this source code in any fashion, you are agreeing to be bound 
//   by the terms of the Microsoft Public License. You must not remove this 
//   notice, or any other, from this software.
// </license>
//-----------------------------------------------------------------------
namespace Microsoft.StyleCop.CSharp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Describes the header at the top of a C# file.
    /// </summary>
    /// <subcategory>other</subcategory>
    public class FileHeader
    {
        #region Private Fields

        /// <summary>
        /// Indicates whether the file header has the generated attribute.
        /// </summary>
        private bool generated;

        /// <summary>
        /// The header text.
        /// </summary>
        private string headerText;

        /// <summary>
        /// The header text wrapped into an Xml tag.
        /// </summary>
        private string headerXml;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the FileHeader class.
        /// </summary>
        /// <param name="headerText">The header text.</param>
        public FileHeader(string headerText)
        {
            Param.RequireNotNull(headerText, "headerText");

            // Save the header text.
            this.headerText = headerText;

            // Attempt to load this into an Xml document.
            try
            {
                if (this.headerText.Length > 0)
                {
                    this.headerXml = string.Format(CultureInfo.InvariantCulture, "<root>{0}</root>", this.headerText);

                    var doc = new XmlDocument();
                    doc.LoadXml(this.headerXml);

                    // Check whether the header has the autogenerated tag.
                    XmlNode node = doc.DocumentElement["autogenerated"];
                    if (node != null)
                    {
                        // Set this as generated code.
                        this.generated = true;
                    }
                    else
                    {
                        node = doc.DocumentElement["auto-generated"];
                        if (node != null)
                        {
                            // Set this as generated code.
                            this.generated = true;
                        }
                    }
                }
            }
            catch (XmlException)
            {
            }
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether the file header contains the auto-generated attribute.
        /// </summary>
        public bool Generated
        {
            get
            {
                return this.generated;
            }
        }

        /// <summary>
        /// Gets the header text.
        /// </summary>
        public string HeaderText
        {
            get
            {
                return this.headerText;
            }
        }

        /// <summary>
        /// Gets the header text string modified such that it is loadable into
        /// an <see cref="XmlDocument"/> object.
        /// </summary>
        public string HeaderXml
        {
            get
            {
                return this.headerXml;
            }
        }

        #endregion Public Properties
    }
}