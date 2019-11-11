# BlazorFileUpload
This is a wrapper of Steve Sanderson's BlazorFileInput:

https://github.com/SteveSandersonMS/BlazorInputFile  

<b>You must use Dot Net Core SDK 3.1 for BlazorFileUpload component to work.</b>

I am using Visual Studio 16.4 Preview 4.

<b>Nuget Package: DataJuggler.Blazor.FileUpload</b>

All the credit goes to Steve Sanderson's BlazorFileInput component. I created this project so I can create a Blazor project and use all Dot Net Core components without using Dot Net Standard, so I can create a Blazor UI Factory that has been on my to do list.

This repo contains a working sample:
https://github.com/DataJuggler/BlazorFileUpload

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

If you like this, please subscribe to my YouTube channel:

https://www.youtube.com/channel/UCaw0joqvisKr3lYJ9Pd2vHA

www.datajuggler.com


