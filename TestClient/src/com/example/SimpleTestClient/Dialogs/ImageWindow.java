package com.example.SimpleTestClient.Dialogs;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.ImageView;
import android.widget.TextView;
import com.example.SimpleTestClient.Adapters.ImageAdapterItem;
import com.example.SimpleTestClient.R;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 06.11.13
 * Time: 22:39
 * To change this template use File | Settings | File Templates.
 */
public class ImageWindow
{
    public static void ShowImageWindow(Activity activity,ImageAdapterItem item)
    {
//        LayoutInflater inflater = (LayoutInflater) activity.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
//        View layout = inflater.inflate(R.layout.dialog_image_with_text, (ViewGroup) activity.findViewById(R.id.dialog_imagetext_root));
//        final PopupWindow pwindo = new PopupWindow(layout, 300, 370, true);
//        pwindo.showAtLocation(layout, Gravity.CENTER, 0, 0);
//
//        Button btnClosePopup = (Button) layout.findViewById(R.id.dialog_imagetext_btn_exit);
//        btnClosePopup.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                pwindo.dismiss();
//            }
//        });


        LayoutInflater inflater = (LayoutInflater)activity.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        View layout = inflater.inflate(R.layout.dialog_image_with_text, (ViewGroup) activity.findViewById(R.id.dialog_imagetext_root));

        ImageView imageView = (ImageView)layout.findViewById(R.id.dialog_imagetext_image);
        imageView.setImageBitmap(item.bitmap);

        TextView textView = (TextView)layout.findViewById(R.id.dialog_imagetext_text);
        textView.setText(item.text);

        AlertDialog.Builder builder;
        AlertDialog alertDialog;

        builder = new AlertDialog.Builder(activity.getParent());
        builder.setView(layout);
        alertDialog = builder.create();
        alertDialog.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        alertDialog.show();
    }

}
