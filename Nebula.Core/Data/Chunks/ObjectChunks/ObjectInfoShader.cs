﻿using Nebula.Core.Data.Chunks.BankChunks.Shaders;
using Nebula.Core.Memory;
using Nebula.Core.Utilities;

namespace Nebula.Core.Data.Chunks.ObjectChunks
{
    public class ObjectInfoShader : Chunk
    {
        public int? ShaderHandle;
        public int[] ShaderParameters = new int[0];

        public ObjectInfoShader()
        {
            ChunkName = "ObjectInfoShader";
            ChunkID = 0x4448;
        }

        public override void ReadCCN(ByteReader reader, params object[] extraInfo)
        {
            ShaderHandle = reader.ReadInt();
            int paramN = NebulaCore.PackageData.ShaderBank.Shaders[(int)ShaderHandle!].Parameters.Length;
            int fakeN = reader.ReadInt();
            ShaderParameters = new int[paramN];
            reader.Skip((fakeN - paramN) * 4);

            for (int i = 0; i < paramN; i++)
                ShaderParameters[i] = reader.ReadInt();

            ((ObjectInfo)extraInfo[0]).Shader = this;
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
