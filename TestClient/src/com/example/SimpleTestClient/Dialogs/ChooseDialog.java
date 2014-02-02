package com.example.SimpleTestClient.Dialogs;

import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import com.example.SimpleTestClient.R;

import java.util.ArrayList;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 29.08.13
 * Time: 20:13
 * To change this template use File | Settings | File Templates.
 */
public class ChooseDialog  extends DialogFragment {
    private ListView chooseList;

    public ArrayList<String> getItems() {
        return items;
    }
    public void setItems(ArrayList<String> items) {
        this.items = items;
    }
    private ArrayList<String> items =  new ArrayList<String>();


    public void setmOnItemClickHandler(OnItemClickListener mOnClickCallback) {
        this.mOnClickCallback = mOnClickCallback;
    }
    private OnItemClickListener mOnClickCallback;

//    @Override
//    public Dialog onCreateDialog(Bundle savedInstanceState) {
//
//        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
//        // Get the layout inflater
//        LayoutInflater inflater = getActivity().getLayoutInflater();
//
//        // Inflate and set the layout for the dialog
//        // Pass null as the parent view because its going in the dialog layout
//        builder.setView(inflater.inflate(R.layout.dialog_choose, null));
//
//
//
//        //this.chooseList = (ListView) this.getActivity().findViewById(R.id.dialog_choose_list);
//        this.chooseList = (ListView) this.getActivity().findViewById(R.id.dialog_choose_list);
//
//        this.chooseList.setAdapter(new ArrayAdapter<String>(getActivity().getApplicationContext(),android.R.layout.simple_list_item_1, this.items));
////        this.chooseList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
////            @Override
////            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
////                onItemClick(adapterView,view,i,l);
////            }
////        });
//        return builder.create();
//    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.dialog_choose, container, false);
        this.chooseList = (ListView) v.findViewById(R.id.dialog_choose_list);

        this.chooseList.setAdapter(new ArrayAdapter<String>(getActivity().getApplicationContext(),android.R.layout.simple_list_item_1, this.items));
        this.chooseList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                onItemClick(adapterView,view,i,l);
            }
        });
        getDialog().setTitle("Выберите нужный вариант");


        return v;
    }

    public ChooseDialog(ArrayList<String> items) {
        super();
        this.items = items;
}

    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
        if(mOnClickCallback != null)
        {
            mOnClickCallback.OnClick(i,items.get(i));
        }
        this.getDialog().cancel();
    }

    public interface OnItemClickListener
    {
        public void OnClick(int num, String value);
    }
}