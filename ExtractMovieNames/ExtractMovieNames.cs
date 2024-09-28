using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ExtractMovieNames
{
    public partial class ExtractMovieNames : Form
    {
        Movies moviesList = new Movies();

        public ExtractMovieNames()
        {
            InitializeComponent();
        }

        #region btnBrowse_Click
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Text Files (*.txt*)|*.TXT*|" + "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileNames.Count() > 1)
                    txtFiles.Text = "[MULTIPLE FILES]";
                else
                    txtFiles.Text = openFileDialog.FileName;
            }
        }
        #endregion

        #region btnConsolidate_Click
        private void btnConsolidate_Click(object sender, EventArgs e)
        {
            bool readingComics = false;
            List<string> comics = new List<string>();

            using (StreamWriter outFile = new StreamWriter("ConsolidatedMovies.txt", false))
            {
                List<string> movies = new List<string>();
                string currentDrive = "";
                string line;
                string spacer = " - ";

                foreach (string file in openFileDialog.FileNames)
                {
                    StreamReader inFile = new StreamReader(file);

                    while ((line = inFile.ReadLine()) != null)
                    {
                        if (line.StartsWith("---Comics---"))
                        {
                            readingComics = true;
                        }
                        else if (line.StartsWith("---End Comics---"))
                        {
                            readingComics = false;
                        }
                        else if (readingComics)
                        {
                            if (!comics.Contains(line))
                                comics.Add(line);
                        }
                        else if (file.ToLower().Contains("consolidatedmovies.txt"))
                        {
                            movies.Add(line);
                        }
                        else if (!line.StartsWith("Free space:") && !line.StartsWith("(Contains backup folder created on: "))
                        {
                            if (line.Contains(":\\"))
                                currentDrive = line;
                            else if (line != "" && line.ToLower().EndsWith("(case)") && !movies.Contains(line, StringComparer.OrdinalIgnoreCase))
                                movies.Add(line);
                            else if (line != "" && !movies.Contains(line + spacer + currentDrive, StringComparer.OrdinalIgnoreCase))
                                movies.Add(line + spacer + currentDrive);
                        }
                    }
                }
                movies.Sort();

                foreach (string mov in movies)
                    outFile.WriteLine(mov);

                outFile.Close();
            }
            MessageBox.Show($"Consolidated list written to: {Environment.NewLine} {Environment.NewLine} {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConsolidatedMovies.txt")}");

            if (!comics.Any())
                return;

            using (StreamWriter outFile = new StreamWriter("Comics.txt", false))
            {
                comics.ForEach(comic =>  outFile.WriteLine(comic));
            }
            MessageBox.Show($"Comics list written to: {Environment.NewLine} {Environment.NewLine} {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comics.txt")}");
        }
        #endregion

        #region btnStart_Click
        private void btnStart_Click(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            rtbLog.Text = "";

            if (rbSelectedFolder.Checked)
            {
                if (Directory.Exists(txtPath.Text))
                    GetMovies(null, txtPath.Text);
            }
            else
            {

                foreach (DriveInfo drive in drives)
                {
                    string path = Path.Combine(drive.Name, "Fulldisc");
                    if (Directory.Exists(path))
                        GetMovies(drive, path);
                    
                }
            }

            rtbLog.Text += "Done";
        }

        private void GetMovies(DriveInfo drive, string path)
        {
            string label = string.Empty;
            if (drive != null)
            {
                label = drive.VolumeLabel;
                if (label == string.Empty)
                    label = drive.Name.Substring(0, 1);
            }

            using (StreamWriter outFile = new StreamWriter("Movies on " + label + ".txt", false))
            {
                rtbLog.Text += "Looking for movies in " + path + Environment.NewLine;

                outFile.WriteLine(label + ": (" + path + ")" + Environment.NewLine);

                string[] movies = GetMovies(path);
                List<string> orderedMovies = new List<string>();

                foreach (string movie in movies)
                {
                    if (movie.ToLower().Contains("audiobooks") || movie.ToLower().Contains("audio books"))
                    {
                        string bookPath = movie.Substring(0, movie.LastIndexOf('_'));
                        foreach (string author in Directory.GetDirectories(bookPath))
                        {
                            string authorName = author.Substring(author.LastIndexOf('\\') + 1);
                            if (HasSubfolders(Path.Combine(bookPath, author)))
                            {
                                foreach (string book in Directory.GetDirectories(Path.Combine(bookPath, author)))
                                {
                                    string bookName = book.Substring(book.LastIndexOf('\\') + 1);
                                    orderedMovies.Add("[AUDIOBOOK]" + authorName + " - " + bookName);
                                }
                            }
                            else
                            {
                                string bookName = authorName;
                                orderedMovies.Add("[AUDIOBOOK]" + authorName);
                            }
                        }
                    }
                    else
                    {
                        orderedMovies.Add(GetMovieName(movie, label));
                    }
                }

                orderedMovies.Sort();
                foreach (string mov in orderedMovies)
                    outFile.WriteLine(mov);

                if (drive != null)
                {
                    //Write out the drive's free space 
                    double freeSpace = (((drive.AvailableFreeSpace / 1024.0) / 1024.0) / 1024.0);
                    outFile.Write(Environment.NewLine);
                    outFile.WriteLine("Free space: " + freeSpace.ToString("0.00") + " GB");

                    //Check for backup folder
                    if (Directory.Exists(Path.Combine(drive.Name, "backup")))
                    {
                        DirectoryInfo backup = new DirectoryInfo(Path.Combine(drive.Name, "backup"));
                        outFile.Write("(Contains backup folder created on: " + backup.CreationTime.ToShortDateString() + ")");
                    }

                    //Check for Comics folder
                    if (Directory.Exists(Path.Combine(drive.Name, "Comics")))
                    {
                        DirectoryInfo comics = new DirectoryInfo(Path.Combine(drive.Name, "Comics"));
                        WriteComicsFolderContents(comics, outFile);
                    }
                }
                outFile.Close();
            }

            //Create XML file containing all of the Movie objects
            XmlSerializer x = new XmlSerializer(typeof(Movies));
            using (TextWriter writer = new StreamWriter(Path.Combine(Application.StartupPath, label + ".xml")))
            {
                x.Serialize(writer, moviesList);
                writer.Close();
            }
        }

        private void WriteComicsFolderContents(DirectoryInfo comics, StreamWriter outFile)
        {
            var comicFolders = GetComicFolders(comics.FullName);
            if (!comicFolders.Any())
                return;

            outFile.WriteLine();
            outFile.WriteLine("---Comics---");
            comicFolders.ToList().ForEach(folder => outFile.WriteLine(folder));
            outFile.WriteLine("---End Comics---");
        }
        #endregion

        #region GetMovieFilenames
        private string[] GetMovieFilenames(string path)
        {
            try
            {
                List<string> files = new List<string>();
                foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly))
                {
                    FileInfo currentFile = new FileInfo(file);
                    long fileSize = currentFile.Length;
                    double size = (((fileSize / 1024.0) / 1024.0) / 1024.0);
                    string sz = size.ToString("0.00");
                    files.Add(file + "_(" + sz + " GB)");
                }
                return files.ToArray();

                //return Directory.GetFiles(path, "*.iso");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error getting ISO files from \"" + path + "\"" + Environment.NewLine + Environment.NewLine + ex.Message);

                return null;
            }
        }
        #endregion

        #region GetMovies
        private string[] GetMovies(string path)
        {
            try
            {
                string[] files = GetMovieFilenames(path);
                string[] folders = GetMovieFolders(path);
                string[] combined = new string[files.Length + folders.Length];
                Array.Copy(files, combined, files.Length);
                Array.Copy(folders, 0, combined, files.Length, folders.Length);
                Array.Sort(combined);
                return combined;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error getting folders from \"" + path + "\"" + Environment.NewLine + Environment.NewLine + ex.Message);

                return null;
            }
        }
        #endregion

        #region GetMovieFolders
        private string[] GetMovieFolders(string path)
        {
            try
            {
                List<string> folders = new List<string>();
                foreach (string folder in Directory.GetDirectories(path))
                {
                    try
                    {
                        DirectoryInfo currentFolder = new DirectoryInfo(Path.Combine(path, folder));
                        long directorySize = GetDirectorySize(folder);
                        double size = (((directorySize / 1024.0) / 1024.0) / 1024.0);
                        string sz = size.ToString("0.00");
                        folders.Add(folder + "_(" + sz + " GB)");
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Error getting folders from \"" + folder + "\"" + Environment.NewLine + Environment.NewLine + ex.Message);
                    }
                }
                return folders.ToArray();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error getting folders from \"" + path + "\"" + Environment.NewLine + Environment.NewLine + ex.Message);

                return null;
            }
        }
        #endregion

        private string[] GetComicFolders(string path)
        {
            try
            {
                List<string> folders = new List<string>();
                foreach (string folder in Directory.GetDirectories(path))
                {
                    if (Directory.GetFiles(folder).Any())
                    {
                        DirectoryInfo currentFolder = new DirectoryInfo(Path.Combine(path, folder));
                        long directorySize = GetDirectorySize(folder);
                        double size = (((directorySize / 1024.0) / 1024.0) / 1024.0);
                        string sz = size.ToString("0.00");
                        if (!folder.EndsWith(@"\Comics"))
                        {
                            string parentFolder = Path.GetDirectoryName(folder);
                            folders.Add($"{Path.GetFileName(folder)} ({parentFolder.Substring(parentFolder.IndexOf(@"Comics\") + 7)})");
                        }
                    }
                }

                Directory.GetDirectories(path).ToList().ForEach(subfolder => folders.AddRange(GetComicFolders(subfolder)));

                return folders.ToArray();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error getting folders from \"" + path + "\"" + Environment.NewLine + Environment.NewLine + ex.Message);

                return null;
            }
        }

        static long GetDirectorySize(string p)
        {
            // 1.
            // Get array of all file names.
            string[] a = Directory.GetFiles(p, "*.*", SearchOption.AllDirectories);

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                // 3.
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            // 4.
            // Return total size
            return b;
        }
        static bool HasSubfolders(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            DirectoryInfo[] subdirs = directory.GetDirectories();
            return (subdirs.Length != 0);
        }

        private bool IsRomanNumeral(string romanValue)
        {
            Regex rgx = new Regex(@"^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$");
            return  rgx.IsMatch(romanValue);
        }
        private bool IsSeasonAndDiscDesignation(string input)
        {
            Regex rgx = new Regex(@"S[0-9]+D[0-9]+");
            return rgx.IsMatch(input);
        }
        #region GetMovieName
        private string GetMovieName(string file, string driveLabel)
        {
            string movieName = "";
            string fileName;
            string size = string.Empty;
            string[] words;
            Movie movie = new Movie();

            try
            {      
                size = file.Substring(file.IndexOf("_("));
                size = size.Substring(1);
                file = file.Substring(0, file.IndexOf("_("));
                
                fileName = Path.GetFileNameWithoutExtension(file);

                if (file.Contains('[') && file.Contains(']'))
                {
                    movieName = fileName;
                }
                else
                {
                    if (file.Contains('_'))
                        words = fileName.Split('_');
                    else if (file.Contains(' '))
                        words = fileName.Split(' ');
                    else if (file.Contains('.'))
                        words = fileName.Split('.');
                    else
                        words = new string[1] { fileName };

                    foreach (string word in words)
                    {
                        if (word.Length > 1 && !IsRomanNumeral(word) && !IsSeasonAndDiscDesignation(word))
                            movieName += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                        else
                            movieName += word + " ";
                    }

                    movieName = movieName.Trim();

                    if (movieName.StartsWith("The ") && movieName.Length > 4)
                        movieName = movieName.Substring(4) + ", The";
                    else if (movieName.StartsWith("An ") && movieName.Length > 3)
                        movieName = movieName.Substring(3) + ", An";
                    else if (movieName.StartsWith("A ") && movieName.Length > 2)
                        movieName = movieName.Substring(2) + ", A";
                }

                movie.Name = movieName;
                string tempSize = size.Replace("(", "");
                tempSize = tempSize.Replace(" GB)", "");
                movie.Size = Convert.ToDouble(tempSize);
                movie.DriveLabel = driveLabel;

                moviesList.Add(movie);
                
                return movieName + " " + size;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error getting the movie name from \"" + file + "\"" + Environment.NewLine + Environment.NewLine + ex.Message);

                return null;
            }
        }
        #endregion

        private void btnFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnDupBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Text Files (*.txt*)|*.TXT*|" + "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtDupSourceFile.Text = openFileDialog.FileName;
            }
        }

        private void btnFindDuplicates_Click(object sender, EventArgs e)
        {
            string line;
            string dups = string.Empty;
            Dictionary<string, string> movieNames = new Dictionary<string, string>();
            Dictionary<string, int> dupsOnDrive = new Dictionary<string, int>();

            using (StreamReader inFile = new StreamReader(txtDupSourceFile.Text))
            {
                while ((line = inFile.ReadLine()) != null)
                    movieNames.Add(line, line.Substring(0, line.IndexOf('(')).Trim());


                var lookup = movieNames.ToLookup(x => x.Value, x => x.Key).Where(x => x.Count() > 1);//.OrderBy(f=>f.Key.Substring(f.Key.LastIndexOf('-')));

                foreach (var item in lookup)
                {
                    var keys = item.Aggregate("", (s, v) => s + System.Environment.NewLine + v);
                    dups += keys + Environment.NewLine;// "The following keys have the value " + item.Key + ":" + keys;

                    foreach (var dup in item)
                    {
                        if (dup.Contains("(Case)"))
                            continue;

                        string driveName = dup.Substring(dup.LastIndexOf('-'));
                        driveName = driveName.Substring(2, driveName.IndexOf(':') - 2);

                        if (!dupsOnDrive.ContainsKey(driveName))
                            dupsOnDrive[driveName] = 1;
                        else
                            dupsOnDrive[driveName] += 1;
                    }
                }

                dups += Environment.NewLine + Environment.NewLine + $"Drive\t\t\t\tNumber of duplicates" + 
                        Environment.NewLine + "-----------------------------------------------------" + Environment.NewLine;

                int longestDriveLength = dupsOnDrive.Keys.OrderByDescending(s => s.Length).First().Length;// dupsOnDrive.Keys.Where(k=>k.)
                foreach (KeyValuePair<string, int> kvp in dupsOnDrive.OrderByDescending(k => k.Value))
                {
                    int spacesToAdd = longestDriveLength - kvp.Key.Length;
                    dups += kvp.Key.PadRight(longestDriveLength + 10) + kvp.Value.ToString() + Environment.NewLine;
                }

                using (StreamWriter outFile = new StreamWriter("DuplicateMovies.txt", false))
                {
                    outFile.Write(dups);
                    outFile.Close();

                    MessageBox.Show("Duplicate Movies list written to " + Application.StartupPath + "\\DuplicateMovies.txt");
                }
            }
        }
        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }
    }
}
