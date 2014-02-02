package TestLibrary;

/**
 * Created with IntelliJ IDEA.
 * User: Pit
 * Date: 02.07.13
 * Time: 18:30
 * To change this template use File | Settings | File Templates.
 */

import TestLibrary.Abstract.QuestionBase;
import TestLibrary.Client.ClientQuestion;
import android.content.Context;
import android.widget.LinearLayout;
import com.example.SimpleTestClient.Utils.Common;

import java.util.HashMap;

/**
 * класс для управлением тестом
 */
public class QuestionsManager
{
   private static HashMap<Integer,QuestionBase> QuestiosByNumber;
   private static QuestionBase CurrentQuestion = null;

   public static void ShowQuestion(int Number,LinearLayout layout, Context context)
   {
       QuestionBase question = GetQuestionByNumber(Number);
       CurrentQuestion = question;

       if(question == null)
       {throw new IllegalArgumentException();}

      // question.Draw(layout,context);

   }

   public static  void AddClientQuestionAnswer(ClientQuestion CQ)
   {

       if(Common.GlobalValues.ClientCommon.Data.Report.containsId(CQ.QuestionID) == true)
       {
           //Common.GlobalValues.ClientCommon.Data.Report.Questions.get(Common.GlobalValues.ClientCommon.Data.Report.Questions.indexOf(Common.GlobalValues.ClientCommon.Data.Report.getById(CQ.QuestionID)));
           Common.GlobalValues.ClientCommon.Data.Report.Questions.toArray()[
                   Common.GlobalValues.ClientCommon.Data.Report.Questions.indexOf(
                           Common.GlobalValues.ClientCommon.Data.Report.getById(CQ.QuestionID))] =CQ;
       }
       else
       {
           Common.GlobalValues.ClientCommon.Data.Report.Questions.add(CQ);
       }
   }

    public  static  ClientQuestion GetClientQuestion(int QuestionNumber)
    {
        QuestionBase qB = GetQuestionByNumber(QuestionNumber);
        if(qB == null){throw new IllegalArgumentException();}

        ClientQuestion Cq = Common.GlobalValues.ClientCommon.Data.Report.getById(qB.ID);
        return  Cq;
    }

    public static  QuestionBase GetQuestionByNumber(int Number)
    {
      if(QuestiosByNumber == null)
      {
            MakeQuestionsByNumberHashMap();
      }

      for (QuestionBase q : QuestiosByNumber.values())
      {
          if(q.Number == Number)
          {
              return  q;
          }
      }
      return  null;
    }

   public static  void MakeQuestionsByNumberHashMap()
   {
       QuestiosByNumber = new HashMap<Integer, QuestionBase>();
       for(QuestionBase qEl : Common.GlobalValues.ClientCommon.test.Questions)
       {
           QuestiosByNumber.put(qEl.Number, qEl);
       }
   }

    public static void ShowNextQuestion(int Current,LinearLayout layout, Context context)
    {
      int Next = Current+1;
      if(Next >= Common.GlobalValues.ClientCommon.test.Questions.size())
      {
          throw new Error();
      }

      ShowQuestion(Next,layout,context);
    }

    public static  void ShowPreviousQuestion(int Current,LinearLayout layout, Context context)
    {
        int Previous = Current-1;
        if(Previous < 1)
        {
            throw new Error();
        }

        ShowQuestion(Previous,layout,context);
    }

}
