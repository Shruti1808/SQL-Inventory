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
  }
}
