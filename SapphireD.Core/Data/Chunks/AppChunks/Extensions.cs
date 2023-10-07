﻿using SapphireD.Core.Memory;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace SapphireD.Core.Data.Chunks.AppChunks
{
    public class Extensions : Chunk
    {
        public Extension[] Exts; // Color Mode
        public ushort Conditions;

        public Extensions()
        {
            ChunkName = "Extensions";
            ChunkID = 0x2234;
        }

        public override void ReadCCN(ByteReader reader)
        {
            int ExtensionCount = reader.ReadUShort();
            Conditions = reader.ReadUShort();

            Exts = new Extension[reader.ReadUShort()];

            for (int i = 0; i < ExtensionCount; i++)
            {
                Extension ext = new Extension();
                ext.ReadCCN(reader);
                Exts[i] = ext;
            }

            SapDCore.PackageData.Extensions = this;
        }

        public override void ReadMFA(ByteReader reader)
        {

        }

        public override void WriteCCN(ByteWriter writer)
        {

        }

        public override void WriteMFA(ByteWriter writer)
        {

        }
    }
}