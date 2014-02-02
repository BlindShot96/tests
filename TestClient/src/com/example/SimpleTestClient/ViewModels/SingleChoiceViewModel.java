package com.example.SimpleTestClient.ViewModels;

import TestLibrary.Answer;
import TestLibrary.Client.ClientQuestion;
import TestLibrary.Client.ClientQuestionAnswer;
import TestLibrary.MediaData;
import TestLibrary.Questions.QSingleChoice;
import android.content.Context;

import java.util.ArrayList;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: Pit
 * Date: 17.08.13
 * Time: 12:37
 * To change this template use File | Settings | File Templates.
 */
public class SingleChoiceViewModel extends QuestionBaseViewModel
{

    public static String ARG_SelectedAnswer = "SelectedAnswer";

    /**
     * выбранный ответ
     */
    Answer selectedAnswer;
    public Answer getSelectedAnswer() {
        return selectedAnswer;
    }
    public void setSelectedAnswer(Answer selectedAnswer) {
        this.selectedAnswer = selectedAnswer;
        this.isAnswered = true;
        if(onPropertyChangedListener != null)
        {
          this.onPropertyChangedListener.OnPropertyChange(this,ARG_SelectedAnswer);
        }
    }
    public void setSelectedAnswer(int num) {
        setSelectedAnswer((Answer) this.questionModel.Answers.get(num));
    }

    /**
     * список ответа на вопрос
     * @return
     */
    public ArrayList<Answer> getAnswers()
    {
        ArrayList<Answer> res = new ArrayList<Answer>();
        for(Answer ans : this.questionModel.Answers)
        {
             res.add(ans);
        }
        return res;
    }

    /**
     * текст вопроса
     * @return
     */
    public String getQuestionText()
    {
        if(questionModel.Data != null){
          return questionModel.Data.Text;
        }
        else
        {
            return null;
        }
    }

    /**
     * данные вопроса
     * @return
     */
    public MediaData getQuestionData()
    {
//        if(this.questionModel.Data != null)
//        {
            return this.questionModel.Data;
//        }
//        else
//        {
//            return null;
//        }
    }

    public SingleChoiceViewModel(Context context, QSingleChoice questionModel) {
        super(context, questionModel);
    }

//    @Override
//    public void Save() {
//        if(selectedAnswer != null)
//        {
//            ClientQuestionAnswer ans = new ClientQuestionAnswer();
//            ans.Data.Text = selectedAnswer.ID;
//            this.clientQuestion.Answers.add(ans);
//            super.Save();
//        }
//        else
//        {
//            Common.CommonMethods.DialogUtils.ShowToast("Вы не выбрвли ответ",this.context);
//        }
//    }

//    @Override
//    public void loadSavedAnswer() {
//       if(selectedAnswer != null)
//       {
//           onPropertyChangedListener.OnPropertyChange(this,ARG_SelectedAnswer);
//       }
//    }

    @Override
    public ClientQuestion MakeClientQuestion(){
        if(selectedAnswer != null){
        ClientQuestion res = super.MakeClientQuestion();
        res.Answers = new ArrayList<ClientQuestionAnswer>();

        ClientQuestionAnswer ans = new ClientQuestionAnswer();
        ans.Data.Text = this.selectedAnswer.ID;

        res.Answers.add(ans);
        return  res;
        }
        return null;
    }

    @Override
    public void LoadAnswers(List<ClientQuestionAnswer> clientQuestionAnswers) {
        ClientQuestionAnswer cqa = clientQuestionAnswers.get(0);

        int num =0;
        for (Answer ans : getQuestionModel().Answers)
        {
            if(ans.ID.equals(cqa.Data.Text))
            {
                this.setSelectedAnswer(num);
            }
            num++;
        }
    }


}
