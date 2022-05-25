﻿namespace Project_1.Aids
{
    public static class GetNamespace
    {
        public static string? OfType(object? obj)
            => Safe.Run(() => obj?.GetType()?.Namespace ?? string.Empty, string.Empty);
    }
}