using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WorkingWithCSVFile.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WorkingWithCSVFile
{
    public class CsvProvider
    {
        private readonly string _url;
        public CsvProvider(string url)
        {
            _url = url;
        }

        public IEnumerable<CsvItemModel> DownloadCsv()
        {
            var csv = DownloadCSV(_url);
            var res = ConvertToModel(csv);
            return res;
        }

        public IEnumerable<CsvItemModel> LoadFromDisk(string path)
        {
            var csv = File.ReadAllText(path);
            var res = ConvertToModel(csv);
            return res;
        }

        public void SaveCsv(string path, IEnumerable<CsvItemModel> items)
        {
            var res = new StringBuilder();
            res.Append(GetCsvTitles() + Environment.NewLine);

            foreach (var item in items)
            {
                res.Append(ConvertToCsvString(item) + Environment.NewLine);
            }

            File.WriteAllText(path, res.ToString());
        }

        #region private methods

        private string DownloadCSV(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            var resp = (HttpWebResponse)req.GetResponse();

            var result = string.Empty;
            using (var sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }

            return result;
        }

        private IEnumerable<CsvItemModel> ConvertToModel(string data)
        {
            

            var res = new List<CsvItemModel>();
            var items = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in items.Skip(1))
            {

                var fields = ProcessLine(item);
                               
                if (fields.Length < 20)
                {
                    Console.WriteLine($"Invalid Data : {item}");
                    continue;
                }
                var mappedModel = MapTo(fields);
                res.Add(mappedModel);
            }

            return res;
        }

        private string[] ProcessLine(string text)
        {
            if (!text.Contains("\""))
                return text.Split(',');
            
            var openQu = false;
            var sb = new StringBuilder();
            foreach (char nextChar in text)
            {
                
                if (nextChar == ',' && !openQu)
                {
                    sb.Append("\",\"");
                    continue;
                }
                if (nextChar == '"' && !openQu)
                {
                    sb.Append(nextChar);
                    openQu = true;
                    continue;
                }

                if (nextChar == '"' && openQu)
                {
                    sb.Append(nextChar);
                    openQu = false;
                    continue;
                }
                sb.Append(nextChar);

            }
            if(openQu)
                sb.Append("\"");
            
            return sb.ToString().Split(new[] { "\",\"" }, StringSplitOptions.None);
        }


        private CsvItemModel MapTo(string[] items)
        {
            return new CsvItemModel()
            {
                DHNo = string.IsNullOrWhiteSpace(items[0])
                     ? (int?)null
                     : Convert.ToInt32(items[0]),
                Network =  items[1],
                UnitNumber = string.IsNullOrWhiteSpace(items[2])
                     ? (int?)null
                     : Convert.ToInt32(items[2]),
                Aquifer =  items[3],
                Easting = string.IsNullOrWhiteSpace(items[4])
                     ? (decimal?)null
                     : Convert.ToDecimal(items[4]),
                Northing = string.IsNullOrWhiteSpace(items[5])
                     ? (decimal?)null
                     : Convert.ToDecimal(items[5]),
                Zone = string.IsNullOrWhiteSpace(items[6])
                     ? (int?)null
                     : Convert.ToInt32(items[6]),
                UnitNo = items[7],
                ObsNo =  items[8],
                ObsDate = string.IsNullOrWhiteSpace(items[9])
                    ? (DateTime?)null
                    : Convert.ToDateTime(DateTime.ParseExact(items[9], "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                Swl = string.IsNullOrWhiteSpace(items[10])
                    ? (decimal?)null
                    : Convert.ToDecimal(items[10]),
                Rswl = string.IsNullOrWhiteSpace(items[11])
                    ? (decimal?)null
                    : Convert.ToDecimal(items[11]),
                Pressure = string.IsNullOrWhiteSpace(items[12])
                    ? (decimal?)null
                    : Convert.ToDecimal(items[12]),
                Temperature = string.IsNullOrWhiteSpace(items[13])
                    ? (decimal?)null
                    : Convert.ToDecimal(items[13]),
                DryInd = items[14],
                AnomInd = items[15],
                PumpInd = items[16],
                MeasuredDuring = items[17],
                DataSource =  items[18],
                Comments =  items[19]
            };
        }

        private int i=0;
        private string ConvertToCsvString(CsvItemModel data)
        {
            i++;
            var res = $"{i},";
            
            var prps = data.GetType().GetProperties();
            
            foreach (var prop in prps)
            {
                
                if (prop.Name == "UnitNo")
                    continue;

                var val = prop.GetValue(data)?.ToString();
                
                if (prop.PropertyType == typeof(DateTime?))
                    val = ((DateTime?)prop.GetValue(data))?.ToString("dd/MM/yyy");

                res += $"{val},";
            }
            res = res.Remove(res.Length-1, 1);
            return res;
        }

        private string GetCsvTitles()
        {
            var res = "0,";

            var prps = typeof(CsvItemModel).GetProperties();
            foreach (var prop in prps)
            {
                if (prop.Name == "UnitNo")
                    continue;

                var dispAttr = (DisplayAttribute)prop.GetCustomAttributes(typeof(DisplayAttribute), false)?.First();
                res += $"{dispAttr.Name},";
            }
            res = res.Remove(res.Length - 1, 1);
            return res;
        }

        #endregion
    }
}
