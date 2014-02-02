package com.example.SimpleTestClient.ViewModels;

import TestLibrary.Abstract.QuestionBase;
import TestLibrary.Client.ClientQuestion;
import TestLibrary.Client.ClientQuestionAnswer;
import TestLibrary.QuestionsManager;
import android.content.Context;

import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: Pit
 * Date: 17.08.13
 * Time: 12:15
 * To change this template use File | Settings | File Templates.
 */
public abstract class QuestionBaseViewModel
{
    public static class StringProperties
    {
        public static String QuestionModel = "QuestionModel";
        public static String ClientQuestion = "ClientQuestion";
    }

    public QuestionBaseViewModel(Context context, QuestionBase questionModel) {
        this.context = context;
        this.questionModel = questionModel;
    }

    /**
     * событие изменения открытого свойства
     */
    protected  OnPropertyChanged onPropertyChangedListener=new OnPropertyChanged() {
        @Override
        public void OnPropertyChange(Object sender, String propertyName) {
        }
    };
    public void setOnPropertyChangedListener(OnPropertyChanged onPropertyChangedListener) {
        if(onPropertyChangedListener != null){
           this.onPropertyChangedListener = onPropertyChangedListener;
        }
        else
        {
            throw new IllegalArgumentException();
        }
    }


    /**
     * Модель вопроса
     */
    protected QuestionBase questionModel;
    public QuestionBase getQuestionModel() {
        return questionModel;
    }
    public void setQuestionModel(QuestionBase questionModel) {
        this.questionModel = questionModel;
        onPropertyChangedListener.OnPropertyChange(this,StringProperties.QuestionModel);
    }

    /**
     * Ответ клиента на вопрос
     */
    protected ClientQuestion clientQuestion = new ClientQuestion();
    public ClientQuestion getClientQuestion() {
        return clientQuestion;
    }
    public void setClientQuestion(ClientQuestion clientQuestion) {
        this.clientQuestion = clientQuestion;
        onPropertyChangedListener.OnPropertyChange(this,StringProperties.ClientQuestion);
    }


    /**
     * контекст
     */
    protected Context context;
    public Context getContext() {
        return context;
    }
    public void setContext(Context context) {
        this.context = context;
    }


    /**
     * ответили ли на этот вопрос
     */
    protected boolean isAnswered;
    public boolean isAnswered() {
        return isAnswered;
    }



    public String Name()
    {
        return this.questionModel.Name;
    }

    /**
     * сохранение в ответ клиента
     */
    public  void  Save()
    {
        if(clientQuestion != null)
        {
          this.clientQuestion.QuestionID = this.questionModel.ID;
          QuestionsManager.AddClientQuestionAnswer(this.clientQuestion);
        }
    }

    public ClientQuestion MakeClientQuestion()
    {
        ClientQuestion res = new ClientQuestion();
        res.QuestionID = this.getQuestionModel().ID;
        return res;
    }

    public abstract void LoadAnswers(List<ClientQuestionAnswer> clientQuestionAnswers);


//    /**
//     * загрузка сохранённого ответа
//     */
//    public abstract void loadSavedAnswer();

    //интерфейсы
    public interface OnPropertyChanged
    {
        public void OnPropertyChange(Object sender,String propertyName);
    }
}
