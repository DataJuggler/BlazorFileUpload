using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataJuggler.Blazor.FileUpload
{
    public class UploadedFileInfo
    {
        #region Private Variables
        private string partialGuid;
        private DateTime lastModified;
        private string name;
        private long size;
        private string type;
        private bool aborted;
        private string errorMessage;
        private Exception exception;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of an UploadedFileInfo object
        /// </summary>
        /// <param name="file"></param>
        /// <param name="partialGuid"></param>
        public UploadedFileInfo(IFileListEntry file, string partialGuid)
        {
            // verify both objects exist
            if ((file != null) && (!String.IsNullOrWhiteSpace(partialGuid)))
            {
                // Set all the properties
                this.LastModified = file.LastModified;
                this.Name = file.Name;
                this.PartialGuid = partialGuid;
                this.Size = file.Size;
                this.Type = file.Type;
            }
        }
        #endregion

        #region Properties
            
            #region Aborted
            /// <summary>
            /// This property gets or sets the value for 'Aborted'.
            /// </summary>
            public bool Aborted
            {
                get { return aborted; }
                set { aborted = value; }
            }
            #endregion
            
            #region ErrorMessage
            /// <summary>
            /// This property gets or sets the value for 'ErrorMessage'.
            /// </summary>
            public string ErrorMessage
            {
                get { return errorMessage; }
                set { errorMessage = value; }
            }
            #endregion
            
            #region Exception
            /// <summary>
            /// This property gets or sets the value for 'Exception'.
            /// </summary>
            public Exception Exception
            {
                get { return exception; }
                set { exception = value; }
            }
            #endregion
            
            #region HasException
            /// <summary>
            /// This property returns true if this object has an 'Exception'.
            /// </summary>
            public bool HasException
            {
                get
                {
                    // initial value
                    bool hasException = (this.Exception != null);
                    
                    // return value
                    return hasException;
                }
            }
            #endregion
            
            #region LastModified
            /// <summary>
            /// This property gets or sets the value for 'LastModified'.
            /// </summary>
            public DateTime LastModified
            {
                get { return lastModified; }
                set { lastModified = value; }
            }
            #endregion
            
            #region Name
            /// <summary>
            /// This property gets or sets the value for 'Name'.
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            #endregion

            #region NameWithPartialGuid
            /// <summary>
            /// This read only property returns the File Name plus the PartialGuid.
            /// </summary>
            public string NameWithPartialGuid
            {
                get
                {
                    // initial value
                    string nameWithPartialGuid = Name + "." + PartialGuid;

                    // return value
                    return nameWithPartialGuid;
                }
            }
            #endregion
            
            #region PartialGuid
            /// <summary>
            /// This property gets or sets the value for 'PartialGuid'.
            /// </summary>
            public string PartialGuid
            {
                get { return partialGuid; }
                set { partialGuid = value; }
            }
            #endregion
            
            #region Size
            /// <summary>
            /// This property gets or sets the value for 'Size'.
            /// </summary>
            public long Size
            {
                get { return size; }
                set { size = value; }
            }
            #endregion
            
            #region Type
            /// <summary>
            /// This property gets or sets the value for 'Type'.
            /// </summary>
            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            #endregion
            
        #endregion

    }
}
