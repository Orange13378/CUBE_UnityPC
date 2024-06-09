using System.Collections.Generic;
using UnityEngine;

namespace CubeMVC
{
    public class ChestModel
    {
        public List<ChestItem> Items = new();
        public GameObject CurrentOpenedItem;
        public GameObject[] Chests;
    }
}