using Ninject.Modules;
using SimpleBookKeeping.Unility;
using SimpleBookKeeping.Unility.Interfaces;

namespace SimpleBookKeeping
{
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IHashCalculator>().To<Md5HashCalculator>();
        }
    }
}