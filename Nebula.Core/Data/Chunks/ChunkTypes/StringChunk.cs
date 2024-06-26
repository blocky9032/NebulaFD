﻿using Nebula.Core.Memory;

namespace Nebula.Core.Data.Chunks.ChunkTypes
{
    public abstract class StringChunk : Chunk
    {
        public string Value = string.Empty;

        public override void ReadCCN(ByteReader reader, params object[] extraInfo)
        {
            Value = reader.ReadYuniversal();
        }

        public override void ReadMFA(ByteReader reader, params object[] extraInfo)
        {

        }

        public override void WriteCCN(ByteWriter writer, params object[] extraInfo)
        {
            writer.WriteYunicode(Value);
        }

        public override void WriteMFA(ByteWriter writer, params object[] extraInfo)
        {

        }
    }
}
