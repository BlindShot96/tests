package com.example.SimpleTestClient.Utils;

import TestLibrary.Client.ClientData;
import TestLibrary.Client.ClientResult;
import TestLibrary.Test;
import android.annotation.TargetApi;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.res.Resources;
import android.database.Cursor;
import android.net.Uri;
import android.os.Environment;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;
import com.example.SimpleTestClient.R;
import org.simpleframework.xml.Serializer;
import org.simpleframework.xml.core.Persister;

import java.io.*;
import java.net.URISyntaxException;
import java.util.Arrays;
import java.util.List;

/**
 * Created by Pit on 10.06.13.
 */
public class Common {

    public static  class GlobalValues{

        public  static  class  Internet
        {
           public  static  String  SERVER_IP;
           public  static int SERVER_PORT = 5678;
        }

        public static  class Intents
        {
            public static final int FILE_SELECT_CODE = 0;

            public static  class Actions
            {
                public static  String DownloadTestAction = "SimpleTest.intent.DownloadTestServer";
                public static  String OpenTestAction = "SimpleTest.intent.OpenTestXml";

                public static String SendToServerAction = "SimpleTest.intent.SendToServer";
                public  static String SaveOnDevice = "SimpleTest.intent.SaveOnDevice";
            }

            public static  class Extra
            {
                public static String MESSAGE = "MSG";
                public static String IP = "IP";
            }
        }

        public static class ClientCommon
        {
            public static ClientData Data = new ClientData();
            public  static ClientResult Result = new ClientResult();
            public  static Test test = null;
        }

        public  static  class DataCommon
        {
            public static  String TEMP_CLIENT_DATA_FILE_NAME = "client_result_temp.xml";
            public static  String TEMP_TEST_FILE_NAME = "test_temp.xml";

            public  static  class FileExtensions
            {

              public static List<String> ImageFileEtensions = Arrays.asList(new String[]
                {
                    ".jpg",
                    ".png",
                    ".bmp"
                });

               public static List<String> TextFileEtensions = Arrays.asList(new String[]
                    {
                            ".txt",
                            ".html",
                            ".doc",
                            ".docx"
                    });

               public static List<String> AudioFileEtensions = Arrays.asList(new String[]
                    {
                            ".wav",
                            ".mp3"
                    });
            }
        }

        public static class Settings
        {
            public static int TextSize = 20;
        }
    }

    public static class CommonMethods
    {
        public static class SerializationUtils
        {
//            public static String Serialize(Object o,Context context) throws Exception {
//                String res = null;
//                Serializer serializer = new Persister();
//                OutputStreamWriter writer = null;
//                File.createTempFile("serialized.xml","serialized.xml");
//                writer = CommonMethods.FileUtils.GetOutputStreamWriter("serialized.xml",context);
//                serializer.write(o,writer);
//                res = CommonMethods.FileUtils.readStringFromFile(context,"serialized.xml");
//                return res;
//            }

            public static String Serialize(Object o,Context context) throws Exception {

                String res = null;
                File temp = FileUtils.getTempFile(context,"example.xml");

                Serializer serializer = new Persister();
                serializer.write(o,temp);

                res = FileUtils.ReadFromTempFile(context,"example.xml");
                return res;

            }

            public static void Save(Object o,Context context,String path) throws Exception {
                String res = null;
                File temp = new File(path);

                Serializer serializer = new Persister();
                serializer.write(o,temp);
            }

            public static Object Open(Class<?> type, String path) throws Exception {
                Serializer serializer = new Persister();
                Object res = serializer.read(type,path);
                return res;
            }

            public  static Object Deserialize(Class<?> type,String xml) throws Exception {
                Reader reader = new CharArrayReader(xml.toCharArray());
                Serializer serializer = new Persister();
                Object res = serializer.read(type,reader);
                return res;
            }

        }

        public static class FileUtils
        {

            public static void SaveTextToFile(String text,String filename) throws IOException {
                File file = new File(Environment.getExternalStorageDirectory(),filename);
                FileOutputStream fos;
                byte[] data = text.getBytes();
                fos = new FileOutputStream(file);
                fos.write(data);
                fos.flush();
                fos.close();
            }

            public static String getPath(Context context, Uri uri) throws URISyntaxException {
                if ("content".equalsIgnoreCase(uri.getScheme())) {
                    String[] projection = { "_data" };
                    Cursor cursor = null;

                    try {
                        cursor = context.getContentResolver().query(uri, projection, null, null, null);
                        int column_index = cursor.getColumnIndexOrThrow("_data");
                        if (cursor.moveToFirst()) {
                            return cursor.getString(column_index);
                        }
                    } catch (Exception e) {
                        // Eat it
                    }
                }
                else if ("file".equalsIgnoreCase(uri.getScheme())) {
                    return uri.getPath();
                }

                return null;
            }

            public static void WriteToTempFile(String text,Context context,String TEMP_FILE_NAME) throws IOException {

                File tempFile = getTempFile(context,TEMP_FILE_NAME);
                FileWriter writer = new FileWriter(tempFile);

                /** Saving the contents to the file*/
                writer.write(text);

                /** Closing the writer object */
                writer.close();
            }

            public static File getTempFile(Context context,String TEMP_FILE_NAME)
            {
                File tempFile;
                /** Getting Cache Directory */
                File cDir = context.getCacheDir();

                /** Getting a reference to temporary file, if created earlier */
                tempFile = new File(cDir.getPath() + "/" + TEMP_FILE_NAME) ;
                return tempFile;
            }
            public static String ReadFromTempFile(Context context,String TEMP_FILE_NAME) throws IOException {
                File tempFile = getTempFile(context,TEMP_FILE_NAME);

                FileReader fReader = new FileReader(tempFile);
                BufferedReader bReader = new BufferedReader(fReader);



                String res = "";
                String strLine = null;
                while( (strLine=bReader.readLine()) != null  ){
                    res+=strLine+"\n";
                }
                return res;
            }

            public static String readStringFromFile(Context context,String path) throws IOException {
              String ret = "";
                File file = new File(path);
                FileInputStream inputStream = new FileInputStream(file);

              if ( inputStream != null )
              {
                InputStreamReader inputStreamReader = new InputStreamReader(inputStream);
                BufferedReader bufferedReader = new BufferedReader(inputStreamReader);
                String receiveString = "";
                StringBuilder stringBuilder = new StringBuilder();

                while ( (receiveString = bufferedReader.readLine()) != null )
                {
                  stringBuilder.append(receiveString).append("\n");
                }
                 inputStream.close();
                 ret = stringBuilder.toString();
              }

              return ret;
            }

            public static void writeStringToFile(Context context,String data,String path) throws IOException {
                OutputStreamWriter outputStreamWriter = new OutputStreamWriter(context.openFileOutput(path, Context.MODE_PRIVATE));
                outputStreamWriter.write(data);
                outputStreamWriter.close();
            }

            public static OutputStreamWriter GetOutputStreamWriter(String FileName,Context c) {
                try {
                    OutputStreamWriter outputStreamWriter = new OutputStreamWriter(c.openFileOutput(FileName, Context.MODE_PRIVATE));
                    return  outputStreamWriter;
                }
                catch (IOException e) {
                    Log.e("FILE", "File write failed: " + e.toString());
                    return null;
                }
            }

            public static void SaveClientDataToFile(ClientData data,String path,Context context)
            {
                if( data != null)
                {
                    try{
                       String XmlReport = Common.CommonMethods.SerializationUtils.Serialize(data,context);
                        File file = new File(path);
                        if(file.exists() == false)
                        {
                            file.createNewFile();
                        }
                        FileOutputStream fos;
                        byte[] buf = XmlReport.getBytes();
                        fos = new FileOutputStream(file);
                        fos.write(buf);
                        fos.flush();
                        fos.close();
                    }
                    catch (Exception ex)
                    {
                        Common.CommonMethods.DialogUtils.ShowErrorMessage("Не удалось сохранить файл" + ex.getMessage(),context);
                    }
                }
            }

            public static void SaveClientDataToTempFile(ClientData data,Context context)
            {
                SaveClientDataToFile(data,
                        context.getCacheDir().getPath() + File.separator + GlobalValues.DataCommon.TEMP_CLIENT_DATA_FILE_NAME ,
                        context);
            }

        }

        public static class DialogUtils
        {
            public static void ShowErrorMessage(String msg,Context context)
            {
                AlertDialog ad = new AlertDialog.Builder(context).create();
                ad.setCancelable(false); // This blocks the 'BACK' button
                ad.setMessage(msg);
                ad.setButton("OK", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.dismiss();
                    }
                });
                ad.show();
            }

            public static void ShowYNDialog(String title,String msg,Context context,DialogInterface.OnClickListener Yeslistener,DialogInterface.OnClickListener Nolistener)
            {
                new AlertDialog.Builder(context)
                        .setTitle(title)
                        .setMessage(msg)
                        .setPositiveButton("Да", Yeslistener)
                        .setNegativeButton("Нет",Nolistener)
                        .show();
            }

            public static void ShowMultiChoiceDialog(final String title, final String[] items,final Context context, DialogInterface.OnMultiChoiceClickListener listener)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(context);
                boolean[] bul = new boolean[items.length];
                for(int i = 0; i < items.length; i++)
                {
                    bul[i] = true;
                }
                builder.setMultiChoiceItems(items,bul,listener);

                AlertDialog alert = builder.create();
                alert.show();
            }

            public static void ShowToast(String message,Context c)
            {
                Toast toast = Toast.makeText(c,message, Toast.LENGTH_LONG);
                toast.setGravity(Gravity.CENTER, 0, 0);
                toast.show();
            }

            public static void ShowChooseDialog(final String title, final String[] items,final Context c, DialogInterface.OnClickListener listener)
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(c);
                builder.setTitle(title);
                builder = builder.setItems(items,listener);

                AlertDialog alert = builder.create();
                alert.show();
            }

            public static void ShowTextDialog(final String Text,Context context)
            {
                AlertDialog.Builder popupBuilder = new AlertDialog.Builder(context);
                TextView myMsg = new TextView(context);
                myMsg.setText(Text);
                myMsg.setGravity(Gravity.CENTER_HORIZONTAL);
                popupBuilder.setView(myMsg);

                AlertDialog dialog = popupBuilder.create();
                dialog.show();
            }
            public static void ShowScrolTextDialog(final String Text,Context context)
            {
                Resources Res = context.getResources();
                LayoutInflater inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
                View myScrollView = inflater.inflate(R.layout.view_scrolling_text, null, false);

                TextView tv = (TextView) myScrollView
                        .findViewById(com.example.SimpleTestClient.R.id.view_scrolling_text_textview);

                tv.setText(Text);

                new AlertDialog.Builder(context).setView(myScrollView)
                        .setTitle("Scroll View")
                        .setPositiveButton("OK", new DialogInterface.OnClickListener() {
                            @TargetApi(17)
                            public void onClick(DialogInterface dialog, int id) {
                                dialog.cancel();
                            }

                        }).show();
            }
        }

        public static String GetUtf8StringFrom1251(String in) throws UnsupportedEncodingException {
            String res = new String(in.getBytes("Cp1251"),"UTF-8");
            return res;
        }
    }

    public static  class  CommonInterfaces
    {
        public interface OnSelectHandler
        {
            public void OnSelect(int number);
        }
    }

}
