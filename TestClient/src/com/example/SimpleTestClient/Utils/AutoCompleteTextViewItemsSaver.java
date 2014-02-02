package com.example.SimpleTestClient.Utils;

import android.content.Context;
import android.net.Uri;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.ArrayList;

/**
 * Created by пётр on 08.01.14.
 */
public abstract  class AutoCompleteTextViewItemsSaver
{
   protected String name = "default";
   protected abstract void getName();

   protected ArrayList<String> strings = new ArrayList<String>();

   public void add(String str)
   {
       this.strings.add(str);
   }

    public void clear()
    {
        this.strings.clear();
    }

    public void save(Context context)
    {
        File file = getTempFile(context,this.name);
        try {
            new RandomAccessFile(file,"rw").setLength(0);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            FileWriter f = new FileWriter(file);
            for(String s : strings)
            {
              f.append(s);
            }
            f.flush();
            f.close();
        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    public void load(Context context)
    {
        File file = getTempFile(context,this.name);

    }

      private File getTempFile(Context context, String url) {

        File file = null;
        try {
            String fileName = Uri.parse(url).getLastPathSegment();
            file = File.createTempFile(fileName, null, context.getCacheDir());
            return file;
        } catch (Exception e) {
          return file;
        }
      }

}
