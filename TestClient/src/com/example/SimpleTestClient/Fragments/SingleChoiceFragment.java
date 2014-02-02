package com.example.SimpleTestClient.Fragments;

import TestLibrary.Answer;
import TestLibrary.MediaFile;
import TestLibrary.Questions.QSingleChoice;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.*;
import com.example.SimpleTestClient.Activities.ImageItemActivity;
import com.example.SimpleTestClient.Adapters.ImageAdapter;
import com.example.SimpleTestClient.Adapters.ImageAdapterItem;
import com.example.SimpleTestClient.R;
import com.example.SimpleTestClient.Utils.Common;
import com.example.SimpleTestClient.Utils.ListViewHelper;
import com.example.SimpleTestClient.ViewModels.QuestionBaseViewModel;
import com.example.SimpleTestClient.ViewModels.SingleChoiceViewModel;

import java.util.ArrayList;

/**
 * Created with IntelliJ IDEA.
 * User: Pit
 * Date: 17.08.13
 * Time: 13:36
 * To change this template use File | Settings | File Templates.
 */
public class SingleChoiceFragment  extends Fragment implements QuestionBaseViewModel.OnPropertyChanged {

    final static String ARG_POSITION = "position";
    final static String ARG_VIEWMODEL = "VIEWMODEL";
    int mCurrentPosition = -1;
    /**
     * статический объект для сохранения текущего состояния, например view model
     */
    private static Object _savedInstanceObject;

    ListView AnswersList;
    GridView ImagesView;
    TextView MainTextView;
    //TextView ResultView;
    Gallery ImagesGallery;

    SingleChoiceViewModel viewModel;
    public SingleChoiceViewModel getViewModel() {
        return viewModel;
    }
    public void setViewModel(SingleChoiceViewModel viewModel) {
        this.viewModel = viewModel;
        this.viewModel.setOnPropertyChangedListener(this);
    }
    public void setViewModel(Context context,QSingleChoice q) {
        this.viewModel = new SingleChoiceViewModel(context,q);
    }
    public void setViewModel(Context context,int num) {
        this.viewModel = new SingleChoiceViewModel(context,
                (QSingleChoice) Common.GlobalValues.ClientCommon.test.getQuestionByNumber(num));
    }
    private int getQuestionPosition()
    {
        try{
        return viewModel.getQuestionModel().Number;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }

    public SingleChoiceFragment(SingleChoiceViewModel vm)
    {
       setViewModel(vm);
    }

    public SingleChoiceFragment()
    {}

    private void Initialize()
    {
        this.AnswersList = (ListView) getActivity().findViewById(R.id.fargment_single_choice_question_answerslist);
        this.MainTextView = (TextView) getActivity().findViewById(R.id.fargment_single_choice_question_maintextview);
       // this.ResultView = (TextView) getActivity().findViewById(R.id.fargment_single_choice_question_resultview);
        this.ImagesGallery = (Gallery)getActivity().findViewById(R.id.fargment_single_choice_question_imageslist);
        this.viewModel.setOnPropertyChangedListener(this);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        super.onCreateView(inflater,container,savedInstanceState);
        // If activity recreated (such as from screen rotate), restore
        // the previous article selection set by onSaveInstanceState().
        // This is primarily necessary when in the two-pane layout.
        if (savedInstanceState != null) {
            //mCurrentPosition = savedInstanceState.getInt(ARG_POSITION);
           //setViewModel((SingleChoiceViewModel)savedInstanceState.get(ARG_POSITION));
//            if(savedInstanceState.containsKey(ARG_VIEWMODEL)){
//                if(savedInstanceState.getBoolean(ARG_VIEWMODEL) == true)
//                {
//                    setViewModel((SingleChoiceViewModel) _savedInstanceObject);
//                }
//            }
        }

        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fargment_single_choice_question, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();
//        Bundle savedInstanceState = getArguments();
//        if (savedInstanceState != null) {
//            //mCurrentPosition = savedInstanceState.getInt(ARG_POSITION);
//            //setViewModel((SingleChoiceViewModel)savedInstanceState.get(ARG_POSITION));
//            if(savedInstanceState.containsKey(ARG_VIEWMODEL)){
//                if(savedInstanceState.getBoolean(ARG_VIEWMODEL) == true)
//                {
//                    setViewModel((SingleChoiceViewModel) _savedInstanceObject);
//                }
//            }
//        }

        Initialize();
        Update();

        // During startup, check if there are arguments passed to the fragment.
        // onStart is a good place to do this because the layout has already been
        // applied to the fragment at this point so we can safely call the method
        // below that sets the article text.
//        Bundle args = getArguments();
//        if (args != null) {
//            // Set article based on argument passed in
//            //Update(args.getInt(ARG_POSITION));
//            Update(new SingleChoiceViewModel(getActivity().getApplicationContext(),
//                    (QSingleChoice) Common.GlobalValues.ClientCommon.test.getQuestionByNumber(args.getInt(ARG_POSITION))));
//        } else if (getQuestionPosition() != -1) {
//            // Set article based on saved instance state defined during onCreateView
////           Update(new SingleChoiceViewModel(getActivity().getApplicationContext(),
////                    Common.GlobalValues.ClientCommon.test.getQuestionByNumber(getQuestionPosition())));
//        }
    }

    public void Update() {
          setAnswers();
          setMainText();
          setImagesView();
    }

    public void Update(SingleChoiceViewModel vm) {
        this.setViewModel(vm);
        Update();
    }

    private void setAnswers()
    {
        final int AnswersCount = this.viewModel.getAnswers().size();
        ArrayList<String> stsrs = new ArrayList<String>();
        for (Answer ans : this.viewModel.getAnswers())
        {
            stsrs.add(ans.Data.Text);
        }



//        ViewGroup.LayoutParams lp = (ViewGroup.LayoutParams) this.AnswersList.getLayoutParams();
//        int height = (stsrs.size()) * 80;
//        lp.height = height;
//        this.AnswersList.setLayoutParams(lp);

        this.AnswersList.setAdapter(new ArrayAdapter<String>(getActivity().getApplicationContext(), R.layout.view_simple_list_item_light, stsrs));
        this.AnswersList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
               viewModel.setSelectedAnswer(i);
               AnswersList.setItemChecked(i, true);
            }
        });

        ListViewHelper.setListViewHeightBasedOnChildren(this.AnswersList);

        if(this.viewModel.getSelectedAnswer()!=null)
        {
            this.AnswersList.setItemChecked(this.viewModel.getQuestionModel().Answers.indexOf(this.viewModel.getSelectedAnswer()),true);
            OnPropertyChange(this.viewModel,"SelectedAnswer");
        }



    }

    private void setMainText()
    {
        this.MainTextView.setText(this.viewModel.getQuestionModel().Data.Text);
    }

    private void setImagesView()
    {
        if(this.viewModel.getQuestionData().GetImages() != null)
        {
            final ImageAdapter adapter = new ImageAdapter(this.getActivity().getApplicationContext(),
                    ImageAdapter.ConvertImagesFiles((ArrayList<MediaFile>)this.viewModel.getQuestionData().GetImages(),
                    getActivity().getResources()));
            this.ImagesGallery.setAdapter(adapter);
            this.ImagesGallery.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {

                @Override
                public boolean onItemLongClick(AdapterView<?> adapterView, View view, int i, long l) {
                    try{
                    Intent intent = new Intent(getActivity().getApplicationContext(), ImageItemActivity.class);
                    ImageItemActivity.ShowingImage = (ImageAdapterItem)adapter.getItem(i);
                    startActivity(intent);
                    }
                    catch (Exception ex)
                    {
                        int t =0;
                    }
                    return true;
                }
            });

        }
    }

    private void setResultText()
    {
      //  this.ResultView.setText(this.viewModel.getSelectedAnswer().Data.Text);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);

//        if(this.viewModel != null)
//        {
//            _savedInstanceObject = this.viewModel;
//            outState.putBoolean(ARG_VIEWMODEL,true);
//        }
        // Save the current article selection in case we need to recreate the fragment

       // outState.putInt(ARG_POSITION, getQuestionPosition());
    }

    @Override
    public void OnPropertyChange(Object sender, String propertyName) {
        if(propertyName == "SelectedAnswer")
        {
            setResultText();
        }
    }
}
