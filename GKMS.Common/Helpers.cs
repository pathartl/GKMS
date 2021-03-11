using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GKMS.Common
{
    public static class Helpers
    {
        public static IEnumerable<Type> GetSupportedGameTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IGame).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).ToList();
        }
    }
}
