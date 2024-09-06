using System;
using System.Collections.Generic;

namespace DeveloperSample.ClassRefactoring
{
    public enum SwallowType
    {
        African, European
    }

    public enum SwallowLoad
    {
        None, Coconut
    }

    public class SwallowFactory
    {
        public static Swallow GetSwallow(SwallowType swallowType) => new(swallowType);
    }

    public class Swallow
    {
        public SwallowType Type { get; }
        public SwallowLoad Load { get; private set; }

        private static readonly Dictionary<(SwallowType, SwallowLoad), double> AirspeedVelocities = new()
        {
            {(SwallowType.African, SwallowLoad.None), 22},
            {(SwallowType.African, SwallowLoad.Coconut), 18},
            {(SwallowType.European, SwallowLoad.None), 20},
            {(SwallowType.European, SwallowLoad.Coconut), 16}
        };

        public Swallow(SwallowType swallowType)
        {
            Type = swallowType;
        }

        public void ApplyLoad(SwallowLoad load)
        {
            Load = load;
        }

        public double GetAirspeedVelocity()
        {
           if(AirspeedVelocities.TryGetValue((Type,Load),out var velocity))
           {
                return velocity;
           }
            throw new InvalidOperationException();
        }
    }
}