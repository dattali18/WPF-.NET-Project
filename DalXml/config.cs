using DO;
using System.Xml.Linq;

namespace Dal;

public static class config
{

    private static int lastProductID = 100000; //int.Parse(((XElement)XElement.Load(@"..\xml\Products.xml").LastNode).Element("ID").Value);
    public static int LastProductID
    {
        set 
        {
            lastProductID = value;
        }
        get
        {
            lastProductID++;
            return lastProductID;
        }
    }


    private static int lastOrderID = 100000; //int.Parse(((XElement)XElement.Load(@"..\xml\Orders.xml").LastNode).Element("ID").Value);
    public static int LastOrederID
    {
        set {
            lastOrderID = value;
        }
        get
        {
            lastOrderID++;
            return lastOrderID;
        }
    }

    public  static void initData()
    {
        int[] productsID = new int[20];

        DalProduct products = new();
        productsID[0] = products.Create("Apple MacBook Pro", Categories.Laptop, 7000.0D, 10);
        productsID[1] = products.Create("Dell Vostro", Categories.Laptop, 5000.0D, 10);
        productsID[2] = products.Create("Lenovo Ideapad", Categories.Laptop, 3000.0D, 10);
        productsID[3] = products.Create("Apple Mac Pro", Categories.Desktop, 23000.0D, 20);
        productsID[4] = products.Create("Dell Precision", Categories.Desktop, 17000.0D, 20);
        productsID[5] = products.Create("Asus ROG Strix", Categories.Desktop, 16000.0D, 5);
        productsID[6] = products.Create("HP ZBook Fury", Categories.Desktop, 16500.0D, 5);
        productsID[7] = products.Create("Dell PowerEdge", Categories.Server, 9000.0D, 8);
        productsID[8] = products.Create("Lenovo ThinkSystem", Categories.Server, 7700.0D, 30);
        productsID[9] = products.Create("IPhone X", Categories.Phone, 2000.0D, 10);
        productsID[10] = products.Create("IPhone 11", Categories.Phone, 1500.0D, 12);
        productsID[11] = products.Create("IPhone 12", Categories.Phone, 2100.0D, 4);
        productsID[12] = products.Create("IPhone 13", Categories.Phone, 1990.0D, 5);
        productsID[13] = products.Create("Samsung Galaxy", Categories.Phone, 2200.0D, 5);
        productsID[14] = products.Create("Google Pixel", Categories.Phone, 1789.0D, 30);
        productsID[15] = products.Create("RealMe 12", Categories.Phone, 1000.0D, 12);
        productsID[16] = products.Create("AirPod Pro", Categories.Accessories, 200.0D, 30);
        productsID[17] = products.Create("AirPod Max", Categories.Accessories, 500.0D, 20);
        productsID[18] = products.Create("Mouse Pad", Categories.Accessories, 60.0D, 10);
        productsID[19] = products.Create("Magic Mouse", Categories.Accessories, 100.0D, 15);


        int[] orderID = new int[20];

        DalOrder orders = new();
        orderID[0] = orders.Create("Violette Strong", "VioletteStrong@gmail.com", "871 West Kingston Rd.Evans, GA 30809", new DateTime(2022, 11, 5), null, null);
        orderID[1] = orders.Create("Dorit Proulx", "DoritProulx@gmail.com", "593 Saxton Dr. Statesville, NC 28625", new DateTime(2022, 11, 5), new DateTime(2022, 11, 3), null);
        orderID[2] = orders.Create("Abraham Kendall", "AbrahamKendall@gmail.com", "95 Linden Street Morrisville, PA 19067", new DateTime(2022, 11, 5), null, null);
        orderID[3] = orders.Create("Bronwyn Boucher", "BronwynBoucher@gmail.com", "299 Amherst Street Chardon, OH 44024", new DateTime(2022, 11, 5), null, null);
        orderID[4] = orders.Create("Gilbert Jakab", "GilbertJakab@gmail.com", "8250 Tailwater Lane Chicopee, MA 01020", new DateTime(2022, 11, 5), new DateTime(2022, 11, 3), new DateTime(2022, 11, 9));
        orderID[5] = orders.Create("Henrik Admiraal", "HenrikAdmiraal@gmail.com", "9551 E. Leatherwood Rd. Ames, IA 50010", new DateTime(2022, 11, 5), null, null);
        orderID[6] = orders.Create("Anna Smola", "AnnaSmola@gmail.com", "7007 Carriage St. Lumberton, NC 28358", new DateTime(2022, 11, 5), null, null);
        orderID[7] = orders.Create("Courteney Christison", "CourteneyChristison@gmail.com", "7315 North Ave. Fishers, IN 46037", new DateTime(2022, 11, 5), null, null);
        orderID[8] = orders.Create("Adeline Blanchett", "AdelineBlanchett@gmail.com", "7418 Newcastle Rd. Auburn, NY 13021", new DateTime(2022, 11, 5), null, null);
        orderID[9] = orders.Create("Corrine Tailler", "CorrineTailler@gmail.com", "15 Pulaski Road Suwanee, GA 30024", new DateTime(2022, 11, 5), new DateTime(2022, 11, 3), null);
        orderID[10] = orders.Create("Daniele Michaelis", "DanieleMichaelis@gmail.com", "7418 Newcastle Rd.Auburn, NY 13021", new DateTime(2022, 11, 5), new DateTime(2022, 11, 3), new DateTime(2022, 11, 9));
        orderID[11] = orders.Create("Neville Thomas", "NevilleThomas@gmail.com", "823 Arnold StreetKernersville, NC 27284", new DateTime(2022, 11, 5), new DateTime(2022, 11, 3), new DateTime(2022, 11, 9));
        orderID[12] = orders.Create("Deborah Caulfield", "DeborahCaulfield@gmail.com", "345 Church StreetCentral Islip, NY 11722", new DateTime(2022, 11, 5), null, null);
        orderID[13] = orders.Create("DeniseWilton", "Denise Wilton@gmail.com", "9730 West William St. Kings Mountain, NC 28086", new DateTime(2022, 11, 5), new DateTime(2022, 11, 3), null);
        orderID[14] = orders.Create("DenverHoffman", "Denver Hoffman@gmail.com", "554 Brown Dr. New Hyde Park, NY 11040", new DateTime(2022, 11, 5), new DateTime(2022, 11, 3), null);
        orderID[15] = orders.Create("Scot Homewood", "ScotHomewood@gmail.com", "18 N. Harvey Circle Melbourne, FL 32904", new DateTime(2022, 11, 5), null, null);
        orderID[16] = orders.Create("Ilia Graves", "IliaGraves@gmail.com", "1 Smith Drive West Fargo, ND 58078", new DateTime(2022, 11, 5), null, null);
        orderID[17] = orders.Create("Margit Arthur", "MargitArthur@gmail.com", "882 Williams Ave. Indianapolis, IN 46201", new DateTime(2022, 11, 5), null, null);
        orderID[18] = orders.Create("Werner Alan", "WernerAlan@gmail.com", "487 North Avenue Lake Jackson, TX 77566", new DateTime(2022, 11, 5), new DateTime(2022, 11, 3), null);
        orderID[19] = orders.Create("Noga Tatum", "NogaTatum@gmail.com", "385 Snake Hill Dr. Little Rock, AR 72209", new DateTime(2022, 11, 5), null, null);

        Random rand = new(0);
        DalOrderItem orderItems = new();


        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[0]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[4]) ?? new(), orders.Get(item => item?.ID == orderID[0]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[10]) ?? new(), orders.Get(item => item?.ID == orderID[0]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[13]) ?? new(), orders.Get(item => item?.ID == orderID[0]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[8]) ?? new(), orders.Get(item => item?.ID == orderID[0]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[9]) ?? new(), orders.Get(item => item?.ID == orderID[0]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[19]) ?? new(), orders.Get(item => item?.ID == orderID[0]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[1]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[1]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[2]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[2]) ?? new(), orders.Get(item => item?.ID == orderID[2]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[0]) ?? new(), orders.Get(item => item?.ID == orderID[3]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[3]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[0]) ?? new(), orders.Get(item => item?.ID == orderID[4]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[4]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[5]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[5]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[6]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[2]) ?? new(), orders.Get(item => item?.ID == orderID[6]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[0]) ?? new(), orders.Get(item => item?.ID == orderID[7]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[7]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[0]) ?? new(), orders.Get(item => item?.ID == orderID[8]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[8]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[9]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[9]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[10]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[2]) ?? new(), orders.Get(item => item?.ID == orderID[10]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[0]) ?? new(), orders.Get(item => item?.ID == orderID[11]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[11]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[0]) ?? new(), orders.Get(item => item?.ID == orderID[12]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[12]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[13]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[13]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[14]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[2]) ?? new(), orders.Get(item => item?.ID == orderID[14]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[0]) ?? new(), orders.Get(item => item?.ID == orderID[15]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[15]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[16]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[2]) ?? new(), orders.Get(item => item?.ID == orderID[16]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[0]) ?? new(), orders.Get(item => item?.ID == orderID[17]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[17]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[0]) ?? new(), orders.Get(item => item?.ID == orderID[18]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[18]) ?? new(), rand.Next(0, 2));

        orderItems.Create(products.Get(item => item?.ID == productsID[3]) ?? new(), orders.Get(item => item?.ID == orderID[19]) ?? new(), rand.Next(0, 2));
        orderItems.Create(products.Get(item => item?.ID == productsID[5]) ?? new(), orders.Get(item => item?.ID == orderID[19]) ?? new(), rand.Next(0, 2));

    }
}
