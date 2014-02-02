package TestLibrary;

import android.util.Base64;
import org.simpleframework.xml.Attribute;
import org.simpleframework.xml.Element;
import org.simpleframework.xml.Root;

/**
 * Created by Pit on 11.06.13.
 */
@Root(name = "MediaFile")
public class MediaFile {

    @Attribute(name = "ID",required = false)
    public String ID;

    /// <summary>
    /// байты файла
    /// </summary>
    @Element(name = "Bytes",required = false)
    public String Bytes;

    public byte[] getBytes()
    {
       return Base64.decode(this.Bytes,Base64.DEFAULT);
    }

    /// <summary>
    /// расширение файла с точкой
    /// </summary>
    /// <example>.jpg</example>
    @Attribute(name = "FileExtension",required =  false)
    public String FileExtension;

    /// <summary>
    /// имя файла
    /// </summary>
    @Attribute(name = "FileName",required = false)
    public String FileName;

    /// <summary>
    /// описание файла
    /// </summary>
    @Element(name = "Text",required = false)
    public String Text ;

}
