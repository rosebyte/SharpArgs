﻿using RoseByte.SharpArgs.Internal;

namespace SharpArgs.Tests.TestObjects
{
    public class DummyStringArgument : Argument<string>
    {
        public DummyStringArgument(string name) : base(name, default, string.Empty, default) { }

    }
}