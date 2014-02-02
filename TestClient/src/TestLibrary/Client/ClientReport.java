package TestLibrary.Client;

import org.simpleframework.xml.ElementList;
import org.simpleframework.xml.Root;

import java.util.ArrayList;

/**
 * Created by Pit on 13.06.13.
 */
@Root(name = "Report")
public class ClientReport
{
    @ElementList(required = false,name = "Questions", entry="Question")
    public ArrayList<ClientQuestion> Questions = new ArrayList<ClientQuestion>();

    public ClientQuestion getById(String id)
    {
        for (ClientQuestion q : Questions)
        {
            if(q.QuestionID.equals(id))
            {
                return q;
            }
        }
        throw new IllegalArgumentException();
    }

    public boolean containsId(String id)
    {
        for (ClientQuestion q : Questions)
        {
            if(q.QuestionID.equals(id))
            {
                return true;
            }
        }
        return false;
    }
}
