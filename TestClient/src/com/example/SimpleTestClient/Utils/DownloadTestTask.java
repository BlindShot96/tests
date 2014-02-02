package com.example.SimpleTestClient.Utils;

import android.content.Context;

import com.example.SimpleTestClient.Internet.TcpClient;
import com.example.SimpleTestClient.R;
import com.example.SimpleTestClient.Utils.Common;
import com.example.SimpleTestClient.Utils.SpinnerAsyncTask;

import java.io.IOException;
import java.io.UnsupportedEncodingException;

/**
 * Created by пётр on 08.01.14.
 */
public class DownloadTestTask extends SpinnerAsyncTask<String,String,String> {

    String IpAddress;
    int PortNumber;
    private TcpClient client = new TcpClient();

    private String message;
    public String getMessage() {
        return message;
    }
    public void setMessage(String message) {
        this.message = message;
    }

    public DownloadTestTask(Context context, String IpAddress, int portNumber, String msg) {
        super(context);
        this.IpAddress = IpAddress;
        this.PortNumber = portNumber;
        this.setMessage(msg);
    }

    @Override
    protected void onPreExecute()
    {
        super.onPreExecute();
        this.dialog.setMessage(this.context.getResources().getString(R.string.downloading_test));
    }


    @Override
    protected String doInBackground(String... message) {

        try
        {
            client.Connect(this.IpAddress, Common.GlobalValues.Internet.SERVER_PORT);
        }
        catch(Exception ex)
        {
            client.Stop();
            return "#Unable to connect";
        }

        try
        {
            client.Send(this.getMessage());
        }
        catch (Exception e)
        {
            client.Stop();
            return ("#Unable to send info");
        }

        String res = null;

        try
        {
            res = client.NewRecieve();
        }
        catch (Exception e)
        {
            client.Stop();
            return "#Unable to receive";
        }

        client.Stop();
        return  res;
    }


    @Override
    protected void onPostExecute(String result) {
        super.onPostExecute(result);

        String TestXml = "";

        if(result.toCharArray()[0] == '#')
        {
            Common.CommonMethods.DialogUtils.ShowErrorMessage("Не получилось скачать тест" +'\n'+ result,this.context);
        }

        else if(result != null)
        {
            try {
                TestXml = Common.CommonMethods.GetUtf8StringFrom1251(result);
                try {
                    Common.CommonMethods.FileUtils.SaveTextToFile(TestXml,"test.xml");
                } catch (IOException e) {
                    int t =0;
                }
            } catch (UnsupportedEncodingException e) {
                TestXml = result;
            }
            NotifyResultExecuteListeners(result);
        }
        else if(result == null)
        {
            Common.CommonMethods.DialogUtils.ShowErrorMessage("Не получилось скачать тест",this.context);
        }

    }


}
