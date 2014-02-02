package com.example.SimpleTestClient.Dialogs;

import android.app.Dialog;
import android.content.Context;
import android.graphics.*;
import android.os.Bundle;
import android.widget.ImageView;
import android.widget.TextView;
import com.example.SimpleTestClient.R;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 23.08.13
 * Time: 20:44
 * To change this template use File | Settings | File Templates.
 */
public class ImageTextDialog extends Dialog {

    public Bitmap getBitmap() {
        return bitmap;
    }
    public void setBitmap(Bitmap bitmap) {
        this.bitmap = bitmap;
        if(isShowing() && this.imageView!=null)
        {
            this.imageView.setImageBitmap(this.bitmap);
        }
    }
    private Bitmap bitmap;

    public String getText() {
        return text;
    }
    public void setText(String text) {
        this.text = text;
        if(isShowing() && this.textView!=null)
        {
           this.textView.setText(this.text);
        }
    }
    private String text;

    ImageView imageView;
    TextView textView;

    public ImageTextDialog(Context context,Bitmap bmp,String text) {
        super(context);
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.dialog_image_with_text);

        this.textView = (TextView)this.findViewById(R.id.dialog_imagetext_text);
        this.imageView = (ImageView)this.findViewById(R.id.dialog_imagetext_image);

        if(this.bitmap != null)
        {
            this.imageView.setImageBitmap(this.bitmap);
        }
        else
        {
            this.imageView.setImageBitmap(DrawTextOnBitmap("Ошибка!", Color.RED));
        }

        this.textView.setText(this.text);
    }

    private static  Bitmap DrawTextOnBitmap(String text, int color)
    {
        Paint paint = new Paint();
        paint.setStyle(Paint.Style.FILL);
        paint.setColor(color);
        paint.setTextSize(16);
        paint.setAntiAlias(true);
        paint.setTypeface(Typeface.MONOSPACE);

        Bitmap bm = Bitmap.createBitmap(16, 16, Bitmap.Config.ALPHA_8);
        float x = bm.getWidth();
        float y = bm.getHeight();
        Canvas c = new Canvas(bm);
        c.drawText(text, x, y, paint);

        return  bm;
    }
}
