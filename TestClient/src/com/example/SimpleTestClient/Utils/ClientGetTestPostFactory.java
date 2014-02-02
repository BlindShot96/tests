package com.example.SimpleTestClient.Utils;

/**
 * Created by пётр on 08.01.14.
 */
public class ClientGetTestPostFactory
{
    public static String  createPost(String name,String lastname, String group)
    {
       String res = "<?xml version=\"1.0\" encoding=\"utf-8\" ?> <Client Name =\""+name+"\" Request=\"GETTEST\" LastName=\"" + lastname+ "\" Group =\"" + group + "\">GET_TEST</Client>";
       return res;
    }
}
