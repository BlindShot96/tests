package com.example.SimpleTestClient.Adapters;

/**
 * Created by пётр on 10.01.14.
 */

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.app.FragmentStatePagerAdapter;

import com.example.SimpleTestClient.Utils.QuestionFactory;
import com.example.SimpleTestClient.ViewModels.QuestionBaseViewModel;

import java.util.ArrayList;
import java.util.Locale;

/**r} that returns a fragment corresponding to
 * one of the sections/tabs/pages.
 */
public class SectionsPagerAdapter extends FragmentStatePagerAdapter {

    ArrayList<QuestionBaseViewModel> mViewModels;
    public SectionsPagerAdapter(FragmentManager fm, ArrayList<QuestionBaseViewModel> viewModels) {
        super(fm);
        this.mViewModels = viewModels;
    }

    @Override
    public Fragment getItem(int position) {
        Fragment frag = QuestionFactory.GetFragment(mViewModels.get(position));
        return frag;
    }

    @Override
    public int getCount()
    {
        if(mViewModels != null)
        {
           return mViewModels.size();
        }
        else
        {
            return 0;
        }
    }

    @Override
    public CharSequence getPageTitle(int position) {
        if(mViewModels != null)
        {
            return mViewModels.get(position).Name();
        }
        else
        {
            return "";
        }
    }



}