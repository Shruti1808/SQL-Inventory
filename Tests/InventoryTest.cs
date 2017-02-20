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
    //Save
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      Collection testCollection = new Collection("Epicodus", "C# Class Students");

      testCollection.Save();
      List<Collection> result = Collection.GetAll();
      List<Collection> testList = new List<Collection>{testCollection};
      Assert.Equal(testList, result);
    }

 //Check if correct Id is assigned to the object
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Collection testCollection = new Collection("Epicodus", "C# Class Students");

      testCollection.Save();
      Collection savedCollection = Collection.GetAll()[0];

      int result = savedCollection.GetId();
      int testId = testCollection.GetId();

      Assert.Equal(testId, result);
    }
     //Find
    [Fact]
    public void Test_Find_FindcollectionInDatabase()
    {
      Collection testCollection = new Collection("Epicodus", "C# Class Students");
      testCollection.Save();
      Collection foundCollection = Collection.Find(testCollection.GetId());
      Assert.Equal(testCollection,foundCollection);

    }
  }
}
