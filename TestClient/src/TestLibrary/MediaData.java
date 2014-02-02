package TestLibrary;

import com.example.SimpleTestClient.Utils.Common;
import org.simpleframework.xml.Element;
import org.simpleframework.xml.ElementList;
import org.simpleframework.xml.Root;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Pit on 11.06.13.
 */
@Root(name = "MediaData")
public class MediaData {

    @Element(name = "Text",required =  false)
    public String Text;

//    @ElementMap(required = false,name = "Files",entry="Item", key="Key", value = "Value", valueType = MediaFile.class,keyType = String.class,attribute = false,inline = true)
//    public HashMap<String,MediaFile> Files;
   // @ElementList(entry = "File",inline = true,required = false)
    @ElementList(name = "Files")
    public List<MediaFile> Files = new ArrayList<MediaFile>();

    public MediaData(String text)
    {
        this.Text = text;
    }

    public MediaData(){}



    public List<MediaFile> GetImages()
    {
        return  GetFilesByExtensions(Common.GlobalValues.DataCommon.FileExtensions.ImageFileEtensions);
    }

    public List<MediaFile> GetFilesByExtensions(List<String> extensions)
    {
        if(this.Files.size() == 0){return  null;}
        List<MediaFile> res = new ArrayList<MediaFile>();
        for(MediaFile file : this.Files)
        {
            for(String i_ext : extensions)
            {
                if(file.FileExtension.equals(i_ext))
                {
                    res.add(file);
                }
            }
        }

        if(res.size() == 0){return  null;}
        else{return res;}
    }

}
