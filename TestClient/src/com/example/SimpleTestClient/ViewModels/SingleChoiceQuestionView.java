package com.example.SimpleTestClient.ViewModels;

import TestLibrary.Answer;
import TestLibrary.Client.ClientQuestion;
import TestLibrary.MediaFile;
import TestLibrary.Questions.QSingleChoice;
import TestLibrary.QuestionsManager;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.*;
import com.example.SimpleTestClient.Adapters.ImageAdapter;
import com.example.SimpleTestClient.Adapters.ImageAdapterItem;
import com.example.SimpleTestClient.R;

import java.util.ArrayList;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: Pit
 * Date: 12.07.13
 * Time: 21:45
 * To change this template use File | Settings | File Templates.
 */
public class SingleChoiceQuestionView extends LinearLayout
{
    public interface AnswerChooseListener
    {
        public void OnAnswerChoose(Object choose);
    }

    AnswerChooseListener answerChooseListener;
    ClientQuestion SavedAnswer = null;

    QSingleChoice QuestionData;


    Context context;
    LinearLayout layout;
    ListView AnswersList;
    GridView ImagesView;
    TextView MainTextView;
    TextView ResultView;
    TextView HtmlView;
    public SingleChoiceQuestionView(Context context) {
        super(context);
        this.context = context;

        initComponent();
    }

    public void SetQuestionData(QSingleChoice data)
    {
        this.QuestionData = data;

        this.SavedAnswer = QuestionsManager.GetClientQuestion(this.QuestionData.Number);
        if(this.SavedAnswer != null)
        {
            ShowSaved(this.SavedAnswer);
        }
    }

    public void  SetChooseListener(AnswerChooseListener listener)
    {
        this.answerChooseListener = listener;
    }

    private void OnAnswerChoose(Object choose)
    {
      if(answerChooseListener != null)
      {
          this.ResultView.setText("Ваш выбор: " + choose.toString());
          answerChooseListener.OnAnswerChoose(choose);
      }
    }

    private void initComponent() {

        LayoutInflater inflater = (LayoutInflater) getContext().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        inflater.inflate(R.layout.fargment_single_choice_question, this);

        this.layout = (LinearLayout)findViewById(R.id.fargment_single_choice_question_linear_layout);
        //this.AnswersList  = (ListView)findViewById(R.id.fargment_single_choice_question_answerslist);
       // this.ImagesView = (GridView)findViewById(R.id.view_images_gallery_gallery);
        this.MainTextView = (TextView)findViewById(R.id.fargment_single_choice_question_maintextview);
       // this.ResultView = (TextView)findViewById(R.id.fargment_single_choice_question_resultview);

        ShowMediaData();
        ShowAnswers();
    }

    private  void ShowMediaData()
    {
        if(this.QuestionData.Data.Text != null)
        {
            TextView tv = new TextView(context);
            tv.setText(this.QuestionData.Data.Text);
            this.layout.addView(tv);
        }

        if(this.QuestionData.Data.Files.size() != 0)
        {
            List<MediaFile> images =this.QuestionData.Data.GetImages();
            final ArrayList<ImageAdapterItem> Items = new ArrayList<ImageAdapterItem>();
            if(images != null)
            {
                for (MediaFile image : images)
                {
                    ImageAdapterItem item = new ImageAdapterItem(image.Bytes.getBytes(),image.Text);
                    Items.add(item);
                }

                ImageAdapter adapter =  new ImageAdapter(this.context,Items);
                this.ImagesView.setAdapter(adapter);
                this.ImagesView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                        ImageAdapterItem item = Items.get(i);
                        //ImageTextDialog dialog = new ImageTextDialog(context,item.bitmap,item.text);
                       // dialog.show();
                    }
                });
            }

//            List<MediaFile> htmls = this.QuestionData.Data.GetHtmlFiles();
//            if(htmls != null)
//            {
//                //show html
//
//            }
        }

    }

    private void ShowAnswers()
    {
        final int AnswersCount = this.QuestionData.Answers.size();
        String[] items = new String[AnswersCount];
        for(int i =0; i<AnswersCount; i++)
        {
            Answer el = (Answer) this.QuestionData.Answers.get(i);
            items[i] = el.Data.Text;
        }

        AnswersList = new ListView(context);
        AnswersList.setLayoutParams(new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.MATCH_PARENT, 3.0f));
        AnswersList.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
        AnswersList.setAdapter(new ArrayAdapter<String>(context , android.R.layout.simple_list_item_single_choice, items));
        AnswersList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                OnAnswerChoose(i);
            }
        });


    }

    private void ShowSaved(ClientQuestion cq)
    {

    }


}
