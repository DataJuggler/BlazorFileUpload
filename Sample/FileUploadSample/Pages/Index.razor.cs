

#region using statements

using DataJuggler.Blazor.FileUpload;
using System.IO;
using System;
using System.Collections.Generic;
using DataJuggler.UltimateHelper;
using DataJuggler.UltimateHelper.Objects;
using Microsoft.AspNetCore.Components;

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
        private MarkupString xmlString;
        private List<XmlLine> lines;
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

            #region OnFileUploaded2(UploadedFileInfo uploadedFileInfo)
            /// <summary>
            /// This method is used to handle a MemoryStream returned from the uploaded file,
            /// and the memory stream is going to be attempted to be converted to an Xml string.
            /// </summary>
            private void OnFileUploaded2(UploadedFileInfo uploadedFileInfo)
            {
                try
                {
                    // if aborted
                    if (uploadedFileInfo.Aborted)
                    {
                        // get the status
                        XmlString = (MarkupString) uploadedFileInfo.ErrorMessage;
                    }
                    else
                    {
                        // if the MemoryStream was uploaded
                        if (uploadedFileInfo.HasStream)
                        {
                            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                            string temp = encoding.GetString(uploadedFileInfo.Stream.ToArray());

                            // Get the lines
                            List<TextLine> lines = WordParser.GetTextLines(temp.TrimEnd());

                            // If the lines collection exists and has one or more items
                            if (ListHelper.HasOneOrMoreItems(lines))
                            {
                                // local
                                int count = 0;

                                // Create the lines
                                this.Lines = new List<XmlLine>();

                                // Iterate the collection of TextLine objects
                                foreach (TextLine line in lines)
                                {
                                    if (TextHelper.Exists(line.Text))
                                    {
                                        // cast the XmlLine as a Line
                                        XmlLine xmlLine = new XmlLine();

                                        // Set the properties
                                        xmlLine.Text = line.Text;

                                        // not the first line or the last line
                                        if (count != 0)
                                        {
                                            // Indent
                                            xmlLine.Indent = true;
                                        }
                                    
                                        // Add this line
                                        this.Lines.Add(xmlLine);
                                        
                                        // increment the count
                                        count++;
                                    }
                                }

                                // Make sure we indent
                                this.Lines[this.Lines.Count - 1].Indent = false;
                            }
                        }
                    }
                }
                catch (Exception error)
                {   
                    // for debugging only for this sample
                    string err = error.ToString();
                }
                finally
                {
                    // Might be needed ?
                    StateHasChanged();
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
                Lines = null;

                // Refresh the UI
                StateHasChanged();
            }
            #endregion
            
        #endregion

        #region Properties

            #region HasLines
            /// <summary>
            /// This property returns true if this object has a 'Lines'.
            /// </summary>
            public bool HasLines
            {
                get
                {
                    // initial value
                    bool hasLines = (this.Lines != null);
                    
                    // return value
                    return hasLines;
                }
            }
            #endregion
            
            #region Lines
            /// <summary>
            /// This property gets or sets the value for 'Lines'.
            /// </summary>
            public List<XmlLine> Lines
            {
                get { return lines; }
                set { lines = value; }
            }
            #endregion
            
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
            
            #region XmlString
            /// <summary>
            /// This property gets or sets the value for 'XmlString'.
            /// </summary>
            public MarkupString XmlString
            {
                get { return xmlString; }
                set { xmlString = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
