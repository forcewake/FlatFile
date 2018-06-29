//namespace FlatFile.Tests.Specifications.Extensions
//{
//    using System;
//    using System.Linq.Expressions;
//    using FlatFile.Core.Extensions;
//    using TechTalk.SpecFlow;

//    public static class ScenarioContextExtensions
//    {
//        public static void Add<T>(this ScenarioContext context, Expression<Func<T>> expression, object value)
//        {
//            var name = expression.GetMemberName();
//            context.Add(name, value);
//        }

//        public static bool TryGetValue<T>(this ScenarioContext context, Expression<Func<T>> expression, out T value)
//        {
//            var name = expression.GetMemberName();
//            return context.TryGetValue(name, out value);
//        }
//    }
//}