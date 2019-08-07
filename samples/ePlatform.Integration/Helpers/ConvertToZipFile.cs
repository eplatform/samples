using System.IO;
using System.IO.Compression;

namespace ePlatform.Integration.Helpers
{
    public class ConvertToZipFile
    {
        public Stream ZipFile(Stream source, string fileName)
        {
            var zipStream = new MemoryStream();
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                var fileInArchive = archive.CreateEntry(fileName, CompressionLevel.Optimal);

                using (var entryStream = fileInArchive.Open())
                {
                    source.CopyTo(entryStream);
                }
            }
            return zipStream;
        }
        public byte[] ZipFileToByte(Stream source, string fileName)
        {
            byte[] fileBytes;
            using (var stream = ZipFile(source, fileName))
            {
                stream.Seek((long)0, SeekOrigin.Begin);
                fileBytes = new byte[checked(stream.Length)];
                stream.Read(fileBytes, 0, (int)fileBytes.Length);
            }
            return fileBytes;
        }
    }
}