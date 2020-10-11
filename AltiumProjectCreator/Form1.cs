using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq.Expressions;

namespace AltiumProjectCreator
{
    public partial class Main : Form
    {
        string newFileStartPath = "";
        string sourcePath = "";
        string targetPath = "";
        string designFilesPath = "";

        public Main()
        {
            InitializeComponent();
            textBox_templateProjectFilePath.Text = @"";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            button_createProject.Font = new Font(button_createProject.Font, FontStyle.Bold);
        }

        private void button_searchTemplateFilePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog_searchTemplate.SelectedPath = "";
            DialogResult result = folderBrowserDialog_searchTemplate.ShowDialog();
            if (result == DialogResult.OK)
            {
                string templateFilePath = folderBrowserDialog_searchTemplate.SelectedPath;
                textBox_templateProjectFilePath.Text = templateFilePath;
            }
            else
            {
                // do nothing
            }
        }

        private void button_searchNewProjectFilePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog_searchNewProject.SelectedPath = @newFileStartPath;
            DialogResult result = folderBrowserDialog_searchNewProject.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newProjectFilePath = folderBrowserDialog_searchNewProject.SelectedPath;
                textBox_newProjectTargetFilePath.Text = newProjectFilePath;
            }
            else
            {
                // do nothing
            }
        }

        private void button_createProject_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            string readMeFileName = "README.md";

            sourcePath = @textBox_templateProjectFilePath.Text;
            targetPath = @textBox_newProjectTargetFilePath.Text;

            if (!string.IsNullOrWhiteSpace(this.textBox_newProjectTargetFilePath.Text) && !string.IsNullOrWhiteSpace(this.textBox_templateProjectFilePath.Text) && !string.IsNullOrWhiteSpace(this.textBox_newProjectName.Text))
            {
                if (System.IO.Directory.Exists(sourcePath))
                {
                    string[] sourceFiles = System.IO.Directory.GetFiles(sourcePath);
                    string oldFileName = "";
                    string newFileName = "";
                    int delay = 100;

                    // count number of lines in PrjPCB file for progress bar increment calculation
                    string[] PrjPCB_file_lineCount = File.ReadAllLines(Path.Combine(sourcePath, "PROJECT_NAME_V01R01.PrjPCB"));
                    int PrjPCB_numLines = PrjPCB_file_lineCount.Length;
                    textBox_debug.AppendText("There are " + PrjPCB_numLines.ToString() + " lines in the PrjPCB file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // count number of lines in DsnWrk file for progress bar increment calculation
                    string[] DsnWrk_file_lineCount = File.ReadAllLines(Path.Combine(sourcePath, "PROJECT_NAME_V01R01.DsnWrk"));
                    int DsnWrk_numLines = DsnWrk_file_lineCount.Length;
                    textBox_debug.AppendText("There are " + DsnWrk_numLines.ToString() + " lines in the DsnWrk file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // count number of lines in OutJob file for progress bar increment calculation
                    string outJobFileName = "";
                    foreach (string file in sourceFiles) // dyanmically find OutJob file name
                    {
                        if (file.Contains(".OutJob"))
                        {
                            //store file name to string
                            outJobFileName = Path.GetFileName(file);
                            textBox_debug.AppendText("Found OutJob file: " + Path.GetFileName(outJobFileName)); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                    }

                    string[] OutJob_file_lineCount = File.ReadAllLines(Path.Combine(sourcePath, outJobFileName));
                    int OutJob_numLines = OutJob_file_lineCount.Length;
                    textBox_debug.AppendText("There are " + OutJob_numLines.ToString() + " lines in the OutJob file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    double numTicks = 8 + 2 * (sourceFiles.Length) - 1 + OutJob_numLines + DsnWrk_numLines + PrjPCB_numLines;
                    progressBar_fileCopy.Maximum = Convert.ToInt32(numTicks);
                    int incrementValue = (int)Math.Ceiling(1 / numTicks);

                    // ------------- CREATE FOLDERS IN NEW PROJECT DIRECTORY ------------- //
                    System.IO.Directory.CreateDirectory(targetPath + @"/ASSETS"); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating ASSETS folder......"; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating ASSETS folder..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + @"/ASSETS/" + readMeFileName))
                        {
                            // Add some text to file    
                            byte[] heading = new UTF8Encoding(true).GetBytes("# " + textBox_newProjectName.Text + "\r\n\r\n");
                            fs.Write(heading, 0, heading.Length);
                            byte[] description = new UTF8Encoding(true).GetBytes("Assets folder containing supporting files and documents.");
                            fs.Write(description, 0, description.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    System.IO.Directory.CreateDirectory(targetPath + @"/ASSETS" + @"/3D"); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating 3D sub folder..."; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating 3D sub folder..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + @"/ASSETS" + @"/3D/" + readMeFileName))
                        {
                            // Add some text to file    
                            byte[] heading = new UTF8Encoding(true).GetBytes("# " + textBox_newProjectName.Text + "\r\n\r\n");
                            fs.Write(heading, 0, heading.Length);
                            byte[] description = new UTF8Encoding(true).GetBytes("3D files.");
                            fs.Write(description, 0, description.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    System.IO.Directory.CreateDirectory(targetPath + @"/ASSETS" + @"/DATASHEETS"); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating DATASHEETS sub folder..."; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating DATASHEETS sub folder..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + @"/ASSETS" + @"/DATASHEETS/" + readMeFileName))
                        {
                            // Add some text to file    
                            byte[] heading = new UTF8Encoding(true).GetBytes("# " + textBox_newProjectName.Text + "\r\n\r\n");
                            fs.Write(heading, 0, heading.Length);
                            byte[] description = new UTF8Encoding(true).GetBytes("Datasheets.");
                            fs.Write(description, 0, description.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    System.IO.Directory.CreateDirectory(targetPath + @"/ASSETS" + @"/INTERNAL"); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating INTERNAL DOCS sub folder..."; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating INTERNAL DOCS sub folder..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + @"/ASSETS" + @"/INTERNAL/" + readMeFileName))
                        {
                            // Add some text to file    
                            byte[] heading = new UTF8Encoding(true).GetBytes("# " + textBox_newProjectName.Text + "\r\n\r\n");
                            fs.Write(heading, 0, heading.Length);
                            byte[] description = new UTF8Encoding(true).GetBytes("Tesla internal documents.");
                            fs.Write(description, 0, description.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    System.IO.Directory.CreateDirectory(targetPath + @"/ASSETS" + @"/SIMULATIONS"); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating SIMULATIONS sub folder..."; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating SIMULATIONS sub folder..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + @"/ASSETS" + @"/SIMULATIONS/" + readMeFileName))
                        {
                            // Add some text to file    
                            byte[] heading = new UTF8Encoding(true).GetBytes("# " + textBox_newProjectName.Text + "\r\n\r\n");
                            fs.Write(heading, 0, heading.Length);
                            byte[] description = new UTF8Encoding(true).GetBytes("SPICE simulations.");
                            fs.Write(description, 0, description.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    System.IO.Directory.CreateDirectory(targetPath + @"/DESIGN_FILES"); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating DESIGN FILES folder..."; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating DESIGN FILES folder..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + @"/DESIGN_FILES/" + readMeFileName))
                        {
                            // Add some text to file    
                            byte[] heading = new UTF8Encoding(true).GetBytes("# " + textBox_newProjectName.Text + "\r\n\r\n");
                            fs.Write(heading, 0, heading.Length);
                            byte[] description = new UTF8Encoding(true).GetBytes("Design files.");
                            fs.Write(description, 0, description.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    System.IO.Directory.CreateDirectory(targetPath + @"/FIRMWARE"); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating FIRMWARE folder..."; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating FIRMWARE folder..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + @"/FIRMWARE/" + readMeFileName))
                        {
                            // Add some text to file    
                            byte[] heading = new UTF8Encoding(true).GetBytes("# " + textBox_newProjectName.Text + "\r\n\r\n");
                            fs.Write(heading, 0, heading.Length);
                            byte[] description = new UTF8Encoding(true).GetBytes("Firmware.");
                            fs.Write(description, 0, description.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    System.IO.Directory.CreateDirectory(targetPath + @"/RELEASED_FILES_RFQs"); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating RELEASED FILES AND RFQs folder..."; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating RELEASED FILES AND RFQs folder..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + @"/RELEASED_FILES_RFQs/" + readMeFileName))
                        {
                            // Add some text to file    
                            byte[] heading = new UTF8Encoding(true).GetBytes("# " + textBox_newProjectName.Text + "\r\n\r\n");
                            fs.Write(heading, 0, heading.Length);
                            byte[] description = new UTF8Encoding(true).GetBytes("Quotes and pack-n-go file packages that have been shared with CMs.");
                            fs.Write(description, 0, description.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    // ------------- COPY CONTENTS FROM TEMPLATE INTO DESIGN_FILES\V01R01 ------------- //
                    foreach (string file in sourceFiles)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        fileName = System.IO.Path.GetFileName(file);
                        if (fileName != "debug.log")
                        {
                            destFile = System.IO.Path.Combine(targetPath + @"\DESIGN_FILES", fileName);

                            label_debugMessages.Text = "Copying " + fileName + "..."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Copying " + fileName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                            System.IO.File.Copy(file, destFile, true);
                            System.Threading.Thread.Sleep(delay);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }

                    // ------------- CREATE GITIGNORE IN ROOT DIRECTORY ------------- //
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + "/.gitignore"))
                        {
                            // Add some text to file    
                            byte[] gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.PrjPCBStructure" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.SchDocPreview" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.PcbDocPreview" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("__Previews" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.PrjPcbStructure" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.Dat" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.REP" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.TLT" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.LOG" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.log" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.htm" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("*.$$$" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("Project\\ Logs" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("Project Outputs for*" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("History/*" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("exports/*" + "\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    // ------------- CREATE README IN ROOT DIRECTORY ------------- //
                    try
                    {
                        using (FileStream fs = File.Create(targetPath + "/README.md"))
                        {
                            // Add some text to file    
                            byte[] gitIgnoreLine = new UTF8Encoding(true).GetBytes("# " + textBox_newProjectName.Text + "\r\n\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes(textBox_projectDescription.Text + "\r\n\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("# " + "Designer" + "\r\n\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes(textBox_projectDesigner.Text + "\r\n\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);

                            gitIgnoreLine = new UTF8Encoding(true).GetBytes("# " + "Links" + "\r\n\r\n");
                            fs.Write(gitIgnoreLine, 0, gitIgnoreLine.Length);
                        }
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }

                    // ------------- RENAME RELEVANT FILES TO REFLECT NEW PROJECT NAME ------------- //
                    string designFilesPath = targetPath + @"\DESIGN_FILES";
                    string[] destinationFiles = System.IO.Directory.GetFiles(designFilesPath);

                    foreach (string file in destinationFiles)
                    {
                        fileName = System.IO.Path.GetFileName(file);
                        if (fileName.Contains("PROJECT_NAME_V01R01"))
                        {
                            oldFileName = fileName;
                            newFileName = textBox_newProjectName.Text;

                            label_debugMessages.Text = "Renaming " + oldFileName + " to " + newFileName; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Renaming " + oldFileName + " to " + newFileName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                            System.IO.File.Move(Path.Combine(designFilesPath, oldFileName), Path.Combine(designFilesPath, newFileName) + Path.GetExtension(Path.Combine(designFilesPath, oldFileName)));
                            System.Threading.Thread.Sleep(delay);
                        }
                        else
                        {
                            label_debugMessages.Text = "Skipping " + fileName + "..."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Skipping " + fileName + "..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                            System.Threading.Thread.Sleep(delay);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }

                    // ------------- EDIT ALTIUM TEXT FILES (.PrjPCB) ------------- //
                    string PrjPCBFilePathAndName = Path.Combine(designFilesPath, newFileName) + ".PrjPCB";
                    textBox_debug.AppendText("File path: " + PrjPCBFilePathAndName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    string replacementLine = "";
                    string[] PrjPCB_arr = File.ReadAllLines(PrjPCBFilePathAndName);
                    var PrjPCB_writer = new StreamWriter(PrjPCBFilePathAndName);
                    textBox_debug.AppendText("PrjPCB file opened for writing..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    for (int i = 0; i < PrjPCB_arr.Length; i++)
                    {
                        string PrjPCB_line = PrjPCB_arr[i];
                        if (PrjPCB_line.Contains("PROJECT_NAME_V01R01"))
                        {
                            textBox_debug.AppendText("Changing line " + (i + 1).ToString() + " in PrjPCB file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in PrjPCB file."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Old Line: " + PrjPCB_line); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            replacementLine = PrjPCB_line.Replace("PROJECT_NAME_V01R01", newFileName);
                            PrjPCB_writer.WriteLine(replacementLine);
                            textBox_debug.AppendText("New Line: " + replacementLine); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                        else
                        {
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in PrjPCB file."; label_debugMessages.Refresh();
                            PrjPCB_writer.WriteLine(PrjPCB_line);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }
                    PrjPCB_writer.Close();
                    textBox_debug.AppendText("PrjPCB file closed."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // ------------- EDIT ALTIUM TEXT FILES (.DsnWrk) ------------- //
                    string DsnWrkFilePathAndName = Path.Combine(designFilesPath, newFileName) + ".DsnWrk";
                    textBox_debug.AppendText("File path: " + DsnWrkFilePathAndName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    string[] DsnWrk_arr = File.ReadAllLines(DsnWrkFilePathAndName);
                    var DsnWrk_writer = new StreamWriter(DsnWrkFilePathAndName);
                    textBox_debug.AppendText("DsnWrk file opened for writing..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    for (int i = 0; i < DsnWrk_arr.Length; i++)
                    {
                        string DsnWrk_line = DsnWrk_arr[i]; 
                        if (DsnWrk_line.Contains("PROJECT_NAME_V01R01"))
                        {
                            textBox_debug.AppendText("Changing line " + (i + 1).ToString() + " in DsnWrk file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in DsnWrk file."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Old Line: " + DsnWrk_line); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            replacementLine = DsnWrk_line.Replace("PROJECT_NAME_V01R01", newFileName);
                            DsnWrk_writer.WriteLine(replacementLine);
                            textBox_debug.AppendText("New Line: " + replacementLine); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                        else
                        {
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in DsnWrk file."; label_debugMessages.Refresh();
                            DsnWrk_writer.WriteLine(DsnWrk_line);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }
                    DsnWrk_writer.Close();
                    textBox_debug.AppendText("DsnWrk file closed."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // ------------- EDIT ALTIUM TEXT FILES (.OutJob) ------------- //
                    string OutJobFilePathAndName = Path.Combine(designFilesPath, outJobFileName);
                    textBox_debug.AppendText("File path: " + OutJobFilePathAndName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    string[] OutJob_arr = File.ReadAllLines(OutJobFilePathAndName);
                    var OutJob_writer = new StreamWriter(OutJobFilePathAndName);
                    textBox_debug.AppendText("OutJob file opened for writing..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    for (int i = 0; i < OutJob_arr.Length; i++)
                    {
                        string OutJob_line = OutJob_arr[i];
                        if (OutJob_line.Contains("PROJECT_NAME_V01R01"))
                        {
                            textBox_debug.AppendText("Changing line " + (i + 1).ToString() + " in OutJob file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in OutJob file."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Old Line: " + OutJob_line); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            replacementLine = OutJob_line.Replace("PROJECT_NAME_V01R01", newFileName);
                            OutJob_writer.WriteLine(replacementLine);
                            textBox_debug.AppendText("New Line: " + replacementLine); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                        else
                        {
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in OutJob file."; label_debugMessages.Refresh();
                            OutJob_writer.WriteLine(OutJob_line);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }
                    OutJob_writer.Close();
                    textBox_debug.AppendText("OutJob file closed."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                }
                label_debugMessages.Text = "NEW PROJECT CREATION IS COMPLETE!"; label_debugMessages.Refresh();
                textBox_debug.AppendText("NEW PROJECT CREATION IS COMPLETE!"); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
            }
            else
            {
                MessageBox.Show("You must choose a project name, source file path, and target file path.", "ERROR");
            }
        }

        private void button_upVerProject_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox_templateProjectFilePath.Text))
            {
                string fileExt = ""; // string for extracting file extension for determining whether or not the file should be copied
                string fileName = ""; // string for extracting file name for determining name of file to be copied
                int delay = 100;

                sourcePath = @textBox_templateProjectFilePath.Text;
                
                string[] sourceFiles = System.IO.Directory.GetFiles(sourcePath);

                string outJobFileName = "";
                string prjPCBFileName = "";
                string dsnWrkFileName = "";
                string prjPCBStructureFileName = "";
                string prjPCBVariantsFileName = "";

                foreach (string file in sourceFiles) // dyanmically find OutJob file name
                {
                    if (file.Contains(".OutJob"))
                    {
                        outJobFileName = Path.GetFileName(file);
                        textBox_debug.AppendText("Found OutJob file: " + Path.GetFileName(outJobFileName)); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    }
                    else if (file.Contains(".PrjPCBStructure"))
                    {
                        prjPCBStructureFileName = Path.GetFileName(file);
                        textBox_debug.AppendText("Found PrjPCBStructure file: " + Path.GetFileName(prjPCBStructureFileName)); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    }
                    else if (file.Contains(".PrjPCBVariants"))
                    {
                        prjPCBVariantsFileName = Path.GetFileName(file);
                        textBox_debug.AppendText("Found PrjPCBVariants file: " + Path.GetFileName(prjPCBVariantsFileName)); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    }
                    else if (file.Contains(".PrjPCB"))
                    {
                        prjPCBFileName = Path.GetFileName(file);
                        textBox_debug.AppendText("Found PrjPCB file: " + Path.GetFileName(prjPCBFileName)); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    }
                    else if (file.Contains(".DsnWrk"))
                    {
                        dsnWrkFileName = Path.GetFileName(file);
                        textBox_debug.AppendText("Found DsnWrk file: " + Path.GetFileName(dsnWrkFileName)); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    }
                    else { }
                }

                // count number of lines in PrjPCB file for progress bar increment calculation
                string[] PrjPCB_file_lineCount = File.ReadAllLines(Path.Combine(sourcePath, prjPCBFileName));
                int PrjPCB_numLines = PrjPCB_file_lineCount.Length;
                textBox_debug.AppendText("There are " + PrjPCB_numLines.ToString() + " lines in the PrjPCB file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                // count number of lines in DsnWrk file for progress bar increment calculation
                string[] DsnWrk_file_lineCount = File.ReadAllLines(Path.Combine(sourcePath, dsnWrkFileName));
                int DsnWrk_numLines = DsnWrk_file_lineCount.Length;
                textBox_debug.AppendText("There are " + DsnWrk_numLines.ToString() + " lines in the DsnWrk file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                // count number of lines in OutJob file for progress bar increment calculation
                string[] OutJob_file_lineCount = File.ReadAllLines(Path.Combine(sourcePath, outJobFileName));
                int OutJob_numLines = OutJob_file_lineCount.Length;
                textBox_debug.AppendText("There are " + OutJob_numLines.ToString() + " lines in the OutJob file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                // calculate number of ticks and increment value for progress bar
                double numTicks = 8 + 2 * (sourceFiles.Length) - 1 + OutJob_numLines + DsnWrk_numLines + PrjPCB_numLines;
                progressBar_fileCopy.Maximum = Convert.ToInt32(numTicks);
                int incrementValue = (int)Math.Ceiling(1 / numTicks);

                if (!string.IsNullOrWhiteSpace(this.textBox_templateProjectFilePath.Text))
                {
                    // TODO: figure out current version, increment by 1, built up file name append with rev01
                    // parse out version and revision from filename
                    string[] splitPreviousFileNameAndVerRev = prjPCBFileName.Split('_');
                    string previousFileName_verAndRev = prjPCBFileName.Split('.')[0];
                    string previousFileName = splitPreviousFileNameAndVerRev[0];
                    string[] verRev_and_ext = splitPreviousFileNameAndVerRev[1].Split('.');
                    string version_and_rev = verRev_and_ext[0];

                    // parse out just version
                    string[] version = version_and_rev.Split('V');
                    string[] version2 = version[1].Split('R');
                    string version_parsed = version2[0];

                    // convert version to integer
                    int version_as_int = Convert.ToInt32(version_parsed);

                    // increment version by 1
                    version_as_int++;

                    // convert incremented version and revision back to string
                    string inc_ver_asString = version_as_int.ToString();
                    if (version_as_int < 10)
                    {
                        inc_ver_asString = "0" + inc_ver_asString;
                    }
                    else { }

                    string upVerString = "V" + inc_ver_asString + "R01";

                    string newFileName_VerRev = previousFileName + "_" + upVerString;
                    textBox_debug.AppendText("NEW FILE NAME: " + newFileName_VerRev); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    designFilesPath = sourcePath.Substring(0, sourcePath.Length - 7);
                    textBox_debug.AppendText("DESIGN FILES PATH : " + designFilesPath); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    targetPath = Path.Combine(designFilesPath, upVerString);
                    textBox_debug.AppendText("TARGET PATH : " + targetPath); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // create new version folder in design files directory
                    System.IO.Directory.CreateDirectory(targetPath); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating project folder for " + upVerString + "..."; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating project folder for " + upVerString + "..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    string destFile = "";
                    // TODO: figure out all files that should be copied over (.SchDoc, .PcbDoc, .PCBDwf, .PrjPCB, .PrjPCBStructure, .PrjPCBVariants, .OutJob)
                    foreach (string file in sourceFiles)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        fileName = System.IO.Path.GetFileName(file);
                        string fileName_noExt = fileName.Split('.')[0];
                        fileExt = System.IO.Path.GetExtension(file);
                        if (fileExt == ".SchDoc" || fileExt == ".PcbDoc" || fileExt == ".PCBDwf" || fileExt == ".PrjPCB" || fileExt == ".PrjPCBStructure" || fileExt == ".PrjPCBVariants" || fileExt == ".OutJob" || fileExt == ".DsnWrk")
                        {
                            destFile = System.IO.Path.Combine(targetPath, fileName);

                            label_debugMessages.Text = "Copying " + fileName + "..."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Copying " + fileName + "..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                            System.IO.File.Copy(file, destFile, true);
                            System.Threading.Thread.Sleep(delay);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }

                    // TODO: rename files to reflect up version
                    string[] destinationFiles = System.IO.Directory.GetFiles(targetPath);

                    foreach (string file in destinationFiles)
                    {
                        fileName = System.IO.Path.GetFileName(file);
                        if (fileName.Contains(previousFileName))
                        {

                            label_debugMessages.Text = "Renaming " + previousFileName + " to " + newFileName_VerRev; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Renaming " + previousFileName + " to " + newFileName_VerRev); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                            System.IO.File.Move(Path.Combine(targetPath, fileName), Path.Combine(targetPath, newFileName_VerRev) + Path.GetExtension(Path.Combine(targetPath, fileName)));
                            System.Threading.Thread.Sleep(delay);
                        }
                        else
                        {
                            label_debugMessages.Text = "Skipping rename for " + fileName + "..."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Skipping rename for " + fileName + "..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                            System.Threading.Thread.Sleep(delay);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }

                    // TODO: edit files to reflect new name
                    // ------------- EDIT ALTIUM TEXT FILES (.PrjPCB) ------------- //
                    string PrjPCBFilePathAndName = Path.Combine(targetPath, newFileName_VerRev) + ".PrjPCB";
                    textBox_debug.AppendText("File path: " + PrjPCBFilePathAndName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    string replacementLine = "";
                    string[] PrjPCB_arr = File.ReadAllLines(PrjPCBFilePathAndName);
                    var PrjPCB_writer = new StreamWriter(PrjPCBFilePathAndName);
                    textBox_debug.AppendText("PrjPCB file opened for writing..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    for (int i = 0; i < PrjPCB_arr.Length; i++)
                    {
                        string PrjPCB_line = PrjPCB_arr[i];
                        if (PrjPCB_line.Contains(previousFileName_verAndRev))
                        {
                            textBox_debug.AppendText("Changing line " + (i + 1).ToString() + " in PrjPCB file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in PrjPCB file."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Old Line: " + PrjPCB_line); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            replacementLine = PrjPCB_line.Replace(previousFileName_verAndRev, newFileName_VerRev);
                            PrjPCB_writer.WriteLine(replacementLine);
                            textBox_debug.AppendText("New Line: " + replacementLine); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                        else
                        {
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in PrjPCB file."; label_debugMessages.Refresh();
                            PrjPCB_writer.WriteLine(PrjPCB_line);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }
                    PrjPCB_writer.Close();
                    textBox_debug.AppendText("PrjPCB file closed."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // ------------- EDIT ALTIUM TEXT FILES (.DsnWrk) ------------- //
                    string DsnWrkFilePathAndName = Path.Combine(targetPath, newFileName_VerRev) + ".DsnWrk";
                    textBox_debug.AppendText("File path: " + DsnWrkFilePathAndName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    string[] DsnWrk_arr = File.ReadAllLines(DsnWrkFilePathAndName);
                    var DsnWrk_writer = new StreamWriter(DsnWrkFilePathAndName);
                    textBox_debug.AppendText("DsnWrk file opened for writing..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    for (int i = 0; i < DsnWrk_arr.Length; i++)
                    {
                        string DsnWrk_line = DsnWrk_arr[i];
                        if (DsnWrk_line.Contains(previousFileName_verAndRev))
                        {
                            textBox_debug.AppendText("Changing line " + (i + 1).ToString() + " in DsnWrk file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in DsnWrk file."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Old Line: " + DsnWrk_line); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            replacementLine = DsnWrk_line.Replace(previousFileName_verAndRev, newFileName_VerRev);
                            DsnWrk_writer.WriteLine(replacementLine);
                            textBox_debug.AppendText("New Line: " + replacementLine); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                        else
                        {
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in DsnWrk file."; label_debugMessages.Refresh();
                            DsnWrk_writer.WriteLine(DsnWrk_line);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }
                    DsnWrk_writer.Close();
                    textBox_debug.AppendText("DsnWrk file closed."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // ------------- EDIT ALTIUM TEXT FILES (.OutJob) ------------- //

                    string OutJobFilePathAndName = Path.Combine(targetPath, outJobFileName);
                    textBox_debug.AppendText("File path: " + OutJobFilePathAndName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    string[] OutJob_arr = File.ReadAllLines(OutJobFilePathAndName);
                    var OutJob_writer = new StreamWriter(OutJobFilePathAndName);
                    textBox_debug.AppendText("OutJob file opened for writing..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    for (int i = 0; i < OutJob_arr.Length; i++)
                    {
                        string OutJob_line = OutJob_arr[i];
                        if (OutJob_line.Contains(previousFileName_verAndRev))
                        {
                            textBox_debug.AppendText("Changing line " + (i + 1).ToString() + " in OutJob file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in OutJob file."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Old Line: " + OutJob_line); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            replacementLine = OutJob_line.Replace(previousFileName_verAndRev, newFileName_VerRev);
                            OutJob_writer.WriteLine(replacementLine);
                            textBox_debug.AppendText("New Line: " + replacementLine); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                        else
                        {
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in OutJob file."; label_debugMessages.Refresh();
                            OutJob_writer.WriteLine(OutJob_line);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }
                    OutJob_writer.Close();
                    textBox_debug.AppendText("OutJob file closed."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                }
                else
                {
                    MessageBox.Show("You must choose a project to up-version.", "ERROR");
                }
            }
        }

        private void button_upRevProject_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox_templateProjectFilePath.Text))
            {
                string fileExt = ""; // string for extracting file extension for determining whether or not the file should be copied
                string fileName = ""; // string for extracting file name for determining name of file to be copied
                int delay = 100;

                sourcePath = @textBox_templateProjectFilePath.Text;
                targetPath = @textBox_newProjectTargetFilePath.Text;
                string[] sourceFiles = System.IO.Directory.GetFiles(sourcePath);

                string outJobFileName = "";
                string prjPCBFileName = "";
                string dsnWrkFileName = "";
                string prjPCBStructureFileName = "";
                string prjPCBVariantsFileName = "";

                foreach (string file in sourceFiles) // dyanmically find OutJob file name
                {
                    if (file.Contains(".OutJob"))
                    {
                        //store file name to string
                        outJobFileName = Path.GetFileName(file);
                        textBox_debug.AppendText("Found OutJob file: " + Path.GetFileName(outJobFileName)); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    }
                    else if (file.Contains(".PrjPCBStructure"))
                    {
                        prjPCBStructureFileName = Path.GetFileName(file);
                        textBox_debug.AppendText("Found PrjPCBStructure file: " + Path.GetFileName(prjPCBStructureFileName)); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    }
                    else if (file.Contains(".PrjPCBVariants"))
                    {
                        prjPCBVariantsFileName = Path.GetFileName(file);
                        textBox_debug.AppendText("Found PrjPCBVariants file: " + Path.GetFileName(prjPCBVariantsFileName)); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    }
                    else if (file.Contains(".PrjPCB"))
                    {
                        prjPCBFileName = Path.GetFileName(file);
                    }
                    else if (file.Contains(".DsnWrk"))
                    {
                        dsnWrkFileName = Path.GetFileName(file);
                    }
                    else { }
                }

                // count number of lines in PrjPCB file for progress bar increment calculation
                string[] PrjPCB_file_lineCount = File.ReadAllLines(Path.Combine(sourcePath, prjPCBFileName));
                int PrjPCB_numLines = PrjPCB_file_lineCount.Length;
                textBox_debug.AppendText("There are " + PrjPCB_numLines.ToString() + " lines in the PrjPCB file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                // count number of lines in DsnWrk file for progress bar increment calculation
                string[] DsnWrk_file_lineCount = File.ReadAllLines(Path.Combine(sourcePath, dsnWrkFileName));
                int DsnWrk_numLines = DsnWrk_file_lineCount.Length;
                textBox_debug.AppendText("There are " + DsnWrk_numLines.ToString() + " lines in the DsnWrk file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                // count number of lines in OutJob file for progress bar increment calculation
                string[] OutJob_file_lineCount = File.ReadAllLines(Path.Combine(sourcePath, outJobFileName));
                int OutJob_numLines = OutJob_file_lineCount.Length;
                textBox_debug.AppendText("There are " + OutJob_numLines.ToString() + " lines in the OutJob file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                // calculate number of ticks and increment value for progress bar
                double numTicks = 8 + 2 * (sourceFiles.Length) - 1 + OutJob_numLines + DsnWrk_numLines + PrjPCB_numLines;
                progressBar_fileCopy.Maximum = Convert.ToInt32(numTicks);
                int incrementValue = (int)Math.Ceiling(1 / numTicks);

                if (!string.IsNullOrWhiteSpace(this.textBox_templateProjectFilePath.Text))
                {
                    // TODO: figure out current version, increment by 1, built up file name append with rev01
                    // parse out version and revision from filename
                    string[] splitPreviousFileNameAndVerRev = prjPCBFileName.Split('_');
                    string previousFileName_verAndRev = prjPCBFileName.Split('.')[0];
                    string previousFileName = splitPreviousFileNameAndVerRev[0];
                    string[] verRev_and_ext = splitPreviousFileNameAndVerRev[1].Split('.');
                    string version_and_rev = verRev_and_ext[0];

                    // parse out just version
                    string[] version = version_and_rev.Split('V');
                    string[] version2 = version[1].Split('R');
                    string version_parsed = version2[0];

                    // parse out just revision
                    string[] revision = version_and_rev.Split('R');
                    string revision_parsed = revision[1];

                    // convert revision to integer
                    int revision_as_int = Convert.ToInt32(revision_parsed);

                    // increment revision by 1
                    revision_as_int++;

                    // convert incremented revision back to string
                    string inc_rev_asString = revision_as_int.ToString();

                    if (revision_as_int < 10)
                    {
                        inc_rev_asString = "0" + inc_rev_asString;
                    }
                    else { }

                    string upVerAndRevString = "V" + version_parsed + "R" + inc_rev_asString;
                    string newFileName_VerRev = previousFileName + "_" + upVerAndRevString;
                    textBox_debug.AppendText("NEW FILE NAME: " + newFileName_VerRev); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    designFilesPath = sourcePath.Substring(0, sourcePath.Length - 7);
                    textBox_debug.AppendText("DESIGN FILES PATH : " + designFilesPath); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    targetPath = Path.Combine(designFilesPath, upVerAndRevString);
                    textBox_debug.AppendText("TARGET PATH : " + targetPath); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // create new version folder in design files directory
                    System.IO.Directory.CreateDirectory(targetPath); progressBar_fileCopy.Increment(incrementValue); label_debugMessages.Text = "Creating project folder for " + upVerAndRevString + "..."; label_debugMessages.Refresh(); System.Threading.Thread.Sleep(delay);
                    textBox_debug.AppendText("Creating project folder for " + upVerAndRevString + "..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    string destFile = "";
                    // TODO: figure out all files that should be copied over (.SchDoc, .PcbDoc, .PCBDwf, .PrjPCB, .PrjPCBStructure, .PrjPCBVariants, .OutJob)
                    foreach (string file in sourceFiles)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        fileName = System.IO.Path.GetFileName(file);
                        string fileName_noExt = fileName.Split('.')[0];
                        fileExt = System.IO.Path.GetExtension(file);
                        if (fileExt == ".SchDoc" || fileExt == ".PcbDoc" || fileExt == ".PCBDwf" || fileExt == ".PrjPCB" || fileExt == ".PrjPCBStructure" || fileExt == ".PrjPCBVariants" || fileExt == ".OutJob" || fileExt == ".DsnWrk")
                        {
                            destFile = System.IO.Path.Combine(targetPath, fileName);

                            label_debugMessages.Text = "Copying " + fileName + "..."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Copying " + fileName + "..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                            System.IO.File.Copy(file, destFile, true);
                            System.Threading.Thread.Sleep(delay);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }

                    // TODO: rename files to reflect up version
                    string[] destinationFiles = System.IO.Directory.GetFiles(targetPath);

                    foreach (string file in destinationFiles)
                    {
                        fileName = System.IO.Path.GetFileName(file);
                        if (fileName.Contains(previousFileName))
                        {

                            label_debugMessages.Text = "Renaming " + previousFileName + " to " + newFileName_VerRev; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Renaming " + previousFileName + " to " + newFileName_VerRev); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                            System.IO.File.Move(Path.Combine(targetPath, fileName), Path.Combine(targetPath, newFileName_VerRev) + Path.GetExtension(Path.Combine(targetPath, fileName)));
                            System.Threading.Thread.Sleep(delay);
                        }
                        else
                        {
                            label_debugMessages.Text = "Skipping rename for " + fileName + "..."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Skipping rename for " + fileName + "..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                            System.Threading.Thread.Sleep(delay);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }

                    // TODO: edit files to reflect new name
                    // ------------- EDIT ALTIUM TEXT FILES (.PrjPCB) ------------- //
                    string PrjPCBFilePathAndName = Path.Combine(targetPath, newFileName_VerRev) + ".PrjPCB";
                    textBox_debug.AppendText("File path: " + PrjPCBFilePathAndName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    string replacementLine = "";
                    string[] PrjPCB_arr = File.ReadAllLines(PrjPCBFilePathAndName);
                    var PrjPCB_writer = new StreamWriter(PrjPCBFilePathAndName);
                    textBox_debug.AppendText("PrjPCB file opened for writing..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    for (int i = 0; i < PrjPCB_arr.Length; i++)
                    {
                        string PrjPCB_line = PrjPCB_arr[i];
                        if (PrjPCB_line.Contains(previousFileName_verAndRev))
                        {
                            textBox_debug.AppendText("Changing line " + (i + 1).ToString() + " in PrjPCB file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in PrjPCB file."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Old Line: " + PrjPCB_line); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            replacementLine = PrjPCB_line.Replace(previousFileName_verAndRev, newFileName_VerRev);
                            PrjPCB_writer.WriteLine(replacementLine);
                            textBox_debug.AppendText("New Line: " + replacementLine); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                        else
                        {
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in PrjPCB file."; label_debugMessages.Refresh();
                            PrjPCB_writer.WriteLine(PrjPCB_line);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }
                    PrjPCB_writer.Close();
                    textBox_debug.AppendText("PrjPCB file closed."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // ------------- EDIT ALTIUM TEXT FILES (.DsnWrk) ------------- //
                    string DsnWrkFilePathAndName = Path.Combine(targetPath, newFileName_VerRev) + ".DsnWrk";
                    textBox_debug.AppendText("File path: " + DsnWrkFilePathAndName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    string[] DsnWrk_arr = File.ReadAllLines(DsnWrkFilePathAndName);
                    var DsnWrk_writer = new StreamWriter(DsnWrkFilePathAndName);
                    textBox_debug.AppendText("DsnWrk file opened for writing..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    for (int i = 0; i < DsnWrk_arr.Length; i++)
                    {
                        string DsnWrk_line = DsnWrk_arr[i];
                        if (DsnWrk_line.Contains(previousFileName_verAndRev))
                        {
                            textBox_debug.AppendText("Changing line " + (i + 1).ToString() + " in DsnWrk file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in DsnWrk file."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Old Line: " + DsnWrk_line); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            replacementLine = DsnWrk_line.Replace(previousFileName_verAndRev, newFileName_VerRev);
                            DsnWrk_writer.WriteLine(replacementLine);
                            textBox_debug.AppendText("New Line: " + replacementLine); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                        else
                        {
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in DsnWrk file."; label_debugMessages.Refresh();
                            DsnWrk_writer.WriteLine(DsnWrk_line);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }
                    DsnWrk_writer.Close();
                    textBox_debug.AppendText("DsnWrk file closed."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    // ------------- EDIT ALTIUM TEXT FILES (.OutJob) ------------- //

                    string OutJobFilePathAndName = Path.Combine(targetPath, outJobFileName);
                    textBox_debug.AppendText("File path: " + OutJobFilePathAndName); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();

                    string[] OutJob_arr = File.ReadAllLines(OutJobFilePathAndName);
                    var OutJob_writer = new StreamWriter(OutJobFilePathAndName);
                    textBox_debug.AppendText("OutJob file opened for writing..."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                    for (int i = 0; i < OutJob_arr.Length; i++)
                    {
                        string OutJob_line = OutJob_arr[i];
                        if (OutJob_line.Contains(previousFileName_verAndRev))
                        {
                            textBox_debug.AppendText("Changing line " + (i + 1).ToString() + " in OutJob file."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in OutJob file."; label_debugMessages.Refresh();
                            textBox_debug.AppendText("Old Line: " + OutJob_line); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                            replacementLine = OutJob_line.Replace(previousFileName_verAndRev, newFileName_VerRev);
                            OutJob_writer.WriteLine(replacementLine);
                            textBox_debug.AppendText("New Line: " + replacementLine); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                        }
                        else
                        {
                            label_debugMessages.Text = "Changing line " + (i + 1).ToString() + " in OutJob file."; label_debugMessages.Refresh();
                            OutJob_writer.WriteLine(OutJob_line);
                        }
                        progressBar_fileCopy.Increment(incrementValue);
                    }
                    OutJob_writer.Close();
                    textBox_debug.AppendText("OutJob file closed."); textBox_debug.AppendText(Environment.NewLine); textBox_debug.Refresh();
                }
                else
                {
                    MessageBox.Show("You must choose a project to up-revision.", "ERROR");
                }
            }
        }
    }
}
