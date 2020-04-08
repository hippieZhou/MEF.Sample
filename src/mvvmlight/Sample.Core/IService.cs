using System;

namespace Sample.Core
{
    public interface IService
    {
        void QueryData(int numuber, Action<int> action);
    }
}