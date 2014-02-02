package TestLibrary.Abstract;

import TestLibrary.Answer;
import TestLibrary.MediaData;
import org.simpleframework.xml.*;

import java.util.ArrayList;

//------

/**
 * Created by Pit on 11.06.13.
 */
@Root(name = "Question")
public abstract class QuestionBase {

    public enum QuestionType
    {
        SingleChoice,
        MultiChoice,
        TextQuestion
    }


    @Attribute(name = "Name")
    public String Name;

    @Attribute(name = "ID")
    public String ID;

    @Transient
    public  QuestionType Type;

    @Attribute(name = "MaxBall")
    public int MaxBall;

    @Attribute(name = "MinBall")
    public int MinBall;

    @Attribute(name = "Number")
    public int Number;

    @Element(name = "MediaData")
    public MediaData Data = new MediaData();

    @ElementList(name = "Answers")
    public ArrayList<Answer> Answers = new ArrayList<Answer>();

   //public abstract  void Draw(LinearLayout Question_layout,Context context);

}
