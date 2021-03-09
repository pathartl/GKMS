using System;

namespace GKMS.Common
{
    public interface IGame
    {
        string Name { get; }

        string GetKey();
        void ChangeKey(string newKey);

        string[] Keys { get; set; }
    }
}
