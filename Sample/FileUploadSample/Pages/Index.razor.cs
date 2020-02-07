

#region using statements

using DataJuggler.Blazor.FileUpload;

#endregion

namespace FileUploadSample.Pages
{

    #region class Index
    /// <summary>
    /// This class is the code behind for the Sample page.
    /// </summary>
    public partial class Index
    {

        #region Private Variables
        private string status;
        #endregion

        #region Methods

            #region OnFileUploaded(UploadedFileInfo uploadedFileInfo)
            /// <summary>
            /// method returns the File Uploaded
            /// </summary>
            private void OnFileUploaded(UploadedFileInfo uploadedFileInfo)
            {
                // if aborted
                if (uploadedFileInfo.Aborted)
                {
                    // get the status
                    status = uploadedFileInfo.ErrorMessage;
                }
                else
                {
                    // get the status
                    status = "The file " + uploadedFileInfo.FullName + " was uploaded.";
                    
                    // other information about the file is available
                    //DateTime lastModified = uploadedFileInfo.LastModified;
                    //string nameAsItIsOnDisk = uploadedFileInfo.NameWithPartialGuid;
                    //string partialGuid = uploadedFileInfo.PartialGuid;
                    //long size = uploadedFileInfo.Size;
                    //string type = uploadedFileInfo.Type;
                    
                    // The above information can be used to display, and / or process a file
                }
            }
            #endregion
            
            #region OnReset(string notUsedByRequiredArg)
            /// <summary>
            /// method returns the Reset
            /// </summary>
            private void OnReset(string notUsedByRequiredArg)
            {
                // erase status
                status = "";
                
                // Refresh the UI
                StateHasChanged();
            }
            #endregion
            
        #endregion

        #region Properties

            #region Status
            /// <summary>
            /// This property gets or sets the value for 'Status'.
            /// </summary>
            public string Status
            {
                get { return status; }
                set { status = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
