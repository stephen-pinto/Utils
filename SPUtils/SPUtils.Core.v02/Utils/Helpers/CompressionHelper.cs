using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace SPUtils.Core.v02.Utils.Helpers
{
    public class CompressionHelper
    {
        public byte[] Compress(string content)
        {
            var contentBytes = Encoding.UTF8.GetBytes(content);
            byte[] result = null;
            
            using (var memInputStream = new MemoryStream(contentBytes, false))
            {
                using (var memOutputStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(memOutputStream, CompressionLevel.Optimal))
                    {
                        int bytesRead = 0;
                        byte[] buffer = new byte[4096];
                        
                        while((bytesRead = memInputStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            gzipStream.Write(buffer, 0, bytesRead);
                        }
                    }

                    result = memOutputStream.ToArray();
                }
            }

            return result;
        }

        public string UnCompress(byte[] commpressedContent)
        {
            string result = null;

            using (var memInputStream = new MemoryStream(commpressedContent, false))
            {
                using (var memOutputStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(memOutputStream, CompressionMode.Decompress))
                    {
                        int bytesRead = 0;
                        byte[] buffer = new byte[4096];
                        
                        while ((bytesRead = gzipStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            memOutputStream.Write(buffer, 0, bytesRead);
                        }
                    }

                    result = Encoding.UTF8.GetString(memOutputStream.ToArray());
                }
            }

            return result;
        }
    }
}
