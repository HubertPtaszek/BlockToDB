using BlockToDB.Dictionaries;
using System;
using System.Collections.Generic;

namespace BlockToDB.Infrastructure
{
    public class MainContext
    {
        public int PersonId { get; set; }

        public bool IsAuthenticated { get; set; }

        private string _id;
        public MainContext()
        {
            _id = Guid.NewGuid().ToString();
        }
    }
}
