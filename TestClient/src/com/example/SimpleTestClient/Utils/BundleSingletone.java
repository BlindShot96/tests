package com.example.SimpleTestClient.Utils;

import java.util.HashMap;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 06.01.14
 * Time: 20:32
 * To change this template use File | Settings | File Templates.
 */
public class BundleSingletone
{
   private static HashMap<String,Object> Objects = new HashMap<String, Object>();

   public static void put(String id, Object object)
   {
       if(Objects.containsKey(id)==true)
       {
           Objects.remove(id);
       }
       Objects.put(id,object);
   }

    public static Object get_without_del(String id)
    {
       return Objects.get(id);
    }

    public static Object get(String id)
    {
        if(Objects.containsKey(id)==false)
        {
            return null;
        }
        Object res = Objects.get(id);
        Objects.remove(id);
        return res;
    }

    public static void remove(String id)
    {
        Objects.remove(id);
    }
}
