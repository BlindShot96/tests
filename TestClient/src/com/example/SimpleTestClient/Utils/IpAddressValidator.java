package com.example.SimpleTestClient.Utils;

import android.webkit.URLUtil;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 09.10.13
 * Time: 1:18
 * To change this template use File | Settings | File Templates.
 */
public class IpAddressValidator implements ValidatorInterface
{
    private static final String PATTERN =
            "^([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." +
                    "([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." +
                    "([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\." +
                    "([01]?\\d\\d?|2[0-4]\\d|25[0-5])$";
    @Override
    public String getDescription(Object value) {
        return "";
    }

    @Override
    public boolean Validate(Object value) {
        String ip = "";
        try{
            ip = (String)value;
        }
        catch (Exception ex){return false;}

        Pattern pattern = Pattern.compile(PATTERN);
        Matcher matcher = pattern.matcher(ip);
        if(matcher.matches()==true)
        {
            return true;
        }
        else
        {
           return  URLUtil.isValidUrl(ip);
        }
    }
}