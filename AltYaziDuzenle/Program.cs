using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace AltYaziDuzenle
{
    class Program
    {
        public class Time
        {
            int hour, min, sec, minsec;
            const int gap = -2;
            public Time(string time)
            {
                //{00:00:18,644}
                string[] tmp = time.ToString().Split(':');
                hour = Convert.ToInt32(tmp[0]);
                min = Convert.ToInt32(tmp[1]);
                sec = Convert.ToInt32(tmp[2].Split(',')[0]);
                minsec = Convert.ToInt32(tmp[2].Split(',')[1]);
            }

            public string GetTime()
            {
                DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, min, sec, minsec).AddSeconds(gap);
                return $"{dateTime.Hour.ToString("00")}:{dateTime.Minute.ToString("00")}:{dateTime.Second.ToString("00")},{dateTime.Millisecond.ToString("000")}";
            }

        }

        static void Main(string[] args)
        {
            string pattern = @"\d{2}\:\d{2}\:\d{2}\,\d{3}";
            Regex regex = new Regex(pattern);

            string path = @"AlphaGo 2017 - 1080p.srt";
            string text = File.ReadAllText(path, Encoding.GetEncoding("iso-8859-9"));

            MatchCollection matches = regex.Matches(text);
            foreach (var item in matches)
            {
                Time time = new Time(item.ToString());
                text = text.Replace(item.ToString(), time.GetTime());
            }
            Console.WriteLine("Islem tamamlandi....");
            Console.ReadLine();

            File.WriteAllText("yeni.srt", text);
        }
    }
}
