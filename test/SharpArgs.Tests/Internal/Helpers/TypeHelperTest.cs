using System;
using System.Collections.Generic;
using RoseByte.SharpArgs.Internal.Helpers;
using RoseByte.SharpArgs.Tests.TestObjects;
using Xunit;

namespace RoseByte.SharpArgs.Tests.Internal.Helpers
{
    public class TypeHelperTest
    {
        [Fact]
        public void ShouldCreateInstance()
        {
            var sut = new TypeHelper<ISecondRoute>(
                new List<Type>{typeof(WithRouteAttribute), typeof(SecondRoute)});
            
            Assert.Equal(typeof(WithRouteAttribute), sut.Types["first"]);
            Assert.Equal(typeof(WithRouteAttribute), sut.Types["second"]);
            Assert.Equal(typeof(SecondRoute), sut.Types["secondroute"]);
        }
    }
}