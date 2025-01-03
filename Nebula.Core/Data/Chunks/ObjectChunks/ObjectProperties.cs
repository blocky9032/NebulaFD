﻿using Nebula.Core.Memory;
using Nebula.Core.Utilities;
using ObjCommon = Nebula.Core.Data.Chunks.ObjectChunks.ObjectCommon.ObjectCommon;

namespace Nebula.Core.Data.Chunks.ObjectChunks
{
    public class ObjectProperties : Chunk
    {
        public ObjectProperties()
        {
            ChunkName = "ObjectProperties";
            ChunkID = 0x2256;
        }

        public override void ReadCCN(ByteReader reader, params object[] extraInfo)
        {
            if (Parameters.DontIncludeObjects)
                return;
            int curHandle = 0;
            FrameItems listItems = NebulaCore.PackageData.FrameItems;
            reader.Skip(4);
            while (reader.Size() > reader.Tell())
            {
                ByteReader propertyReader = new ByteReader(Decompressor.DecompressBlock(reader.ReadBytes(reader.ReadInt())));
                new ObjCommon().ReadCCN(propertyReader, listItems.Items[curHandle++]);
                propertyReader.Close();
                reader.Skip(4);
            }
        }

        public override void ReadMFA(ByteReader reader, params object[] extraInfo)
        {

        }

        public override void WriteCCN(ByteWriter writer, params object[] extraInfo)
        {

        }

        public override void WriteMFA(ByteWriter writer, params object[] extraInfo)
        {

        }
    }
}
