using MapAssist.Helpers;
using ResurrectedTrade.AgentBase.Memory;
using System;

namespace MapAssist.Integrations.ResurrectedTrade
{
    public class ResolvablePattern : Pattern
    {
        private readonly int _operandOffset;

        public ResolvablePattern(string pattern, int operandOffset) : base(pattern)
        {
            _operandOffset = operandOffset;
        }

        public Ptr Resolve(byte[] buffer, Ptr patternOffset)
        {
            var instructionEnd = patternOffset + _operandOffset + 4;
            var relativePosition = BitConverter.ToUInt32(buffer, patternOffset + _operandOffset);
            return instructionEnd + relativePosition;
        }
    }
}
