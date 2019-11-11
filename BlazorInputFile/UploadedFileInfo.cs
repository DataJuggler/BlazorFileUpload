using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlazorInputFile
{
    public class UploadedFileInfo
    {
        #region Private Variables
        private string fileName;
        private string partialGuid;
        private DateTime lastModified;
        private string name;
        private long size;
        private string type;
        #endregion

        #region Properties

            #region FileName
            /// <summary>
            /// This property gets or sets the value for 'FileName'.
            /// </summary>
            public string FileName
            {
                get { return fileName; }
                set { fileName = value; }
            }
            #endregion

            #region FileNameWithPartialGuid
            /// <summary>
            /// This read only property returns the FileName plus the PartialGuid.
            /// </summary>
            public string FileNameWithPartialGuid
            {
                get
                {
                    // initial value
                    string fileNameWithPartialGuid = FileName + "." + PartialGuid;

                    // return value
                    return fileNameWithPartialGuid;
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
