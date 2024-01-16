﻿using Autofac;
using EQTool.Models;
using EQTool.Services;
using EQTool.ViewModels;
using EQToolShared.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EQToolTests
{
    [TestClass]
    public class ChChainTests
    {
        private readonly IContainer container;
        public ChChainTests()
        {
            container = DI.Init();
        }

        [TestMethod]
        public void Parse1()
        {
            var service = container.Resolve<ChParser>();
            var d = service.ChCheck("Curaja shouts, 'GG 014 CH -- Wreckognize'");
            Assert.AreEqual(d.Recipient, "Wreckognize");
            Assert.AreEqual(d.Caster, "Curaja");
            Assert.AreEqual(d.Position, 14);
            Assert.AreEqual(d.RecipientGuild, "GG");
        }

        [TestMethod]
        public void Parse2()
        {
            var service = container.Resolve<ChParser>();
            var d = service.ChCheck("Hanbox shouts, 'GG 001 CH -- Beefwich'");
            Assert.AreEqual(d.Recipient, "Beefwich");
            Assert.AreEqual(d.Caster, "Hanbox");
            Assert.AreEqual(d.Position, 1);
            Assert.AreEqual(d.RecipientGuild, "GG");
        }

        [TestMethod]
        public void Parse4()
        {
            var service = container.Resolve<ChParser>();
            var d = service.ChCheck("Hanbox shouts, 'GG 001 CH --Beefwich'");
            Assert.AreEqual(d.Recipient, "Beefwich");
            Assert.AreEqual(d.Caster, "Hanbox");
            Assert.AreEqual(d.Position, 1);
            Assert.AreEqual(d.RecipientGuild, "GG");
        }

        [TestMethod]
        public void Parse3()
        {
            var service = container.Resolve<ChParser>();
            var d = service.ChCheck("Vaeric tells the guild, 'Currently signed up as 001 in CH chain'");
            Assert.IsNull(d);
        }

        [TestMethod]
        public void Parse5()
        {
            var service = container.Resolve<ChParser>();
            var d = service.ChCheck("Wartburg says out of character, 'CA 004 CH -- Sam'");
            Assert.AreEqual(d.Recipient, "Sam");
            Assert.AreEqual(d.Caster, "Wartburg");
            Assert.AreEqual(d.Position, 4);
            Assert.AreEqual(d.RecipientGuild, "CA");
        }

        [TestMethod]
        public void Parse51()
        {
            var service = container.Resolve<ChParser>();
            var d = service.ChCheck("Wartburg says out of character, '004 CH - Sam'");
            Assert.AreEqual(d.Recipient, "Sam");
            Assert.AreEqual(d.Caster, "Wartburg");
            Assert.AreEqual(d.Position, 4);
        }

        [TestMethod]
        public void Parse6()
        {
            var service = container.Resolve<ChParser>();
            var d = service.ChCheck("Hanbox tells the guild, 'GG 001 CH --Beefwich'");
            Assert.AreEqual(d.Recipient, "Beefwich");
            Assert.AreEqual(d.Caster, "Hanbox");
            Assert.AreEqual(d.Position, 1);
            Assert.AreEqual(d.RecipientGuild, "GG");
        }

        [TestMethod]
        public void Parse7()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric,
                ChChainTagOverlay = "GG"
            };
            var d = service.ChCheck("Hanbox tells the guild, 'GG 001 CH --Beefwich'");

            Assert.AreEqual(d.Recipient, "Beefwich");
            Assert.AreEqual(d.Caster, "Hanbox");
            Assert.AreEqual(d.Position, 1);
            Assert.AreEqual(d.RecipientGuild, "GG");
        }

        [TestMethod]
        public void Parse8()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric,
                ChChainTagOverlay = "GG"
            };
            var d = service.ChCheck("Hanbox tells the guild, 'CA 001 CH --Beefwich'");
            Assert.IsNull(d);
        }

        [TestMethod]
        public void Parse9()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric,
                ChChainTagOverlay = "GGG"
            };
            var d = service.ChCheck("Hanbox tells the raid, 'GGG 001 CH --Beefwich'");

            Assert.AreEqual(d.Recipient, "Beefwich");
            Assert.AreEqual(d.Caster, "Hanbox");
            Assert.AreEqual(d.Position, 1);
            Assert.AreEqual(d.RecipientGuild, "GGG");
        }

        [TestMethod]
        public void Parse10()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric,
                ChChainTagOverlay = "GGG"
            };
            var d = service.ChCheck("Amberel tells the raid,  'GGG CH - Asirk - 10 s'");
            Assert.IsNull(d);
        }

        [TestMethod]
        public void Parse11()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric,
                ChChainTagOverlay = "GGG"
            };
            var d = service.ChCheck("Windarie tells the group, 'Bufzyn 111 --- CH on << Tinialita  >> --- 111'");
            Assert.IsNull(d);
        }

        [TestMethod]
        public void Parse12()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric,
                ChChainTagOverlay = "CA"
            };
            var d = service.ChCheck("You say out of character, 'CA 002 CH -- Aaryk'");
            Assert.AreEqual(d.Recipient, "Aaryk");
            Assert.AreEqual(d.Caster, "You");
            Assert.AreEqual(d.Position, 2);
            Assert.AreEqual(d.RecipientGuild, "CA");
        }

        [TestMethod]
        public void Parse13()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric
            };
            var d = service.ChCheck("Kaboomslang -> Distributin: ch plz");
            Assert.IsNull(d);
        }

        [TestMethod]
        public void Parse14()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric
            };
            var d = service.ChCheck("You told someone, 'when CH chains are e a 1-2 full rounds of max dmg hits though if u can'");
            Assert.IsNull(d);
        }

        [TestMethod]
        public void Parse15()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric
            };
            var d = service.ChCheck("somecleric tells the guild, '003 - CH 5T'");
            Assert.IsNull(d);
        }

        [TestMethod]
        public void Parse16()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric
            };
            var d = service.ChCheck("Windarie auctions, '111 --- CH << Mandair  >> --- 111'");
            Assert.AreEqual(d.Recipient, "Mandair");
            Assert.AreEqual(d.Caster, "Windarie");
            Assert.AreEqual(d.Position, 111);
        }

        [TestMethod]
        public void Parse17()
        {
            var service = container.Resolve<ChParser>();
            var player = container.Resolve<ActivePlayer>();
            player.Player = new PlayerInfo
            {
                Level = 54,
                PlayerClass = PlayerClasses.Cleric
            };
            var d = service.ChCheck("Mutao auctions, '777 CH <>> Mandair <<> 777'");
            Assert.AreEqual(d.Recipient, "Mandair");
            Assert.AreEqual(d.Caster, "Mutao");
            Assert.AreEqual(d.Position, 777);
        }
    }
}
