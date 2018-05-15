using System;
using System.Collections.Generic;
using System.Linq;
using InfiCoreDojo.Domain;

namespace InfiCoreDojo.DataAccess.JsonFiles
{
    public class JsonFileLevelDal : JsonFileDal<Level>, ILevelDal
    {
        public JsonFileLevelDal() : base(@"levels.json") {}
    }
}
