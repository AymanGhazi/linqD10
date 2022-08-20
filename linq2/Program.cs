
using D10;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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

        //Console.WriteLine(R01?.ProductName ?? "NA");
        #endregion

        #region Aggregate -Imidiate Execusion
        var Result3 = ProductList.Count();
         Result3 = ProductList.Count(x=>x.UnitsInStock==0);

       var  Result4 = ProductList.Max();
        var ResultMIN = ProductList.Min(p => p.ProductName.Length);
        var Rslt = (from p in ProductList
                   where p.ProductName.Length == ProductList.Min(p => p.ProductName.Length)
                   select p).FirstOrDefault();
        //Console.WriteLine(ProductList.Sum(p=>p.UnitsInStock));

        #endregion

        #region generators Operators
        // only way to call them is from enumerator operator
     var result5 = Enumerable.Range(0, 10); //IEnumerable of int 
     var R05 = Enumerable.Empty<Product>();
     var R06 = Enumerable.Repeat(3, 10);
       
     var R07 = Enumerable.Repeat(ProductList[2], 5);

        //foreach (var item in R07)
        //{
        //    Console.WriteLine(item);
        //}
        #endregion

        #region SelectMany
        //Output Sequence count > input seq count 
        List<string> NameList = new List<string>()
        {
            "Ahmed Ayman","Khalid Ayman","Ebrahim Mohamed"
        };
        var Result7 = NameList.SelectMany(p => p.Split(' ')).OrderByDescending(p=>p.Length);
        //Query Syntax using multiple from
        var Resukt9 = from p in NameList
                      from Sn in p.Split(' ')
                      orderby Sn.Length descending
                      select Sn;

        //foreach (var item in Resukt9)
        //{
        //    Console.WriteLine(item);
        //}
        #endregion

        #region casting Opertors Imidiate Execsuion
        List<Product> UnAvProd = ProductList.Where(p => p.UnitsInStock == 0).ToList();

        var parr = ProductList.Where(p => p.UnitPrice == 0)
                                  //.OrderBy(p => p.UnitPrice)
                                  //.ToArray();
                                  // .ToHashSet(); //Unique 
                                  //.ToDictionary(p=>p.ProductID,p=> new
                                  //{
                                  //    p.ProductID,
                                  //    p.ProductName
                                  //});   //Dic<long,Anonymous[long,string]>
                                  .ToLookup(p=>p.ProductID);
        //foreach (var item in parr)
        //{
        //    Console.WriteLine(item);
        //}

        #endregion


        #region Set Operatorss
        var List01 = Enumerable.Range(0, 10);
        var List1 = List01.ToList();
        //Console.WriteLine(List1[0]);
        //Console.WriteLine(List1[^1]);
        // List<int> Result11 = List1[1..10];

        var Seq02 = Enumerable.Range(50, 100); //50-149

        var Rslt1 = Seq02.Union(List01); //remove Duplicates
        var Rslt2 = Seq02.Concat(List01).Distinct();
        var Rslt3 = Seq02.Except(List01);
        var Rslt4 = Seq02.Intersect(List01);


        //foreach (var item in Rslt3)
        //{
        //    Console.Write(item);
        //}

        #endregion
        #region Quantifiers returns T or F
        //Console.WriteLine(ProductList.Any()); //returns True if the input is at least one element
        //Console.WriteLine(ProductList.Any(x=>x.UnitPrice>200)); //returns True if the input is at least one element matches the predicate
        //Console.WriteLine(ProductList.All(x => x.UnitPrice > 200)); //returns True if the inputs All match predicate 
        //Console.WriteLine(Rslt3.SequenceEqual(Rslt4)); //Get if two of the sequences matches each others

        #endregion
        #region Zip 
        //input 2 sequence and combine into one sequence
        var Lst02=Enumerable.Range(0, 10);
        var Rslt03 = NameList.Zip(Lst02,(FN,i)=> new {i,name= FN.ToUpper()});
        //foreach (var item in Rslt03)
        //{
        //    Console.WriteLine(item);
        //}
        #endregion
        #region Grouping
        //returns Groups not sequences

        var Rslt04 = from p in ProductList
                     where p.UnitsInStock > 0
                     group p by p.Category
                     into prdGroup where prdGroup.Count()>=10
                     orderby prdGroup.Count() descending
                     select new { Category=prdGroup.Key , productCount= prdGroup.Count() };


        var Rslt05 = ProductList.GroupBy(x => x.Category).Where(p => p.Count() >= 10)
                       .OrderByDescending(x => x.Count()).Select(p => new
                       {
                           Category = p.Key,
                           productCount = p.Count()
                       });

        //foreach (var itemG in Rslt05)
        //{
        //    Console.WriteLine($"Item Key {itemG.Category} and count = {itemG.productCount}");
        //    //foreach (var item in itemG)
        //    //{
        //    //    Console.WriteLine($"... {item.ProductName}");
        //    //}
        //}
        #endregion

        #region Let / Into introducing new range variable in Query Syntax
        List<string> Names = new List<string>()
        {
            "Ahmed", "Khalid","Ebrahim","Mohamed"
        };
        //aeiouAEIOU
        //regx is best used with string maipulations
        var Result08 = from N in Names
                       select Regex.Replace(N, "[aeiouAEIOU]", String.Empty)
                       //Restart Query with new range variable
                       into NOVowel
                       where NOVowel.Length >= 3
                       orderby NOVowel, NOVowel.Length descending
                       select NOVowel;
      // Let is  Using the old Range variable
      var Result09= from N in Names
                    let NOVowel = Regex.Replace(N , "[aeiouAEIOU]", String.Empty)
                    orderby NOVowel, NOVowel.Length descending
                    select NOVowel;

        foreach (var item in Result09)
        {
            Console.WriteLine(item);
        }
        #endregion
    }
}