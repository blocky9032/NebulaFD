﻿using SapphireD.Core.Data.Chunks.ObjectChunks.ObjectCommon.ObjectMovementDefinitions;
using SapphireD.Core.Memory;

namespace SapphireD.Core.Data.Chunks.ObjectChunks.ObjectCommon
{
    public class ObjectMovement : Chunk
    {
        public short Control;
        public short Type;
        public byte Move;
        public byte Opt;
        public int StartingDirection;
        public ObjectMovementDefinition MovementDefinition = new();

        public ObjectMovement()
        {
            ChunkName = "ObjectMovement";
        }

        public override void ReadCCN(ByteReader reader, params object[] extraInfo)
        {
            long StartOffset = (long)extraInfo[0];
            int NameOffset = reader.ReadInt();
            int MovementID = reader.ReadInt();
            int DataOffset = reader.ReadInt();
            int DataSize = reader.ReadInt();
            reader.Seek(StartOffset + DataOffset);

            Control = reader.ReadShort();
            Type = reader.ReadShort();
            Move = reader.ReadByte();
            Opt = reader.ReadByte();
            reader.Skip(2);
            StartingDirection = reader.ReadInt();

            switch (Type)
            {
                case 0:
                    MovementDefinition = new ObjectMovementStatic();
                    break;
                case 1:
                    MovementDefinition = new ObjectMovementMouse();
                    break;
                case 2:
                    MovementDefinition = new ObjectMovementRace();
                    break;
                case 3:
                    MovementDefinition = new ObjectMovementGeneric();
                    break;
                case 4:
                    MovementDefinition = new ObjectMovementBall();
                    break;
                case 5:
                    MovementDefinition = new ObjectMovementPath();
                    break;
                case 9:
                    MovementDefinition = new ObjectMovementPlatform();
                    break;
                case 14:
                    MovementDefinition = new ObjectMovementExtension();
                    long ReturnOffset = reader.Tell();
                    reader.Seek(StartOffset + NameOffset);
                    ((ObjectMovementExtension)MovementDefinition).FileName = reader.ReadYuniversal();
                    ((ObjectMovementExtension)MovementDefinition).ID = MovementID;
                    reader.Seek(ReturnOffset);
                    break;
            }

            MovementDefinition.ReadCCN(reader, DataSize - 12, Control, Type, Move, Opt, StartingDirection);
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