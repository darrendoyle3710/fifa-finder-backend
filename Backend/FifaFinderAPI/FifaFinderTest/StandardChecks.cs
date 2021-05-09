using FifaFinderAPI.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FifaFinderAPI.Tests
{
    public class StandardChecks
    {
        [Fact]
        public void DBOperationsUp_Test()
        {
            InitDB initDB = new InitDB();
            int count = initDB.UpOperations.Count;
            initDB.UpOperations.ToString();
            Assert.IsType<int>(count);
        }
        [Fact]
        public void DBOperationsDown_Test()
        {
            InitDB initDB = new InitDB();
            int count = initDB.DownOperations.Count;
            initDB.DownOperations.ToString();
            Assert.IsType<int>(count);
        }

    }
}
