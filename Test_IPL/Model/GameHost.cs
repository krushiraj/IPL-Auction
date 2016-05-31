using NetworkHelper;
using System;

namespace Test_IPL
{
        public class GameHost
        {
            public string Name { get; set; }
            public Guid Id { get; set; }
            public ICommunicationChannel CommChannel { get; set; }
        }
}
