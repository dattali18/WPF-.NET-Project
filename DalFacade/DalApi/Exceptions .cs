namespace DalApi;

[Serializable()]
public class ObjectNotFoundException: Exception
{
    protected ObjectNotFoundException() : base() { }
    public ObjectNotFoundException(string message) : base(message) {}
    public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}

[Serializable()]
public class DuplicateIDException : Exception
{
    protected DuplicateIDException() : base() { }
    public DuplicateIDException (string message) : base(message) { }
    public DuplicateIDException(string message, Exception innerException) : base(message, innerException) { }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
