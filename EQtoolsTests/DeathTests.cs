﻿using Autofac;
using EQTool.Services.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EQToolTests
{
    [TestClass]
    public class DeathTests
    {
        private readonly IContainer container;
        public DeathTests()
        {
            container = DI.Init();
        }

        [TestMethod]
        // has the player died
        public void TestMethod1()
        {
            DateTime timestamp = new DateTime();

            var service = container.Resolve<DeathParser>();
            var rv = service.ParseDeath("Just some random line", timestamp);
            Assert.AreEqual(false, rv);
        }

        [TestMethod]
        // has the player died
        public void TestMethod2()
        {
            DateTime timestamp = new DateTime();

            var service = container.Resolve<DeathParser>();
            var rv = service.ParseDeath("You have been slain", timestamp);
            Assert.AreEqual(true, rv);
        }

        [TestMethod]
        // player has died multiple times
        public void TestMethod3()
        {
            DateTime timestamp = new DateTime();

            var service = container.Resolve<DeathParser>();
            service.ParseDeath("You have been slain", timestamp);
            service.ParseDeath("You have been slain", timestamp.AddSeconds(40.0));
            service.ParseDeath("You have been slain", timestamp.AddSeconds(80.0));
            service.ParseDeath("You have been slain", timestamp.AddSeconds(80.0));

            var count = service.DeathLoopResponse();
            Assert.AreEqual(4, count);
        }

        [TestMethod]
        // player has died multiple times, but by the time the last death occurs, the first death has scrolled off the tracking list
        public void TestMethod4()
        {
            DateTime timestamp = new DateTime();

            var service = container.Resolve<DeathParser>();
            service.ParseDeath("You have been slain", timestamp);
            service.ParseDeath("You have been slain", timestamp.AddSeconds(40.0));
            service.ParseDeath("You have been slain", timestamp.AddSeconds(130.0));

            var count = service.DeathLoopResponse();
            Assert.AreEqual(2, count);
        }


        [TestMethod]
        // player has died multiple times, but then shows sign of life, so the death tracking list is purged
        // melee
        public void TestMethod5()
        {
            DateTime timestamp = new DateTime();

            var service = container.Resolve<DeathParser>();
            service.ParseDeath("You have been slain", timestamp);
            service.ParseDeath("You have been slain", timestamp);
            service.ParseDeath("You have been slain", timestamp);

            service.ParseSignOfLife("You slice Soandso for 100 points of damage");
            var count = service.DeathLoopResponse();
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        // player has died multiple times, but then shows sign of life, so the death tracking list is purged
        // casting
        public void TestMethod6()
        {
            DateTime timestamp = new DateTime();

            var service = container.Resolve<DeathParser>();
            service.ParseDeath("You have been slain", timestamp);
            service.ParseDeath("You have been slain", timestamp);
            service.ParseDeath("You have been slain", timestamp);

            service.ParseSignOfLife("You begin casting");
            var count = service.DeathLoopResponse();
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        // player has died multiple times, but then shows sign of life, so the death tracking list is purged
        // communicating
        public void TestMethod7()
        {
            DateTime timestamp = new DateTime();

            var service = container.Resolve<DeathParser>();
            service.ParseDeath("You have been slain", timestamp);
            service.ParseDeath("You have been slain", timestamp);
            service.ParseDeath("You have been slain", timestamp);

            service.ParseSignOfLife("You shout, something something");
            var count = service.DeathLoopResponse();
            Assert.AreEqual(0, count);
        }

    }
}
