# BlazorFileUpload
This is a wrapper of Steve Sanderson's BlazorFileInput: https://github.com/SteveSandersonMS/BlazorInputFile

Update 10.24.2021:

# Blazor Excelerate
I have a new full Blazor Sample available called Blazor Excelerate

https://github.com/DataJuggler/Blazor.Excelerate

Live site:
https://excelerate.datajuggler.com/

Blazor Excelerate uses this Nuget package (DataJuggler.BlazorFileUpload) to upload Excel files.

Blazor Excelerate also uses Nuget package DataJuggler.Excelerate to code generate C# classes from Excel Header rows.

Update 2.23.2020: I created a new sample project that saves data including the path to upload profile pictures and images to SQL Server. The sample code is located here:

Blazor Image Gallery: https://github.com/DataJuggler/BlazorImageGallery

This samples takes a little more work to get setup, as you must:
1. Create a SQL Server database and execute a sql script to create the two tables and stored procedures
2. Build a connectionstring and paste the value into a Systen environment variable.

There also a complete tutorial on blogger: https://datajugglerblazor.blogspot.com/2020/02/building-blazor-image-gallery-complete.html

And also a video

Building Blazor Image Gallery
https://youtu.be/3xKXJQ4qThQ

Breaking Change Version 1.3.2: ButtonClassName is now CustomButtonClassName. I was confusing ResetButtonClassName with ButtonClassName, so I renamed ButtonClassName to CustomButtonClassName, and ResetButtonClassName is used only for the Reset button.

<b>This project has now been converted for .Net 5.</b>

Update 1.16.2021:
I am using Visual Studio 16.8.4, but any version 16.8+ should be fine.

<b>Nuget Package: DataJuggler.Blazor.FileUpload</b>

All the credit goes to Steve Sanderson's BlazorFileInput component. I created this project so I can create a Blazor project and use all Dot Net Core components without using Dot Net Standard.

This repo contains a working sample:
https://github.com/DataJuggler/BlazorFileUpload

Quick Usage:

For usage, create a new Blazor project.

Add a reference to DataJuggler.Blazor.FileUpload Nuget Package, or reference the projects as the Sample does.
In the Pages folder of the new Blazor project, Modiify _Host.cshtml to have the following reference:

    <script src="_content/BlazorInputFile/inputfile.js"></script>

Place the above reference directly above the reference to blazor.server.js:

    <script src="_framework/blazor.server.js"></script>
<br/><br/>
Replace the existing text in Index.razor with the code below:

    @page "/"
    @using DataJuggler.Blazor.FileUpload

    <h3>File Upload Test</h3>

    <div class="fileuploader">
        <FileUpload CustomSuccessMessage="Your file uploaded successfully." OnChange="OnFileUploaded"></FileUpload>
        @status
    </div>

    @code
    {
    // display the filename after upload
    string status;

    private void OnFileUploaded(UploadedFileInfo uploadedFileInfo)
    {   
         // get the status
         status = "The file " + uploadedFileInfo.FullName + " was uploaded.";

         // showUploadButton is now false until the Reset button is clicked.
         showUploadButton = false;
         
          // other information about the file is available
          //DateTime lastModified = uploadedFileInfo.LastModified;
          //string nameAsItIsOnDisk = uploadedFileInfo.NameWithPartialGuid;
          //string partialGuid = uploadedFileInfo.PartialGuid;
          //long size = uploadedFileInfo.Size;
          //string type = uploadedFileInfo.Type;
    }
}

In the new Blazor project you created, in the wwwroot folder, open the file site.css and add this entry on a couple of lines below Import bootstrap line:
.fileuploader
{
font-family: Verdana;
font-size: 12px;
}

Also in the wwwroot folder, create a new folder called Upload.
That's it! You are ready to use BlazorFileUploader.

# Update 1.12.2020: Documentation
Includes New Features for version 1.2.1.
<br>
# Parameters / Properties:

# string AllowedExtensions - (Optional)
This optional parameter is a semicolon delimited list of extensions that If set, allow you to filter types of files that may be uploaded.
This is used in conjunction with the CustomExtensionMessage.
Example: AllowedExtensions=".jpg;.png;"

Note: The AllowedExtensions is a filter for after the file has uploaded, the extension is checked and if the extension is not included the upload is aborted. This does not filter the file dialog browser.

# bool AppendPartialGuid - Default Value = true
If true, the file name of the uploaded file will be changed to include some random characters to ensure uniqueness in case
a user or users uploads two files with the same name.
This is used in conjunction with the PartialGuidLength property. 
Example: AppendPartialGuid="true". The file uploaded will be changed to something similar to: myphoto.dl30xm37-sk7.png.

# string CustomButtonClassName - (Optional) - Defaults to 'button'
This property gets or sets the value for CustomButtonClassName. If ShowCustomButton is true, this value is used to allow you to customize
the button's appearance by setting a CSS class name.
Example: CustomButtonClassName="buttonwide". 

# string CustomErrorMessage - (Optional) Default Value = "An error occurred uploading your file.";
If an error occurs uploading a file, this message will be shown to the user.
This property could be expanded to have CSS properties to allow you to change it.
Example: CustomErrorMessage="Oops, something went wrong. Please try again or a different file."

# int CustomId - (Optional) 
This property allows you to set a value such as a databaseId, parentId, categoryId, etc. as a way to classify the uploaded file.
This value is returned with UploadFileInfo after the file is uploaded, if set.
Example: CustomId="@SelectedPlayerId" (where @SelectedPlayerId is a variable available to the current scope).

# string CustomMaxHeightMessage (Optional)
This property allows you to set a message to handle uploaded image files that are larger than the bounds you specified in MaxHeight.
This applies only to Image files that .png and .jpg extensions for now. Contact me if that is an issue for you.
The MaxHeight value and CustomMaxHeightMessage must be both be set for this validation to work.
The upload is aborted if both of these values are set, and the system can determine that the height is greater than allowed.
This is new code and untested, please report the good, the bad and the ugly.
Example: CustomMaxHeightMessage="The image uploaded exceeds the maximum height of 960 pixels.".

# string CustomMinHeightMessage (Optional)
This property allows you to set a message to handle uploaded image files that are smaller than the bounds you specified in MinHeight.
This applies only to Image files that .png and .jpg extensions for now. Contact me if that is an issue for you.
The MixHeight value and CustomMixHeightMessage must be both be set for this validation to work.
The upload is aborted if both of these values are set, and the system can determine that the height is smaller than allowed.
This is new code and untested, please report the good, the bad and the ugly.
Example: CustomMinHeightMessage="The image uploaded must have a height of atleast 640 pixels".

# string CustomRequiredSizeMessage (Optional)
This property allows you to specify a message to handle uploaded image files that must be EXACTLY the bound you specifie in 
RequiredHeight and RequiredWidth. This applies only to Image files that .png and .jpg extensions for now. Contact me if that is an issue for you. The MixHeight value and CustomMixHeightMessage must be both be set for this validation to work.
The upload is aborted if either validation fails for RequiredHeight or RequiredWidth, and if this message is set, and the system can determine that the height and / or widht is different than the required value you specified.
This is new code and untested, please report the good, the bad and the ugly.
Example: CustomMinHeightMessage="The image uploaded must have a height of atleast 640 pixels".

# string CustomSuccessMessage (Optional)
This property gets or sets the value for CustomSuccessMessage.
Example: CustomSuccessMessage = 'Congratulations, your file has been uploaded and is being processed.'

# string FileTooLargeMessage
This property gets or sets the value for FileTooLargeMessage.
This property is used in conjuction with the MaxFileSize property.
Example: 'Files must be 4 megabytes or smaller.'

# bool FilterByExtension
This property gets or sets the value for FilterByExtension.
If true, only files with a matching extension will be allowed.
Example: FilterByExtension="true"

# bool HasCustomMaxHeightMessage
This read only property returns true if this object has a 'CustomMaxHeightMessage' set.
  
# bool HasCustomMinHeightMessage
This read only property returns true if this object has a 'CustomMinHeightMessage' set.
  
# bool HasCustomRequiredSizeMessage
This read only property returns true if this object has a 'CustomRequiredSizeMessage' set.
  
# bool HasMaxHeight
This read only property returns true if this object has a 'MaxHeight' value set.
  
# bool HasMaxWidth
This read only property returns true if this object has a 'MaxWidth' value set.
  
# bool HasMinHeight
This read only property returns true if this object has a 'MinHeight' value set.
  
# bool HasMinWidth
This read only property returns true if this object has a 'MinWidth' value set.
  
# bool HasRequiredHeight
This read only property returns true if this object has a 'RequiredHeight' value set.
  
# bool HasRequiredWidth
This read only property returns true if this object has a 'RequiredWidth' value set.

# bool HasStatus
This read only property returns true if this object has a 'Status' value set.

# int MaxFileSize
This property gets or sets the value for MaxFileSize. This property is the size in bytes.
This property is used in conjunction with the FileTooLargeMessage property.
Example: MaxFileSize=4194304 (4 megs).

# MaxHeight
This property gets or sets the value for MaxHeight. This property only applies to Image files, .png's and .jpg's for now.
If true, files will be rejected if the height in pixels exceeds this value. This property is used in conjunction with the
property 'CustomMaxHeightMessage'. Both of these values must be set for this validation to work.
Example: MaxHeight=960. 

# MaxWidth
This property gets or sets the value for MaxWidth. This property only applies to Image files, .png's and .jpg's for now.
If true, files will be rejected if the width in pixels exceeds this value. This property is used in conjunction with the
property 'CustomMaxWidthMessage'. Both of these values must be set for this validation to work.
Example: MaxHeight=960. 

# MinHeight
This property gets or sets the value for MinHeight. If MinHeight is set, any image files less than the height of this value
will be rejected. This property works in conjunction with the CustomMinHeightMessage property.
Note: This only works for .jpg and .png files. 
Example: MinHeight="400"

# MinWidth
This property gets or sets the value for MinWidth.
If MinWidth is set, any image files less than the Width of this value
will be rejected. This property works in conjunction with the CustomMinWidthMessage property.
Note: This only works for .jpg and .png files.
Example: MinWidth="640"

# OnChange
This property gets or sets the delegate that will be called after a file is uploaded.
This method returns an UploadFileInfo object which contains information about the file that was uploaded.
Example: OnChange="OnFileUploaded"

# OnReset (Optional)
This property gets or sets the delegate that will be called after the Reset button is clicked.
This can be used to change a displayed image or any other UI changes needed after a file is removed.
Example: OnReset="MyOnResetMethod"

    private void MyOnResetMethod(string notUsedButRequiredArg)
    {
        // erase status
        status = "";

        // toggle to true
        showUploadButton = true;

        // Refresh the UI
        StateHasChanged();
    }

# PartialGuidLength
This property specifies how many characters of a PartialGuid will be added to ensure uniqueness.
Values range from 1-32, but a minimum of 8 is recommended if you are to use this property.
Example: PartialGuidLength="10"

# RequiredHeight
This property gets or sets the value for RequiredHeight.
If RequiredHeight is set, any files not matching the exact size
will be rejected. Note: This only works for .jpg and .png files.
This property is used in conjunction with the property CustomRequiredSizeMessage.
Example: RequiredHeight="256"

# RequiredWidth
This property gets or sets the value for RequiredWidth.
If RequiredWidth is set, any files not matching the exact size
will be rejected. Note: This only works for .jpg and .png files.
This property is used in conjunction with the property CustomRequiredSizeMessage.
Example: RequiredWidth="512"

# ResetButtonText
This property gets or sets the text for the button that displays after an upload.
The default text is 'Reset', but you may change it using this property.
This property is used in conjunction with ShowResetButton.
Use the property ButtonClassName to set a CSS class to style the button.
Example: ResetButtonText="New".

# ShowResetButton
If true, this button will show after an upload completes. This is used to reset the file upload control.
This is brand new code and needs to be tested. The default value is true, but you may turn it off.
Example: ShowResetButton="false"

# ShowStatus
This property gets or sets the value for ShowStatus.
If true, any messages will be shown to the user. True is the default value.
Example: ShowStatus="false".

# Status
This property gets or sets the value for Status.
Any validation messages or other messages will be set to this value.
Example: Status="Select A Photo For Your Profile."

# Tag
This property gets or sets the value for Tag.
This optional parameter property can be used to set information to help
name or classify uploaded files. This value is returned with UploadFileInfo.
Example: Tag="New User Signup Profile Picture".

# UploadComplete
The system will handle setting this property, as after a file has been uploaded this value will be set to true.

# UploadFolder
This property gets or sets the value for UploadFolder.
The default folder is wwwroot/Upload, but you may override this if you like:
Example: UploadFolder="Images\Gallery\Artists\"

Let me know if you have any questions or anything was not explained very well. 

If you like this project, please subscribe to my YouTube channel:

https://www.youtube.com/channel/UCaw0joqvisKr3lYJ9Pd2vHA

www.datajuggler.com


