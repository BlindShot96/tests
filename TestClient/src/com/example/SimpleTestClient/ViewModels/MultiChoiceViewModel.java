package com.example.SimpleTestClient.ViewModels;

import TestLibrary.Answer;
import TestLibrary.Client.ClientQuestion;
import TestLibrary.Client.ClientQuestionAnswer;
import TestLibrary.MediaData;
import TestLibrary.Questions.QMultiChoice;
import android.content.Context;

import java.util.ArrayList;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 25.08.13
 * Time: 19:53
 * To change this template use File | Settings | File Templates.
 */
public class MultiChoiceViewModel extends QuestionBaseViewModel {
    public MultiChoiceViewModel(Context context, QMultiChoice questionModel) {
        super(context, questionModel);
    }

    public static String ARG_SelectedAnswers = "SelectedAnswers";

    /**
     * выбранные ответы
     */
    ArrayList<Answer> selectedAnswers = new ArrayList<Answer>();
    public ArrayList<Answer> getSelectedAnswers() {
        return selectedAnswers;
    }
    public void setSelectedAnswers(ArrayList<Answer> selectedAnswers) {
        this.selectedAnswers = selectedAnswers;
        if(selectedAnswers.size() > 0){this.isAnswered = true;}
        else if(selectedAnswers.size() == 0){this.isAnswered = false;}
        if(onPropertyChangedListener != null)
        {
            this.onPropertyChangedListener.OnPropertyChange(this,ARG_SelectedAnswers);
        }
    }
    public void setSelectedAnswers(int[] selectedNumbers) {
        ArrayList<Answer> res = new ArrayList<Answer>();
        for (int num:selectedNumbers)
        {
            try{
                  res.add((Answer) this.questionModel.Answers.get(num));
            }
            catch (Exception ex){}
        }
        setSelectedAnswers(res);
    }
    //------------
    public void addSelectedAnswer(Answer answer)
    {
        this.isAnswered = true;
        if(this.selectedAnswers.contains(answer) != true)
        {
           this.selectedAnswers.add(answer);
           onPropertyChangedListener.OnPropertyChange(this,ARG_SelectedAnswers);
        }
    }

    public void addSelectedAnswer(int num)
    {
        try{
           addSelectedAnswer(this.getQuestionModel().Answers.get(num));
        }
        catch (Exception ex)
        {}
    }
     //--------------
    public void addWithRemoveSelectedAnswer(int num)
    {
        try{
            addWithRemoveSelectedAnswer(this.getQuestionModel().Answers.get(num));
        }
        catch (Exception ex)
        {}
    }
    public void addWithRemoveSelectedAnswer(Answer answer)
    {
        if(this.selectedAnswers.contains(answer) != true)
        {
            this.addSelectedAnswer(answer);
        }
        else
        {
            this.removeSelectedAnswer(answer);
        }
    }
    //----------------
    public void removeSelectedAnswer(Answer answer)
    {
        if(this.selectedAnswers.contains(answer) == true)
        {
            this.selectedAnswers.remove(answer);
            onPropertyChangedListener.OnPropertyChange(this,ARG_SelectedAnswers);
        }

        if(this.selectedAnswers.size() == 0)
        {
            this.isAnswered = false;
        }
    }
    public void removeSelectedAnswer(int num)
    {
        try{
            removeSelectedAnswer(this.getQuestionModel().Answers.get(num));
        }
        catch (Exception ex)
        {}
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
        if(this.questionModel.Data != null)
        {
            return this.questionModel.Data;
        }
        else
        {
            return null;
        }
    }


//    @Override
//    public void Save() {
//        if(selectedAnswers.size() > 0)
//        {
//            for(Answer an : this.getSelectedAnswers())
//            {
//                ClientQuestionAnswer ans = new ClientQuestionAnswer();
//                ans.Data.Text = an.ID;
//                this.clientQuestion.Answers.add(ans);
//            }
//
//            super.Save();
//        }
//        else
//        {
//            Common.CommonMethods.DialogUtils.ShowToast("Вы не выбрвли ответ",this.context);
//            this.isAnswered = false;
//        }
//    }
//
//    @Override
//    public void loadSavedAnswer() {
//        if(selectedAnswers.size() > 0)
//        {
//            onPropertyChangedListener.OnPropertyChange(this,ARG_SelectedAnswers);
//        }
//    }

    @Override
    public ClientQuestion MakeClientQuestion(){
        if(selectedAnswers.size() != 0)
        {
            ClientQuestion res = super.MakeClientQuestion();
            res.Answers = new ArrayList<ClientQuestionAnswer>();

            for(Answer answer : this.selectedAnswers)
            {
              ClientQuestionAnswer ans = new ClientQuestionAnswer();
              ans.Data.Text = answer.ID;
              res.Answers.add(ans);
            }

            return  res;
        }
        return null;
    }

    @Override
    public void LoadAnswers(List<ClientQuestionAnswer> clientQuestionAnswers) {
        for (ClientQuestionAnswer Cans: clientQuestionAnswers)
        {
            String id = Cans.Data.Text;
            int num = 0;
            for (Answer ans : getQuestionModel().Answers)
            {
               if(ans.ID.equals(id))
               {
                  this.addSelectedAnswer(num);
               }
               num++;
            }
        }
    }


}
