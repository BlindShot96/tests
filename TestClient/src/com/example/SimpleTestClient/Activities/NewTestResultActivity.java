package com.example.SimpleTestClient.Activities;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.SimpleTestClient.Internet.TcpClient;
import com.example.SimpleTestClient.R;
import com.example.SimpleTestClient.Utils.Common;
import com.example.SimpleTestClient.Utils.IpAddressValidator;
import com.example.SimpleTestClient.Utils.TestAndClientResult;

import org.simpleframework.xml.Serializer;
import org.simpleframework.xml.core.Persister;

import TestLibrary.Client.ClientData;
import TestLibrary.Client.ClientReport;
import TestLibrary.Client.ClientResult;

/**
 * Created by пётр on 09.01.14.
 */
public class NewTestResultActivity extends Activity
{
    String action;

    public void setXmlReport(String xmlReport) {
        XmlReport = xmlReport;
    }

    String XmlReport;

    TextView ResultsView;
    Button SendBtn;
    Button SaveBtn;
    Button SettingsBtn;

    private void Initialize()
    {
        ResultsView = (TextView)findViewById(R.id.activity_new_test_result_mark_box);
        SendBtn = (Button)findViewById(R.id.activity_new_test_result_send);
        SendBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SendToServer();
            }
        });


        SettingsBtn = (Button)findViewById(R.id.activity_new_test_result_settings_button);
        SettingsBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SettingsDialog();
            }
        });

        SetInfo();

        action = Common.GlobalValues.Intents.Actions.SendToServerAction;
        ParseTask task = new ParseTask();
        task.execute(Common.GlobalValues.ClientCommon.Data);
    }

    private void SetInfo()
    {
        ((TextView)findViewById(R.id.activity_new_test_result_address_box)).setText(Common.GlobalValues.Internet.SERVER_IP);
        ((TextView)findViewById(R.id.activity_new_test_result_name_box)).setText(Common.GlobalValues.ClientCommon.Data.Name);
        ((TextView)findViewById(R.id.activity_new_test_result_surname_box)).setText(Common.GlobalValues.ClientCommon.Data.LastName);
        ((TextView)findViewById(R.id.activity_new_test_result_group_box)).setText(Common.GlobalValues.ClientCommon.Data.Group);

        if(Common.GlobalValues.ClientCommon.Result != null)
        {
            ((TextView)findViewById(R.id.activity_new_test_result_mark_box)).setText("Оценка: " + Integer.toString(Common.GlobalValues.ClientCommon.Result.Mark));
            ((TextView)findViewById(R.id.activity_new_test_result_ball_box)).setText("Первичный балл: " + Integer.toString(Common.GlobalValues.ClientCommon.Result.ClientBalls));
            ((TextView)findViewById(R.id.activity_new_test_result_procent_box)).setText("Процент правильных ответов: " + Float.toString(Common.GlobalValues.ClientCommon.Result.Percent));
        }


    }

    private  void SettingsDialog()
    {
        LayoutInflater li = LayoutInflater.from(this);
        View promptsView = li.inflate(R.layout.dialog_load_temp_test_and_results, null);

        AlertDialog.Builder builder = new AlertDialog.Builder(
                this);
        builder.setView(promptsView);


        final EditText IPAddrBoxIn = (EditText)promptsView.findViewById(R.id.dialog_open_temp_test_and_results_Ip_adress);


        IPAddrBoxIn.setText(Common.GlobalValues.Internet.SERVER_IP);
        final EditText NameBox = (EditText)promptsView.findViewById(R.id.dialog_open_temp_test_and_results_NameBox);
        try{
            NameBox.setText(Common.GlobalValues.ClientCommon.Data.Name);
        }
        catch (Exception ex)
        {
            Log.d(ex.getMessage(), ex.getMessage());
        }
        final EditText LastNameBox = (EditText)promptsView.findViewById(R.id.dialog_open_temp_test_and_results_LastName);
        LastNameBox.setText(Common.GlobalValues.ClientCommon.Data.LastName);
        final EditText GroupBox = (EditText)promptsView.findViewById(R.id.dialog_open_temp_test_and_results_Group);
        GroupBox.setText(Common.GlobalValues.ClientCommon.Data.Group);

        // Inflate and set the layout for the dialog
        // Pass null as the parent view because its going in the dialog layout
        builder.setPositiveButton("Сохранить", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int id) {
                boolean can_show = false;

                if (new IpAddressValidator().Validate(IPAddrBoxIn.getText().toString()) == false) {
                    can_show = true;
                }
                if ((NameBox.getText().toString().hashCode() == 0)) {
                    can_show = true;
                }
                if (LastNameBox.getText().toString().hashCode() == 0) {
                    can_show = true;
                }

                if (GroupBox.getText().toString().hashCode() == 0) {
                    can_show = true;
                }

                if (can_show == false) {
                    Common.GlobalValues.Internet.SERVER_IP = IPAddrBoxIn.getText().toString();
                    Common.GlobalValues.ClientCommon.Data.Name = NameBox.getText().toString();
                    Common.GlobalValues.ClientCommon.Data.LastName = LastNameBox.getText().toString();
                    Common.GlobalValues.ClientCommon.Data.Group = GroupBox.getText().toString();

                    SetInfo();

                    dialog.dismiss();
                }

            }
        }) ;
        builder.setNegativeButton("Отменить", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int id) {
                dialog.dismiss();
            }
        });
        builder.create().show();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_test_result);
        Initialize();
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu items for use in the action bar
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_test_result, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
//        if(item.getItemId() == R.id.menu_activity_test_result_action_save)
//        {
//            SaveButton();
//        }
        if(item.getItemId()==R.id.menu_activity_test_result_action_about)
        {
            AlertDialog ad = new AlertDialog.Builder(this).create();
            ad.setIcon(R.drawable.ic_action_about);
            ad.setTitle("Информация");
            //  ad.setCancelable(false); // This blocks the 'BACK' button
            ad.setMessage(getResources().getString(R.string.info_result_activity));
            ad.setButton("OK", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                }
            });
            ad.show();
        }
        if(item.getItemId() == R.id.menu_activity_test_result_action_new_test)
        {
            Common.GlobalValues.ClientCommon.Data=new ClientData();
            Common.GlobalValues.ClientCommon.Result = new ClientResult();
            Common.GlobalValues.ClientCommon.test = null;
            startActivity(new Intent(NewTestResultActivity.this,RegisterActivity.class));
        }
        return false;

    }

    private void SendToServer()
    {
        InternetTask task = new InternetTask();
        task.execute(XmlReport,Common.GlobalValues.Internet.SERVER_IP);
    }

//    private void Open()
//    {
//
//    }
//
    private void SaveResults(String path)
    {
        TestAndClientResult buffer = new TestAndClientResult(Common.GlobalValues.ClientCommon.test,Common.GlobalValues.ClientCommon.Data);
        try {
            Common.CommonMethods.SerializationUtils.Save(buffer,this,path);
        } catch (Exception e) {
            Common.CommonMethods.DialogUtils.ShowErrorMessage("Не удалось сохранить файл" + e.getMessage(),this);
        }
    }

    private void SaveButton()
    {
        try{
            int iLoop = 0;
            int iTryCount = 2;
            for (iLoop = 0; iLoop < iTryCount; iLoop++)
            {
                Intent intent = null;

                if (iLoop == 0)
                {
                    // OI File Manager
                    intent = new Intent(Intent.ACTION_PICK);
                    intent.setData(Uri.fromFile(Environment.getExternalStorageDirectory()));
                    intent.setAction("org.openintents.action.PICK_FILE");
                    intent.putExtra("org.openintents.extra.TITLE", "SaveButton As...");
                }
                else if (iLoop == 1)
                {
                    // AndExplorer
                    intent = new Intent(Intent.ACTION_PICK);
                    intent.setDataAndType(Uri.fromFile(Environment.getExternalStorageDirectory()),
                            "vnd.android.cursor.dir/lysesoft.andexplorer.file");
                    intent.putExtra("explorer_title", "SaveButton As...");
                    intent.putExtra("browser_filter_extension_whitelist", "*.png");
                    intent.putExtra("browser_line", "enabled");
                    intent.putExtra("browser_line_textfield", "file1.png");
                }

                if (intent != null)
                {
                    try
                    {
                        // Try to launch activity
                        startActivityForResult(intent, Common.GlobalValues.Intents.FILE_SELECT_CODE);

//                     // TODO: Remove this notification on Publish
//                     Toast.makeText(m_baseSurfaceView.getContext(),
//                             "Try : " + iLoop, Toast.LENGTH_SHORT).show();

                        // If everything gone well, then break from here
                        // otherwise see catch(..) block
                        break;
                    }
                    catch (Exception e)
                    {
                        e.printStackTrace();

                        // If all tries are done, then conclusion is that,
                        // the user needs to install some file-manager application
                        // to locate where to save the file
                        if (iLoop == iTryCount - 1)
                        {
//                         Toast.makeText(
//                                 m_baseSurfaceView.getContext(),
//                                 "Please Install some File Manager"
//                                         + " application to locate destination.",
//                                 Toast.LENGTH_LONG).show();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonMethods.DialogUtils.ShowErrorMessage(ex.getMessage(),this);
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {

        if(requestCode == Common.GlobalValues.Intents.FILE_SELECT_CODE && resultCode == RESULT_OK)
        {
            switch (requestCode) {
                case Common.GlobalValues.Intents.FILE_SELECT_CODE: {
                    if (resultCode==RESULT_OK && data!=null && data.getData()!=null) {
                        String theFilePath = data.getData().getPath();
                        // Common.CommonMethods.DialogUtils.ShowErrorMessage(theFilePath,this);
                        SaveResults(theFilePath);
                    }
                    break;
                }
            }

        }

        super.onActivityResult(requestCode, resultCode, data);
    }

    private void ShowResults(String xml)
    {
        ClientResult result = new ClientResult();
        try {
            Serializer serializer = new Persister();
            result = (ClientResult)serializer.read(ClientResult.class,xml,false);
            Common.GlobalValues.ClientCommon.Result = result;
            // result = (ClientResult)Common.CommonMethods.SerializationUtils.Deserialize(ClientResult.class,xml);
        } catch (Exception e) {
            Common.CommonMethods.DialogUtils.ShowErrorMessage("Не удалось получить результаты тестирования",NewTestResultActivity.this);
            return;
        }

        String res ="Количество баллов: " + result.ClientBalls + "\n"+"Процент правильных ответов: " + result.Percent + "%" + "\n" +"Оценка: " + result.Mark;

        TextView Mark = (TextView)findViewById(R.id.activity_new_test_result_mark_box);
        TextView Procent = (TextView)findViewById(R.id.activity_new_test_result_procent_box);
        TextView Ball = (TextView)findViewById(R.id.activity_new_test_result_ball_box);

        try{
            Mark.setText("Оценка: "+Integer.toString(result.Mark));
            Ball.setText("Первичный балл: "+Integer.toString(result.ClientBalls) + "/" + Integer.toString(result.AllBalls));
            Procent.setText("Процент правильных ответов: "+Float.toString(result.Percent));
        }

        catch (Exception ex)
        {
            Common.CommonMethods.DialogUtils.ShowErrorMessage("ERRRORRR!!!!",this);
        }
    }


    /**
     * download test from remote server
     * example: task.execute("message to server","192.168.1.5");
     */
    public class InternetTask extends AsyncTask<String,String,String> {

        private TcpClient client = new TcpClient();
        private ProgressDialog dialog;

        @Override
        protected void onPreExecute() {
            try{
                super.onPreExecute();
                dialog = new ProgressDialog(NewTestResultActivity.this);
                this.dialog.setMessage("Получение результатов тестирования."+"\n" + "Пожалуйста подождите...");
                this.dialog.show();
            }
            catch (Exception ex)
            {
                int o =9;
            }
        }

        @Override
        protected String doInBackground(String... message) {

            final String msg = message[0];
            final String ip = message[1];

            try
            {
                client.Connect(ip, Common.GlobalValues.Internet.SERVER_PORT);
            }
            catch(Exception ex)
            {
                client.Stop();
                return "#Неудалось соединиться с сервером";
            }

            try
            {
                client.Send(msg);
            }
            catch (Exception e)
            {
                client.Stop();
                return "#Неудалось отправить данные на сервер";
            }

            String res = null;

            try
            {
                res = client.Recieve();
            }
            catch (Exception e)
            {
                client.Stop();
                return "#Неудалось получить ответ сервера";
            }

            client.Stop();
            return  res;
        }


        @Override
        protected void onPostExecute(String result) {
            super.onPostExecute(result);

            if (dialog.isShowing()) {
                dialog.dismiss();
            }

            if(result.toCharArray()[0] == '#')
            {
                result = result.substring(1);
                Common.CommonMethods.DialogUtils.ShowErrorMessage(result,NewTestResultActivity.this);
                return;
            }
            if(result.toCharArray()[1] == '#')
            {
                result = result.substring(2);
                Common.CommonMethods.DialogUtils.ShowErrorMessage(result,NewTestResultActivity.this);
                return;
            }
            else if(result != null)
            {
                result = result.substring(1);
                ShowResults(result);
            }

            else if(result == null)
            {
                String[] items = new String[]{"Попробовать снова", "Сохранить на устройстве"};
                Common.CommonMethods.DialogUtils.ShowChooseDialog("Не удалось связаться с сервером",items,NewTestResultActivity.this,
                        new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialogInterface, int i) {
                                if(i == 0)
                                {
                                    SendToServer();
                                }
                                if(i ==1 )
                                {
                                    SaveButton();
                                }
                            }
                        });
            }

        }

    }

    public class ParseTask extends AsyncTask<ClientData,String, String>
    {
        private ProgressDialog progressDialog;
        private String xmlMessage;

        @Override
        protected void onPreExecute()
        {
            try{
                super.onPreExecute();
                progressDialog = new ProgressDialog(getApplicationContext());
                this.progressDialog.setMessage("Разбор результатов тестировния."+"\n" + "Пожалуйста подождите...");
                this.progressDialog.show();
            }
            catch (Exception ex)
            {
                int o =9;
            }
        }

        @Override
        protected String doInBackground(ClientData... message)
        {
            ClientData Report = message[0];
            String  res = null;
            try {
                res = Common.CommonMethods.SerializationUtils.Serialize(Report,getApplicationContext());
            } catch (Exception e) {
                e.printStackTrace();  //To change body of catch statement use File | Settings | File Templates.
            }
            return res;

        }

        @Override
        protected void onPostExecute(String result)
        {
            super.onPostExecute(result);
            String[] items = new String[]{"Отправить на сервер", "Сохранить на устройстве"};

            if (progressDialog.isShowing()) {
                progressDialog.dismiss();
            }

            if(result != null)
            {
                setXmlReport(result);
//                 if(action.equals(Common.GlobalValues.Intents.Actions.SendToServerAction))
//                 {
//                     setXmlReport(result);
//                      SendToServer();
//                 }
//                 if(action.equals(Common.GlobalValues.Intents.Actions.SaveOnDevice))
//                 {
//                     setXmlReport(result);
//                     SaveButton();
//                 }
            }
            else
            {

            }
        }
    }


}