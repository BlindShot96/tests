package com.example.SimpleTestClient.Adapters;

import TestLibrary.MediaFile;
import android.content.Context;
import android.content.res.Resources;
import android.graphics.*;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Gallery;
import android.widget.ImageView;

import java.util.ArrayList;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: Pit
 * Date: 12.07.13
 * Time: 15:35
 * To change this template use File | Settings | File Templates.
 */
public class ImageAdapter extends BaseAdapter
{
    public List<ImageAdapterItem> Items = new ArrayList<ImageAdapterItem>();

    private Context mContext;

    public int getCount() {
        return Items.size();
    }

    public Object getItem(int position) {
        return Items.get(position);
    }

    public long getItemId(int position) {
        return position;
    }

    public ImageAdapter(Context context, ArrayList<ImageAdapterItem> items)
    {
        this.mContext = context;
        this.Items = items;
    }

    // create a new ImageView for each item referenced by the Adapter
    public View getView(int position, View convertView, ViewGroup parent) {
        ImageView imageView;
        if (convertView == null) {  // if it's not recycled, initialize some attributes
            imageView = new ImageView(mContext);
            imageView.setLayoutParams(new Gallery.LayoutParams(250, 250));
            imageView.setScaleType(ImageView.ScaleType.CENTER_CROP);
            imageView.setPadding(8, 8, 8, 8);
        } else {
            imageView = (ImageView) convertView;
        }

        try
        {
          imageView.setImageBitmap(Items.get(position).bitmap);
        }
        catch (Exception ex)
        {
            imageView.setImageBitmap(DrawTextOnBitmap("Error!",Color.RED));
        }
        return imageView;
    }

    /**
     * рисует на bitmap текст с заданным цветом
     * @param text
     * @param color
     * @return
     */
    public static  Bitmap DrawTextOnBitmap(String text, int color)
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

    /**
     * трансформирует список файлов в список объектов изображений
     * @param files список файлов с изображениями
     * @return
     */
    public static ArrayList<ImageAdapterItem> ConvertImagesFiles(ArrayList<MediaFile> files,Resources res)
    {
        ArrayList<ImageAdapterItem> result = new ArrayList<ImageAdapterItem>();
        for (MediaFile file : files)
        {

          // byte[] b1 = file.getBytes();
          // byte[] b2 = ImageAdapterItem.Test(res);

           ImageAdapterItem item  = new ImageAdapterItem(file.getBytes(),file.Text);
           result.add(item);
        }
        return  result;
    }
}
