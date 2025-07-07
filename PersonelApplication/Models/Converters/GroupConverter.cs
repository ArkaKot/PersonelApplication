using PersonelApplication.Models.Domains;
using PersonelApplication.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelApplication.Models.Converters
{
    public static class GroupConverter
    {
        public static GroupWrapper ToWrapper(this Group model)
        {
            return new GroupWrapper
            {

                Id = model.Id,
                Name = model.Name,
         
            };

        }

        public static Group ToDao(this GroupWrapper model)
        {
            return new Group
            {
                //Id = model.Id,
                Name = model.Name,

            };

        }
    }
}
