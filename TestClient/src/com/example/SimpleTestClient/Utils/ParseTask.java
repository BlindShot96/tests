package com.example.SimpleTestClient.Utils;

import android.app.ProgressDialog;
import android.content.Context;
import android.os.AsyncTask;
import org.simpleframework.xml.Serializer;
import org.simpleframework.xml.core.Persister;

/**
 * Created with IntelliJ IDEA.
 * User: Pit
 * Date: 07.10.13
 * Time: 14:46
 * To change this template use File | Settings | File Templates.
 */
public  class ParseTask<T> extends AsyncTask<T,String,T>
{
    public OnParseExecute onParseExecuteListener = new OnParseExecute() {@Override public void OnParseExecute(Object result) { }};
    public String Message = "Parsing operation";
    public Context context;

    private ProgressDialog dialog;
    private String xml;

    public ParseTask(Context context)
    {
        super();
        this.context = context;
    }

    @Override
    protected void onPreExecute()
    {
        try{
            super.onPreExecute();
            dialog = new ProgressDialog(context);
            this.dialog.show();
            this.dialog.setMessage(this.Message);
        }
        catch (Exception ex)
        {
            int o =9;
        }
    }

    @Override
    protected T doInBackground(Object... message)
    {

        xml = (String)message[0];
        T res = null;

        Serializer serializer = new Persister();
        try {
            res = (T) serializer.read(res.getClass(),xml,false);
        } catch (Exception e) {
            e.printStackTrace();  //To change body of catch statement use File | Settings | File Templates.
        }
        return res;
    }

    @Override
    protected void onPostExecute(T result)
    {
        super.onPostExecute(result);

        if (dialog.isShowing()) {
            dialog.dismiss();
        }
        this.onParseExecuteListener.OnParseExecute(result);

    }

    public interface OnParseExecute
    {
        public void  OnParseExecute(Object result);
    }
}
