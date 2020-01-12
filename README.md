# BlazorFileUpload
This is a wrapper of Steve Sanderson's BlazorFileInput:

https://github.com/SteveSandersonMS/BlazorInputFile  

<b>You must use Dot Net Core SDK 3.1 for BlazorFileUpload component to work.</b>

I am using Visual Studio 16.4.2, but any version 16.4+ should be fine.

<b>Nuget Package: DataJuggler.Blazor.FileUpload</b>

All the credit goes to Steve Sanderson's BlazorFileInput component. I created this project so I can create a Blazor project and use all Dot Net Core components without using Dot Net Standard, so I can create a Blazor UI Factory that has been on my to do list.

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

    private void OnFileUploaded(string fileName)
    {
        status = "The file " + fileName + " was uploaded.";
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

Update 1.12.2020: Documentation includes New Features for version 1.2.1:

Parameters / Properties:

# string AllowedExtensions - (Optional)
This optional parameter is a semicolon delimited list of extensions that If set, allow you to filter types of files that may be uploaded.
This is used in conjunction with the CustomExtensionMessage.
Example: AllowedExtensions=".jpg;.png;"

# bool AppendPartialGuid - Default Value = true
If true, the file name of the uploaded file will be changed to include some random characters to ensure uniqueness in case
a user or users uploads two files with the same name.
This is used in conjunction with the PartialGuidLength property. 
Example: AppendPartialGuid="true". The file uploaded will be changed to something similar to: myphoto.dl30xm37-sk7.png.

# string ButtonClassName - (Optional) - Defaults to 'button'
This property gets or sets the value for ButtonClassName. If ShowResetButton is true, this value is used to allow you to customize
the Reset button's appearance by setting a CSS class name.
Example: ButtonClassName="woodbutton". 

# string CustomErrorMessage - (Optional) Default Value = "An error occurred uploading your file.";
If an error occurs uploading a file, this message will be shown to the user.
This property could be expanded to have CSS properties to allow you to change it.
Example: CustomErrorMessage="Oops, something went wrong. Please try again or a different file."

# int CustomId - (Optional) 
This property allows you to set a value such as a databaseId, parentId, categoryId, etc. as a way to classify the uploaded file.
This value is returned with UploadFileInfo after the file is uploaded, if set.
Example: CustomId="@SelectedPlayerId" (where @SelectedPlayerId is a variable available to the current scope).

# CustomMaxHeightMessage (Optional)
This property allows you to set a message to handle uploaded image files that are larger than the bounds you specified in MaxHeight.
This applies only to Image files that .png and .jpg extensions for now. Contact me if that is an issue for you.
The MaxHeight value and CustomMaxHeightMessage must be both be set for this validation to work.
The upload is aborted if both of these values are set, and the system can determine that the height is greater than allowed.
This is new code and untested, please report the good, the bad and the ugly.
Example: CustomMaxHeightMessage="The image uploaded exceeds the maximum height of 960 pixels.".

# CustomMinHeightMessage (Optional)
This property allows you to set a message to handle uploaded image files that are smaller than the bounds you specified in MinHeight.
This applies only to Image files that .png and .jpg extensions for now. Contact me if that is an issue for you.
The MixHeight value and CustomMixHeightMessage must be both be set for this validation to work.
The upload is aborted if both of these values are set, and the system can determine that the height is smaller than allowed.
This is new code and untested, please report the good, the bad and the ugly.
Example: CustomMinHeightMessage="The image uploaded must have a height of atleast 640 pixels".

# CustomRequiredSizeMessage
This property allows you to specify a message to handle uploaded image files that must be EXACTLY the bound you specifie in 
RequiredHeight and RequiredWidth. This applies only to Image files that .png and .jpg extensions for now. Contact me if that is an issue for you. The MixHeight value and CustomMixHeightMessage must be both be set for this validation to work.
The upload is aborted if either validation fails for RequiredHeight or RequiredWidth, and if this message is set, and the system can determine that the height and / or widht is different than the required value you specified.
This is new code and untested, please report the good, the bad and the ugly.
Example: CustomMinHeightMessage="The image uploaded must have a height of atleast 640 pixels".

# More To Come, This Is A Work In Progress

If you like this project, please subscribe to my YouTube channel:

https://www.youtube.com/channel/UCaw0joqvisKr3lYJ9Pd2vHA

www.datajuggler.com


