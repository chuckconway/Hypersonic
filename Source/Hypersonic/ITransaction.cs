using System;
using System.Data;

namespace Hypersonic
{
    public interface ITransaction : IDbTransaction
    {
        void Begin(IsolationLevel isoLationLevel);

    }
}