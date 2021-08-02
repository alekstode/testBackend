using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface
{
    public interface ISessionRepository
    {
        void InitSession(string session);
        bool IsSessionInited { get; }
    }
}
