package com.example.SimpleTestClient.Utils;

import TestLibrary.Abstract.QuestionBase;
import TestLibrary.Questions.QMultiChoice;
import TestLibrary.Questions.QSingleChoice;
import TestLibrary.Questions.QTextAnswer;
import android.content.Context;
import android.support.v4.app.Fragment;
import com.example.SimpleTestClient.Fragments.MultiChoiceFragment;
import com.example.SimpleTestClient.Fragments.SingleChoiceFragment;
import com.example.SimpleTestClient.Fragments.TextChoiceFragment;
import com.example.SimpleTestClient.ViewModels.MultiChoiceViewModel;
import com.example.SimpleTestClient.ViewModels.QuestionBaseViewModel;
import com.example.SimpleTestClient.ViewModels.SingleChoiceViewModel;
import com.example.SimpleTestClient.ViewModels.TextChoiceViewModel;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 22.08.13
 * Time: 18:45
 * To change this template use File | Settings | File Templates.
 */
public  class QuestionFactory {
    public static Fragment GetFragment(Context context,QuestionBase questionBase)
    {
        switch (questionBase.Type)
        {
            case SingleChoice:
            {
                return new SingleChoiceFragment(new SingleChoiceViewModel(context, (QSingleChoice) questionBase));
            }
            case MultiChoice:
            {
                return new MultiChoiceFragment(new MultiChoiceViewModel(context, (QMultiChoice) questionBase));
            }
            case TextQuestion:
            {
                return new TextChoiceFragment(new TextChoiceViewModel(context,(QTextAnswer)questionBase));
            }
            default:
            {
                return null;
            }
        }
    }

    public static Fragment GetFragment(QuestionBaseViewModel viewModel)
    {
        switch (viewModel.getQuestionModel().Type)
        {
            case SingleChoice:
            {
                return new SingleChoiceFragment((SingleChoiceViewModel) viewModel);
            }
            case MultiChoice:
            {
                return new MultiChoiceFragment((MultiChoiceViewModel) viewModel);
            }
            case TextQuestion:
            {
                return new TextChoiceFragment((TextChoiceViewModel) viewModel);
            }
            default:
            {
                return null;
            }
        }
    }

    public static QuestionBaseViewModel GetViewModel(Context context,QuestionBase questionBase)
    {
        switch (questionBase.Type)
        {
            case SingleChoice:
            {
                return new SingleChoiceViewModel(context, (QSingleChoice) questionBase);
            }
            case MultiChoice:
            {
                return new MultiChoiceViewModel(context, (QMultiChoice) questionBase);
            }
            case TextQuestion:
            {
                return new TextChoiceViewModel(context,(QTextAnswer)questionBase);
            }
            default:
            {
                return null;
            }
        }
    }
}
