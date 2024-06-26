﻿using Nebula.Core.Memory;

namespace Nebula.Core.Data.Chunks.FrameChunks.Events.Parameters
{
    public class ParameterClick : ParameterChunk
    {
        public byte Button;
        public byte IsDouble;

        public ParameterClick()
        {
            ChunkName = "ParameterClick";
        }

        public override void ReadCCN(ByteReader reader, params object[] extraInfo)
        {
            Button = reader.ReadByte();
            IsDouble = reader.ReadByte();
        }

        public override void WriteMFA(ByteWriter writer, params object[] extraInfo)
        {
            writer.WriteByte(Button);
            writer.WriteByte(IsDouble);
        }

        public override string ToString()
        {
            return "Click " + Button + ", " + IsDouble;
        }
    }
}
