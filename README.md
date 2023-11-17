# BlazorFileUpload

This project was originally a wrapper of Steve Sanderson's BlazorFileInput, but now has been
updated to the InputFile .NET component. This project has been updated to .NET 8.

Blazor Gallery is a C#, Blazor, SQL Server image portfolio site, allowing anyone to create up to
five folders with up to 20 images per file.

Live Demo: https://blazorgallery.com

To see a complete working example, with source code please visit:

Blazor Gallery
https://github.com/DataJuggler/BlazorGallery

Blazor Gallery can also be installed as a dotnet cli project: 
(You may change the top line to any directory you wish)

    cd c:\Projects\BlazorGallery
    dotnet new install DataJuggler.BlazorGallery
    dotnet new DataJuggler.BlazorGallery

Another complete working example, with source code please visit:

<img src=https://excelerate.datajuggler.com/Images/ExcelerateLogoSmallWhite.png height=128 width=128>
<img src=https://excelerate.datajuggler.com/Images/logotextsparkled.png>

Blazor Excelerate <br />
https://excelerate.datajuggler.com <br />
Code Generate C# Classes From Excel Header Rows

The source code for the above project is available at:

https://github.com/DataJuggler/Blazor.Excelerate

Here is an example of creating a file upload component:

    @using DataJuggler.Blazor.FileUpload

    <FileUpload CustomSuccessMessage="Your file uploaded successfully." 
        OnReset="OnReset" ResetButtonClassName="localbutton" ShowStatus="false"
        PartialGuidLength="12" MaxFileSize=@UploadLimit FilterByExtension="true" 
        ShowCustomButton="true"  ButtonText="Upload Excel" OnChange="OnFileUploaded"
        CustomButtonClassName="@OrangeButton" AllowedExtensions=".xlsx"
        ShowResetButton="false" AppendPartialGuid="true"
        CustomExtensionMessage="Only .xlsx extensions are allowed." 
        InputFileClassName="customfileupload" Visible=false Status="Refresh"
        FileTooLargeMessage=@FileTooLargeMessage>
    </FileUpload>
    

To handle the File Upload event 'OnFileUploaded'. The code shown also starts a progress bar timer and reads the sheet names
using Nuget package DataJuggler.Excelerate (the Nuget package that powers Blazor Excelerate). 

    #region OnFileUploaded(UploadedFileInfo file)
    /// <summary>
    /// This method On File Uploaded
    /// </summary>
    public void OnFileUploaded(UploadedFileInfo file)
    {
        // if the file was uploaded
        if (!file.Aborted)
        {
            // Show the Progressbar
            ShowProgress = true;
            
            // if the ProgressBar
            if (HasProgressBar)
            {
                // Start the Timer
                ProgressBar.Start();
            }
            
            // Create a model
            GetSheetNamesModel model = new GetSheetNamesModel();
            
            // Set the model
            model.FullPath = file.FullPath;
            
            // Store this for later
            ExcelPath = file.FullPath;
            
            // reload the model
            HandleDiscoverSheets(model);
        }
        else
        {
            // for debugging only
            if (file.HasException)
            {
                // for debugging only
                string message = file.Exception.Message;
            }
        }
    }
    #endregion
    
Updates

11.17.2023: This project has been updated to .NET8.

8.13.2023: DataJuggler.Blazor.Components was updated because DataJuggler.UltimateHelper was updated.

version 7.1.0
7.2.2023: DataJuggler.BlazorFileUpload now supports multiple file uploads.

7.1.2
7.22.2023: DataJuggler.Blazor.Components was updated.