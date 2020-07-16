using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AliaksNad.Battleship.Data.Contexts;
using AliaksNad.Battleship.Data.Models;
using AliaksNad.Battleship.Logic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AliaksNad.Battleship.Logic.Test
{
    [TestClass]
    public class GameServiceTest
    {
        [TestMethod]
        public void Test_Success_Add_New_BattleArea()
        {
            var source = new List<BattleAreaDb> { new BattleAreaDb() { }, new BattleAreaDb() { }, new BattleAreaDb() { } }.AsQueryable();

            var dbSet = new Mock<DbSet<BattleAreaDb>>();
            dbSet.As<IQueryable<BattleAreaDb>>().Setup(s => s.Provider).Returns(source.Provider);
            dbSet.As<IQueryable<BattleAreaDb>>().Setup(s => s.ElementType).Returns(source.ElementType);
            dbSet.As<IQueryable<BattleAreaDb>>().Setup(s => s.GetEnumerator()).Returns(source.GetEnumerator());
            dbSet.As<IQueryable<BattleAreaDb>>().Setup(s => s.Expression).Returns(source.Expression);

            var contextMock = new Mock<BattleAreaContext>();
            contextMock.Setup(x => x.BattleAreas).Returns(dbSet.Object);

            contextMock.Verify(s => s.SaveChanges(), Times.Once);
            dbSet.Verify(s => s.Add(It.IsAny<BattleAreaDb>()), Times.Once);
        }
    }
}
