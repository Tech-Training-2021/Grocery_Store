﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace NatureFresh
{
    class ItemDetails
    {
        //File path for ItemDetails.json. Modify the path according to the placement of json file.
        private string detailsJson = @"..//..//ItemDetails.json";

        //Get a Specific item(To be upgraded to constructore)
        private void _GetItem()
        {
            var json = File.ReadAllText(detailsJson);
            Console.WriteLine("\nEnter the item you want to find");
            var itemInput = Console.ReadLine().ToLower();
            var JsonObject = JObject.Parse(json);

            if (JsonObject[itemInput] != null)
            {
                var item = JsonObject[itemInput];
                Console.WriteLine("\nID: " + item["id"]);
                Console.WriteLine("Name: " + itemInput.ToUpper());
                Console.WriteLine("Price: " + item["price"]);
                Console.WriteLine("Quantity: " + item["quantity"]);
                Console.WriteLine("Weight: " + item["weight"]);
                Console.WriteLine("Unit: " + item["unit"]);
            }
            else
            {
                Console.WriteLine("\nThe item does not exist in the menu yet!");
            }
        }

        //Get All the items available in the Json/Database
        private void _GetAllItems()
        {
            var json = File.ReadAllText(detailsJson);
            var JsonObject = JObject.Parse(json);
            
            foreach (var item in JsonObject)
            {
                Console.WriteLine("\n" + item.Key.ToUpper());
                Console.WriteLine("-Price: " + item.Value["price"]);
                Console.WriteLine("-Quantity: " + item.Value["quantity"]);
                Console.WriteLine("-Weight: " + item.Value["weight"]);
                Console.WriteLine("-Unit: " + item.Value["unit"]);
            }
        }

        private void _UpdateItemStock(string InputValue, string newValue)
        {
            InputValue = InputValue.ToLower();
            var json = File.ReadAllText(detailsJson);
            var JsonObject = JObject.Parse(json);

            foreach (var item in JsonObject)
            {
                if(item.Key == InputValue)
                {
                    try
                    {
                        item.Value["quantity"] = newValue;
                        string output = JsonConvert.SerializeObject(JsonObject, Formatting.Indented);
                        File.WriteAllText(detailsJson, output);
                        return; // Stop the function
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("\nError: " + e);
                        break;
                    }
                }
            }
            Console.WriteLine("\nItem not in inventory!");
            
        }

        //public methods calling the private ones
        public void GetItem()
        {
            _GetItem();
        }

        public void GetAllItems()
        {
            _GetAllItems();
        }

        public void UpdateItemStock(string InputValue, string newValue)
        {
            _UpdateItemStock(InputValue, newValue);
        }

        public int id { get; set; }
        public int price { get; set; }
        public int stock { get; set; }


    }
}
