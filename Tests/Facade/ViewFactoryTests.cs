using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data;
using Project_1.Domain;
using Project_1.Facade;


namespace Project_1.Tests.Facade
{
    public abstract class ViewFactoryTests<TFactory, TView, TObj, TData> 
        : SealedClassTests<TFactory, BaseViewFactory<TView, TObj, TData>>
        where TFactory : BaseViewFactory<TView, TObj, TData>, new() 
        where TView : class, new() 
        where TData : UniqueData, new() 
        where TObj : UniqueEntity<TData> {
        [TestMethod] public virtual void CreateTest() { }
        [TestMethod] public virtual void CreateViewTest() {
            var v = GetRandom.Value<TView>();
            var o = obj.Create(v);
            areEqualProperties(v, o.Data);
        }
        [TestMethod] public virtual void CreateObjectTest() {
            var d = GetRandom.Value<TData>();
            var v = obj.Create(toObject(d));
            areEqualProperties(d, v);
        }
        [TestMethod] public virtual void CreateUndefinedTest()
        {
            var d = GetRandom.Value<TData>();
            var v = obj.CreateUndefined(toObject(d));
            areEqualProperties(d, v);
        }
        protected abstract TObj toObject(TData d);
    }
}
