package TestLibrary.Client;

import org.simpleframework.xml.Attribute;
import org.simpleframework.xml.Element;
import org.simpleframework.xml.Root;

/**
 * Created by Pit on 13.06.13.
 */
@Root(name = "Client")
public class ClientData
{
    @Attribute(name = "Request")
    public String _request = "SETRESULT";

    @Attribute(name = "Name")
    public String Name;

    @Attribute(name = "LastName")
    public String LastName;

    @Attribute(name = "Group")
    public String Group;

    @Element(name = "Report")
    public ClientReport Report = new ClientReport();
}
