using Nancy;
using System;
using System.Collections.Generic;
using Inventory;

namespace InventoryModule
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => View["index.cshtml"];
      Post["/new_photo"] = _ => {
        string locationInput = Request.Form["location-taken"];
        string captionInput = Request.Form["caption"];
        Collection newCollection = new Collection(locationInput, captionInput);
        newCollection.Save();
        Dictionary<string, string> Model = new Dictionary<string, string>(){};
        Model.Add("placeTaken", locationInput);
        Model.Add("caption", captionInput);
        return View["new_photo.cshtml", Model];

      };
      Get["/all_photo"]= _=>{
        List<Collection> output = Collection.GetAll();
        return View["all_photo.cshtml", output];
      };
      Get["/cleared"]= _ => {
        Collection.DeleteAll();
        return View["cleared.cshtml"];
      };
    }
  }
}
