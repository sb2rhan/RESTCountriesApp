using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;

namespace RestCountriesApp.Utilities
{
    /// <summary>
    /// Parser class for any type of data
    /// </summary>
    public static class DataParser
    {
        /// <summary>
        /// Converts the data of svg file to Base64 format
        /// </summary>
        /// <param name="pathToFile">Url path to svg file</param>
        /// <returns>A string in Base64 Format</returns>
        public static string EncodeSvgFileToBase64(string pathToFile)
        {
            using MemoryStream memoryStream = DataAccess.DownloadSvgFile(pathToFile);

            byte[] imageBytes = memoryStream.ToArray();

            return Convert.ToBase64String(imageBytes, Base64FormattingOptions.None);
        }

    }
}
