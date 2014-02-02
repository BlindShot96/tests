package com.example.SimpleTestClient.Adapters;

import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import com.example.SimpleTestClient.R;

import java.io.ByteArrayOutputStream;

/**
 * Created with IntelliJ IDEA.
 * User: Pit
 * Date: 13.07.13
 * Time: 19:17
 * To change this template use File | Settings | File Templates.
 */
public class ImageAdapterItem
{
    /**
     * изображение
     */
    public Bitmap bitmap;

    /**
     * описание изображения
     */
    public String text;

    public ImageAdapterItem(byte[] bytes, String text)
    {
        this.text = text;
        Bitmap bmp= BitmapFactory.decodeByteArray(bytes, 0, bytes.length);
        if(bmp == null)
        {
            bmp = ImageAdapter.DrawTextOnBitmap("Ошибка!", Color.RED);
        }

        this.bitmap = bmp;
    }

    public ImageAdapterItem(Resources res, int id, String text)
    {
        this.text = text;
        Bitmap bmp = BitmapFactory.decodeResource(res,id);

        if(bmp==null)
        {
            bmp = ImageAdapter.DrawTextOnBitmap("Ошибка!", Color.RED);
        }

        this.bitmap = bmp;
    }

//    public static byte[] Test(Resources res)
//    {
//        Bitmap bmp = BitmapFactory.decodeResource(res, R.drawable.test);
//        ByteArrayOutputStream stream = new ByteArrayOutputStream();
//        bmp.compress(Bitmap.CompressFormat.JPEG, 100, stream);
//        byte[] bitMapData = stream.toByteArray();
//
//        return bitMapData;
//    }


}
