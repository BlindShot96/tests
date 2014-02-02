package TestLibrary;

import TestLibrary.Abstract.QuestionBase;
import TestLibrary.Abstract.TestBase;
import TestLibrary.Questions.QMultiChoice;
import TestLibrary.Questions.QSingleChoice;
import TestLibrary.Questions.QTextAnswer;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Pit on 11.06.13.
 */
public class Test extends TestBase {

    public String[] GetHeadLines()
    {
        String[] res = new String[Questions.size()];
        for(int i=0;i<Questions.size();i++)
        {
            res[i] = String.valueOf(i + 1);
        }
        return res;
    }

    public List<String> GetListHeadlines()
    {
        List<String> res = new ArrayList<String>();
        for(int i=0;i<Questions.size();i++)
        {
            res.add(String.valueOf(i + 1));
        }
        return res;
    }

    public static Test CreateTest()
    {
        Test res = new Test();
        res.Settings.Name = "test1";
        res.Settings.TeacherName = "teacher1";
        QuestionBase q1 = new QSingleChoice();
        {
            q1.Name = "Test.CreateTest()";
            q1.ID = "id1";
            q1.Number =0;
            q1.Data.Text = "Мячик массой m бросили с земли вертикально вверх. Через время t после броска мячик оказался на максимальной высоте.\n" +
                    "Чему равен модуль изменения импульса мячика за это время? Ускорение свободного падения равно g.  Сопротивление воздуха не учитывать.";

            Answer a1 = new Answer();
            a1.Data.Text = "2mgt";
            a1.ID = "ans1";

            Answer a2 = new Answer();
            a2.Data.Text = "mgt/2";
            a2.ID = "ans2";

            Answer a3 = new Answer();
            a3.Data.Text = "mgt";
            a3.ID = "ans3";

            Answer a4 = new Answer();
            a4.Data.Text = "mg/t";
            a4.ID = "ans4";

            q1.Answers.add(a1);
            q1.Answers.add(a2);
            q1.Answers.add(a3);
            q1.Answers.add(a4);

            MediaFile file = new MediaFile();
            file.FileName = "LongText";
            file.FileExtension = ".txt";
            file.ID = "file1";
            file.Text = "text";
            file.Bytes = "masha shla po shosse".getBytes().toString();

            q1.Data.Files.add(file);
        }

        QuestionBase q2 = new QMultiChoice();
        {
            q2.Name = "Вопрос 2";
            q2.ID = "id2";
            q2.Number =1;
            q2.Data.Text = "Небольшое тело движется вдоль оси OX. Его координата x изменяется с течением времени t по закону \n" +
                    "  x(t)=2+t−t2, где t  выражено в секундах, а x – в метрах.\n" +
                    " Чему равна проекция ускорения этого тела на ось OX в момент времени t=1 с?";

            Answer a1 = new Answer();
            a1.Data.Text = "–1 м/с2";
            a1.ID = "ans1";

            Answer a2 = new Answer();
            a2.Data.Text = "2 м/с2";
            a2.ID = "ans2";

            Answer a3 = new Answer();
            a3.Data.Text = "–2 м/с2";
            a3.ID = "ans3";

            Answer a4 = new Answer();
            a4.Data.Text = "1 м/с2";
            a4.ID = "ans4";

            q2.Answers.add(a1);
            q2.Answers.add(a2);
            q2.Answers.add(a3);
            q2.Answers.add(a4);
        }

        QuestionBase q3 = new QTextAnswer();
        {
            q3.Name = "Вопрос 2";
            q3.ID = "id2";
            q3.Number =1;
            q3.Data.Text = "Напишите ваше имя";
        }

        res.Questions.add(q1);
        res.Questions.add(q2);
        res.Questions.add(q3);
       // res.SortByNumber();

        return  res;
    }
}
