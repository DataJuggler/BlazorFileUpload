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
        private bool appendPartialGuid;
        private string partialGuid;
        private DateTime lastModified;
        private string name;
        private long size;
        private string type;
        private bool aborted;
        private string errorMessage;
        private string extension;
        private Exception exception;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of an UploadedFileInfo object
        /// </summary>
        /// <param name="file"></param>
        /// <param name="partialGuid"></param>
        public UploadedFileInfo(IFileListEntry file, string partialGuid, bool appendPartialGuid)
        {
            // verify both objects exist
            if ((file != null) && (!String.IsNullOrWhiteSpace(partialGuid)))
            {
                // Set all the properties
                this.LastModified = file.LastModified;
                this.AppendPartialGuid = appendPartialGuid;
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
            
            #region AppendPartialGuid
            /// <summary>
            /// This property gets or sets the value for 'AppendPartialGuid'.
            /// </summary>
            public bool AppendPartialGuid
            {
                get { return appendPartialGuid; }
                set { appendPartialGuid = value; }
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
            
            #region Extension
            /// <summary>
            /// This property gets or sets the value for 'Extension'.
            /// </summary>
            public string Extension
            {
                get { return extension; }
                set { extension = value; }
            }
            #endregion

            #region FullName
            /// <summary>
            /// This read only property returns the File Name plus the PartialGuid, if AppendPartialGuid is true.
            /// </summary>
            public string FullName
            {
                get
                {
                    // return value
                    return NameWithPartialGuid;
                }
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
            
            #region HasPartialGuid
            /// <summary>
            /// This property returns true if the 'PartialGuid' exists.
            /// </summary>
            public bool HasPartialGuid
            {
                get
                {
                    // initial value
                    bool hasPartialGuid = (!String.IsNullOrWhiteSpace(this.PartialGuid));
                    
                    // return value
                    return hasPartialGuid;
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
                    string nameWithPartialGuid = Name;
                    
                    // if the value for AppendPartialGuid is true
                    if (AppendPartialGuid)
                    {
                        // set the return value
                        nameWithPartialGuid = name.Substring(0, name.Length - extension.Length + 1) + partialGuid + extension;
                    }

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
