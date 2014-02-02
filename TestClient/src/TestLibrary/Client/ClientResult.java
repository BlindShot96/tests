package TestLibrary.Client;

import org.simpleframework.xml.Attribute;
import org.simpleframework.xml.Root;

/**
 * Created by Pit on 15.06.13.
 */

@Root(name = "Result")
public class ClientResult
{
    @Attribute(name = "Mark")
    public int Mark;

    @Attribute(name = "Balls")
    public int ClientBalls;

    @Attribute(name = "AllBalls")
    public int AllBalls;

    @Attribute(name = ("Percent"))
    public float Percent;

}
