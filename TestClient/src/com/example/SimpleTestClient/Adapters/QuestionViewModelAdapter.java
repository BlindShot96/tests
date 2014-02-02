package com.example.SimpleTestClient.Adapters;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.SimpleTestClient.R;
import com.example.SimpleTestClient.ViewModels.QuestionBaseViewModel;
import com.example.SimpleTestClient.Utils.ShowMode;

import java.util.ArrayList;

/**
 * Created by пётр on 08.01.14.
 */
public class QuestionViewModelAdapter extends ArrayAdapter<QuestionBaseViewModel> {

    private ArrayList<QuestionBaseViewModel> objects;
    Activity ctx;
    LayoutInflater lInflater;
    ShowMode showMode;

    public QuestionViewModelAdapter(Activity context, ArrayList<QuestionBaseViewModel> objects, ShowMode showMode) {
        super(context,R.layout.view_simple_list_item_light,objects);
        this.objects = objects;
        this.ctx = context;
        this.lInflater = context.getLayoutInflater();
        this.showMode = showMode;
    }

    // кол-во элементов
    @Override
    public int getCount() {
        return objects.size();
    }

    // элемент по позиции
    @Override
    public QuestionBaseViewModel getItem(int position) {
        return objects.get(position);
    }

    // id по позиции
    @Override
    public long getItemId(int position) {
        return position;
    }

    // пункт списка
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        // используем созданные, но не используемые view
        View view = convertView;
        if (view == null) {
            if(showMode == ShowMode.Portrait){
                    view = this.lInflater.inflate(R.layout.view_simple_list_item_dark, parent, false);
            }
            else
            {
                view = this.lInflater.inflate(R.layout.view_simple_list_item_light,parent,false);
            }

        }

        QuestionBaseViewModel p = getViewModel(position);

        // заполняем View в пункте списка данными из товаров: наименование, цена
        // и картинка
        ((TextView) view.findViewById(R.id.view_simple_list_item_text)).setText(p.Name());
//        if(view.findViewById(R.id.view_simple_list_item_image)!=null && p.isAnswered() == true)
//        {
//            ImageView imageView = (ImageView)view.findViewById(R.id.view_simple_list_item_image);
//            imageView.setImageResource(R.drawable.ic_action_accept);
//        }

        return view;
    }

    // товар по позиции
    public QuestionBaseViewModel getViewModel(int position) {
        return ((QuestionBaseViewModel) getItem(position));
    }


}
