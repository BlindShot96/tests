/*
 * Copyright (C) 2012 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package com.example.SimpleTestClient.Activities;

import TestLibrary.Abstract.QuestionBase;
import TestLibrary.Client.ClientData;
import TestLibrary.Client.ClientQuestion;
import TestLibrary.Client.ClientReport;
import TestLibrary.Test;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.os.Bundle;
import android.support.v4.app.ActionBarDrawerToggle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.GravityCompat;
import android.support.v4.view.ViewPager;
import android.support.v4.widget.DrawerLayout;
import android.support.v4.widget.ViewDragHelper;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ListAdapter;
import android.widget.ListView;

import com.example.SimpleTestClient.R;
import com.example.SimpleTestClient.Utils.Common;
import com.example.SimpleTestClient.Utils.QuestionFactory;
import com.example.SimpleTestClient.ViewModels.QuestionBaseViewModel;
import com.example.SimpleTestClient.Adapters.QuestionViewModelAdapter;
import com.example.SimpleTestClient.Utils.ShowMode;

import java.lang.reflect.Field;
import java.util.ArrayList;

public class TestMainActivity extends FragmentActivity {

    private static Object _savedInstanceObject;

    ListView mQuestionsList;
    FrameLayout questionLayout;

    public final static String ARG_POSITION = "position";
    private final  static String ARG_QVModelsList = "object";
    private  QuestionBaseViewModel showingQuestion;

    private int getShowingNumber()
    {
        if(this.questionBaseViewModels != null && this.showingQuestion!=null)
        {
            return this.questionBaseViewModels.indexOf(this.showingQuestion);
        }
        else
        {
            return -1;
        }
    }

    private Exception Debugexxx=null;

    Button  endButton;
    Button nextButton;
    Button prevButton;

    private DrawerLayout mDrawerLayout;
    private ActionBarDrawerToggle mDrawerToggle;

    Test test;
    public Test getTest() {
        return test;
    }
    public void setTest(Test test) {
        this.test = test;
    }

    private ArrayList<QuestionBaseViewModel> questionBaseViewModels;
    private ArrayList<QuestionBaseViewModel> getQuestionsViewModels()
    {
        return questionBaseViewModels;
    }
    private void setQuestionViewModels(Test t)
    {
        if(t==null || t.Questions == null){this.questionBaseViewModels = new ArrayList<QuestionBaseViewModel>(); return;}
        ArrayList<QuestionBaseViewModel> res = new ArrayList<QuestionBaseViewModel>();
        for (QuestionBase q : t.Questions)
        {
            res.add(QuestionFactory.GetViewModel(getApplicationContext(),q));
        }
        this.questionBaseViewModels = res;

        if(Common.GlobalValues.ClientCommon.Data!=null && Common.GlobalValues.ClientCommon.Data.Report!=null && Common.GlobalValues.ClientCommon.Data.Report.Questions!= null && Common.GlobalValues.ClientCommon.Data.Report.Questions.size() != 0)
        {
            for (ClientQuestion cq : Common.GlobalValues.ClientCommon.Data.Report.Questions)
            {
               if(cq.Answers!= null && cq.Answers.size()!=0)
               {
                  for (QuestionBaseViewModel vm : questionBaseViewModels)
                  {
                      if(vm.getQuestionModel().ID.equals(cq.QuestionID))
                      {
                         vm.LoadAnswers(cq.Answers);
                      }
                  }
               }
            }
        }
    }

    public TestMainActivity(Test test)
    {
          this.setTest(test);
    }

    public TestMainActivity()
    {
       if(Common.GlobalValues.ClientCommon.test != null)
       {
          this.setTest(Common.GlobalValues.ClientCommon.test);
       }
    }

    private void Initialize()
    {
        if(questionBaseViewModels == null){
            setQuestionViewModels(this.test);
        }

        if(findViewById(R.id.activity_test_main_drawer_layout)!=null)
        {
            InitializePortait();
        }
        else
        {
           // this.getActionBar().hide();
        }

        this.endButton = (Button)findViewById(R.id.activity_test_main_button_end);
        this.endButton.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    OnEndTest();
                }
            });


        this.mQuestionsList = (ListView) findViewById(R.id.activity_test_main_questions_list);
        final QuestionViewModelAdapter adapter = new QuestionViewModelAdapter(this,this.questionBaseViewModels,this.getShowMode());
        this.mQuestionsList.setAdapter((ListAdapter)adapter);
        this.mQuestionsList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                onQuestionSelected(adapter.getViewModel(i));
                if(mDrawerLayout != null)
                {
                   mDrawerLayout.closeDrawers();
                }
            }
        });

       this.questionLayout = (FrameLayout)findViewById(R.id.activity_test_main_question_detail);
        this.nextButton = (Button)findViewById(R.id.test_main_activity_next_button);
        this.nextButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                 showNextQuestion();
            }
        });
        this.prevButton = (Button)findViewById(R.id.test_main_activity_previous_button);
        this.prevButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ShowPreviousQuestion();
            }
        });
    }

    private void InitializePortait()
    {

        mDrawerLayout = (DrawerLayout) findViewById(R.id.activity_test_main_drawer_layout);
        mDrawerLayout.setDrawerShadow(R.drawable.drawer_shadow, GravityCompat.START);
        mDrawerToggle = new ActionBarDrawerToggle(
                this,
                mDrawerLayout,
                R.drawable.ic_drawer,
                R.string.app_name,
                R.string.app_name) {

            /** Called when a drawer has settled in a completely closed state. */
            public void onDrawerClosed(View view) {
                if(showingQuestion != null && getActionBar()!=null){
                  getActionBar().setTitle(showingQuestion.Name());
                }
                invalidateOptionsMenu(); // creates call to onPrepareOptionsMenu()
            }

            /** Called when a drawer has settled in a completely open state. */
            public void onDrawerOpened(View drawerView) {
                if(getActionBar()!=null){
                getActionBar().setTitle("Вопросы");}
                invalidateOptionsMenu(); // creates call to onPrepareOptionsMenu()
            }
        };

        // Set the drawer toggle as the DrawerListener
        mDrawerLayout.setDrawerListener(mDrawerToggle);
        // enable ActionBar app icon to behave as action to toggle nav drawer
        getActionBar().setDisplayHomeAsUpEnabled(true);
        getActionBar().setHomeButtonEnabled(true);

        Field mDragger = null;//mRightDragger or mLeftDragger based on Drawer Gravity
        try {
            mDragger = mDrawerLayout.getClass().getDeclaredField(
                    "mLeftDragger");

            mDragger.setAccessible(true);
            ViewDragHelper draggerObj = (ViewDragHelper) mDragger
                    .get(mDrawerLayout);

            Field mEdgeSize = draggerObj.getClass().getDeclaredField(
                    "mEdgeSize");
            mEdgeSize.setAccessible(true);
            int edge = mEdgeSize.getInt(draggerObj);

            mEdgeSize.setInt(draggerObj, edge * 3);
        } catch(Exception e) {
            e.printStackTrace();
        }
    }

    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.test_main_layout);

        if(savedInstanceState != null)
        {

            if(savedInstanceState.containsKey(ARG_QVModelsList)  == true && savedInstanceState.getBoolean(ARG_QVModelsList) == true)
            {
                 this.questionBaseViewModels= (ArrayList<QuestionBaseViewModel>) _savedInstanceObject;
            }
        }

        Initialize();

        if(savedInstanceState != null && savedInstanceState.containsKey(ARG_POSITION)==true){

            onQuestionSelected(this.questionBaseViewModels.get(savedInstanceState.getInt(TestMainActivity.ARG_POSITION)));
            if(this.mQuestionsList != null)
            {
                this.mQuestionsList.setItemChecked(savedInstanceState.getInt(TestMainActivity.ARG_POSITION),true);
            }
        }
        else
        {
            if(this.questionBaseViewModels!= null && this.questionBaseViewModels.size()>0){
              onQuestionSelected(this.questionBaseViewModels.get(0));
              if(this.mQuestionsList != null)
              {
                this.mQuestionsList.setItemChecked(0,true);
              }
            }
        }

    }

    private void showInformationBox()
    {
          if(this.showingQuestion != null)
          {
              String msg ="";
              String title = "";
              if(this.showingQuestion.getQuestionModel().Type == QuestionBase.QuestionType.SingleChoice)
              {
                 msg = getResources().getString(R.string.info_question_singleChoice);
                 title = "Вопрос с одиночным выбором";
              }
              if(this.showingQuestion.getQuestionModel().Type == QuestionBase.QuestionType.MultiChoice)
              {
                  msg = getResources().getString(R.string.info_question_multiChoice);
                  title = "Вопрос с множественном выбором";
              }
              if(this.showingQuestion.getQuestionModel().Type == QuestionBase.QuestionType.TextQuestion)
              {
                  msg = getResources().getString(R.string.info_question_textChoice);
                  title = "Вопрос с ответом в виде краткой строки";
              }

              AlertDialog ad = new AlertDialog.Builder(this).create();
              ad.setIcon(R.drawable.ic_action_about);
              ad.setTitle(title);
            //  ad.setCancelable(false); // This blocks the 'BACK' button
              ad.setMessage(msg);
              ad.setButton("OK", new DialogInterface.OnClickListener() {
                  @Override
                  public void onClick(DialogInterface dialog, int which) {
                      dialog.dismiss();
                  }
              });
              ad.show();

          }
    }

    private void showNextQuestion()
    {
        if(getShowingNumber() != -1 && getShowingNumber() < this.questionBaseViewModels.size()-1)
        {
            int next = getShowingNumber()+1;
            this.mQuestionsList.setItemChecked(next,true);
            this.onQuestionSelected(this.questionBaseViewModels.get(next));
        }
    }

    private void ShowPreviousQuestion()
    {
        if(getShowingNumber() != -1 && getShowingNumber() > 0)
        {
            int prev = getShowingNumber()-1;
            this.mQuestionsList.setItemChecked(prev,true);
            this.onQuestionSelected(this.questionBaseViewModels.get(prev));
        }
    }

    public void onQuestionSelected(QuestionBaseViewModel selectedQuestion) {
        this.showingQuestion = selectedQuestion;

        if(getShowingNumber() == 0)
        {
            this.nextButton.setActivated(true);
            this.prevButton.setActivated(false);
        }
        if(getShowingNumber() == this.questionBaseViewModels.size()-1)
        {
            this.nextButton.setActivated(false);
            this.prevButton.setActivated(true);
        }

        Fragment frag = QuestionFactory.GetFragment(selectedQuestion);
        if(frag != null)
        {
            replaceFragment(frag);
        }
        else
        {
           replaceFragment(new Fragment());
        }

        getActionBar().setTitle(selectedQuestion.Name());
        //this.mQuestionsList.invalidateViews();
        try{
        SaveAnswers();
        }
        catch (Exception ex)
        {
            ex.printStackTrace();
        }
    }

    private void replaceFragment(Fragment newFragment) {

        FragmentTransaction trasection = getSupportFragmentManager().beginTransaction();
        if(!newFragment.isAdded())
        {
            try{
                trasection.replace(R.id.activity_test_main_question_detail, newFragment);
                trasection.addToBackStack(null);
                trasection.commit();
            }
            catch (Exception ex)
            {
                int t =0;
            }
        }else
        {
            trasection.show(newFragment);
        }

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu items for use in the action bar
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_activity_test_main, menu);

        return super.onCreateOptionsMenu(menu);
    }

    @Override
    protected void onPostCreate(Bundle savedInstanceState) {
        super.onPostCreate(savedInstanceState);
        // Sync the toggle state after onRestoreInstanceState has occurred.
       // mDrawerToggle.syncState();
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);
        mDrawerToggle.onConfigurationChanged(newConfig);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
        if(mDrawerToggle != null && mDrawerLayout!=null){
          if (mDrawerToggle.onOptionsItemSelected(item)) {
              return true;
          }
        }

        int id = item.getItemId();
        if(id == R.id.menu_activity_test_result_action_about)
        {
            showInformationBox();
        }

        return false;

    }

    @Override
    public void onBackPressed() {
        Common.CommonMethods.DialogUtils.ShowYNDialog("Выход из теста","Вы уверены, что хотите прервать тестирование? \n " +
                "Все ваши результаты будут удалены!",this,new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {
               //YES
                Common.GlobalValues.ClientCommon.test = null;
                Common.GlobalValues.ClientCommon.Data.Report = new ClientReport();
                startActivity(new Intent(TestMainActivity.this,TestDownloadActivty.class));
            }
        },
            new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        //NO
                        return;
                    }
                });
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);

        // Save the current article selection in case we need to recreate the fragment
         try
         {
            outState.putInt(ARG_POSITION, this.showingQuestion.getQuestionModel().Number);
         }
         catch (Exception ex)
         {
           this.Debugexxx=ex;
         }
        if(questionBaseViewModels!= null && questionBaseViewModels.size() != 0)
        {
            _savedInstanceObject = questionBaseViewModels;
            outState.putBoolean(ARG_QVModelsList,true);
        }
    }

    public void OnEndTest() {

        ArrayList<String> not_q = new ArrayList<String>();
        boolean isAnswered = true;
        ArrayList<QuestionBaseViewModel> vm = this.questionBaseViewModels;
        for (QuestionBaseViewModel viewModel : this.questionBaseViewModels)
        {
           if(viewModel.isAnswered() == false)
           {
             not_q.add(viewModel.getQuestionModel().Name);
             isAnswered = false;
           }
        }

        SaveAnswers();

        if(isAnswered == false)
        {
            Common.CommonMethods.DialogUtils.ShowYNDialog("","Закончить тестирование? Вы ответили не на все вопросы",this,
                    new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {
                            dialogInterface.dismiss();
                            Intent intent = new Intent(TestMainActivity.this,NewTestResultActivity.class);
                            try{
                                startActivity(intent);
                            }
                            catch (Exception ex)
                            {
                                int gi =0;
                            }
                        }
                    },
                    new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {
                            dialogInterface.dismiss();
                        }
                    });
        }
        else {

            Common.CommonMethods.DialogUtils.ShowYNDialog("","Закончить тестирование?",this,
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        dialogInterface.dismiss();
                        Intent intent = new Intent(TestMainActivity.this,NewTestResultActivity.class);
                        try{
                            startActivity(intent);
                        }
                        catch (Exception ex)
                        {
                            int gi =0;
                        }
                    }
                },
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        dialogInterface.dismiss();
                    }
                });
        }
    }

    private void SaveAnswers()
    {
        ClientData clientData = new ClientData();
        {
            clientData.Name = Common.GlobalValues.ClientCommon.Data.Name;
            clientData.LastName = Common.GlobalValues.ClientCommon.Data.LastName;
            clientData.Group = Common.GlobalValues.ClientCommon.Data.Group;
        }

        ClientReport report = new ClientReport();
        report.Questions = new ArrayList<ClientQuestion>();

        for(QuestionBaseViewModel viewModel : this.questionBaseViewModels)
        {
            if(viewModel.isAnswered() == true)
            {
               report.Questions.add(viewModel.MakeClientQuestion());
            }
        }

        clientData.Report = report;

        Common.GlobalValues.ClientCommon.Data = clientData;
    }

    private ShowMode getShowMode()
    {
        if(this.getResources().getBoolean(R.bool.has_two_panes) == true)
        {
            return ShowMode.Landscape;
        }
        if(this.getResources().getBoolean(R.bool.has_two_panes) == false)
        {
            return ShowMode.Portrait;
        }

      if(this.getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE)
      {
          if(findViewById(R.id.activity_test_main_drawer_layout)!=null)
          {
              return ShowMode.Landscape_mini;
          }
          else
          {
              return ShowMode.Landscape;
          }
      }
      if(this.getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT)
      {
            return ShowMode.Portrait;
      }
      return ShowMode.Landscape;
    }

}