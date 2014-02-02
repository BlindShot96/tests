package TestLibrary.Client;

import TestLibrary.MediaData;
import org.simpleframework.xml.Element;
import org.simpleframework.xml.Root;


/**
 * Created by Pit on 13.06.13.
 */
@Root(name = "Answer")
public class ClientQuestionAnswer
{
    @Element(name = "MediaData")
    public MediaData Data  = new MediaData();
}
