namespace Be.Vlaanderen.Basisregisters.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ToStringBuilder
    {
        // T.ToString(t => ....)
        public static string? ToString<T>(
            this T x,
            Func<T, IEnumerable<object>> stringValuesFunc)
            => x.ToString(stringValuesFunc, null);

        public static string? ToString<T>(
            this T x,
            Func<T, IEnumerable<object>> stringValuesFunc,
            Func<object, object, object>? valueConcatFunc)
            => ToString(stringValuesFunc(x), valueConcatFunc);

        public static string? ToString(
            Func<IEnumerable<object>> stringValuesFunc)
            => ToString(stringValuesFunc(), (Func<object, object, object>?) null);

        public static string? ToString(
            IEnumerable<object> stringValues)
            => ToString(stringValues, (Func<object, object, object>?) null);

        public static string? ToString(
            IEnumerable<object> stringValuesFunc,
            Func<object, object, object>? valueConcatFunc)
        {
            object StringAggregator(object l, object r)
            {
                if (valueConcatFunc == null)
                    valueConcatFunc = (l1, r1) => $"{l1}, {r1}";

                return valueConcatFunc(l, r);
            }

            return stringValuesFunc.Aggregate(StringAggregator).ToString();
        }
    }
}
