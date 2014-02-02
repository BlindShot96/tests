package TestLibrary.Questions;

import TestLibrary.Abstract.QuestionBase;

/**
 * Created by Pit on 11.06.13.
 */
public class QSingleChoice extends QuestionBase {

    public QSingleChoice()
    {
        this.Type = QuestionType.SingleChoice;
    }

        //implements Common.CommonInterfaces.OnSelectHandler,
//        DialogInterface.OnClickListener, View.OnClickListener{
//
//    ClientQuestion mClientQuestion;
//    ClientQuestionAnswer mClientQuestionAnswer;
//
//    Context context;
//    LinearLayout layout;
//    int selected;
//    TextView SelectedInfo;
//    String s_text = "Ваш выбор: ";
//    boolean  IsSaved =false;
//
//    @Override
//    public void Draw(LinearLayout Question_layout, Context context) {
//
//        if(Common.GlobalValues.ClientCommon.Data.Report.Questions.containsKey(new StringElement(this.ID)) == false)
//        {
//            mClientQuestion = new ClientQuestion();
//            mClientQuestionAnswer = new ClientQuestionAnswer();
//            mClientQuestion.QuestionID = this.ID;
//            mClientQuestion.Answers.add(mClientQuestionAnswer);
//        }
//
//        this.layout = Question_layout;
//        this.context = context;
//
//        this.SelectedInfo = new TextView(context);
//        this.SelectedInfo.setText(s_text);
//        this.SelectedInfo.setTextSize(20);
//
//        int AnswersCount = this.Answers.size();
//
//        if(this.Data != null)
//        {
//            TextView tv = new TextView(context);
//            tv.setText(this.Data.Text);
//            layout.addView(tv);
//        }
//
//        if(AnswersCount <=4)
//        {
//            int i =0;
//            for (AnswerElement ans : this.Answers.values())
//            {
//                Button btn = new Button(context);
//
//
//                if(ans.answer.Data.Text != null)
//                {
//                    btn.setText(i+1 + ": " +ans.answer.Data.Text);
//                }
//                else
//                {
//                    btn.setText(i+1);
//                }
//
//                btn.setOnClickListener(this);
//                layout.addView(btn);
//                i++;
//            }
//        }
//        else
//        {
//            String[] items = new String[AnswersCount];
//            for(int i =0; i<AnswersCount; i++)
//            {
//                AnswerElement el = (AnswerElement) this.Answers.values().toArray()[i];
//                if(el.answer.Data.Text != null)
//                {
//                    items[i] = el.answer.Data.Text;
//                }
//                else
//                {
//                    items[i] = String.valueOf(i + 1);
//                }
//            }
//
//            Common.CommonMethods.DialogUtils.ShowChooseDialog("Choose your answer", items, context, this);
//        }
//        this.layout.addView(this.SelectedInfo);
//
//        if(Common.GlobalValues.ClientCommon.Data.Report.Questions.containsKey(new StringElement(this.ID)) == true)
//        {
//            ClientQuestionElement el = Common.GlobalValues.ClientCommon.Data.Report.Questions.get(new StringElement(this.ID));
//            try
//            {
//
//               this.SelectedInfo.setText("Ваш выбор: "+
//                       this.Answers.get(new StringElement(el.Question.Answers.get(0).Data.Text)).answer.Data.Text);
//            }
//            catch (Exception ex)
//            {
//                int yy=0;
//            }
//            IsSaved = true;
//        }
//
//    }
//
//    @Override
//    public void OnSelect(final int num) {
//
//    if(IsSaved != true)
//    {
//        AnswerElement el = (AnswerElement) this.Answers.values().toArray()[num];
//        if( el.answer.Data.Text != null)
//        {
//
//            this.SelectedInfo.setText(s_text +  el.answer.Data.Text);
//        }
//        else
//        {
//            this.SelectedInfo.setText(s_text +  num);
//        }
//
//        if(Common.GlobalValues.ClientCommon.Data.Report.Questions.containsKey(mClientQuestion.QuestionID) == true)
//        {
//            Common.GlobalValues.ClientCommon.Data.Report.Questions.remove(mClientQuestion.QuestionID);
//        }
//           mClientQuestionAnswer.Data.Text = el.answer.ID;
//           Common.GlobalValues.ClientCommon.Data.Report.Questions.put(
//                    new StringElement(mClientQuestion.QuestionID),
//                    new ClientQuestionElement(mClientQuestion));
//
//            int y =0;
//    }
//    else
//    {
//            Common.CommonMethods.DialogUtils.ShowYNDialog("","Вы уверены, что хотите поменять свой ответ",context,
//                    new DialogInterface.OnClickListener() {
//                        @Override
//                        public void onClick(DialogInterface dialogInterface, int i) {
//                            dialogInterface.dismiss();
//                            IsSaved = false;
//                            OnSelect(num);
//                        }
//                    },
//            new DialogInterface.OnClickListener() {
//                @Override
//                public void onClick(DialogInterface dialogInterface, int i) {
//                    dialogInterface.dismiss();
//                }
//            });
//        }
//    }
//
//    @Override
//    public void onClick(DialogInterface dialogInterface, int i) {
//        OnSelect(i);
//    }
//
//    @Override
//    public void onClick(View view) {
//        Button button = (Button)view;
//        selected = Integer.parseInt(String.valueOf(button.getText().toString().toCharArray()[0])) - 1;
//        button.setSelected(true);
//        OnSelect(selected);
//    }
}
