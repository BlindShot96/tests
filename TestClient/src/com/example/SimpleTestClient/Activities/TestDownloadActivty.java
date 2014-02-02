package com.example.SimpleTestClient.Activities;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.SimpleTestClient.R;
import com.example.SimpleTestClient.Utils.Common;
import com.example.SimpleTestClient.Utils.IpAddressValidator;
import com.example.SimpleTestClient.Utils.ClientGetTestPostFactory;
import com.example.SimpleTestClient.Utils.DownloadTestTask;
import com.example.SimpleTestClient.Utils.ParseTestTask;
import com.example.SimpleTestClient.Utils.SpinnerAsyncTask;

import java.net.URISyntaxException;

import TestLibrary.Client.ClientReport;
import TestLibrary.Test;

/**
 * Created by пётр on 08.01.14.
 */
public class TestDownloadActivty extends Activity {

    Button Start_btn;
    Button DownloadBtn;

    String TestXml;
    String IpAddr;
    String message;

    EditText ServerAddressBox;

    private void Initialize()
    {
        Start_btn = (Button)findViewById(R.id.activity_test_download_start_button);
        Start_btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startTest();
            }
        });
        DownloadBtn = (Button)findViewById(R.id.activity_test_download_dowbload_button);
        DownloadBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
               downloadTest();
            }
        });

        ServerAddressBox = (EditText)findViewById(R.id.activity_test_download_server_address_box);
        ServerAddressBox.setText(Common.GlobalValues.Internet.SERVER_IP);

        ServerAddressBox.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
                ServerAddressBox.setTextColor(getResources().getColor(android.R.color.holo_red_dark));
            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i2, int i3)
            {
                IpAddressValidator validator = new IpAddressValidator();
                if(validator.Validate(charSequence.toString()) == false)
                {
                    ServerAddressBox.setTextColor(getResources().getColor(android.R.color.holo_red_dark));
                }
                else
                {
                    ServerAddressBox.setTextColor(getResources().getColor(android.R.color.holo_green_dark));
                }


            }

            @Override
            public void afterTextChanged(Editable editable) {
                IpAddressValidator validator = new IpAddressValidator();
               if(validator.Validate(editable.toString()) == false)
               {
                   ServerAddressBox.setTextColor(getResources().getColor(android.R.color.holo_red_dark));
               }
               else
               {
                   ServerAddressBox.setTextColor(getResources().getColor(android.R.color.holo_green_dark));
               }

            }
        });

        if(Common.GlobalValues.ClientCommon.test == null)
        {
            this.message = ClientGetTestPostFactory.createPost(Common.GlobalValues.ClientCommon.Data.Name, Common.GlobalValues.ClientCommon.Data.LastName, Common.GlobalValues.ClientCommon.Data.Group);
        }
        else
        {
            ShowTestInfo(Common.GlobalValues.ClientCommon.test);
        }
    }

    @Override
    public void onBackPressed() {

                        Common.GlobalValues.ClientCommon.test = null;
                        Common.GlobalValues.ClientCommon.Data.Report = new ClientReport();
                        startActivity(new Intent(TestDownloadActivty.this,RegisterActivity.class));

    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_download_test);
        Initialize();
    }



    private void startTest()
    {
        if(Common.GlobalValues.ClientCommon.test == null)
        {
            Common.CommonMethods.DialogUtils.ShowTextDialog("Тест ещё не загружен/открыт.",this);
            return;
        }
        Common.CommonMethods.DialogUtils.ShowYNDialog("","Начать тестирование?",TestDownloadActivty.this,
                new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        dialogInterface.dismiss();
                        Intent intent = new Intent(TestDownloadActivty.this, TestMainActivity.class);
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

    private void downloadTest()
    {

        String ip = this.ServerAddressBox.getText().toString();
        Common.GlobalValues.Internet.SERVER_IP = ip;
        IpAddressValidator validator= new IpAddressValidator();
        if(validator.Validate(ip) == false)
        {
            Common.CommonMethods.DialogUtils.ShowErrorMessage("Неправильный адрес сервера",this);
            return;
        }
        else{
          this.IpAddr = ip;
          DownloadTestTask task = new DownloadTestTask(this,this.IpAddr,5678,this.message);
          task.setOnResultExecuteListener(new SpinnerAsyncTask.OnResultExecute() {
            @Override
            public void OnResultExecuted(Object result) {
                ParseTest((String)result);
            }
          });
            task.execute();
        }
    }

    private void openTest()
    {
        try{
        Intent intent = new Intent();
        intent.addCategory(Intent.CATEGORY_OPENABLE);
        intent.setType("file/*");
        intent.setAction(Intent.ACTION_GET_CONTENT);
        startActivityForResult(intent, Common.GlobalValues.Intents.FILE_SELECT_CODE);
        }
        catch (Exception ex)
        {
            Common.CommonMethods.DialogUtils.ShowErrorMessage("Не удалось открыть файл теста", this);
        }

    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        switch (requestCode) {
            case Common.GlobalValues.Intents.FILE_SELECT_CODE:
                if (resultCode == RESULT_OK) {
                    // Get the Uri of the selected file
                    Uri uri = data.getData();
                    Log.d("FILE", "File Uri: " + uri.toString());
                    // Get the path
                    String path = null;
                    try {
                        path = Common.CommonMethods.FileUtils.getPath(this, uri);
                    } catch (URISyntaxException e) {
                        Common.CommonMethods.DialogUtils.ShowErrorMessage("Невозмоно открыть файл", TestDownloadActivty.this);
                    }
                    Log.d("FILE", "File Path: " + path);

                    try{
                        String result = Common.CommonMethods.FileUtils.readStringFromFile(TestDownloadActivty.this,uri.getPath());
                        this.TestXml = result;
                        ParseTest(this.TestXml);
                    }catch (Exception ex)
                    {
                        Common.CommonMethods.DialogUtils.ShowErrorMessage("Невозмоно открыть файл" + '\n'+ex.getMessage(),TestDownloadActivty.this);
                        return;
                    }


                }
                break;
        }
        super.onActivityResult(requestCode, resultCode, data);
    }

    private void ParseTest(String xml)
    {
//        ParseTask task = new ParseTask();
//        task.execute(xml);\
       ParseTestTask task = new ParseTestTask(this,xml);
       task.setOnResultExecuteListener(new SpinnerAsyncTask.OnResultExecute() {
           @Override
           public void OnResultExecuted(Object result) {
               ShowTestInfo((Test)result);
           }
       });
        task.execute();
    }

    private  void ShowTestInfo(Test test)
    {
        TextView name  = (TextView)findViewById(R.id.activity_test_download_test_name_box);
        TextView teacher_name  = (TextView)findViewById(R.id.activity_test_download_teacher_name_box);
        TextView description  = (TextView)findViewById(R.id.activity_test_download_test_description_box);

        if(test.Settings.Name == null)
        {
            test.Settings.Name = "";
        }
        if(test.Settings.TeacherName == null)
        {
            test.Settings.TeacherName = "";
        }
        if(test.Data.Text == null)
        {
            test.Data.Text = "";
        }

        name.setText(test.Settings.Name);
        teacher_name.setText(test.Settings.TeacherName);
        description.setText(test.Data.Text);
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu items for use in the action bar
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_activity_test_download, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
       if(item.getItemId() == R.id.menu_activity_test_download_action_open)
       {
           openTest();
       }
       if(item.getItemId()==R.id.menu_activity_test_result_action_about)
       {
            AlertDialog ad = new AlertDialog.Builder(this).create();
            ad.setIcon(R.drawable.ic_action_about);
            ad.setTitle("Информация");
            //  ad.setCancelable(false); // This blocks the 'BACK' button
            ad.setMessage(getResources().getString(R.string.info_download_activity));
            ad.setButton("OK", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                }
            });
            ad.show();
       }

       return false;

    }


}
