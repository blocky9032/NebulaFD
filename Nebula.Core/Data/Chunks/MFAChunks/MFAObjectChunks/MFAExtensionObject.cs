﻿using Nebula.Core.Memory;

namespace Nebula.Core.Data.Chunks.MFAChunks.MFAObjectChunks
{
    public class MFAExtensionObject : MFAActive
    {
        public int Type;
        public string Name = string.Empty;
        public string FileName = string.Empty;
        public uint Magic;
        public string SubType = string.Empty;
        public int Version;
        public int ID;
        public int Private;
        public byte[] Data = new byte[0];

        public MFAExtensionObject()
        {
            ChunkName = "MFAExtensionObject";
        }

        public override void ReadMFA(ByteReader reader, params object[] extraInfo)
        {
            base.ReadMFA(reader, extraInfo);

            Type = reader.ReadInt();
            if (Type == -1)
            {
                Name = reader.ReadAutoYuniversal();
                FileName = reader.ReadAutoYuniversal();
                Magic = reader.ReadUInt();
                SubType = reader.ReadAutoYuniversal();
            }

            uint RealSize = reader.ReadUInt();
            uint EndPositon = (uint)reader.Tell() + RealSize;
            int DataSize = reader.ReadInt();
            reader.Skip(4);
            Version = reader.ReadInt();
            ID = reader.ReadInt();
            Private = reader.ReadInt();
            Data = reader.ReadBytes(Math.Max(0, DataSize - 20));
            reader.Seek(EndPositon);
        }

        public override void WriteMFA(ByteWriter writer, params object[] extraInfo)
        {
            base.WriteMFA(writer, extraInfo);

            writer.WriteInt(Type);
            if (Type == -1)
            {
                writer.WriteAutoYunicode(Name);
                writer.WriteAutoYunicode(FileName);
                writer.WriteUInt(Magic);
                writer.WriteAutoYunicode(SubType);
            }

            int offset = 20 + (NebulaCore.Fusion == 1.5f ? 4 : 0);

            writer.WriteInt(Data.Length + offset);
            writer.WriteInt(Data.Length + offset);
            writer.WriteInt(-1);
            writer.WriteInt(Version);
            writer.WriteInt(ID);
            writer.WriteInt(Private);

            if (NebulaCore.Fusion == 1.5f)
                writer.Skip(4);

            writer.WriteBytes(Data);
        }
    }
}
