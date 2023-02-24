using System;



public class PagedList<T> : List<T>
{
    public PagedList(MetaData metaData)
    {
        MetaData = metaData;
    }

    public MetaData MetaData { get; set; }
}
