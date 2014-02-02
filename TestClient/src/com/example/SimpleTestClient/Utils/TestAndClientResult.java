package com.example.SimpleTestClient.Utils;

import TestLibrary.Client.ClientData;
import TestLibrary.Test;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 07.10.13
 * Time: 23:30
 * To change this template use File | Settings | File Templates.
 */
public class TestAndClientResult
{
    public Test Test;
    public ClientData ClientData;

    public TestAndClientResult(TestLibrary.Test test, TestLibrary.Client.ClientData clientData) {
        Test = test;
        ClientData = clientData;
    }
    public TestAndClientResult(){}
}
