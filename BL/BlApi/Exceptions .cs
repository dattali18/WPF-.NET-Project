namespace BlApi;

[Serializable()]
public class OutOfReachIDException : Exception
{
    protected OutOfReachIDException() : base() {}

    public OutOfReachIDException(string msg) : base(msg) {}

    public OutOfReachIDException(string message, Exception innerException) : base(message, innerException) {}
}


[Serializable()]
public class NegativePriceException : Exception
{
    protected NegativePriceException() : base() {}

    public NegativePriceException(string msg) : base(msg) {}

    public NegativePriceException(string message, Exception innerException) : base(message, innerException) {}
}

[Serializable()]
public class NegativeAmountException : Exception
{
    protected NegativeAmountException() : base() { }

    public NegativeAmountException(string msg) : base(msg) { }

    public NegativeAmountException(string message, Exception innerException) : base(message, innerException) { }
}

[Serializable()]
public class DeleteProductFromOrderException : Exception
{
    protected DeleteProductFromOrderException() : base() { }
    
    public DeleteProductFromOrderException(string msg) : base(msg) { }

    public DeleteProductFromOrderException(string msg, Exception innerException) : base(msg, innerException) { }

}

[Serializable()]
public class OutOfStockException : Exception
{
    protected OutOfStockException() : base() { }

    public OutOfStockException(string msg) : base(msg) { }

    public OutOfStockException(string message, Exception innerException) : base(message, innerException) { }
}

[Serializable()]
public class EmailNotValidException : Exception
{
    protected EmailNotValidException() : base() { }

    public EmailNotValidException(string msg) : base(msg) { }

    public EmailNotValidException(string message, Exception innerException) : base(message, innerException) { }
}

[Serializable()]
public class NotAbleToDeleteException : Exception
{
    protected NotAbleToDeleteException() : base() { }

    public NotAbleToDeleteException(string msg) : base(msg) { }

    public NotAbleToDeleteException(string message, Exception innerException) : base(message, innerException) { }
}

[Serializable()]
public class AuthenticationFailException : Exception
{
    protected AuthenticationFailException() : base() { }

    public AuthenticationFailException(string msg) : base(msg) { }

    public AuthenticationFailException(string message, Exception innerException) : base(message, innerException) { }
}