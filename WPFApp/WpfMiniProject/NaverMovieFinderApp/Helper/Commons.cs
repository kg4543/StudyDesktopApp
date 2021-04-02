using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace NaverMovieFinderApp
{
    class Commons
    {
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        public static async Task <MessageDialogResult> ShowMessageAsync(
            string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            return await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync(title, message, style, null);
        }

        public static string GetOpenApiResult(string openApiUrl, string clientID, string clientSecret)
        {
            var result = "";

            try
            {
                WebRequest request = WebRequest.Create(openApiUrl);
                request.Headers.Add("X-Naver-Client-ID", clientID);
                request.Headers.Add("X-Naver-Client-Secret", clientSecret);

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                result = reader.ReadToEnd();

                reader.Close();
                stream.Close();
                response.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외 발생 : {ex}");
            }

            return result;
        }

        /// <summary>
        /// HTML 태그 삭제 메서드
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StripHtmlTag(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", ""); //HTML 태그 삭제하는 정규 표현식
        }

        public static string StripPipe(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            return text.Substring(0, text.LastIndexOf("|")).Replace("|", ", ");

            /*string result = text.Replace("|",", ");
            return result.Substring(0, result.LastIndexOf(","));*/
        }

        public static string Stripamp(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            return text.Replace("&amp;", "");
        }
    }
}
