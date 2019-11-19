using NUnit.Framework;
using static GoogleResponse;
using Assignment3Lib;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestHaveWeMet()
        {
            location l = new location { longitudeE7 = 1, latitudeE7 = 5, timestampMs = 500 };
            location k = new location { longitudeE7 = 1, latitudeE7 = 5, timestampMs = 500 };
            location[] ll = { l };
            location[] kk = { k };

            GoogleResponse g = new GoogleResponse { locations = ll };
            GoogleResponse gg = new GoogleResponse { locations = kk };
            Assert.True(Assignment3Lib.GoogleMethods.haveWeMet(g, gg));
        }

        [Test]
        public void TestAlibi()
        {
            location l = new location { longitudeE7 = 1, latitudeE7 = 5, timestampMs = 500 };
            location[] ll = { l };
            GoogleResponse g = new GoogleResponse { locations = ll };
            Assert.NotNull(Assignment3Lib.GoogleMethods.checkAlibi(g, UnixTimeStampToDateTime(500)));
        }

    }
}