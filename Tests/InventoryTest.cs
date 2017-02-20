using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Inventory
{
  public class InventoryTest : IDisposable
  {
    public InventoryTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=inventory_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Collection.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Collection.GetAll().Count;
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfAllPropertiesAreTheSame()
    {
      Collection firstCollection = new Collection("Epicodus", "C# Class Students");
      Collection secondCollection = new Collection("Epicodus", "C# Class Students");

      Assert.Equal(firstCollection,secondCollection);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      Collection testCollection = new Collection("Epicodus", "C# Class Students");

      testCollection.Save();
      List<Collection> result = Collection.GetAll();
      List<Collection> testList = new List<Collection>{testCollection};
      Console.WriteLine(result[0].GetPlaceTaken());
      Console.WriteLine(testList[0].GetPlaceTaken());
      Assert.Equal(testList, result);
    }
  }
}
