using System;
using System.IO;

namespace Valheim
{
    public struct ZDOID : IEquatable<ZDOID>
    {
        public static ZDOID None = new ZDOID(0L, 0U);
        private long m_userID;
        private uint m_id;
        private int m_hash;

        public ZDOID(BinaryReader reader)
        {
            this.m_userID = reader.ReadInt64();
            this.m_id = reader.ReadUInt32();
            this.m_hash = 0;
        }

        public ZDOID(long userID, uint id)
        {
            this.m_userID = userID;
            this.m_id = id;
            this.m_hash = 0;
        }

        public ZDOID(ZDOID other)
        {
            this.m_userID = other.m_userID;
            this.m_id = other.m_id;
            this.m_hash = other.m_hash;
        }

        public override string ToString()
        {
            return this.m_userID.ToString() + ":" + this.m_id.ToString();
        }

        public static bool operator ==(ZDOID a, ZDOID b)
        {
            return a.m_userID == b.m_userID && (int)a.m_id == (int)b.m_id;
        }

        public static bool operator !=(ZDOID a, ZDOID b)
        {
            return a.m_userID != b.m_userID || (int)a.m_id != (int)b.m_id;
        }

        public bool Equals(ZDOID other)
        {
            return other.m_userID == this.m_userID && (int)other.m_id == (int)this.m_id;
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is ZDOID zdoid && zdoid.m_userID == this.m_userID && (int)zdoid.m_id == (int)this.m_id;
        }

        public override int GetHashCode()
        {
            if (this.m_hash == 0)
                this.m_hash = this.m_userID.GetHashCode() ^ this.m_id.GetHashCode();
            return this.m_hash;
        }

        public bool IsNone()
        {
            return this.m_userID == 0L && this.m_id == 0U;
        }

        public long userID
        {
            get
            {
                return this.m_userID;
            }
        }

        public uint id
        {
            get
            {
                return this.m_id;
            }
        }
    }

}
