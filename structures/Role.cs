using ShitRP.structures.interfaces;
using ShitRP.util;

namespace ShitRP.structures
{
    public class Role : IRole
    {
        protected Permission permissions;

        public Permission getPermissions()
        {
            return this.permissions;
        }
    }
}
