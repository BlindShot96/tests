package TestLibrary.Abstract;

import TestLibrary.MediaData;
import org.simpleframework.xml.Attribute;
import org.simpleframework.xml.Element;
import org.simpleframework.xml.Root;

/**
 * Created by Pit on 11.06.13.
 */
@Root(name = "AnswerBase")
public  class AnswerBase {

    @Attribute(name = "ID",required = false)
    public String ID;



    @Element(name = "MediaData")
    public MediaData Data = new MediaData();
}
