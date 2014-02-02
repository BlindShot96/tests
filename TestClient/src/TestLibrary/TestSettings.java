package TestLibrary;

import org.simpleframework.xml.Attribute;

/**
 * Created with IntelliJ IDEA.
 * User: пётр
 * Date: 28.08.13
 * Time: 15:11
 * To change this template use File | Settings | File Templates.
 */
public class TestSettings
{
    /// <summary>
    /// название теста
    /// </summary>
    @Attribute(name = "Name")
    public String Name;

    /// <summary>
    /// имя учителя
    /// </summary>
    @Attribute(name = "TeacherName")
    public String TeacherName;

    /// <summary>
    /// максимальная оценка
    /// </summary>
    @Attribute(name = "MaxMark")
    public int MaxMark;

    /// <summary>
    /// минимальная оценка
    /// </summary>
    @Attribute(name = "MinMark")
    public int MinMark;


    /// <summary>
    /// время на тест в минутах
    /// </summary>
    @Attribute(name = "MinuteTime")
    public int MinuteTime;

}
