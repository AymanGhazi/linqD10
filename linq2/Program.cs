
using D10;
using static D10.ListGenerators;
public class Program
{
    private static void Main(string[] args)
    {
        //Where is Filteration
        //select is transformation
        #region Where
        var result = ProductList.Where(product => product.UnitsInStock == 0);
            result=from p in ProductList where p.UnitsInStock == 0 select p;
            result = ProductList.Where(p => p.UnitsInStock == 0 && p.Category == "Meat/Poultry");
      //indexed where valid in fluent syntax Not Query expression
             result = result.Where((p, i) => p.UnitsInStock == 0 && i <= 5);
        var UinS= ProductList.Select(p => p.UnitsInStock);
        var Category = from p in ProductList select p.Category;
        //AnnonymousDataTypes
        var NewResult = ProductList.Where(p => p.UnitsInStock == 0).Select(p => new
        {
            id = p.ProductID,
            p.ProductName,
        });
        var NewResult2 = from p in ProductList where p.UnitsInStock==0  select new
        {
            id = p.ProductID,
            p.Category,
            p.UnitPrice
            //introduce new range variable to contain the Query
        } into taxedProduct where taxedProduct.UnitPrice> 32
                         select taxedProduct;
        var NewResult3 = from p in NewResult2 where p.UnitPrice > 20 select p;

        //foreach (var item in NewResult2)
        //{
        //    Console.WriteLine(item);
        //}

        #endregion

        #region Ordering Elements
        var Result = ProductList.OrderByDescending(p => p.UnitsInStock).Select(p=> new
        {
            p.ProductName,
            p.UnitsInStock,
            p.UnitPrice
        });
        Result = from p in ProductList
                 orderby p.UnitsInStock descending ,p.UnitPrice
                 select new
                 {
                     p.ProductName,
                     p.UnitsInStock,
                     p.UnitPrice
                 };

         Result = ProductList.OrderByDescending(p => p.UnitsInStock).ThenBy(p=>p.UnitPrice).Select(p => new
        {
            p.ProductName,
            p.UnitsInStock,
            p.UnitPrice
        });


        //foreach (var item in Result)
        //{
        //    Console.WriteLine(item);
        //}
        #endregion
        #region Element Operators -- Imiediate Execusion
        List<Product> DDproductLists = new List<Product>(); //Empty Sequence
        var R01 = ProductList.First();
        R01 = ProductList.Last(p=>p.UnitsInStock==0);
        R01 = DDproductLists.FirstOrDefault(p=>p.UnitPrice>5); //Default(Product)

        R01 = ProductList?.ElementAtOrDefault(170);
        //throws exeption if there is more than one element even there is OrDefault
        R01 = ProductList?.SingleOrDefault(p=>p.ProductID==1);

        //Hyprid Syntax (Query).Fluent
        var R = (from p in ProductList
                where p.UnitsInStock == 0
                select new
                {
                    p.ProductName,
                    p.Category
                }).FirstOrDefault();

        Console.WriteLine(R01?.ProductName ?? "NA");
        #endregion

    }
}