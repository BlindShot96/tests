package com.example.SimpleTestClient.ViewModels;

import TestLibrary.Client.ClientQuestion;
import TestLibrary.Client.ClientQuestionAnswer;
import TestLibrary.MediaData;
import TestLibrary.Questions.QTextAnswer;
import android.content.Context;

import java.util.ArrayList;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 01.09.13
 * Time: 13:22
 * To change this template use File | Settings | File Templates.
 */
public class TextChoiceViewModel  extends QuestionBaseViewModel
{
    public static String ARG_SelectedAnswer = "SelectedAnswer";

    /**
     * выбранный ответ
     */
    String textAnswer="#Null";
    public String getTextAnswer() {
        if(textAnswer == null){return "";}
        if(textAnswer.equals("#Null") == false)
        {
            return textAnswer;
        }
        else
        {
            return null;
        }
    }
    public void setTextAnswer(String textAnswer) {
        this.textAnswer = textAnswer;

        this.isAnswered = true;

        if(onPropertyChangedListener != null)
        {
            this.onPropertyChangedListener.OnPropertyChange(this,ARG_SelectedAnswer);
        }
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
        if(this.questionModel.Data != null)
        {
            return this.questionModel.Data;
        }
        else
        {
            return null;
        }
    }

    public TextChoiceViewModel(Context context, QTextAnswer questionModel) {
        super(context, questionModel);
    }

//    @Override
//    public void Save() {
//            ClientQuestionAnswer ans = new ClientQuestionAnswer();
//            ans.Data.Text = textAnswer;
//            this.clientQuestion.Answers.add(ans);
//            super.Save();
//    }
//
//    @Override
//    public void loadSavedAnswer() {
//        if(this.isAnswered == true)
//        {
//            onPropertyChangedListener.OnPropertyChange(this,ARG_SelectedAnswer);
//        }
//    }

    @Override
    public ClientQuestion MakeClientQuestion(){

            ClientQuestion res = super.MakeClientQuestion();
            res.Answers = new ArrayList<ClientQuestionAnswer>();

            ClientQuestionAnswer ans = new ClientQuestionAnswer();
            ans.Data.Text = textAnswer;

            res.Answers.add(ans);
            return  res;
    }

    @Override
    public void LoadAnswers(List<ClientQuestionAnswer> clientQuestionAnswers) {
        ClientQuestionAnswer ans = clientQuestionAnswers.get(0);
        setTextAnswer(ans.Data.Text);
    }


}
