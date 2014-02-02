package com.example.SimpleTestClient.Utils;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.os.AsyncTask;

import com.example.SimpleTestClient.R;

/**
 * Created by пётр on 08.01.14.
 */
public abstract class SpinnerAsyncTask <Params, Progress, Result> extends AsyncTask <Params, Progress, Result> {

    protected ProgressDialog dialog;
    protected Context context;

    public interface OnResultExecute
    {
        public void OnResultExecuted(Object result);
    }

    private OnResultExecute onResultExecuteListener;
    public OnResultExecute getOnResultExecuteListener() {
        return onResultExecuteListener;
    }
    public void setOnResultExecuteListener(OnResultExecute onResultExecuteListener) {
        this.onResultExecuteListener = onResultExecuteListener;
    }


    public SpinnerAsyncTask(Context context)
    {
        this.context = context;
    }

    @Override
    protected void onPreExecute()
    {
        super.onPreExecute();
        dialog = new ProgressDialog(this.context);
        dialog.setCancelable(false);
        this.dialog.show();
    }


    @Override
    protected void  onPostExecute(Result result)
    {
        super.onPostExecute(result);
        if (dialog != null) {
            dialog.dismiss();
        }
    }

    protected void NotifyResultExecuteListeners(Object result)
    {
        if(this.onResultExecuteListener != null)
        {
            this.onResultExecuteListener.OnResultExecuted(result);
        }
    }

    @Override
    protected void  onCancelled(Result result)
    {
        super.onCancelled(result);
        if (dialog != null) {
            dialog.dismiss();
        }
    }
}
