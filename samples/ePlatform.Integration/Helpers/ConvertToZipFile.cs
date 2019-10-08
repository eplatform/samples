using System;
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
        public MemoryStream UnzipFile(Stream zipStream)
        {
            var unzipStream = new MemoryStream();
            using (ZipArchive zip = new ZipArchive(zipStream))
            {
                if (zip.Entries.Count == 0)
                {
                    throw new Exception("Zip içerisinde dosya yok.");
                }
                if (zip.Entries.Count > 1)
                {
                    throw new Exception("Zip içerisinde birden fazla dosya bulunamaz.");
                }

                var zipEntry = zip.Entries[0];
                using (var stream = zipEntry.Open())
                {
                    stream.CopyTo(unzipStream);
                }
            }
            unzipStream.Seek((long)0, SeekOrigin.Begin);
            return unzipStream;
        }
        public MemoryStream UnzipFile(byte[] data)
        {
            MemoryStream memoryStream;
            using (var stream = new MemoryStream(data))
            {
                memoryStream = UnzipFile(stream);
            }
            return memoryStream;
        }
    }
}