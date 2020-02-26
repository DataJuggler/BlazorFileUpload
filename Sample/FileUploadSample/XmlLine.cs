

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataJuggler.UltimateHelper.Core.Objects;

#endregion

namespace FileUploadSample
{

    #region class XmlLine
    /// <summary>
    /// This class extends XmlLine as I needed an Indent property.
    /// </summary>
    public class XmlLine
    {
        
        #region Private Variables
        private bool indent;
        private string text;
        #endregion

        #region Properties

            #region Indent
            /// <summary>
            /// This property gets or sets the value for 'Indent'.
            /// </summary>
            public bool Indent
            {
                get { return indent; }
                set { indent = value; }
            }
            #endregion
            
            #region Text
            /// <summary>
            /// This property gets or sets the value for 'Text'.
            /// </summary>
            public string Text
            {
                get { return text; }
                set { text = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
