package com.example.SimpleTestClient.Utils;

import android.content.Context;

import com.example.SimpleTestClient.R;
import com.example.SimpleTestClient.Utils.Common;
import com.example.SimpleTestClient.Utils.SpinnerAsyncTask;

import org.simpleframework.xml.Serializer;
import org.simpleframework.xml.core.Persister;
import TestLibrary.Test;

public class ParseTestTask extends SpinnerAsyncTask<String,String,Test>
{
    private String xml;

    public ParseTestTask(Context context, String TestXml) {
        super(context);
        this.xml = TestXml;
    }

    @Override
    protected void onPreExecute()
    {
        super.onPreExecute();
        this.dialog.setMessage(this.context.getResources().getString(R.string.parsing_test));
    }

    @Override
    protected Test doInBackground(String... message)
    {
        if(xml.toCharArray()[0]  != ' ' && xml.toCharArray()[0] != '<')
        {
            xml = xml.substring(1);
        }

        Test res;
        res = new Test();

        Serializer serializer = new Persister();
        try {
            res = serializer.read(Test.class,xml,false);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return res;
    }

    @Override
    protected void onPostExecute(Test result)
    {
        super.onPostExecute(result);

        if(result == null)
        {
            Common.CommonMethods.DialogUtils.ShowErrorMessage("Не правльный формат теста.",this.context);
        }

        if(result != null)
        {
            Common.GlobalValues.ClientCommon.test = result;
            // Common.CommonMethods.DialogUtils.ShowToast("Тест разобран!", TestInfoActivity.this);
            //ShowTestInfo(result);
            NotifyResultExecuteListeners(result);
        }

    }
}
