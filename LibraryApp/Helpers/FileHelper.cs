using iTextSharp.text;
using iTextSharp.text.pdf;
using LibraryApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Helpers
{
    public class FileHelper
    {
        public static void Write(object obj,string filename)
        {
            var serializer=new JsonSerializer();
            using (var sw=new StreamWriter(filename))
            {
                using (var jw=new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, obj);
                }
            }
        }

        public static T Read<T>(string filename) where T : class
        {
            var serializer = new JsonSerializer();
            using (var sr=new StreamReader(filename))
            {
                using (var jr=new JsonTextReader(sr))
                {
                    var result = serializer.Deserialize<T>(jr);
                    return result;
                }
            }
        }

        public static void WritePdf(Student s)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pdfFilePath = Path.Combine(desktopPath, $"{s.ID}-student.pdf");
            using (FileStream fs = new FileStream(pdfFilePath, FileMode.Create))
            {
                Document doc = new Document(PageSize.A5);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();


                string billtxt = $@"
                -------------------------------------------
                |          Student NO BILL                       
                |  FULLNAME {s.Fullname}           
                |  Age      {s.Age}   
                |  Your ID  {s.ID}
                |  please do not forget you ID {s.ID}
                --------------------------------------------
                    ";


                Paragraph paragraph = new Paragraph(billtxt);
                doc.Add(paragraph);

                doc.Close();
            }
        }
    }
}
