﻿using Autofac;
using EQTool.Services.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EQToolTests
{
    [TestClass]
    public class EnrageTests
    {
        private readonly IContainer container;
        public EnrageTests()
        {
            container = DI.Init();
        }

        [TestMethod]
        public void Parse1()
        {
            var service = container.Resolve<EnrageParser>();
            var d = service.EnrageCheck("Cekenar has become ENRAGED.", DateTime.Now);
            Assert.AreEqual(d.NpcName, "Cekenar");
        }
    }
}
