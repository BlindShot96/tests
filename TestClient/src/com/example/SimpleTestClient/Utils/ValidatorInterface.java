package com.example.SimpleTestClient.Utils;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 27.08.13
 * Time: 22:53
 * To change this template use File | Settings | File Templates.
 */
public interface ValidatorInterface {
    public String getDescription(Object value);
    public boolean Validate(Object value);
}
