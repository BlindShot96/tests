package TestLibrary.Client;

import org.simpleframework.xml.Attribute;
import org.simpleframework.xml.ElementList;
import org.simpleframework.xml.Root;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Pit on 13.06.13.
 */
@Root(name = "Question")
public class ClientQuestion
{
    @Attribute(name = "ID")
    public String QuestionID;

    @ElementList(name = "Answers")
    public List<ClientQuestionAnswer> Answers = new ArrayList<ClientQuestionAnswer>();
}
