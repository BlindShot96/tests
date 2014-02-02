package TestLibrary.Abstract;

//import org.simpleframework.*;

import TestLibrary.MediaData;
import TestLibrary.TestSettings;
import org.simpleframework.xml.Element;
import org.simpleframework.xml.ElementList;
import org.simpleframework.xml.Root;

import java.util.ArrayList;

/**
 * Created by Pit on 11.06.13.
 */
@Root(name = "Test")
//@Order(elements = {"Item[1]/Value/Question","Item[2]/Key/string"})
public  class TestBase {

//    @Attribute(name = "Name")
//    public String Name;
//
//    @Attribute(name = "TeacherName")
//    public String TeacherName;
//
//    @Attribute(name = "MaxMark")
//    public int MaxMark;
//
//    @Attribute(name = "MinMark")
//    public  int MinMark;
//
//    @Attribute(name = "MinuteTime")
//    public int Minutes;

    @Element(name = "Settings")
    public TestSettings Settings = new TestSettings();

    @Element(name = "MediaData")
    public MediaData Data = new MediaData();

    @ElementList(name = "Questions")
    public ArrayList<QuestionBase> Questions = new ArrayList<QuestionBase>();


//    public void SortByNumber()
//    {
//        int j =0;
//        int i =0;
//
//        while(i < Questions.size())
//        {
//            j=0;
//            while( j < Questions.size())
//            {
//                if( j != i)
//                {
//                    if(Questions.get(j).Number < Questions.get(i).Number)
//                    {
//                        Questions.toArray()[i] =  Questions.toArray()[j];
//                    }
//                }
//                j++;
//            }
//            i++;
//        }
//    }
//
    public QuestionBase getQuestionByNumber(int num)
    {
        for (QuestionBase qEl : Questions)
        {
            if(qEl.Number == num)
            {
                return  qEl;
            }
        }
        return  null;
    }

    public QuestionBase getQuestionByID(String id)
    {
        for (QuestionBase qEl : Questions)
        {
            if(qEl.ID == id)
            {
                return  qEl;
            }
        }
        return  null;
    }
}

