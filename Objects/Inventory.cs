using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Inventory
{
  public class Collection
  {
    private int _id;
    private string _place_taken;
    private string _caption;

    public Collection (string Place_Taken, string Caption, int Id = 0)
    {
      _id = Id;
      _place_taken = Place_Taken;
      _caption = Caption;
    }

    public override bool Equals(System.Object otherCollection)
    {
      if (!(otherCollection is Collection))
      {
        return false;
      }
      else
      {
        Collection newCollection = (Collection) otherCollection;
        bool idEquality = (this.GetId() == newCollection.GetId());
        bool placeTakenEquality = (this.GetPlaceTaken() == newCollection.GetPlaceTaken());
        bool captionEquality =(this.GetCaption() == newCollection.GetCaption());
        return (idEquality && placeTakenEquality && captionEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetPlaceTaken()
    {
      return _place_taken;
    }

    public void SetPlaceTaken(string newPlaceTaken)
    {
      _place_taken = newPlaceTaken;
    }

    public string GetCaption()
    {
      return _caption;
    }

    public void SetCaption(string newCaption)
    {
      _caption = newCaption;
    }

    public static List<Collection> GetAll()
    {
      List<Collection> allCollections = new List<Collection>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM photos;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int photoId = rdr.GetInt32(0);
        string photoPlaceTaken = rdr.GetString(1);
        string photoCaption = rdr.GetString(2);

        Collection newCollection = new Collection(photoPlaceTaken, photoCaption, photoId);
        allCollections.Add(newCollection);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allCollections;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM photos;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO photos (place_taken, caption) OUTPUT INSERTED.id VALUES (@place_taken, @caption);", conn);

      SqlParameter placeTakenParameter = new SqlParameter();
      placeTakenParameter.ParameterName = "@place_taken";
      placeTakenParameter.Value = this.GetPlaceTaken();
      SqlParameter captionParameter = new SqlParameter();
      captionParameter.ParameterName = "@caption";
      captionParameter.Value = this.GetCaption();
      cmd.Parameters.Add(placeTakenParameter);
      cmd.Parameters.Add(captionParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
