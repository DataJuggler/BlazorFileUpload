

#region using statements

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Components.Forms;

#endregion

namespace DataJuggler.Blazor.FileUpload
{

    #region class FileUpload
    /// <summary>
    /// This class is the code behind for the BlazorFileUpload object.
    /// </summary>
    public partial class FileUpload
    {
        
        #region Private Variables
        private string status;
        private bool uploadComplete;
        private string buttonText;
        private string inputFileClassName;
        private string messageClassName;
        private bool showCustomButton;
        private string customButtonClassName;
        private string customButtonTextClassName;
        private string resetButtonClassName;
        private bool visible;
        private bool saveToDisk;
        #endregion

        #region Construcctor()
        /// <summary>
        /// Create a new instance of a FileUpload
        /// </summary>
        public FileUpload()
        {
            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Methods

            #region CheckSize(string extension, Stream ms, UploadedFileInfo uploadedFileInfo)
            /// <summary>
            /// This method returns the Size
            /// </summary>
            public UploadedFileInfo CheckSize(string extension, Stream stream, UploadedFileInfo uploadedFileInfo)
            {
                try
                {
                    // if the file is a .jpg or a .png
                    if (IsImageFile(extension))
                    {
                        // get the image from the memory stream
                        Image image = Bitmap.FromStream(stream);

                        // set the properties for the return value
                        uploadedFileInfo.Height = image.Height;
                        uploadedFileInfo.Width = image.Width;

                        // The RequiredHeight is an exact match

                        // if the RequiredHeight or RequiredWidth and a message is set the upload file
                        if (HasRequiredHeight || (HasRequiredWidth) && (HasCustomRequiredSizeMessage))
                        {
                            // check Required Size

                            // if a RequiredHeight is set
                            if ((HasRequiredHeight) && (image.Height != RequiredHeight))
                            {
                                // Abort for file being incorrect height
                                uploadedFileInfo.Aborted = true;
                            }
                            else if ((HasRequiredWidth) && (image.Width != RequiredWidth))
                            {
                                 // Abort for file being incorrect width
                                uploadedFileInfo.Aborted = true;
                            }
                        }

                        // if not aborted
                        if (!uploadedFileInfo.Aborted)
                        {
                            // check Min Size

                            // if a MinHeight is set
                            if ((HasMinHeight) && (image.Height != MinHeight))
                            {
                                // Abort for file being incorrect height
                                uploadedFileInfo.Aborted = true;
                            }
                            else if ((HasMinWidth) && (image.Width != MinWidth))
                            {
                                    // Abort for file being incorrect width
                                uploadedFileInfo.Aborted = true;
                            }

                            // if Aborted
                            if (uploadedFileInfo.Aborted)
                            {   
                                // Set the Status
                                Status = CustomMinHeightMessage;
                            }
                        }

                        // if not aborted
                        if (!uploadedFileInfo.Aborted)
                        {
                            // check Max Size

                            // if a MaxHeight is set
                            if ((HasMaxHeight) && (image.Height != MaxHeight))
                            {
                                // Abort for file being incorrect height
                                uploadedFileInfo.Aborted = true;
                            }
                            else if ((HasMaxWidth) && (image.Width != MaxWidth))
                            {
                                    // Abort for file being incorrect width
                                uploadedFileInfo.Aborted = true;
                            }

                            // if Aborted
                            if (uploadedFileInfo.Aborted)
                            {   
                                // Set the Status
                                Status = CustomMaxHeightMessage;
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return uploadedFileInfo;
            }
            #endregion
            
            #region FileUploaded(UploadedFileInfo uploadedFileInfo)
            /// <summary>
            /// This event is fired after a file is uploaded, and is used to notify subscribers of the OnChange event.
            /// </summary>
            private void FileUploaded(UploadedFileInfo uploadedFileInfo)
            {
                // Notify the client a file was uploaded
                OnChange.InvokeAsync(uploadedFileInfo);
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Set the Default Values
                MessageClassName = "message";
                InputFileClassName = "inputfile";
                CustomButtonClassName = "buttonwide";
                ResetButtonClassName = "button";
                ButtonText = "Choose File";
                CustomButtonTextClassName = "custombuttontextstyle";
                SaveToDisk = true;
                Visible = true;
            }
            #endregion
            
            #region IsImageFile(string extensions)
            /// <summary>
            /// This method returns true if the extension is an Image File (.jpg or .png for now)
            /// </summary>
            public bool IsImageFile(string extension)
            {
                // initial value
                bool isImageFile = false;

                // if the string exists
                if (!String.IsNullOrEmpty(extension))
                {
                    // if a .jpg or a .png
                    if ((extension.ToLower() == ".jpg") || (extension.ToLower() == ".png"))
                    {
                        // set to true
                        isImageFile = true;
                    }
                }
                
                // return value
                return isImageFile;
            }
            #endregion

            #region OnFileChange(InputFileChangeEventArgs eventArgs)
            /// <summary>
            /// This method gives you access to the files that were uploaded
            /// </summary>
            /// <param name="eventArgs"></param>
            private void OnFileChange(InputFileChangeEventArgs eventArgs)
            {
                //// Get access to the file
                IBrowserFile file = eventArgs.File;

                 // locals
                UploadedFileInfo uploadedFileInfo = null;
                bool abort = false;
                
                // locals
                Stream stream = null; 
                
                // verify the file exists
                if (file != null)
                {
                    try
                    {
                        // the partialGuid is need to ensure uniqueness
                        string partialGuid = Guid.NewGuid().ToString().Substring(0, PartialGuidLength);
                        
                        // create the uploadedFileInfo
                        uploadedFileInfo = new UploadedFileInfo(file, partialGuid, AppendPartialGuid, UploadFolder);
                        
                        // if the file is too large
                        if ((MaxFileSize > 0) && (file.Size > MaxFileSize))
                        {
                            // Show the FileTooLargeMessage
                            status = FileTooLargeMessage;
                            
                            // Upload was aborted
                            uploadedFileInfo.Aborted = true;
                            uploadedFileInfo.ErrorMessage = FileTooLargeMessage;
                            uploadedFileInfo.Exception = new Exception("The file uploaded was too large.");
                        }
                        else
                        {
                            // Create a new instance of a 'FileInfo' object.
                            FileInfo fileInfo = new FileInfo(file.Name);
                            
                            // Set the extension. The ToLower is for just in case. I don't know if it's even possible for an extension to be upper case
                            uploadedFileInfo.Extension = fileInfo.Extension.ToLower();
                            
                            // if FilterByExtension is true and the AllowedExtensions text exists
                            if ((FilterByExtension) && (!String.IsNullOrWhiteSpace(AllowedExtensions)))
                            {
                                // verify the extension exists
                                if (!String.IsNullOrWhiteSpace(fileInfo.Extension))
                                {
                                    // If the allowed extensions // fixed issue where uploading 
                                    abort = !AllowedExtensions.ToLower().Contains(fileInfo.Extension.ToLower());
                                }
                                else
                                {
                                    // you must have an extension
                                    abort = true;
                                }
                            }
                            
                            // Set aborted to true
                            uploadedFileInfo.Aborted = abort;

                            // if we should continue
                            if (!abort)
                            {
                                // await for the data to be copied to the memory stream
                                stream = file.OpenReadStream(MaxFileSize);

                                // Check for abort 1 more time
                                uploadedFileInfo = CheckSize(fileInfo.Extension, stream, uploadedFileInfo);

                                // if abort
                                if (uploadedFileInfo.Aborted)
                                {
                                    // Do not process due to size is not valid, message has already been set
                                    abort = true;
                                }
                            }
                            
                            // if we should continue
                            if (!abort)
                            {
                                // if the value for SaveToDisk is true
                                if (SaveToDisk)
                                {
                                    // get the path
                                    string path = Path.Combine(UploadFolder, uploadedFileInfo.FullName);

                                    // Save tghe file
                                    SaveFileStream(path, stream);
                                }
                                else
                                {
                                    // set the memoryStream
                                    MemoryStream memoryStream = new MemoryStream();
                                    stream.CopyToAsync(memoryStream);

                                    // Set the MemoryStream, to allow people to save outside of the project 
                                    // folder, to disk or other processing like virus scan.
                                    uploadedFileInfo.Stream = memoryStream;
                                }
                                
                                // if there is a CustomSave
                                if (!String.IsNullOrWhiteSpace(CustomSuccessMessage))
                                {
                                    // Show the CustomSuccessMessage
                                    status = CustomSuccessMessage;
                                }
                                else
                                {
                                    // set the status
                                    status = $"Saved file {file.Size} bytes from {file.Name}";
                                }

                                // Set additional properties for UploadFileInfo from this component; these values may be null.
                                uploadedFileInfo.CustomId = CustomId;
                                uploadedFileInfo.Tag = Tag;

                                // The upload has completed
                                UploadComplete = true;
                            }
                            else
                            {
                                // If a CustomExtensionMessage has been set
                                if (!string.IsNullOrWhiteSpace(CustomExtensionMessage))
                                {
                                    // Display the Custom extension doesn't validate message
                                    uploadedFileInfo.ErrorMessage = CustomExtensionMessage;
                                }
                                else
                                {
                                    // Can't think of a better message than this yet, just woke up
                                    uploadedFileInfo.ErrorMessage = "The file uploaded is an invalid extension.";
                                }
                                
                                // Show the exception
                                uploadedFileInfo.Exception = new Exception(uploadedFileInfo.ErrorMessage);
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        // Upload was aborted
                        uploadedFileInfo.Aborted = true;
                        
                        // Store the Exception
                        uploadedFileInfo.Exception = error;
                        
                        // if a CustomErrorMessage is set
                        if (!String.IsNullOrWhiteSpace(CustomErrorMessage))
                        {
                            // Show the custom error message
                            status = CustomErrorMessage;
                        }
                        else
                        {
                            // show the full error
                            status = error.ToString();
                        }
                        
                        // set the error message
                        uploadedFileInfo.ErrorMessage = status;
                    }
                    finally
                    {
                        // Notify the caller the upload was successful or aborted due to an error 
                        FileUploaded(uploadedFileInfo);                        
                    }
                }
            }
            #endregion

            #region ResetFinished()
            /// <summary>
            /// This event is fired after a file is uploaded, and is used to notify subscribers of the OnChange event.
            /// </summary>
            private void ResetFinished()
            {  
                try
                {
                    // Notify the client the Reset button was clicked.
                    OnReset.InvokeAsync("Reset");
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }                
            }
            #endregion
            
            #region Reset()
            /// <summary>
            /// This method Reset
            /// </summary>
            public void Reset()
            {
                // Erase all messages
                Status = "";
                UploadComplete = false;
                
                // Notify any subscribers to this event
                ResetFinished();

                // Update the UI
                StateHasChanged();
            }
            #endregion

            #region SaveFileStream(String path, Stream stream)
            /// <summary>
            /// This method saves the stream
            /// </summary>
            /// <param name="path"></param>
            /// <param name="stream"></param>
            private void SaveFileStream(String path, Stream stream)
            {  
                var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                stream.CopyToAsync(fileStream);
                fileStream.Dispose();
            }
            #endregion
            
        #endregion

        #region Properties

            #region AllowedExtensions
            /// <summary>
            /// This property gets or sets the value for AllowedExtensions.
            /// Example: .jpg;.png;
            /// </summary>
            [Parameter]
            public string AllowedExtensions { get; set; } = "";
            #endregion
            
            #region AppendPartialGuid
            /// <summary>
            /// This property gets or sets the value for AppendPartialGuid.
            /// If true, a random number of digits will be appeneded to a filename to append uniqueness.
            /// </summary>
            [Parameter]
            public bool AppendPartialGuid { get; set; } = true;
            #endregion

            #region ButtonText
            /// <summary>
            /// This property gets or sets the value for 'ButtonText'.
            /// </summary>
            [Parameter]
            public string ButtonText
            {
                get { return buttonText; }
                set { buttonText = value; }
            }
            #endregion
            
            #region CustomButtonClassName
            /// <summary>
            /// This property gets or sets the value for 'CustomButtonClassName'.
            /// </summary>
            [Parameter]
            public string CustomButtonClassName
            {
                get { return customButtonClassName; }
                set { customButtonClassName = value; }
            }
            #endregion
            
            #region CustomButtonTextClassName
            /// <summary>
            /// This property gets or sets the value for 'CustomButtonTextClassName'.
            /// </summary>
            [Parameter]
            public string CustomButtonTextClassName
            {
                get { return customButtonTextClassName; }
                set { customButtonTextClassName = value; }
            }
            #endregion
            
            #region CustomErrorMessage
            /// <summary>
            /// This property gets or sets the value for CustomErrorMessage.
            /// Change this if you wish to display a different message.
            /// </summary>
            [Parameter]
            public string CustomErrorMessage { get; set; } = "An error occurred uploading your file.";
            #endregion
            
            #region CustomExtensionMessage
            /// <summary>
            /// This property gets or sets the value for CustomExtensionMessage.
            /// Example: 'Only .pdf files are allowed.'
            /// </summary>
            [Parameter]
            public string CustomExtensionMessage { get; set; } = "";
            #endregion

            #region CustomId
            /// <summary>
            /// This property gets or sets the value for RequireCustomIddSizeMessage.
            /// Optional. This property can be used to set information such as databaseId, parentId,
            /// categoryId or some other external classification. This value is returned with
            /// UploadFileInfo.
            /// </summary>
            [Parameter]
            public int CustomId { get; set; }
            #endregion

            #region CustomMaxHeightMessage
            /// <summary>
            /// This property gets or sets the value for CustomMaxHeightMessage.
            /// If an uploaded file is a .png or .jpg file and the file exceeds MaxHeight pixels, the StatusMessage
            /// will be set to CustomMaxHeightMessage. You must set CustomMaxHeightMessage and MaxHeight for this
            /// validation to work.
            /// </summary>
            [Parameter]
            public string CustomMaxHeightMessage { get; set; }
            #endregion

            #region CustomMaxWidthMessage
            /// <summary>
            /// This property gets or sets the value for CustomMaxWidthMessage.
            /// If an uploaded file is a .png or .jpg file and the file exceeds MaxWidth pixels, the StatusMessage
            /// will be set to CustomMaxWidthMessage. You must set CustomMaxWidthMessage and MaxWidth for this
            /// validation to work.
            /// </summary>
            [Parameter]
            public string CustomMaxWidthMessage { get; set; }
            #endregion

            #region CustomMinHeightMessage
            /// <summary>
            /// This property gets or sets the value for CustomMinHeightMessage.
            /// If an uploaded file is a .png or .jpg file and the file.Height is lower than MinHeight, the StatusMessage
            /// will be set to CustomMinHeightMessage. You must set CustomMinHeightMessage and MinHeight for this
            /// validation to work.
            /// </summary>
            [Parameter]
            public string CustomMinHeightMessage { get; set; }
            #endregion

            #region CustomMinWidthMessage
            /// <summary>
            /// This property gets or sets the value for CustomMinWidthMessage.
            /// If an uploaded file is a .png or .jpg file and the file.Width is lower than MinWidth, the StatusMessage
            /// will be set to CustomMinWidthMessage. You must set CustomMinWidthMessage and MinWidth for this
            /// validation to work.
            /// </summary>
            [Parameter]
            public string CustomMinWidthMessage { get; set; }
            #endregion

            #region CustomRequiredSizeMessage
            /// <summary>
            /// This property gets or sets the value for CustomRequiredSizeMessage.
            /// Used in conjunction with RequiredHeight and / or Required Width, If RequiredSizeMessage
            /// and an uploaded file that is a .png or .jpg file is not the correct size, the StatusMessage
            /// will be set to this value.
            /// </summary>
            [Parameter]
            public string CustomRequiredSizeMessage { get; set; }
            #endregion
            
            #region CustomSuccessMessage
            /// <summary>
            /// This property gets or sets the value for CustomSuccessMessage.
            /// Example: 'Congratulations, your file has been uploaded and is being processed.'
            /// </summary>
            [Parameter]
            public string CustomSuccessMessage { get; set; } = "";
            #endregion
            
            #region FileTooLargeMessage
            /// <summary>
            /// This property gets or sets the value for FileTooLargeMessage.
            /// Example: 'Files must be 4 megabytes or smaller.'
            /// </summary>
            [Parameter]
            public string FileTooLargeMessage { get; set; } = "The file uploaded is too large";
            #endregion
            
            #region FilterByExtension
            /// <summary>
            /// This property gets or sets the value for FilterByExtension.
            /// If true, only files with a matching extension will be allowed.
            /// </summary>
            [Parameter]
            public bool FilterByExtension { get; set; } = false;
            #endregion
            
            #region HasCustomMaxHeightMessage
            /// <summary>
            /// This property returns true if this object has a 'CustomMaxHeightMessage' set.
            /// </summary>
            public bool HasCustomMaxHeightMessage
            {
                get
                {
                    // initial value
                    bool hasCustomMaxHeightMessage = (!String.IsNullOrEmpty(this.CustomMaxHeightMessage));
                    
                    // return value
                    return hasCustomMaxHeightMessage;
                }
            }
            #endregion

            #region HasCustomMaxWidthMessage
            /// <summary>
            /// This property returns true if this object has a 'CustomMaxWidthMessage'.
            /// </summary>
            public bool HasCustomMaxWidthMessage
            {
                get
                {
                    // initial value
                    bool hasCustomMaxWidthMessage = (!String.IsNullOrEmpty(CustomMaxWidthMessage));
                    
                    // return value
                    return hasCustomMaxWidthMessage;
                }
            }
            #endregion
            
            #region HasCustomMinHeightMessage
            /// <summary>
            /// This property returns true if this object has a 'CustomMinHeightMessage'.
            /// </summary>
            public bool HasCustomMinHeightMessage
            {
                get
                {
                    // initial value
                    bool hasCustomMinHeightMessage = (!String.IsNullOrEmpty(this.CustomMinHeightMessage));
                    
                    // return value
                    return hasCustomMinHeightMessage;
                }
            }
            #endregion
            
            #region HasCustomMinWidthMessage
            /// <summary>
            /// This property returns true if this object has a 'CustomMinWidthMessage'.
            /// </summary>
            public bool HasCustomMinWidthMessage
            {
                get
                {
                    // initial value
                    bool hasCustomMinWidthMessage = (!String.IsNullOrEmpty(CustomMinWidthMessage));
                    
                    // return value
                    return hasCustomMinWidthMessage;
                }
            }
            #endregion
            
            #region HasCustomRequiredSizeMessage
            /// <summary>
            /// This property returns true if this object has a 'CustomRequiredSizeMessage'.
            /// </summary>
            public bool HasCustomRequiredSizeMessage
            {
                get
                {
                    // initial value
                    bool hasCustomRequiredSizeMessage = (!String.IsNullOrEmpty(CustomRequiredSizeMessage));
                    
                    // return value
                    return hasCustomRequiredSizeMessage;
                }
            }
            #endregion

            #region HasMaxHeight
            /// <summary>
            /// This property returns true if MaxHeight is greater than 0.
            /// </summary>
            public bool HasMaxHeight
            {
                get
                {
                    // initial value
                    bool hasMaxHeight = (this.MaxHeight > 0);
                    
                    // return value
                    return hasMaxHeight;
                }
            }
            #endregion
            
            #region HasMaxWidth
            /// <summary>
            /// This property returns true if  'MaxWidth' is greater than 0.
            /// </summary>
            public bool HasMaxWidth
            {
                get
                {
                    // initial value
                    bool hasMaxWidth = (this.MaxWidth > 0);
                    
                    // return value
                    return hasMaxWidth;
                }
            }
            #endregion
            
            #region HasMinHeight
            /// <summary>
            /// This property returns true if MinHeight is greater than 0.
            /// </summary>
            public bool HasMinHeight
            {
                get
                {
                    // initial value
                    bool hasMinHeight = (this.MinHeight > 0);
                    
                    // return value
                    return hasMinHeight;
                }
            }
            #endregion
            
            #region HasMinWidth
            /// <summary>
            /// This property returns true if  'MinWidth' is greater than 0.
            /// </summary>
            public bool HasMinWidth
            {
                get
                {
                    // initial value
                    bool hasMinWidth = (this.MinWidth > 0);
                    
                    // return value
                    return hasMinWidth;
                }
            }
            #endregion
            
            #region HasRequiredHeight
            /// <summary>
            /// This property returns true if this object has a 'RequiredHeight'.
            /// </summary>
            public bool HasRequiredHeight
            {
                get
                {
                    // initial value
                    bool hasRequiredHeight = (this.RequiredHeight != 0);
                    
                    // return value
                    return hasRequiredHeight;
                }
            }
            #endregion
            
            #region HasRequiredWidth
            /// <summary>
            /// This property returns true if this object has a 'RequiredWidth' set.
            /// </summary>
            public bool HasRequiredWidth
            {
                get
                {
                    // initial value
                    bool hasRequiredWidth = (this.RequiredWidth != 0);
                    
                    // return value
                    return hasRequiredWidth;
                }
            }
            #endregion
            
            #region HasStatus
            /// <summary>
            /// This read only property returns true if the 'Status' text exists.
            /// </summary>
            public bool HasStatus
            {
                get
                {
                    // initial value
                    bool hasStatus = (!String.IsNullOrEmpty(this.Status));
                    
                    // return value
                    return hasStatus;
                }
            }
            #endregion

            #region InputFileClassName
            /// <summary>
            /// This property gets or sets the value for 'InputFileClassName'.
            /// </summary>
            [Parameter]
            public string InputFileClassName
            {
                get { return inputFileClassName; }
                set { inputFileClassName = value; }
            }
            #endregion
            
            #region MaxFileSize
            /// <summary>
            /// This property gets or sets the value for MaxFileSize.
            /// This property is the size in bytes. Example: 4194304 (4 megs).
            /// </summary>
            [Parameter]
            public long MaxFileSize { get; set; } = 0;
            #endregion

            #region MaxHeight
            /// <summary>
            /// This property gets or sets the value for MaxHeight.
            /// If MaxHeight is set, any image files exceeding the height of this value
            /// will be rejected. Note: This only works for .jpg and .png files.
            /// </summary>
            [Parameter]
            public int MaxHeight { get; set; }
            #endregion

            #region MaxWidth
            /// <summary>
            /// This property gets or sets the value for MaxWidth.
            /// If MaxWidth is set, any image files exceeding the Width of this value
            /// will be rejected. Note: This only works for .jpg and .png files.
            /// </summary>
            [Parameter]
            public int MaxWidth { get; set; }
            #endregion

            #region MessageClassName
            /// <summary>
            /// This property gets or sets the value for 'MessageClassName'.
            /// </summary>
            [Parameter]
            public string MessageClassName
            {
                get { return messageClassName; }
                set { messageClassName = value; }
            }
            #endregion
            
            #region MinHeight
            /// <summary>
            /// This property gets or sets the value for MinHeight.
            /// If MinHeight is set, any image files less than the height of this value
            /// will be rejected. Note: This only works for .jpg and .png files.
            /// </summary>
            [Parameter]
            public int MinHeight { get; set; }
            #endregion

            #region MinWidth
            /// <summary>
            /// This property gets or sets the value for MinWidth.
            /// If MinWidth is set, any image files less than the Width of this value
            /// will be rejected. Note: This only works for .jpg and .png files.
            /// </summary>
            [Parameter]
            public int MinWidth { get; set; }
            #endregion
            
            #region OnChange
            /// <summary>
            /// This property gets or sets the value for OnChange.
            /// This event will get called after the file is uploaded.
            /// The parameter UploadFileInfo contains information about the uploaded file.
            /// </summary>
            [Parameter] public EventCallback<UploadedFileInfo> OnChange { get; set; }
            #endregion

            #region OnReset
            /// <summary>
            /// This property gets or sets the value for OnReset.
            /// This event will get called after the user clicks the Reset button.
            /// This is needed so clients can reset or adjust the UI when the Reset button is clicked.
            /// </summary>
            [Parameter] public EventCallback<string> OnReset { get; set; }
            #endregion
            
            #region PartialGuidLength
            /// <summary>
             /// This property gets or sets the value for PartialGuidLength.
             /// If append partial guid is true, this value is the number of characters 
             /// appended to the filename.
             /// Example: PartialGuidLength = 10.
             /// Uploaded FileName: myphoto.472e205c-1.jpg'
            /// </summary>
            [Parameter]
            public int PartialGuidLength { get; set; } = 12;
            #endregion

            #region RequiredHeight
            /// <summary>
            /// This property gets or sets the value for RequiredHeight.
            /// If RequiredHeight is set, any files not matching the exact size
            /// will be rejected. Note: This only works for .jpg and .png files.
            /// </summary>
            [Parameter]
            public int RequiredHeight { get; set; }
            #endregion

            #region RequiredWidth
            /// <summary>
            /// This property gets or sets the value for RequiredWidth.
            /// If RequiredWidth is set, any files not matching the exact size
            /// will be rejected. Note: This only works for .jpg and .png files.
            /// </summary>
            [Parameter]
            public int RequiredWidth { get; set; }
            #endregion

            #region ResetButtonClassName
            /// <summary>
            /// This property gets or sets the value for 'ResetButtonClassName'.
            /// </summary>
            [Parameter]
            public string ResetButtonClassName
            {
                get { return resetButtonClassName; }
                set { resetButtonClassName = value; }
            }
            #endregion
            
            #region ResetButtonText
            /// <summary>
            /// This value will be shown on a button after an upload is complete provided that
            /// ShowResetButton is set. The default value for ResetButtonText is 'Reset'.
            /// </summary>
            [Parameter] public string ResetButtonText { get; set; } = "Reset";
            #endregion

            #region SaveToDisk
            /// <summary>
            /// This property gets or sets the value for 'SaveToDisk'.
            /// </summary>
            [Parameter]
            public bool SaveToDisk
            {
                get { return saveToDisk; }
                set { saveToDisk = value; }
            }
            #endregion
            
            #region ShowCustomButton
            /// <summary>
            /// This property gets or sets the value for 'ShowCustomButton'.
            /// </summary>
            [Parameter]
            public bool ShowCustomButton
            {
                get { return showCustomButton; }
                set 
                { 
                    // set the value
                    showCustomButton = value;

                    // if showCustomButton is true
                    if (showCustomButton)
                    {
                        // change to custom button
                        inputFileClassName = "customfileupload";
                    }
                }
            }
            #endregion
            
            #region ShowResetButton
            /// <summary>
            /// This property gets or sets the value for ShowResetButton.
            /// If ShowResetButton is true, a button will be shown that 
            /// reads the text of ResetButtonText. The default value for
            /// ResetButtonText is 'Reset'.
            /// </summary>
            [Parameter]
            public bool ShowResetButton { get; set; } = true;
            #endregion
            
            #region ShowStatus
            /// <summary>
            /// This property gets or sets the value for ShowStatus.
            /// If ShowStatus is true, messages are shown to a user. 
            /// You can override this by setting it false if you want to 
            /// handle your own messages.
            /// </summary>
            [Parameter]
            public bool ShowStatus { get; set; } = true;
            #endregion
            
            #region Status
            /// <summary>
            /// This property gets or sets the value for 'Status'.
            /// </summary>
            [Parameter]
            public string Status
            {
                get { return status; }
                set { status = value; }
            }
            #endregion

            #region Tag
            /// <summary>
            /// This property gets or sets the value for Tag.
            /// Optional. This property can be used to set information to help
            /// name or classify uploaded files. This value is returned with
            /// UploadFileInfo.
            /// </summary>
            [Parameter]
            public string Tag { get; set; }
            #endregion
            
            #region UploadComplete
            /// <summary>
            /// This property gets or sets the value for 'UploadComplete'.
            /// </summary>
            public bool UploadComplete
            {
                get { return uploadComplete; }
                set { uploadComplete = value; }
            }
            #endregion
            
            #region UploadFolder
            /// <summary>
             /// This property gets or sets the value for UploadFolder.
             /// The default folder is wwwroot/Upload, but you may override this if you like:
             /// Example: UploadFolder="Images\Gallery\Artists\"
            /// </summary>
            [Parameter]
            public string UploadFolder { get; set; } = "wwwroot/Upload/";
            #endregion
            
            #region Visible
            /// <summary>
            /// This property gets or sets the value for 'Visible'.
            /// </summary>
            [Parameter]
            public bool Visible
            {
                get { return visible; }
                set { visible = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
