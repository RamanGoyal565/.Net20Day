using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseStudy9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Report report = ReportFactory.Create("PDF");
            report.Rows = new List<object>
            {
                new { Name = "Ravi", Sales = 5000 },
                new { Name = "Anu", Sales = 6200 }
            };
            report.Generate();
            Console.WriteLine(report.Export());
        }
    }

    public interface IExportable
    {
        string Export();
    }

    public abstract class Report : IExportable
    {
        public List<object> Rows { get; set; }
        public abstract void Generate();
        public abstract string Export();
    }

    public class PdfReport : Report
    {
        public override void Generate() { Console.WriteLine("Generating PDF report"); }
        public override string Export() { return "PDF Export: " + Rows.FormatRows(); }
    }

    public class ExcelReport : Report
    {
        public override void Generate() { Console.WriteLine("Generating Excel report"); }
        public override string Export() { return "Excel Export: " + Rows.FormatRows(); }
    }

    public class CsvReport : Report
    {
        public override void Generate() { Console.WriteLine("Generating CSV report"); }
        public override string Export() { return "CSV Export: " + Rows.FormatRows(); }
    }

    public static class ReportFactory
    {
        public static Report Create(string type)
        {
            if (type.Equals("PDF", StringComparison.OrdinalIgnoreCase)) return new PdfReport();
            if (type.Equals("Excel", StringComparison.OrdinalIgnoreCase)) return new ExcelReport();
            return new CsvReport();
        }
    }

    public static class ReportExtensions
    {
        public static string FormatRows(this List<object> rows)
        {
            return string.Join(" | ", rows.Select(delegate(object row) { return row.ToString(); }));
        }
    }
}