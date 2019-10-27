using System;
using System.Collections.Generic;

namespace Thunder.Platform.Core.Context
{
    public class UserContextContents
    {
        public UserContextContents(IReadOnlyDictionary<string, object> nameToValueMap)
        {
            NameToValueMap = nameToValueMap ?? throw new ArgumentNullException(nameof(nameToValueMap));
        }

        public IReadOnlyDictionary<string, object> NameToValueMap
        {
            get;
        }
    }
}
