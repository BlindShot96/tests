package com.example.SimpleTestClient.Fragments;

import TestLibrary.MediaFile;
import TestLibrary.Questions.QTextAnswer;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.*;
import com.example.SimpleTestClient.Activities.ImageItemActivity;
import com.example.SimpleTestClient.Adapters.ImageAdapter;
import com.example.SimpleTestClient.Adapters.ImageAdapterItem;
import com.example.SimpleTestClient.R;
import com.example.SimpleTestClient.Utils.Common;
import com.example.SimpleTestClient.ViewModels.QuestionBaseViewModel;
import com.example.SimpleTestClient.ViewModels.TextChoiceViewModel;

import java.util.ArrayList;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 01.09.13
 * Time: 13:51
 * To change this template use File | Settings | File Templates.
 */
public class TextChoiceFragment extends Fragment implements QuestionBaseViewModel.OnPropertyChanged {

    final static String ARG_POSITION = "position";
    int mCurrentPosition = -1;

    EditText TextAnswerET;
    GridView ImagesView;
    TextView MainTextView;
    Gallery ImagesGallery;

    TextChoiceViewModel viewModel;
    public TextChoiceViewModel getViewModel() {
        return viewModel;
    }
    public void setViewModel(TextChoiceViewModel viewModel) {
        this.viewModel = viewModel;
        this.viewModel.setOnPropertyChangedListener(this);
    }
    public void setViewModel(Context context,QTextAnswer q) {
        this.viewModel = new TextChoiceViewModel(context,q);
    }
    public void setViewModel(Context context,int num) {
        this.viewModel = new TextChoiceViewModel(context,
                (QTextAnswer) Common.GlobalValues.ClientCommon.test.getQuestionByNumber(num));
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

    public TextChoiceFragment(TextChoiceViewModel vm)
    {
        setViewModel(vm);
    }
    public TextChoiceFragment()
    {}

    private void Initialize()
    {
        this.TextAnswerET = (EditText) getActivity().findViewById(R.id.fargment_single_choice_question_text_answer);
        this.MainTextView = (TextView) getActivity().findViewById(R.id.fargment_text_choice_question_maintextview);
        this.ImagesGallery = (Gallery)getActivity().findViewById(R.id.fargment_text_choice_question_imageslist);
        this.viewModel.setOnPropertyChangedListener(this);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        // If activity recreated (such as from screen rotate), restore
        // the previous article selection set by onSaveInstanceState().
        // This is primarily necessary when in the two-pane layout.
        if (savedInstanceState != null) {
            //mCurrentPosition = savedInstanceState.getInt(ARG_POSITION);
            //setViewModel((SingleChoiceViewModel)savedInstanceState.get(ARG_POSITION));
            setViewModel(new TextChoiceViewModel(getActivity().getApplicationContext(),
                    (QTextAnswer) Common.GlobalValues.ClientCommon.test.getQuestionByNumber(savedInstanceState.getInt(ARG_POSITION))));
        }

        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_text_choice_question, container, false);
    }

    @Override
    public void onStart() {
        super.onStart();

        Initialize();
        Update();

        // During startup, check if there are arguments passed to the fragment.
        // onStart is a good place to do this because the layout has already been
        // applied to the fragment at this point so we can safely call the method
        // below that sets the article text.
        Bundle args = getArguments();
        if (args != null) {
            // Set article based on argument passed in
            //Update(args.getInt(ARG_POSITION));
            Update(new TextChoiceViewModel(getActivity().getApplicationContext(),
                    (QTextAnswer) Common.GlobalValues.ClientCommon.test.getQuestionByNumber(args.getInt(ARG_POSITION))));
        } else if (getQuestionPosition() != -1) {
            // Set article based on saved instance state defined during onCreateView
//           Update(new SingleChoiceViewModel(getActivity().getApplicationContext(),
//                    Common.GlobalValues.ClientCommon.test.getQuestionByNumber(getQuestionPosition())));
        }
    }

    public void Update() {
        setAnswer();
        setMainText();
        setImagesView();
    }

    public void Update(TextChoiceViewModel vm) {
        this.setViewModel(vm);
        Update();
    }

    private void setAnswer()
    {
        if(viewModel.isAnswered() == true)
        {
            String str_ans = viewModel.getTextAnswer();
            if(str_ans == null){str_ans = "";}
            this.TextAnswerET.setText(str_ans);
        }

        this.TextAnswerET.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {}
            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {}
            @Override
            public void afterTextChanged(Editable editable) {
                viewModel.setTextAnswer(editable.toString());
            }
        });

//        final int AnswersCount = this.viewModel.getAnswers().size();
//        ArrayList<String> stsrs = new ArrayList<String>();
//        for (Answer ans : this.viewModel.getAnswers())
//        {
//            stsrs.add(ans.Data.Text);
//        }
//
//        this.AnswersList.setAdapter(new ArrayAdapter<String>(getActivity().getApplicationContext(), R.layout.view_simple_list_item_singlechoice_light, stsrs));
//        this.AnswersList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
//            @Override
//            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
//                viewModel.setSelectedAnswer(i);
//                AnswersList.setItemChecked(i, true);
//            }
//        });
//
//        if(this.viewModel.getSelectedAnswer()!=null)
//        {
//            this.AnswersList.setItemChecked(this.viewModel.getQuestionModel().Answers.indexOf(this.viewModel.getSelectedAnswer()),true);
//            OnPropertyChange(this.viewModel,"SelectedAnswer");
//        }
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

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);

        // Save the current article selection in case we need to recreate the fragment
        outState.putInt(ARG_POSITION, getQuestionPosition());
    }

    @Override
    public void OnPropertyChange(Object sender, String propertyName) {
    }
}
