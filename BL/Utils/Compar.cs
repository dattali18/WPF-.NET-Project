using BO;

namespace Utils;

public class OrderComparer : IComparer<BO.Order?>
{
    public int Compare(Order? x, Order? y)
    {
        //if(x?.Status == OrderStatus.OrderPlaced && y?.Status == OrderStatus.OrderPlaced)
        //{
        //    if(x?.OrderPlaced == y?.OrderPlaced) return 0;
        //    else if(x?.OrderPlaced > y?.OrderPlaced) return -1;
        //    else return 1;
        //} else if(x?.Status == OrderStatus.Shipped && y?.Status == OrderStatus.Shipped)
        //{
        //    if (x?.OrderPlaced == y?.OrderPlaced) return 0;
        //    else if (x?.OrderPlaced > y?.OrderPlaced) return -1;
        //    else return 1;
        //} else
        //{
        //    if (x?.Status == OrderStatus.OrderPlaced) return -1;
        //    else if (y?.Status == OrderStatus.OrderPlaced) return 1;
        //    else return 0;
        //}
        if (x?.Status == y?.Status)
        {
            if (x?.OrderPlaced == y?.OrderPlaced) return 0;
            else if (x?.OrderPlaced > y?.OrderPlaced) return 1;
            else return -1;
        }
        else if (x?.Status > y?.Status)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
