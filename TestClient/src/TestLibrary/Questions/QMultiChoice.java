package TestLibrary.Questions;

import TestLibrary.Abstract.QuestionBase;

/**
 * Created by Pit on 11.06.13.
 */
//implements Common.CommonInterfaces.OnSelectHandler, AdapterView.OnItemClickListener
public class QMultiChoice extends QuestionBase  {
    public QMultiChoice()
    {
        this.Type = QuestionBase.QuestionType.MultiChoice;
    }

//    LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.MATCH_PARENT, 3.0f);
//    Context context;
//    LinearLayout layout;
//    TextView SelectedInfo;
//    TextView ChoiceView;
//    ListView AnswersList;
//
//    ClientQuestion clientQuestion;
//    List<ClientQuestionAnswer> clientAnswers = new ArrayList<ClientQuestionAnswer>();
//    List<Integer> Selected = new ArrayList<Integer>();
//
//    public void Draw(LinearLayout Question_layout, final Context context)
//    {
//
//        clientQuestion = new ClientQuestion();
//
//        clientQuestion.QuestionID = this.ID;
//
//        Common.GlobalValues.ClientCommon.Data.Report.Questions.put(
//                new StringElement(clientQuestion.QuestionID),
//                new ClientQuestionElement(clientQuestion));
//
//        final int AnswersCount = this.Answers.size();
//        String[] items = new String[AnswersCount];
//        for(int i =0; i<AnswersCount; i++)
//        {
//            AnswerElement el = (AnswerElement) this.Answers.values().toArray()[i];
//            items[i] = el.answer.Data.Text;
//        }
//
//
//        this.layout = Question_layout;
//        this.context = context;
//        ScrollView scrollView = new ScrollView(context);
//        try{
//        layout.addView(scrollView);
//        }
//        catch (Exception ex)
//        {
//            int yy=0;
//        }
//
//
//        this.SelectedInfo = new TextView(context);
//        this.SelectedInfo.setLayoutParams(lp);
//        this.SelectedInfo.setText(this.Data.Text);
//        this.SelectedInfo.setVerticalScrollBarEnabled(true);
//
//        scrollView.addView(this.SelectedInfo);
//
//        AnswersList = new ListView(context);
//        AnswersList.setLayoutParams(lp);
//        AnswersList.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
//        AnswersList.setAdapter(new ArrayAdapter<String>(context , android.R.layout.simple_list_item_multiple_choice, items));
//        AnswersList.setOnItemClickListener(this);
//
//        layout.addView(AnswersList);
//
//        ChoiceView = new TextView(context);
//        layout.addView(ChoiceView);
//
//    }
//
//    @Override
//    public void OnSelect(int num)
//    {
//        String sel = "";
//        int cntChoice = AnswersList.getCount();
//        SparseBooleanArray sparseBooleanArray = AnswersList.getCheckedItemPositions();
//
//        Selected.clear();
//        this.clientAnswers.clear();
//        this.clientQuestion.Answers.clear();
//
//        for(int i = 0; i < cntChoice; i++){
//
//            if(sparseBooleanArray.get(i)) {
//
//                sel +="|"+ AnswersList.getItemAtPosition(i).toString() + "|";
//                Selected.add(i);
//
//                ClientQuestionAnswer answer = new ClientQuestionAnswer();
//                AnswerElement element = (AnswerElement) this.Answers.values().toArray()[i];
//                answer.Data.Text =  element.answer.ID;
//
//                this.clientAnswers.add(answer);
//                this.clientQuestion.Answers.add(answer);
//            }
//        }
//
//        ChoiceView.setText(sel);
//
//    }
//
//
//    @Override
//    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
//        ListView v = (ListView)adapterView;
//        if(v.isItemChecked(i) == false)
//        {
//            v.setItemChecked(i, false);
//        }
//        else
//        {
//            v.setItemChecked(i, true);
//
//        }
//
//        OnSelect(i);
//    }

}
