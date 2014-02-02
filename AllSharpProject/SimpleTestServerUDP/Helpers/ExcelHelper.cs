using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using SimpleTestServerUDP.Internet;
using TestLibrary;

namespace SimpleTestServerUDP.Helpers
{
    class ExcelHelper
    {
        Application myExcelApp;
        Workbook myExcelWorkbook;
        Worksheet myExcelWorksheet;

        public void MakeDocument(List<Client> clients,  Test originalTest)
        {
            myExcelApp = new Application();
            myExcelWorkbook = myExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            myExcelWorksheet = (Worksheet)myExcelWorkbook.Sheets[1];

            List<string> headers = new List<string>();
            //заполнение столбцов
            headers.Add("Статус");
            headers.Add("Имя");
            headers.Add("Фамилия");
            headers.Add("Группа");
            headers.Add("Оценка");
            headers.Add("Процент");
            foreach (var question in originalTest.Questions)
            {
                headers.Add(question.Name);
            }

            for (int i = 0; i < headers.Count; i++)
            {
                myExcelWorksheet.Cells[1, i+1] = headers[i];
            }

            //--------------------


            int clientStroke = 2;
            foreach (Client c in clients)
            {
                List<string> items = new List<string>();
                if (c.Status == Internet.Status.AnswersVerified)
                {
                    items = new List<string>(new string[6] { c.Status.ToString(), c.Data.Name, c.Data.LastName, c.Data.Group, c.Data.Result.Mark.ToString(), c.Data.Result.Percent.ToString() });
                    foreach (var cq in c.Data.Report.Questions)
                    {
                        items.Add(cq.Ball.ToString());
                    }

                }
                if (c.Status == Internet.Status.RecieveTest)
                {
                    items = new List<string>(new string[6] { c.Status.ToString(), c.Data.Name, c.Data.LastName, c.Data.Group, "Не проверено", "Не проверено" });
                }

                for (int i = 0; i < items.Count; i++)
                {
                    myExcelWorksheet.Cells[clientStroke, i+1] = items[i];
                }

                clientStroke++;
            }
        }

        public void Save(string filename)
        {
            if (myExcelWorksheet != null)
            {
                myExcelWorksheet.SaveAs(filename);
            }
        }

    }
}
