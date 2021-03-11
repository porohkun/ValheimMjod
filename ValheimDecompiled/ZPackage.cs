// Decompiled with JetBrains decompiler
// Type: ZPackage
// Assembly: assembly_valheim, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F48D6A22-6962-45BF-8D82-0AAD6AFA4FDB
// Assembly location: D:\Games\Steam\steamapps\common\Valheim\valheim_Data\Managed\assembly_valheim.dll

using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class ZPackage
{
    private MemoryStream m_stream = new MemoryStream();
    private BinaryWriter m_writer;
    private BinaryReader m_reader;

    public ZPackage()
    {
        this.m_writer = new BinaryWriter((Stream)this.m_stream);
        this.m_reader = new BinaryReader((Stream)this.m_stream);
    }

    public ZPackage(string base64String)
    {
        this.m_writer = new BinaryWriter((Stream)this.m_stream);
        this.m_reader = new BinaryReader((Stream)this.m_stream);
        if (string.IsNullOrEmpty(base64String))
            return;
        byte[] buffer = Convert.FromBase64String(base64String);
        this.m_stream.Write(buffer, 0, buffer.Length);
        this.m_stream.Position = 0L;
    }

    public ZPackage(byte[] data)
    {
        this.m_writer = new BinaryWriter((Stream)this.m_stream);
        this.m_reader = new BinaryReader((Stream)this.m_stream);
        this.m_stream.Write(data, 0, data.Length);
        this.m_stream.Position = 0L;
    }

    public ZPackage(byte[] data, int dataSize)
    {
        this.m_writer = new BinaryWriter((Stream)this.m_stream);
        this.m_reader = new BinaryReader((Stream)this.m_stream);
        this.m_stream.Write(data, 0, dataSize);
        this.m_stream.Position = 0L;
    }

    public void Load(byte[] data)
    {
        this.Clear();
        this.m_stream.Write(data, 0, data.Length);
        this.m_stream.Position = 0L;
    }

    public void Write(ZPackage pkg)
    {
        byte[] array = pkg.GetArray();
        this.m_writer.Write(array.Length);
        this.m_writer.Write(array);
    }

    public void Write(byte[] array)
    {
        this.m_writer.Write(array.Length);
        this.m_writer.Write(array);
    }

    public void Write(byte data)
    {
        this.m_writer.Write(data);
    }

    public void Write(sbyte data)
    {
        this.m_writer.Write(data);
    }

    public void Write(char data)
    {
        this.m_writer.Write(data);
    }

    public void Write(bool data)
    {
        this.m_writer.Write(data);
    }

    public void Write(int data)
    {
        this.m_writer.Write(data);
    }

    public void Write(uint data)
    {
        this.m_writer.Write(data);
    }

    public void Write(ulong data)
    {
        this.m_writer.Write(data);
    }

    public void Write(long data)
    {
        this.m_writer.Write(data);
    }

    public void Write(float data)
    {
        this.m_writer.Write(data);
    }

    public void Write(double data)
    {
        this.m_writer.Write(data);
    }

    public void Write(string data)
    {
        this.m_writer.Write(data);
    }

    public void Write(ZDOID id)
    {
        this.m_writer.Write(id.userID);
        this.m_writer.Write(id.id);
    }

    public void Write(Vector3 v3)
    {
        this.m_writer.Write(v3.x);
        this.m_writer.Write(v3.y);
        this.m_writer.Write(v3.z);
    }

    public void Write(Vector2i v2)
    {
        this.m_writer.Write(v2.x);
        this.m_writer.Write(v2.y);
    }

    public void Write(Quaternion q)
    {
        this.m_writer.Write(q.x);
        this.m_writer.Write(q.y);
        this.m_writer.Write(q.z);
        this.m_writer.Write(q.w);
    }

    public ZDOID ReadZDOID()
    {
        return new ZDOID(this.m_reader.ReadInt64(), this.m_reader.ReadUInt32());
    }

    public bool ReadBool()
    {
        return this.m_reader.ReadBoolean();
    }

    public char ReadChar()
    {
        return this.m_reader.ReadChar();
    }

    public byte ReadByte()
    {
        return this.m_reader.ReadByte();
    }

    public sbyte ReadSByte()
    {
        return this.m_reader.ReadSByte();
    }

    public int ReadInt()
    {
        return this.m_reader.ReadInt32();
    }

    public uint ReadUInt()
    {
        return this.m_reader.ReadUInt32();
    }

    public long ReadLong()
    {
        return this.m_reader.ReadInt64();
    }

    public ulong ReadULong()
    {
        return this.m_reader.ReadUInt64();
    }

    public float ReadSingle()
    {
        return this.m_reader.ReadSingle();
    }

    public double ReadDouble()
    {
        return this.m_reader.ReadDouble();
    }

    public string ReadString()
    {
        return this.m_reader.ReadString();
    }

    public Vector3 ReadVector3()
    {
        return new Vector3()
        {
            x = this.m_reader.ReadSingle(),
            y = this.m_reader.ReadSingle(),
            z = this.m_reader.ReadSingle()
        };
    }

    public Vector2i ReadVector2i()
    {
        return new Vector2i()
        {
            x = this.m_reader.ReadInt32(),
            y = this.m_reader.ReadInt32()
        };
    }

    public Quaternion ReadQuaternion()
    {
        return new Quaternion()
        {
            x = this.m_reader.ReadSingle(),
            y = this.m_reader.ReadSingle(),
            z = this.m_reader.ReadSingle(),
            w = this.m_reader.ReadSingle()
        };
    }

    public ZPackage ReadPackage()
    {
        return new ZPackage(this.m_reader.ReadBytes(this.m_reader.ReadInt32()));
    }

    public void ReadPackage(ref ZPackage pkg)
    {
        byte[] buffer = this.m_reader.ReadBytes(this.m_reader.ReadInt32());
        pkg.Clear();
        pkg.m_stream.Write(buffer, 0, buffer.Length);
        pkg.m_stream.Position = 0L;
    }

    public byte[] ReadByteArray()
    {
        return this.m_reader.ReadBytes(this.m_reader.ReadInt32());
    }

    public string GetBase64()
    {
        return Convert.ToBase64String(this.GetArray());
    }

    public byte[] GetArray()
    {
        this.m_writer.Flush();
        this.m_stream.Flush();
        return this.m_stream.ToArray();
    }

    public void SetPos(int pos)
    {
        this.m_stream.Position = (long)pos;
    }

    public int GetPos()
    {
        return (int)this.m_stream.Position;
    }

    public int Size()
    {
        this.m_writer.Flush();
        this.m_stream.Flush();
        return (int)this.m_stream.Length;
    }

    public void Clear()
    {
        this.m_writer.Flush();
        this.m_stream.SetLength(0L);
        this.m_stream.Position = 0L;
    }

    public byte[] GenerateHash()
    {
        return SHA512.Create().ComputeHash(this.GetArray());
    }
}
