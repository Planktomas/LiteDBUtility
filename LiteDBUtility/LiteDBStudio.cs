using System.Diagnostics;
using System.IO.Compression;

namespace LiteDBUtility
{
    public static class LiteDBStudio
    {
        const string k_LiteDBStudioResourcePath = "LiteDBStudio.zip";
        const string k_LiteDBStudioFolder = "LiteDBStudio";
        static readonly string k_LiteDBStudioPath = Path.Combine(k_LiteDBStudioFolder, "LiteDBStudio.exe");

        public static void Start()
        {
            ExtractZipResource(k_LiteDBStudioResourcePath);
            OpenFileWithDefaultApplication(k_LiteDBStudioPath);
        }

        static void ExtractZipResource(string zipFileName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic);

            Parallel.ForEach(assemblies, (assembly, state) =>
            {
                string? resourcePath = null;
                foreach (string resourceName in assembly.GetManifestResourceNames())
                {
                    if (resourceName.EndsWith(zipFileName))
                    {
                        resourcePath = resourceName;
                        break;
                    }
                }

                if (resourcePath == null)
                    return;

                Directory.CreateDirectory(k_LiteDBStudioFolder);

                using var resourceStream = assembly.GetManifestResourceStream(resourcePath);
                using var archive = new ZipArchive(resourceStream);
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    try
                    {
                        string entryOutputPath = Path.Combine(Directory.GetCurrentDirectory(), k_LiteDBStudioFolder, entry.FullName);
                        entry.ExtractToFile(entryOutputPath, false);
                    }
                    catch (IOException) { }
                }

                state.Stop();
            });
        }

        static void OpenFileWithDefaultApplication(string? filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist.");
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
    }
}